﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

public partial class onsalep : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getdata();
        }
    }

    protected void getdata() 
    {
        string strSql = "";
        strSql += " select Number ,Coupon_No ,Price ,Deadline ,ConnID ,isUse ,Trip ,Crea_Date";
        strSql += " From Coupon";
        strSql += " where ConnID = @ConnID";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlDataAdapter da = new SqlDataAdapter(strSql, strConnString);
        da.SelectCommand.Parameters.Add(new SqlParameter("@ConnID", Session["PERNO"]));
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.DataSource = dt;
        //GridView1.DataKeyNames = "Van_Number";
        GridView1.DataBind();
        connect.Close();
    
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("order_list.aspx");
    }
}