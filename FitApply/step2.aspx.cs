using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;
using Telerik.WebControls;

public partial class step2 : System.Web.UI.Page
{
    string Visa = "";  //紀錄簽證狀態
    string ResponseXML = ""; //XML回傳訊息
    string descrip = "", Mt_Code = "";//艙等,人房

    protected void Page_Load(object sender, EventArgs e)
    {
        clsFunction.Check.CheckSession();

        if (IsPostBack) return;
        div1.Visible = true;
        div2.Visible = false;

        //comp.Value = Request["comp"];
        //sales.Value = Request["sales"];
        c1.Value = Request["txt1"];
        c2.Value = Request["txt2"];
        c3.Value = Request["txt3"];
        c4.Value = Request["txt4"];
        c5.Value = Request["txt5"];
        N.Value = Request["n"];

        Load_Data();
        GetFitData(N.Value);
    }

    

    #region " --- Get --- "
    protected void Load_Data()
    {
        string strSql = "";
        strSql += " select Group_Name.Group_Name,IsFIT,Trip_FIT_Price from Trip";
        strSql += " join Group_Name on Group_Name.Group_Name_No = Trip.Group_Name_No";
        strSql += " where Trip_No = @Trip_No ";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand cmd = new SqlCommand(strSql, connect);
            cmd.Parameters.Add(new SqlParameter("@Trip_No", N.Value));
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Label1.Text += reader["Group_Name"].ToString();
                Label9.Text += reader["Group_Name"].ToString();
            }
            reader.Close();
            cmd.Dispose();
        }
        catch { }
        finally { connect.Close(); }


        //報名人數
        DataTable dt = new DataTable();
        dt.Columns.Add("Type", System.Type.GetType("System.String"));
        dt.Columns.Add("Name", System.Type.GetType("System.String"));
        dt.Columns.Add("Sex", System.Type.GetType("System.String"));
        dt.Columns.Add("EName1", System.Type.GetType("System.String"));
        dt.Columns.Add("EName2", System.Type.GetType("System.String"));
        dt.Columns.Add("Bri", System.Type.GetType("System.DateTime"));
        TableItem.Text = "";
        //報名人數
        if (!string.IsNullOrEmpty(Request["txt1"]) && Convert.ToInt32(Request["txt1"]) > 0)
        {
            count.Value = (Convert.ToInt32(count.Value) + Convert.ToInt32(Request["txt1"])).ToString();
            Label3.Text += "大人：" + Request["txt1"] + "位　";
            TableItem.Text += "<tr>";
            TableItem.Text += "<td align='center' valign='middle'>大人</td>";
            TableItem.Text += "<td align='center' valign='middle'>" + Convert.ToInt32(Request["txt1"]).ToString("#,0") + "</td>";
            TableItem.Text += "</tr>";

            for (int i = 1; i <= Convert.ToInt32(Request["txt1"]); i++)
            {
                DataRow dr = dt.NewRow();
                dr["Type"] = "大人";
                dt.Rows.Add(dr);
            }

        }
        if (!string.IsNullOrEmpty(Request["txt2"]) && Convert.ToInt32(Request["txt2"]) > 0)
        {
            count.Value = (Convert.ToInt32(count.Value) + Convert.ToInt32(Request["txt2"])).ToString();
            Label3.Text += "小孩佔床：" + Request["txt2"] + "位　";
            TableItem.Text += "<tr>";
            TableItem.Text += "<td align='center' valign='middle'>小孩佔床</td>";
            TableItem.Text += "<td align='center' valign='middle'>" + Convert.ToInt32(Request["txt2"]).ToString("#,0") + "</td>";
            TableItem.Text += "</tr>";

            for (int i = 1; i <= Convert.ToInt32(Request["txt2"]); i++)
            {
                DataRow dr = dt.NewRow();
                dr["Type"] = "小孩佔床";
                dt.Rows.Add(dr);
            }
        }
        if (!string.IsNullOrEmpty(Request["txt3"]) && Convert.ToInt32(Request["txt3"]) > 0)
        {
            count.Value = (Convert.ToInt32(count.Value) + Convert.ToInt32(Request["txt3"])).ToString();
            Label3.Text += "小孩不佔床：" + Request["txt3"] + "位　";
            TableItem.Text += "<tr>";
            TableItem.Text += "<td align='center' valign='middle'>小孩不佔床</td>";
            TableItem.Text += "<td align='center' valign='middle'>" + Convert.ToInt32(Request["txt3"]).ToString("#,0") + "</td>"; ;
            TableItem.Text += "</tr>";

            for (int i = 1; i <= Convert.ToInt32(Request["txt3"]); i++)
            {
                DataRow dr = dt.NewRow();
                dr["Type"] = "小孩不佔床";
                dt.Rows.Add(dr);
            }
        }
        if (!string.IsNullOrEmpty(Request["txt4"]) && Convert.ToInt32(Request["txt4"]) > 0)
        {
            count.Value = (Convert.ToInt32(count.Value) + Convert.ToInt32(Request["txt4"])).ToString();
            Label3.Text += "小孩加床：" + Request["txt4"] + "位　";
            TableItem.Text += "<tr>";
            TableItem.Text += "<td align='center' valign='middle'>小孩加床</td>";
            TableItem.Text += "<td align='center' valign='middle'>" + Convert.ToInt32(Request["txt4"]).ToString("#,0") + "</td>";
            TableItem.Text += "</tr>";

            for (int i = 1; i <= Convert.ToInt32(Request["txt4"]); i++)
            {
                DataRow dr = dt.NewRow();
                dr["Type"] = "小孩加床";
                dt.Rows.Add(dr);
            }
        }
        if (!string.IsNullOrEmpty(Request["txt5"]) && Convert.ToInt32(Request["txt5"]) > 0)
        {
            count.Value = (Convert.ToInt32(count.Value) + Convert.ToInt32(Request["txt5"])).ToString();
            Label3.Text += "嬰兒：" + Request["txt5"] + "位　";
            TableItem.Text += "<tr>";
            TableItem.Text += "<td align='center' valign='middle'>嬰兒</td>";
            TableItem.Text += "<td align='center' valign='middle'>" + Convert.ToInt32(Request["txt5"]).ToString("#,0") + "</td>";
            TableItem.Text += "</tr>";

            for (int i = 1; i <= Convert.ToInt32(Request["txt5"]); i++)
            {
                DataRow dr = dt.NewRow();
                dr["Type"] = "嬰兒";
                dt.Rows.Add(dr);
            }
        }

        strSql = " SELECT Name ,ID ,Phone ,TEL1 ,TEL2 ,TEL3 ,Mail FROM Member_Web where ID = @ID ";
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@ID", Session["PerIDNo"]));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                //中文姓名
                Label5.Text = reader["Name"].ToString();
                //手機號碼
                Label6.Text = reader["Phone"].ToString();
                //連絡電話
                Label7.Text = "( " + reader["TEL1"].ToString() + " ) " + reader["TEL2"].ToString() + (!string.IsNullOrEmpty(reader["TEL3"].ToString()) ? " #" + reader["TEL3"].ToString() : "");
                //電子郵件
                Label8.Text = reader["Mail"].ToString();
            }
            reader.Close();
            comm.Dispose();
        }
        catch { }
        finally
        {
            connect.Close();
        }

        GridView1.AutoGenerateColumns = false;
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    private void GetFitData(string Trip_No)
    {
        DataTable dt = new DataTable();
        string strSql = "";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            strSql = " SELECT *,row_number() OVER(ORDER BY orderby) as n FROM Trip_Fit";
            strSql += " WHERE IsSelfPay = 0";
            strSql += " AND Trip_No = @Trip_No";
            strSql += " ORDER BY orderby";

            SqlDataAdapter da = new SqlDataAdapter(strSql, connect);
            da.SelectCommand.Parameters.Add(new SqlParameter("@Trip_No", Trip_No));
            da.Fill(dt);

            GridView2.DataSource = dt;
            GridView2.DataKeyNames = new string[] { "Number" };
            GridView2.DataBind();

            strSql = " SELECT *,row_number() OVER(ORDER BY orderby) as n  FROM Trip_Fit";
            strSql += " WHERE IsSelfPay = 1";
            strSql += " AND Trip_No = @Trip_No";
            strSql += " ORDER BY orderby";

            da = new SqlDataAdapter(strSql, connect);
            da.SelectCommand.Parameters.Add(new SqlParameter("@Trip_No", Trip_No));
            dt = new DataTable();
            da.Fill(dt);

            GridView3.DataSource = dt;
            GridView3.DataKeyNames = new string[] { "Number" };
            GridView3.DataBind();
        }
        catch { }
        finally { connect.Close(); }
    }

    private int GetSum()
    {
        int sum = 0;

        for (int ii = 0; ii <= GridView2.Rows.Count - 1; ii++)
        {
            string hiddFPrice = ((HiddenField)GridView2.Rows[ii].FindControl("hidFPrice")).Value;
            string BookPax = ((DropDownList)GridView2.Rows[ii].FindControl("dlnum")).SelectedValue;

            if (Convert.ToInt32(BookPax) > 0)
            {
                sum += Convert.ToInt32(hiddFPrice) * Convert.ToInt32(BookPax);
            }
        }

        for (int ii = 0; ii <= GridView3.Rows.Count - 1; ii++)
        {
            string hiddFPrice = ((HiddenField)GridView3.Rows[ii].FindControl("hidFPrice")).Value;
            string BookPax = ((DropDownList)GridView3.Rows[ii].FindControl("dlnum2")).SelectedValue;

            if (Convert.ToInt32(BookPax) > 0)
            {
                sum += Convert.ToInt32(hiddFPrice) * Convert.ToInt32(BookPax);
            }
        }

        return sum;
    }
    #endregion

    #region " --- Function --- "
    protected void fn_Save()
    {
        string strSql = "";
        string strConnTel = "";
        string strConnFax = "";
        string strComp_Code = "";
        string strCONT_CELL = "";
        string strCONT_MAIL = "";
        string strSales_Name = "";

        // 抓取聯絡人相關資料
        strSql += " SELECT Agent_M.AGT_NAME1,Agent_M.TEL1_ZONE,Agent_M.TEL1,Agent_M.FAX_ZONE,Agent_M.FAX";
        strSql += " ,Agent_M.Sales,AGENT_L.CONT_ZONE,AGENT_L.CONT_TEL,AGENT_L.CFAX_ZONE,AGENT_L.CONT_FAX";
        strSql += " ,AGENT_L.CONT_CELL,AGENT_L.CONT_MAIL";
        strSql += " FROM Agent_M";
        strSql += " LEFT JOIN AGENT_L ON AGENT_L.AGT_NAME1 = Agent_M.AGT_NAME1";
        strSql += " WHERE Agent_M.COMP_NO = @COMP_NO";
        strSql += " AND AGENT_L.AGT_IDNo = @AGT_IDNo";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@COMP_NO", Session["Compno"]));
            comm.Parameters.Add(new SqlParameter("@AGT_IDNo", Session["PerIDNo"]));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                // 同業聯絡人(若同業有輸入資料就帶資料，若沒有的話帶公司資料)
                if (!string.IsNullOrEmpty(reader["CONT_TEL"].ToString()))
                {
                    if (!string.IsNullOrEmpty(reader["CONT_ZONE"].ToString()))
                    { strConnTel += reader["CONT_ZONE"].ToString(); }
                    if (!string.IsNullOrEmpty(reader["CONT_TEL"].ToString()))
                    { strConnTel += (string.IsNullOrEmpty(strConnTel) ? "" : "-") + reader["CONT_TEL"].ToString(); }
                }
                else
                {
                    if (!string.IsNullOrEmpty(reader["TEL1_ZONE"].ToString()))
                    { strConnTel += reader["TEL1_ZONE"].ToString(); }
                    if (!string.IsNullOrEmpty(reader["TEL1"].ToString()))
                    { strConnTel += (string.IsNullOrEmpty(strConnTel) ? "" : "-") + reader["TEL1"].ToString(); }
                }


                if (!string.IsNullOrEmpty(reader["CONT_FAX"].ToString()))
                {
                    if (!string.IsNullOrEmpty(reader["CFAX_ZONE"].ToString()))
                    { strConnFax += reader["CFAX_ZONE"].ToString(); }
                    if (!string.IsNullOrEmpty(reader["CONT_FAX"].ToString()))
                    { strConnFax += (string.IsNullOrEmpty(strConnFax) ? "" : "-") + reader["CONT_FAX"].ToString(); }
                }
                else
                {
                    if (!string.IsNullOrEmpty(reader["FAX_ZONE"].ToString()))
                    { strConnFax += reader["FAX_ZONE"].ToString(); }
                    if (!string.IsNullOrEmpty(reader["FAX"].ToString()))
                    { strConnFax += (string.IsNullOrEmpty(strConnFax) ? "" : "-") + reader["FAX"].ToString(); }
                }

                strComp_Code = reader["AGT_NAME1"].ToString();
                strCONT_CELL = reader["CONT_CELL"].ToString();
                strCONT_MAIL = reader["CONT_MAIL"].ToString();
                strSales_Name = reader["Sales"].ToString();
            }
            reader.Close();
            comm.Dispose();
        }
        catch { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('出錯了。');", true); return; }
        finally { connect.Close(); }


        // ****************************************************************************************************
        // 若取不到資料，就抓取 agent_m 的資料
        if (string.IsNullOrEmpty(strConnTel) && string.IsNullOrEmpty(strConnFax))
        {
            strSql = " SELECT AGT_NAME1,(TEL1_ZONE + TEL1) as TEL ,(FAX_ZONE + FAX) as FAX ,Sales ";
            strSql += " FROM Agent_M";
            strSql += " where COMP_NO = @COMP_NO ";
            try
            {
                connect.Open();
                SqlCommand comm = new SqlCommand(strSql, connect);
                comm.Parameters.Add(new SqlParameter("@COMP_NO", Session["Compno"]));
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    strConnTel = reader["TEL"].ToString();
                    strConnFax = reader["FAX"].ToString();
                    strComp_Code = reader["AGT_NAME1"].ToString();
                    strSales_Name = reader["Sales"].ToString();
                }
                reader.Close();
                comm.Dispose();
            }
            catch { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('出錯了。');", true); return; }
            finally { connect.Close(); }
        }

        // ****************************************************************************************************
        // 檢查字元長度
        if (strConnTel.Length > 16)
        { strConnTel = strConnTel.Substring(0, 16); }
        if (strConnFax.Length > 16)
        { strConnFax = strConnFax.Substring(0, 16); }
        if (strCONT_CELL.Length > 10)
        { strCONT_CELL = strCONT_CELL.Substring(0, 10); }

        // ****************************************************************************************************
        string fitID = "";
        string fitNo = "";
        strSql = "DECLARE @numberdate nvarchar(6);";
        strSql += " DECLARE @Number nvarchar(12);";
        strSql += " select @numberdate =";
        strSql += " cast(year(getdate()) as nvarchar) + (case when len(cast(month(getdate()) as nvarchar)) = 1 then '0'+ cast(month(getdate()) as nvarchar) else cast(month(getdate()) as nvarchar) end)";
        strSql += " SELECT @Number =";
        strSql += " 'K'+ @numberdate";
        strSql += " + REPLICATE('0', 5 - len(CAST(isnull(max(CAST(right(FitNO,5) AS float)),0)+ 1 as nvarchar)))";
        strSql += " + CAST(isnull(max(cast(right(FitNO,5) as float)),0)+1 as nvarchar)";
        strSql += " FROM fit10";
        strSql += " where left(FitNO,7) = 'K' + @numberdate";

        strSql += " insert into fit10 ( ";
        strSql += " [FitNO],[FitDate],[Sales],[Conn_Name],[Conn_Cell],[Conn_Tel],[Conn_Fax],[Conn_Mail],[Conn_People],[creaUsr],[creaDate],[ar_count],[EnliI_Date],[ConnTel]";
        strSql += " ) VALUES ( ";
        strSql += " @Number ,@FitDate ,@Sales ,@Conn_Name ,@Conn_Cell ,@Conn_Tel ,@Conn_Fax ,@Conn_Mail ,@Conn_People ,@user ,getdate() ,@ar_count ,'2',@ConnTel";
        strSql += " ) ";
        strSql += " select @Number as Number,scope_identity() AS Tr10Number";

        string tel = Tel1.Text.Trim() + Tel2.Text.Trim() + "#" + Tel3.Text.Trim();
        string Fax = Fax1.Text.Trim() + Fax2.Text.Trim();
        if (tel == "#") { tel = ""; }
        try
        {
            connect.Open();

            SqlCommand cmd = new SqlCommand(strSql, connect);
            cmd.Parameters.Add(new SqlParameter("@FitDate", DateTime.Now.ToString("yyyy/MM/dd")));
            cmd.Parameters.Add(new SqlParameter("@Sales", strSales_Name));
            cmd.Parameters.Add(new SqlParameter("@Conn_Name", Label5.Text.Trim()));
            cmd.Parameters.Add(new SqlParameter("@Conn_Cell", Label6.Text.Trim()));
            cmd.Parameters.Add(new SqlParameter("@Conn_Tel", Label7.Text.Trim()));
            cmd.Parameters.Add(new SqlParameter("@Conn_Fax", Fax));
            cmd.Parameters.Add(new SqlParameter("@Conn_Mail", Label8.Text.Trim()));
            cmd.Parameters.Add(new SqlParameter("@Conn_People", count.Value));
            cmd.Parameters.Add(new SqlParameter("@user", Session["PerIDNo"]));
            cmd.Parameters.Add(new SqlParameter("@ar_count", GetSum().ToString()));
            cmd.Parameters.Add(new SqlParameter("@ConnTel", tel));

            SqlDataReader reader1 = cmd.ExecuteReader();
            if (reader1.Read())
            {
                fitNo = reader1["Number"].ToString();
                fitID = reader1["Tr10Number"].ToString();
            }
            cmd.Dispose();
            reader1.Close();
            connect.Close();

            fn_Save20(fitID);
            fn_Save30(fitID, fitNo);

            fn_Send_XML(fitNo, fitID);
        }
        catch
        {
            connect.Close();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('出錯了。');", true);
            return;
        }
    }

    protected void fn_Save20(string strFitID)
    {
        string strSql = "";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        //內含項目
        for (int ii = 0; ii <= GridView2.Rows.Count - 1; ii++)
        {
            HiddenField hidFitName = ((HiddenField)GridView2.Rows[ii].FindControl("hidFitName"));
            HiddenField hidFPrice = ((HiddenField)GridView2.Rows[ii].FindControl("hidFPrice"));
            string BookPax = ((DropDownList)GridView2.Rows[ii].FindControl("dlnum")).SelectedValue;
            string Number = GridView2.DataKeys[ii].Value.ToString();

            if (Convert.ToInt32(BookPax) > 0)
            {
                // 寫入 Fit20
                try
                {
                    connect.Open();
                    strSql = " insert into Fit20 ( ";
                    strSql += " [FitID],[FitType],[FitName],[FitPeople],[creaUsr]";
                    strSql += " ,[creaDate],[UpdUsr],[UpdDate],[Cost_Mony],[FIT20_MONY_TYPE]";
                    strSql += " ,[FIT20_MONY_PERC],[FIT20_Profit]";
                    strSql += " ) VALUES ( ";
                    strSql += " @FitID ,'3' ,@FitName ,@FitPeople ,@user ";
                    strSql += " ,getdate() ,@user ,getdate() ,@Cost_Mony ,@FIT20_MONY_TYPE";
                    strSql += " ,@FIT20_MONY_PERC ,@FIT20_Profit";
                    strSql += " ) ";

                    SqlCommand cmd = new SqlCommand(strSql, connect);
                    cmd.Parameters.Add(new SqlParameter("@FitID", strFitID));
                    cmd.Parameters.Add(new SqlParameter("@FitName", hidFitName.Value));
                    cmd.Parameters.Add(new SqlParameter("@FitPeople", BookPax));
                    cmd.Parameters.Add(new SqlParameter("@user", Session["PerIDNo"]));
                    cmd.Parameters.Add(new SqlParameter("@Cost_Mony", hidFPrice.Value));
                    cmd.Parameters.Add(new SqlParameter("@FIT20_MONY_TYPE", "NTD"));
                    cmd.Parameters.Add(new SqlParameter("@FIT20_MONY_PERC", "1"));
                    cmd.Parameters.Add(new SqlParameter("@FIT20_Profit", "0"));
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                finally { connect.Close(); }
            }           
        }

        //自費項目
        for (int ii = 0; ii <= GridView3.Rows.Count - 1; ii++)
        {
            HiddenField hidFitName = ((HiddenField)GridView3.Rows[ii].FindControl("hidFitName"));
            HiddenField hidFPrice = ((HiddenField)GridView3.Rows[ii].FindControl("hidFPrice"));
            string BookPax = ((DropDownList)GridView3.Rows[ii].FindControl("dlnum2")).SelectedValue;
            string Number = GridView3.DataKeys[ii].Value.ToString();

            if (Convert.ToInt32(BookPax) > 0)
            {
                try
                {
                    connect.Open();

                    strSql = " insert into Fit20 ( ";
                    strSql += " [FitID],[FitType],[ExpName],[ExpValue],[creaUsr]";
                    strSql += " ,[creaDate],[UpdUsr],[UpdDate],[Cost_Mony],[FIT20_MONY_TYPE]";
                    strSql += " ,[FIT20_MONY_PERC],[FIT20_Profit]";
                    strSql += " ) VALUES ( ";
                    strSql += " @FitID ,'4' ,@FitName ,@FitPeople ,@user ";
                    strSql += " ,getdate() ,@user ,getdate() ,@Cost_Mony ,@FIT20_MONY_TYPE ";
                    strSql += " ,@FIT20_MONY_PERC ,@FIT20_Profit ";
                    strSql += " ) ";

                    SqlCommand cmd = new SqlCommand(strSql, connect);
                    cmd.Parameters.Add(new SqlParameter("@FitID", strFitID));
                    cmd.Parameters.Add(new SqlParameter("@FitName", hidFitName.Value));
                    cmd.Parameters.Add(new SqlParameter("@FitPeople", BookPax));
                    cmd.Parameters.Add(new SqlParameter("@user", Session["PerIDNo"]));
                    cmd.Parameters.Add(new SqlParameter("@Cost_Mony", hidFPrice.Value));
                    cmd.Parameters.Add(new SqlParameter("@FIT20_MONY_TYPE", "NTD"));
                    cmd.Parameters.Add(new SqlParameter("@FIT20_MONY_PERC", "1"));
                    cmd.Parameters.Add(new SqlParameter("@FIT20_Profit", "0"));
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                finally { connect.Close(); }
            }
        }
    }

    protected void fn_Save30(string strFitID ,string strFitNo)
    {
        string strSql = "";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        for (int ii = 0; ii <= GridView1.Rows.Count - 1; ii++)
        {
            Label strType = (Label)GridView1.Rows[ii].FindControl("Lab_type");
            TextBox strName = (TextBox)GridView1.Rows[ii].FindControl("Txt_name");
            TextBox strEname1 = (TextBox)GridView1.Rows[ii].FindControl("Txt_Ename1");
            TextBox strEname2 = (TextBox)GridView1.Rows[ii].FindControl("Txt_Ename2");
            TextBox strBri = (TextBox)GridView1.Rows[ii].FindControl("Txt_bri");


            strSql = " insert into Fit30( ";
            strSql += " [FitID],[FitNO],[SEQ_NO],[Cust_Name],[ENGL_CUST]";
            strSql += " ,[Birt_Date],[CreaUsr],[CreaDate],[UpdDate],[UpdUsr]";
            strSql += " ) VALUES ( ";
            strSql += " @FitID ,@FitNO ,@SEQ_NO ,@Cust_Name ,@ENGL_CUST";
            strSql += " ,@Birt_Date ,@user ,getdate() ,getdate() ,@user ";
            strSql += " ) ";

            connect.Open();
            SqlCommand cmd = new SqlCommand(strSql, connect);
            cmd.Parameters.Add(new SqlParameter("@FitID", strFitID));
            cmd.Parameters.Add(new SqlParameter("@FitNO", strFitNo));
            cmd.Parameters.Add(new SqlParameter("@SEQ_NO", (ii+1).ToString("0000")));
            cmd.Parameters.Add(new SqlParameter("@Cust_Name", strName.Text.Trim()));
            cmd.Parameters.Add(new SqlParameter("@ENGL_CUST", strEname1.Text.ToUpper() + "," + strEname2.Text.ToUpper()));

            if (string.IsNullOrEmpty(strBri.Text)) { cmd.Parameters.Add(new SqlParameter("@BIRT_DATE", DBNull.Value)); }
            else { cmd.Parameters.Add(new SqlParameter("@BIRT_DATE", Convert.ToDateTime(strBri.Text).ToString("yyyy-MM-dd"))); }

            cmd.Parameters.Add(new SqlParameter("@user", Session["PerIDNo"]));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connect.Close();
        }
    }

    protected void fn_Send_XML(string strTr10Number, string strfitID)
    {
        //要傳送的資料
        string strXML = "";
        strXML += "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
        strXML += "<Message>";
        strXML += "<MA>";
        strXML += "<A><![CDATA[artisan988]]></A>";  //驗證碼
        strXML += "<B><![CDATA[" + strTr10Number + "]]></B>";  //網路報名單號
        strXML += "</MA>";
        strXML += "</Message>";

        string strURL = "http://210.71.206.199:502/xml/GetApplyFitB2B.aspx";
        //string strURL = "http://localhost:789/xml/GetApplyFitB2B.aspx";
        string strComNum = "";
        fn_Show_XML(SendRequest(strURL, strXML, strComNum), strfitID);
    }

    protected void ErrorDel(string strFitID)
    {

        string strSql = "";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        strSql = " delete fit10 FitID where FitID = @FitID";
        strSql += " delete fit20 FitID where FitID = @FitID";
        strSql += " delete fit30 FitID where FitID = @FitID";
        try
        {
            connect.Open();

            SqlCommand cmd = new SqlCommand(strSql, connect);
            cmd.Parameters.Add(new SqlParameter("@FitID", strFitID));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        catch { }
        finally { connect.Close();}

        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('報名失敗')", true);
    }
    #endregion

    #region " --- 控制項 --- "
    protected void Button1_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "history.go(-2);", true); return;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string tel = Tel1.Text.Trim() + Tel2.Text.Trim() + "#" + Tel3.Text.Trim();
        string Fax = Fax1.Text.Trim() + Fax2.Text.Trim();

        if (Label5.Text.Trim() == "") 
        { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('請輸入姓名');", true); }
        else if (tel.Replace("#", "") == "" && Label6.Text.Trim() == "" && Fax.Trim() == "" && Label7.Text.Trim() == "" && Label8.Text.Trim() == "")
        { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('請輸入任一聯絡方式');", true); }
        else
        {
            div1.Visible = false;
            div2.Visible = true;
        }
        
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "history.go(-2);", true); return;
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        
        bool isCheck = true;
        for (int ii = 0; ii <= GridView1.Rows.Count - 1; ii++)
        {
            TextBox Txt_name = (TextBox)GridView1.Rows[ii].FindControl("Txt_name");
            TextBox Txt_bri = (TextBox)GridView1.Rows[ii].FindControl("Txt_bri");

            if (string.IsNullOrEmpty(Txt_name.Text.Trim()))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('旅客中文姓名必填。');", true);
                isCheck = false;
                Button4.Enabled = true;
                return;
            }

            if (!string.IsNullOrEmpty(Txt_bri.Text.Trim()))
            {
                string strDate = Txt_bri.Text.Trim();
                DateTime dt;
                if (!DateTime.TryParse(strDate, out dt))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('西元生日錯誤。\\n西元生日格式範例：2016/01/23');", true);
                    isCheck = false;
                    Button4.Enabled = true;
                    return;
                }
            }
        }

        
        if (isCheck) { fn_Save(); }
        
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridViewRow row = (GridViewRow)e.Row;
            DropDownList dlnum = (DropDownList)row.FindControl("dlnum");

            int sum = 0;
            if (c1.Value != "") { sum += Convert.ToInt32(c1.Value); }
            if (c2.Value != "") { sum += Convert.ToInt32(c2.Value); }
            if (c3.Value != "") { sum += Convert.ToInt32(c3.Value); }
            if (c4.Value != "") { sum += Convert.ToInt32(c4.Value); }
            if (c5.Value != "") { sum += Convert.ToInt32(c5.Value); }

            for (int i = 0; i < sum + 1; i++)
            {
                dlnum.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }
    }

    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridViewRow row = (GridViewRow)e.Row;
            DropDownList dlnum2 = (DropDownList)row.FindControl("dlnum2");

            int sum = 0;
            if (c1.Value != "") { sum += Convert.ToInt32(c1.Value); }
            if (c2.Value != "") { sum += Convert.ToInt32(c2.Value); }
            if (c3.Value != "") { sum += Convert.ToInt32(c3.Value); }
            if (c4.Value != "") { sum += Convert.ToInt32(c4.Value); }
            if (c5.Value != "") { sum += Convert.ToInt32(c5.Value); }

            for (int i = 0; i < sum + 1; i++)
            {
                dlnum2.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }
    }
    #endregion

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
        //request.ContentType = "application/x-www-form-urlencoded";
        request.ContentType = "text/xml";
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
    protected void fn_Show_XML(string xmlData, string fitID)
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
                if (ResponseXML == "OK") { Response.Redirect("step3.aspx?ID=" + fitID + "&no=" + N.Value); }
                else { ErrorDel(fitID); return; }
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

    #region " --- IP --- "
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
    #endregion
}