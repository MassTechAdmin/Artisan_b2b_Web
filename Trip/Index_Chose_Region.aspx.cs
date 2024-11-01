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
using System.Data.OleDb;

public partial class Trip_Index_Chose_Region : System.Web.UI.Page
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
        OleDbConnection conn = TripDB.CreateConnection1();
        conn.Open();
        string strsql = "";
        strsql += " select * from chose ";
        strsql += " WHERE Chose_No = '" + HidNo1.Value + "'";
        OleDbDataAdapter da = new OleDbDataAdapter(strsql, conn);
        DataTable dt = new DataTable();
        da.Fill(dt);
        Image1.ImageUrl = "../" + dt.Rows[0]["Chose_Pic"].ToString();
        TextBox1.Text = dt.Rows[0]["Chose_Link"].ToString();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        OleDbConnection conn = TripDB.CreateConnection1();
        conn.Open();
        string strsql = "";
        strsql += " update chose set ";
        if (FileUpload1.FileName.IndexOf(".") > 0)
        {
            string tourtitlepic2 = DateTime.Now.ToString("yyyyMMddHHmmss") + "." + FileUpload1.FileName.Split('.').GetValue(1);
            string uppath = Server.MapPath(@"~\NewPageFile\" + tourtitlepic2);
            FileUpload1.SaveAs(uppath);
            strsql += "  Chose_Pic ='NewPageFile\\" + tourtitlepic2 + "',";
        }
        strsql += " Chose_Link ='" + TextBox1.Text + "'";
        strsql += " where Chose_No = '"+ HidNo1.Value +"'";
        OleDbCommand comm = new OleDbCommand(strsql, conn);
        comm.ExecuteReader();
        comm.Dispose();
        conn.Close();
        Response.Write("<script language='javascript' type='text/javascript'>alert('修改成功！'); window.location='Index_Chose.aspx';</script>");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        OleDbConnection conn = TripDB.CreateConnection1();
        conn.Open();
        string strsql = "";
        strsql += " INSERT INTO chose (Chose_Pic,Chose_Link) VALUES (";
        if (FileUpload1.FileName.IndexOf(".") > 0)
        {
            string tourtitlepic2 = DateTime.Now.ToString("yyyyMMddHHmmss") + "." + FileUpload1.FileName.Split('.').GetValue(1);
            string uppath = Server.MapPath(@"~\NewPageFile\" + tourtitlepic2);
            FileUpload1.SaveAs(uppath);
            strsql += "'NewPageFile\\" + tourtitlepic2 + "' ,";
        }
        strsql += "'" + TextBox1.Text + "') ";
        OleDbCommand comm = new OleDbCommand(strsql, conn);
        comm.ExecuteReader();
        comm.Dispose();
        conn.Close();
        Response.Write("<script language='javascript' type='text/javascript'>alert('修改成功！'); window.location='Index_Chose.aspx';</script>");
    }
}
