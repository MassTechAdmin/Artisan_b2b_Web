using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Apply2 : System.Web.UI.Page
{
    string CustIdno = "", CompNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
	clsFunction.Check.CheckSession();
        if (IsPostBack) return;
        Load_Data();
        DisplayMustPoint();
        DisplayRemainPoint();
        LoadCheckBoxSetting();
    }

    protected void Load_Data()
    {
        string strSql = "";
        strSql += " SELECT IND_FG_IDNo,IND_FG_Name,IND_FG_ID,IND_FG_Phone,IND_FG_EMail,IND_FG_CompNO";
        strSql += " ,AGT_NAME2,(TEL1_ZONE + TEL1) as TEL,name ,pager ,compno";
        strSql += " ,AGENT_L.Cont_Zone,AGENT_L.Cont_Tel";
        strSql += " FROM IND_FG ";
        strSql += " left join AGENT_M on IND_FG.IND_FG_CompNO = AGENT_M.COMP_NO ";
        strSql += " left join AGENT_L on IND_FG.IND_FG_IDNo = AGENT_L.AGT_IDNo ";
        strSql += " left join Person on AGENT_L.SALE_CODE = Person.perno ";
        strSql += " where IND_FG_ID = @IND_FG_ID ";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@IND_FG_ID", Session["PERNO"]));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                CustIdno = Convert.ToString(reader["IND_FG_IDNo"]);
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
                Lbl_Phone.Text = reader["IND_FG_Phone"].ToString();
                //會員姓名
                Lbl_Name.Text = reader["IND_FG_Name"].ToString();
                //電子郵件
                Lbl_EMail.Text = reader["IND_FG_EMail"].ToString();
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
        strSql += " FROM [Tour_Price]";
        strSql += " WHERE Number = @Number ";
        strSql += " ORDER BY [Sequ_No]";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@Number", strNumber));
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                int intAgentPrice = Convert.ToInt32(reader["AgentPrice"].ToString());
                if (reader["Tick_Type"].ToString() == "A")
                {
                    //沒價格不鎖
                    //litPay0.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                    //沒價格要鎖
                    if (intAgentPrice == 0) { litPay0.Text = "不提供"; TextBox0.Enabled = false; break; }
                    if (intAgentPrice <= 9000 && intAgentPrice > 0) { litPay0.Text = "來電洽詢"; TextBox0.Enabled = false; break; }
                    if (intAgentPrice > 9000)
                        litPay0.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                }
                else
                {
                    switch (reader["Bed_Type"].ToString().ToUpper())
                    {
                        case "1": // 小孩 佔床
                            //沒價格不鎖
                            //litPay1.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                            //沒價格要鎖
                            if (intAgentPrice == 0) { litPay1.Text = "不提供"; TextBox1.Enabled = false; break; }
                            if (intAgentPrice <= 9000 && intAgentPrice > 0) { litPay1.Text = "來電洽詢"; TextBox1.Enabled = false; break; }
                            if (intAgentPrice > 9000)
                                litPay1.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                            break;
                        case "2": // 小孩 不佔床
                            //沒價格不鎖
                            //litPay3.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                            //沒價格要鎖
                            if (intAgentPrice == 0) { litPay3.Text = "不提供"; TextBox3.Enabled = false; break; }
                            if (intAgentPrice <= 9000 && intAgentPrice > 0) { litPay3.Text = "來電洽詢"; TextBox3.Enabled = false; break; }
                            if (intAgentPrice > 9000)
                                litPay3.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                            break;
                        case "3": // 小孩 加床
                            //沒價格不鎖
                            //litPay2.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                            //沒價格要鎖
                            if (intAgentPrice == 0) { litPay2.Text = "不提供"; TextBox2.Enabled = false; break; }
                            if (intAgentPrice <= 9000 && intAgentPrice > 0) { litPay2.Text = "來電洽詢"; TextBox2.Enabled = false; break; }
                            if (intAgentPrice > 9000)
                                litPay2.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
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
            try {
                if (TextBox0.Text.Trim().IndexOf("-") >= 0 || TextBox0.Text.Trim().IndexOf("+") >= 0) 
                { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('大人人數輸入有誤。');", true); return; }
                money0 = Convert.ToInt16(TextBox0.Text.Trim()); 
            }
            catch { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('大人人數輸入有誤。');", true); return; }
        }
        if (!string.IsNullOrEmpty(TextBox1.Text.Trim())) 
        {
            try {
                if (TextBox1.Text.Trim().IndexOf("-") >= 0 || TextBox1.Text.Trim().IndexOf("+") >= 0) 
                { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('小孩佔床人數輸入有誤。');", true); return; } 
                money1 = Convert.ToInt16(TextBox1.Text.Trim());
            }
            catch { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('小孩佔床人數輸入有誤。');", true); return; }
        }
        if (!string.IsNullOrEmpty(TextBox2.Text.Trim())) 
        {
            try {
                if (TextBox2.Text.Trim().IndexOf("-") >= 0 || TextBox2.Text.Trim().IndexOf("+") >= 0) 
                { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('小孩加床人數輸入有誤。');", true); return; }
                money2 = Convert.ToInt16(TextBox2.Text.Trim()); 
            }
            catch { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('小孩加床人數輸入有誤。');", true); return; }
        }
        if (!string.IsNullOrEmpty(TextBox3.Text.Trim())) 
        {
            try {
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
        
        Server.Transfer("~/OLApply2/Apply3.aspx?n=" + Request["n"]);

    }

    protected void DisplayMustPoint()
    {
        try
        {

            MustPoint.Text = "";
            //string strsql = "", GropNumb="";
            //string connstr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ConnectionString;
            //SqlConnection conn = new SqlConnection(connstr);
            //conn.Open();
            //strsql += " SELECT TOP 1 Grop_Numb FROM [trip].[dbo].[Grop]";
            //strsql += " WHERE Van_Number=@VanNumber";
            //SqlCommand comm = new SqlCommand(strsql, conn);
            //comm.Parameters.Add(new SqlParameter("@VanNumber", Convert.ToString(Request["n"])));
            //SqlDataReader reader = comm.ExecuteReader();
            //if (reader.HasRows)
            //{
            //    while (reader.Read())
            //    {
            //        GropNumb=Convert.ToString(reader["Grop_Numb"]);
            //    }
            //}
            //reader.Close();
            //comm.Dispose();
            //conn.Close();

            string strsql2 = "";
            string connstr2 = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ConnectionString;
            SqlConnection conn2 = new SqlConnection(connstr2);
            conn2.Open();
            strsql2 += " SELECT TOP 1 MoneyPoint FROM [trip].[dbo].[Grop]";
            strsql2 += " WHERE Van_Number=@VanNumber";
            SqlCommand comm2 = new SqlCommand(strsql2, conn2);
            comm2.Parameters.Add(new SqlParameter("@VanNumber", Convert.ToString(Request["n"])));
            SqlDataReader reader2 = comm2.ExecuteReader();
            if (reader2.HasRows)
            {
                while (reader2.Read())
                {
                    MustPoint.Text = Convert.ToString(reader2["MoneyPoint"]);
                    txtMustPoint.Text = Convert.ToString(reader2["MoneyPoint"]);
                }
            }
            else
            {
                MustPoint.Text = "0";
            }
            reader2.Close();
            comm2.Dispose();
            conn2.Close();



        }
        catch (Exception ex)
        {

        }
    }

    protected void DisplayRemainPoint()
    {
        try
        {
            RemainPoint.Text = "";
            string strsql3 = "";
            string connstr3 = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ConnectionString;
            SqlConnection conn3 = new SqlConnection(connstr3);
            conn3.Open();
            strsql3 += " SELECT TOP 1 point_balance FROM Point_Recode";
            //strsql3 += " WHERE COMP_NO=@CompNo AND Cust_Idno=@CustIdno";
            strsql3 += " WHERE Cust_Idno=@CustIdno";
            strsql3 += " ORDER BY crea_date DESC";
            SqlCommand comm3 = new SqlCommand(strsql3, conn3);
            //comm3.Parameters.Add(new SqlParameter("@CompNo", CompNo));
            comm3.Parameters.Add(new SqlParameter("@CustIdno", CustIdno));
            SqlDataReader reader3 = comm3.ExecuteReader();
            if (reader3.HasRows)
            {
                while (reader3.Read())
                {
                    RemainPoint.Text = Convert.ToString(reader3["point_balance"]);
                    txtRemainPoint.Text = Convert.ToString(reader3["point_balance"]);
                }
            }
            else
            {
                RemainPoint.Text = "0";
            }
            reader3.Close();
            comm3.Dispose();
            conn3.Close();
            Session["BeforeRemainPoint"] = Convert.ToInt32(RemainPoint.Text);
        }
        catch (Exception ex)
        { }
    }
    protected void LoadCheckBoxSetting()
    {
       
        ConfirmUsePoint.Text = "紅利積點要折抵此項團費：";
        
        if (RemainPoint.Text == "0")
        {
            ConfirmUsePoint.Enabled = false;
            
        }
        if (litPay0.Text == "不提供")
        {

            if (litPay1.Text == "不提供")
            {

                if (litPay2.Text == "不提供")
                {

                    if (litPay3.Text == "不提供")
                    {
                        ConfirmUsePoint.Enabled = false;
                    }
                }
            }
        }
    }
    protected void ConfirmUsePoint_CheckedChanged(object sender, EventArgs e)
    {
        if (ConfirmUsePoint.Checked == true)
        {
            txtConfirmUsePoint.Text = "1";
        }
        else
        {
            txtConfirmUsePoint.Text = "";
        }
    }
}