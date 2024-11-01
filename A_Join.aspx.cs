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
using Microsoft.VisualBasic;
using System.Xml;
using System.Net;
using System.Text;
using System.IO;


public partial class A_Join : System.Web.UI.Page
{
    string ResponseXML = ""; //XML回傳訊息
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
	    Counter.Counter.fn_Account();
            Show_City();
            Show_City_Area();
        }
    }
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

    protected void ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (!CheckBox1.Checked == true)
        { 
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('請閱讀審核資料相關事項，並打勾同意其內容。');", true); 
            return; 
        }
        
        lblMessage.Text = "";

        string strSN = txbCOMP_NO.Text.Trim();
        if (!checkCompanyNo(strSN))
        {
            lblMessage.Text = "統一編號輸入錯誤，請重新輸入。";
            return;
        }


        string strCOMP_NO = "";
        string strSql = " SELECT COMP_NO,AGT_NAME1,AGT_NAME2,AGT_RESP,CONN_ZIP,ADDRESS,TEL1_ZONE,TEL1 FROM AGENT_M WHERE COMP_NO=@COMP_NO";
        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(constring);
        try
        {
            connect.Open();
            SqlCommand command = new SqlCommand(strSql, connect);
            command.Parameters.Add(new SqlParameter("@COMP_NO", txbCOMP_NO.Text.Trim()));
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Panel1.Visible = false;
                Panel2.Visible = true;
                Panel3.Visible = true;
                strCOMP_NO = reader["COMP_NO"].ToString();
                lblSN.Text = reader["COMP_NO"].ToString();
                lblCompName.Text = reader["AGT_NAME2"].ToString();
                lblResp.Text = reader["AGT_RESP"].ToString();
                lblZip.Text = reader["CONN_ZIP"].ToString();
                lblAddr.Text = reader["ADDRESS"].ToString();
                lblCompTel.Text = "";
                if (!string.IsNullOrEmpty(reader["TEL1_ZONE"].ToString()))
                {
                    lblCompTel.Text += "(" + reader["TEL1_ZONE"].ToString() + ")";
                }
                lblCompTel.Text += reader["TEL1"].ToString();

                hidCompName1.Value = reader["AGT_NAME1"].ToString();
            }
            command.Dispose();
            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            connect.Close();
        }

        if (string.IsNullOrEmpty(strCOMP_NO))
        {
            lblMessage.Text = "沒有此公司資料，請確認資料是否正確。！";
            return;
        }
    }
    /// <summary>
    /// Check SN
    /// </summary>
    /// <param name="CompanyId "></param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static bool checkCompanyNo(string CompanyId)
    {
        int CompanyNo = 0;
        if (CompanyId == null || CompanyId.Trim().Length != 8)
        {
            return false;
        }
        if (!int.TryParse(CompanyId, out CompanyNo))
        {
            return false;
        }
        int[] Logic = new int[] {1,2,1,2,1,2,4,1};
        int addition = 0;
        int sum = 0;
        int j = 0;
        int x = 0;
        for (x = 0; x <= Logic.Length - 1; x++)
        {
            int no = Convert.ToInt32(CompanyId.Substring(x, 1));
            j = no * Logic[x];
            addition = ((j / 10) + (j % 10));
            sum += addition;
        }
        if (sum % 10 == 0)
        {
            return true;
        }
        if (CompanyId.Substring(6, 1) == "7")
        {
            if (sum % 10 == 9)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 聯絡人資料
    /// </summary>
    protected void ImageButton2_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (!CheckBox1.Checked == true)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('請閱讀審核資料相關事項，並打勾同意其內容。');", true);
            return;
        }

        if (string.IsNullOrEmpty(txbID.Text.Trim()))
        {
            //Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "alert('請輸入身份證字號！');", True)
            this.Response.Write("<script language='javascript' type='text/javascript'>alert('請輸入身份證字號');</script>");
            return;
        }
        else if (txbID.Text.Trim().Length != 10)
        {
            this.Response.Write("<script language='javascript' type='text/javascript'>alert('身份證格式不正確');</script>");
            return;
        }

        string strMsg = CheckIdcode(txbID.Text.Trim());
        if (!string.IsNullOrEmpty(strMsg))
        {
            //Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "alert('" & strMsg & "');", True)
            this.Response.Write("<script language='javascript' type='text/javascript'>alert('" + strMsg + "');</script>");
            return;
        }

        // 身份證字號重覆
        string strIND_FG_IDNo = "";
        string strSql = " SELECT IND_FG_IDNo FROM IND_FG WHERE IND_FG_IDNo=@IND_FG_IDNo";
        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(constring);
        try
        {
            connect.Open();
            SqlCommand command = new SqlCommand(strSql, connect);
            command.Parameters.Add(new SqlParameter("@IND_FG_IDNo", txbID.Text.Trim()));
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                strIND_FG_IDNo = reader["IND_FG_IDNo"].ToString();
            }
            command.Dispose();
            reader.Close();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            connect.Close();
        }

        if (!string.IsNullOrEmpty(strIND_FG_IDNo))
        {
            //Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "alert('若您忘記密碼，請告知公司服務人員。');", True)
            this.Response.Write("<script language='javascript' type='text/javascript'>alert('若您忘記密碼，請告知公司服務人員。');</script>");
            return;
        }

        lblIDNO.Text = txbID.Text.Trim().ToUpper();
        txbUSR_ID.Text = txbID.Text.Trim().ToUpper();

        Panel3.Visible = false;
        Panel4.Visible = true;
    }
    /// <summary>
    /// 申請人資料
    /// </summary>
    protected void ImageButton3_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (!CheckBox1.Checked == true)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('請閱讀審核資料相關事項，並打勾同意其內容。');", true);
            return;
        }

        if (string.IsNullOrEmpty(txbAGTC_NM.Text.Trim()))
        {
            txbAGTC_NM.Focus();
            //Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "alert('請輸入中文姓名！');", True)
            this.Response.Write("<script language='javascript' type='text/javascript'>alert('請輸入中文姓名!');</script>");
            return;
        }

        if (string.IsNullOrEmpty(txbUSR_ID.Text.Trim()) || txbUSR_ID.Text.Trim().Length < 4)
        {
            txbUSR_ID.Focus();
            //Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "alert('請輸入帳號！');", True)
            this.Response.Write("<script language='javascript' type='text/javascript'>alert('請輸入帳號(請至少輸入4 ~ 10碼)!');</script>");
            return;
        }

        if (string.IsNullOrEmpty(txbUSR_PASSWD.Text.Trim()) || txbUSR_PASSWD.Text.Trim().Length < 4)
        {
            txbUSR_PASSWD.Focus();
            //Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "alert('請輸入帳號！');", True)
            this.Response.Write("<script language='javascript' type='text/javascript'>alert('請輸入密碼(請至少輸入4 ~ 10碼)!');</script>");
            return;
        }

        if (string.IsNullOrEmpty(txbUSR_PASSWD2.Text.Trim()))
        {
            txbUSR_PASSWD2.Focus();
            //Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "alert('請輸入帳號！');", True)
            this.Response.Write("<script language='javascript' type='text/javascript'>alert('請輸入密碼!');</script>");
            return;
        }

        if (txbUSR_PASSWD.Text.Trim() != txbUSR_PASSWD2.Text.Trim())
        {
            txbUSR_PASSWD.Focus();
            //Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "alert('請輸入帳號！');", True)
            this.Response.Write("<script language='javascript' type='text/javascript'>alert('請輸入密碼!');</script>");
            return;
        }

        if (string.IsNullOrEmpty(txbCNTA_T3_CCD.Text.Trim()) || string.IsNullOrEmpty(txbCNTA_T3.Text.Trim()))
        {
            txbCNTA_T3_CCD.Focus();
            //Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "alert('請輸入帳號！');", True)
            this.Response.Write("<script language='javascript' type='text/javascript'>alert('請輸入聯絡電話!');</script>");
            return;
        }

        if (string.IsNullOrEmpty(txbCNTA_T8.Text.Trim()))
        {
            txbCNTA_T8.Focus();
            //Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "alert('請輸入帳號！');", True)
            this.Response.Write("<script language='javascript' type='text/javascript'>alert('請輸入手機號碼!');</script>");
            return;
        }

        if (string.IsNullOrEmpty(txbCNTA_E1.Text.Trim()))
        {
            txbCNTA_E1.Focus();
            //Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "alert('請輸入E-Mail！');", True)
            this.Response.Write("<script language='javascript' type='text/javascript'>alert('請輸入E-Mail!');</script>");
            return;
        }


        // 檢查
        string strIND_FG_ID = "";
        string strSql = " SELECT AGT_IDNo FROM AGENT_L WHERE AGT_IDNo=@AGT_IDNo";
        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(constring);
        try
        {
            connect.Open();
            SqlCommand command = new SqlCommand(strSql, connect);
            command.Parameters.Add(new SqlParameter("@AGT_IDNo", txbUSR_ID.Text.Trim()));
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                strIND_FG_ID = reader["AGT_IDNo"].ToString();
            }
            command.Dispose();
            reader.Close();
        }
        finally
        {
            connect.Close();
        }

        if (!string.IsNullOrEmpty(strIND_FG_ID))
        {
            txbUSR_ID.Focus();
            //Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "alert('帳號重覆，請重新輸入。');", True)
            this.Response.Write("<script language='javascript' type='text/javascript'>alert('帳號重覆，請重新輸入!');</script>");
            return;
        }


        fn_Insert_IND_FG();
    }
    /// <summary>
    /// 新增資料
    /// </summary>
    /// <remarks></remarks>
    protected void fn_Insert_IND_FG()
    {
        int intCheck = -1;
        string strSql = "";
        strSql += " INSERT INTO [IND_FG](";
        strSql += " [IND_FG_IDNo],[IND_FG_Name],[IND_FG_PW],[IND_FG_TEL],[IND_FG_TEL2]";
        strSql += " ,[IND_FG_TEL3],[IND_FG_FAX],[IND_FG_FAX2],[IND_FG_IS_FN],[IND_FG_Phone]";
        strSql += " ,[IND_FG_EMail],[IND_FG_City],[IND_FG_Country],[IND_FG_AddrZip],[IND_FG_Addr]";
        strSql += " ,[IND_FG_Verify],[IND_FG_CompNO],[Crea_Date],[Crea_User],[IND_FG_ID]";
        strSql += " ) VALUES (";
        strSql += " @IND_FG_IDNo,@IND_FG_Name,@IND_FG_PW,@IND_FG_TEL,@IND_FG_TEL2";
        strSql += " ,@IND_FG_TEL3,@IND_FG_FAX,@IND_FG_FAX2,@IND_FG_IS_FN,@IND_FG_Phone";
        strSql += " ,@IND_FG_EMail,@IND_FG_City,@IND_FG_Country,@IND_FG_AddrZip,@IND_FG_Addr";
        strSql += " ,@IND_FG_Verify,@IND_FG_CompNO,@Crea_Date,@Crea_User,@IND_FG_ID";
        strSql += " )";


        strSql += " INSERT INTO [AGENT_L] (";
        strSql += " [AGT_NAME1],[AGT_CONT],[CONT_ZONE],[CONT_TEL],[CFAX_ZONE]";
        strSql += " ,[CONT_FAX],[CONT_CELL],[CONT_BBC],[CONT_MAIL],[SALE_CODE]";
        strSql += " ,[crea_date],[crea_user],[upd_date],[upd_user],[loginname]";
        strSql += " ,[AGT_IDNo],AGT_PW,AGT_CompNO,AGT_ID,CONT_TEL_Ext";
        strSql += " ,AGT_City,AGT_Country,AGT_AddrZip,AGT_Addr,AGT_Is_FN";
        strSql += " ,AGT_Sex";
        strSql += " ) VALUES (";
        strSql += " @AGT_NAME1,@AGT_CONT,@CONT_ZONE,@CONT_TEL,@CFAX_ZONE";
        strSql += " ,@CONT_FAX,@CONT_CELL,@CONT_BBC,@CONT_MAIL,@SALE_CODE";
        strSql += " ,@crea_date2,@crea_user2,@upd_date2,@upd_user2,@loginname";
        strSql += " ,@AGT_IDNo,@AGT_PW,@AGT_CompNO,@AGT_ID,@CONT_TEL_Ext";
        strSql += " ,@AGT_City,@AGT_Country,@AGT_AddrZip,@AGT_Addr,@AGT_Is_FN";
        strSql += " ,@AGT_Sex";
        strSql += " )";


        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            // 性別
            string strAGT_Sex = lblIDNO.Text.Trim();
            // 檢查身份證，若身份證是有問題的，性別的欄位就會是空白
            if (CheckIdcode(strAGT_Sex) != "") 
            { strAGT_Sex = ""; }
            // 判斷身份證字號第2碼，若是1=男，2=女
            if (strAGT_Sex.Length > 2)
            {
                if (strAGT_Sex.Substring(1, 1) == "1")
                { strAGT_Sex = "M"; }
                else
                { strAGT_Sex = "F"; }
            }

            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            // 新增到 IND_FG
            comm.Parameters.Add(new SqlParameter("@IND_FG_IDNo", lblIDNO.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@IND_FG_Name", txbAGTC_NM.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@IND_FG_PW", txbUSR_PASSWD.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@IND_FG_TEL", txbCNTA_T3_CCD.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@IND_FG_TEL2", txbCNTA_T3.Text.Trim()));

            comm.Parameters.Add(new SqlParameter("@IND_FG_TEL3", txbCNTA_T3_ZIP.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@IND_FG_FAX", txbCNTA_T2_CCD.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@IND_FG_FAX2", txbCNTA_T2.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@IND_FG_IS_FN", (rbIND_FG.SelectedValue == "1" ? "Y" : "N")));
            comm.Parameters.Add(new SqlParameter("@IND_FG_Phone", txbCNTA_T8.Text.Trim()));

            comm.Parameters.Add(new SqlParameter("@IND_FG_EMail", txbCNTA_E1.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@IND_FG_City", ddlAddr_City.SelectedValue));
            comm.Parameters.Add(new SqlParameter("@IND_FG_Country", ddlAddr_Country.SelectedValue));
            comm.Parameters.Add(new SqlParameter("@IND_FG_AddrZip", txbAddr_ccd.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@IND_FG_Addr", txbAddr.Text.Trim()));

            comm.Parameters.Add(new SqlParameter("@IND_FG_Verify", "N"));
            comm.Parameters.Add(new SqlParameter("@IND_FG_CompNO", lblSN.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@Crea_Date", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")));
            comm.Parameters.Add(new SqlParameter("@Crea_User", ""));
            comm.Parameters.Add(new SqlParameter("@IND_FG_ID", txbUSR_ID.Text.Trim()));

            // 新增到 AGENT_L
            comm.Parameters.Add(new SqlParameter("@AGT_NAME1", hidCompName1.Value));
            comm.Parameters.Add(new SqlParameter("@AGT_CONT", txbAGTC_NM.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@CONT_ZONE", txbCNTA_T3_CCD.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@CONT_TEL", txbCNTA_T3.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@CFAX_ZONE", txbCNTA_T2_CCD.Text.Trim()));

            comm.Parameters.Add(new SqlParameter("@CONT_FAX", txbCNTA_T2.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@CONT_CELL", txbCNTA_T8.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@CONT_BBC", ""));
            comm.Parameters.Add(new SqlParameter("@CONT_MAIL", txbCNTA_E1.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@SALE_CODE", lblIDNO.Text.Trim()));

            comm.Parameters.Add(new SqlParameter("@crea_date2", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")));
            comm.Parameters.Add(new SqlParameter("@crea_user2", lblIDNO.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@upd_date2", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")));
            comm.Parameters.Add(new SqlParameter("@upd_user2", lblIDNO.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@loginname", ""));

            comm.Parameters.Add(new SqlParameter("@AGT_IDNo", lblIDNO.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@AGT_PW", txbUSR_PASSWD.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@AGT_CompNO", lblSN.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@AGT_ID", txbUSR_ID.Text.Trim())); //舊的ID
            comm.Parameters.Add(new SqlParameter("@CONT_TEL_Ext", txbCNTA_T3_ZIP.Text.Trim()));

            comm.Parameters.Add(new SqlParameter("@AGT_City", ddlAddr_City.SelectedValue));
            comm.Parameters.Add(new SqlParameter("@AGT_Country", ddlAddr_Country.SelectedValue));
            comm.Parameters.Add(new SqlParameter("@AGT_AddrZip", txbAddr_ccd.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@AGT_Addr", txbAddr.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@AGT_Is_FN", (rbIND_FG.SelectedValue == "1" ? "Y" : "N"))); //是否靠行

            comm.Parameters.Add(new SqlParameter("@AGT_Sex", (strAGT_Sex.Length != 1 ? "" : strAGT_Sex))); //性別
            intCheck = comm.ExecuteNonQuery();
            comm.Dispose();
        }
        finally
        {
            connect.Close();
        }

        if (intCheck > -1)
        {
            //Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "alert('歡迎您加入本公司同業會員！!經由本公司確認通知後，請由同業專業登入!!'); window.location = 'Logined.aspx';", True)
            //this.Response.Write("<script language='javascript' type='text/javascript'>alert('歡迎您加入本公司同業會員！!經由本公司確認通知後，請由同業專業登入!'); window.location = 'Default.aspx';</script>");
            fn_Send_XML(lblIDNO.Text.Trim(), lblSN.Text.Trim());
        }
        else
        {
            //Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "alert('新增失敗，請確認資料，重新輸入！');", True)
            this.Response.Write("<script language='javascript' type='text/javascript'>alert('新增失敗，請確認資料，重新輸入!');</script>");
        }
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

    protected string CheckIdcode(string strIDNo)
    {
        string strMessage = "";
        string Letter = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string[] MirrorNumber = {"10","11","12","13","14","15","16","17","34","18","19","20","21","22","35","23","24","25","26","27","28","29","32","30","31","33"};
        string[] Checker = {"1","9","8","7","6","5","4","3","2","1","1"};
        string N1 = strIDNo.ToString().Substring(0,1).ToUpper();
        int I = 0;
        int N1Ten = 0;
        int N1Unit = 0;
        int Total = 0;
        byte Pos = Convert.ToByte(Letter.IndexOf(N1).ToString());
        if (Pos < 0)
        {
            strMessage = "申請人身份證字號第 1 碼必須為英文字母";
            return strMessage;
        }
        if (strIDNo.Length < 10)
        {
            strMessage = "申請人身份證字號共有 10 碼";
            return strMessage;
        }
        for (I = 1; I <= 9; I++)
        {
            if (!(Convert.ToInt16(strIDNo.Substring(I, 1)[0]) >= 48 & Convert.ToInt16(strIDNo.Substring(I, 1)[0]) <= 57))
            {
                strMessage = "申請人身份證字號第 2 ~ 9 碼必須為數字";
                return strMessage;
            }
        }
        if (!(strIDNo.Substring(1, 1) == "1" | strIDNo.Substring(1, 1) == "2"))
        {
            strMessage = "申請人身份證字號第 2 碼必須為 1 或 2";
            return strMessage;
        }
        N1Ten = Convert.ToInt16(MirrorNumber[Pos].Substring(0, 1));
        N1Unit =Convert.ToInt16(MirrorNumber[Pos].Substring(1, 1));
        Total = N1Ten * Convert.ToInt16(Checker[0]) + N1Unit * Convert.ToInt16(Checker[1]);
        for (I = 2; I <= 10; I++)
        {
            Total = Total + Convert.ToInt16(strIDNo.Substring(I-1, 1)) * Convert.ToInt16(Checker[I]);
        }
        if (Total % 10 != 0)
        {
            strMessage = "申請人身份證字號輸入錯誤！";
            return strMessage;
        }

        return strMessage;
    }


    #region " --- 傳送XML資料 --- "
    protected void fn_Send_XML(string id,string compid)
    {
        //要傳送的資料
        string strXML = "";
        strXML += "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
        strXML += "<Message>";
        strXML += "<MA>";
        strXML += "<A><![CDATA[" + id + "]]></A>";  
        strXML += "<B><![CDATA[" + compid + "]]></B>";
        strXML += "</MA>";
        strXML += "</Message>";

        string strURL = "http://210.71.206.199:502/xml/GetB2BJoin.aspx";
        //string strURL = "http://localhost:35425/OLApply2B/xml/GetApplyChecked.aspx";
        string strComNum = "";
        fn_Show_XML(SendRequest(strURL, strXML, strComNum));

    }

    /// <summary>
    /// 傳送的function
    /// </summary>
    /// <param name="uri">網址</param>
    /// <param name="poscontent">傳送的資料</param>
    /// <returns></returns>
    public string SendRequest(string uri, string poscontent, string strComNum)
    {
        string strMessage = "";
        string responseText = "";

        //設置編碼
        byte[] postBody = System.Text.Encoding.UTF8.GetBytes(poscontent);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
        request.Method = "POST";
        //request.Timeout = 60000;  //設置超時屬性。默認為100000毫秒（100秒）。
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = postBody.Length;
        request.AllowWriteStreamBuffering = true;
        HttpWebResponse response = null;
        Stream dataStream = null;
        StreamReader reader = null;
        try
        {
            dataStream = request.GetRequestStream();
            dataStream.Write(postBody, 0, postBody.Length);
            dataStream.Close();
            response = (HttpWebResponse)request.GetResponse();
            dataStream = response.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("UTF-8");
            reader = new StreamReader(dataStream, encode);
            responseText = reader.ReadToEnd(); //回傳結果
        }
        catch (WebException ex1)
        {
            //HttpWebResponse exResponse = (HttpWebResponse)ex1.Response;
            //MessageBox.Show(ex1.Message);
            strMessage = ex1.ToString();
        }
        catch (NotSupportedException ex2)
        {
            //MessageBox.Show(ex2.Message);
            strMessage = ex2.ToString();
        }
        catch (ProtocolViolationException ex3)
        {
            //MessageBox.Show(ex3.Message);
            strMessage = ex3.ToString();
        }
        catch (InvalidOperationException ex4)
        {
            //MessageBox.Show(ex4.Message);
            strMessage = ex4.ToString();
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.ToString());
            strMessage = ex.ToString();
        }
        finally
        {
            if (response != null) response.Close();
            if (dataStream != null) dataStream.Close();
            if (reader != null) reader.Close();
        }

        if (string.IsNullOrEmpty(responseText))
        {
            string strPrint = "";
            strPrint = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

            strPrint += "<SHOWMSG>";
            strPrint += "<SHOWDATA>";
            strPrint += "<SHOPID>" + strComNum + "</SHOPID>";
            strPrint += "<DETAIL_NUM></DETAIL_NUM >";
            strPrint += "<DETAIL_ITEM></DETAIL_ITEM >";
            strPrint += "<STATUS_CODE><![CDATA[1003]]></STATUS_CODE>";
            strPrint += "<STATUS_DESC><![CDATA[系統維護中]]></STATUS_DESC>";
            strPrint += "<SYS_DESC><![CDATA[" + strMessage + "]]></SYS_DESC>";
            strPrint += "<CONFIRM>FAIL</CONFIRM>";
            strPrint += "</SHOWDATA>";
            strPrint += "</SHOWMSG>";


            responseText = strPrint;
        }

        return responseText;
    }
    #endregion

    #region " --- 回傳XML資料 --- "
    /// <summary>
    /// 顯示回傳的xml資料
    /// </summary>
    /// <param name="xmlData"></param>
    protected void fn_Show_XML(string xmlData)
    {
        //if (System.Web.HttpContext.Current.Request.RequestType == "GET")
        {
            //接收並讀取POST過來的XML資料
            StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Request.InputStream);
            try
            {
                XmlDocument dom = new XmlDocument();
                dom.LoadXml(xmlData);

                ResponseXML = dom.SelectSingleNode("SHOWMSG").ChildNodes.Item(0).ChildNodes.Item(6).InnerText;
                string errorStr = dom.SelectSingleNode("SHOWMSG").ChildNodes.Item(0).ChildNodes.Item(4).InnerText;

                if (ResponseXML == "OK") 
                { 
                    this.Response.Write("<script language='javascript' type='text/javascript'>alert('歡迎您加入本公司同業會員！!經由本公司確認通知後，請由同業專業登入!'); window.location = 'Default.aspx';</script>"); 
                }
                else
                {
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('報名失敗')", true);
                    delData(lblIDNO.Text.Trim());
                    
                    Response.Write("<script language='javascript' type='text/javascript'>alert('" + errorStr.Replace("'","\"") + "');</script>");
                    //Response.Write("<script language='javascript' type='text/javascript'>alert('失敗');</script>");

                }
                //顯示回傳訊息結果
                //string strPrint = "";
                //strPrint += "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                //strPrint += "<SHOWMSG>";
                //strPrint += Show_XML(dom.SelectSingleNode("SHOWMSG").ChildNodes);
                //strPrint += "</SHOWMSG>";

                //System.Web.HttpContext.Current.Response.Write(strPrint);
            }
            catch (Exception ex)
            {
                string strPrint = "";
                strPrint += "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                strPrint += "<SHOWMSG>";
                strPrint += ex.ToString();
                strPrint += "</SHOWMSG>";

                //System.Web.HttpContext.Current.Response.ContentType = "text/xml";
                System.Web.HttpContext.Current.Response.Write(strPrint);
            }
        }
    }
    /// <summary>
    /// 顯示XML訊息
    /// </summary>
    /// <param name="strXmlMA"></param>
    /// <returns></returns>
    protected string Show_XML(XmlNodeList xnlXmlMA)
    {
        string strPrint = "";
        for (int ii = 0; ii <= xnlXmlMA.Count - 1; ii++)
        {
            System.Xml.XmlNodeList xnlXml = xnlXmlMA.Item(ii).ChildNodes;

            strPrint += xnlXmlMA.Item(ii).Name;
            strPrint += xnlXmlMA.Item(ii).InnerXml;
            //if (xnlXml.Count == 1)
            //{
            //    strPrint += "<" + xnlXmlMA.Item(ii).Name + ">" + xnlXmlMA.Item(ii).InnerXml + "</" + xnlXmlMA.Item(ii).Name + ">";
            //}
            //else
            //{
            //    strPrint += "<" + xnlXmlMA.Item(ii).Name + ">";
            //    strPrint += Show_XML(xnlXml);
            //    strPrint += "</" + xnlXmlMA.Item(ii).Name + ">";
            //}
        }

        return strPrint;
    }
    private void delData(string strB)
    {
        string strsql = "";

        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(constring);

        try
        {
            connect.Open();

            strsql = " delete AGENT_L WHERE AGT_IDNo = @ID";
            SqlCommand cmd = new SqlCommand(strsql, connect);
            cmd.Parameters.Add(new SqlParameter("@ID", strB));
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            strsql = " delete IND_FG WHERE IND_FG_IDNo = @ID";
            cmd = new SqlCommand(strsql, connect);
            cmd.Parameters.Add(new SqlParameter("@ID", strB));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        catch { }
        finally { connect.Close(); }
    }
    #endregion
}
