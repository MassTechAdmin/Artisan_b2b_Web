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
using System.Collections.Generic;


public partial class order_list : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Counter.Counter.fn_Account();
        clsFunction.Check.CheckSession();
        if (!IsPostBack)
        {
            DateTime dt = DateTime.Now;

            datepicker2.Text = dt.ToString ("yyyy/MM/dd");
            datepicker1.Text = DateTime.Now.AddMonths(-3).ToString("yyyy/MM/dd");
            getdata();
            ViewState["SortExpression"] = "EnliI_Date"; //設定預設的排序欄位
            ViewState["SortDirection"] = SortDirection.Ascending;//設定預設的排序方式 
        }
        
    }

    #region === GridView 內容 ===
    private void getdata()
    {
        string strSql = "";
        strSql += "  SELECT Tour_Numb,Remark,a.Grop_Name,a.Grop_Depa,a.Crea_Date";
        strSql += " ,TR10_OL.crea_user,Comp_Conn,a.Down_Payment";
        strSql += " ,a.Grop_Number,a.Grop_Pdf,a.Trip_No,a.Grop_Liner,a.Area_Code";
        strSql += " ,Reg_Status,ISNULL(TR10_OL.BookPax,0) as BookPax,TR10_OL.netcustnumb,TourFee";
        strSql += " ,SUM(case when  TR20_OL.BookPax <> '0' then TR20_OL.Tour_Mony*TR20_OL.BookPax  else '0' end) AS total  ,EnliI_Date";
        strSql += " ,VAN_HK,VAN_RQ,VAN_Cancel";
        strSql += " FROM TR10_OL";
        strSql += " LEFT JOIN trip.dbo.grop as a on TR10_OL.Tour_Numb = a.Grop_Numb";
        strSql += " left join TR20_OL on TR20_OL.Tr10Number = TR10_OL.Number";
        strSql += " Where TR10_OL.crea_user = @crea_user";
        strSql += " and TR10_OL.enli_code='2'";
        strSql += " and TR10_OL.EnliI_Date >= @grop_Depa1";
        strSql += " and TR10_OL.EnliI_Date <= @grop_Depa2";

        if (DropDownList1.SelectedValue != "全選")
        {
            strSql += " and reg_status = @reg_status";
        }
        strSql += " GROUP BY Tour_Numb,Remark,a.Grop_Name,a.Grop_Depa,a.Crea_Date ,TR10_OL.crea_user,Comp_Conn,a.Down_Payment ";
        strSql += " ,a.Grop_Number,a.Grop_Pdf,a.Trip_No,a.Grop_Liner,a.Area_Code ";
        strSql += " ,Reg_Status,TR10_OL.BookPax,TR10_OL.netcustnumb,TourFee,EnliI_Date ";
        strSql += " ,VAN_HK,VAN_RQ,VAN_Cancel";
        strSql += " order by enliI_date";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlDataAdapter da = new SqlDataAdapter(strSql, strConnString);
        da.SelectCommand.Parameters.Add(new SqlParameter("@crea_user", Session["PERNO"]));
        da.SelectCommand.Parameters.Add(new SqlParameter("@grop_Depa1", datepicker1.Text.ToString() + " 00:00:00"));
        da.SelectCommand.Parameters.Add(new SqlParameter("@grop_Depa2", datepicker2.Text.ToString() + " 23:59:59"));
        da.SelectCommand.Parameters.Add(new SqlParameter("@reg_status", DropDownList1.SelectedValue));
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.DataSource = dt;
        //GridView1.DataKeyNames = "Van_Number";
        GridView1.DataBind();
        connect.Close();

    }
    protected string small_total(string strTourFee, string strBookPax) {
        int total = 0;
        if (strBookPax != "")
        {
             total = int.Parse(strTourFee) * int.Parse(strBookPax);
           
        }
        return total.ToString();
    }
     
    protected string fn_RtnGridViewGrop_Name(string strArea_No, string strGrop_Pdf, string strTripNo, string strDate, string strType)
    {
        if (string.IsNullOrEmpty(strGrop_Pdf))
        {
            ////針對北歐行程(版型不同) 2011/12/29
            //if (Convert.ToInt32(strArea_No) == 6)
            //{
            //    Response.Write("<a onclick=\"gotripnoSP('" + reader2.GetValue(11).ToString() + "','" + reader2.GetValue(4).ToString() + "','" + Convert.ToDateTime(reader2.GetValue(2).ToString()).Year + "/" + Convert.ToDateTime(reader2.GetValue(2).ToString()).Month.ToString("00") + "/" + Convert.ToDateTime(reader2.GetValue(2).ToString()).Day.ToString("00") + "');\" style=\"cursor:pointer;cursor:hand; \" ><span class=\"styleorange\">" + reader2.GetValue(1).ToString() + "</spqn></a>");
            //}
            //else
            //{
            //    Response.Write("<a onclick=\"gotripno('" + reader2.GetValue(11).ToString() + "','" + reader2.GetValue(4).ToString() + "','" + Convert.ToDateTime(reader2.GetValue(2).ToString()).Year + "/" + Convert.ToDateTime(reader2.GetValue(2).ToString()).Month.ToString("00") + "/" + Convert.ToDateTime(reader2.GetValue(2).ToString()).Day.ToString("00") + "');\" style=\"cursor:pointer;cursor:hand; \" ><span class=\"styleorange\">" + reader2.GetValue(1).ToString() + "</spqn></a>");
            //}
            return "TripList.aspx?TripNo=" + strTripNo + "&Date=" + strDate + "&type=" + strType;
            //"<a href =http://www.artisan.com.tw/GropPDF/" + reader2.GetValue(17) + " target='_blank' ><span class=\"styleorange\">" + reader2.GetValue(1).ToString() + "</span></a>";
        }
        else
        {
            //return "<a href =http://www.artisan.com.tw/GropPDF/" + reader2.GetValue(17) + " target='_blank' ><span class=\"styleorange\">" + reader2.GetValue(1).ToString() + "</span></a>";
            return "http://www.artisan.com.tw/GropPDF/" + strGrop_Pdf;
        }
    }
    protected string fn_RtnGridViewGroup_Name(string strGrop_Name, string strGroup_Name)
    {
        if (string.IsNullOrEmpty(strGroup_Name))
        { return strGrop_Name; }

        return strGroup_Name;
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
        return Convert.ToDateTime(strCrea_Date).AddDays(WorkDay_Desposit).ToString("yyyy/MM/dd");

    }
    protected string check_reg(string strReg_Status, string strBookPax, string strVAN_HK, string strVAN_RQ, string strVAN_Cancel)
    {
        string strMsg = "";
        // 可收訂
        if (strVAN_HK != "0")
        { strMsg += (strMsg == "" ? "" : ",") + strVAN_HK + "人HK"; }
        // 候補
        if (strVAN_RQ != "0")
        { strMsg += (strMsg == "" ? "" : ",") + strVAN_RQ + "人RQ"; }
        // 刪除
        if (strVAN_Cancel != "0")
        { strMsg += (strMsg == "" ? "" : ",") + strVAN_Cancel + "人取消"; }

        // 若找不到資料時，傳RQ
        if (strMsg == "")
        { strMsg = "RQ機位候補"; }

        return strMsg;

        //if (strBookPax)
        //{ return "RQ機位候補"; }


        //if (strReg_Status == "HK")
        //{
        //    return strReg_Status.ToString() + "機位確認";
        //}
        //else
        //{
        //    return "RQ機位候補";
        //}
    }
    protected string fn_RtnIsHoliday(System.DateTime dtTemp)
    {
        string strHoliday = "";
        string strSql = "";
        strSql += " SELECT [Holiday] FROM [Holiday]";
        strSql += " WHERE Holiday = @Holiday";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        System.Data.SqlClient.SqlConnection connect = new System.Data.SqlClient.SqlConnection(strConnString);
        connect.Open();
        System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(strSql, connect);
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

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        getdata(); //取資料   
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        getdata();
        GridViewSortExpression = e.SortExpression;
        int pageIndex = GridView1.PageIndex;
        GridView1.DataBind();
        GridView1.PageIndex = pageIndex;
    }
    private string GridViewSortExpression
    {
        get { return (ViewState["GridViewSortExpression"] as string == null ? string.Empty : ViewState["GridViewSortExpression"] as string); }
        set { ViewState["GridViewSortExpression"] = value; }
    }
    private string GridViewSortDirection
    {
        get { return (ViewState["SortDirection"] as string == null ? "ASC" : ViewState["SortDirection"] as string); }
        set { ViewState["SortDirection"] = value; }
    }

    private string GetSortDirection()
    {
        switch ((GridViewSortDirection))
        {
            case "ASC":
                GridViewSortDirection = "DESC";
                break;
            case "DESC":
                GridViewSortDirection = "ASC";
                break;
        }
        return GridViewSortDirection;
    }
    #endregion

    #region === Control ===
    protected void Button1_Click(object sender, EventArgs e)
    {
        getdata();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("onsalep.aspx");
    }
    #endregion

}
