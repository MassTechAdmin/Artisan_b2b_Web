using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Xml;

public partial class send_sms : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

        Boolean blnIsSend = false;
        string strNetNumber = Convert.ToString(Request["n5"] + "");
        string strCust_Numb = Convert.ToString(Request["n7"] + "");
        string strDEPA_DAY = Convert.ToString(Request["n8"] + ""); // 出發日期

        string strSql = "";
        strSql = " SELECT netcustnumb FROM SMS_log";
        strSql += " WHERE netcustnumb=@netcustnumb";
        strSql += " AND Sales_Send='Y'";
        strSql += " AND isnull(netcustnumb,'') <> ''";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@netcustnumb", strNetNumber));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                blnIsSend = true;
            }
            reader.Close();
            comm.Dispose();
        }
        finally
        {
            connect.Close();
        }


        strSql = " SELECT Cust_Numb FROM SMS_log";
        strSql += " WHERE Cust_Numb=@Cust_Numb";
        strSql += " AND Sales_Send='Y'";
        strSql += " AND isnull(Cust_Numb,'') <> ''";
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@Cust_Numb", strCust_Numb));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                blnIsSend = true;
            }
            reader.Close();
            comm.Dispose();
        }
        finally
        {
            connect.Close();
        }


        if (blnIsSend == true)
        {
            Label1.Text = "已發送過簡訊。";
        }
        else
        {
            string n1 = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["n1"].ToString().Replace("%2B", "+")));

            string strContent = "";
            string strPhone = Request["n6"].ToString();

            strContent = "報名" + strDEPA_DAY + "巨匠" + n1 + "之行程共" + Request["n2"].ToString() + "人,訂金共" + Request["n3"].ToString() + "元,請於" + Request["n4"].ToString() + "前付訂";
            fn_send_sms(strNetNumber, strContent, strPhone, strCust_Numb);


            strContent = "請於繳交訂金的同時交付護照(台胞證)影本,並填妥護照自帶書,旅客資料表。表格下載網址:http://www.grp.com.tw/data/";
            fn_send_sms(strNetNumber, strContent, strPhone, strCust_Numb);
        }
    }

    protected void fn_send_sms(string strNetNumber, string strContent, string strPhone, string strCust_Numb)
    {
        // 傳送簡訊"遠傳"
        string response = SendRequest(strContent, strPhone).Result;
        string resultCode = ParseResponse(response);

        // 將結果寫入資料庫內
        string strSql = "";
        strSql += " insert into SMS_log (";
        strSql += " netcustnumb,Cust_Numb,Send_Phone,Send_Date,SMS_Content,Reply,Sales_Send";
        strSql += " ) values (";
        strSql += " @netcustnumb,@Cust_Numb,@Send_Phone,@Send_Date,@SMS_Content,@Reply,@Sales_Send";
        strSql += " ) ";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@netcustnumb", strNetNumber));
            comm.Parameters.Add(new SqlParameter("@Cust_Numb", strCust_Numb));
            comm.Parameters.Add(new SqlParameter("@Send_Phone", strPhone));
            comm.Parameters.Add(new SqlParameter("@Send_Date", Convert.ToDateTime(System.DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss")));
            comm.Parameters.Add(new SqlParameter("@SMS_Content", strContent));
            comm.Parameters.Add(new SqlParameter("@Reply", response)); //strMSG
            comm.Parameters.Add(new SqlParameter("@Sales_Send", (resultCode == "00000" ? "Y" : "N")));
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

    public static async Task<string> SendRequest(string strSmsBody, string strPhone)
    {
        string url = "http://61.20.35.179:6500/mpsiweb/smssubmit";

        // 將SMS內容轉換為Base64
        string base64SmsBody = Convert.ToBase64String(Encoding.UTF8.GetBytes(strSmsBody));

        // 構造XML字串
        string xmlString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
"<SmsSubmitReq>" +
"<SysId>OMPUSHTK</SysId>" +
"<SrcAddress>01916900012103600000</SrcAddress>" +
"<DestAddress>{1}</DestAddress>" + //886930136596
"<SmsBody>{0}</SmsBody>" +
"<DrFlag>false</DrFlag>" +
"</SmsSubmitReq>";
        xmlString = string.Format(xmlString, base64SmsBody, strPhone);

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

    public static string ParseResponse(string xmlResponse)
    {
        try
        {
            // 加载 XML 响应
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlResponse);

            // 获取 ResultCode 的值
            XmlNode resultCodeNode = xmlDoc.SelectSingleNode("//ResultCode");
            if (resultCodeNode != null)
            {
                return resultCodeNode.InnerText.Trim();
            }
            else
            {
                return "ResultCode not found";
            }
        }
        catch (Exception ex)
        {
            return "Error parsing response: " + ex.Message;
        }
    }
}