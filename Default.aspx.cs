using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

public partial class Default_b2b : System.Web.UI.Page
{
    string indexUrl = "https://www.artisan.com.tw/";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

        fn_Show_Chose();
        getVision();
        getExceptional();
        get_DMbanner();
    }

    #region === 登入 ===
    protected void ImageButtonLogin_Click(object sender, EventArgs e)
    {
        fn_Login_Check();
    }

    private void fn_Login_Check()
    {
        Session["PERNO"] = null; //帳號
        Session["Compno"] = null; //公司編號
        Session["PerName"] = null; //員工姓名
        Session["PerIDNo"] = null; //員工身份證字號

        if (string.IsNullOrEmpty(TextBox1.Text.Trim()))
        {
            TextBox1.Focus();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('帳號必須輸入！');", true);
            return;
        }

        if (string.IsNullOrEmpty(TextBox2.Text.Trim()))
        {
            TextBox2.Focus();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('密碼必須輸入！');", true);
            return;
        }

        if (TextBox3.Text.Trim().ToUpper() != Convert.ToString(Session["v$code"]))
        {
            TextBox2.Text = "";
            TextBox3.Text = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('驗證碼錯誤,請輸入驗證碼！');", true);
            return;
        }

        fn_Login_Data();
    }

    private void fn_Login_Data()
    {
        Session["PERNO"] = null; //帳號
        Session["Compno"] = null; //公司編號
        Session["PerName"] = null; //員工姓名
        Session["PerIDNo"] = null; //員工身份證字號

        string strAccount = TextBox1.Text.Trim();
        string strPassword = TextBox2.Text.Trim();
        string strSql = "";
        strSql += " SELECT AGENT_L.AGT_IDNo,AGENT_L.AGT_PW,AGENT_L.AGT_CompNO,AGENT_L.AGT_IDNo,AGENT_M.COMP_NO,AGENT_L.AGT_CONT";
        strSql += " FROM AGENT_L";
        strSql += " LEFT JOIN AGENT_M ON AGENT_M.AGT_NAME1 = AGENT_L.AGT_NAME1";
        strSql += " WHERE AGENT_L.AGT_IDNO = @AGT_IDNO";
        strSql += " AND AGENT_L.AGT_PW = @AGT_PW";
        strSql += " AND AGENT_L.AGT_IsVerify = 1";
        //strSql += " AND AGENT_M.AGT_Check = 'Y'";

        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(constring);
        try
        {
            connect.Open();

            SqlCommand command = new SqlCommand(strSql, connect);
            command.Parameters.Add(new SqlParameter("@AGT_IDNO", TextBox1.Text.Trim()));
            command.Parameters.Add(new SqlParameter("@AGT_PW", TextBox2.Text.Trim()));
            //必需要經過審核過的，才能放行
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Session.Timeout = 120;
                Session["PERNO"] = reader["AGT_IDNo"].ToString().Trim(); //帳號
                Session["Compno"] = reader["COMP_NO"].ToString().Trim(); //公司編號
                Session["PerName"] = reader["AGT_CONT"].ToString().Trim(); //員工姓名
                Session["PerIDNo"] = reader["AGT_IDNo"].ToString().Trim(); //員工身份證字號
            }
            else
            {
                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox1.Focus();
                //Label1.Text = "找不到此帳號的資料，請重新登入或驗證碼輸入錯誤";
            }
            reader.Close();
            command.Dispose();
        }
        finally
        {
            connect.Close();
        }

        if (!string.IsNullOrEmpty(Convert.ToString(Session["PERNO"])) & !string.IsNullOrEmpty(Convert.ToString(Session["Compno"])) & !string.IsNullOrEmpty(Convert.ToString(Session["PerName"])))
        {
            string strURL = "~/index.aspx";

            try
            {
                string newip = IPAddress;
                strSql = " insert into loginaccount (";
                strSql += " login_id, ip, indexdate, inputdate";
                strSql += " ) values (";
                strSql += " @login_id, @ip, @indexdate, getdate()";
                strSql += " )";
                connect.Open();
                SqlCommand comm5 = new SqlCommand(strSql, connect);
                comm5.Parameters.Add(new SqlParameter("@login_id", TextBox1.Text.Trim()));
                comm5.Parameters.Add(new SqlParameter("@ip", newip));
                comm5.Parameters.Add(new SqlParameter("@indexdate", DateTime.Today.ToShortDateString()));
                comm5.ExecuteNonQuery();
                comm5.Dispose();
                connect.Close();

                HttpCookie cookies = Request.Cookies["URLCOOKIE"];
                if (cookies != null)
                {
                    strURL = cookies.Values["URL"];
                    cookies.Expires = System.DateTime.Now.AddDays(-1);
                    cookies.Values.Clear();
                    Response.Cookies.Set(cookies);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/");
            }
            finally { connect.Close();}

            if (strURL != "")
            {
                Response.Redirect(strURL, false);
            }
            else
            {
                Response.Redirect("~/");
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('找不到此帳號的資料，請重新登入或驗證碼輸入錯誤！');", true);
        }
    }

    public static string IPAddress
    {
        get
        {
            string result = String.Empty;
            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (result != null && result != String.Empty)
            {
                //可能有代理    
                if (result.IndexOf(".") == -1)    //沒有，肯定是非IPv4格式  
                    result = null;
                else
                {
                    if (result.IndexOf(",") != -1)
                    {
                        //有，估計有多個代理，取第一個不是內網的ip。    
                        result = result.Replace(" ", "").Replace("'", "");
                        string[] temparyip = result.Split(",;".ToCharArray());
                        for (int i = 0; i < temparyip.Length; i++)
                        {
                            if (isIPAddress(temparyip[i])
                                && temparyip[i].Substring(0, 3) != "10."
                                //&& temparyip[i].Substring(0, 7) != "192.168"
                                && temparyip[i].Substring(0, 7) != "172.16.")
                            {
                                return temparyip[i];    //找到不是內網的地址   
                            }
                        }
                    }
                    else if (isIPAddress(result)) //代理即是IP格式    
                        return result;
                    else
                        result = null;    //代理中的内容 非IP，取IP    
                }
            }
            string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (null == result || result == String.Empty)
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (result == null || result == String.Empty)
                result = HttpContext.Current.Request.UserHostAddress;
            return result;
        }
    }

    private static bool isIPAddress(string strAddress)
    {
        bool bResult = true;

        foreach (char ch in strAddress)
        {
            if ((false == Char.IsDigit(ch)) && (ch != '.'))
            {
                bResult = false;
                break;
            }
        }

        return bResult;
    }
    #endregion

    #region === 橫幅廣告輪播 ===
    protected void fn_Show_Chose()
    {
        string strSql = "";
        //strSql += " select * from Special_Offer";
        //strSql += " ORDER BY Orderby";
        strSql = "  select top 13 Number,L_Title,Picture,URL";
        strSql += " from Spotlight_Main ";  //Special_Event
        strSql += " ORDER BY cast(OrderBy as int)";
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
                strData += "<ul class='b2b-bannerBar-tool'>";
                while (reader.Read())
                {
                    strData += "<li><a href=\"" + reader["URL"].ToString() + "\"><img src='https://www.artisan.com.tw/Zupload16/new_web/" + reader["Picture"].ToString() + "' alt='' /></a></li>";
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

    #region === 新視界 ===
    private void getVision()
    {
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection conn = new SqlConnection(strConnString);

        try
        {
            conn.Open();

            string strsql = "";
            string str = "";
            strsql = " select * from NewVision";
            strsql += " order by OrderBY,Number desc";

            SqlCommand comm = new SqlCommand(strsql, conn);
            SqlDataReader reader = comm.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    str += "<li><a href='" + reader["url"].ToString() + "'>";
                    str = getTripData(reader["Trip_no"].ToString(), str);

                    litVision.Text += str;
                    str = "";
                }
            }

            comm.Dispose();
            reader.Close();
        }
        catch { }
        finally { conn.Close(); conn = null; }
    }

    private string getTripData(string tripNo, string str)
    {
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection conn = new SqlConnection(strConnString);

        try
        {
            conn.Open();

            string strSql = "";
            string strTripNo = fn_RtnTripNo(tripNo);
            strSql += " SELECT  Top 1 IsNull(Min(Case When IsNull(Grop.Grop_Tour,0) > 9000 Then (Grop_Tour) End),0) AS Grop_Tour , Trip_Name";
            strSql += " FROM Trip";
            strSql += " LEFT JOIN Grop ON Trip.Trip_no = grop.trip_no";
            strSql += " WHERE 1=1";
            strSql += " AND tourtype <> 'ITF'";
            if (!string.IsNullOrEmpty(strTripNo)) { strSql += " AND Grop.Trip_no in (" + strTripNo + ")"; }
            strSql += " AND (isnull(hidden,'') <> 'y')";
            strSql += " AND (Grop.TourType = N'典藏' OR Trip.Trip_Hide=0)";
            strSql += " AND Grop_Depa >= getdate()";
            strSql += " AND IsNull(Grop.Grop_Tour,0) > 9000";
            strSql += " Group by Trip_Name";

            SqlCommand comm = new SqlCommand(strSql, conn);
            SqlDataReader reader = comm.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    str += reader["Trip_Name"].ToString();
                    str += "</a></li>";
                }
            }
            else { str = ""; }

            comm.Dispose();
            reader.Close();
        }
        catch { }
        finally { conn.Close(); conn = null; }

        return str;
    }

    protected string fn_RtnTripNo(string strTripNo)
    {
        string strSql = "";
        strSql = "SELECT trip_no FROM trip";
        strSql += " WHERE 1=1";
        strSql += " AND Trip.Trip_Hide=0";
        if (!string.IsNullOrEmpty(strTripNo))
            strSql += " AND Trip.Trip_Classify = @Trip_Classify";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            if (!string.IsNullOrEmpty(strTripNo))
                comm.Parameters.Add(new SqlParameter("@Trip_Classify", strTripNo));
            SqlDataReader reader = comm.ExecuteReader();
            strTripNo = "'" + strTripNo + "'";
            while (reader.Read())
            {
                strTripNo += (string.IsNullOrEmpty(strTripNo) ? "" : ",") + "'" + reader["trip_no"].ToString() + "'";
            }
            reader.Close();
            comm.Dispose();
        }
        finally
        {
            connect.Close();
        }

        return strTripNo;
    }
    #endregion

    #region === 精選行程 ===
    protected void getExceptional()
    {
        //精選行程
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection conn = new SqlConnection(strConnString);
        string strsql = "";
        strsql  = " select top 13 *,Group_Category_Name";
        strsql += " from Exceptional_Choices";
        strsql += " left join Group_Category on GC_NO = Group_Category_No";
        strsql += " where 1=1 ";
        strsql += " and title <> ''";
        strsql += " order by orderby";

        try
        {
            conn.Open();
            SqlCommand comm = new SqlCommand(strsql, conn);
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                litExImg.Text += "<img src='" + indexUrl + "Zupload16/new_web/" + reader["Pic"].ToString() + "' style='display: none;' alt='' border='0'>";
                litSpan.Text += "<span></span> ";

                litExData.Text += "<ul style='display: none;'>　 " + reader["title"].ToString();
                if (reader["Schedule_title1"].ToString() != "")
                {
                    litExData.Text += "<li>";
                    litExData.Text += "<a href='" + reader["Schedule_url1"].ToString() + "'>";
                    litExData.Text += reader["Schedule_title1"].ToString() + "</a>";
                    //if (!string.IsNullOrEmpty(Convert.ToString(Session["ID"]) + ""))
                    //{
                    //    litExData.Text += "<a href='" + reader["Schedule_url1"].ToString() + "'>";
                    //    //litExData.Text += "<img src='images/icon_new.jpg' alt='new'>";
                    //    litExData.Text += reader["Schedule_title1"].ToString() + "</a>";
                    //}
                    //else 
                    //{ litExData.Text += reader["Schedule_title1"].ToString(); }
                    litExData.Text += "</li>";

                }

                for (int no = 2; no < 6; no++)
                {
                    if (reader["Schedule_title" + no.ToString()].ToString() != "")
                    {
                        litExData.Text += "<li>";
                        litExData.Text += "<a href='" + reader["Schedule_url" + no.ToString()].ToString() + "'>";
                        litExData.Text += reader["Schedule_title" + no.ToString()].ToString() + "</a>";
                        //if (!string.IsNullOrEmpty(Convert.ToString(Session["ID"]) + ""))
                        //{
                        //    litExData.Text += "<a href='" + reader["Schedule_url" + no.ToString()].ToString() + "'>";
                        //    litExData.Text += reader["Schedule_title" + no.ToString()].ToString() + "</a>";
                        //}
                        //else 
                        //{ litExData.Text += reader["Schedule_title" + no.ToString()].ToString(); }
                        litExData.Text += "</li>";
                    }
                }
                litExData.Text += "</ul>";
            }

            comm.Dispose();
            reader.Close();
        }
        catch { }
        finally { conn.Close();}
    }
    #endregion

    //DMbanner
    private void get_DMbanner()
    {
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection conn = new SqlConnection(strConnString);

        try
        {
            conn.Open();

            string strsql = "";
            strsql = " select top 6 * from New_Web_four";
            strsql += " where Tag = '6'";
            strsql += " order by cast(OrderBY as int),Number desc";

            SqlCommand comm = new SqlCommand(strsql, conn);
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                litDM_banner.Text += "<a href='" + reader["url"].ToString() + "'>";
                litDM_banner.Text += "<img src='" + reader["Intro"].ToString() + "' alt='' />";
                litDM_banner.Text += "</a>";
            }

            comm.Dispose();
            reader.Close();
        }
        catch { }
        finally { conn.Close(); conn = null; }
    }
}