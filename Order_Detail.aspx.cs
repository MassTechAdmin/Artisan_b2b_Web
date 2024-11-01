using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class Order_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) { return; }
        fn_GetData();
    }
    #region === GridView 內容 ===
    protected void fn_GetData()
    {
        String strSql = "";
        strSql += " SELECT Tour_Numb,Remark,a.Grop_Name,a.Grop_Depa,a.Crea_Date";
        strSql += " ,TR10_OL.crea_user,Comp_Conn,a.Down_Payment";
        strSql += " ,a.Grop_Number,a.Grop_Pdf,a.Trip_No,a.Grop_Liner,a.Area_Code";
        strSql += " ,b.Tour_mony,Reg_Status,ISNULL(b.BookPax,0) as BookPax,TR10_OL.netcustnumb,TourFee";
        strSql += " ,b.Bed_Type,b.Tick_Type";
        strSql += " FROM TR10_OL";
        strSql += " LEFT JOIN trip.dbo.grop as a on TR10_OL.Tour_Numb = a.Grop_Numb";
        strSql += " LEFT JOIN B2B.dbo.TR20_OL as b on TR10_OL.Number = b.tr10Number";
        strSql += " Where TR10_OL.crea_user = @crea_user";
        strSql += " and TR10_OL.netcustnumb = @netcustnumb";
        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(constring);
        try
        {
            connect.Open();

            SqlCommand command = new SqlCommand(strSql, connect);
            command.Parameters.Add(new SqlParameter("@crea_user", Session["PERNO"]));
            command.Parameters.Add(new SqlParameter("@netcustnumb", Request["no"]));
             SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Label1.Text = reader["netcustnumb"].ToString().Trim();
                Label2.Text = reader["Comp_Conn"].ToString().Trim();
                if (reader["Reg_Status"].ToString()  == "HK")
                {
                    Label3.Text = "HK 機位確認";
                }
                else
                {
                    Label3.Text = "RQ 機位候補";
                }
                Label4.Text = reader["Trip_no"].ToString().Trim();
                Label5.Text = reader["Grop_Depa"].ToString().Trim();
                Label6.Text = reader["Grop_Name"].ToString().Trim();
                Label11.Text = Down_pay_time(reader["Crea_Date"].ToString().Trim());
               if (reader["Tick_Type"].ToString().Trim() == "A" )
               {   Label12.Text = reader["Tour_mony"].ToString().Trim();
                   Label13.Text = reader["Tour_mony"].ToString().Trim();
                   Label14.Text = reader["BookPax"].ToString().Trim();
                   int x = int.Parse(Label13.Text) * int.Parse(Label14.Text);
                   Label15.Text = x.ToString();
                }
               else if (reader["Tick_Type"].ToString().Trim() == "C" && reader["Bed_Type"].ToString().Trim()== "1")
               {
                   Label16.Text = reader["Tour_mony"].ToString().Trim();
                   Label17.Text = reader["Tour_mony"].ToString().Trim();
                   Label18.Text = reader["BookPax"].ToString().Trim();
                   int x = int.Parse(Label17.Text) * int.Parse(Label18.Text);
                   Label19.Text = x.ToString();
               }
               else if (reader["Tick_Type"].ToString().Trim() == "C" && reader["Bed_Type"].ToString().Trim() == "2")
               {
                   Label20.Text = reader["Tour_mony"].ToString().Trim();
                   Label21.Text = reader["Tour_mony"].ToString().Trim();
                   Label22.Text = reader["BookPax"].ToString().Trim();
                   int x = int.Parse(Label21.Text) * int.Parse(Label22.Text);
                   Label23.Text = x.ToString();
               }
               else if (reader["Tick_Type"].ToString().Trim() == "C" && reader["Bed_Type"].ToString().Trim() == "3")
               {
                   Label24.Text = reader["Tour_mony"].ToString().Trim();
                   Label25.Text = reader["Tour_mony"].ToString().Trim();
                   Label26.Text = reader["BookPax"].ToString().Trim();
                   int x = int.Parse(Label25.Text) * int.Parse(Label26.Text);
                   Label27.Text = x.ToString();
               }
               else if(reader["Tick_Type"].ToString().Trim() == "I" )
               {
                   Label28.Text = reader["Tour_mony"].ToString().Trim();
                   Label29.Text = reader["Tour_mony"].ToString().Trim();
                   Label30.Text = reader["BookPax"].ToString().Trim();
                   int x = int.Parse(Label29.Text) * int.Parse(Label30.Text);
                   Label31.Text = x.ToString();
               }
               int y = int.Parse(Label15.Text) + int.Parse(Label19.Text) + int.Parse(Label23.Text) + int.Parse(Label27.Text) + int.Parse(Label31.Text);
               Label37.Text = y.ToString().Trim();
               Label7.Text  = Label14.Text;
               Label38.Text = Label18.Text;
               Label39.Text = Label22.Text;
               Label40.Text = Label26.Text;
               Label41.Text = Label30.Text;
               int z = int.Parse(Label7.Text) + int.Parse(Label38.Text) + int.Parse(Label39.Text) + int.Parse(Label40.Text) + int.Parse(Label41.Text);
               Label8.Text = z.ToString();
               Label9.Text = reader["Down_Payment"].ToString().Trim();
               int i = z * int.Parse(Label9.Text);
               Label10.Text = i.ToString();
               Label42.Text = Label11.Text;
            }
            reader.Close();
            command.Dispose();
        }
        finally
        {
            connect.Close();
        }
        connect.Close();
    }
    protected string fn_RtnIsHoliday(System.DateTime dtTemp)
    {
        string strHoliday = "";
        string strSql = "";
        strSql += " SELECT [Holiday] FROM [Holiday]";
        strSql += " WHERE Holiday = @Holiday";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand command = new SqlCommand(strSql, connect);
        command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Holiday", dtTemp));
        System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            strHoliday = Convert.ToDateTime(reader["Holiday"].ToString()).ToString("yyyy/MM/dd");
        }
        reader.Close();
        command.Dispose();
        connect.Close();

        return strHoliday;
    }
    protected string Down_pay_time(string strCrea_Date)
    {


        int WorkDay_Desposit = 0;
        int WorkDay = 0;
        System.DateTime dtToday = default(System.DateTime);
        //訂金最後期限 / 機位保留期限
        while (WorkDay < 2)
        {
            WorkDay_Desposit += 1;
            dtToday = Convert.ToDateTime(strCrea_Date).AddDays(WorkDay_Desposit);
            if (string.IsNullOrEmpty(fn_RtnIsHoliday(dtToday)))
            {
                WorkDay += 1;
            }
        }
        return Convert.ToDateTime(strCrea_Date).AddDays(WorkDay).ToString("yyyy/MM/dd");


    }
    #endregion
    protected void Button2_Click(object sender, EventArgs e)
    {
       
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("order_list.aspx");
    }
}