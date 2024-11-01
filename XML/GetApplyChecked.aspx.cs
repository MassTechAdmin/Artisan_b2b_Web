/* ****************************************************************************************************
// 同業線上報名使用
**************************************************************************************************** */
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using System.Transactions;
using System.IO;
using System.Xml;

public partial class GetApplyChecked : System.Web.UI.Page
{
    string strPrint = ""; //記錄回傳的XML訊息
    string xmlData = ""; //記錄傳過來的xml值
    String GLBConnectionString = "";
    String TRANConnectionString = "";
    String TRIPConnectionString = "";
    string SaleName = "";

    string[] scode = { "A", "G", "L", "C" };
    string[] scode2 = { "A00309", "G11227", "A00106", "" };

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Request.RequestType == "POST")
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "text/xml";
            Response.Charset = "UTF-8";

            strPrint += "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
            strPrint += "<SHOWMSG>";
            Apply_Insert();
            strPrint += "</SHOWMSG>";

            Response.Write(strPrint);
        }
    }
    #region " --- XML --- "
    /// <summary>
    /// 新增到Apply資料表
    /// </summary>
    protected void Apply_Insert()
    {
        string strXML_A = "", strXML_B = "", strXML_C = "";

        //接收並讀取POST過來的XML資料
        StreamReader reader = new StreamReader(Request.InputStream);
        xmlData = reader.ReadToEnd();
        try
        {
            //聲明一個XMLDoc文件對像,Load XML字串
            XmlDocument dom = new XmlDocument();
            dom.LoadXml(xmlData);

            //讀取XML訊息(Data)
            System.Xml.XmlNodeList strXmlMA = dom.SelectSingleNode("Message").ChildNodes;
            for (int ii = 0; ii <= strXmlMA.Count - 1; ii++)
            {
                if (strXmlMA.Item(ii).Name == "MA")
                {
                    System.Xml.XmlNodeList strXml = strXmlMA.Item(ii).ChildNodes;
                    #region " --- 讀取XML(Data)資料 --- "
                    for (int index = 0; index <= strXml.Count - 1; index++)
                    {
                        switch (strXml.Item(index).Name)
                        {
                            case "A": // 驗證碼
                                strXML_A = strXml.Item(index).InnerText;
                                break;
                            case "B": // 報名網路單號
                                strXML_B = strXml.Item(index).InnerText;
                                break;
                            case "C": // 團號
                                strXML_C = strXml.Item(index).InnerText;
                                break;
                        }
                    }
                    #endregion

                    #region " --- 寫入資料庫 --- "
                    fn_ReadData(strXML_B); //讀取報名資料
                    fn_ReadData_Agent_L(_Conn_Idno); //讀取內部的同業聯絡人資料
//if (_Comp_Conn != "" && _CONT_CELL != "")
                    if (_Comp_Conn != "")
                    {
                        fn_Strreserve(strXML_C);
                        fn_rtnDataTabel_B2B();

                        try
                        {
                            fn_InsertTR10(strXML_C, "");
                        }
                        catch { }

                        // 更新狀態回傳給TR10_OL
                        // 只要有一個是候補就顯示狀態為候補
                        fn_Update_Tr10_OL();

                        try
                        {
                            string code = checkSourceCode();
                            if (code != "")
                            {
                                SaleName = getSaleName(_Sale_Code);
                                getSale(code);
                                fn_InsertTR10(strXML_C, code);
                                Update_Pak(strXML_C, code);
                                updateReserve(code, strXML_C);
                            }
                        }
                        catch { }
                        

                        //回傳成功XML
                        strPrint += "<SHOWDATA>";
                        strPrint += "<SHOPID>00</SHOPID>";
                        strPrint += "<DETAIL_NUM>" + strXML_B + "</DETAIL_NUM >";
                        strPrint += "<DETAIL_ITEM></DETAIL_ITEM >";
                        strPrint += "<STATUS_CODE>0000</STATUS_CODE>";
                        strPrint += "<STATUS_DESC>成功</STATUS_DESC>";
                        strPrint += "<STATUS_OleDb>OK</STATUS_OleDb>";
                        strPrint += "<CONFIRM>OK</CONFIRM>";
                        strPrint += "</SHOWDATA>";
                    }
                    else
                    {
                        string strMailTo = System.Web.Configuration.WebConfigurationManager.AppSettings["mail:ErrorSend"];
                        string strBodyMsg = "";
                        strBodyMsg += "訊息提示: 沒有讀取到聯絡人資料<br>";
                        strBodyMsg += "網路報名單號: " + strXML_B + "<br>";

                        clsFunction.EMail.Send_Mail("B2B線上報名發生錯誤..." + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), strMailTo, strBodyMsg);
                        strPrint += "<SHOWDATA>";
                        strPrint += "<SHOPID>00</SHOPID>";
                        strPrint += "<DETAIL_NUM>" + strXML_B + "</DETAIL_NUM >";
                        strPrint += "<DETAIL_ITEM></DETAIL_ITEM >";
                        strPrint += "<STATUS_CODE>1000</STATUS_CODE>";
                        strPrint += "<STATUS_DESC><![CDATA[沒有讀取到聯絡人資料]]></STATUS_DESC>";
                        strPrint += "<STATUS_OleDb></STATUS_OleDb>";
                        strPrint += "<CONFIRM>FAIL</CONFIRM>";
                        strPrint += "</SHOWDATA>";
                    }

                    #endregion
                }
            }
        }
        catch (Exception ex)
        {
            string strErrorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber().ToString();
            string currentName = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetMethod().Name;
            //string callName = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileName();
            string strMailTo = System.Web.Configuration.WebConfigurationManager.AppSettings["mail:ErrorSend"];

            string strBodyMsg = "";
            strBodyMsg += "訊息提示: " + ex.Message.ToString() + "<br>";
            strBodyMsg += "錯誤行數: " + strErrorLine + "<br>";
            //strBodyMsg += "錯誤檔案: " + callName + "<br>";
            strBodyMsg += "呼叫方法: " + currentName + "<br>";
            strBodyMsg += "網路報名單號: " + strXML_B + "<br>";

            clsFunction.EMail.Send_Mail("B2B線上報名發生錯誤..." + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), strMailTo, strBodyMsg);
            strPrint += "<SHOWDATA>";
            strPrint += "<SHOPID>00</SHOPID>";
            strPrint += "<DETAIL_NUM>" + strXML_B + "</DETAIL_NUM >";
            strPrint += "<DETAIL_ITEM></DETAIL_ITEM >";
            strPrint += "<STATUS_CODE>1000</STATUS_CODE>";
            strPrint += "<STATUS_DESC><![CDATA[" + ex.Message + "]]></STATUS_DESC>";
            strPrint += "<STATUS_OleDb></STATUS_OleDb>";
            strPrint += "<CONFIRM>FAIL</CONFIRM>";
            strPrint += "</SHOWDATA>";
        }
    }
    /// <summary>
    /// 顯示XML訊息
    /// </summary>
    /// <param name="strXmlMA"></param>
    /// <returns></returns>
    public static string Show_XML(XmlNodeList xnlXmlMA)
    {
        bool bolIsWrite = false;

        string strMSG_ShopID = "", strMSG_Prod_Num = "", strMSG_Prod_Item = "", strMSG_Status_Code = "", strMSG_Status_Desc = "";
        string strMSG_Status_OleDb = "", strMSG_Confirm = "";
        string strPrint = "";

        for (int ii = 0; ii <= xnlXmlMA.Count - 1; ii++)
        {
            System.Xml.XmlNodeList xnlXml = xnlXmlMA.Item(ii).ChildNodes;

            if (xnlXml.Count == 1)
            {
                strPrint += "<" + xnlXmlMA.Item(ii).Name + ">" + xnlXmlMA.Item(ii).InnerXml + "</" + xnlXmlMA.Item(ii).Name + ">";

                switch (xnlXmlMA.Item(ii).Name.ToUpper())
                {
                    case "SHOPID":
                        strMSG_ShopID = xnlXmlMA.Item(ii).InnerText;
                        break;
                    case "DETAIL_NUM":
                        strMSG_Prod_Num = xnlXmlMA.Item(ii).InnerText;
                        break;
                    case "DETAIL_ITEM":
                        strMSG_Prod_Item = xnlXmlMA.Item(ii).InnerText;
                        break;
                    case "STATUS_CODE":
                        strMSG_Status_Code = xnlXmlMA.Item(ii).InnerText;
                        break;
                    case "STATUS_DESC":
                        strMSG_Status_Desc = xnlXmlMA.Item(ii).InnerText;
                        break;
                    case "STATUS_OleDb":
                        strMSG_Status_OleDb = xnlXmlMA.Item(ii).InnerText;
                        break;
                    case "CONFIRM":
                        strMSG_Confirm = xnlXmlMA.Item(ii).InnerText;
                        if (strMSG_Confirm == "FAIL")
                            bolIsWrite = true;
                        break;
                }
            }
            else
            {
                strPrint += "<" + xnlXmlMA.Item(ii).Name + ">";
                strPrint += Show_XML(xnlXml);
                strPrint += "</" + xnlXmlMA.Item(ii).Name + ">";
            }
        }

        return strPrint;
    }
    #endregion

    #region " --- 復原候補狀態 --- "
    /// <summary>
    /// 復原候補狀態
    /// </summary>
    /// <remarks></remarks>
    protected void fn_Strreserve(string strNum)
    {
        // 存檔記錄要發送簡訊的人員資料
        DataTable dt = new DataTable();
        dt.Columns.Add("Cust_Numb");
        dt.Columns.Add("Sale_Code");
        dt.Columns.Add("Tour_Numb");
        dt.Columns.Add("Comp_Code");
        dt.Columns.Add("Comp_Conn");
        dt.Columns.Add("ConnTel");
        dt.Columns.Add("BookPax");
        dt.Columns.Add("P_Count");
        dt.Columns.Add("Line_Code");
        dt.Columns.Add("Line_Code_Agent");

        // ****************************************************************************************************
        // Reserve 0 (候補 可復原 滿團下報名後變候補)
        // Reserve 1 (可收訂)
        // Reserve 2 (候補 不可復原 可收訂狀態被清成候補)
        // ****************************************************************************************************
        int total = 0; // 計算可復原人數
        string strKeep_Amount = "0"; //保留
        string strgrop = "";
        string strsql = "";
        //機位數 保留數 pak
        strsql = " select tour.NUM as 團號,isnull(tour.EXPECT,0) as 機位,isnull(tour.keep_amount,0) as 保留,isnull(tour.pak_amount,0) as pak,isnull(CLOS_PEOP,'') as CLOS_PEOP";
        strsql += " from tour ";
        strsql += " where num = @num";

        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["GLBConnectionString_247"].ToString();
        SqlConnection connect = new SqlConnection(constring);
        connect.Open();
        SqlCommand command = new SqlCommand(strsql, connect);
        command.Parameters.Add(new SqlParameter("@num", strNum));
        SqlDataReader reader = command.ExecuteReader();
        strsql = "";
        if (reader.Read())
        {
            strKeep_Amount = reader["保留"].ToString();

            // ****************************************************************************************************
            if (String.IsNullOrEmpty(reader["CLOS_PEOP"].ToString()))
            {
                //已收訂人數
                string strsql2 = "";
                strsql2 += " select count(*) as 已收訂";
                strsql2 += " from tr20 ";
                strsql2 += " where 1=1 ";
                strsql2 += " and grop_numb ='" + reader["團號"].ToString() + "'";
                strsql2 += " and reserve = 1 ";
                strsql2 += " and isnull(del_data,'') = ''";
                strsql2 += " and isnull(group_change,'') ='' ";
                strsql2 += " and tr20.tour_type <> '領隊'";
                strsql2 += " AND tr20.Tour_Type <> 'Join Tour'"; //Join Tour不列入候補 by roger 20150407
                strsql2 += " AND tr20.Tour_Type <> N'FIT票'";    //FIT票 不列印候補 by roger 20160413
                strsql2 += " AND tr20.Tick_Type <> 'INF'";       //INF 不計算候補 by roger 20160421 
                strsql2 += " AND tr20.Tour_Type <> 'Tour Only'"; //20190710

                string constring2 = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRANConnectionString_247"].ToString();
                SqlConnection connect2 = new SqlConnection(constring2);
                connect2.Open();
                SqlCommand command2 = new SqlCommand(strsql2, connect2);
                SqlDataReader reader2 = command2.ExecuteReader();
                if (reader2.Read())
                {
                    //計算可復原人數
                    total = Convert.ToInt32(reader["機位"].ToString()) - Convert.ToInt32(reader["保留"].ToString()) - Convert.ToInt32(reader["pak"].ToString()) - Convert.ToInt32(reader2["已收訂"].ToString()) - 1;
                }
                reader2.Close();
                command2.Dispose();
                connect2.Close();

                if (total > 0)
                {
                    //可復原編號
                    string strsql3 = "";
                    strsql3 += " select top " + total + " tr20.tr20number, tr20.crea_date, tr20.Reserve, tr10.NetCustNumb,tr10.Tour_Numb,tr10.Comp_Code,tr10.Comp_Conn";
                    strsql3 += " ,tr10.ConnTel,tr10.BookPax,tr10.Cust_Numb,tr10.Sale_Code,Person.Line_Code,PAgent.Line_Code as Line_Code_Agent";
                    strsql3 += " from tr20";
                    strsql3 += " left join tr10 on tr10.Cust_Numb = tr20.Cust_Numb";
                    strsql3 += " left join glb.dbo.Person Person on Person.perno = tr10.Sale_Code";
                    strsql3 += " left join glb.dbo.Person PAgent on PAgent.perno = Person.Agent_Person";
                    strsql3 += " where 1=1 ";
                    strsql3 += " and tr20.grop_numb ='" + reader["團號"].ToString() + "'";
                    strsql3 += " and tr20.reserve = 0 ";
                    strsql3 += " and isnull(tr20.del_data,'') = ''";
                    strsql3 += " and isnull(tr20.group_change,'') ='' ";
                    strsql3 += " order by tr20.crea_date,tr20.tr20Number";

                    SqlConnection connect3 = new SqlConnection(constring2);
                    connect3.Open();
                    SqlCommand command3 = new SqlCommand(strsql3, connect3);
                    SqlDataReader reader3 = command3.ExecuteReader();
                    while (reader3.Read())
                    {
                        //所有可復原編號
                        strgrop += (string.IsNullOrEmpty(strgrop) ? "" : ",") + reader3["tr20number"].ToString();

                        //寫入候補轉收訂log
                        strsql += " insert into clear_0to1_log (";
                        strsql += " Run_User, Cust_Numb, Tr20Number, Tour, Reserve, crea_date";
                        strsql += " ) values (";
                        strsql += " N'WEB',N'" + reader3["cust_numb"] + "', " + reader3["tr20number"] + ", N'" + reader["團號"].ToString() + "', " + reader3["Reserve"] + ", N'" + Convert.ToDateTime(reader3["crea_date"]).ToString("yyyy/MM/dd HH:mm:ss.fff") + "' ";
                        strsql += " )";

                        // 存檔記錄要發送簡訊的人員資料
                        DataRow[] foundRows = null;
                        foundRows = dt.Select("Cust_Numb='" + reader3["Cust_Numb"].ToString() + "' and Sale_Code='" + reader3["Sale_Code"].ToString() + "'");
                        if (foundRows.Length == 0)
                        {
                            DataRow row = dt.NewRow();
                            row["Cust_Numb"] = reader3["Cust_Numb"].ToString();
                            row["Sale_Code"] = reader3["Sale_Code"].ToString();
                            row["Tour_Numb"] = reader3["Tour_Numb"].ToString();
                            row["Comp_Code"] = reader3["Comp_Code"].ToString();
                            row["Comp_Conn"] = reader3["Comp_Conn"].ToString();
                            row["ConnTel"] = reader3["ConnTel"].ToString();
                            row["BookPax"] = reader3["BookPax"].ToString();
                            row["Line_Code"] = reader3["Line_Code"].ToString();
                            row["Line_Code_Agent"] = reader3["Line_Code_Agent"].ToString();
                            row["P_Count"] = 1;
                            dt.Rows.Add(row);
                        }
                        else
                        {
                            foundRows[0]["P_Count"] = (Int32)foundRows[0]["P_Count"] + 1;
                        }
                    }
                    reader3.Close();
                    command3.Dispose();
                    connect3.Close();
                }
            }
        }

        for (int i = 0; i <= total; i++)
        {
            if (i > 0)
            {
                if (strgrop.Length > 0)
                {
                    //可復原修改為可收訂狀態
                    string strsql4 = "";
                    strsql4 += " update tr20 ";
                    strsql4 += " set reserve = 1";
                    strsql4 += " ,enli_date = getdate() ";
                    strsql4 += " where 1=1 ";
                    strsql4 += " and tr20number in (" + strgrop + ") ";
                    //更新完tr20的資料，再記錄變動的資料
                    strsql4 += strsql;
                    string constring2 = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRANConnectionString_247"].ToString();
                    SqlConnection connect4 = new SqlConnection(constring2);
                    connect4.Open();
                    SqlCommand command4 = new SqlCommand(strsql4, connect4);
                    command4.ExecuteNonQuery();
                    command4.Dispose();
                }
            }
        }
        reader.Close();
        command.Dispose();
        connect.Close();


        //將資料更新到網站去
        fn_Update_Grop_Data(strNum, strKeep_Amount);
        //string constring5 = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        //SqlConnection connect5 = new SqlConnection(constring5);
        //connect5.Open();
        //strsql = "update grop set ";
        //strsql += " Reg_Reserve = '" + GET_Reg_Reserve(strNum) + "'";
        //strsql += " ,reg_standby = '" + strKeep_Amount + "'";
        //strsql += " ,Reg_CheckOK ='" + GET_Reg_CheckOK(strNum) + "'";
        //strsql += " where grop_numb = '" + strNum + "'";
        //SqlCommand command5 = new SqlCommand(strsql, connect5);
        //command5.ExecuteReader();
        //command5.Dispose();
        //connect5.Close();


        // 候補補上需發送LINE
        if (dt.Rows.Count > 0)
        {
            for (int ii = 0; ii <= dt.Rows.Count - 1; ii++)
            {
                // 報名單業務的line
                if (!string.IsNullOrEmpty(dt.Rows[ii]["Line_Code"].ToString()))
                {
                    string strMessage = "";
                    strMessage += "候補補上通知" + "\r\n";
                    strMessage += "團號：" + dt.Rows[ii]["Tour_Numb"].ToString() + "\r\n";
                    if (!string.IsNullOrEmpty(dt.Rows[ii]["Comp_Code"].ToString()))
                    {
                        strMessage += "同行名稱：" + dt.Rows[ii]["Comp_Code"].ToString() + "\r\n";
                    }
                    strMessage += "聯絡人：" + dt.Rows[ii]["Comp_Conn"].ToString() + "\r\n";
                    strMessage += "手機：" + dt.Rows[ii]["ConnTel"].ToString() + "\r\n";
                    strMessage += "報名人數：" + dt.Rows[ii]["BookPax"].ToString() + "\r\n";
                    strMessage += "補上人數：" + dt.Rows[ii]["P_Count"].ToString() + "\r\n";
                    //clsFunction.LINE.Is_Send_Line(dt.Rows[ii]["Line_Code"].ToString(), strMessage);
                }

                // 代理人的line
                if (!string.IsNullOrEmpty(dt.Rows[ii]["Line_Code_Agent"].ToString()))
                {
                    string strMessage = "";
                    strMessage += "候補補上通知" + "\r\n";
                    strMessage += "團號：" + dt.Rows[ii]["Tour_Numb"].ToString() + "\r\n";
                    if (!string.IsNullOrEmpty(dt.Rows[ii]["Comp_Code"].ToString()))
                    {
                        strMessage += "同行名稱：" + dt.Rows[ii]["Comp_Code"].ToString() + "\r\n";
                    }
                    strMessage += "聯絡人：" + dt.Rows[ii]["Comp_Conn"].ToString() + "\r\n";
                    strMessage += "手機：" + dt.Rows[ii]["ConnTel"].ToString() + "\r\n";
                    strMessage += "報名人數：" + dt.Rows[ii]["BookPax"].ToString() + "\r\n";
                    strMessage += "補上人數：" + dt.Rows[ii]["P_Count"].ToString() + "\r\n";
                    //clsFunction.LINE.Is_Send_Line(dt.Rows[ii]["Line_Code_Agent"].ToString(), strMessage);
                }
            }
        }
    }

    public void fn_Update_Grop_Data(String strNUM, String strKeep_Amount)
    {
        string strReg_Reserve = "0";
        string strReg_CheckOK = "0";
        string strReg_INF = "0";
        string strReg_FIT = "0";

        string strsql = "";
        // 抓取現有的統計資料
        strsql = " select sum(case when ISNULL(TR20.Group_Change,'') = '' and ISNULL(TR20.Del_Data,'') = '' and (TR20.Reserve = 0 or TR20.Reserve =2 ) then 1 else 0 end) as Reg_Reserve";//候補人數
        strsql += " ,sum(case when ISNULL(TR20.Group_Change,'') = '' and ISNULL(TR20.Del_Data,'') = '' and TR20.Reserve = 1 and tr20.AR_check = '1' then 1 else 0 end) as Reg_CheckOK";//收訂人數
        strsql += " ,sum(case when ISNULL(TR20.Group_Change,'') = '' and ISNULL(TR20.Del_Data,'') = '' and TR20.Tick_Type = 'INF' then 1 else 0 end) as 'Reg_INF'"; //INF
        strsql += " ,sum(case when ISNULL(TR20.Group_Change,'') = '' and ISNULL(TR20.Del_Data,'') = '' and TR20.Tour_Type = 'FIT票' then 1 else 0 end) as 'Reg_FIT'";//FIT票
        strsql += " from tr20";
        strsql += " where GROP_NUMB = @GROP_NUMB";
        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRANConnectionString_247"].ToString();
        SqlConnection connect = new SqlConnection(constring);
        connect.Open();
        SqlCommand command = new SqlCommand(strsql, connect);
        command.Parameters.Add(new SqlParameter("@GROP_NUMB", strNUM));
        SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            strReg_Reserve = reader["Reg_Reserve"].ToString();
            strReg_CheckOK = reader["Reg_CheckOK"].ToString();
            strReg_INF = reader["Reg_INF"].ToString();
            strReg_FIT = reader["Reg_FIT"].ToString();
        }
        reader.Close();
        command.Dispose();
        connect.Close();


        // 更新到網站
        strsql = "update grop set ";
        strsql += "  Reg_Standby = '" + strKeep_Amount + "'";
        strsql += " ,Reg_Reserve = '" + strReg_Reserve + "'";
        strsql += " ,Reg_CheckOK = '" + strReg_CheckOK + "'";
        strsql += " ,Reg_INF = '" + strReg_INF + "'";
        strsql += " ,Reg_FIT = '" + strReg_FIT + "'";
        strsql += " where grop_numb = '" + strNUM + "'";
        string constring5 = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect5 = new SqlConnection(constring5);
        connect5.Open();
        SqlCommand command5 = new SqlCommand(strsql, connect5);
        command5.ExecuteReader();
        command5.Dispose();
        connect5.Close();
    }

    //public object GET_Reg_Reserve(string num)
    //{
    //    int i = 0;
    //    string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRANConnectionString_247"].ToString();
    //    SqlConnection connect = new SqlConnection(constring);
    //    connect.Open();

    //    string strsql = "";
    //    strsql = " select count(*) as 候補人數 from tr20 ";
    //    strsql += " where (reserve = 0 or reserve =2 )  ";
    //    strsql += " and grop_numb ='" + num + "'";
    //    strsql += " and isnull(del_data,'') = ''";
    //    strsql += " and isnull(group_change,'') =''";

    //    SqlCommand command = new SqlCommand(strsql, connect);
    //    SqlDataReader reader = command.ExecuteReader();
    //    if (reader.Read())
    //    {
    //        i = Convert.ToInt32(reader["候補人數"].ToString());
    //    }
    //    reader.Close();
    //    command.Dispose();
    //    connect.Close();
    //    return i;
    //}

    //public object GET_Reg_CheckOK(string num)
    //{
    //    int i = 0;
    //    string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRANConnectionString_247"].ToString();
    //    SqlConnection connect = new SqlConnection(constring);
    //    connect.Open();

    //    string strsql = "";
    //    strsql = " select count(*) as 收訂人數 from tr20 ";
    //    strsql += " where (reserve = 1 )  ";
    //    strsql += " and grop_numb ='" + num + "'";
    //    strsql += " and isnull(del_data,'') = ''";
    //    strsql += " and isnull(group_change,'') =''";
    //    strsql += " and ar_check='1' ";

    //    SqlCommand command = new SqlCommand(strsql, connect);
    //    SqlDataReader reader = command.ExecuteReader();
    //    if (reader.Read())
    //    {
    //        i = Convert.ToInt32(reader["收訂人數"].ToString());
    //    }
    //    reader.Close();
    //    command.Dispose();
    //    connect.Close();
    //    return i;
    //}
    #endregion

    #region " === 設定變數資料 === "
    protected string _Number = string.Empty; // 系統編號
    protected string _Cust_Numb = string.Empty;
    protected string _NetCustNumb = string.Empty;
    protected string _Enli_Code = string.Empty;
    protected string _EnliI_Date = string.Empty;

    protected string _Tour_Numb = string.Empty;
    protected string _Comp_Code = string.Empty;
    protected string _Comp_Conn = string.Empty;
    protected string _Conn_Idno = string.Empty;
    protected string _ConnTel = string.Empty;
    protected string _ConnFax = string.Empty;

    protected string _BookPax = string.Empty;
    protected string _TourFee = string.Empty;
    protected string _Remark = string.Empty;
    protected string _Remark2 = string.Empty;
    protected string _crea_date = string.Empty;

    protected string _crea_user = string.Empty;
    protected string _loginIP = string.Empty;
    protected string _Examine = string.Empty;
    protected string _Examine_date = string.Empty;
    protected string _Group_Change = string.Empty;

    protected string _del_data = string.Empty;
    //protected string _sale_code = string.Empty;
    protected string _AGT_TR10_Check = string.Empty;
    //protected string _Reg_Status = string.Empty;

    protected string _K_T1 = string.Empty;
    protected string _K_T2 = string.Empty;
    //protected string _Remark = string.Empty;
    //protected string _Remark2 = string.Empty;
    protected string _Sale_Code = string.Empty;
    protected string _OP_CODE = string.Empty;
    protected string _CONT_CELL = string.Empty;
    protected string _CONT_MAIL = string.Empty;
    protected string _Cust_Numb_Target = string.Empty; //247的系統編號
    protected string _Sales_Area = string.Empty;//業務分區
    protected string _EXPECT = "0";
    protected string _Keep_Amount = "0";
    protected string _Tour_Kind = "0";
    protected string _PAK_Amount = "0";
    protected string _TR10_OL_Reg_Status = "HK"; //要寫入tr10_ol用的，判斷是否為後補(HK:有機位且OK的、RQ:候補)

    protected string _LineCode = string.Empty;
    protected string _LineCode2 = string.Empty;
    protected string _Grop_Name = string.Empty;
    //20190802
    protected int _VAN_HK = 0;
    protected int _VAN_RQ = 0;

    protected DataTable _dtTr20_OL = new DataTable(); //讀取資料-線上報名人數
    protected DateTime _dtTr20CreaDate = DateTime.Today;
    //20191227
    protected string _dept1 = string.Empty;
    protected string _dept2 = string.Empty;
    protected string _dept3 = string.Empty;
    protected string _LineCode3 = string.Empty;
    #endregion

    #region " === 寫入資料庫 === "
    /// <summary>
    /// 抓取訂單相關資料
    /// </summary>
    protected void fn_ReadData(string strNetCust_Numb)
    {
        string strsql = "";
        strsql += " SELECT [Number],[Cust_Numb],[netcustnumb],[Enli_Code],[EnliI_Date]";
        strsql += " ,[Tour_Numb],[Comp_Code],[Comp_Conn],[ConnTel],[ConnFax]";
        strsql += " ,[BookPax],[TourFee],[Remark],[Remark2],[crea_date]";
        strsql += " ,[Crea_User],[loginIP],[Examine],[Examine_date],[Group_Change]";
        strsql += " ,[del_data],[sale_code],[AGT_TR10_Check],[Reg_Status],[K_T1]";
        strsql += " ,[K_T2],CONT_CELL,CONT_MAIL";
        strsql += " FROM [TR10_OL]";
        strsql += " WHERE [netcustnumb] = @netcustnumb";

        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(constring);
        connect.Open();
        SqlCommand cmd = new SqlCommand(strsql, connect);
        cmd.Parameters.Add(new SqlParameter("@NetCustNumb", strNetCust_Numb));
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            _Sale_Code = fn_RtnSaleCode(reader["Crea_User"].ToString());
            _Sales_Area = fn_RtnSales_Area(_Sale_Code);
            _OP_CODE = fn_RtnOP_CODE(reader["Tour_Numb"].ToString());

            _Number = reader["Number"].ToString();
            _Cust_Numb = reader["Cust_Numb"].ToString();
            _NetCustNumb = reader["netcustnumb"].ToString();
            _Enli_Code = reader["Enli_Code"].ToString();
            _EnliI_Date = reader["EnliI_Date"].ToString();

            _Tour_Numb = reader["Tour_Numb"].ToString();
            _Comp_Code = reader["Comp_Code"].ToString();
            //_Comp_Conn = reader["Comp_Conn"].ToString();
            _Conn_Idno = reader["Crea_User"].ToString();
            //_ConnTel = reader["ConnTel"].ToString();
            //_ConnFax = reader["ConnFax"].ToString();

            _BookPax = reader["BookPax"].ToString();
            _TourFee = reader["TourFee"].ToString();
            //_Remark = reader["Remark"].ToString();
            //_Remark2 = reader["Remark2"].ToString();
            _crea_date = reader["crea_date"].ToString();

            _loginIP = reader["loginIP"].ToString();
            _Examine = reader["Examine"].ToString();
            _Examine_date = reader["Examine_date"].ToString();
            _Group_Change = reader["Group_Change"].ToString();

            _del_data = reader["del_data"].ToString();
            _AGT_TR10_Check = reader["AGT_TR10_Check"].ToString();

            _K_T1 = reader["K_T1"].ToString();
            _K_T2 = reader["K_T2"].ToString();
            _Remark = reader["Remark"].ToString();
            _Remark2 = reader["Remark2"].ToString();
            //_CONT_CELL = reader["CONT_CELL"].ToString();
            //_CONT_MAIL = reader["CONT_MAIL"].ToString();
        }
        reader.Close();
        cmd.Dispose();
        connect.Close();
    }
    // 抓取會員相關資料
    protected void fn_ReadData_Agent_L(string strCrea_User)
    {
        string strsql = "";
        strsql += " SELECT AGT_NAME1,AGT_CONT,CONT_ZONE,CONT_TEL,CFAX_ZONE,CONT_FAX,CONT_CELL,CONT_MAIL,AGT_IDNo";
        strsql += " FROM [AGENT_L]";
        strsql += " WHERE [AGT_IDNo] = @AGT_IDNo";

        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["GLBConnectionString_247"].ToString();
        SqlConnection connect = new SqlConnection(constring);
        connect.Open();
        SqlCommand cmd = new SqlCommand(strsql, connect);
        cmd.Parameters.Add(new SqlParameter("@AGT_IDNo", strCrea_User));
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            _Comp_Conn = reader["AGT_CONT"].ToString(); //同業聯絡人
            //_Conn_Idno = reader["AGT_IDNo"].ToString(); 
            _ConnTel = reader["CONT_ZONE"].ToString(); //市內電話
            _ConnTel += (string.IsNullOrEmpty(_ConnTel) ? "" : "-") + reader["CONT_TEL"].ToString();
            _ConnFax = reader["CFAX_ZONE"].ToString(); //傳真
            
            _ConnFax += (string.IsNullOrEmpty(_ConnTel) ? "" : "-") + reader["CONT_FAX"].ToString();
            _CONT_CELL = reader["CONT_CELL"].ToString(); //手機
            _CONT_MAIL = reader["CONT_MAIL"].ToString(); //email
        }
        reader.Close();
        cmd.Dispose();
        connect.Close();
    }
    /// <summary>
    /// 回傳 Sale Code
    /// </summary>
    /// <param name="strIND_FG_ID"></param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string fn_RtnSaleCode(string strIND_FG_ID)
    {
        string strPerNO = "";
        string strSql = "";
        strSql += " SELECT AGENT_M.AGT_NAME1,AGENT_L.AGT_IDNo,AGENT_L.SALE_CODE,Person.name AS SALE_NAME";
        strSql += " FROM AGENT_L";
        strSql += " LEFT JOIN AGENT_M ON AGENT_M.AGT_NAME1 = AGENT_L.AGT_NAME1";
        strSql += " LEFT JOIN Person ON Person.perno = AGENT_L.SALE_CODE";
        strSql += " WHERE AGENT_L.AGT_IDNo = @AGT_IDNo";

        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["GLBConnectionString_247"].ToString();
        SqlConnection connect = new SqlConnection(constring);
        try
        {
            connect.Open();
            SqlCommand command = new SqlCommand(strSql, connect);
            command.Parameters.Add(new SqlParameter("@AGT_IDNo", strIND_FG_ID));
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                strPerNO = reader["SALE_CODE"].ToString();
                _crea_user = reader["SALE_NAME"].ToString();
            }
            reader.Close();
            command.Dispose();
        }
        finally
        {
            connect.Close();
        }

        // 若沒資料的話，就抓卡哥的資料
        if (string.IsNullOrEmpty(strPerNO))
        {
            strPerNO = "A00301";
            _crea_user = "張仲能";
        }

        return strPerNO;
    }
    private string fn_RtnSales_Area(string strPerNo)
    {
        string strSales_Area = "";
        string strSql = "";
        strSql += " SELECT Sales_Area,dept1,dept2,dept3 FROM [Person]";
        strSql += " WHERE PerNo = @PerNo";

        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["GLBConnectionString_247"].ToString();
        SqlConnection connect = new SqlConnection(constring);
        try
        {
            connect.Open();
            SqlCommand command = new SqlCommand(strSql, connect);
            command.Parameters.Add(new SqlParameter("@PerNo", strPerNo));
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                strSales_Area = reader["Sales_Area"].ToString();
                //20191227
                _dept1 = reader["dept1"].ToString();
                _dept2 = reader["dept2"].ToString();
                _dept3 = reader["dept3"].ToString();
            }
            reader.Close();
            command.Dispose();
        }
        finally
        {
            connect.Close();
        }

        return strSales_Area;
    }
    private string fn_RtnOP_CODE(string strNUM)
    {
        string strOP_CODE = "";
        string strSql = "";
        strSql += " SELECT OP_CODE,isnull(EXPECT,0) as EXPECT,isnull(Keep_Amount,0) as Keep_Amount,isnull(Tour_Kind,0) as Tour_Kind,isnull(PAK_Amount,0) as PAK_Amount";
        strSql += " FROM TOUR";
        strSql += " WHERE NUM = @NUM";

        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["GLBConnectionString_247"].ToString();
        SqlConnection connect = new SqlConnection(constring);
        try
        {
            connect.Open();
            SqlCommand command = new SqlCommand(strSql, connect);
            command.Parameters.Add(new SqlParameter("@NUM", strNUM));
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                strOP_CODE = reader["OP_CODE"].ToString();

                _EXPECT = reader["EXPECT"].ToString();
                _Keep_Amount = reader["Keep_Amount"].ToString();
                _Tour_Kind = reader["Tour_Kind"].ToString();
                _PAK_Amount = reader["PAK_Amount"].ToString();
            }
            reader.Close();
            command.Dispose();
        }
        finally
        {
            connect.Close();
        }

        return strOP_CODE;
    }
    /// <summary>
    /// 重新取得一次編號，以免重覆
    /// </summary>
    /// <param name="strNum"></param>
    protected void fn_InsertTR10(string strNum, string code)
    {
        string TargetCustNumb = "";
        TargetCustNumb = _Cust_Numb_Target;
        _Cust_Numb_Target = "";
        // ****************************************************************************************************
        // 新增時若遇到假日，需往後延至第一個上班日開始算
        while (!string.IsNullOrEmpty(fn_RtnIsHoliday(_dtTr20CreaDate)))
        {
            _dtTr20CreaDate = _dtTr20CreaDate.AddDays(1);
        }

        string strSql = "";
        //string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRANConnectionString_247"].ToString();
        getConstring(code);
        string constring = TRANConnectionString;

        SqlConnection connect = new SqlConnection(constring);

        using (TransactionScope scope = new TransactionScope())
        {
            string strRemark = "";
            strRemark += (string.IsNullOrEmpty(strRemark) ? "" : " ") + _Remark;
            strRemark += (string.IsNullOrEmpty(strRemark) ? "" : " ") + _Remark2;
            if (_K_T1 != "0")
            { strRemark += (string.IsNullOrEmpty(strRemark) ? "" : " ") + "高北單程" + _K_T1 + "人"; }
            if (_K_T2 != "0")
            { strRemark += (string.IsNullOrEmpty(strRemark) ? "" : " ") + "高北來回" + _K_T2 + "人"; }

            // ****************************************************************************************************
            connect.Open();
            int intReadCount = 0; //判斷讀取5次，若5次都發生錯誤就會跳出
            while (_Cust_Numb_Target == "")
            {
                intReadCount += 1;

                strSql = " DECLARE @numberdate nvarchar(6);";
                strSql += " DECLARE @Number nvarchar(12);";
                strSql += " select @numberdate =";
                strSql += " cast(year(getdate()) as nvarchar) + (case when len(cast(month(getdate()) as nvarchar)) = 1 then '0'+ cast(month(getdate()) as nvarchar) else cast(month(getdate()) as nvarchar) end)";
                strSql += " SELECT @Number =";
                strSql += " 'G'+ @numberdate";
                strSql += " + REPLICATE('0', 5 - len(CAST(isnull(max(CAST(right(CUST_NUMB,5) AS float)),0)+ 1 as nvarchar)))";
                strSql += " + CAST(isnull(max(cast(right(CUST_NUMB,5) as float)),0)+1 as nvarchar)";
                strSql += " FROM TR10";
                strSql += " WHERE left(CUST_NUMB,7) = 'G' + @numberdate";

                strSql += " INSERT INTO TR10 (";
                strSql += " Cust_Numb, netcustnumb, EnliI_Date, Tour_Numb, Enli_Code ";
                strSql += " , Comp_Code, Comp_Conn, Conn_Idno, ConnTel, ConnFax, Sale_Code";
                strSql += " , BookPax, TourFee, DocuPax, Remark, DepositPax";
                strSql += " , DepositEach, Deposit, Title_flag, Ticket, crea_date";
                strSql += " , crea_user, loginname, Charge_Examine, B2B_Verify, OP_CODE";
                strSql += " , CONT_CELL, CONT_MAIL";
                if (code != "") { strSql += " ,Source_Agent_No,Source_TR10_Cust_Numb"; }
                //20191227
                strSql += " ,dept1,dept2,dept3";
                strSql += " ) VALUES (";
                strSql += " @Number, @netcustnumb, @EnliI_Date, @Tour_Numb, @Enli_Code ";
                strSql += " , @Comp_Code, @Comp_Conn, @Conn_Idno, @ConnTel, @ConnFax, @Sale_Code";
                strSql += " , @BookPax, @TourFee, @DocuPax, @Remark, @DepositPax";
                strSql += " , @DepositEach, @Deposit, @Title_flag, @Ticket, @crea_date";
                strSql += " , @crea_user, @loginname, @Charge_Examine, @B2B_Verify, @OP_CODE";
                strSql += " , @CONT_CELL, @CONT_MAIL";
                if (code != "") { strSql += " ,'A','" + TargetCustNumb + "'"; }
                //20191227
                strSql += " ,@dept1,@dept2,@dept3";
                strSql += " )";

                //strSql += " update TR10 set";
                //strSql += " EnliI_Date=null";
                //strSql += " where EnliI_Date='1900/9/9'";

                strSql += " select @Number as Number";


                SqlCommand cmd = new SqlCommand(strSql, connect);
                //20191227
                cmd.Parameters.Add(new SqlParameter("@dept1", _dept1));
                cmd.Parameters.Add(new SqlParameter("@dept2", _dept2));
                cmd.Parameters.Add(new SqlParameter("@dept3", _dept3));

                cmd.Parameters.Add(new SqlParameter("@netcustnumb", _NetCustNumb));
                cmd.Parameters.Add(new SqlParameter("@EnliI_Date", (MyFunction.Check.IsDate(_EnliI_Date) ? (Object)Convert.ToDateTime(_EnliI_Date).ToString("yyyy/MM/dd HH:mm:ss") : DBNull.Value)));
                //cmd.Parameters.Add(new SqlParameter("@EnliI_Date", DateTime.Today));
                cmd.Parameters.Add(new SqlParameter("@Tour_Numb", _Tour_Numb));
                if (code == "")
                {
                    cmd.Parameters.Add(new SqlParameter("@Enli_Code ", _Enli_Code));
                    cmd.Parameters.Add(new SqlParameter("@Comp_Code", _Comp_Code));
                    cmd.Parameters.Add(new SqlParameter("@Remark", strRemark));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@Enli_Code ", "2"));
                    cmd.Parameters.Add(new SqlParameter("@Comp_Code", _Comp_Code));
                    cmd.Parameters.Add(new SqlParameter("@Remark", ""));
                }

                cmd.Parameters.Add(new SqlParameter("@Comp_Conn", _Comp_Conn));
                cmd.Parameters.Add(new SqlParameter("@Conn_Idno", _Conn_Idno));
                cmd.Parameters.Add(new SqlParameter("@ConnTel", _ConnTel));
                cmd.Parameters.Add(new SqlParameter("@ConnFax", _ConnFax));
                cmd.Parameters.Add(new SqlParameter("@Sale_Code", _Sale_Code));

                cmd.Parameters.Add(new SqlParameter("@BookPax", _BookPax));
                cmd.Parameters.Add(new SqlParameter("@TourFee", _TourFee));
                cmd.Parameters.Add(new SqlParameter("@DocuPax", "0"));
                //cmd.Parameters.Add(new SqlParameter("@Remark", strRemark));
                cmd.Parameters.Add(new SqlParameter("@DepositPax", "0"));

                cmd.Parameters.Add(new SqlParameter("@DepositEach", "0"));
                cmd.Parameters.Add(new SqlParameter("@Deposit", "0"));
                cmd.Parameters.Add(new SqlParameter("@Title_flag", false));
                cmd.Parameters.Add(new SqlParameter("@Ticket", "1"));
                cmd.Parameters.Add(new SqlParameter("@crea_date", DateTime.Now));

                cmd.Parameters.Add(new SqlParameter("@crea_user", _crea_user));
                cmd.Parameters.Add(new SqlParameter("@loginname", "127.0.0.1"));
                cmd.Parameters.Add(new SqlParameter("@Charge_Examine", "2"));
                cmd.Parameters.Add(new SqlParameter("@B2B_Verify", "Y"));
                cmd.Parameters.Add(new SqlParameter("@OP_CODE", _OP_CODE));

                cmd.Parameters.Add(new SqlParameter("@CONT_CELL", _CONT_CELL));
                cmd.Parameters.Add(new SqlParameter("@CONT_MAIL", _CONT_MAIL));

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    _Cust_Numb_Target = reader["Number"].ToString();
                }
                reader.Close();
                cmd.Dispose();

                // 判斷執行5次都還取不到編號，就跳出錯誤
                if (intReadCount == 5 && String.IsNullOrEmpty(_Cust_Numb_Target))
                {
                    _Cust_Numb_Target = "ERROR";
                }
            }
            connect.Close();


             // 判斷，只要主檔不是發生錯誤的，那就同步新增到其他地方
            if (_Cust_Numb_Target != "ERROR")
            {
                connect.Open();
                //// ****************************************************************************************************
                //// 更新tour的人數
                //// ****************************************************************************************************
                //double BookPax = 0; //報名人數
                //double DocuPax = 0; //收件人數
                //double DepositPax = 0; //收訂人數
                //strSql = " SELECT SUM(isnull([BookPax],0)) as BookPax,SUM(isnull([DocuPax],0)) as DocuPax,SUM(isnull([DepositPax],0)) as DepositPax,[TOUR_NUMB]";
                //strSql += " FROM [TR10]";
                //strSql += " WHERE ([TOUR_NUMB]=@TOUR_NUMB)";
                //strSql += " GROUP BY [TOUR_NUMB]";
                //SqlCommand command2 = new SqlCommand(strSql, connect);
                //command2.Parameters.Add(new SqlParameter("@TOUR_NUMB", strNum));
                //SqlDataReader reader2 = command2.ExecuteReader();
                //if (reader2.Read())
                //{
                //    BookPax = Convert.ToDouble(reader2["BookPax"].ToString());
                //    DocuPax = Convert.ToDouble(reader2["DocuPax"].ToString());
                //    DepositPax = Convert.ToDouble(reader2["DepositPax"].ToString());
                //}
                //reader2.Close();
                //command2.Dispose();


                //fn_UpdateTour(BookPax, DocuPax, DepositPax, strNum);


                // ****************************************************************************************************
                // 更新tour的人數
                // ****************************************************************************************************
                string strNumber = "";
                strSql = "SELECT [Number] FROM [TR10] where [Cust_numb]=@Cust_numb";
                SqlCommand command3 = new SqlCommand(strSql, connect);
                command3.Parameters.Add(new SqlParameter("@Cust_numb", _Cust_Numb_Target));
                SqlDataReader reader3 = command3.ExecuteReader();
                if (reader3.Read())
                {
                    strNumber = reader3["Number"].ToString();
                }
                command3.Dispose();
                reader3.Close();
                connect.Close();

                // ****************************************************************************************************
                //新增tr20明細  電腦代號,報名單號
                subedit_data(strNumber, _Cust_Numb_Target, code);

                //20190131
                if (code != "")
                {
                    try
                    {
                        string strMessage = "";
                        strMessage = _Tour_Numb + "/";
                        strMessage += _Comp_Code + "/" + _Comp_Conn + "//";
                        strMessage += _Grop_Name + "/";
                        strMessage += _BookPax + "人/";
                        strMessage += "共賣報名單";

                        //switch (code)
                        //{
                        //    case "G":
                        //        if (_LineCode != "") { clsFunction.LINE.Is_Send_Line2(_LineCode, strMessage); }
                        //        if (_LineCode2 != "") { clsFunction.LINE.Is_Send_Line2(_LineCode2, strMessage); }
                        //        if (_LineCode3 != "") { clsFunction.LINE.Is_Send_Line2(_LineCode3, strMessage); }
                        //        break;
                        //    case "L":
                        //        if (_LineCode != "") { clsFunction.LINE.Is_Send_Line(_LineCode, strMessage); }
                        //        if (_LineCode2 != "") { clsFunction.LINE.Is_Send_Line(_LineCode2, strMessage); }
                        //        if (_LineCode3 != "") { clsFunction.LINE.Is_Send_Line(_LineCode3, strMessage); }
                        //        break;
                        //    default:
                        //        if (_LineCode != "") { clsFunction.LINE.Is_Send_Line(_LineCode, strMessage); }
                        //        if (_LineCode2 != "") { clsFunction.LINE.Is_Send_Line(_LineCode2, strMessage); }
                        //        if (_LineCode3 != "") { clsFunction.LINE.Is_Send_Line(_LineCode3, strMessage); }
                        //        break;
                        //}
                    }
                    catch { }
                }
            }

            // ****************************************************************************************************
            // 設定交易完成
            scope.Complete();
        }

        // ****************************************************************************************************
        // 更新tour的人數
        // ****************************************************************************************************
        double BookPax = 0; //報名人數
        double DocuPax = 0; //收件人數
        double DepositPax = 0; //收訂人數

        try
        {
            strSql = " SELECT SUM(isnull([BookPax],0)) as BookPax,SUM(isnull([DocuPax],0)) as DocuPax,SUM(isnull([DepositPax],0)) as DepositPax,[TOUR_NUMB]";
            strSql += " FROM [TR10]";
            strSql += " WHERE ([TOUR_NUMB]=@TOUR_NUMB)";
            strSql += " GROUP BY [TOUR_NUMB]";
            connect.Open();
            SqlCommand command2 = new SqlCommand(strSql, connect);
            command2.Parameters.Add(new SqlParameter("@TOUR_NUMB", strNum));
            SqlDataReader reader2 = command2.ExecuteReader();
            if (reader2.Read())
            {
                BookPax = Convert.ToDouble(reader2["BookPax"].ToString());
                DocuPax = Convert.ToDouble(reader2["DocuPax"].ToString());
                DepositPax = Convert.ToDouble(reader2["DepositPax"].ToString());
            }
            reader2.Close();
            command2.Dispose();
            connect.Close();

            fn_UpdateTour(BookPax, DocuPax, DepositPax, strNum, code);

            // ****************************************************************************************************
            //更新行程上架報名人數
            // ****************************************************************************************************
            UPdate_Trip_BookPax(code);
        }
        catch { }
        
    }
    protected void fn_UpdateTour(double BookPax, double DocuPax, double DepositPax, string strNum, string code)
    {
        string strSql = "";
        strSql += " UPDATE TOUR SET";
        strSql += " BookPAX = @BookPAX";
        strSql += " , DocuPAX = @DocuPAX";
        strSql += " , DepositPAX = @DepositPAX";
        strSql += " , [Open] = @Open";
        strSql += " WHERE (NUM = @NUM)";
        //string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["GLBConnectionString_247"].ToString();
        getConstring(code);
        string constring = GLBConnectionString;

        SqlConnection connect = new SqlConnection(constring);
        connect.Open();
        SqlCommand command2 = new SqlCommand(strSql, connect);
        command2.Parameters.Add(new SqlParameter("@BookPAX", BookPax));
        command2.Parameters.Add(new SqlParameter("@DocuPAX", DocuPax));
        command2.Parameters.Add(new SqlParameter("@DepositPAX", DepositPax));
        command2.Parameters.Add(new SqlParameter("@Open", BookPax));
        command2.Parameters.Add(new SqlParameter("@NUM", strNum));
        command2.ExecuteNonQuery();
        command2.Dispose();
        connect.Close();
    }
    /// <summary>
    /// 新增tr20明細  電腦代號,報名單號
    /// </summary>
    private void subedit_data(string strNumb, string strGNumb, string code)
    {
        string strsql = "";

        // ****************************************************************************************************
        // 讀取目前團體報名人數
        // ****************************************************************************************************
        //int RecodeNum = Convert.ToInt32(_BookPax); //報名人數
        int RecodeNum = 0; //報名人數
        int intBookPax = 0;
        string strCLOS_PEOP = "";

        strsql = " SELECT count(tr20.cust_numb) as BookPax,CLOS_PEOP from tr20 ";
        strsql += " left join tr10 on tr20.Tr10Number = tr10.Number ";
        strsql += " left join glb.dbo.TOUR tour on tour.NUM = tr20.GROP_NUMB";
        strsql += " where tr20.grop_numb = @Grop_Numb";
        strsql += " and Charge_Examine = 2 ";
        strsql += " and (ISNULL(tr20.Group_Change, '') = '') ";
        strsql += " and (ISNULL(tr20.del_data, '') = '') ";
        strsql += " and reserve != 0 ";
        strsql += " and reserve != 2 ";
        strsql += " and tr20.tour_type <> '領隊'";
        strsql += " AND tr20.Tour_Type <> 'Join Tour'"; //Join Tour不列入候補 by roger 20150407
        strsql += " AND tr20.Tour_Type <> N'FIT票'";    //FIT票 不列印候補 by roger 20160413
        strsql += " AND tr20.Tick_Type <> 'INF'";       //INF 不計算候補 by roger 20160421 
        strsql += " AND tr20.Tour_Type <> 'Tour Only'"; //20190710
        strsql += " GROUP BY CLOS_PEOP";

        //string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRANConnectionString_247"].ToString();
        getConstring(code);
        string constring = TRANConnectionString;

        SqlConnection connect = new SqlConnection(constring);
        connect.Open();
        SqlCommand cmd = new SqlCommand(strsql, connect);
        cmd.Parameters.Add(new SqlParameter("@Grop_Numb", _Tour_Numb));
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            intBookPax = Convert.ToInt32(reader["BookPax"].ToString());
            strCLOS_PEOP = reader["CLOS_PEOP"].ToString();
        }
        reader.Close();
        cmd.Dispose();

        // ****************************************************************************************************
        // 讀取基本資料
        // ****************************************************************************************************
        //strsql = " SELECT [Tr20Number],[Tr10Number],TR10_OL.[Cust_Numb],TR10_OL.[netcustnumb],[GROP_NUMB],[Enli_Date]";
        //strsql += " ,[TR20_OL].[BookPax],[Tour_Mony],[Tick_Type],[Bed_Type]";
        //strsql += " FROM [TR20_OL]";
        //strsql += " LEFT JOIN TR10_OL ON TR10_OL.Number = [TR20_OL].Tr10Number";
        //strsql += " WHERE TR10_OL.[netcustnumb] = @netcustnumb";
        //string constring_B2B = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        //SqlConnection connect_B2B = new SqlConnection(constring_B2B);
        //connect_B2B.Open();
        //SqlDataAdapter da = new SqlDataAdapter(strsql, constring_B2B);
        //da.SelectCommand.Parameters.Add(new SqlParameter("@netcustnumb", _NetCustNumb));
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        //connect_B2B.Close();

        //DataTable dt = fn_rtnDataTabel_B2B();

        // ****************************************************************************************************
        // 插入資料庫
        // ****************************************************************************************************
        int intIndex = 0;
        for (int ii = 0; ii < _dtTr20_OL.Rows.Count; ii++)
        {
            RecodeNum = Convert.ToInt32(_dtTr20_OL.Rows[ii]["BookPax"].ToString());
            for (int jj = 1; jj <= RecodeNum; jj++)
            {
                intBookPax += 1;
                intIndex += 1;

                strsql = "insert into tr20 (";
                strsql += " Tr10Number,Cust_Numb,Enli_Date,Sequ_No,Cust_Idno";
                strsql += " ,Cust_Name,Bed_Type,EAT,Tick_Type,Tour_Type";
                strsql += " ,Tour_Mony1,Tour_Mony2,Tour_Mony,DepositEach,INFO_FLAG,Conf_Flag,ID";
                strsql += " ,PHOTO,PPT,FPAPER,INK,TICK_NUMB";
                strsql += " ,Remark,Invo_numb,Invo_Date,Invo_Open,Cu10Number";
                strsql += " ,GROP_NUMB,Ar_Mony,SUB_MONY,Add_Mony,Arto_Mony";
                strsql += " ,Subx_Mony,Ap_Mony,Add_Net,Apto_Mony,Apsub_Mony";
                strsql += " ,PDeposit,crea_user,crea_date,loginname,MRSS_Name";
                strsql += " ,E_Mail,Cell_Tel,reserve,company_area,Room_Type";
                strsql += " ,NetCustNumb";
                strsql += " ) values (";
                strsql += " " + strNumb + ",";//電腦編號
                strsql += " N'" + strGNumb + "',";//報名單號
                strsql += " '" + DateTime.Today.ToShortDateString() + "',";//報名日期
                strsql += " N'" + intIndex.ToString("0000") + "',";//序號
                strsql += " '',";//身份證號

                strsql += " '',";//旅客姓名
                //佔床
                if (_dtTr20_OL.Rows[ii]["Bed_Type"].ToString() == "2")
                { strsql += " N'不佔床',"; }
                else if (_dtTr20_OL.Rows[ii]["Bed_Type"].ToString() == "3")
                { strsql += " N'加床',"; }
                else
                { strsql += " N'佔床',"; }

                strsql += " N'一般',";//飲食

                //票別
                if (_dtTr20_OL.Rows[ii]["Tick_Type"].ToString() == "C")
                { strsql += " N'CHD',"; }
                else
                { strsql += " N'大人',"; }

                strsql += " N'全程',";//動態

                if (code == "")
                {
                    strsql += " " + Convert.ToInt32(_dtTr20_OL.Rows[ii]["Tour_Mony"].ToString()) + ",";//預設團費
                    strsql += " 0,";//主管折扣團費
                    strsql += " " + Convert.ToInt32(_dtTr20_OL.Rows[ii]["Tour_Mony"].ToString()) + ",";//團費
                }
                else
                {
                    strsql += " " + _TourFee + ",";//預設團費
                    strsql += " 0,";//主管折扣團費
                    strsql += " " + _TourFee + ",";//團費
                }
                /*
                strsql += " " + Convert.ToInt32(_dtTr20_OL.Rows[ii]["Tour_Mony"].ToString()) + ",";//預設團費
                strsql += " 0,";//主管折扣團費
                strsql += " " + Convert.ToInt32(_dtTr20_OL.Rows[ii]["Tour_Mony"].ToString()) + ",";//團費
                */
                strsql += " 0,";//訂金收入
                strsql += " '',";//進度
                strsql += " 3,";//訂單確認等級
                strsql += " 0,";//ID

                strsql += " 0,";//照片
                strsql += " 0,";//護照
                strsql += " 0,";//戶口
                strsql += " 0,";//印章
                strsql += " '',";//機票號碼

                strsql += " '',";//備註
                strsql += " '',";//代轉號碼
                strsql += " NULL,";//開立日期
                strsql += " '',";//開立人
                strsql += " '',";//Cu10Number

                strsql += " N'" + _Tour_Numb + "',";//團號
                strsql += " 0,"; //金額要預設為0
                strsql += " 0,";
                strsql += " 0,";
                strsql += " 0,";

                strsql += " 0,";
                strsql += " 0,";
                strsql += " 0,";
                strsql += " 0,";
                strsql += " 0,";

                strsql += " 0,";
                strsql += " N'" + _crea_user + "',";//建立者
                //strsql += " N'" + DateTime.Today.ToShortDateString() + "',";//建立時間
                strsql += " N'" + _dtTr20CreaDate.ToShortDateString() + "',";//建立時間
                strsql += " N'" + GetIpAddress() + "',";//ip
                strsql += " N'MR.',";//稱呼

                strsql += " '',";//email
                strsql += " '',";//手機

                //判斷是否為候補 (機位-保留-pak-已收訂-1)
                if ((Convert.ToInt32(intBookPax) <= Convert.ToInt32(_EXPECT) - Convert.ToInt32(_Keep_Amount) - Convert.ToInt32(_PAK_Amount) - 1) && String.IsNullOrEmpty(strCLOS_PEOP))
                {
                    strsql += " 1,";//可收訂
                    _VAN_HK += 1;   //20190802
                }
                else
                {
                    _TR10_OL_Reg_Status = "RQ"; //判斷候補(若有一人為後補，前台就顯示為候補)
                    strsql += " 0,";//候補w
                    _VAN_RQ += 1;   //20190802
                }

                strsql += " N'" + _Sales_Area + "'";//業務區域
                strsql += " ,'二小床'"; //床型
                strsql += " ,N'" + _NetCustNumb + "'"; //床型
                strsql += " )";

                SqlCommand commadd = new SqlCommand(strsql, connect);
                commadd.ExecuteNonQuery();
                commadd.Dispose();
            }
        }

        connect.Close();
    }
    /// <summary>
    /// 讀取報名人數資料
    /// </summary>
    /// <returns></returns>
    protected DataTable fn_rtnDataTabel_B2B()
    {
        string strsql = "";
        strsql = " SELECT [Tr20Number],[Tr10Number],TR10_OL.[Cust_Numb],TR10_OL.[netcustnumb],[GROP_NUMB],[Enli_Date]";
        strsql += " ,[TR20_OL].[BookPax],[Tour_Mony],[Tick_Type],[Bed_Type]";
        strsql += " FROM [TR20_OL]";
        strsql += " LEFT JOIN TR10_OL ON TR10_OL.Number = [TR20_OL].Tr10Number";
        strsql += " WHERE TR10_OL.[netcustnumb] = @netcustnumb";
        string constring_B2B = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect_B2B = new SqlConnection(constring_B2B);
        connect_B2B.Open();
        SqlDataAdapter da = new SqlDataAdapter(strsql, constring_B2B);
        da.SelectCommand.Parameters.Add(new SqlParameter("@netcustnumb", _NetCustNumb));
        //DataTable dt = new DataTable();
        da.Fill(_dtTr20_OL);
        connect_B2B.Close();

        return _dtTr20_OL;
    }
    /// <summary>
    /// 更新行程上架報名人數
    /// </summary>
    protected void UPdate_Trip_BookPax(string code)
    {
        double BookPax = 0;

        //報名人數
        string strSql = "";
        strSql += " SELECT sum(isnull([BookPax],0)) as BookPax,[TOUR_NUMB]";
        strSql += " FROM [TR10]";
        strSql += " WHERE ([TOUR_NUMB]=@TOUR_NUMB)";
        strSql += " AND Charge_Examine = 2";
        strSql += " group by [TOUR_NUMB]";
        //string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRANConnectionString_247"].ToString();
        getConstring(code);
        string constring = TRANConnectionString;
        SqlConnection connect = new SqlConnection(constring);
        connect.Open();
        SqlCommand command2 = new SqlCommand(strSql, connect);
        command2.Parameters.Add(new SqlParameter("@TOUR_NUMB", _Tour_Numb));
        SqlDataReader reader2 = command2.ExecuteReader();
        if (reader2.Read())
        {
            BookPax = Convert.ToDouble(reader2["BookPax"].ToString());
        }
        reader2.Close();
        command2.Dispose();
        connect.Close();


        strSql = " Update Grop set ";
        strSql += " Reg_Ok = @Reg_Ok";
        strSql += " where Grop_Numb = @Grop_Numb";
        //string conTRIP = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TripConnectionString"].ToString();
        string conTRIP = TRIPConnectionString;
        SqlConnection connTRIP = new SqlConnection(conTRIP);
        connTRIP.Open();
        SqlCommand cmdTRIP = new SqlCommand(strSql, connTRIP);
        cmdTRIP.Parameters.Add(new SqlParameter("@Reg_Ok", BookPax));
        cmdTRIP.Parameters.Add(new SqlParameter("@Grop_Numb", _Tour_Numb));
        cmdTRIP.ExecuteNonQuery();
        cmdTRIP.Dispose();
        connTRIP.Close();
    }
    /// <summary>
    /// 更新tr10程式
    /// </summary>
    protected void fn_Update_Tr10_OL()
    {
        string strsql = "";
        strsql += " UPDATE [TR10_OL] SET";
        strsql += " [Reg_Status] = @Reg_Status";
        strsql += " ,[VAN_HK] = @VAN_HK ,[VAN_RQ] = @VAN_RQ";
        strsql += " WHERE [netcustnumb] = @netcustnumb";
        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(constring);
        try
        {
            connect.Open();
            SqlCommand cmd = new SqlCommand(strsql, connect);
            cmd.Parameters.Add(new SqlParameter("@Reg_Status", _TR10_OL_Reg_Status));
            cmd.Parameters.Add(new SqlParameter("@VAN_HK", _VAN_HK.ToString()));
            cmd.Parameters.Add(new SqlParameter("@VAN_RQ", _VAN_RQ.ToString()));
            cmd.Parameters.Add(new SqlParameter("@NetCustNumb", _NetCustNumb));
            cmd.ExecuteReader();
            cmd.Dispose();
        }
        catch { }
        finally { connect.Close(); }
        
    }
    #endregion

    protected string fn_RtnIsHoliday(DateTime dtTemp)
    {
        string strHoliday = "";

        string strSql = "";
        strSql += " SELECT [Holiday] FROM [Holiday]";
        strSql += " WHERE Holiday = @Holiday";
        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TripConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(constring);

        try
        {
            connect.Open();
            SqlCommand command = new SqlCommand(strSql, connect);
            command.Parameters.Add(new SqlParameter("@Holiday", dtTemp));
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                strHoliday = DateTime.Parse(reader["Holiday"].ToString()).ToString("yyyy/MM/dd");
            }
            reader.Close();
            command.Dispose();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            connect.Close();
        }

        return strHoliday;
    }

    public string GetIpAddress()
    {
        string strIpAddr = string.Empty;

        if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null || Request.ServerVariables["HTTP_X_FORWARDED_FOR"].IndexOf("unknown") > 0)
        {
            strIpAddr = Request.ServerVariables["REMOTE_ADDR"];
        }
        else if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"].IndexOf(",") > 0)
        {
            strIpAddr = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Substring(1, Request.ServerVariables["HTTP_X_FORWARDED_FOR"].IndexOf(",") - 1);
        }
        else if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"].IndexOf(";") > 0)
        {
            strIpAddr = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Substring(1, Request.ServerVariables["HTTP_X_FORWARDED_FOR"].IndexOf(";") - 1);
        }
        else
        {
            strIpAddr = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        }
        return strIpAddr; ;
    }

    #region "=== P報P收即時更新 ==="
    protected string checkSourceCode()
    {
        string code = "";
        string strSql = "";
        strSql += " SELECT Source_Agent_No FROM Grop";
        strSql += " WHERE Grop_numb = @Grop_numb";
        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TripConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(constring);

        try
        {
            connect.Open();
            SqlCommand command = new SqlCommand(strSql, connect);
            command.Parameters.Add(new SqlParameter("@Grop_numb", _Tour_Numb));
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                code = reader["Source_Agent_No"].ToString();
                if (code == "A") { code = ""; }
            }
            reader.Close();
            command.Dispose();
        }
        catch (Exception ex) { }
        finally { connect.Close(); }

        return code;
    }

    private void getConstring(string code)
    {
        switch (code)
        {
            case "A":
                GLBConnectionString = "Data Source=192.168.1.247;Initial Catalog=GLB;Persist Security Info=True;User ID=sa;Password=joetime";
                TRANConnectionString = "Data Source=192.168.1.247;Initial Catalog=TRAN;Persist Security Info=True;User ID=sa;Password=joetime";
                TRIPConnectionString = "Data Source=210.200.219.246;Initial Catalog=Trip;Persist Security Info=True;User ID=sa;Password=joetime";
                break;
            case "G":
                GLBConnectionString = "Data Source=192.168.6.247;Initial Catalog=GLB;Persist Security Info=True;User ID=sa;Password=joetime";
                TRANConnectionString = "Data Source=192.168.6.247;Initial Catalog=TRAN;Persist Security Info=True;User ID=sa;Password=joetime";
                TRIPConnectionString = "Data Source=210.200.219.246;Initial Catalog=Giant_Trip;Persist Security Info=True;User ID=sa;Password=joetime";
                break;
            case "L":
                GLBConnectionString = "Data Source=192.168.1.237;Initial Catalog=GLB;Persist Security Info=True;User ID=sa;Password=joetime";
                TRANConnectionString = "Data Source=192.168.1.237;Initial Catalog=TRAN;Persist Security Info=True;User ID=sa;Password=joetime";
                TRIPConnectionString = "Data Source=210.200.219.242,1902;Initial Catalog=Luxe2017;Persist Security Info=True;User ID=sa;Password=luxe2511";
                break;
            case "C":
                GLBConnectionString = "Data Source=192.168.11.247;Initial Catalog=GLB;Persist Security Info=True;User ID=sa;Password=joetime";
                TRANConnectionString = "Data Source=192.168.11.247;Initial Catalog=TRAN;Persist Security Info=True;User ID=sa;Password=joetime";
                TRIPConnectionString = "Data Source=210.200.219.242,1902;Initial Catalog=TTourWeb;Persist Security Info=True;User ID=sa;Password=luxe2511";
                break;
            default:
                TRANConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRANConnectionString_247"].ToString();
                GLBConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["GLBConnectionString_247"].ToString();
                TRIPConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TripConnectionString"].ToString();
                break;
        }
    }

    public int Target_Tr20_Count(string strTargetTour_Numb, string check, string code)
    {
        int intReturnValue = 0;

        string strsql = "";
        strsql = " select count(*) as tr10_CNT from tr10";
        strsql += " left join tr20 on tr20.Cust_Numb = tr10.Cust_Numb";
        strsql += " where Tour_Numb = @Tour_Numb";
        strsql += " and isnull(tr10.Del_Data,'') = ''";
        strsql += " and isnull(tr10.Group_Change,'') = ''";
        strsql += " and isnull(tr20.Del_Data,'') = ''";
        strsql += " and isnull(tr20.Group_Change,'') = ''";
        strsql += " and Tour_Type <> 'Join Tour'";
        if (check == "1") { strsql += " and AR_check = 1"; }

        SqlConnection conn1 = new SqlConnection();
        getConstring(code);
        conn1.ConnectionString = TRANConnectionString;
        conn1.Open();
        SqlCommand comm1 = new SqlCommand(strsql, conn1);
        comm1.Parameters.Add(new SqlParameter("@Tour_Numb", strTargetTour_Numb));
        SqlDataReader reader = comm1.ExecuteReader();
        if (reader.Read())
        {
            intReturnValue = Convert.ToInt16(reader["tr10_CNT"].ToString());
        }
        comm1.Dispose();
        conn1.Close();

        return intReturnValue;
    }

    public void Update_Pak(string Source_Grop_Numb, string code)
    {
        string strsql1 = "";
        string pa = "";
        string ps = "";
        int intKeep_Amount = 0;
        int intKeep_Amount2 = 0;
        intKeep_Amount = Target_Tr20_Count(Source_Grop_Numb, "1", code);
        intKeep_Amount2 = Target_Tr20_Count(Source_Grop_Numb, "2", code);

        SqlConnection conn1 = new SqlConnection();
        getConstring(code);
        conn1.ConnectionString = GLBConnectionString;
        try
        {
            conn1.Open();

            strsql1 = " UPDATE Tour SET";
            strsql1 += " Pak_ArCheck_Sync = ISNULL(Keep_Amount,0) + @Pak_ArCheck_Sync";
            strsql1 += " ,Pak_SignUp_Sync = ISNULL(Keep_Amount,0) + @Pak_SignUp_Sync";
            strsql1 += " WHERE Num=@Num";
            strsql1 += " select Pak_ArCheck_Sync,Pak_SignUp_Sync from Tour WHERE Num=@Num";

            SqlCommand comm1 = new SqlCommand(strsql1, conn1);
            comm1.Parameters.Add(new SqlParameter("@Num", Source_Grop_Numb));
            comm1.Parameters.Add(new SqlParameter("@Pak_ArCheck_Sync", intKeep_Amount));
            comm1.Parameters.Add(new SqlParameter("@Pak_SignUp_Sync", intKeep_Amount2));
            SqlDataReader reader = comm1.ExecuteReader();
            if (reader.Read())
            {
                pa = reader["Pak_ArCheck_Sync"].ToString();
                ps = reader["Pak_SignUp_Sync"].ToString();
            }
            reader.Close();
            comm1.Dispose();
        }
        catch (Exception ex) { }
        finally { conn1.Close(); }

        conn1.ConnectionString = TRIPConnectionString;
        try
        {
            conn1.Open();

            strsql1 = " UPDATE Grop SET";
            strsql1 += " Pak_ArCheck_Sync = @Pak_ArCheck_Sync";
            strsql1 += " ,Pak_SignUp_Sync = @Pak_SignUp_Sync";
            strsql1 += " WHERE Grop_Numb = @Grop_Numb";

            SqlCommand comm1 = new SqlCommand(strsql1, conn1);
            comm1.Parameters.Add(new SqlParameter("@Pak_ArCheck_Sync", pa));
            comm1.Parameters.Add(new SqlParameter("@Pak_SignUp_Sync", ps));
            comm1.Parameters.Add(new SqlParameter("@Grop_Numb", Source_Grop_Numb));
            comm1.ExecuteNonQuery();
            comm1.Dispose();
        }
        catch (Exception ex) { }
        finally { conn1.Close(); }

        for (int x = 0; x < scode.Length; x++)
        {
            Update_Pak_Target(Source_Grop_Numb, scode[x], pa, ps);
        }
        /*
        Update_Pak_Target(Source_Grop_Numb, "A", pa, ps);
        Update_Pak_Target(Source_Grop_Numb, "L", pa, ps);
        Update_Pak_Target(Source_Grop_Numb, "G", pa, ps);
        */
    }

    public void Update_Pak_Target(string Source_Grop_Numb, string code, string pa, string ps)
    {
        string strsql1 = "";
        SqlConnection conn1 = new SqlConnection();
        getConstring(code);
        conn1.ConnectionString = GLBConnectionString;
        try
        {

            conn1.Open();

            strsql1 = " UPDATE Tour SET";
            strsql1 += " Pak_ArCheck_Sync = @Pak_ArCheck_Sync";
            strsql1 += " ,Pak_SignUp_Sync = @Pak_SignUp_Sync";
            strsql1 += " WHERE Num = @Num";
            strsql1 += " and Source_Agent_No <> ''";
            SqlCommand comm1 = new SqlCommand(strsql1, conn1);
            comm1.Parameters.Add(new SqlParameter("@Num", Source_Grop_Numb));
            comm1.Parameters.Add(new SqlParameter("@Pak_ArCheck_Sync", pa));
            comm1.Parameters.Add(new SqlParameter("@Pak_SignUp_Sync", ps));
            comm1.ExecuteNonQuery();
            comm1.Dispose();
        }
        catch (Exception ex) { }
        finally { conn1.Close(); }

        conn1.ConnectionString = TRIPConnectionString;
        try
        {
            conn1.Open();

            strsql1 = " UPDATE Grop SET";
            strsql1 += " Pak_ArCheck_Sync = @Pak_ArCheck_Sync";
            strsql1 += " ,Pak_SignUp_Sync = @Pak_SignUp_Sync";
            strsql1 += " WHERE Grop_Numb = @Grop_Numb";
            strsql1 += " and Source_Agent_No  <> ''";
            SqlCommand comm1 = new SqlCommand(strsql1, conn1);
            comm1.Parameters.Add(new SqlParameter("@Pak_ArCheck_Sync", pa));
            comm1.Parameters.Add(new SqlParameter("@Pak_SignUp_Sync", ps));
            comm1.Parameters.Add(new SqlParameter("@Grop_Numb", Source_Grop_Numb));
            comm1.ExecuteNonQuery();
            comm1.Dispose();
        }
        catch (Exception ex) { }
        finally { conn1.Close(); }
    }

    private void getSale(string code)
    {
        string str = "";
        string strsql1 = "";
        string compuid = "";
        _Comp_Code = "凱旋02";

        SqlConnection conn1 = new SqlConnection();
        conn1.ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["GLBConnectionString_247"].ToString();
        try
        {
            conn1.Open();

            strsql1 = " select name,compuid from Person";
            strsql1 += " left join COMPANY on COMPANY.compno = PERSON.compno";
            strsql1 += " where perno = @perno";

            SqlCommand comm1 = new SqlCommand(strsql1, conn1);
            comm1.Parameters.Add(new SqlParameter("@perno", _Sale_Code));
            SqlDataReader reader = comm1.ExecuteReader();
            if (reader.Read())
            {
                str = reader["name"].ToString();
                compuid = reader["compuid"].ToString();
            }
            reader.Close();
            comm1.Dispose();
        }
        catch (Exception ex) { }
        finally { conn1.Close(); }

        
        getConstring(code);
        conn1.ConnectionString = GLBConnectionString;
        try
        {
            conn1.Open();
            SqlCommand comm1;
            SqlDataReader reader;
            if (code != "G")
            {
                strsql1 = " select SALE_CODE from AGENT_L";
                strsql1 += " left join AGENT_M on AGENT_L.AGT_NAME1 = AGENT_M.AGT_NAME1";
                strsql1 += " where AGT_CONT = @AGT_CONT";
                strsql1 += " and COMP_NO = @COMP_NO";

                comm1 = new SqlCommand(strsql1, conn1);
                comm1.Parameters.Add(new SqlParameter("@AGT_CONT", str));
                comm1.Parameters.Add(new SqlParameter("@COMP_NO", compuid));
                reader = comm1.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        str = reader["SALE_CODE"].ToString();
                    }
                    else
                    {
                        str = "";
                    }
                }
                else
                {
                    str = "";
                }
                reader.Close();
                comm1.Dispose();
                System.Threading.Thread.Sleep(1);

                if (str == "")
                {
                    strsql1 = " select perno from Person";
                    strsql1 += " where name = (";
                    strsql1 += " select Sales from AGENT_M";
                    strsql1 += " where COMP_NO = @COMP_NO";
                    strsql1 += " )";

                    comm1 = new SqlCommand(strsql1, conn1);
                    comm1.Parameters.Add(new SqlParameter("@COMP_NO", compuid));
                    reader = comm1.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            _Sale_Code = reader["perno"].ToString();
                        }
                        else
                        {
                            for (int x = 0; x < scode.Length; x++)
                            {
                                if (scode[x] == code) { _Sale_Code = scode2[x]; }
                            }
                            /*
                            switch (code)
                            {
                                case "G":
                                    _Sale_Code = "A00223";
                                    break;
                                case "L":
                                    _Sale_Code = "A00106";
                                    break;
                                default:
                                    _Sale_Code = "A00309";
                                    break;
                            }
                            */
                            compuid = "97440969";
                        }
                    }
                    else
                    {
                        for (int x = 0; x < scode.Length; x++)
                        {
                            if (scode[x] == code) { _Sale_Code = scode2[x]; }
                        }
                        /*
                        switch (code)
                        {
                            case "G":
                                _Sale_Code = "A00223";
                                break;
                            case "L":
                                _Sale_Code = "A00106";
                                break;
                            default:
                                _Sale_Code = "A00309";
                                break;
                        }
                        */
                        compuid = "97440969";
                    }

                    reader.Close();
                    comm1.Dispose();
                    System.Threading.Thread.Sleep(1);
                }
                else { _Sale_Code = str; }
            }
            //20190417, 巨大固定業務 BY 世昌
            if (code == "G") { _Sale_Code = "G11227"; }

            strsql1 = " select TEL1_ZONE,TEL1,FAX_ZONE,FAX,AGT_NAME1 from AGENT_M";
            strsql1 += " where COMP_NO = @COMP_NO";
            comm1 = new SqlCommand(strsql1, conn1);
            comm1.Parameters.Add(new SqlParameter("@COMP_NO", compuid));
            reader = comm1.ExecuteReader();
            if (reader.Read())
            {
                _ConnTel = reader["TEL1_ZONE"].ToString() + reader["TEL1"].ToString();
                _ConnFax = reader["FAX_ZONE"].ToString() + reader["FAX"].ToString();
                _Comp_Code = reader["AGT_NAME1"].ToString();
            }
            reader.Close();
            comm1.Dispose();
            System.Threading.Thread.Sleep(1);

            //20190130
            strsql1 = " SELECT Person.Line_Code, PAgent.Line_Code as Line_Code_Agent, PAgent2.Line_Code as Line_Code_Agent2 FROM Person";
            strsql1 += " left join Person PAgent on PAgent.perno = Person.Agent_Person";
            strsql1 += " left join Person PAgent2 on PAgent2.perno = Person.Agent_Person2";
            strsql1 += " where Person.perno = @perno";
            comm1 = new SqlCommand(strsql1, conn1);
            comm1.Parameters.Add(new SqlParameter("@perno", _Sale_Code));
            reader = comm1.ExecuteReader();
            if (reader.Read())
            {
                _LineCode = reader["Line_Code"].ToString();
                _LineCode2 = reader["Line_Code_Agent"].ToString();
                _LineCode3 = reader["Line_Code_Agent2"].ToString();
            }
            reader.Close();
            comm1.Dispose();
            System.Threading.Thread.Sleep(1);

            //20190213
            strsql1 = " select NAME1,AgentPrice,OP_CODE,Keep_Amount from tour";
            strsql1 += " left join TOUR_PRICE on TOUR.Number = TOUR_PRICE.Number";
            strsql1 += " where num = @num";
            strsql1 += " and Tick_Type = 'A'";
            comm1 = new SqlCommand(strsql1, conn1);
            comm1.Parameters.Add(new SqlParameter("@NUM", _Tour_Numb));
            reader = comm1.ExecuteReader();
            if (reader.Read())
            {
                _Grop_Name = reader["NAME1"].ToString();
                _TourFee = reader["AgentPrice"].ToString();
                _OP_CODE = reader["OP_CODE"].ToString();
                _Keep_Amount = reader["Keep_Amount"].ToString();
            }
            reader.Close();
            comm1.Dispose();
            System.Threading.Thread.Sleep(1);

            //20190612
            strsql1 = " SELECT [Sales_Area],dept1,dept2,dept3 FROM [PERSON] WHERE [perno] = @perno";
            comm1 = new SqlCommand(strsql1, conn1);
            comm1.Parameters.Add(new SqlParameter("@perno", _Sale_Code));
            reader = comm1.ExecuteReader();
            if (reader.Read())
            {
                _Sales_Area = reader["Sales_Area"].ToString();
                //20191227
                _dept1 = reader["dept1"].ToString();
                _dept2 = reader["dept2"].ToString();
                _dept3 = reader["dept3"].ToString();
            }
            reader.Close();
            comm1.Dispose();
            System.Threading.Thread.Sleep(1);
        }
        catch (Exception ex) { }
        finally { conn1.Close(); }


        conn1.ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["GLBConnectionString_247"].ToString();
        try
        {
            conn1.Open();

            strsql1 = " select pager,e_mail from Person";
            strsql1 += " where name = @name";
            SqlCommand comm1 = new SqlCommand(strsql1, conn1);
            comm1.Parameters.Add(new SqlParameter("@name", SaleName));
            SqlDataReader reader = comm1.ExecuteReader();
            if (reader.Read())
            {
                _CONT_CELL = reader["pager"].ToString();
                _CONT_MAIL = reader["e_mail"].ToString();
            }
            reader.Close();
            comm1.Dispose();
            System.Threading.Thread.Sleep(1);
        }
        catch (Exception ex) { }
        finally { conn1.Close(); }
        _Comp_Conn = SaleName;
    }

    private string getSaleName(string code)
    {
        string str = "";
        string strsql = "";
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["GLBConnectionString_247"].ToString();
        try
        {
            conn.Open();

            strsql = " select name from Person";
            strsql += " where perno = @perno";
            SqlCommand comm = new SqlCommand(strsql, conn);
            comm.Parameters.Add(new SqlParameter("@perno", code));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                str = reader["name"].ToString();
            }
            reader.Close();
            comm.Dispose();
        }
        catch { }
        finally { conn.Close(); }

        return str;
    }

    private void updateReserve(string code, string num)
    {
        string strsql = "";
        SqlConnection conn = new SqlConnection();
        DataTable dt = new DataTable();

        getConstring(code);
        conn.ConnectionString = TRANConnectionString;

        try
        {
            conn.Open();
            strsql = " select Source_TR10_Cust_Numb,Sequ_No,Reserve from tr20";
            strsql += " left join tr10 on Number = Tr10Number";
            strsql += " where Reserve in (0,2)";
            strsql += " and Tour_Numb = @Tour_Numb";
            strsql += " and Source_Agent_No = 'A'";

            SqlCommand comm = new SqlCommand(strsql, conn);
            comm.Parameters.Add(new SqlParameter("@Tour_Numb", num));
            SqlDataAdapter da = new SqlDataAdapter();

            da.SelectCommand = comm;
            da.Fill(dt);
        }
        catch { }
        finally { conn.Close(); }

        conn.ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRANConnectionString_247"].ToString();

        try
        {
            conn.Open();

            for (int i = 0; i < dt.Rows.Count - 1; i++)
            {
                strsql = " update tr20 set";
                strsql += " Reserve = @Reserve";
                strsql += " where Cust_Numb = @Cust_Numb";
                strsql += " and Sequ_No = @Sequ_No";

                SqlCommand comm = new SqlCommand(strsql, conn);
                comm.Parameters.Add(new SqlParameter("@Cust_Numb", dt.Rows[i]["Source_TR10_Cust_Numb"].ToString()));
                comm.Parameters.Add(new SqlParameter("@Sequ_No", dt.Rows[i]["Sequ_No"].ToString()));
                comm.Parameters.Add(new SqlParameter("@Reserve", dt.Rows[i]["Reserve"].ToString()));
                comm.ExecuteNonQuery();
                comm.Dispose();
            }
        }
        catch { }
        finally { conn.Close(); }
    }
    #endregion
}
