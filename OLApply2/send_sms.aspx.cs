using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Web.UI.WebControls;

public partial class send_sms : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

        Boolean blnIsSend = false;
        string strNetNumber = Convert.ToString(Request["n5"] + "");
        string strCust_Numb = Convert.ToString(Request["n7"] + "");
        

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

            string strContent = "您好,您報名巨匠" + n1 + "之行程共" + Request["n2"].ToString() + "人,訂金共" + Request["n3"].ToString() + "元,請於" + Request["n4"].ToString() + "前付訂";
            string strPhone = Request["n6"].ToString();
            //Response.Write(strNetNumber + "<br/>" + strPhone + "<br/>");
            //Response.Write(strContent + "-----" + strContent.Length);
            fn_send_sms(strNetNumber, strContent, strPhone, strCust_Numb);

            strContent = "請於繳交訂金的同時交付護照(台胞證)影本,並填妥護照自帶書,旅客資料表。表格下載網址:http://www.grp.com.tw/data/";
            fn_send_sms(strNetNumber, strContent, strPhone, strCust_Numb);
        }

        //string n1 = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["n1"].ToString().Replace("%2B", "+")));
        //string strNetNumber = Request["n5"].ToString();
        //string strContent = "您好,您報名巨匠" + n1 + "之行程共" + Request["n2"].ToString() + "人,訂金共" + Request["n3"].ToString() + "元,請於" + Request["n4"].ToString() + "前付訂";
        //string strPhone = Request["n6"].ToString();
        //string strCust_Numb = Convert.ToString(Request["n7"] + "");
        ////Response.Write(strNetNumber + "<br/>" + strPhone + "<br/>");
        ////Response.Write(strContent + "-----" + strContent.Length);
        //fn_send_sms(strNetNumber, strContent, strPhone, strCust_Numb);

        //strContent = "請於繳交訂金的同時交付護照(台胞證)影本,並填妥護照自帶書,旅客資料表。表格下載網址:http://www.grp.com.tw/data/";
        //fn_send_sms(strNetNumber, strContent, strPhone, strCust_Numb);
    }

    protected void fn_send_sms(string strNetNumber, string strContent, string strPhone, string strCust_Numb)
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
            comm.Parameters.Add(new SqlParameter("@Reply", strMSG));
            comm.Parameters.Add(new SqlParameter("@Sales_Send", "Y"));
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
}