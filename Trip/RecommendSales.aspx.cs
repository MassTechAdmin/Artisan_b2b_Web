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
using System.Data.SqlClient;
using System.EnterpriseServices.Internal;

public partial class RecommendSales : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsFunction.Check.Check_Login_ID_And_PW();
        if (this.IsPostBack == false)
        {
            fn_DropDownList_Area();
            LU1();
        }
    }
    private const int _firstEditCellIndex = 2;
    private void LU1()
    {
        string strSql = "";
        strSql += " select RecommendSales.*";
        strSql += " ,(case when RecommendSales.Rec_Area = '90002' then N'中國' else Area.Area_Name end) as Area_Name";
        strSql += " ,(case when RecommendSales.Rec_GroupName = '1' then N'巨匠推薦行程' else N'日本精選' end) as GroupName";
        strSql += " from RecommendSales";
        strSql += " left join trip.dbo.area on area.area_no = RecommendSales.Rec_Area";
        //strSql += " where Rec_Area = " + DropDownList1.SelectedValue + "";
        //strSql += " where Rec_GroupName = '" + ddlGCName.SelectedValue + "'";
        if (DropDownList1.SelectedIndex != 0)
        {
            strSql += " where Rec_Area =" + DropDownList1.SelectedValue + "";
        }
        strSql += " order by RecommendSales.Rec_GroupName,RecommendSales.Rec_Order";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand cmd = new SqlCommand(strSql, connect);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmd.Dispose();
            connect.Close();

            GridView1.DataSource = dt;
            GridView1.DataKeyNames = new string[] { "Rec_No" };
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write("<script language='javascript' type='text/javascript'>alert('網站或資料庫維護中，請稍侯在試！'); history.back();</script>");
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        switch (e.CommandName)
        {
            case ("SingleClick"):
                int _rowIndex = int.Parse(e.CommandArgument.ToString());
                int _columnIndex = int.Parse(Request.Form["__EVENTARGUMENT"]);
                _gridView.SelectedIndex = _rowIndex;
                Control _editControl = _gridView.SelectedRow.Cells[1].Controls[1];
                string aa = ((Label)_editControl).Text;
                Response.Redirect("RecommendSales_Edit.aspx?no=" + aa);
                break;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(constring);
        try
        {
            connect.Open();
            for (int ii = 0; ii < GridView1.Rows.Count; ii++)
            {
                string strSql = "Update RecommendSales set Rec_Order= @Rec_Order";
                strSql += " Where Rec_No=@Rec_No";
                TextBox TxbOrder = (TextBox)GridView1.Rows[ii].FindControl("TxbOrder");
                HiddenField HidKeyWord_No = (HiddenField)GridView1.Rows[ii].FindControl("HidKeyWord_No");
                SqlCommand cmd = new SqlCommand(strSql, connect);
                cmd.Parameters.Add(new SqlParameter("@Rec_Order", TxbOrder.Text.Trim()));
                cmd.Parameters.Add(new SqlParameter("@Rec_No", HidKeyWord_No.Value));
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            LU1();
        }
        finally
        {
            connect.Close();
        }
        return;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton _singleClickButton = (LinkButton)e.Row.Cells[0].Controls[0];
            string _jsSingle = ClientScript.GetPostBackClientHyperlink(_singleClickButton, "");

            e.Row.Attributes.Add("onMouseOver", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#6699ff';");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=currentcolor;");

            e.Row.Cells[2].Width = 70;

            for (int columnIndex = _firstEditCellIndex; columnIndex < e.Row.Cells.Count; columnIndex++)
            {
                string js = _jsSingle.Insert(_jsSingle.Length - 2, columnIndex.ToString());
                e.Row.Cells[columnIndex].Attributes["onclick"] = js;
                e.Row.Cells[columnIndex].Attributes["style"] += "cursor:pointer;cursor:hand;";
            }
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        LU1();
        GridView1.DataSource = SortDataTable(GridView1.DataSource as DataTable, true);
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
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
        switch (GridViewSortDirection)
        {
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
        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            if (GridViewSortExpression != string.Empty)
            {
                if (isPageIndexChanging)
                {
                    dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GridViewSortDirection);
                }
                else
                {
                    dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GetSortDirection());
                }
            }
            return dataView;
        }
        else
        {
            return new DataView();
        }
    }

    private string GridViewSortExpression
    {
        get { return ViewState["SortExpression"] as string ?? string.Empty; }
        set { ViewState["SortExpression"] = value; }
    }

    private string GridViewSortDirection
    {
        get { return ViewState["SortDirection"] as string ?? "ASC"; }
        set { ViewState["SortDirection"] = value; }
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        this.GridView1.Dispose();
        base.Dispose();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        add_check();
    }

    private void add_check()
    {
        bool check = true;

        if (check == true)
        {
            add_data();
        }
    }

    private void add_data()
    {
        string strSql = "";
        strSql += " insert into RecommendSales (";
        strSql += " Rec_Title,Rec_Area,Rec_Order,Rec_GroupName";
        strSql += " ) VALUES (";
        strSql += " @Rec_Title,@Rec_Area,@Rec_Order,@Rec_GroupName";
        strSql += " )";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand comm = new SqlCommand(strSql, connect);

        comm.Parameters.Add(new SqlParameter("@Rec_Title", TextBox2.Text.Trim()));
        comm.Parameters.Add(new SqlParameter("@Rec_Area", DropDownList1.SelectedValue));
        comm.Parameters.Add(new SqlParameter("@Rec_Order", 999));
        comm.Parameters.Add(new SqlParameter("@Rec_GroupName", ddlGCName.SelectedValue));
        comm.ExecuteNonQuery();
        connect.Close();
        TextBox2.Text = "";

        LU1();
    }

    private string getpath()
    {
        string strpath = Server.MapPath(@"~\App_Data\ForStr.txt");

        return strpath;
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strKey = GridView1.DataKeys[e.RowIndex].Value.ToString();
        string strSql = " delete from RecommendSales where Rec_No=@Rec_No";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@Rec_No", strKey));
            comm.ExecuteNonQuery();
            comm.Dispose();
        }
        finally
        {
            connect.Close();
        }

        LU1();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        LU1();
    }

    protected void fn_DropDownList_Area()
    {
        string strSql = "";
        strSql += " SELECT [Area_No], [Area_Name] FROM Trip.dbo.[Area] WHERE Array <> 0 ORDER BY [Area_No]";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlDataAdapter da = new SqlDataAdapter(strSql, strConnString);
        DataTable dt = new DataTable();
        da.Fill(dt);
        DropDownList1.DataSource = dt;
        DropDownList1.DataValueField = "Area_No";
        DropDownList1.DataTextField = "Area_Name";
        DropDownList1.DataBind();
        connect.Close();

        DropDownList1.Items.Insert(0, new ListItem("全選", "0"));
        //DropDownList1.Items.Insert(0, new ListItem("新視界", "90000"));
        //DropDownList1.Items.Insert(1, new ListItem("典藏", "90001"));
        //DropDownList1.Items.Insert(0, new ListItem("中國", "90002"));
        DropDownList1.SelectedIndex = 0;
    }
}
