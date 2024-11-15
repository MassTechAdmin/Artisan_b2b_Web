﻿using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

namespace Counter
{
    /// <summary>
    /// Counter 的摘要描述
    /// </summary>
    public class Counter
    {
        #region 計數器
        public static void fn_Account()
        {
            string areano =  Convert.ToString(HttpContext.Current.Request["area_no"] + ""); //Convert.ToString(request["area_no", ""]);
            string TripNo = Convert.ToString(HttpContext.Current.Request["TripNo"] +  "");
            string FileName = VirtualPathUtility.GetFileName(HttpContext.Current.Request.FilePath);
            bool blnIsData = false;
            string newip = IPAddress;
            string typeno = "";
            //給予分類之值
            if (FileName == "Logined.aspx")
            { typeno = "1"; } //登入後首頁值為1
            else if (FileName == "A_Join.aspx")
            { typeno = "2"; } //申請帳號頁面值為2
            else if (FileName == "ClassifyProduct.aspx" && TripNo == "" && areano == "")
            { typeno = "3"; } //總圖表值為3
            else if (FileName == "Sel.aspx")
            { typeno = "4"; } //報名管理值為4
            else if (FileName == "Rraveler.aspx")
            { typeno = "5"; } //行程主檔值為5
            else if (FileName == "Default.aspx")
            { typeno = "6"; } //B2B首頁值為6
            else if (FileName == "TripList.aspx" && TripNo != "")
            { typeno = "7"; }
            else if (FileName == "TripIntroduction.aspx" && TripNo != "")
            { typeno = "7"; }
            else { typeno = "0"; }
            if (typeno != "7")
            {
                string strsql = "Select * From TourAccountTop10 where ip ='" + newip + "' and type ='" + typeno + "'";
                string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
                SqlConnection conn = new SqlConnection(strConnString);
                conn.Open();
                SqlCommand comm = new SqlCommand(strsql, conn);
                SqlDataReader reader = comm.ExecuteReader();
                //當一個ip-a加1之後，其ip-a要在加1必須等十個人看過網頁之後，總數才會在加1
                if (reader.Read() == false && typeno != "0")
                {   
                    blnIsData = true;
                }
                reader.Close();
                comm.Dispose();


                if (blnIsData)
                {
                    int intCnt = 0;
                    strsql = "Select count(*) as CNT From TourAccountTop10 where type ='" + typeno + "'";
                    SqlCommand comm2 = new SqlCommand(strsql, conn);
                    SqlDataReader reader2 = comm2.ExecuteReader();
                    if (reader2.Read())
                    {
                        intCnt = Convert.ToInt32(reader2["CNT"].ToString());
                    }
                    reader2.Close();
                    comm2.Dispose();


                    if (Convert.ToInt16(intCnt) > 9)
                    {
                        string strID = "";
                        blnIsData = false;
                        strsql = " SELECT TOP 1 * FROM (";
                        strsql += " Select top 10 id";
                        strsql += " From TourAccountTop10";
                        strsql += " where type ='" + typeno + "'";
                        strsql += " order by id desc";
                        strsql += " ) TABLE1";
                        strsql += " ORDER BY ID";
                        SqlCommand comm3 = new SqlCommand(strsql, conn);
                        SqlDataReader reader3 = comm3.ExecuteReader();
                        if (reader3.Read())
                        {
                            blnIsData = true;
                            strID = reader3["id"].ToString();
                        }
                        comm3.Dispose();
                        reader3.Close();


                        if (blnIsData)
                        {
                            strsql = "delete From TourAccountTop10 where type ='" + typeno + "'";
                            SqlCommand comm4 = new SqlCommand(strsql, conn);
                            comm4.ExecuteNonQuery();
                            comm4.Dispose();
                        }
                    }



                    strsql = " insert into TourAccountTop10 (";
                    strsql += " ip,indexdate,inputdate,type";
                    strsql += " ) values (";
                    strsql += " '" + newip + "',";
                    strsql += " '" + DateTime.Today.ToShortDateString() + "',";
                    strsql += " getdate(), '" + typeno + "'";
                    strsql += " )";

                    SqlCommand comm5 = new SqlCommand(strsql, conn);
                    comm5.ExecuteNonQuery();
                    comm5.Dispose();



                    strsql = " insert into TourAccount (";
                    strsql += " ip,indexdate,inputdate,type";
                    strsql += " ) values (";
                    strsql += " '" + newip + "',";
                    strsql += " '" + DateTime.Today.ToShortDateString() + "',";
                    strsql += " getdate() ,'" + typeno + "'";
                    strsql += " )";
                    SqlCommand comm8 = new SqlCommand(strsql, conn);
                    comm8.ExecuteNonQuery();
                    comm8.Dispose();


                    blnIsData = false;
                    strsql = "select * from TourAccountTotal where type ='" + typeno + "'";
                    SqlCommand comm12 = new SqlCommand(strsql, conn);
                    SqlDataReader reader4 = comm12.ExecuteReader();
                    if (reader4.Read())
                    {
                        blnIsData = true;
                    }
                    reader4.Close();

                    // 
                    if (blnIsData)
                    {
                        strsql = "update TourAccountTotal set total = (select total from TourAccountTotal where type ='" + typeno + "') + 1 where type ='" + typeno + "'";
                        SqlCommand comm9 = new SqlCommand(strsql, conn);
                        comm9.ExecuteNonQuery();
                        comm9.Dispose();
                    }
                    else
                    {
                        strsql = " insert into TourAccountTotal (";
                        strsql += " total , type";
                        strsql += " ) values (";
                        strsql += "'1' , '" + typeno + "')";
                        SqlCommand comm11 = new SqlCommand(strsql, conn);
                        comm11.ExecuteNonQuery();
                        comm11.Dispose();
                    }
                }
                conn.Close();
            }
            else if(typeno == "7")
            {
                string strsql = "Select * From bindextop10 where ip ='" + newip + "' and company_id ='" + TripNo + "'";
                string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
                SqlConnection conn = new SqlConnection(strConnString);
                conn.Open();
                SqlCommand comm = new SqlCommand(strsql, conn);
                SqlDataReader reader = comm.ExecuteReader();
                //當一個ip-a加1之後，其ip-a要在加1必須等十個人看過網頁之後，總數才會在加1
                if (reader.Read() == false && typeno != "0")
                {
                    blnIsData = true;
                }
                reader.Close();
                comm.Dispose();


                if (blnIsData)
                {
                    int intCnt = 0;
                    strsql = "Select count(*) as CNT From BindexTop10 where company_id ='" + TripNo + "'";
                    SqlCommand comm2 = new SqlCommand(strsql, conn);
                    SqlDataReader reader2 = comm2.ExecuteReader();
                    if (reader2.Read())
                    {
                        intCnt = Convert.ToInt32(reader2["CNT"].ToString());
                    }
                    reader2.Close();
                    comm2.Dispose();


                    if (Convert.ToInt16(intCnt) > 9)
                    {
                        string strID = "";
                        blnIsData = false;
                        strsql = " SELECT TOP 1 * FROM (";
                        strsql += " Select top 10 id";
                        strsql += " From BindexTop10";
                        strsql += " where company_id ='" + TripNo + "'";
                        strsql += " order by id desc";
                        strsql += " ) TABLE1";
                        strsql += " ORDER BY ID";
                        SqlCommand comm3 = new SqlCommand(strsql, conn);
                        SqlDataReader reader3 = comm3.ExecuteReader();
                        if (reader3.Read())
                        {
                            blnIsData = true;
                            strID = reader3["id"].ToString();
                        }
                        comm3.Dispose();
                        reader3.Close();


                        if (blnIsData)
                        {
                            strsql = "delete From BindexTop10 where company_id ='" + TripNo + "'";
                            SqlCommand comm4 = new SqlCommand(strsql, conn);
                            comm4.ExecuteNonQuery();
                            comm4.Dispose();
                        }
                    }



                    strsql = " insert into BindexTop10 (";
                    strsql += " ip,indexdate,inputdate,company_id";
                    strsql += " ) values (";
                    strsql += " '" + newip + "',";
                    strsql += " '" + DateTime.Today.ToShortDateString() + "',";
                    strsql += " getdate(), '" + TripNo + "'";
                    strsql += " )";

                    SqlCommand comm5 = new SqlCommand(strsql, conn);
                    comm5.ExecuteNonQuery();
                    comm5.Dispose();



                    strsql = " insert into Bcount (";
                    strsql += " ip,indexdate,inputdate,company_id";
                    strsql += " ) values (";
                    strsql += " '" + newip + "',";
                    strsql += " '" + DateTime.Today.ToShortDateString() + "',";
                    strsql += " getdate() ,'" + TripNo + "'";
                    strsql += " )";
                    SqlCommand comm8 = new SqlCommand(strsql, conn);
                    comm8.ExecuteNonQuery();
                    comm8.Dispose();
                }
                conn.Close();
            }

            //20190125
            if (Convert.ToString(HttpContext.Current.Request["ga"] + "") != "")
            {
                fn_Account2();
            }  
        }

        private static void fn_Account2()
        {
            string areano = Convert.ToString(HttpContext.Current.Request["area_no"] + ""); //Convert.ToString(request["area_no", ""]);
            string classno = Convert.ToString(HttpContext.Current.Request["ga"] + "");
            string TripNo = Convert.ToString(HttpContext.Current.Request["TripNo"] + "");
            string productdate = Convert.ToString(HttpContext.Current.Request["Date"] + "");
            bool blnIsData = false;
            string newip = IPAddress;
            string typeno = "99";

            string strsql = "Select * From TourAccountTop10 where ip ='" + newip + "' and type ='" + typeno + "' and area_no = '" + areano + "' and Class_no = '" + classno + "'";
            string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["count_ConnectionString1"].ToString();
            SqlConnection conn = new SqlConnection(strConnString);
            conn.Open();
            SqlCommand comm = new SqlCommand(strsql, conn);
            SqlDataReader reader = comm.ExecuteReader();
            //當一個ip-a加1之後，其ip-a要在加1必須等十個人看過網頁之後，總數才會在加1
            if (reader.Read() == false && typeno != "0")
            {
                blnIsData = true;
            }
            reader.Close();
            comm.Dispose();


            if (blnIsData)
            {
                int intCnt = 0;
                strsql = "Select count(*) as CNT From TourAccountTop10 where type ='" + typeno + "' and area_no = '" + areano + "' and Class_no = '" + classno + "'";
                SqlCommand comm2 = new SqlCommand(strsql, conn);
                SqlDataReader reader2 = comm2.ExecuteReader();
                if (reader2.Read())
                {
                    intCnt = Convert.ToInt32(reader2["CNT"].ToString());
                }
                reader2.Close();
                comm2.Dispose();


                if (Convert.ToInt16(intCnt) > 9)
                {
                    string strID = "";
                    blnIsData = false;
                    strsql = " SELECT TOP 1 * FROM (";
                    strsql += " Select top 10 id";
                    strsql += " From TourAccountTop10";
                    strsql += " where type ='" + typeno + "' and area_no = '" + areano + "' and Class_no = '" + classno + "'";
                    strsql += " order by id desc";
                    strsql += " ) TABLE1";
                    strsql += " ORDER BY ID";
                    SqlCommand comm3 = new SqlCommand(strsql, conn);
                    SqlDataReader reader3 = comm3.ExecuteReader();
                    if (reader3.Read())
                    {
                        blnIsData = true;
                        strID = reader3["id"].ToString();
                    }
                    comm3.Dispose();
                    reader3.Close();


                    if (blnIsData)
                    {
                        strsql = "delete From TourAccountTop10 where type ='" + typeno + "' and area_no = '" + areano + "' and Class_no = '" + classno + "' and id <= '" + strID + "'";
                        SqlCommand comm4 = new SqlCommand(strsql, conn);
                        comm4.ExecuteNonQuery();
                        comm4.Dispose();
                    }
                }



                strsql = " insert into TourAccountTop10 (";
                strsql += " ip,indexdate,inputdate,type,area_no,class_no";
                strsql += " ) values (";
                strsql += " '" + newip + "',";
                strsql += " '" + DateTime.Today.ToShortDateString() + "',";
                strsql += " getdate(), '" + typeno + "','" + areano + "','" + classno + "'";
                strsql += " )";

                SqlCommand comm5 = new SqlCommand(strsql, conn);
                comm5.ExecuteNonQuery();
                comm5.Dispose();



                strsql = " insert into TourAccount (";
                strsql += " ip,indexdate,inputdate,type,area_no,class_no";
                strsql += " ) values (";
                strsql += " '" + newip + "',";
                strsql += " '" + DateTime.Today.ToShortDateString() + "',";
                strsql += " getdate() ,'" + typeno + "','" + areano + "','" + classno + "'";
                strsql += " )";
                SqlCommand comm8 = new SqlCommand(strsql, conn);
                comm8.ExecuteNonQuery();
                comm8.Dispose();


                blnIsData = false;
                strsql = "select * from TourAccountTotal where type ='" + typeno + "' and area_no = '" + areano + "' and Class_no = '" + classno + "'";
                SqlCommand comm12 = new SqlCommand(strsql, conn);
                SqlDataReader reader4 = comm12.ExecuteReader();
                if (reader4.Read())
                {
                    blnIsData = true;
                }
                reader4.Close();

                // 
                if (blnIsData)
                {
                    strsql = " update TourAccountTotal set ";
                    strsql += " total = (select total from TourAccountTotal where type ='" + typeno + "' and area_no = '" + areano + "' and Class_no = '" + classno + "') + 1 ";
                    strsql += " where type ='" + typeno + "' and area_no = '" + areano + "' and Class_no = '" + classno + "'";
                    SqlCommand comm9 = new SqlCommand(strsql, conn);
                    comm9.ExecuteNonQuery();
                    comm9.Dispose();
                }
                else
                {
                    strsql = " insert into TourAccountTotal (";
                    strsql += " total , type, area_no , class_no";
                    strsql += " ) values (";
                    strsql += "'1' , '" + typeno + "','" + areano + "','" + classno + "')";
                    SqlCommand comm11 = new SqlCommand(strsql, conn);
                    comm11.ExecuteNonQuery();
                    comm11.Dispose();
                }
            }
            conn.Close();
        }

        private static long request(string p)
        {
            throw new NotImplementedException();
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
                            result = result.Replace(" ", "").Replace("\"", "");
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

        /// <summary>
        /// 顯示計數器
        /// </summary>
        /// 
        private void fn_ShowCount()
        {
            string strAgentNo = System.Web.Configuration.WebConfigurationManager.AppSettings["AgentNo"].ToString();
            string strsql = "Select total From TourAccountTotal where companyid = '" + strAgentNo + "'";
            string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["count_ConnectionString1"].ToString();
            SqlConnection conn = new SqlConnection(strConnString);
            conn.Open();
            SqlCommand commCount = new SqlCommand(strsql, conn);
            SqlDataReader readerCount = commCount.ExecuteReader();

            string counter = "";
            string g = "";

            if (readerCount.Read())
            {
                counter = readerCount.GetValue(0).ToString();
            }
            commCount.Dispose();
            readerCount.Close();
            conn.Close();

            int i = 0;
            for (i = 0; i <= counter.Length - 1; i++)
            {
                g = g + counter.Substring(i, 1);
            }

            //lblCounter.Text = g.ToString();
        }
        #endregion
    }
}
