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
using System.Data.OleDb;
using System.Security.Cryptography;
using System.Text;

public partial class Trip_login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ClassFunction clsFun = new ClassFunction();
        clsFun.CheckWebAddress();


        if (this.IsPostBack == false)
        {
            TextBox1.Focus();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        OleDbConnection conn = TripDB.CreateConnection();
        conn.Open();

        string strSql = "";



        //strsql = "select Login_Account from LoginTrip";
        //strsql += " where Login_Account='" + Format.Replace(TextBox1.Text, getpath()) + "'";
        //strsql += " and Login_Password='" + GetMd5Str(Format.Replace(TextBox2.Text, getpath())) + "'";
        strSql = "select Login_Account from LoginTrip";
        strSql += " where Login_Account = ?";
        strSql += " and Login_Password = ?";

        OleDbCommand comm = new OleDbCommand(strSql, conn);
        comm.Parameters.Add(new OleDbParameter("@Login_Account", TextBox1.Text.Trim()));
        comm.Parameters.Add(new OleDbParameter("@Login_Password", TextBox2.Text.Trim()));
        OleDbDataReader reader = comm.ExecuteReader();

        if (reader.Read() == true && Convert.ToString(Session["v$code"]) == TextBox3.Text.ToUpper())
        {
            Session["TripName"] = reader.GetValue(0).ToString();
            //Session["TripName"] = "admin";
            //Response.Write("<script language='javascript' type='text/javascript'>alert('登入成功！'); window.location='index_chose.aspx';</script>");
            Response.Write("<script language='javascript' type='text/javascript'>alert('登入成功！'); window.location='n_keyword.aspx';</script>");
        }
        else
        {
            Response.Write("<script language='javascript' type='text/javascript'>alert('帳號密碼錯誤，或驗證碼輸入不正確！'); window.location='Login.aspx';</script>");
        }

        comm.Dispose();
        reader.Close();

        conn.Close();
        conn = null;
    }

    public static string GetMd5Str(string ConvertString)//16位元md5
    {
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
        t2 = t2.Replace("-", "");
        return t2;
    }

    private string getpath()
    {
        string strpath = Server.MapPath(@"~\App_Data\ForStr.txt");

        return strpath;
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        base.Dispose();
    }
}
