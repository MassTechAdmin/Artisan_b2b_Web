﻿using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Data.SqlClient;

namespace clsFunction
{
    /// <summary>
    /// Check 相關
    /// </summary>
    public class Check
    {


        /// <summary>
        /// 檢查是否為數字
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(object Expression)
        {
            // Variable to collect the Return value of the TryParse method.
            bool isNum;

            // Define variable to collect out parameter of the TryParse method. If the conversion fails, the out parameter is zero.
            double retNum;

            // The TryParse method converts a string in a specified style and culture-specific format to its double-precision floating point number equivalent.
            // The TryParse method does not generate an exception if the conversion fails. If the conversion passes, True is returned. If it does not, False is returned.
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

            return isNum;
        }

        /// <summary>
        /// 檢查日期是否正確
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsDate(object dt)
        {
            try
            {
                //System.DateTime.Parse(dt.ToString().Substring(0, 4) + "/" + dt.ToString().Substring(4, 2) + "/" + dt.ToString().Substring(6, 2));
                System.DateTime.Parse(dt.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 檢查"會員"登入Session
        /// </summary>
        public static void CheckSession()
        {
            if (string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["PERNO"])) ||
                string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["Compno"])) ||
                string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["PerName"])) ||
                string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["PerIDNo"])))
            {
                string strURL = HttpContext.Current.Request.Url.AbsoluteUri;
                HttpContext.Current.Response.Cookies["URLCOOKIE"].Expires = DateTime.Now.AddDays(1);
                HttpContext.Current.Response.Cookies["URLCOOKIE"].HttpOnly = true;
                //HttpContext.Current.Response.Cookies["URLCOOKIE"].Secure = true;
                HttpContext.Current.Response.Cookies["URLCOOKIE"]["URL"] = strURL;

                HttpContext.Current.Response.Redirect("~/Default.aspx");
            }
        }

        public static void Check_Login_ID_And_PW()
        {
            if (string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["TripName"])))
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
            }
        }

        /// <summary>
        /// 檢查會員是否審核中
        /// </summary>
        public static bool Check_Ing(string ID)
        {
            string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
            SqlConnection connect = new SqlConnection(strConnString);
            string strSql = "";

            //先判斷是否審核中
            strSql = " select OLD_ID FROM Confirm where OLD_ID = @OLD_ID and isnull(del_date,'') = ''";
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@OLD_ID", ID));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Close();
                comm.Dispose();
                connect.Close();
                return true;
            }
            else
            {
                reader.Close();
                comm.Dispose();
                connect.Close();
                return false;
            }
        }

        /// <summary>
        /// 檢查會員是否審核通過
        /// </summary>
        public static bool Check_Account()
        {
            bool blnReturn = false;
            string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
            SqlConnection connect = new SqlConnection(strConnString);
            string strSql = "";
            strSql = " SELECT AGT_IDNo, AGT_IsVerify";
            strSql += " FROM AGENT_L";
            strSql += " WHERE AGENT_L.AGT_IDNo = @AGT_IDNo";
            strSql += " AND AGT_IsVerify = 1";

            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@AGT_IDNo", Convert.ToString(HttpContext.Current.Session["PERNO"])));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            { blnReturn = true; }
            else
            { blnReturn = false; }
            reader.Close();
            comm.Dispose();
            connect.Close();
            return blnReturn;
        }
    }
/// <summary>
    /// 寄送 E-Mail 相關設定
    /// </summary>
    public class EMail
    {
        //寄件者名稱
        private static string strSendName = "巨匠旅遊同業服務網";//"Intertek Website";
        private static string strPort = "25";//"25";
        //寄件者mail
        private static string strFrom = "artisan@mail.mass-tech.com.tw";//strSendName + "<Intertek.website@intertek.com>";
        //是否採用 Html 格式
        private static bool bolIsHtml = true;
        //是否使用 Secure Sockets Layer (SSL) 加密連線
        private static bool bolIsSsl = false;

        /// <summary>
        /// 判斷是否為整確的mail格式
        /// </summary>
        /// <param name="strMail">E-Mail</param>
        public static bool IsEmail(string strMail)
        {
            string[] strAddMail = strMail.Split(';');
            for (int i = 0; i <= strAddMail.Length - 1; i++)
            {
                string strRegex = "^[_\\.0-9a-z-]+@([0-9a-z][0-9a-z-]+\\.){1,4}[a-z]{2,3}$";
                Regex re = new Regex(strRegex);
                if (!re.IsMatch(strAddMail[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// EMamil發送
        /// </summary>
        /// <param name="strMailSubject"></param>
        /// <param name="strMailAddress"></param>
        /// <param name="strMailBody"></param>
        /// <returns></returns>
        public static bool Send_Mail(string strMailSubject, string strMailAddress, string strMailBody)
        {
            //寄件資料
            if (!string.IsNullOrEmpty(strMailAddress))
            {
                try
                {
                    string[] strAddMail = strMailAddress.Split(';');
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "mail.mass-tech.com.tw";
                    smtp.Port = 25;
                    smtp.Credentials = new System.Net.NetworkCredential("artisan@mail.mass-tech.com.tw", "720822");
                    smtp.EnableSsl = bolIsSsl;
                    using (MailMessage msg = new MailMessage())
                    {
                        msg.From = new MailAddress(strFrom);
                        for (int jj = 0; jj < strAddMail.Length; jj++)
                            msg.To.Add(new MailAddress(strAddMail[jj]));
                        msg.Subject = strMailSubject;
                        msg.Body = strMailBody;
                        //訊息內容
                        msg.SubjectEncoding = System.Text.Encoding.UTF8;
                        msg.BodyEncoding = System.Text.Encoding.UTF8;
                        msg.IsBodyHtml = bolIsHtml;

                        smtp.Send(msg);
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}