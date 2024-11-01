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

public partial class Member : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsFunction.Check.CheckSession();
        if (!IsPostBack)
        {
            fn_GLB_Sales_News();
            fn_Show_Sales_News();

            fn_Show_Chose();
            Show_City();
            Show_City_Area();
            fn_Show_Data();
        }
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
    #region === update ===
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txbUSR_PASSWD.Text) || !string.IsNullOrEmpty(txbUSR_PASSWD2.Text))
        {
            if (txbUSR_PASSWD.Text.Trim() != txbUSR_PASSWD2.Text.Trim())
            {
                txbUSR_PASSWD.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('請輸入密碼！');", true);
                //this.Response.Write("<script language='javascript' type='text/javascript'>alert('請輸入密碼!'); window.location = 'Member.aspx';</script>");
                return;
            }
        }

        if (string.IsNullOrEmpty(txbCNTA_T3_CCD.Text.Trim()) || string.IsNullOrEmpty(txbCNTA_T3.Text.Trim()))
        {
            txbCNTA_T3_CCD.Focus();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('請輸入聯絡電話！');", true);
            //this.Response.Write("<script language='javascript' type='text/javascript'>alert('請輸入聯絡電話!'); window.location = 'Member.aspx';</script>");
            return;
        }

        if (string.IsNullOrEmpty(txbCNTA_T8.Text.Trim()))
        {
            txbCNTA_T8.Focus();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('請輸入手機號碼！');", true);
            //this.Response.Write("<script language='javascript' type='text/javascript'>alert('請輸入手機號碼!'); window.location = 'Member.aspx';</script>");
            return;
        }

        if (string.IsNullOrEmpty(txbCNTA_E1.Text.Trim()))
        {
            txbCNTA_E1.Focus();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('請輸入E-Mail！');", true);
            //this.Response.Write("<script language='javascript' type='text/javascript'>alert('請輸入E-Mail!'); window.location = 'Member.aspx';</script>");
            return;
        }

        fn_UPDATE_IND_FG();
    }
    protected void fn_UPDATE_IND_FG()
    {
        int intCheck = -1;
        string strSql = "";
        strSql += " UPDATE[IND_FG] SET ";
        strSql += " [IND_FG_TEL]=@IND_FG_TEL";
        strSql += " ,[IND_FG_TEL2]=@IND_FG_TEL2";
        strSql += " ,[IND_FG_TEL3]=@IND_FG_TEL3";
        strSql += " ,[IND_FG_FAX]=@IND_FG_FAX";
        strSql += " ,[IND_FG_FAX2]=@IND_FG_FAX2";
        strSql += " ,[IND_FG_Phone]=@IND_FG_Phone";
        strSql += " ,[IND_FG_EMail]=@IND_FG_EMail";
        strSql += " ,[IND_FG_City]=@IND_FG_City";
        strSql += " ,[IND_FG_Country]=@IND_FG_Country";
        strSql += " ,[IND_FG_AddrZip]=@IND_FG_AddrZip";
        strSql += " ,[IND_FG_Addr]=@IND_FG_Addr";
        if(!string.IsNullOrEmpty(txbUSR_PASSWD.Text) && !string.IsNullOrEmpty(txbUSR_PASSWD2.Text))
        {
            strSql += ",IND_FG_PW=@IND_FG_PW";
        }
        strSql += " WHERE IND_FG_ID=@IND_FG_ID";


        strSql += " UPDATE [AGENT_L] SET";
        strSql += " [CONT_ZONE]=@CONT_ZONE";
        strSql += " ,[CONT_TEL]=@CONT_TEL";
        strSql += " ,[CFAX_ZONE]=@CFAX_ZONE";
        strSql += " ,[CONT_FAX]=@CONT_FAX";
        strSql += " ,[CONT_CELL]=@CONT_CELL";
        strSql += " ,[CONT_MAIL]=@CONT_MAIL";
        strSql += " WHERE AGT_CONT=@AGT_CONT";
        strSql += " AND AGT_IDNo=@AGT_IDNo";


        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            // 新增到 IND_FG
            comm.Parameters.Add(new SqlParameter("@IND_FG_TEL", txbCNTA_T3_CCD.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@IND_FG_TEL2", txbCNTA_T3.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@IND_FG_TEL3", txbCNTA_T3_ZIP.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@IND_FG_FAX", txbCNTA_T2_CCD.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@IND_FG_FAX2", txbCNTA_T2.Text.Trim()));

            comm.Parameters.Add(new SqlParameter("@IND_FG_Phone", txbCNTA_T8.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@IND_FG_EMail", txbCNTA_E1.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@IND_FG_City", ddlAddr_City.SelectedValue));
            comm.Parameters.Add(new SqlParameter("@IND_FG_Country", ddlAddr_Country.SelectedValue));
            comm.Parameters.Add(new SqlParameter("@IND_FG_AddrZip", txbAddr_ccd.Text.Trim()));

            comm.Parameters.Add(new SqlParameter("@IND_FG_Addr", txbAddr.Text.Trim()));
            if (!string.IsNullOrEmpty(txbUSR_PASSWD.Text) && !string.IsNullOrEmpty(txbUSR_PASSWD2.Text))
            {
                comm.Parameters.Add(new SqlParameter("@IND_FG_PW", txbUSR_PASSWD.Text.Trim()));
            }
            comm.Parameters.Add(new SqlParameter("@IND_FG_ID", Session["PERNO"].ToString()));

            // 新增到 AGENT_L
            string CONT_ZONE = (string.IsNullOrEmpty(txbCNTA_T3_ZIP.Text.Trim()) ? "" : "#" + txbCNTA_T3_ZIP.Text.Trim());
            //分機若有資料就加入#

            comm.Parameters.Add(new SqlParameter("@CONT_ZONE", txbCNTA_T3_CCD.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@CONT_TEL", txbCNTA_T3.Text.Trim() + CONT_ZONE));
            comm.Parameters.Add(new SqlParameter("@CFAX_ZONE", txbCNTA_T2_CCD.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@CONT_FAX", txbCNTA_T2.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@CONT_CELL", txbCNTA_T8.Text.Trim()));

            comm.Parameters.Add(new SqlParameter("@CONT_MAIL", txbCNTA_E1.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@AGT_CONT", Session["PerName"].ToString()));
            comm.Parameters.Add(new SqlParameter("@AGT_IDNo", Session["PerIDNo"].ToString()));
            intCheck = comm.ExecuteNonQuery();
            comm.Dispose();
        }
        finally
        {
            connect.Close();
        }

        if (intCheck > -1)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改完成，請由同業專區登入!!'); window.location = 'Default.aspx';", true);
            //this.Response.Write("<script language='javascript' type='text/javascript'>alert('修改完成，請由同業專區登入!'); window.location = 'Member.aspx';</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改失敗，請確認資料，重新輸入！');", true);
            //this.Response.Write("<script language='javascript' type='text/javascript'>alert('修改失敗，請確認資料，重新輸入!'); window.location = 'Member.aspx';</script>");
        }
    }
    protected void fn_Show_Data()
    {
        string strSql = " SELECT * FROM IND_FG WHERE IND_FG_ID=@IND_FG_ID";
        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(constring);
        try
        {
            connect.Open();
            SqlCommand command = new SqlCommand(strSql, connect);
            command.Parameters.Add(new SqlParameter("@IND_FG_ID", Session["PERNO"]));
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                txbCNTA_T3_CCD.Text = reader["IND_FG_TEL"].ToString();
                txbCNTA_T3.Text = reader["IND_FG_TEL2"].ToString();
                txbCNTA_T3_ZIP.Text = reader["IND_FG_TEL3"].ToString();
                txbCNTA_T2_CCD.Text = reader["IND_FG_FAX"].ToString();
                txbCNTA_T2.Text = reader["IND_FG_FAX2"].ToString();
                txbCNTA_T8.Text = reader["IND_FG_Phone"].ToString();
                txbCNTA_E1.Text = reader["IND_FG_EMail"].ToString();
                Show_City();
                ddlAddr_City.SelectedIndex = ddlAddr_City.Items.IndexOf(ddlAddr_City.Items.FindByValue(reader["IND_FG_City"].ToString()));
                Show_City_Area();
                ddlAddr_Country.SelectedIndex = ddlAddr_Country.Items.IndexOf(ddlAddr_Country.Items.FindByValue(reader["IND_FG_Country"].ToString()));
                txbAddr_ccd.Text = reader["IND_FG_AddrZip"].ToString();
                txbAddr.Text = reader["IND_FG_Addr"].ToString();
            }
            command.Dispose();
            reader.Close();
        }
        finally
        {
            connect.Close();
        }
    }
    #endregion
    #region === 地區下拉 ===
    protected void Show_City()
    {
        string strSql = "";
        strSql += " SELECT City_AreaNo,City_Area FROM City WHERE City_IsShow = 'true' ORDER BY City_AreaNo";
        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ConnectionString;
        SqlConnection connect = new SqlConnection(constring);
        connect.Open();
        SqlDataAdapter da = new SqlDataAdapter(strSql, constring);
        System.Data.DataTable dt = new System.Data.DataTable();
        da.Fill(dt);
        ddlAddr_City.DataSource = dt;
        ddlAddr_City.DataValueField = "City_AreaNo";
        ddlAddr_City.DataTextField = "City_Area";
        ddlAddr_City.DataBind();

        ddlAddr_City.Items.Insert(0, new ListItem("請選擇縣市", ""));
        connect.Close();
    }
    protected void Show_City_Area()
    {
        string strSql = "";
        strSql += " SELECT [CA_AreaNo],[CA_City_AreaNo],[CA_Desc] FROM [City_Area]";
        strSql += " WHERE CA_City_AreaNo = @CA_City_AreaNo";
        strSql += " ORDER BY CA_AreaNo";
        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ConnectionString;
        SqlConnection connect = new SqlConnection(constring);
        connect.Open();
        SqlDataAdapter da = new SqlDataAdapter(strSql, constring);
        da.SelectCommand.Parameters.Add(new SqlParameter("@CA_City_AreaNo", ddlAddr_City.SelectedValue));
        System.Data.DataTable dt = new System.Data.DataTable();
        da.Fill(dt);
        ddlAddr_Country.DataSource = dt;
        ddlAddr_Country.DataValueField = "CA_AreaNo";
        ddlAddr_Country.DataTextField = "CA_Desc";
        ddlAddr_Country.DataBind();

        if (string.IsNullOrEmpty(ddlAddr_City.SelectedValue))
        {
            ddlAddr_Country.Items.Insert(0, new ListItem("請選擇鎮市區", ""));
        }
        connect.Close();
    }
    protected void ddlAddr_City_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Show_City_Area();
        txbAddr_ccd.Text = ddlAddr_Country.SelectedValue;
        this.txbUSR_PASSWD.Attributes["value"] = this.txbUSR_PASSWD.Text;
        this.txbUSR_PASSWD2.Attributes["value"] = this.txbUSR_PASSWD2.Text;
    }
    protected void ddlAddr_Country_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        txbAddr_ccd.Text = ddlAddr_Country.SelectedValue;
        this.txbUSR_PASSWD.Attributes["value"] = this.txbUSR_PASSWD.Text;
        this.txbUSR_PASSWD2.Attributes["value"] = this.txbUSR_PASSWD2.Text;
    }
    #endregion
}
