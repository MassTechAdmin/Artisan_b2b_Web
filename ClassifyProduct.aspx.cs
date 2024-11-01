using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public partial class ClassifyProduct : System.Web.UI.Page
{
    int nowPage = 1;        //現在分頁
    int strlength = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //aaa.Value = DateTime.Today.ToString("yyyy-MM-dd");
	//Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('"+ aaa.Value +"');", true);
        clsFunction.Check.CheckSession();
        if (!IsPostBack)
        {
            Counter.Counter.fn_Account();

            if (Convert.ToString(Request["RadDatePicker1"] + "") == "")
            { rdpDate.Value = DateTime.Today.ToString("yyyy-MM-dd"); }
            else { rdpDate.Value = Convert.ToDateTime(Request["RadDatePicker1"]).ToString("yyyy-MM-dd"); }

            if (Convert.ToString(Request["RadDatePicker2"] + "") == "")
            {
                if (Request["area"] == "7")
                { rdpDate2.Value = DateTime.Today.AddMonths(16).ToString("yyyy-MM-dd"); }
                else if (Request["area"] == "6")
                { rdpDate2.Value = DateTime.Today.AddMonths(9).ToString("yyyy-MM-dd"); }
                else
                { rdpDate2.Value = DateTime.Today.AddYears(1).AddMonths(6).ToString("yyyy-MM-dd"); }
            }
            else { rdpDate2.Value = Convert.ToDateTime(Request["RadDatePicker2"]).ToString("yyyy-MM-dd"); }

            if (Request["area_no"] == "11" && Request["sgcn"] == "76" && DateTime.Today.AddMonths(6) <= new DateTime(2020, 12, 31))
            { rdpDate2.Value = new DateTime(2020, 12, 31).ToString("yyyy-MM-dd"); }

            if (Session["orderby"] != null) { RadioButtonList1.SelectedValue = Session["orderby"].ToString(); Session["orderby"] = null; }


            hidOrder.Value = RadioButtonList1.SelectedValue;

            fn_DropDownList_Area();
            fn_DropDownList_Area2();

            fn_Show_Data();
            pagination();
        }
    }

    #region === 搜尋選項 ===
    /// <summary>
    /// 判斷地區編號
    /// </summary>
    /// <returns></returns>
    protected void fn_GetTripArea()
    {
        string strTripNo = Convert.ToString(Request["TripNo"] + "");

        string strSql = "";
        strSql = "select trip_no,area_no from trip";
        strSql += " where trip_no = @trip_no";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            if (!string.IsNullOrEmpty(strTripNo))
                comm.Parameters.Add(new SqlParameter("@trip_no", strTripNo));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                if (reader["area_no"].ToString() == "7")
                {
                    rdpDate.Value = DateTime.Today.ToString("yyyy-MM-dd");
                    rdpDate2.Value = DateTime.Today.AddYears(2).ToString("yyyy-MM-dd");
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
    protected void fn_DropDownList_Area()
    {
        string strSql = "";
        strSql += " SELECT [Area_No], [Area_Name] FROM [Area] ";
        strSql += " ORDER BY [Area_No]";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlDataAdapter da = new SqlDataAdapter(strSql, strConnString);
        DataTable dt = new DataTable();
        da.Fill(dt);
        DropDownList1.DataSource = dt;
        DropDownList1.DataValueField = "Area_No";
        DropDownList1.DataTextField = "Area_Name";
        DropDownList1.DataBind();
        connect.Close();

        DropDownList1.Items.Insert(0, new ListItem("(區域或主題)", "0"));
        DropDownList1.SelectedIndex = 0;
    }

    protected void fn_DropDownList_Area2()
    {
        string strSql = "";
        strSql += " select Group_Category_No,Group_Category_Name,Glb_Id from Group_Category";
        strSql += " LEFT JOIN Area ON Area_Id=Glb_Id";
        strSql += " where Area_No = '" + DropDownList1.SelectedValue + "'";
        strSql += " and MultiCountry <> 0";
        strSql += " order by GC_OrderBy";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlDataAdapter da = new SqlDataAdapter(strSql, strConnString);
        DataTable dt = new DataTable();
        da.Fill(dt);
        DropDownList2.DataSource = dt;
        DropDownList2.DataValueField = "Group_Category_No";
        DropDownList2.DataTextField = "Group_Category_Name";
        DropDownList2.DataBind();
        connect.Close();

        DropDownList2.Items.Insert(0, new ListItem("(國家或主題)", "0"));
        DropDownList2.SelectedIndex = 0;
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fn_DropDownList_Area2();
    }
    // 團體旅遊
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string strOutDate = "";
        string strOutDate2 = "";
        string strArea = "";
        string strCountry = "";
        string tt = "";
        string Url = "ClassifyProduct.aspx?l=l";

        if (Convert.ToString(rdpDate.Value) != null)
        {
            if (Convert.ToDateTime(rdpDate.Value) >= new DateTime(2012, 1, 1))
            {
                strOutDate = Convert.ToString(rdpDate.Value);
                Url += "&RadDatePicker1=" + strOutDate;
            }
        }

        if (Convert.ToString(rdpDate2.Value) != null)
        {
            if (Convert.ToDateTime(rdpDate2.Value) >= new DateTime(2012, 1, 1))
            {
                strOutDate2 = Convert.ToString(rdpDate2.Value);
                Url += "&RadDatePicker2=" + strOutDate2;
            }
        }
        if (DropDownList1.SelectedValue != null && DropDownList1.SelectedIndex != 0)
        {
            strArea = DropDownList1.SelectedValue;
            Url += "&area=" + strArea;
        }

        if (DropDownList2.SelectedValue != "" && DropDownList2.SelectedIndex != 0)
        {
            strCountry = DropDownList2.SelectedValue;
            Url += "&sgcn=" + strCountry;
        }
        if (!string.IsNullOrEmpty(txbKey.Text.Trim()))
        {
            tt = HttpUtility.UrlEncode(txbKey.Text.Trim().Replace("'", ""));
            Url += "&tp=" + tt;
        }

        if (Url == "ClassifyProduct.aspx?l=l")
        {
            Url = "ClassifyProduct.aspx";
        }

        Session["orderby"] = hidOrder.Value;
        Response.Redirect("~/" + Url);
    }
    #endregion

    #region === GridView 內容 ===
    protected void fn_Show_Data()
    {
        string strTourType = Convert.ToString(Request["tourtype"] + "");
        string strTripNo = Convert.ToString(Request["TripNo"] + "");
        string strAirport = Convert.ToString(Request["airport"] + ""); //出發地
        string strShow = Convert.ToString(Request["show"] + ""); //同業專賣

        string strArea = Convert.ToString(Request["area"] + "");
        DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(strArea));

        fn_DropDownList_Area2();

        string sgcn = Convert.ToString(Request["sgcn"] + "");
        DropDownList2.SelectedIndex = DropDownList2.Items.IndexOf(DropDownList2.Items.FindByValue(sgcn));

        string strOutDate = "";
        string strOutDate2 = "";
        string tt = HttpUtility.UrlDecode(Convert.ToString(Request["tp"] + ""));
        txbKey.Text = tt;

        Format obj = new Format();
        if (Convert.ToString(Request["RadDatePicker1"]) != null)
        {
            if (Convert.ToString(Request["RadDatePicker1"]) != "1980-01-01")
            {
                strOutDate = Convert.ToString(Request["RadDatePicker1"]);
            }
        }

        if (Convert.ToString(Request["RadDatePicker2"]) != null)
        {
            if (Convert.ToString(Request["RadDatePicker2"]) != "1980-01-01")
            {
                strOutDate2 = Convert.ToString(Request["RadDatePicker2"]);
            }
        }

        // 判斷行程，若是遊輪團的是話，日期區間改成2年 by roger
        if (Convert.ToString(Request["TripNo"]) != null)
        {
            fn_GetTripArea();
        }

        string strSql = "";
        strSql = " SELECT Grop_Numb,Van_Number,Grop_Name,Grop_Depa,Grop_Day,Grop_Liner" + "\r\n";
        strSql += " ,isnull(Grop.Reg_FIT,0)+isnull(Grop.Reg_INF,0)+isnull(Grop.Grop_Expect,0) as Grop_Expect,Grop_Visa,Grop_Tax,Grop_Tour,Replace(Grop_Intro, '<B>', '') as Grop_Intro" + "\r\n";
        strSql += " ,area.Area_Name,grop.Trip_No,Grop_Number,reg_ok,reg_standby" + "\r\n";
        strSql += " ,reg_checkok,Area.area_no,grop_pdf,isnull(grop_close,0) as Grop_Close,isnull(grop_ok,0) as grop_ok" + "\r\n";
        strSql += " ,isnull(pak,0) as pak,Group_Name,Agent_tour,reg_reserve,grop.Group_Category_No" + "\r\n";
        strSql += " ,Grop.Tour_Kind,Grop.Group_Name_No,grop.TourType,trip.trip_early_bird_url" + "\r\n"; //,count(Tour_Price.Number) as Cruises_Cnt" + "\r\n";
        strSql += " ,Pak_ArCheck_Sync,Pak_SignUp_Sync,isnull(Grop.Source_Agent_No,'') as Source_Agent_No,Grop.Grop_JoinTour" + "\r\n";
        strSql += " ,Grop.group_standby,Right(datename(weekday,Grop_Depa),1) as wd" + "\r\n";

        strSql += " ,0 as 'FullGroup',0 as 'FullGroupGo',0 as 'GroupOK'" + "\r\n"; //預先設定值為0的欄位，要計算人數使用
        strSql += " into [#Grop_Temp]" + "\r\n";
        strSql += " From Grop" + "\r\n";
        strSql += " LEFT JOIN trip on trip.trip_no = grop.trip_no" + "\r\n";
        strSql += " LEFT JOIN Area ON Area.Area_ID = GROP.Area_Code" + "\r\n";
        strSql += " LEFT JOIN Group_Name ON Group_Name.Group_Name_No = Grop.Group_Name_No" + "\r\n";
        strSql += " LEFT JOIN Tour_Price ON Tour_Price.Number = Grop.Van_Number and Tour_Price.Tick_Type = 'Cruises' and Tour_Price.adult_agent <> 0" + "\r\n";
        strSql += " where Grop_Depa >= @Grop_Depa" + "\r\n";
        strSql += " and isnull(hidden,'') <> 'y'" + "\r\n";
        strSql += " and Trip.Trip_Hide = 0" + "\r\n";
        strSql += " and Grop.CANC_PEOL = ''" + "\r\n";
        strSql += " and (Grop.ShowWeb = '0' OR Grop.ShowWeb = '2')" + "\r\n";
        if (strShow == "2") { strSql += " and (Grop.ShowWeb = '2')" + "\r\n"; }
        if (!string.IsNullOrEmpty(strArea)) strSql += " and Trip.Area_No = @Area_No" + "\r\n";
        if (!string.IsNullOrEmpty(sgcn)) strSql += " and SecClass_No = @SecClass_No" + "\r\n";
        if (!string.IsNullOrEmpty(rdpDate.Value)) strSql += " and Grop_Depa >= @Grop_Depa_1" + "\r\n";
        if (strArea != "11") { if (!string.IsNullOrEmpty(rdpDate2.Value)) strSql += " and Grop_Depa <= @Grop_Depa_2" + "\r\n"; }
        ////strSql += " and grop.trip_no in (" + strTripNo + ")" + "\r\n";

        // 搜尋團名&行程標題
        if (!string.IsNullOrEmpty(tt))
        {
            strSql += " AND (" + "\r\n";
            strSql += "      EXISTS (SELECT 1 FROM TripValue WHERE TripValue.Trip_No = Grop.Trip_No and TripValue.TripValue_Title like @TripValue_Title)" + "\r\n";
            strSql += "      OR Group_Name like @Grop_Name" + "\r\n";
            strSql += " )" + "\r\n";
        }

        if (!string.IsNullOrEmpty(strTripNo))
        {
            strSql += "    AND (" + "\r\n";
            strSql += "        EXISTS (SELECT 1 FROM Trip Trip_2 WHERE Trip_2.Trip_No = Grop.Trip_No and Trip_2.Trip_Classify = @Trip_Classify)" + "\r\n";
            strSql += "        OR Grop.Trip_No = @Trip_Classify" + "\r\n";
            strSql += "    )" + "\r\n";
        }

        // 出發地
        if (!string.IsNullOrEmpty(strAirport))
        {
            switch (strAirport)
            {
                case "TSA": //台北
                    strSql += " AND (EXISTS (SELECT 1 FROM Aire WHERE Aire.Grop_Numb = Grop.Grop_Numb AND ISNULL(Grop_Numb,'') <> '' AND Aire.Aire_Journey='1' AND Aire.Aire_StartConutry=N'松山'))";
                    break;
                case "TPE": //桃園
                    strSql += " AND (EXISTS (SELECT 1 FROM Aire WHERE Aire.Grop_Numb = Grop.Grop_Numb AND ISNULL(Grop_Numb,'') <> '' AND Aire.Aire_Journey='1' AND Aire.Aire_StartConutry=N'桃園'))";
                    break;
                case "RMQ": //台中
                    strSql += " AND (EXISTS (SELECT 1 FROM Aire WHERE Aire.Grop_Numb = Grop.Grop_Numb AND ISNULL(Grop_Numb,'') <> '' AND Aire.Aire_Journey='1' AND Aire.Aire_StartConutry=N'台中'))";
                    break;
                case "KHH": //高雄
                    strSql += " AND (EXISTS (SELECT 1 FROM Aire WHERE Aire.Grop_Numb = Grop.Grop_Numb AND ISNULL(Grop_Numb,'') <> '' AND Aire.Aire_Journey='1' AND Aire.Aire_StartConutry=N'高雄'))";
                    break;
            }
        }

        // GROUP BY
        strSql += " group by Grop_Numb,Van_Number,Grop_Name,Grop_Depa,Grop_Day" + "\r\n";
        strSql += " ,Grop_Liner,Grop_Expect,Grop_Visa,Grop_Tax,Grop_Tour" + "\r\n";
        strSql += " ,Grop_Intro,area.Area_Name,grop.Trip_No,Grop_Number,reg_ok" + "\r\n";
        strSql += " ,reg_standby,reg_checkok,Area.area_no,grop_pdf,grop_close" + "\r\n";
        strSql += " ,grop_ok,pak,Group_Name,Agent_tour,reg_reserve" + "\r\n";
        strSql += " ,grop.Group_Category_No,Grop.Tour_Kind,Grop.Group_Name_No,Grop.Reg_FIT,Grop.Reg_INF,grop.TourType,trip.trip_early_bird_url" + "\r\n";
        strSql += " ,Pak_ArCheck_Sync,Pak_SignUp_Sync,Grop.Source_Agent_No,Grop_JoinTour" + "\r\n";
        strSql += " ,Grop.group_standby" + "\r\n";


        strSql += " update[#Grop_Temp] set" + "\r\n";
        strSql += " FullGroup = Grop_Expect - Pak_ArCheck_Sync - Reg_Standby - Pak - Grop_JoinTour" + "\r\n";
        strSql += " , FullGroupGo = Grop_Expect - Pak_SignUp_Sync - Reg_Standby - Pak - Grop_JoinTour" + "\r\n";
        strSql += " , GroupOK = Pak_ArCheck_Sync - Grop_JoinTour" + "\r\n";
        strSql += " where(Tour_Kind = '19' OR Source_Agent_No != '') AND Source_Agent_No != 'A'" + "\r\n";

        strSql += " update[#Grop_Temp] set" + "\r\n";
        strSql += " FullGroup = Grop_Expect - Reg_CheckOK - Reg_Standby - Pak - Grop_JoinTour" + "\r\n";
        strSql += " , FullGroupGo = Grop_Expect - Reg_Ok - Reg_Standby - Pak - Grop_JoinTour" + "\r\n";
        strSql += " , GroupOK = Reg_CheckOK - Grop_JoinTour" + "\r\n";
        strSql += " where FullGroup = 0 and FullGroupGo = 0 and GroupOK = 0" + "\r\n";
        strSql += " and Tour_Kind<> '19'" + "\r\n";
        strSql += " and (Source_Agent_No = 'A' or Source_Agent_No = '')" + "\r\n";

        strSql += " select Tour_Kind, Source_Agent_No, FullGroup, FullGroupGo, GroupOK" + "\r\n";
        strSql += " ,* from [#Grop_Temp]" + "\r\n";
        // 20240217 by roger 新增功能搜尋列表可以只搜尋"已成團"或"可報名"的團體
        // 已成團 兩個條件成立才成立 Grop_OK = '1' 和 FullGroupGo >= 16
        // 可報名 兩個條件成立才成立 GroupOK < 16 和 Grop_OK <> '1'
        string strSignType = Convert.ToString(Request["signtype"] + "");
        if (strSignType != "")
        {
            strSql += " where FullGroup > 0" + "\r\n";
            strSql += " and Grop_Close <> '1'" + "\r\n"; //滿團(1=滿團,0=未滿團)
            if (strSignType == "1,2")
            { strSql += " and ((Grop_OK = '1' or GroupOK >= 16) or (GroupOK < 16 AND Grop_OK <> '1'))" + "\r\n"; }
            else if (strSignType == "1") //已成團
            { strSql += " and (Grop_OK = '1' or GroupOK >= 16)" + "\r\n"; }
            else if (strSignType == "2") //可報名
            { strSql += " and (GroupOK < 16 AND Grop_OK <> '1')" + "\r\n"; }
        }

        // 排序
        switch (hidOrder.Value)
        {
            case "0":
                strSql += " ORDER BY Grop_Depa,Van_Number " + "\r\n";
                break;
            case "1":
                strSql += " ORDER BY Grop_Name,Van_Number " + "\r\n";
                break;
            case "2":
                strSql += " ORDER BY Area_No,Van_Number " + "\r\n";
                break;
            case "3":
                strSql += " ORDER BY Grop_Day,Van_Number " + "\r\n";
                break;
            case "4":
                strSql += " ORDER BY Grop_Tour,Van_Number " + "\r\n";
                break;
            default:
                strSql += " ORDER BY Grop_Depa,Van_Number " + "\r\n";
                break;
        }

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlDataAdapter da = new SqlDataAdapter(strSql, strConnString);
        da.SelectCommand.Parameters.Add(new SqlParameter("@Grop_Depa", DateTime.Today));
        if (!string.IsNullOrEmpty(strArea)) da.SelectCommand.Parameters.Add(new SqlParameter("@Area_No", strArea));
        if (!string.IsNullOrEmpty(sgcn)) da.SelectCommand.Parameters.Add(new SqlParameter("@SecClass_No", sgcn));
        if (!string.IsNullOrEmpty(rdpDate.Value)) da.SelectCommand.Parameters.Add(new SqlParameter("@Grop_Depa_1", rdpDate.Value));
        if (!string.IsNullOrEmpty(rdpDate2.Value)) da.SelectCommand.Parameters.Add(new SqlParameter("@Grop_Depa_2", rdpDate2.Value));

        // 搜尋團名&行程標題
        if (!string.IsNullOrEmpty(tt))
        {
            da.SelectCommand.Parameters.Add(new SqlParameter("@TripValue_Title", "%" + tt + "%"));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Grop_Name", "%" + tt + "%"));
        }

        if (!string.IsNullOrEmpty(strTripNo))
        {
            da.SelectCommand.Parameters.Add(new SqlParameter("@Trip_Classify", strTripNo));
        }

        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();
        connect.Close();
    }

    protected string fn_RtnTripNo()
    {
        string strTripNo = Convert.ToString(Request["TripNo"] + "");

        string strSql = "";
        strSql = "select trip_no,area_no from trip";
        if (!string.IsNullOrEmpty(strTripNo))
        {
            strSql += " where Trip_Classify = @Trip_Classify";
        }

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            if (!string.IsNullOrEmpty(strTripNo))
                comm.Parameters.Add(new SqlParameter("@Trip_Classify", strTripNo));
            SqlDataReader reader = comm.ExecuteReader();
            strTripNo = "'" + strTripNo + "'";
            while (reader.Read())
            {
                strTripNo += (string.IsNullOrEmpty(strTripNo) ? "" : ",") + "'" + reader["trip_no"].ToString() + "'";
            }
            reader.Close();
            comm.Dispose();
        }
        finally
        {
            connect.Close();
        }

        //把最後的逗點用掉
        //strTripNo = strTripNo.Substring(0, strTripNo.Length - 1);

        return strTripNo;
    }
    // 團名-網址
    protected string fn_RtnGridViewGrop_Name(string strArea_No, string strGrop_Pdf, string strTripNo, string strDate, string strType, string strTourType, string strTrip_Early_Bird_Url)
    {
        /*
        if (strTourType == "典藏")
        {
            return "http://www.luxetravel.com.tw/Group.html";
        }
        */
        if (strTrip_Early_Bird_Url != "")
        {
            //return "http://www.luxetravel.com.tw/Group.html";
            return strTrip_Early_Bird_Url;
        }

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
            if (strType.Trim() == "") { strType = "none"; }
            return "TripIntroduction.aspx?TripNo=" + strTripNo + "&Date=" + strDate + "&type=" + strType;
            //"<a href =http://www.artisan.com.tw/GropPDF/" + reader2.GetValue(17) + " target='_blank' ><span class=\"styleorange\">" + reader2.GetValue(1).ToString() + "</span></a>";
        }
        else
        {
            //return "<a href =http://www.artisan.com.tw/GropPDF/" + reader2.GetValue(17) + " target='_blank' ><span class=\"styleorange\">" + reader2.GetValue(1).ToString() + "</span></a>";
            return "http://www.artisan.com.tw/GropPDF/" + strGrop_Pdf;
        }
    }
    // 簽證
    protected string fn_RtnGridViewGrop_Visa(string strGrop_Visa)
    {
        switch (strGrop_Visa)
        {
            case "0":
                return "含";
            case "1":
                return "不含";
            case "2":
                return "免簽";
            default:
                return strGrop_Visa;
        }
    }
    // 動態
    protected string fn_RtnGridViewGrop_Close(string strGrop_Close, string strGrop_Expect, string strReg_CheckOK, string strReg_Standby, string strPak, string strReg_Ok, string strGrop_OK, string strGrop_JoinTour, string ps, string pa, string code, string tourkind, string strGroup_Standby)
    {
        if (ps == "") { ps = "0"; }
        if (pa == "") { pa = "0"; }
        if (strGrop_Close == "True")
        {
            return "<div class='status red'><img src='./images/list_icon02.png' alt=''>滿團</div>";
        }
        else
        {
            if (strGrop_OK == "True")
            {
                return "<div class='status green'><img src='./images/list_icon04.png' alt=''>已成團</div>";
            }
            else if (strGroup_Standby == "True")
            {
                return "<div class='status orange'><img src='./images/list_icon03.png' alt=''>滿候補</div>";
            }
            else
            {
                if ((tourkind == "19" || code != "") && code != "A")
                {
                    if (Convert.ToInt32(strGrop_Expect) - Convert.ToInt32(pa) - Convert.ToInt32(strReg_Standby) - Convert.ToInt32(strPak) + Convert.ToInt32(strGrop_JoinTour) <= 0)
                    {
                        return "<div class='status red'><img src='./images/list_icon02.png' alt=''>滿團</div>";
                    }
                    else
                    {
                        if (Convert.ToInt32(strGrop_Expect) - Convert.ToInt32(ps) - Convert.ToInt32(strReg_Standby) - Convert.ToInt32(strPak) + Convert.ToInt32(strGrop_JoinTour) <= 0)
                        {
                            return "<div class='status orange'><img src='./images/list_icon03.png' alt=''>滿團可候補</div>";
                        }
                        else if (Convert.ToInt32(pa) - Convert.ToInt32(strGrop_JoinTour) >= 16)
                        {
                            return "<div class='status green'><img src='./images/list_icon04.png' alt=''>已成團</div>";
                        }
                        else
                        {
                            return "<div class='status blue'><img src='./images/list_icon05.png' alt=''>可報名</div>";
                        }
                    }
                }
                else
                {
                    //if (Convert.ToInt32(strGrop_Expect) - Convert.ToInt32(strReg_CheckOK) - Convert.ToInt32(strReg_Standby) - Convert.ToInt32(strPak) - Convert.ToInt32(strReg_Reserve) - 1 <= 0)
                    if (Convert.ToInt32(strGrop_Expect) - Convert.ToInt32(strReg_CheckOK) - Convert.ToInt32(strReg_Standby) - Convert.ToInt32(strPak) + Convert.ToInt32(strGrop_JoinTour) <= 0)
                    {
                        return "<div class='status red'><img src='./images/list_icon02.png' alt=''>滿團</div>";
                    }
                    else
                    {
                        //if (Convert.ToInt32(strGrop_Expect) - Convert.ToInt32(strReg_Ok) - Convert.ToInt32(strReg_Standby) - Convert.ToInt32(strPak) - Convert.ToInt32(strReg_Reserve) - 1 <= 0)
                        if (Convert.ToInt32(strGrop_Expect) - Convert.ToInt32(strReg_Ok) - Convert.ToInt32(strReg_Standby) - Convert.ToInt32(strPak) + Convert.ToInt32(strGrop_JoinTour) <= 0)
                        {
                            return "<div class='status orange'><img src='./images/list_icon03.png' alt=''>滿團可候補</div>";
                        }
                        else if (Convert.ToInt32(strReg_CheckOK) - Convert.ToInt32(strGrop_JoinTour) >= 16)
                        {
                            return "<div class='status green'><img src='./images/list_icon04.png' alt=''>已成團</div>";
                        }
                        else
                        {
                            return "<div class='status blue'><img src='./images/list_icon05.png' alt=''>可報名</div>";
                        }
                    }
                }
            }
        }
    }
    //人數
    //protected string fn_regcheckpeople(string strReg_Ok, string strReg_standby, string strreg_reserve, string strpak)
    protected string fn_regcheckpeople(string strReg_Ok, string strreg_checkok, string strReg_standby, string strpak, string strreg_reserve, string ps, string pa, string code, string tourkind)
    {
        if (ps == "") { ps = "0"; }
        if (pa == "") { pa = "0"; }
        int i = 0;


        if ((tourkind == "19" || code != "") && code != "A")
        {
            if ((Convert.ToInt32(ps) + Convert.ToInt32(strReg_standby)) >= 0)
            {
                return (Convert.ToInt32(ps) + Convert.ToInt32(strReg_standby)).ToString();
            }
            else
            {
                return "0";
            }
        }
        else
        {
            //i = Convert.ToInt32(strReg_Ok) - Convert.ToInt32(strreg_reserve) - Convert.ToInt32(strreg_checkok);
            // 20140807 by roger 要把"後補人數"加入"報名人數"
            i = Convert.ToInt32(strReg_Ok) - Convert.ToInt32(strreg_checkok);
            // if ((Convert.ToInt32(strReg_Ok) - Convert.ToInt32(strReg_standby) - Convert.ToInt32(strreg_reserve)) >= 0)
            if ((Convert.ToInt32(strreg_checkok) + Convert.ToInt32(strReg_standby) + Convert.ToInt32(strpak) + i) >= 0)
            {
                // return (Convert.ToInt32(strReg_Ok) - Convert.ToInt32(strReg_standby) - Convert.ToInt32(strreg_reserve)).ToString();  
                return (Convert.ToInt32(strreg_checkok) + Convert.ToInt32(strReg_standby) + Convert.ToInt32(strpak) + i).ToString();
            }
            else
            {
                return "0";
            }
        }
    }
    //收訂人數
    protected string fn_regcheckOKpeople(string strreg_checkok, string strReg_standby, string strpak, string pa, string code, string tourkind)
    {
        if (pa == "") { pa = "0"; }
        if ((tourkind == "19" || code != "") && code != "A")
        {
            if ((Convert.ToInt32(pa) + Convert.ToInt32(strReg_standby)) >= 0)
            {
                return (Convert.ToInt32(pa) + Convert.ToInt32(strReg_standby)).ToString();
            }
            else
            {
                return "0";
            }
        }
        else
        {
            if ((Convert.ToInt32(strreg_checkok) + Convert.ToInt32(strReg_standby) + Convert.ToInt32(strpak)) >= 0)
            {
                return (Convert.ToInt32(strreg_checkok) + Convert.ToInt32(strReg_standby) + Convert.ToInt32(strpak)).ToString();
            }
            else
            {
                return "0";
            }
        }
    }
    protected Boolean fn_XML_Cruises(string strGroup_Name_No)
    {
        /*
        String XMLnode = Server.MapPath(@"~/ZUpload/BaseData/Cruises.xml");// 抓取參數資料
        System.Xml.XmlDocument dom = new System.Xml.XmlDocument();
        dom.Load(XMLnode); //可讀文件
        
        System.Xml.XmlNodeList strXml = dom.SelectSingleNode("data").ChildNodes;
        for (int index = 0; index <= strXml.Count - 1; index++)
        {
            if (strGroup_Name_No == strXml.Item(index).InnerText)
            {
                return true;
            }
        }
        */
        return false;
    }
    // 報名check
    protected Boolean fn_RtnGrop_Close(string strGrop_Close, string strGrop_Expect, string strReg_CheckOK, string strReg_Standby, string strPak,
                                    string strReg_Ok, string strGrop_OK, string strReg_Reserve, string strGrop_Depa, string strGroup_Category_No,
                                    string strArea_No, string strTour_Kind, string strVan_number, string strGroup_Name_No, string strTourType)
    {
        // 結團不能再報名
        if (strGrop_Close == "True") { return false; }

        /*20170317 郵輪能線上報名 by世昌
        //郵輪不能線上報名
        if (DropDownList1.SelectedValue == "7") { return false; }
        if (strArea_No == "7") { return false; }*/
        if (strGroup_Category_No == "86") { return false; }
        // 日期格式錯誤，不能報名
        if (!clsFunction.Check.IsDate(strGrop_Depa)) { return false; }
        // 7天前就不能報名了
        if (Convert.ToDateTime(strGrop_Depa).AddDays(-7) < DateTime.Today) { return false; }
        // 若是pak團就不能報名 by roger
        if (strTour_Kind == "18" || strTour_Kind == "19") { return false; }

        // 20170317 包船不能報名 by世昌
        //if (strCruises_Cnt != "0") { return false; }
        // 20170713 名字有"藍寶石公主"就不能報名
        if (fn_XML_Cruises(strGroup_Name_No)) { return false; }


        return true;


        ////郵輪不能線上報名
        //if (DropDownList1.SelectedValue == "7") { return false; }

        //// 7天前就不能再報名了
        //if (Convert.ToDateTime(strGrop_Depa).AddDays(-7) <= DateTime.Today) { return false; }

        //if (clsFunction.Check.IsDate(strGrop_Depa))
        //{
        //    if (Convert.ToDateTime(strGrop_Depa) < Convert.ToDateTime("2016/03/01"))
        //    {
        //        return false;
        //    }

        //    if (strGroup_Category_No == "55" && Convert.ToDateTime(strGrop_Depa) < Convert.ToDateTime("2016/06/23"))
        //    {
        //        return false;
        //    }
        //}
        //else
        //{
        //    return false;
        //}

        //if (strGrop_Close == "True")
        //{
        //    return false;
        //}
        //else
        //{
        //    if (strGrop_OK == "True")
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        if (Convert.ToInt32(strGrop_Expect) - Convert.ToInt32(strReg_CheckOK) - Convert.ToInt32(strReg_Standby) - Convert.ToInt32(strPak) - Convert.ToInt32(strReg_Reserve) - 1 <= 0)
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            if (Convert.ToInt32(strGrop_Expect) - Convert.ToInt32(strReg_Ok) - Convert.ToInt32(strReg_Standby) - Convert.ToInt32(strPak) - Convert.ToInt32(strReg_Reserve) - 1 <= 0)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //}
    }
    // 稅金
    protected string fn_RtnGridViewGrop_Tax(string strGrop_Tax)
    {
        switch (strGrop_Tax)
        {
            case "0":
                return "含";
            case "1":
                return "不含";
            default:
                return strGrop_Tax;
        }
    }
    // 團費
    protected string fn_RtnGridViewGrop_Tour(string strGrop_Tour)
    {
        Format obj = new Format();
        if (obj.IsNumeric(strGrop_Tour))
        {
            double dblGrop_Tour = Convert.ToDouble(strGrop_Tour);

            if (dblGrop_Tour <= 1)
                return "<span style='color:red;	'>來電洽詢</span>";
            else
                return String.Format("NT.{0:0,0}", dblGrop_Tour); //dblGrop_Tour.ToString("<span style='color:red'>NT.{0:0,0}</span>");
        }

        return "&nbsp;";
    }
    // 回傳團名
    protected string fn_RtnGridViewGroup_Name(string strGrop_Name, string strGroup_Name, string strGrop_Intro)
    {
        if (string.IsNullOrEmpty(strGroup_Name))
        { return strGrop_Name + "<br /><span class=\"style_white_blue\">" + strGrop_Intro + "</span>"; }

        return strGroup_Name + "<br /><span class=\"style_white_blue\">" + strGrop_Intro + "</span>";
    }

    protected void imgButton_Click2(object sender, ImageClickEventArgs e)
    {
        ImageButton curTextBox = (ImageButton)sender;
        int gvRowIndex = (curTextBox.NamingContainer as GridViewRow).RowIndex;
        if (clsFunction.Check.Check_Ing(Session["PERNO"].ToString()))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('會員審核中......');", true);
        }
        else
        {
            if (clsFunction.Check.Check_Account())
            {
                Response.Redirect("OLApply/Apply1.aspx?n=" + GridView1.DataKeys[gvRowIndex].Value.ToString());
            }
            else
            {
                Response.Redirect("~/OLApply/Check.aspx");
            }
        }
    }
    #endregion

    #region === Intro ===
    protected string getListPoint(string numb, string intro)
    {
        string str = "";

        if (intro.IndexOf("www.artisan.com.tw/images/handtohand.gif") != -1) { str = "<img src='images/mode_icon01.png' alt=''>"; }

        string strSql = "";
        strSql = " SELECT * FROM ListPoint";
        strSql += " WHERE 1=1";
        strSql += " AND Agent_No = '1'";
        strSql += " AND Trip_no = @Trip_no";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@Trip_no", numb));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                str += reader["intro"].ToString();
            }
            reader.Close();
            comm.Dispose();
        }
        finally
        {
            connect.Close();
        }

        return str;
    }
    protected string getGropIntro(string intro)
    {
        string str = "";
        // 往下滑動的特效
        //str = "<ul class='list-point'>";
        //str += intro.Replace("<img src=\"http://www.artisan.com.tw/images/handtohand.gif\" border=\"0\" align=\"absmiddle\">", "");  //移除舊版聯營圖示
        //str += "</ul>";

        // 沒任何特效
        str = "<div class='list-point'>";
        str += intro.Replace("width=","").Replace("<img src=\"http://www.artisan.com.tw/images/handtohand.gif\" border=\"0\" align=\"absmiddle\">", "");  //移除舊版聯營圖示
        str += "</div>";
        return str;
    }
    #endregion

    #region === GridView 排序 ===
    //private string getOrderBy()
    //{
    //    string str = "";

    //    switch (hidOrder.Value)
    //    {
    //        case "0":
    //            str = " ORDER BY Grop_Depa ";
    //            break;
    //        case "1":
    //            str = " ORDER BY Grop_Name ";
    //            break;
    //        case "2":
    //            str = " ORDER BY Area.area_no ";
    //            break;
    //        case "3":
    //            str = " ORDER BY Grop_Day ";
    //            break;
    //        case "4":
    //            str = " ORDER BY Grop_Tour ";
    //            break;
    //        default:
    //            break;
    //    }
    //    return str;
    //}
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        hidOrder.Value = RadioButtonList1.SelectedValue;
        fn_Show_Data();
    }
    #endregion

    #region === 分頁 ===
    private void pagination()
    {
        string url = "";
        int sum = GridView1.PageCount;
        int prev = nowPage - 1;
        int next = nowPage + 1;

        url = "javascript:__doPostBack(\"GridView1\",\"Page$" + prev.ToString() + "\")";
        //url = "javascript:__doPostBack(\"GridView1\",\"Page$11\")";

        if (nowPage == 1) { litPag.Text = "<li class='button'><a class='disabled' href='#0'>上一頁</a></li>"; }
        else { litPag.Text = "<li class='button'><a href='" + url + "'>上一頁</a></li>"; }

        if (nowPage > 5)
        {
            url = "javascript:__doPostBack(\"GridView1\",\"Page$1\")";
            litPag.Text += "<li><a href='" + url + "'>1</a></li>";
            if (nowPage != 6) { litPag.Text += "<li><span>...</span></li>"; }
        }

        for (int i = nowPage - 3; i < nowPage + 4; i++)
        {
            if (i > sum) { break; }
            if (i > 0)
            {
                url = "javascript:__doPostBack(\"GridView1\",\"Page$" + i.ToString() + "\")";
                if (i == nowPage) { litPag.Text += "<li><a href='#0' class='current'>" + i.ToString() + "</a></li>"; }
                else { litPag.Text += "<li><a href='" + url + "'>" + i.ToString() + "</a></li>"; }
            }
        }

        if (nowPage + 3 < sum)
        {
            litPag.Text += "<li><span>...</span></li>";
            url = "javascript:__doPostBack(\"GridView1\",\"Page$" + sum + "\")";
            litPag.Text += "<li><a href='" + url + "'>" + sum + "</a></li>";
        }

        url = "javascript:__doPostBack(\"GridView1\",\"Page$" + next.ToString() + "\")";
        if (nowPage == sum) { litPag.Text += "<li class='button'><a class='disabled' href='#0'>下一頁</a></li>"; }
        else { litPag.Text += "<li class='button'><a href='" + url + "'>下一頁</a></li>"; }

        /*
                    <li class="button"><a class="disabled" href="#0">上一頁</a></li>
                    <li><a class="current" href="#0">1</a></li>
                    <li><a href="#0">2</a></li>
                    <li><a href="#0">3</a></li>
                    <li><a href="#0">4</a></li>
                    <li><span>...</span></li>
                    <li><a href="#0">20</a></li>
                    <li class="button"><a href="#0">下一頁</a></li>*/
    }
    #endregion

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        fn_Show_Data();
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();

        nowPage = e.NewPageIndex + 1;
        pagination();
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        //2018過年專用，過期可刪除
        if (DateTime.Now >= Convert.ToDateTime("2018/02/14 20:00:00") &&  DateTime.Now < Convert.ToDateTime("2018/02/21 10:00:00"))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('春節期間(2/15-2/20)暫不提供網站線上報名服務\\n並將於2/21早上10點恢復服務,懇請海涵,感謝!!');", true);
            return;
        }

        LinkButton curTextBox = (LinkButton)sender;
        int gvRowIndex = (curTextBox.NamingContainer as GridViewRow).RowIndex;

        if (clsFunction.Check.Check_Ing(Session["PERNO"].ToString()))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('會員審核中......');", true);
        }
        else
        {
            if (clsFunction.Check.Check_Account())
            {
                Response.Redirect("OLApply/Apply1.aspx?n=" + GridView1.DataKeys[gvRowIndex].Value.ToString());
            }
            else
            {
                Response.Redirect("~/OLApply/Check.aspx");
            }
        }
    }
}