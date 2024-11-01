using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.OleDb;
using System.Xml;
using System.IO;
using System.Net;
using System.Text;
using System.Data.SqlClient;

namespace MyFunction
{
    #region " --- 安全性檢查 ---"
    /// <summary>
    /// ClassFunction 的摘要描述
    /// </summary>
    public class ClassFunction
    {
       
        /// <summary>
        /// 檢查網址是否合法
        /// </summary>
        public void CheckWebAddress()
        {
            string strCheckData = "'|ox|;|and|exec|insert|select|delete|update|count|*|%|chr|mid|master|truncate|char|declare|aspx|asp|php";
            string[] strSplitData = strCheckData.Split((char)'|');

            int urlCount = System.Web.HttpContext.Current.Request.QueryString.Keys.Count;
            string[] str = new string[urlCount];

            for (int i = 0; i <= urlCount - 1; i++)
            {
                str[i] = System.Web.HttpContext.Current.Request.QueryString.Get(i).ToString();
                for (int intIndex = 0; intIndex <= strSplitData.Length - 1; intIndex++)
                {
                    if (str[i].Trim().ToLower().IndexOf(strSplitData[intIndex]) > 0)
                    {
                        System.Web.HttpContext.Current.Response.Redirect("http://www.google.com/");
                        System.Web.HttpContext.Current.Response.End();
                    }

                }
            }
        }

        ///// <summary>
        ///// 回傳系統位置
        ///// </summary>
        ///// <returns></returns>
        //public static string Return_Web_Site()
        //{
        //    //return "http://210.71.206.200/cobweb/";
        //    return "http://act.mass-tech.com.tw/";
        //}
    }
    #endregion

    #region " --- 檢查 ---"
    /// <summary>
    /// 檢查
    /// </summary>
    public class Check
    {
        /// <summary>
        /// 檢查是否為數定
        /// </summary>
        /// <param name="Expression">傳入元件</param>
        /// <returns>True/False</returns>
        public static bool IsNumeric(object Expression)
        {
            bool isNum;

            double retNum;

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
                System.DateTime.Parse(dt.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 將英數等半形符號，每兩個字元視為一個全形字/長度
        /// </summary>
        /// <param name="TheMixedString">混合字串(來源)</param>
        /// <param name="TheLength">欲取得之字串長度</param>
        /// <returns>String</returns>
        /// <remarks></remarks>
        public static string Get_HalfWay(string TheMixedString, int TheLength)
        {
            try
            {
                if (string.IsNullOrEmpty(TheMixedString) | TheLength <= 0)
                {
                    return " ";
                }
                else
                {
                    char c = '\0';
                    byte[] strbytes = null;
                    float sngTotalLength = 0.0f;
                    int intPosition = 0;
                    //foreach (char c_loopVariable in TheMixedString.ToCharArray())
                    for (int ii = 0; ii < TheMixedString.Length; ii++)
                    {
                        //c = c_loopVariable;
                        //strbytes = Encoding.UTF8.GetBytes(c);
                        strbytes = Encoding.UTF8.GetBytes(TheMixedString.Substring(ii, 1));
                        if (strbytes.Length == 3)
                        {
                            sngTotalLength += 1;
                        }
                        else
                        {
                            sngTotalLength += 0.5f;
                        }

                        //'這段的用意是用來避免讓回傳字串長度超出範圍
                        if (sngTotalLength + 0.5 <= TheLength)
                        {
                            intPosition += 1;
                        }
                        else
                        {
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }

                    if (TheMixedString.Length > TheLength)
                    {
                        return TheMixedString.Substring(0, intPosition + 1) + "...";
                    }
                    else
                    {
                        return TheMixedString.Substring(0, intPosition + 1);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return " ";
        }
    }
    /// <summary>
    /// 檢查登入的帳號密碼
    /// </summary>
    public static class Check_Login
    {
        /// <summary>
        /// 檢查帳號、密碼
        /// </summary>
        public static void Check_Login_ID_And_PW()
        {
            if (HttpContext.Current.Session["PerID"] == null || HttpContext.Current.Session["PerPW"] == null || HttpContext.Current.Session["PerName"] == null)
            {
                HttpContext.Current.Response.Write("<script language='javascript' type='text/javascript'>alert('閒置時間過久, 請重新登入。'); window.parent.location='login.aspx';</script>");
                HttpContext.Current.Response.End();
            }
            else
            {
                HttpContext.Current.Session["PerID"] = HttpContext.Current.Session["PerID"]; //密碼
                HttpContext.Current.Session["PerPW"] = HttpContext.Current.Session["PerPW"]; //員工帳號
            }
        }
    }
    #endregion

    //#region " --- 抓取網站相關資料 ---"
    //public class WebData
    //{
    //    /// <summary>
    //    /// 回傳網站標題
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string Return_WebTitle()
    //    {
    //        string strWebTitle = "";
    //        string strSql = "";
    //        strSql += " SELECT WebTitle FROM Web_Setup";
    //        System.Data.OleDb.OleDbConnection conn = db.CreateConnection();
    //        try
    //        {
    //            conn.Open();
    //            System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(strSql, conn);
    //            System.Data.OleDb.OleDbDataReader reader = command.ExecuteReader();
    //            if (reader.Read())
    //            {
    //                strWebTitle = reader["WebTitle"].ToString();
    //            }
    //            reader.Close();
    //            command.Dispose();
    //        }
    //        finally
    //        {
    //            conn.Close();
    //        }

    //        return strWebTitle;
    //    }

    //    /// <summary>
    //    /// 回傳系統參數
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string Return_Global_Set(string strGLB_Code)
    //    {
    //        string strWebTitle = "";
    //        string strSql = " SELECT [GLB_Code],[GLB_Desc] FROM Global_Set WHERE [GLB_Code]=@GLB_Code";
    //        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ExchangeCenterConnectionString"].ToString();
    //        SqlConnection connect = new SqlConnection(strConnString);
    //        try
    //        {
    //            connect.Open();
    //            SqlCommand command = new SqlCommand(strSql, connect);
    //            command.Parameters.Add(new SqlParameter("@GLB_Code", strGLB_Code));
    //            SqlDataReader reader = command.ExecuteReader();
    //            if (reader.Read())
    //            {
    //                strWebTitle = reader["GLB_Desc"].ToString();
    //            }
    //            reader.Close();
    //            command.Dispose();
    //        }
    //        finally
    //        {
    //            connect.Close();
    //        }

    //        return strWebTitle;
    //    }
    //}
    //#endregion

    //報名資料

    #region 日期
    /// <summary>
    /// 日期
    /// </summary>
    public class Date
    {
        /// <summary>
        /// 取得某一日期是該年的第幾週
        /// </summary>
        /// <param name="dt">日期</param>
        /// <returns>該日期在該年的週數</returns>
        public static int GetWeekOfYear(DateTime dt)
        {
            System.Globalization.GregorianCalendar gc = new System.Globalization.GregorianCalendar();

            return gc.GetWeekOfYear(dt, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }
    }
    #endregion   
}

