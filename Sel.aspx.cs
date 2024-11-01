using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Text;
using DataSet1TableAdapters;

public partial class Sel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
	Counter.Counter.fn_Account();
        clsFunction.Check.CheckSession();
        if (this.IsPostBack == false)
        {
            string ct1 = Convert.ToString(Request.QueryString["CT1"]);
            if (!string.IsNullOrEmpty(ct1))
            {
                TextBox1.Text = ct1;
            }
            TextBox1.Focus();
            LU1();
        }
    }
    //取得資料
	private void LU1()
	{
		string strSql = "";
		strSql += " SELECT TR10.Number as 自動編號";
		strSql += " , TR10.netcustnumb as 網路報名單號";
		strSql += " , CONVERT(varchar(12) , TR10.EnliI_Date, 111) as 報名日期";
		strSql += " , TR10.Comp_Conn as 聯絡人";
		strSql += " , left(trip.dbo.grop.grop_name,15) as 團名";
		strSql += " , isnull(sum(isnull(TR20.Tour_Mony,0)),0) as 團費總額";
		strSql += " , TR10.BookPax as 報名人數";
		strSql += " FROM TR10";
		strSql += " left join trip.dbo.grop on trip.dbo.grop.grop_numb = tr10.tour_numb";
		strSql += " left join tr20 on tr20.tr10number = tr10.number";
		strSql += " where 1=1";
		switch (this.DropDownList1.SelectedValue) {
			//Case "1"
			//    strSql &= " and TR10.CUST_NUMB like ?"
			case "2":
				strSql += " and trip.dbo.grop.grop_name like ?";
				break;
			case "3":
				strSql += " and CONVERT(varchar(100), TR10.EnliI_Date, 111) = ?";
				break;
			case "4":
				strSql += " and TR10.netcustnumb like ?";
				break;
			//Case "5"
			//    strSql &= " and TR10.Comp_Code like ?"
			//Case "6"
			//    strSql &= " and TR10.Comp_Conn like ?"
		}
		//strSql &= " and tr10.Comp_Code = '" & Session("compno") & "' "
		strSql += " and tr10.Comp_Conn = '" + Session["PerName"] + "' ";
		strSql += " group by TR10.Cust_Numb, TR10.netcustnumb, TR10.EnliI_Date";
		strSql += " , TR10.Comp_Code, TR10.Comp_Conn, TR10.Tour_Numb";
		strSql += " , TR10.BookPax, TR10.Number";
		strSql += " , trip.dbo.grop.grop_name";
		strSql += " order by TR10.cust_numb desc";
		string strConnection = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BOleDBConnectionString"].ConnectionString;
		System.Data.OleDb.OleDbConnection strConn = new System.Data.OleDb.OleDbConnection(strConnection);
		strConn.Open();
		System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(strSql, strConnection);
		switch (this.DropDownList1.SelectedValue) {
			//Case "1"
			//    da.SelectCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("@CUST_NUMB", "%" & TextBox1.Text.Trim() & "%"))

			case "2":
				da.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@grop_name", "%" + TextBox1.Text.Trim() + "%"));
				break;
			case "3":
				string aa = "";
				if (clsFunction.Check.IsDate(TextBox1.Text) == true) {
					aa = TextBox1.Text;
				} else {
					aa = "1902/1/1";
				}
				da.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@EnliI_Date", aa));
				break;
			case "4":
				da.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@netcustnumb", "%" + TextBox1.Text.Trim() + "%"));
				break;
			//Case "5"
			//    da.SelectCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("@Comp_Code", "%" & TextBox1.Text.Trim() & "%"))
			//Case "6"
			//    da.SelectCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("@Comp_Conn", "%" & TextBox1.Text.Trim() & "%"))
			//Case "7"
			//    da.SelectCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("@name2", "%" & TextBox1.Text.Trim() & "%"))
		}

		DataTable dt = new DataTable();
		da.Fill(dt);
		strConn.Close();

		GridView1.Width = 700;
		GridView1.AllowPaging = true;
		GridView1.AllowSorting = true;
		GridView1.PageSize = 20;

		GridView1.DataSource = dt;
		GridView1.DataKeyNames = new string[] { "自動編號" };
		GridView1.DataBind();

		try {


		} catch {
			Response.Write("資料庫維護中，請稍後");
		}
	}

	//報名狀態
	public string fn_Return_Charge_Examine(string check, string trip_no)
	{
		string strExamine = "";
        string total = "";
        string op_check = "";
        string money_in = "";
        int GroupChange = 0;
        int cancel = 0;
        string strsql = "";

		string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRANOleDBConnectionString"].ToString();
		System.Data.OleDb.OleDbConnection connect = new System.Data.OleDb.OleDbConnection(constring);
		connect.Open();

        strsql = " select group_change, del_data, tr10.bookpax, num, CANC_PEOL, isnull((select sum(cancel_mony) from tr20 where cust_numb = '" + trip_no + "'),0) as cancel_mony ";
		strsql += " from tr10 ";
		strsql += " left join glb.dbo.tour on tr10.tour_numb = tour.num ";
        strsql += " where cust_numb = '" + trip_no + "' ";
		System.Data.OleDb.OleDbCommand command_c = new System.Data.OleDb.OleDbCommand(strsql, connect);
		System.Data.OleDb.OleDbDataReader reader_c = command_c.ExecuteReader();
		if (reader_c.Read()) {
			//If Not String.IsNullOrEmpty(reader_c("group_change").ToString) Then
			//    GroupChange = 1
			//End If
			cancel = Convert.ToInt16(reader_c["cancel_mony"].ToString());
			if (reader_c["CANC_PEOL"].ToString() == "0" & !string.IsNullOrEmpty(reader_c["CANC_PEOL"].ToString())) {
				if (string.IsNullOrEmpty(reader_c["group_change"].ToString()) & string.IsNullOrEmpty(reader_c["del_data"].ToString())) {
                    switch (check)
                    {
						case "":
							strExamine = "未審核";
							break;
						case "0":
							strExamine = "未審核";
							break;
						case "1":
							strExamine = "<span style='color:Blue;'>審核中</span>";
							break;
						case "2":
							strsql = " select count(cust_numb) as tt ";
							strsql += " FROM TR20 ";
                            strsql += " where tr20.cust_numb = '" + trip_no + "' ";
							strsql += " and ar_check = 'true' ";
							strsql += " and isnull(del_data,'')='' ";
							strsql += " and isnull(Group_Change,'')='' ";

							System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(strsql, connect);
							System.Data.OleDb.OleDbDataReader reader = command.ExecuteReader();
							if (reader.Read()) {
								total = reader["tt"].ToString();
							}
							reader.Close();
							command.Dispose();

							strsql = " select count(cust_numb) as tt ";
							strsql += " FROM TR20 ";
                            strsql += " where tr20.cust_numb = '" + trip_no + "' ";
							strsql += " and Reserve = 0 ";
							strsql += " and isnull(del_data,'')='' ";
							strsql += " and isnull(Group_Change,'')='' ";

							System.Data.OleDb.OleDbCommand command2 = new System.Data.OleDb.OleDbCommand(strsql, connect);
							System.Data.OleDb.OleDbDataReader reader2 = command2.ExecuteReader();
							int Reserve = 0;
							if (reader2.Read()) {
								Reserve = Convert.ToInt16(reader2["tt"].ToString());
							}
							reader2.Close();
							command2.Dispose();

							if (Reserve <= 0) {
								if (Convert.ToInt16(total) < Convert.ToInt16(reader_c["bookpax"].ToString())) {
									strExamine = "<span style='color:#FA8000;'>請收訂收件</span>";
								}
								if (total == reader_c["bookpax"]) {
									strExamine = "<span style='color:#FA8000;'>請收件</span>";
								}
							} else {
								strExamine = "<span style='color:Blue;'>候補</span>";
							}

							break;
						//strsql = " select op_check from tr10 where cust_numb = '" & 報名單號 & "'  "
						//Dim command2 As New Data.OleDb.OleDbCommand(strsql, connect)
						//Dim reader2 As Data.OleDb.OleDbDataReader = command2.ExecuteReader()
						//If reader2.Read() Then
						//    If reader2("op_check") = True Then
						//        op_check = 1
						//    End If
						//End If
						//reader2.Close()
						//command2.Dispose()

						//If op_check = 1 Then
						//    strExamine = "<span style='color:#FA8000;'>ok</span>"
						//End If

						case "3":
							strExamine = "<span style='color:Blue;'>異動審核中</span>";
							break;
						case "4":
							strExamine = "<span style='color:Blue;'>轉團審核中</span>";
							break;
					}

				} else if (!string.IsNullOrEmpty(reader_c["del_data"].ToString())) {
					if (cancel > 0) {
						strExamine = "<span style='color:red;'>已刪除($" + cancel + ")</span>";
					} else {
						strExamine = "<span style='color:red;'>已刪除</span>";
					}
				} else if (!string.IsNullOrEmpty(reader_c["group_change"].ToString())) {
					if (cancel > 0) {
						strExamine = "<span style='color:red;'>已轉團($" + cancel + ")</span>";
					} else {
						strExamine = "<span style='color:red;'>已轉團</span>";
					}
				}
			} else {
				strExamine = "<span style='color:#888;'>團體取消</span>";
			}


		}
		reader_c.Close();
		command_c.Dispose();

		connect.Dispose();

		return strExamine;
	}

    public string fn_Return_KeyIn(string trip_no)
	{
		string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRANOleDBConnectionString"].ToString();
		System.Data.OleDb.OleDbConnection connect = new System.Data.OleDb.OleDbConnection(constring);
		connect.Open();
		string strExamine = "";
		string strsql = "";
		strsql = " select isnull(OP_Check,0) as OP_Check ";
		strsql += " FROM TR10 ";
        strsql += " where cust_numb = '" + trip_no + "' ";
		System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(strsql, connect);
		System.Data.OleDb.OleDbDataReader reader = command.ExecuteReader();

		if (reader.Read()) {
			if (reader["OP_Check"].ToString() == "0") {
				strsql = "SELECT tr20.Cust_Idno, tr20.Cust_Name,  ";
				strsql += " cu10.Birt_date, cu10.Engl_cust, ";
				strsql += " cu10.Pass_NO, cu10.Pass_Issu, cu10.Pass_Vali ";
				strsql += " FROM TR20 ";
				strsql += " left join glb.dbo.cu10 on tr20.cu10number = glb.dbo.cu10.number ";
                strsql += " where tr20.Cust_numb = '" + trip_no + "' ";
				System.Data.OleDb.OleDbCommand command2 = new System.Data.OleDb.OleDbCommand(strsql, connect);
				System.Data.OleDb.OleDbDataReader reader2 = command2.ExecuteReader();
				int check_null = 1;
				if (reader2.HasRows == false) {
					check_null = 0;
				}
				while (reader2.Read()) {
					//判斷每個欄位是否有空值
					if (string.IsNullOrEmpty(reader2["Cust_Idno"].ToString())) {
						check_null = 0;
					}
					if (string.IsNullOrEmpty(reader2["Cust_Name"].ToString())) {
						check_null = 0;
					}
					if (string.IsNullOrEmpty(reader2["Birt_date"].ToString())) {
						check_null = 0;
					}
					if (string.IsNullOrEmpty(reader2["Engl_cust"].ToString())) {
						check_null = 0;
					}
					if (string.IsNullOrEmpty(reader2["Pass_NO"].ToString())) {
						check_null = 0;
					}
					if (string.IsNullOrEmpty(reader2["Pass_Issu"].ToString())) {
						check_null = 0;
					}
					if (string.IsNullOrEmpty(reader2["Pass_Vali"].ToString())) {
						check_null = 0;
					}
				}
				reader2.Close();
				command2.Dispose();

				if (check_null == 0) {
					strExamine = "●";
				} else {
					strExamine = "☆";
				}
			} else {
				strExamine = "㊣";
			}

		}
		reader.Close();
		command.Dispose();
		connect.Dispose();

		//strExamine = 報名單號

		return strExamine;
	}

		//因第0個是隱藏的按鈕，第1個是自動編號
	private const int _firstEditCellIndex = 2;

	protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
	{
		GridView _gridView = (GridView)sender;
		switch ((e.CommandName)) {
			case "SingleClick":

				int _rowIndex = int.Parse(e.CommandArgument.ToString());

				int _columnIndex = int.Parse(Request.Form["__EVENTARGUMENT"]);

				_gridView.SelectedIndex = _rowIndex;

				Control _editControl = _gridView.Rows[_rowIndex].Cells[1].Controls[1];
				string aa = ((Label)_editControl).Text;

				Response.Redirect("DataList.aspx?Nid=" + aa.ToString());

				break;
			//Dim _editControl2 As Control = _gridView.Rows(_rowIndex).Cells(2).Controls(1)
			//Dim aa2 As String = DirectCast(_editControl2, Label).Text()

			//If aa2 = "2" Then
			//    Response.Redirect("Group_TRGop_edit_ticket.aspx?Nid=" & aa)
			//Else
			//    Response.Redirect("Group_TRGop_edit.aspx?Nid=" & aa)
			//End If

		}
	}

	//隱藏自動編號
	//原本e.Row.Cells()設定是3,4  因多加審核更改成4,5,6  1/17 by 阿信
	protected void GridView1_RowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow | e.Row.RowType == DataControlRowType.Header) {
			e.Row.Cells[2].Visible = false;
			//e.Row.Cells(4).Visible = False
			//e.Row.Cells(5).Visible = False
			//e.Row.Cells(6).Visible = False
			//e.Row.Cells(7).Visible = False
		}
	}

	//將每列資料加上javascript，讓使用者可以直接按
	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
	{

		if ((e.Row.RowType == DataControlRowType.DataRow)) {
			LinkButton _singleClickButton = (LinkButton)e.Row.Cells[0].Controls[0];
			string _jsSingle = ClientScript.GetPostBackClientHyperlink(_singleClickButton, "");

			int columnIndex = _firstEditCellIndex;

			e.Row.Attributes.Add("onMouseOver", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#6699ff';");
			e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=currentcolor;");


			while ((columnIndex < e.Row.Cells.Count)) {
				string js = _jsSingle.Insert((_jsSingle.Length - 2), columnIndex.ToString());
				e.Row.Cells[columnIndex].Attributes["onclick"] = js;
				e.Row.Cells[columnIndex].Attributes["style"] = (e.Row.Cells[columnIndex].Attributes["style"] + "cursor:pointer;cursor:hand;");

				columnIndex = (columnIndex + 1);
			}
		}
	}

	//註冊javascript，以免驗證的時侯出錯

	protected override void Render(HtmlTextWriter writer)
	{
		foreach (GridViewRow r in GridView1.Rows) {
			if ((r.RowType == DataControlRowType.DataRow)) {
				int columnIndex = _firstEditCellIndex;
				while ((columnIndex < r.Cells.Count)) {
					Page.ClientScript.RegisterForEventValidation((r.UniqueID + "$ctl00"), columnIndex.ToString());
					columnIndex = (columnIndex + 1);
				}
			}
		}

		base.Render(writer);
	}

	//換頁
	protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
	{
		LU1();
		GridView1.DataSource = SortDataTable(GridView1.DataSource as DataTable, true);
		GridView1.PageIndex = e.NewPageIndex;
		GridView1.DataBind();

		Response.Cookies["search_page_trgop"].Value = HttpUtility.UrlEncode(e.NewPageIndex.ToString());
        Response.Cookies["search_page_trgop"].Expires = System.DateTime.Now.AddDays(1);
		//使搜尋條件可以存活一天
	}

	//排序
	protected void GridView1_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
	{
		LU1();
		GridViewSortExpression = e.SortExpression;
		int pageIndex = GridView1.PageIndex;
		GridView1.DataSource = SortDataTable(GridView1.DataSource as DataTable, false);
		GridView1.DataBind();
		GridView1.PageIndex = pageIndex;
	}

	private string GetSortDirection()
	{
		switch ((GridViewSortDirection)) {
			case "ASC":
				GridViewSortDirection = "DESC";
				break;
			case "DESC":
				GridViewSortDirection = "ASC";
				break;
		}
		return GridViewSortDirection;
	}

	protected DataView SortDataTable(DataTable dataTable, bool isPageIndexChanging)
	{
		if ((dataTable != null)) {
			DataView dataView = new DataView(dataTable);
			if (GridViewSortExpression != string.Empty) {
				if (isPageIndexChanging) {
					dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GridViewSortDirection);
				} else {
					dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GetSortDirection());
				}
			}
			return dataView;
		} else {
			return new DataView();
		}
	}

	private string GridViewSortExpression {
		get { return (ViewState["GridViewSortExpression"] as string == null ? string.Empty : ViewState["GridViewSortExpression"] as string); }
		set { ViewState["GridViewSortExpression"] = value; }
	}

	private string GridViewSortDirection {
		get { return (ViewState["SortDirection"] as string == null ? "ASC" : ViewState["SortDirection"] as string); }
		set { ViewState["SortDirection"] = value; }
	}

	protected void Page_Unload(object sender, System.EventArgs e)
	{
		this.Dispose();
		base.Dispose();
		this.GridView1.Dispose();
	}

	protected void Button1_Click(object sender, System.EventArgs e)
	{
		LU1();

		//儲存搜尋條件
		Response.Cookies["search_dropdown_trgop"].Value = HttpUtility.UrlEncode(this.DropDownList1.SelectedValue);
        Response.Cookies["search_dropdown_trgop"].Expires = System.DateTime.Now.AddDays(1);
		//使搜尋條件可以存活一天

		Response.Cookies["search_keyword_trgop"].Value = HttpUtility.UrlEncode(TextBox1.Text);
        Response.Cookies["search_keyword_trgop"].Expires = System.DateTime.Now.AddDays(1);
		//使搜尋條件可以存活一天

		Response.Cookies["search_page_trgop"].Value = HttpUtility.UrlEncode(this.GridView1.PageIndex.ToString());
        Response.Cookies["search_page_trgop"].Expires = System.DateTime.Now.AddDays(1);
		//使搜尋條件可以存活一天
	}
	public void Index_Sel()
	{
		Unload += Page_Unload;
		Load += Page_Load;
	}
}
