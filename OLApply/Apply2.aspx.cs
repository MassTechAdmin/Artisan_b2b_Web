using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Apply2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsFunction.Check.CheckSession();
        if (IsPostBack) return;
        Load_Data();
    }

    protected void Load_Data()
    {
        string strSql = "";
        strSql += " SELECT AGENT_L.AGT_CONT,AGENT_L.AGT_IDNo,AGENT_L.CONT_CELL,AGENT_L.CONT_MAIL,AGENT_M.COMP_NO";
        strSql += " ,AGT_NAME2,(TEL1_ZONE + TEL1) as TEL,name,Person.Pager,Person.Compno";
        strSql += " ,AGENT_L.Cont_Zone,AGENT_L.Cont_Tel";
        strSql += " FROM AGENT_L";
        strSql += " left join AGENT_M on AGENT_L.AGT_NAME1 = AGENT_M.AGT_NAME1";
        strSql += " left join Person on AGENT_L.SALE_CODE = Person.PerNo";
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
                //客戶帳號
                Lbl_ID.Text = reader["AGT_NAME2"].ToString();
                //公司電話
                if (string.IsNullOrEmpty(reader["Cont_Tel"].ToString()))
                { Lbl_TEL.Text = reader["TEL"].ToString(); }
                else
                {
                    if (string.IsNullOrEmpty(reader["Cont_Zone"].ToString()))
                    { Lbl_TEL.Text = reader["Cont_Tel"].ToString(); }
                    else
                    { Lbl_TEL.Text = reader["Cont_Zone"].ToString() + "-" + reader["Cont_Tel"].ToString(); }
                }
                //手機號碼
                Lbl_Phone.Text = reader["CONT_CELL"].ToString();
                //會員姓名
                Lbl_Name.Text = reader["AGT_CONT"].ToString();
                //電子郵件
                Lbl_EMail.Text = reader["CONT_MAIL"].ToString();
                //業務代表 台北 ‧ 宋瑞雯　公司電話：02-25676606　手機號碼：0927004275
                Lbl_Sales.Text += reader["name"].ToString();
                switch (reader["compno"].ToString())
                {
                    case "A":
                        Lbl_Sales.Text += " ‧ 台北　公司電話：(02)2518-0011　";
                        break;
                    case "B":
                        Lbl_Sales.Text += " ‧ 高雄　公司電話：(07)2419-888　";
                        break;
                    case "C":
                        Lbl_Sales.Text += " ‧ 台中　公司電話：(04)2255-1168　";
                        break;
                    case "D":
                        Lbl_Sales.Text += " ‧ 桃園　公司電話：(03)3371-222　";
                        break;
                    case "F":
                        Lbl_Sales.Text += " ‧ 台南　公司電話：(06)222-6736　";
                        break;
                    case "H":
                        Lbl_Sales.Text += " ‧ 新竹　公司電話：(03)5283-088　";
                        break;

                }
                Lbl_Sales.Text += "手機號碼：" + reader["pager"].ToString();
            }
            reader.Close();
            comm.Dispose();
            fn_Show_Tour_Price(Request["n"]);
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

    protected void fn_Show_Tour_Price(string strNumber)
    {
        string strSql = "";
        strSql = " SELECT [Number],[Sequ_No],[Tick_Type],[Tour_Type],[Bed_Type]";
        strSql += " ,[Cruises_Type],IsNull([SalePrice],0) AS SalePrice,IsNull([AgentPrice],0) AS AgentPrice";
        strSql += " ,Grop.Group_Category_No,Area.Area_No";
        strSql += " FROM [Tour_Price]";
        strSql += " LEFT JOIN Grop ON Tour_Price.Number = Grop.Van_Number";
        strSql += " LEFT JOIN Area ON Area.Area_ID = GROP.Area_Code";
        strSql += " WHERE Number = @Number ";
        strSql += " ORDER BY [Sequ_No]";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            litPay0.Text = "來電洽詢"; TextBox0.Enabled = false;
            litPay1.Text = "來電洽詢"; TextBox1.Enabled = false;
            litPay3.Text = "來電洽詢"; TextBox3.Enabled = false;
            litPay2.Text = "來電洽詢"; TextBox2.Enabled = false;

            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@Number", strNumber));
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                int intAgentPrice = Convert.ToInt32(reader["AgentPrice"].ToString());
                string strArea_No = reader["Area_No"].ToString();
                string strGroup_Category_No = reader["Group_Category_No"].ToString();
                if (reader["Tick_Type"].ToString() == "A")
                {
                    //沒價格不鎖
                    //litPay0.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                    //沒價格要鎖
                    if (intAgentPrice == 0) { litPay0.Text = "不提供"; TextBox0.Enabled = false; break; }
                    //if (intAgentPrice <= 9000 && intAgentPrice > 0) { litPay0.Text = "來電洽詢"; TextBox0.Enabled = false; break; }
                    if (intAgentPrice > 9000)
                    {
                        litPay0.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                        TextBox0.Enabled = true;
                    }
                    else if (strGroup_Category_No == "86" && intAgentPrice > 3000) // 基隆起航，大於3000元就顯示
                    {
                        litPay0.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                        TextBox0.Enabled = true;
                    }
                    else if (strArea_No == "31" && intAgentPrice > 300) // 台灣館，大於300元就顯示
                    {
                        litPay0.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                        TextBox0.Enabled = true;
                    }
                    else
                    {
                        litPay0.Text = "來電洽詢"; TextBox0.Enabled = false; break;
                    }
                }
                else
                {
                    switch (reader["Bed_Type"].ToString().ToUpper())
                    {
                        case "1": // 小孩 佔床
                            //沒價格要鎖
                            if (intAgentPrice == 0) { litPay1.Text = "不提供"; TextBox1.Enabled = false; break; }
                            //if (intAgentPrice <= 9000 && intAgentPrice > 0) { litPay1.Text = "來電洽詢"; TextBox1.Enabled = false; break; }
                            if (intAgentPrice > 9000)
                            {
                                litPay1.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                                TextBox1.Enabled = true;
                            }
                            else if (strGroup_Category_No == "86" && intAgentPrice > 3000) // 基隆起航，大於3000元就顯示
                            {
                                litPay1.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                                TextBox1.Enabled = true;
                            }
                            else if (strArea_No == "31" && intAgentPrice > 300) // 台灣館，大於300元就顯示
                            {
                                litPay1.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                                TextBox1.Enabled = true;
                            }
                            else
                            {
                                litPay1.Text = "來電洽詢"; TextBox1.Enabled = false; break;
                            }
                            break;
                        case "2": // 小孩 不佔床
                            //沒價格要鎖
                            if (intAgentPrice == 0) { litPay3.Text = "不提供"; TextBox3.Enabled = false; break; }
                            //if (intAgentPrice <= 9000 && intAgentPrice > 0) { litPay3.Text = "來電洽詢"; TextBox3.Enabled = false; break; }
                            if (intAgentPrice > 9000)
                            {
                                litPay3.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                                TextBox3.Enabled = true;
                            }
                            else if (strGroup_Category_No == "86" && intAgentPrice > 3000) // 基隆起航，大於3000元就顯示
                            {
                                litPay3.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                                TextBox3.Enabled = true;
                            }
                            else if (strArea_No == "31" && intAgentPrice > 300) // 台灣館，大於300元就顯示
                            {
                                litPay3.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                                TextBox3.Enabled = true;
                            }
                            else
                            {
                                litPay3.Text = "來電洽詢"; TextBox3.Enabled = false; break;
                            }
                            break;
                        case "3": // 小孩 加床
                            //沒價格要鎖
                            if (intAgentPrice == 0) { litPay2.Text = "不提供"; TextBox2.Enabled = false; break; }
                            //if (intAgentPrice <= 9000 && intAgentPrice > 0) { litPay2.Text = "來電洽詢"; TextBox2.Enabled = false; break; }
                            if (intAgentPrice > 9000)
                            {
                                litPay2.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                                TextBox2.Enabled = true;
                            }
                            else if (strGroup_Category_No == "86" && intAgentPrice > 3000) // 基隆起航，大於3000元就顯示
                            {
                                litPay2.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                                TextBox2.Enabled = true;
                            }
                            else if (strArea_No == "31" && intAgentPrice > 300) // 台灣館，大於300元就顯示
                            {
                                litPay2.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                                TextBox2.Enabled = true;
                            }
                            else
                            {
                                litPay2.Text = "來電洽詢"; TextBox2.Enabled = false; break;
                            }
                            break;
                    }
                }
            }
            reader.Close();
            comm.Dispose();
        }
        finally
        {
            connect.Close();
        }
    }

    protected void btn_previous_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "history.go(-2);", true); return;
    }

    protected void btn_next_Click(object sender, EventArgs e)
    {
        int money0 = 0, money1 = 0, money2 = 0, money3 = 0;
        if (!string.IsNullOrEmpty(TextBox0.Text.Trim()))
        {
            try
            {
                if (TextBox0.Text.Trim().IndexOf("-") >= 0 || TextBox0.Text.Trim().IndexOf("+") >= 0)
                { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('大人人數輸入有誤。');", true); return; }
                money0 = Convert.ToInt16(TextBox0.Text.Trim());
            }
            catch { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('大人人數輸入有誤。');", true); return; }
        }
        if (!string.IsNullOrEmpty(TextBox1.Text.Trim()))
        {
            try
            {
                if (TextBox1.Text.Trim().IndexOf("-") >= 0 || TextBox1.Text.Trim().IndexOf("+") >= 0)
                { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('小孩佔床人數輸入有誤。');", true); return; }
                money1 = Convert.ToInt16(TextBox1.Text.Trim());
            }
            catch { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('小孩佔床人數輸入有誤。');", true); return; }
        }
        if (!string.IsNullOrEmpty(TextBox2.Text.Trim()))
        {
            try
            {
                if (TextBox2.Text.Trim().IndexOf("-") >= 0 || TextBox2.Text.Trim().IndexOf("+") >= 0)
                { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('小孩加床人數輸入有誤。');", true); return; }
                money2 = Convert.ToInt16(TextBox2.Text.Trim());
            }
            catch { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('小孩加床人數輸入有誤。');", true); return; }
        }
        if (!string.IsNullOrEmpty(TextBox3.Text.Trim()))
        {
            try
            {
                if (TextBox3.Text.Trim().IndexOf("-") >= 0 || TextBox3.Text.Trim().IndexOf("+") >= 0)
                { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('小孩不佔床人數輸入有誤。');", true); return; }
                money3 = Convert.ToInt16(TextBox3.Text.Trim());
            }
            catch { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('小孩不佔床人數輸入有誤。');", true); return; }
        }
        if (money0 + money1 + money2 + money3 <= 0)
        { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('報名人數至少一位。');", true); return; }

        if (!string.IsNullOrEmpty(Tai_Kao1.Text.Trim()))
        {
            try
            {
                if (Tai_Kao1.Text.Trim().IndexOf("-") >= 0 || Tai_Kao1.Text.Trim().IndexOf("+") >= 0)
                { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('高北單程人數輸入有誤。');", true); return; }
                money3 = Convert.ToInt16(Tai_Kao1.Text.Trim());
            }
            catch { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('高北單程人數輸入有誤。');", true); return; }
        }

        if (!string.IsNullOrEmpty(Tai_Kao2.Text.Trim()))
        {
            try
            {
                if (Tai_Kao2.Text.Trim().IndexOf("-") >= 0 || Tai_Kao2.Text.Trim().IndexOf("+") >= 0)
                { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('高北來回人數輸入有誤。');", true); return; }
                money3 = Convert.ToInt16(Tai_Kao2.Text.Trim());
            }
            catch { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('高北來回人數輸入有誤。');", true); return; }
        }

        Server.Transfer("~/OLApply/Apply3.aspx?n=" + Request["n"]);

    }
}