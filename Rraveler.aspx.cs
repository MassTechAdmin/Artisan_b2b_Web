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

public partial class Rraveler : System.Web.UI.Page
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
        strSql += " ,Cust_Name as '旅客姓名'";
        strSql += " , Cust_Idno as '身份證'";
        strSql += " , left(trip.dbo.grop.grop_name,15) as '團名'";
        strSql += " , CONVERT(varchar(12) , TR10.EnliI_Date, 111) as '報名日期'";
        strSql += " FROM tr20";
        strSql += " Left join tr10 on tr20.tr10number = tr10.number";
        strSql += " left join trip.dbo.grop on trip.dbo.grop.grop_numb = tr10.tour_numb";
        strSql += " where 1=1";
        switch (this.DropDownList1.SelectedValue)
        {
            //Case "1"
            //    strSql &= " and TR10.CUST_NUMB like ?"
            case "2":
                strSql += " and trip.dbo.grop.grop_name like ?";
                break;
            case "3":
                strSql += " and CONVERT(varchar(100), TR10.EnliI_Date, 111) = ?";
                break;
            case "4":
                strSql += " and Cust_Name like ?";
                break;
            //Case "5"
            //    strSql &= " and TR10.Comp_Code like ?"
            //Case "6"
            //    strSql &= " and TR10.Comp_Conn like ?"
        }
        //strSql &= " and tr10.Comp_Code = '" & Session("compno") & "' "
        strSql += " and tr10.sale_code = '" + Session["PERNO"] + "' ";
        strSql += " order by TR10.cust_numb desc";
        string strConnection = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BOleDBConnectionString"].ConnectionString;
        System.Data.OleDb.OleDbConnection strConn = new System.Data.OleDb.OleDbConnection(strConnection);
        strConn.Open();
        System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(strSql, strConnection);
        switch (this.DropDownList1.SelectedValue)
        {
            //Case "1"
            //    da.SelectCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter("@CUST_NUMB", "%" & TextBox1.Text.Trim() & "%"))

            case "2":
                da.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@grop_name", "%" + TextBox1.Text.Trim() + "%"));
                break;
            case "3":
                string aa = "";
                if (clsFunction.Check.IsDate(TextBox1.Text) == true)
                {
                    aa = TextBox1.Text;
                }
                else
                {
                    aa = "1902/1/1";
                }
                da.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@EnliI_Date", aa));
                break;
            case "4":
                da.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("@Cust_Name", "%" + TextBox1.Text.Trim() + "%"));
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

        try
        {


        }
        catch
        {
            Response.Write("資料庫維護中，請稍後");
        }
    }

    //隱藏自動編號
    //原本e.Row.Cells()設定是3,4  因多加審核更改成4,5,6  1/17 by 阿信
    protected void GridView1_RowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow | e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].Visible = false;
            //e.Row.Cells(4).Visible = False
            //e.Row.Cells(5).Visible = False
            //e.Row.Cells(6).Visible = False
            //e.Row.Cells(7).Visible = False
        }
    }

    //換頁
    protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        LU1();
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
        GridView1.DataBind();
        GridView1.PageIndex = pageIndex;
    }

    private string GetSortDirection()
    {
        switch ((GridViewSortDirection))
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


    private string GridViewSortExpression
    {
        get { return (ViewState["GridViewSortExpression"] as string == null ? string.Empty : ViewState["GridViewSortExpression"] as string); }
        set { ViewState["GridViewSortExpression"] = value; }
    }

    private string GridViewSortDirection
    {
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
