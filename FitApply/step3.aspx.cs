using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Web.UI.WebControls;
using System.Xml;

public partial class OLApply_step3 : System.Web.UI.Page
{
    bool boolSend; //簡訊是否已發送

    protected void Page_Load(object sender, EventArgs e)
    {
        clsFunction.Check.CheckSession();

        if (IsPostBack) return;

        Load_Data();
        Load_Data_FIT();
        
        if (!boolSend)
        {
            string strNetNumber = Lab_No.Text;
            string strContent = Lab_No.Text + "/" + Lab_name.Text + "/" + Hid_ConnPhone.Value + "/";
            strContent += Hid_BookPax.Value + "人/FIT報名單";
            string strPhone = Hid_Phone.Value;
            LineRelease(strNetNumber, strContent, strPhone);
        }
    }

    #region "=== Get ==="
    protected void Load_Data()
    {
        string strSql = "";
        strSql = " select Conn_Name,Conn_Cell,fitNo,ar_count,Conn_People,SMS";
        strSql += " ,SignUpMsg_direct,comptzone_direct,comptel_direct";
        strSql += " ,Person.pager,Person.name,Person.Line_Code,PAgent.Line_Code as Line_Code_Agent";
        strSql += " from fit10";
        strSql += " left join Person on perno = Sales";
        strSql += " left join Person PAgent on PAgent.perno = Person.Agent_Person";
        strSql += " left join COMPANY on COMPANY.compno = Person.compno";
        strSql += " where fitID = @fitID ";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@fitID", Convert.ToString(Request["ID"])));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                Lab_name.Text = reader["Conn_Name"].ToString();
                //聯絡人手機
                Hid_ConnPhone.Value = reader["Conn_Cell"].ToString();
                // 業務本人 LINE
                Hid_LineCode.Value = reader["Line_Code"].ToString();
                // 代理人 LINE
                Hid_LineCode_Agent.Value = reader["Line_Code_Agent"].ToString();

                Lab_No.Text = reader["fitNo"].ToString();
                Lab_Money.Text = Convert.ToInt32(reader["ar_count"]).ToString("n0");

                Lab_comp.Text = reader["SignUpMsg_direct"].ToString();
                Lab_sale.Text = reader["name"].ToString() + "　電話:" + reader["comptzone_direct"].ToString() + "-" + reader["comptel_direct"].ToString() + "　手機：" + reader["pager"].ToString();
                Hid_Phone.Value = reader["pager"].ToString();

                Hid_BookPax.Value = reader["Conn_People"].ToString();

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
    }

    protected void Load_Data_FIT()
    {
        int x = 1;
        string strSql = "";
        int sum = 0;

        strSql += " select Group_Name.Group_Name,IsFIT,Trip_FIT_Price from Trip";
        strSql += " join Group_Name on Group_Name.Group_Name_No = Trip.Group_Name_No";
        strSql += " where Trip_No = @Trip_No ";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();

            SqlCommand cmd = new SqlCommand(strSql, connect);
            cmd.Parameters.Add(new SqlParameter("@Trip_No", Convert.ToString(Request["no"])));
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Lab_GroupName.Text += reader["Group_Name"].ToString();
            }
            reader.Close();
            cmd.Dispose();
        }
        catch { }
        finally { connect.Close(); }

        // ===== 讀取 b2b 的統計資料 =====
        strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            strSql = " SELECT * FROM Fit20";
            strSql += " WHERE fitID = @fitID";
            strSql += " and FitType = 3";
            SqlCommand cmd = new SqlCommand(strSql, connect);
            cmd.Parameters.Add(new SqlParameter("@fitID", Convert.ToString(Request["ID"])));
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                litCostList.Text += "<tr>";
                litCostList.Text += "<td align='center' valign='middle' bgcolor='#EEEEEE'></td>";
                litCostList.Text += "<td width='120' align='center' valign='middle' bgcolor='#EEEEEE'>項目名稱</td>";
                litCostList.Text += "<td width='120' align='center' valign='middle' bgcolor='#EEEEEE'>費用</td>";
                litCostList.Text += "<td width='120' align='center' valign='middle' bgcolor='#EEEEEE'>人數</td>";
                litCostList.Text += "</tr>";

                litCostList.Text += "<tr><td rowspan='rowspan' align='center' valign='middle' bgcolor='#EEEEEE'>內含項目</td>";
                while (reader.Read())
                {
                    litCostList.Text += "</tr>";
                    litCostList.Text += "<tr>";
                    litCostList.Text += "<td align='center' valign='middle'>" + reader["FitName"].ToString() + "</td>";
                    litCostList.Text += "<td align='center' valign='middle'>" + reader["Cost_Mony"].ToString() + "</td>";
                    litCostList.Text += "<td align='center' valign='middle'>" + reader["FitPeople"].ToString() + "</td>";
                    x += 1;

                    sum += Convert.ToInt32(reader["Cost_Mony"]) * Convert.ToInt32(reader["FitPeople"]);
                }
                reader.Close();
                cmd.Dispose();
                litCostList.Text += "</tr>";
            }

            litCostList.Text = litCostList.Text.Replace("'rowspan'", "'" + x.ToString() + "'");
            x = 1;

            strSql = " SELECT * FROM Fit20";
            strSql += " WHERE fitID = @fitID";
            strSql += " and FitType = 4";
            cmd = new SqlCommand(strSql, connect);
            cmd.Parameters.Add(new SqlParameter("@fitID", Convert.ToString(Request["ID"])));
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                litCostList.Text += "<tr><td rowspan='rowspan' align='center' valign='middle' bgcolor='#EEEEEE'>自費項目</td>";
                while (reader.Read())
                {
                    litCostList.Text += "</tr>";
                    litCostList.Text += "<tr>";
                    litCostList.Text += "<td align='center' valign='middle'>" + reader["ExpName"].ToString() + "</td>";
                    litCostList.Text += "<td align='center' valign='middle'>" + reader["Cost_Mony"].ToString() + "</td>";
                    litCostList.Text += "<td align='center' valign='middle'>" + reader["ExpValue"].ToString() + "</td>";
                    x += 1;

                    sum += Convert.ToInt32(reader["Cost_Mony"]) * Convert.ToInt32(reader["ExpValue"]);
                }
                reader.Close();
                cmd.Dispose();
                litCostList.Text += "</tr>";
            }

            litCostList.Text = litCostList.Text.Replace("'rowspan'", "'" + x.ToString() + "'");
            x = 1;

            litCostList.Text += "<tr>";
            litCostList.Text += "<td align='center' valign='middle' bgcolor='#EEEEEE'>人數總計</td>";
            litCostList.Text += "<td width='120' align='center' valign='middle'>" + Hid_BookPax.Value + "</td>";
            litCostList.Text += "<td align='center' valign='middle' bgcolor='#EEEEEE'>費用總計</td>";
            litCostList.Text += "<td width='120' align='center' valign='middle'>$" + sum.ToString("n0") + "</td>";
            litCostList.Text += "</tr>";
        }
        catch { }
        finally { connect.Close(); }
    }

    #endregion
    
    #region "=== 傳LINE訊息 ==="
    ///<summary>
    ///
    ///</summary>>
    ///<param name="xmlData"></param>
    protected void LineRelease(string strNetNumber, string strContent, string strPhone)
    {
        try
        {
            string strMessage = "";
            strMessage += "" + strContent + "\r\n";

            // 報名單業務的line
            if (!string.IsNullOrEmpty(Hid_LineCode.Value))
            {
                string strXML = "";
                strXML += "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                strXML += "<Message>";
                strXML += "<MA>";
                strXML += "<A><![CDATA[lineCodeArtisan]]></A>";
                strXML += "<B>" + Convert.ToString(Hid_LineCode.Value) + "</B>";
                strXML += "<C>" + strMessage + "</C>";
                strXML += "</MA>";
                strXML += "</Message>";
                XmlDocument XmlD = new XmlDocument();
                XmlD.LoadXml(strXML);
                Stream oWriter = null;

                byte[] data = System.Text.Encoding.UTF8.GetBytes(XmlD.OuterXml);
                HttpWebRequest myRequest = ((HttpWebRequest)(WebRequest.Create("http://210.71.206.199:502/xml/SendLineMessage.aspx")));
                myRequest.Method = "POST";
                myRequest.Timeout = 300000;
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentLength = data.Length;
                myRequest.ContentType = "text/xml;charset=utf-8 ";
                oWriter = myRequest.GetRequestStream();
                oWriter.Write(data, 0, data.Length);
                oWriter.Close();
            }

            // 代理人的line
            if (!string.IsNullOrEmpty(Hid_LineCode_Agent.Value))
            {
                string strXML = "";
                strXML += "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                strXML += "<Message>";
                strXML += "<MA>";
                strXML += "<A><![CDATA[lineCodeArtisan]]></A>";
                strXML += "<B>" + Convert.ToString(Hid_LineCode_Agent.Value) + "</B>";
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
            }

            // 當FTI內部系統有收到時，就會記錄成功
            string strSql = "";
            strSql = " update fit10 set";
            strSql += " SMS = 'Y'";
            strSql += " where fitID = @fitID ";
            string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
            SqlConnection connect = new SqlConnection(strConnString);
            try
            {
                connect.Open();
                SqlCommand comm = new SqlCommand(strSql, connect);
                comm.Parameters.Add(new SqlParameter("@fitID", Convert.ToString(Request["ID"])));
                comm.ExecuteNonQuery();
                comm.Dispose();
            }
            catch { }
            finally { connect.Close(); }
        }
        catch (Exception ex) { }
    }
    #endregion
}