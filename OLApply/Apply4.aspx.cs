﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Web.UI.WebControls;
using System.Xml;

public partial class OLApply_Apply4 : System.Web.UI.Page
{
    int intTax = 0;    //稅險
    int intVisa = 0;   //簽證
    int intDiscount = 0;   //折扣
    bool boolSend = true ; //簡訊是否已發送
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        Load_Data();
        Load_Data2();

        if (!boolSend)
        {
            string strNetNumber = Lal_Numb.Text;
            string strContent = Lbl_Grop_Numb.Text + "/" + hidcomp_name.Value + "/" + Lbl_Name.Text + "/" + hidConnPhone.Value + "/" ;
            strContent += (Lbl_Grop_Name.Text.Length > 20 ? Lbl_Grop_Name.Text.Substring(0, 19) : Lbl_Grop_Name.Text) + "/" + hidBookPax.Value + "人/" + hidReg_Status.Value;
            string strPhone = Lbl_Phone.Text;
            //Response.Write(strContent + "-----" + strContent.Length);
            //strPhone = "0928519630";
            try
            {
                if (Load_Data_SMS(strNetNumber) != "Y")
                {
                    //fn_send_sms(strNetNumber, strContent, strPhone);
                    LineRelease(strNetNumber, strContent, strPhone);
                }
            }
            finally
            {
            }
        }
    }

    protected void Load_Data()
    {
        string strSql = "";
        strSql += " DECLARE @net_no nvarchar(20); ";
        strSql += " select top 1 @net_no = Number from TR10_OL where crea_user = @crea_user order by EnliI_Date desc ";
        strSql += " SELECT netcustnumb ,EnliI_Date ,Tour_Numb ,Comp_Code ,Comp_Conn ,L.CONT_CELL ,BookPax ";
        strSql += " ,T.Remark ,Remark2 ,T.sale_code ,Grop_Name ,Grop_Depa ";
        strSql += " ,Down_Payment ,Reg_Status ,Number ,Van_Number, SMS , L.SALE_CODE AS LineOwn";
        strSql += " ,Group_Name.Group_Name";
        strSql += " FROM TR10_OL T ";
        strSql += " left join trip.dbo.Grop G on T.Tour_Numb = G.Grop_Numb";
        strSql += " left join trip.dbo.Group_Name Group_Name on Group_Name.Group_Name_No = G.Group_Name_No";
        strSql += " left join AGENT_L L on T.Comp_Code = L.AGT_NAME1 and T.Comp_Conn = L.AGT_CONT ";
        strSql += " where Number = @net_no ";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@crea_user", Session["PERNO"]));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                //報名單號
                Lal_Numb.Text = reader["netcustnumb"].ToString();
                //團號
                Lbl_Grop_Numb.Text = reader["Tour_Numb"].ToString();
                //出發日期
                hidDepa.Value = Convert.ToDateTime(reader["Grop_Depa"].ToString()).ToString("MMdd");
                Lbl_Grop_Depa.Text = Convert.ToDateTime(reader["Grop_Depa"].ToString()).ToString("yyyy/MM/dd");
                string strDayOfWeek = "";
                switch (Convert.ToInt16(Convert.ToDateTime(reader["Grop_Depa"].ToString()).DayOfWeek))
                {
                    case 0:
                        strDayOfWeek = " (日)";
                        break;
                    case 1:
                        strDayOfWeek = " (一)";
                        break;
                    case 2:
                        strDayOfWeek = " (二)";
                        break;
                    case 3:
                        strDayOfWeek = " (三)";
                        break;
                    case 4:
                        strDayOfWeek = " (四)";
                        break;
                    case 5:
                        strDayOfWeek = " (五)";
                        break;
                    case 6:
                        strDayOfWeek = " (六)";
                        break;
                }
                Lbl_Grop_Depa.Text += strDayOfWeek;
                //訂購人
                Lbl_Name.Text = reader["Comp_Conn"].ToString();
                //Agent公司名稱
                hidcomp_name.Value = reader["Comp_Code"].ToString();
                //Agent聯絡人手機
                hidConnPhone.Value = reader["CONT_CELL"].ToString();
                //團名
                Lbl_Grop_Name.Text = reader["Group_Name"].ToString();
                //業務編號
                hidSaleCode.Value = reader["LineOwn"].ToString();
                
                Lbl_Check.Text = (reader["Reg_Status"].ToString() == "HK" ? "HK　機位確認" : "RQ　候補");
                if (reader["Reg_Status"].ToString() == "HK")
                {
                    hidReg_Status.Value = "HK";
                    //報名狀態
                    msg_tk.Visible = true;
                    //付款DL
                    int intHour = 0;
                    switch(Convert.ToDateTime(reader["EnliI_Date"].ToString()).DayOfWeek.ToString())
                    {
                        case "Sunday":
                            intHour = 48;
                            break;
                        case "Monday":
                            intHour = 48;
                            break;
                        case "Tuesday":
                            intHour = 48;
                            break;
                        case "Wednesday":
                            intHour = 48;
                            break;
                        case "Thursday":
                            intHour = 96;
                            break;
                        case "Friday":
                            intHour = 96;
                            break;
                        case "Saturday":
                            intHour = 72;
                            break;
                    }
                    Lbl_DL1.Text = "付款DL：" + Convert.ToDateTime(reader["EnliI_Date"].ToString()).AddHours(intHour).ToString("yyyy/MM/dd    HH:mm");
                    Lbl_DL2.Text = "請於" + Convert.ToDateTime(reader["EnliI_Date"].ToString()).AddHours(intHour).ToString("yyyy/MM/dd    HH:mm") + "之前完成訂金付款，逾時未繳款恕無法為您保留團體機位。";
                }
                else
                {
                    hidReg_Status.Value = "RQ";
                    msg_tk.Visible = false;
                }
                //報名訂金
                Lbl_BookPax.Text = reader["BookPax"].ToString() + "　席  *  " + reader["Down_Payment"].ToString() + " = ";
                Lbl_BookPax.Text += Convert.ToInt32(reader["BookPax"].ToString()) * Convert.ToInt32(reader["Down_Payment"].ToString());
                hidBookPax.Value = reader["BookPax"].ToString();
                //售價
                fn_Show_Tour_Price(reader["Van_Number"].ToString(), reader["Number"].ToString());
                //簡訊是否已發送
                if (string.IsNullOrEmpty(reader["SMS"].ToString())) boolSend = false; else boolSend = true;
            }
            reader.Close();
            comm.Dispose();
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('出錯了。');", true);
            return;
        }
        finally
        {
            connect.Close();
        }

        //團體售價
        int value1 = int.Parse(Price_Total_0.Text, NumberStyles.AllowThousands);
        if(!string.IsNullOrEmpty(Price_Total_1.Text)) value1 += int.Parse(Price_Total_1.Text, NumberStyles.AllowThousands);
        if (!string.IsNullOrEmpty(Price_Total_2.Text)) value1 += int.Parse(Price_Total_2.Text, NumberStyles.AllowThousands);
        if (!string.IsNullOrEmpty(Price_Total_3.Text)) value1 += int.Parse(Price_Total_3.Text, NumberStyles.AllowThousands);
        PT1.Text = value1.ToString("#,0");
        //金額合計
        PT2.Text = value1.ToString("#,0");
    }

    protected void fn_Show_Tour_Price(string strNumber, string strTr10Number)
    {
        string strSql = "";
        strSql = " SELECT [Number],TP.Tick_Type,TP.Bed_Type,IsNull([SalePrice],0) AS SalePrice ";
        strSql += " ,IsNull([AgentPrice],0) AS AgentPrice,BookPax,Tour_Mony ";
        strSql += " FROM Tour_Price TP ";
        strSql += " left join grop on TP.Number = grop.Van_Number ";
        strSql += " left join B2B.dbo.tr20_OL t20 on grop.Grop_Numb = t20.Grop_Numb ";
        strSql += " WHERE Number = @Number ";
        strSql += " and t20.Tr10Number = @Tr10Number ";
        strSql += " and t20.Tick_Type = TP.Tick_Type ";
        strSql += " and t20.Bed_Type = TP.Bed_Type ";
        strSql += " ORDER BY Sequ_No ";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@Number", strNumber));
            comm.Parameters.Add(new SqlParameter("@Tr10Number", strTr10Number));
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                int intPrice = Convert.ToInt32(reader["Tour_Mony"].ToString());
                if (reader["Tick_Type"].ToString() == "A")
                {
                    //沒價格要鎖
                    if (intPrice > 9000)
                    {
                        Price_Show_0.Text = Convert.ToInt32(intPrice.ToString()).ToString("#,0");
                        Price_Single_0.Text = Convert.ToInt32(intPrice.ToString()).ToString("#,0");
                        Price_Discount_0.Text = intDiscount.ToString();
                        Price_People_0.Text = reader["BookPax"].ToString();
                        R0.Text = reader["BookPax"].ToString();
                        Price_Total_0.Text = ((intPrice - intDiscount) * Convert.ToInt32(Price_People_0.Text)).ToString("#,0");
                    }
                    else
                    {
                        Price_Show_0.Text = "0"; Price_Single_0.Text = "0"; Price_Discount_0.Text = "0"; Price_People_0.Text = "0"; Price_Total_0.Text = "0";
                    }
                }
                else
                {
                    switch (reader["Bed_Type"].ToString().ToUpper())
                    {
                        case "1": // 小孩 佔床
                            //沒價格要鎖
                            if (intPrice > 9000)
                            {
                                Price_Show_1.Text = Convert.ToInt32(intPrice.ToString()).ToString("#,0");
                                Price_Single_1.Text = Convert.ToInt32(intPrice.ToString()).ToString("#,0");
                                Price_Discount_1.Text = intDiscount.ToString();
                                Price_People_1.Text = reader["BookPax"].ToString();
                                R1.Text = reader["BookPax"].ToString();
                                Price_Total_1.Text = ((intPrice - intDiscount) * Convert.ToInt32(Price_People_1.Text)).ToString("#,0");
                            }
                            else
                            {
                                Price_Show_1.Text = "0"; Price_Single_1.Text = "0"; Price_Discount_1.Text = "0"; Price_People_1.Text = "0"; Price_Total_1.Text = "0";
                            }
                            break;
                        case "2": // 小孩 不佔床
                            //沒價格要鎖
                            if (intPrice > 9000)
                            {
                                Price_Show_3.Text = Convert.ToInt32(intPrice.ToString()).ToString("#,0");
                                Price_Single_3.Text = Convert.ToInt32(intPrice.ToString()).ToString("#,0");
                                Price_Discount_3.Text = intDiscount.ToString();
                                Price_People_3.Text = reader["BookPax"].ToString();
                                R3.Text = reader["BookPax"].ToString();
                                Price_Total_3.Text = ((intPrice - intDiscount) * Convert.ToInt32(Price_People_3.Text)).ToString("#,0");
                            }
                            else
                            {
                                Price_Show_3.Text = "0"; Price_Single_3.Text = "0"; Price_Discount_3.Text = "0"; Price_People_3.Text = "0"; Price_Total_3.Text = "0";
                            }
                            break;
                        case "3": // 小孩 加床
                            //沒價格要鎖
                            if (intPrice > 9000)
                            {
                                Price_Show_2.Text = Convert.ToInt32(intPrice.ToString()).ToString("#,0");
                                Price_Single_2.Text = Convert.ToInt32(intPrice.ToString()).ToString("#,0");
                                Price_Discount_2.Text = intDiscount.ToString();
                                Price_People_2.Text = reader["BookPax"].ToString();
                                R2.Text = reader["BookPax"].ToString();
                                Price_Total_2.Text = ((intPrice - intDiscount) * Convert.ToInt32(Price_People_2.Text)).ToString("#,0");
                            }
                            else
                            {
                                Price_Show_2.Text = "0"; Price_Single_2.Text = "0"; Price_Discount_2.Text = "0"; Price_People_2.Text = "0"; Price_Total_2.Text = "0";
                            }
                            break;
                    }
                }
            }
            reader.Close();
            comm.Dispose();
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('出錯了。');", true);
            return;
        }
        finally
        {
            connect.Close();
        } 
    }

    protected void Load_Data2()
    {
        string strSql = "";
        strSql += " SELECT Person.Name,Person.Pager,Person.Compno";
        strSql += " FROM AGENT_L";
        strSql += " left join Person on AGENT_L.SALE_CODE = Person.perno";
        strSql += " where AGENT_L.AGT_IDNo = @AGT_IDNo";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@AGT_IDNo", Session["PERNO"]));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                //業務代表 
                Lbl_Sales.Text += reader["name"].ToString();
                //手機號碼
                Lbl_Phone.Text = reader["pager"].ToString();
                //公司電話
                switch (reader["compno"].ToString())
                {
                    case "A":
                        Lbl_TEL.Text += "(02)2518-0011";
                        break;
                    case "B":
                        Lbl_TEL.Text += "(07)2419-888";
                        break;
                    case "C":
                        Lbl_TEL.Text += "(04)2255-1168";
                        break;
                    case "D":
                        Lbl_TEL.Text += "(03)3371-222";
                        break;
                    case "F":
                        Lbl_TEL.Text += "(06)222-6736";
                        break;
                    case "H":
                        Lbl_TEL.Text += "(03)5283-088";
                        break;

                }
            }
            reader.Close();
            comm.Dispose();
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('出錯了。');", true);
            return;
        }
        finally
        {
            connect.Close();
        }
    }

    protected string Load_Data_SMS(string strNetNumber)
    {
        string strReturn = "";
        string strSql = "";
        strSql += " SELECT SMS FROM TR10_OL";
        strSql += " where NetCustNumb = @NetCustNumb";
        strSql += " and SMS = 'Y'";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@NetCustNumb", strNetNumber));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                //業務代表 
                strReturn = reader["SMS"].ToString();
            }
            reader.Close();
            comm.Dispose();
        }
        finally
        {
            connect.Close();
        }

        return strReturn;
    }

    #region "簡訊寄送"
    protected void fn_send_sms(string strNetNumber, string strContent, string strPhone)
    {
        WebRequest myHttpWebRequest = WebRequest.Create("http://xsms.aptg.com.tw/XSMSAP/api/APIRTFastRequest");
        myHttpWebRequest.Method = "POST";
        StringBuilder sbPost = new StringBuilder();
        //設定XML內容
        sbPost.Append("<soap-env:Envelope xmlns:soap-env='http://schemas.xmlsoap.org/soap/envelope/'>");
        sbPost.Append("     <soap-env:Header/>");
        sbPost.Append("     <soap-env:Body>");
        sbPost.Append("         <Request>");
        sbPost.Append("             <MDN>0985505137</MDN>");
        sbPost.Append("             <UID>0985505137</UID>");
        sbPost.Append("             <UPASS>123456</UPASS>");
        sbPost.Append("             <Subject>B2B線上報名</Subject>");
        sbPost.Append("             <Retry>Y</Retry>");
        sbPost.Append("             <AutoSplit>N</AutoSplit>");
        sbPost.Append("             <StopDateTime>" + Convert.ToDateTime(System.DateTime.Now).ToString("yyyyMMdd") + "2359</StopDateTime>");
        sbPost.Append("             <Message>" + strContent + "</Message>");
        sbPost.Append("             <MDNList>");
        //發送的手機號碼
        //sbPost.Append("                 <MSISDN>" + strPhone + "</MSISDN>");
        if (!string.IsNullOrEmpty(strPhone))
        {
            sbPost.Append("                 <MSISDN>" + strPhone + "</MSISDN>");
        }
        else
        {
            // 若是找不到資料時，就會寄送給卡哥、偉琪、茂祥
            //sbPost.Append("                 <MSISDN>0932012350</MSISDN>"); //卡哥 
            //sbPost.Append("                 <MSISDN>0982906152</MSISDN>"); //偉琪
            //sbPost.Append("                 <MSISDN>0937086308</MSISDN>"); //茂祥
            sbPost.Append("                 <MSISDN>0930136596</MSISDN>"); //育龍
        }
        sbPost.Append("             </MDNList>");
        sbPost.Append("         </Request>");
        sbPost.Append("     </soap-env:Body>");
        sbPost.Append("</soap-env:Envelope>");

        //Response.Write(sbPost.ToString)

        String strMSG = "";
        UTF8Encoding encoding = new UTF8Encoding();
        String PostData = sbPost.ToString();
        Byte[] byte1 = encoding.GetBytes(PostData);

        myHttpWebRequest.ContentType = "text/xml;charset=utf-8";
        myHttpWebRequest.ContentLength = byte1.Length;
        Stream newStream = myHttpWebRequest.GetRequestStream();
        newStream.Write(byte1, 0, byte1.Length);
        newStream.Close();
        HttpWebResponse _response = (HttpWebResponse)myHttpWebRequest.GetResponse();
        StreamReader sr = new StreamReader(_response.GetResponseStream(), System.Text.Encoding.UTF8);
        String GetURL = sr.ReadToEnd();
        strMSG += GetURL.ToString();

        sr.Close();
        _response.Close();

        //Response.Write(strMSG);
        //Response.End();

        string strSql = "";
        strSql += " insert into SMS_log (netcustnumb,Send_Phone,Send_Date,SMS_Content,Reply) values (@netcustnumb,@Send_Phone,@Send_Date,@SMS_Content,@Reply) ";
        strSql += " update TR10_OL set SMS = 'Y' where netcustnumb = @netcustnumb ";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@netcustnumb", strNetNumber));
            comm.Parameters.Add(new SqlParameter("@Send_Phone", strPhone));
            comm.Parameters.Add(new SqlParameter("@Send_Date", Convert.ToDateTime(System.DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss")));
            comm.Parameters.Add(new SqlParameter("@SMS_Content", strContent));
            comm.Parameters.Add(new SqlParameter("@Reply", strMSG));
            comm.ExecuteNonQuery();
            comm.Dispose();
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('出錯了。');", true);
            return;
        }
        finally
        {
            connect.Close();
        }

        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('簡訊已發送。'); location='/index.aspx';", true); return;
    }
    #endregion

    #region "傳LINE訊息"
    protected void fn_SMSLog(string strNetNumber, string strPhone, string strContent, string strMSG)
    {
        string strSql = "";
        strSql += " insert into SMS_log (netcustnumb,Send_Phone,Send_Date,SMS_Content,Reply) values (@netcustnumb,@Send_Phone,@Send_Date,@SMS_Content,@Reply) ";
        strSql += " update TR10_OL set SMS = 'Y' where netcustnumb = @netcustnumb ";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@netcustnumb", strNetNumber));
            comm.Parameters.Add(new SqlParameter("@Send_Phone", strPhone));
            comm.Parameters.Add(new SqlParameter("@Send_Date", Convert.ToDateTime(System.DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss")));
            comm.Parameters.Add(new SqlParameter("@SMS_Content", strContent));
            comm.Parameters.Add(new SqlParameter("@Reply", strMSG));
            comm.ExecuteNonQuery();
            comm.Dispose();
        }
        catch
        {
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('出錯了。');", true);
            //return;
        }
        finally
        {
            connect.Close();
        }
    }
    ///<summary>
    ///
    ///</summary>>
    ///<param name="xmlData"></param>
    protected void LineRelease(string strNetNumber, string strContent, string strPhone)
    {
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection conn = new SqlConnection(strConnString);
        try
        {
            string strMessage = "";
            strMessage += "" + strContent + "\r\n";

            conn.Open();
            string strsql = "";
            strsql = "select Person.Line_Code,PAgent.Line_Code as Line_Code_Agent from [Person]";
            strsql += " left join Person PAgent on PAgent.perno = Person.Agent_Person";
            strsql += " where Person.perno = @perno";
            SqlCommand comm = new SqlCommand(strsql, conn);
            comm.Parameters.Add(new SqlParameter("@perno", hidSaleCode.Value));

            SqlDataReader reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    // 報名單業務的line
                    if (!String.IsNullOrEmpty(reader["Line_Code"].ToString()))
                    {
                        string strXML = "";
                        strXML += "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                        strXML += "<Message>";
                        strXML += "<MA>";
                        strXML += "<A><![CDATA[lineCodeArtisan]]></A>";
                        strXML += "<B>" + reader["Line_Code"].ToString() + "</B>";
                        strXML += "<C>" + strMessage + "</C>";
                        strXML += "</MA>";
                        strXML += "</Message>";
                        XmlDocument XmlD = new XmlDocument();
                        XmlD.LoadXml(strXML);
                        Stream oWriter = null;

                        byte[] data = System.Text.Encoding.UTF8.GetBytes(XmlD.OuterXml);
                        HttpWebRequest myRequest = ((HttpWebRequest)(WebRequest.Create("http://210.71.206.199:502/xml/SendLineMessage.aspx")));
                        //HttpWebRequest myRequest = ((HttpWebRequest)(WebRequest.Create("http://localhost:17202/xml/SendLineMessage.aspx")));
                        myRequest.Method = "POST";
                        myRequest.Timeout = 300000;
                        myRequest.ContentType = "application/x-www-form-urlencoded";
                        myRequest.ContentLength = data.Length;
                        myRequest.ContentType = "text/xml;charset=utf-8 ";
                        oWriter = myRequest.GetRequestStream();
                        oWriter.Write(data, 0, data.Length);
                        oWriter.Close();

                        fn_SMSLog(strNetNumber, reader["Line_Code"].ToString(), strMessage, "Y");
                    }

                    // 代理人的line
                    if (!String.IsNullOrEmpty(reader["Line_Code_Agent"].ToString()))
                    {
                        string strXML = "";
                        strXML += "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                        strXML += "<Message>";
                        strXML += "<MA>";
                        strXML += "<A><![CDATA[lineCodeArtisan]]></A>";
                        strXML += "<B>" + reader["Line_Code_Agent"].ToString() + "</B>";
                        strXML += "<C>" + strMessage + "</C>";
                        strXML += "</MA>";
                        strXML += "</Message>";
                        XmlDocument XmlD = new XmlDocument();
                        XmlD.LoadXml(strXML);
                        Stream oWriter = null;

                        byte[] data = System.Text.Encoding.UTF8.GetBytes(XmlD.OuterXml);
                        HttpWebRequest myRequest = ((HttpWebRequest)(WebRequest.Create("http://210.71.206.199:502/xml/SendLineMessage.aspx")));
                        //HttpWebRequest myRequest = ((HttpWebRequest)(WebRequest.Create("http://localhost:17202/xml/SendLineMessage.aspx")));
                        myRequest.Method = "POST";
                        myRequest.Timeout = 300000;
                        myRequest.ContentType = "application/x-www-form-urlencoded";
                        myRequest.ContentLength = data.Length;
                        myRequest.ContentType = "text/xml;charset=utf-8 ";
                        oWriter = myRequest.GetRequestStream();
                        oWriter.Write(data, 0, data.Length);
                        oWriter.Close();

                        fn_SMSLog(strNetNumber, reader["Line_Code_Agent"].ToString(), strMessage, "Y");
                    }
                }
            }
            reader.Close();
            comm.Dispose();
        }
        catch (Exception ex)
        {
            fn_SMSLog(strNetNumber, "傳送失敗", ex.ToString() + "\r\n" + strContent, "N");
        }
        finally
        {
            conn.Close();
        }
    }
    #endregion
}