﻿using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.IO;
using System.Data;
using System.Data.SqlClient;

public partial class DMdownload_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getarea();

            if (Request.Cookies["DMdownload_Select"] != null)
            {
                DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(Request.Cookies["DMdownload_Select"].Values["AreaNo"]));
            }
        }
    }

    protected void getarea()
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
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        add_data();
    }

    protected void add_data()
    {
        Response.Cookies["DMdownload_Select"].Expires = System.DateTime.Now.AddDays(1);
        Response.Cookies["DMdownload_Select"]["AreaNo"] = DropDownList1.SelectedValue;

        string exhpic = "";
        if (this.FileUpload1.HasFile)
        {
            exhpic = Guid.NewGuid().ToString("N") + Path.GetExtension(this.FileUpload1.FileName);
            string uppath = Server.MapPath(@"~\Zupload\DMdownload\" + exhpic);

            ///-----------------------上傳新的檔案
            if (File.Exists(uppath) == false)
            {
                this.FileUpload1.SaveAs(uppath);
            }
        }


        string strsql = "";
        strsql += " insert into DMdownload (";
        strsql += " Area_No,Area_Name,DM_Title,DM_EndDate";
        if (FileUpload1.HasFile == true)
        {
            strsql += " ,DM_File";
        }
        strsql += " ) values (@Area_No,@Area_Name,@DM_Title,@DM_EndDate";
        if (FileUpload1.HasFile == true)
        {
            strsql += " ,@DM_File";
        }
        strsql += ")";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        connect.Open();

        SqlCommand comm = new SqlCommand(strsql, connect);
        comm.Parameters.Add(new SqlParameter("@Area_NO", DropDownList1.SelectedValue));
        comm.Parameters.Add(new SqlParameter("@Area_Name", DropDownList1.SelectedItem.Text));
        comm.Parameters.Add(new SqlParameter("@DM_Title", TextBox1.Text));
        comm.Parameters.Add(new SqlParameter("@DM_EndDate", RadDatePicker1.DbSelectedDate));

        if (FileUpload1.HasFile == true)
        {
            comm.Parameters.Add(new SqlParameter("@DM_File", exhpic));
        }

        comm.ExecuteNonQuery();
        comm.Dispose();
        connect.Close();
        this.Response.Write("<script language='javascript' type='text/javascript'>alert('新增成功！');  window.location='DMdownload_Select.aspx';</script>");
    }
}