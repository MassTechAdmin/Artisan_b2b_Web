using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class OLApply_Apply1 : System.Web.UI.Page
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
        //strSql += " SELECT top 1 Van_Number ,Grop_Name ,Aire_StartConutry ,Grop_Depa ,Grop_Expect ,Grop_Tour ,Agent_Tour ,g.Grop_Numb ";
        //strSql += " ,(Grop_Expect - Reg_Ok) as Reg ,Grop_Intro ,Grop_Visa ,Grop_Tax ,Down_Payment";
        //strSql += " FROM grop g ";
        //strSql += " left join Aire a on g.Grop_Numb = a.Grop_Numb ";
        //strSql += " where g.Van_Number = @Van_Number ";
        //strSql += " and Aire_Journey = 1 ";
        //strSql += " and Aire_Type = 1 ";
        //strSql += " order by Aire_Numb ";
        strSql += " SELECT Van_Number ,Grop_Name ,Grop_Depa ,Grop_Expect ,Grop_Tour ,Agent_Tour ,Grop_Numb ";
        strSql += " ,(Grop_Expect - Reg_Ok) as Reg ,Grop_Intro ,Grop_Visa ,Grop_Tax ,Down_Payment ";
        strSql += " ,Group_Name";
        strSql += " FROM grop ";
        strSql += " left join Group_Name on Group_Name.Group_Name_No = Grop.Group_Name_No";
        strSql += " where Van_Number = @Van_Number ";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@Van_Number", Request["n"]));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                //團名
                Lbl_Grop_Name.Text = reader["Group_Name"].ToString();
                //團號
                Lbl_Grop_Numb.Text = reader["Grop_Numb"].ToString();
                //出發日期
                Lbl_Grop_Depa.Text = Convert.ToDateTime(reader["Grop_Depa"].ToString()).ToString("yyyy/MM/dd");
                string strDayOfWeek = "";
                switch (Convert.ToInt16(Convert.ToDateTime(reader["Grop_Depa"].ToString()).DayOfWeek))
                {
                    case 0:
                        strDayOfWeek = " (日)";
                        break;
                    case 1:
                        strDayOfWeek = " (一)";
                        break;
                    case 2:
                        strDayOfWeek = " (二)";
                        break;
                    case 3:
                        strDayOfWeek = " (三)";
                        break;
                    case 4:
                        strDayOfWeek = " (四)";
                        break;
                    case 5:
                        strDayOfWeek = " (五)";
                        break;
                    case 6:
                        strDayOfWeek = " (六)";
                        break;
                }
                Lbl_Grop_Depa.Text += strDayOfWeek;
                //直售團費
                //沒價格不鎖
                //Lbl_Grop_Tour.Text = Convert.ToInt32(reader["Grop_Tour"].ToString()).ToString("#,0");
                //沒價格要鎖
                if (Convert.ToInt32(reader["Grop_Tour"].ToString()) > 9000) { Lbl_Grop_Tour.Text = Convert.ToInt32(reader["Grop_Tour"].ToString()).ToString("#,0"); }
                else if (Convert.ToInt16(reader["Grop_Tour"].ToString()) == 0) { Lbl_Grop_Tour.Text = ""; }
                else if (Convert.ToInt16(reader["Grop_Tour"].ToString()) == 1) { Lbl_Grop_Tour.Text = "請來電洽詢"; }
                //同業團費
                //沒價格不鎖
                //Lbl_Agent_Tour.Text = Convert.ToInt32(reader["Agent_Tour"].ToString()).ToString("#,0");
                //沒價格要鎖
                if (Convert.ToInt32(reader["Agent_Tour"].ToString()) > 9000) { Lbl_Agent_Tour.Text = Convert.ToInt32(reader["Agent_Tour"].ToString()).ToString("#,0"); }
                else if (Convert.ToInt16(reader["Agent_Tour"].ToString()) == 0) { Lbl_Agent_Tour.Text = ""; }
                else if (Convert.ToInt16(reader["Agent_Tour"].ToString()) == 1) { Lbl_Agent_Tour.Text = "請來電洽詢"; }
                //稅金
                if (Convert.ToInt16(reader["Grop_Tax"].ToString()) == 0) { Lbl_Grop_Tax.Text = "團費內含稅金"; }
                else if (Convert.ToInt16(reader["Grop_Tax"].ToString()) == 1) { Lbl_Grop_Tax.Text = "團費不含稅金"; }
                //簽證
                if (Convert.ToInt16(reader["Grop_Visa"].ToString()) == 0)
                {
                    Lbl_Grop_Visa.Text = "團費內含簽證費";
                    Lbl_Grop_Visa_Info.Text = "內含簽證費";
                }
                else if (Convert.ToInt16(reader["Grop_Visa"].ToString()) == 1)
                {
                    Lbl_Grop_Visa.Text = "團費不含簽證費";
                    Lbl_Grop_Visa_Info.Text = "不含簽證費";
                }
                else if (Convert.ToInt16(reader["Grop_Visa"].ToString()) == 2)
                {
                    Lbl_Grop_Visa.Text = "免簽證";
                    Lbl_Grop_Visa_Info.Text = "免簽證";
                }
                //訂金
                Lbl_Down_Payment.Text = Convert.ToInt32(reader["Down_Payment"].ToString()).ToString("#,0");
                //小孩報價
                fn_Show_Tour_Price(reader["Van_Number"].ToString());
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

    protected void fn_Show_Tour_Price(string strNumber)
    {
        string strSql = "";
        strSql = " SELECT [Number],[Sequ_No],[Tick_Type],[Tour_Type],[Bed_Type]";
        strSql += " ,[Cruises_Type],IsNull([SalePrice],0) AS SalePrice,IsNull([AgentPrice],0) AS AgentPrice";
        strSql += " FROM [Tour_Price]";
        strSql += " WHERE Number = @Number and Tick_Type = 'C'";
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
                switch (reader["Bed_Type"].ToString().ToUpper())
                {
                    case "1": // 小孩 佔床
                        //沒價格不鎖
                        //litPay1.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                        //沒價格要鎖
                        if (intAgentPrice == 0) { litPay1.Text = "不提供"; break; }
                        if (intAgentPrice <= 9000 && intAgentPrice > 0) { litPay1.Text = "來電洽詢"; break; }
                        if (intAgentPrice > 9000)
                            litPay1.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                        break;
                    case "2": // 小孩 不佔床
                        //沒價格不鎖
                        //litPay3.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                        //沒價格要鎖
                        if (intAgentPrice == 0) { litPay3.Text = "不提供"; break; }
                        if (intAgentPrice <= 9000 && intAgentPrice > 0) { litPay3.Text = "來電洽詢"; break; }
                        if (intAgentPrice > 9000)
                            litPay3.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                        break;
                    case "3": // 小孩 加床
                        //沒價格不鎖
                        //litPay2.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                        //沒價格要鎖
                        if (intAgentPrice == 0) { litPay2.Text = "不提供"; break; }
                        if (intAgentPrice <= 9000 && intAgentPrice > 0) { litPay2.Text = "來電洽詢"; break; }
                        if (intAgentPrice > 9000)
                            litPay2.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                        break;
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

    protected void button_Click(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true) { Response.Redirect("~/OLApply2/Apply2.aspx?n=" + Request["n"]); }  //Server.Transfer("~/OLApply2/Apply2.aspx")
        else
        { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('請先閱讀報名須知，並同意其內容。');", true); return; }
    }
}