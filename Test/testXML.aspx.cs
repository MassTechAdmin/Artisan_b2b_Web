using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Test_testXML : System.Web.UI.Page
{
    string ResponseXML = ""; //XML回傳訊息
    protected void Page_Load(object sender, EventArgs e)
    {
        //要傳送的資料
        string strXML = "";
        strXML += "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
        strXML += "<Message>";
        strXML += "<MA>";
        strXML += "<A><![CDATA[artisan988]]></A>";  //驗證碼
        strXML += "<B><![CDATA[74549]]></B>";  //網路報名單號
        strXML += "<C><![CDATA[AAJCX0540717KIX]]></C>";  //團號
        strXML += "</MA>";
        strXML += "</Message>";

        //string strURL = "http://210.71.206.199:502/xml/GetApplyChecked.aspx";
        string strURL = "http://localhost:62750/xml/GetApplyChecked.aspx";
        string strComNum = "";
        fn_Show_XML(SendRequest(strURL, strXML, strComNum));

    }
    #region " --- 傳送XML資料 --- "
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
                //if (ResponseXML == "OK") { Response.Redirect("Apply4.aspx"); }
                //else { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('報名失敗')", true); return; }
                //顯示回傳訊息結果
                string strPrint = "";
                strPrint += "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                strPrint += "<SHOWMSG>";
                strPrint += Show_XML(dom.SelectSingleNode("SHOWMSG").ChildNodes);
                strPrint += "</SHOWMSG>";

                System.Web.HttpContext.Current.Response.Write(strPrint);
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
    #endregion
}