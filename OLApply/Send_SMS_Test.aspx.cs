using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Web.UI.WebControls;

public partial class OLApply_Send_SMS_Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        fn_send_sms("", txbContent.Text.Trim(), txbPhone.Text.Trim());
    }
    protected void fn_send_sms(string strNetNumber, string strContent, string strPhone)
    {
        //strContent = "看的到嗎\r\n";
        //strContent += "看的到嗎  看的到嗎\r\n";
        //strContent += "看的到嗎  看的到嗎  看的到嗎\r\n";

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
        sbPost.Append("             <Subject>test</Subject>");
        sbPost.Append("             <Retry>Y</Retry>");
        sbPost.Append("             <AutoSplit>N</AutoSplit>");
        sbPost.Append("             <StopDateTime>" + Convert.ToDateTime(System.DateTime.Now).ToString("yyyyMMdd") + "2359</StopDateTime>");
        sbPost.Append("             <Message>" + strContent + "</Message>");
        sbPost.Append("             <MDNList>");
        //發送的手機號碼
        sbPost.Append("                 <MSISDN>" + strPhone + "</MSISDN>");
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

        if (strMSG.IndexOf("成功") > 0) { Label1.Text = "簡訊發送成功"; }
        else { Label1.Text = "簡訊發送失敗，請聯絡 #200"; }
        //Response.Write(strMSG);
        //Response.End();

        //string strSql = "";
        //strSql += " insert into SMS_log (netcustnumb,Send_Phone,Send_Date,SMS_Content,Reply,Sales_Send) values (@netcustnumb,@Send_Phone,@Send_Date,@SMS_Content,@Reply,@Sales_Send) ";
        //string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        //SqlConnection connect = new SqlConnection(strConnString);

        //try
        //{
        //    connect.Open();
        //    SqlCommand comm = new SqlCommand(strSql, connect);
        //    comm.Parameters.Add(new SqlParameter("@netcustnumb", strNetNumber));
        //    comm.Parameters.Add(new SqlParameter("@Send_Phone", strPhone));
        //    comm.Parameters.Add(new SqlParameter("@Send_Date", Convert.ToDateTime(System.DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss")));
        //    comm.Parameters.Add(new SqlParameter("@SMS_Content", strContent));
        //    comm.Parameters.Add(new SqlParameter("@Reply", strMSG));
        //    comm.Parameters.Add(new SqlParameter("@Sales_Send", "Y"));
        //    comm.ExecuteNonQuery();
        //    comm.Dispose();
        //}
        //catch
        //{
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('出錯了。');", true);
        //    return;
        //}
        //finally
        //{
        //    connect.Close();
        //}

        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('簡訊已發送。'); location='/index.aspx';", true); return;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        txbContent.Text = "";
        txbPhone.Text = "";
        Label1.Text = "";
    }
}