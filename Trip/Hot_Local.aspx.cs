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

public partial class Trip_Hot_Local : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsFunction.Check.Check_Login_ID_And_PW();
        if (this.IsPostBack == false)
        {
            LU1();
        }
    }
    private const int _firstEditCellIndex = 2;
    private void LU1()
    {
        string strSql = "";
        strSql += " select Glb_Id ,Descrip , glb_Order from GLB_CODE";
        strSql += " where Glb_Code = 'Hot_Local'";
        strSql += " order by glb_Order";
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
            GridView1.DataKeyNames = new string[] { "Glb_Id" };
            GridView1.DataBind();

        }
        catch
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



                Response.Redirect("SecClass_Edit.aspx?no=" + aa);


                break;
        }
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


    protected void Button1_Click(object sender, EventArgs e)
    {
        for (int ii = 0; ii < GridView1.Rows.Count; ii++)
        {
            HiddenField hidNo = (HiddenField)GridView1.Rows[ii].FindControl("hidNo");
            TextBox txbOrder = (TextBox)GridView1.Rows[ii].FindControl("txbOrder");
            fn_Update_Candidates(hidNo.Value, txbOrder.Text.Trim());
        }
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
        int Items = fn_GLB_Sales_News();
        strSql += " insert into [GLB_CODE] (Glb_Id, Descrip, Glb_Code";
        strSql += ") VALUES (";
        strSql += " @Glb_Id ,@Descrip ,'Hot_Local')";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand comm = new SqlCommand(strSql, connect);

        comm.Parameters.Add(new SqlParameter("@Glb_Id", "Tab" + Items));
        comm.Parameters.Add(new SqlParameter("@Descrip", TextBox2.Text.Trim()));
        comm.ExecuteNonQuery();
        connect.Close();

        strSql = " insert into [Hot_Local] ([HL_Type]";
        strSql += ") VALUES (";
        strSql += " @HL_Type)";
        connect = new SqlConnection(strConnString);
        connect.Open();
        comm = new SqlCommand(strSql, connect);
        comm.Parameters.Add(new SqlParameter("@HL_Type", "Tab" + Items));
        comm.ExecuteNonQuery();

        comm.Clone();
        connect.Close();

        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('新增成功！'); window.location = 'Hot_Local.aspx';", true);
    }


    private string getpath()
    {
        string strpath = Server.MapPath(@"~\App_Data\ForStr.txt");

        return strpath;
    }


    protected int fn_GLB_Sales_News()
    {
        string strSql = "";
        strSql += " select Glb_Id,Descrip from GLB_CODE";
        strSql += " where Glb_Code = 'Hot_Local'";
        strSql += " order by glb_Order";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            int intItem = 0;
            int Item = 0;
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    intItem++;
                    if (fn_GLB_Sales_Check(intItem))
                    {
                        Item = intItem;
                    }
                }
            }
            reader.Close();
            comm.Dispose();

            if (Item == 0)
            {
                return intItem + 1;
            }
            else
            {
                return Item;
            }
        }
        finally
        {
            connect.Close();
        }
    }
    protected bool fn_GLB_Sales_Check(int tab)
    {
        bool bn = true;
        string strSql = "";
        strSql += " select Glb_Id,Descrip from GLB_CODE";
        strSql += " where Glb_Code = 'Hot_Local'";
        strSql += "and Glb_Id=@Glb_Id";
        strSql += " order by glb_Order";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@Glb_Id", "Tab" + tab));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    bn = false;
                }
            }
            else
            reader.Close();
            comm.Dispose();

            return bn;
        }
        finally
        {
            connect.Close();
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strKey = GridView1.DataKeys[e.RowIndex].Value.ToString();
        string strSql = " delete from GLB_CODE where Glb_Code='Hot_Local' and Glb_Id=@Glb_Id";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@Glb_Id", strKey));
            comm.ExecuteNonQuery();
            comm.Dispose();
        }
        finally
        {
            connect.Close();
        }

        strSql = " delete from [Hot_Local] where HL_Type=@HL_Type";
        connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@HL_Type", strKey));
            comm.ExecuteNonQuery();
            comm.Dispose();
        }
        finally
        {
            connect.Close();
        }

        LU1();
    }
    protected void fn_Update_Candidates(string strCand_No, string strCand_Order)
    {
        string strSql = "";
        strSql += " UPDATE [GLB_CODE] SET";
        strSql += " glb_Order = @glb_Order";
        strSql += " WHERE 1=1 ";
        strSql += " AND Glb_Id = @Glb_Id";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand comm = new SqlCommand(strSql, connect);
        comm.Parameters.Add(new SqlParameter("@glb_Order", strCand_Order));
        comm.Parameters.Add(new SqlParameter("@Glb_Id", strCand_No));
        comm.ExecuteNonQuery();
        comm.Dispose();
        connect.Close();

        strSql = " UPDATE [Hot_Local] SET";
        strSql += " [Hl_Order]=@Hl_Order";
        strSql += " WHERE HL_Type=@HL_Type";
        connect = new SqlConnection(strConnString);
        connect.Open();
        comm = new SqlCommand(strSql, connect);
        comm.Parameters.Add(new SqlParameter("@Hl_Order", strCand_Order));
        comm.Parameters.Add(new SqlParameter("@HL_Type", strCand_No));
        comm.ExecuteNonQuery();

        comm.Clone();
        connect.Close();

        Response.Write("<script language='javascript' type='text/javascript'>alert('修改成功！'); window.location='hot_local.aspx';</script>");

    }
}
