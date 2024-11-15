﻿using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

public partial class DMdownload_Select : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) { return; }

        fn_DropDownList_Area();

        if (Request.Cookies["DMdownload_Select"] != null)
        {
            DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(Request.Cookies["DMdownload_Select"].Values["AreaNo"]));
        }

        getdata();
        //fn_DropDownList_gcc();
        
    }
    protected void fn_DropDownList_Area()
    {
        string strSql = "";
        strSql += " SELECT [Area_No], [Area_Name] FROM [Area] ";
        strSql += " WHERE Array <> 0 and Area_ID <> 'Area999'";
        strSql += " ORDER BY [Area_No]";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
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

        //DropDownList1.Items.Insert(0, new ListItem("全選", "0"));
        DropDownList1.SelectedIndex = 0;
    }

    protected void getdata()
    {
        Response.Cookies["DMdownload_Select"].Expires = System.DateTime.Now.AddDays(1);
        Response.Cookies["DMdownload_Select"]["AreaNo"] = DropDownList1.SelectedValue;

        string strSql = "";
        strSql += "select SN,DM_Title,DM_File,DM_EndDate,Area_Name from DMdownload ";
        strSql += " WHERE Area_No = '" + DropDownList1.SelectedValue + "'";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlDataAdapter da = new SqlDataAdapter(strSql, strConnString);
        DataTable dt = new DataTable();
        da.Fill(dt);
        connect.Close();

        this.GridView1.AllowPaging = true;
        this.GridView1.AllowSorting = true;
        this.GridView1.PageSize = 12;
        this.GridView1.DataSource = dt;
        this.GridView1.DataBind();
        this.GridView1.Width = 600;
        dt.Dispose();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;

        switch (e.CommandName)
        {
            case ("SingleClick"):

                int _rowIndex = int.Parse(e.CommandArgument.ToString());

                //int _columnIndex = int.Parse(Request.Form["__EVENTARGUMENT"]);
                _gridView.SelectedIndex = _rowIndex;

                //Control _editControl = _gridView.SelectedRow.Cells[1].Controls[1];
                //string aa = ((Label)_editControl).Text;

                Control _editControl = _gridView.SelectedRow.FindControl("Label1");//.Cells[1].Controls[1];
                string aa = ((Label)_editControl).Text;

                Response.Redirect("DMdownload_Edit.aspx?Aid=" + aa, false);

                break;
        }
    } 
    protected void Button1_Click1(object sender, EventArgs e)
    {

        Response.Redirect("DMdownload_Add.aspx?area=" + DropDownList1.SelectedValue, false);
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        getdata();
        GridView1.DataBind();
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        getdata();
    }
    protected void fn_DropDownList_gcc()
    {
        string strSql = "";
        strSql += " SELECT [Group_Category_No], [Group_Category_Name] FROM [Group_Category] ";
        strSql += " WHERE [MultiCountry] <> 0";
        strSql += " AND [Glb_id] = '" + DropDownList1.SelectedValue + "'";
        strSql += " ORDER BY [Group_Category_No]";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlDataAdapter da = new SqlDataAdapter(strSql, strConnString);
        DataTable dt = new DataTable();
        da.Fill(dt);
        DropDownList2.DataSource = dt;
        DropDownList2.DataValueField = "Group_Category_No";
        DropDownList2.DataTextField = "Group_Category_Name";
        DropDownList2.DataBind();
        connect.Close();

        //DropDownList2.Items.Insert(0, new ListItem("全選", "0"));
        DropDownList2.SelectedIndex = 0;
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        getdata();
    }
}