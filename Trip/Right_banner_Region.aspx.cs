﻿using System;
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

public partial class Trip_Right_banner_Region : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsFunction.Check.Check_Login_ID_And_PW();
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["no"]))
            {
                HidNo1.Value = Request["no"].ToString();
                Button2.Visible = false;
                fn_bind();
            }
            else
            {
                Button1.Visible = false;
            }
        }
    }
    protected void fn_bind()
    {
        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(constring);
        connect.Open();
        string strSql = "";
        strSql += " select * from Banner ";
        strSql += " WHERE banner_No = '" + HidNo1.Value + "'";
        SqlCommand cmd = new SqlCommand(strSql, connect);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        DataTable dt = new DataTable();
        da.Fill(dt);
        Image1.ImageUrl = "../ad3/img/" + dt.Rows[0]["banner_Pic"].ToString();
        TextBox1.Text = dt.Rows[0]["banner_Link"].ToString();
        cmd.Dispose();
        connect.Close();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string strSql = "";
        strSql += " update Banner set ";
        if (FileUpload1.FileName.IndexOf(".") > 0)
        {
            string tourtitlepic2 = DateTime.Now.ToString("yyyyMMddHHmmss") + "." + FileUpload1.FileName.Split('.').GetValue(1);
            string uppath = Server.MapPath(@"~\ad3\img\" + tourtitlepic2);
            FileUpload1.SaveAs(uppath);
            strSql += "  banner_Pic ='" + tourtitlepic2 + "',";
        }
        strSql += "  banner_Link ='" + TextBox1.Text + "'";
        strSql += " where banner_No = '" + HidNo1.Value + "'";
        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(constring);
        connect.Open();

        SqlCommand comm = new SqlCommand(strSql, connect);
        comm.ExecuteReader();
        comm.Dispose();
        connect.Close();
        Response.Write("<script language='javascript' type='text/javascript'>alert('修改成功！'); window.location='Right_banner.aspx';</script>");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string strSql = "";
        strSql += " INSERT INTO Banner (banner_Pic,banner_Link,banner_sec) VALUES (";
        if (FileUpload1.FileName.IndexOf(".") > 0)
        {
            string tourtitlepic2 = DateTime.Now.ToString("yyyyMMddHHmmss") + "." + FileUpload1.FileName.Split('.').GetValue(1);
            string uppath = Server.MapPath(@"~\ad3\img\" + tourtitlepic2);
            FileUpload1.SaveAs(uppath);
            strSql += "'" + tourtitlepic2 + "' ,";
        }
        strSql += "'" + TextBox1.Text + "', ";
        strSql += "'2') ";
        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(constring);
        connect.Open();

        SqlCommand comm = new SqlCommand(strSql, connect);
        comm.ExecuteReader();
        comm.Dispose();
        connect.Close();
        Response.Write("<script language='javascript' type='text/javascript'>alert('新增成功！'); window.location='Right_banner.aspx';</script>");
    }
}
