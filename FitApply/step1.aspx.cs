using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.WebControls;

public partial class step1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsFunction.Check.CheckSession();

        if (IsPostBack) return;
        fn_Read_Data_Trip();
        fn_Read_Data_Agent();
    }

    protected void fn_Read_Data_Trip()
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
            cmd.Parameters.Add(new SqlParameter("@Trip_No", Convert.ToString(Request["n"])));
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Lbl_Grop_Name.Text += reader["Group_Name"].ToString();
            }
            reader.Close();
            cmd.Dispose();
        }
        catch { }
        finally { connect.Close(); }
    }

    protected void fn_Read_Data_Agent()
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        int money1 = 0, money2 = 0, money3 = 0, money4 = 0, money5 = 0;

        if (!string.IsNullOrEmpty(txt1.Text.Trim()))
        {
            try
            {
                if (txt1.Text.Trim().IndexOf("-") >= 0 || txt1.Text.Trim().IndexOf("+") >= 0)
                { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('大人人數輸入有誤。');", true); return; }
                money1 = Convert.ToInt16(txt1.Text.Trim());
            }
            catch { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('大人人數輸入有誤。');", true); return; }
        }
        if (!string.IsNullOrEmpty(txt2.Text.Trim()))
        {
            try
            {
                if (txt2.Text.Trim().IndexOf("-") >= 0 || txt2.Text.Trim().IndexOf("+") >= 0)
                { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('小孩佔床人數輸入有誤。');", true); return; }
                money2 = Convert.ToInt16(txt2.Text.Trim());
            }
            catch { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('小孩佔床人數輸入有誤。');", true); return; }
        }
        if (!string.IsNullOrEmpty(txt3.Text.Trim()))
        {
            try
            {
                if (txt3.Text.Trim().IndexOf("-") >= 0 || txt3.Text.Trim().IndexOf("+") >= 0)
                { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('小孩不佔床人數輸入有誤。');", true); return; }
                money3 = Convert.ToInt16(txt3.Text.Trim());
            }
            catch { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('小孩不佔床人數輸入有誤。');", true); return; }
        }
        if (!string.IsNullOrEmpty(txt4.Text.Trim()))
        {
            try
            {
                if (txt4.Text.Trim().IndexOf("-") >= 0 || txt4.Text.Trim().IndexOf("+") >= 0)
                { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('小孩加床人數輸入有誤。');", true); return; }
                money4 = Convert.ToInt16(txt4.Text.Trim());
            }
            catch { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('小孩加床人數輸入有誤。');", true); return; }
        }
        if (!string.IsNullOrEmpty(txt5.Text.Trim()))
        {
            try
            {
                if (txt5.Text.Trim().IndexOf("-") >= 0 || txt5.Text.Trim().IndexOf("+") >= 0)
                { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('嬰兒人數輸入有誤。');", true); return; }
                money5 = Convert.ToInt16(txt5.Text.Trim());
            }
            catch { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('嬰兒人數輸入有誤。');", true); return; }
        }

        if (money1 == 0) { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('至少一位大人。');", true); return; }
        if (money1 + money2 + money3 + money4 + money5 <= 0)
        { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('報名人數至少一位。');", true); return; }


        if (CheckBox1.Checked)
        {
            Server.Transfer("~/FitApply/step2.aspx?n=" + Request["n"]);
            //if (DropDownList1.SelectedValue == "A" && DropDownList2.SelectedIndex == 0)
            //{
            //    Random rnd = new Random();
            //    for (int i = 0; i < 10; i++)
            //    {
            //        int MinValue = 1;
            //        int MaxValue = DropDownList2.Items.Count;
            //        int SetValue = rnd.Next(MinValue, MaxValue);
            //        DropDownList2.SelectedValue = DropDownList2.Items[SetValue].Value;
            //    }
            //}

            //StringBuilder sbHtml = new StringBuilder();
            //sbHtml.Append("<form id='form1' name='form1' action='step2.aspx' method='post'>");
            //sbHtml.Append("<input type='hidden' name='n' value='" + Request["n"] + "'/>");
            //sbHtml.Append("<input type='hidden' name='comp' value='" + DropDownList1.SelectedValue + "'/>");
            //sbHtml.Append("<input type='hidden' name='sales' value='" + DropDownList2.SelectedValue + "'/>");

            //sbHtml.Append("<input type='hidden' name='t1' value='" + txt1.Text.Trim() + "'/>"); //人數
            //sbHtml.Append("<input type='hidden' name='t2' value='" + txt2.Text.Trim() + "'/>");
            //sbHtml.Append("<input type='hidden' name='t3' value='" + txt3.Text.Trim() + "'/>");
            //sbHtml.Append("<input type='hidden' name='t4' value='" + txt4.Text.Trim() + "'/>");
            //sbHtml.Append("<input type='hidden' name='t5' value='" + txt5.Text.Trim() + "'/>");

            //sbHtml.Append("<input type='submit=Submit' style='display:none;'></form>");
            //sbHtml.Append("<script>document.forms['form1'].submit();</script>");
            //Response.Write(sbHtml);
            //Response.End();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('請閱讀「國外團體旅遊定型契約書」、「團體報名須知」，並打勾同意其內容。');", true);
            return;
        }
    }
}