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

public partial class ForgetPW : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

        fn_GLB_Sales_News();
        fn_Show_Sales_News();

        fn_Show_Chose();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
	if (string.IsNullOrEmpty(TextBox1.Text.Trim()))
        {
            TextBox1.Focus();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('請輸入身分證字號！');", true);
            //this.Response.Write("<script language='javascript' type='text/javascript'>alert('請輸入密碼!'); window.location = 'Member.aspx';</script>");
            return;
        }

        if (string.IsNullOrEmpty(TextBox2.Text.Trim()))
        {
            TextBox2.Focus();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('請輸入電子郵件！');", true);
            //this.Response.Write("<script language='javascript' type='text/javascript'>alert('請輸入聯絡電話!'); window.location = 'Member.aspx';</script>");
            return;
        }

        check_data();
    }
    protected void check_data()
    {
        Boolean blnlock = false;
        string strSendMailData = "";
        string strEMail = "";
        string strID = "";
        string strPW = "";
        string strSql = "";
        strSql += " SELECT IND_FG_IDNo,IND_FG_EMail,IND_FG_ID,IND_FG_PW";
        strSql += " FROM [IND_FG]";
        strSql += " WHERE IND_FG_IDNo = @IND_FG_IDNo";
        strSql += " AND IND_FG_EMail = @IND_FG_EMail";
        strSql += " AND IND_FG_Verify = @IND_FG_Verify";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection con = new SqlConnection(strConnString);
        try
        {
            con.Open();
            SqlCommand command = new SqlCommand(strSql, con);
            command.Parameters.Add(new SqlParameter("@IND_FG_IDNo", TextBox1.Text));
            command.Parameters.Add(new SqlParameter("@IND_FG_EMail", TextBox2.Text));
            command.Parameters.Add(new SqlParameter("@IND_FG_Verify", "Y"));
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                strEMail = reader["IND_FG_EMail"].ToString();
                strID = reader["IND_FG_ID"].ToString();
                strPW = reader["IND_FG_PW"].ToString();
                blnlock = true;
            }
            reader.Close();
            command.Dispose();
        }
        finally
        {
            con.Close();
        }

        if (blnlock)
        {
            strSendMailData += "您好：<br />";
            strSendMailData += "非常感謝您註冊使用凱旋旅行社B2B旅遊同業網，<br />";
            strSendMailData += "您的帳號與密碼如下<br />";
            strSendMailData += "帳號：'" + strID + "'<br />";
            strSendMailData += "密碼：'" + strPW + "'";
            strSendMailData += "如使用上有任何的問題，請來電洽詢當區業務員。<br />";
            strSendMailData += " 祝<br/>";
            strSendMailData += "                     事事順利<br/>";
            strSendMailData += "                                        凱旋旅行社敬上";

            if (clsFunction.EMail.Send_Mail("巨匠旅遊同業服務網-密碼帳號寄送", strEMail, strSendMailData))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('密碼已寄出!')", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('身分證或E_Mail錯誤!')", true);
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('身分證或E_Mail錯誤!')", true);
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    #region === 特惠促銷 ===
    protected void fn_GLB_Sales_News()
    {
        string strSql = "";
        strSql += " select Glb_Id,Descrip from GLB_CODE";
        strSql += " where Glb_Code = 'Sales_News'";
        strSql += " order by Glb_Id";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            string strData = "";
            int intItem = 0;
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                strData += "<ul class=\"tabs\">";
                while (reader.Read())
                {
                    intItem++;
                    strData += "<li class=\"title_blue\"><a href=\"#tab" + intItem.ToString() + "\"><strong>" + reader["Descrip"].ToString() + "</strong></a></li>";
                }
                strData += "</ul>";
            }
            reader.Close();
            comm.Dispose();

            litSale_News_Items.Text = strData;
        }
        finally
        {
            connect.Close();
        }
    }

    protected void fn_Show_Sales_News()
    {
        string strSql = "";
        strSql += " SELECT [SN_No],[SN_Type],[SN_Left_Content],[SN_Right_Content] FROM [Sales_News]";
        strSql += " ORDER BY [SN_Type]";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            string strData = "";
            int intItem = 0;
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                intItem++;

                strData += "<div id=\"tab" + intItem.ToString() + "\" class=\"tab_content\">";

                strData += "<div class=\"sale_left\">";
                strData += reader["SN_Left_Content"].ToString().Replace("Images/Zupload/", "http://www.artisan.com.tw/Images/Zupload/")
                .Replace("/fckEditor/editor/", "http://www.artisan.com.tw/fckEditor/editor/").Replace("images/", "http://www.artisan.com.tw/images/");
                strData += "</div>";

                strData += "<div class=\"sale_right\">";
                strData += reader["SN_Right_Content"].ToString();
                strData += "</div>";

                strData += "</div>";
            }
            reader.Close();
            comm.Dispose();

            litSale_News.Text = strData;
        }
        finally
        {
            connect.Close();
        }
    }
    #endregion

    #region === 橫幅廣告輪播 ===
    protected void fn_Show_Chose()
    {
        string strSql = "";
        strSql += " SELECT [chose_Pic],[chose_Link] FROM [chose]";
        strSql += " ORDER BY [chose_order]";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            string strData = "";
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                strData += "<ul>";
                while (reader.Read())
                {
                    strData += "<li><a href=\"" + reader["chose_Link"].ToString() + "\" target='_blank'><img src=\"http://www.artisan.com.tw/" + reader["chose_Pic"].ToString() + "\" alt=\"\" width=\"224\" height=\"74\" /></a></li>";
                }
                strData += "</ul>";
            }
            reader.Close();
            comm.Dispose();

            litAds.Text = strData;
        }
        finally
        {
            connect.Close();
        }
    }
    #endregion
}
