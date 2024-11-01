using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Xml;

public partial class Test_testSMS : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void fn_send_sms(string strNetNumber, string strContent, string strPhone, string strCust_Numb)
    {
        //// 使用Base64編碼
        ////string originalString = "Hello, World!";
        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(strContent);
        //string base64String = Convert.ToBase64String(bytes);

        //WebRequest myHttpWebRequest = WebRequest.Create("http://61.20.35.179:6500/mpsiweb/smssubmit");
        //myHttpWebRequest.Method = "POST";
        //StringBuilder sbPost = new StringBuilder();
        ////sbPost.Append("<? xml version=\"1.0\" encoding=\"UTF-8\" ?>");
        ////sbPost.Append("<SmsSubmitReq>");
        ////sbPost.Append("<SysId>OMPUSHTK</SysId>");
        ////sbPost.Append("<SrcAddress>01916900012103600000</SrcAddress>");
        ////sbPost.Append("<DestAddress>0930136596</DestAddress>");
        ////sbPost.Append("<SmsBody>" + base64String + "</SmsBody>");
        ////sbPost.Append("<DrFlag>false</DrFlag>");
        //////sbPost.Append("<FirstFailFlag>true</FirstFailFlag>");
        ////sbPost.Append("</SmsSubmitReq>");

        //sbPost.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sbPost.Append("<SmsSubmitReq>");
        //sbPost.Append("<SysId>OMPUSHTK</SysId>");
        //sbPost.Append("<SrcAddress>01916900012103600000</SrcAddress>");
        //sbPost.Append("<DestAddress>886930136596</DestAddress>");
        //sbPost.Append("< >SGVsbG8gV29ybGQ=</SmsBody>");
        //sbPost.Append("<DrFlag>false</DrFlag>");
        //sbPost.Append("</SmsSubmitReq>");


        //Label2.Text = FormatXmlForDisplay(sbPost.ToString());

        // 示例 SMS 內容
        string smsBody = "Hello World";

        // 呼叫發送請求的方法
        string response = SendRequest(smsBody).Result;
        Label1.Text = response;

    }
    public static async Task<string> SendRequest(string strSmsBody)
    {
        string url = "http://61.20.35.179:6500/mpsiweb/smssubmit";

        // 將SMS內容轉換為Base64
        string base64SmsBody = Convert.ToBase64String(Encoding.UTF8.GetBytes(strSmsBody));

        // 構造XML字串
        string xmlString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
"<SmsSubmitReq>" +
"<SysId>OMPUSHTK</SysId>" +
"<SrcAddress>01916900012103600000</SrcAddress>" +
"<DestAddress>886930136596</DestAddress>" +
"<SmsBody>{0}</SmsBody>" +
"<DrFlag>false</DrFlag>" +
"</SmsSubmitReq>";
        xmlString = string.Format(xmlString, base64SmsBody);

        // 将XML字符串进行URL编码
        string encodedXmlString = Uri.EscapeDataString(xmlString);

        // 将URL编码后的XML字符串作为请求参数
        string requestBody = "xml=" + encodedXmlString;

        using (HttpClient client = new HttpClient())
        {
            // 设置请求的内容类型为 application/x-www-form-urlencoded
            HttpContent content = new StringContent(requestBody, Encoding.UTF8, "application/x-www-form-urlencoded");

            // 发送POST请求
            HttpResponseMessage response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                // 读取响应内容
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return "Error: " + response.StatusCode.ToString();
            }
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        fn_send_sms("", "測試簡訊:" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), "0930136596", "");
    }

    private string FormatXmlForDisplay(string xml)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xml);

        StringBuilder sb = new StringBuilder();
        using (System.IO.StringWriter writer = new System.IO.StringWriter(sb))
        {
            using (XmlTextWriter xmlWriter = new XmlTextWriter(writer))
            {
                xmlWriter.Formatting = Formatting.Indented;
                doc.WriteTo(xmlWriter);
            }
        }

        string formattedXml = sb.ToString();
        // HTML-escape the XML content
        return Server.HtmlEncode(formattedXml).Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />");

    }

}