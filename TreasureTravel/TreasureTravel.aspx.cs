﻿using System;
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
using System.Text.RegularExpressions;

public partial class TreasureTravel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getExh();
        }
    }
    //小館行程
    protected void getExh() 
    {
        int picMax = 70;        //圖片簡介字數上限
        int introMax = 50;      //簡介字數上限

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection conn = new SqlConnection(strConnString);

        string strsql = "";
        int i = 0;
        strsql = " SELECT Number,Super_Trip.Area_No,GC_NO,Trip_Name,URL,pic_Intro ";
        strsql += " ,Class,Intro,Picture,Orderby,isnull(Price,'0') as Price ";
        strsql += " ,isnull(Super_Trip.Trip_No,'') as Trip_No,Group_Name.Group_Name,isnull(dm_title,'') as dm_title ";
        strsql += " FROM Super_Trip ";
        strsql += " left join trip on trip.Trip_No = Super_Trip.Trip_No ";
        strsql += " left join Group_Name on Group_Name.Group_Name_No = trip.Group_Name_No";
        strsql += " where Class = '4'";     //1.典藏 2.新視界 3.巨匠 4.珍藏
        strsql += " order by Area_No asc";

        try
        {
            conn.Open();
            SqlCommand comm = new SqlCommand(strsql, conn);
            SqlDataReader reader = comm.ExecuteReader();
            string strArea_No = "";
            while (reader.Read())
            {
                
                if (grop(reader["trip_no"].ToString(), reader["area_no"].ToString(), Request["no"]) == "1" || reader["dm_title"].ToString() != "")
                {
                    if (strArea_No != reader["area_no"].ToString())
                    {
                        //1.西歐 2.東歐 5.南歐 6.北歐 7.郵輪 8.紐澳 9.南亞 10.中東 11.美洲 12.大非洲 27.海島 32.東南亞 35.日本 37.中國
                        switch (reader["Area_No"].ToString())
                        {
                            case "1":
                                Exh.Text += "<h6 class=\"cPink\">西歐</h6>";
                                break;
                            case "2":
                                Exh.Text += "<h6 class=\"cOrg\">東歐</h6>";
                                break;
                            case "5":
                                Exh.Text += "<h6 class=\"cOrg\">南歐</h6>";
                                break;
                            case "6":
                                Exh.Text += "<h6 class=\"cPink\">北歐</h6>";
                                break;
                            case "8":
                            case "9":
                                Exh.Text += "<h6 class=\"cBlu\">紐澳南亞</h6>";
                                break;
                            case "10":
                                Exh.Text += "<h6 class=\"clightPink\">中東</h6>";
                                break;
                            case "11":
                                Exh.Text += "<h6 class=\"cGre\">美洲遊輪</h6>";
                                break;
                            case "12":
                                Exh.Text += "<h6 class=\"clightPink\">古文明.大非洲</h6>";
                                break;
                            case "27":
                            case "35":
                                Exh.Text += "<h6 class=\"clightOrg\">海島.日本.河輪</h6>";
                                break;
                            case "32":
                            case "37":
                                Exh.Text += "<h6 class=\"clightBlu\">東南亞.中國</h6>";
                                break;
                            default:
                                Exh.Text += "<h6 class=\"cPink\">&nbsp</h6>";
                                break;
                        }
                    }
                    strArea_No = reader["area_no"].ToString();

                    Exh.Text += "<figure class=\"block\" data-aos=\"zoom-in-down\" data-aos-duration=\"700\" data-aos-delay=\"300\" data-aos-once=\"true\">";
                    Exh.Text += "<div class=\"block-img\"><img src='https://www.artisan.com.tw/Zupload16/new_web/" + reader["Picture"].ToString() + "' alt=''></div>";

                    //1.西歐 2.東歐 5.南歐 6.北歐 7.郵輪 8.紐澳 9.南亞 10.中東 11.美洲 12.大非洲 27.海島 32.東南亞 35.日本 37.中國
                    switch (reader["Area_No"].ToString())
                    {
                        case "1":
                            Exh.Text += "<figcaption class=\"c001\">" + word_over(reader["Trip_Name"].ToString(), picMax) + "</figcaption>";
                            break;
                        case "2":
                            Exh.Text += "<figcaption class=\"c003\">" + word_over(reader["Trip_Name"].ToString(), picMax) + "</figcaption>";
                            break;
                        case "5":
                            Exh.Text += "<figcaption class=\"c004\">" + word_over(reader["Trip_Name"].ToString(), picMax) + "</figcaption>";
                            break;
                        case "6":
                            Exh.Text += "<figcaption class=\"c001\">" + word_over(reader["Trip_Name"].ToString(), picMax) + "</figcaption>";
                            break;
                        case "7":
                            Exh.Text += "<figcaption class=\"c008\">" + word_over(reader["Trip_Name"].ToString(), picMax) + "</figcaption>";
                            break;
                        case "8":
                            Exh.Text += "<figcaption class=\"c005\">" + word_over(reader["Trip_Name"].ToString(), picMax) + "</figcaption>";
                            break;
                        case "9":
                            Exh.Text += "<figcaption class=\"c006\">" + word_over(reader["Trip_Name"].ToString(), picMax) + "</figcaption>";
                            break;
                        case "10":
                            Exh.Text += "<figcaption class=\"c009\">" + word_over(reader["Trip_Name"].ToString(), picMax) + "</figcaption>";
                            break;
                        case "11":
                            Exh.Text += "<figcaption class=\"c007\">" + word_over(reader["Trip_Name"].ToString(), picMax) + "</figcaption>";
                            break;
                        case "12":
                            Exh.Text += "<figcaption class=\"c010\">" + word_over(reader["Trip_Name"].ToString(), picMax) + "</figcaption>";
                            break;
                        case "27":
                            Exh.Text += "<figcaption class=\"c011\">" + word_over(reader["Trip_Name"].ToString(), picMax) + "</figcaption>";
                            break;
                        case "32":
                            Exh.Text += "<figcaption class=\"c014\">" + word_over(reader["Trip_Name"].ToString(), picMax) + "</figcaption>";
                            break;
                        case "35":
                            Exh.Text += "<figcaption class=\"c012\">" + word_over(reader["Trip_Name"].ToString(), picMax) + "</figcaption>";
                            break;
                        case "37":
                            Exh.Text += "<figcaption class=\"c015\">" + word_over(reader["Trip_Name"].ToString(), picMax) + "</figcaption>";
                            break;
                        default:
                            Exh.Text += "<figcaption>" + word_over(reader["Trip_Name"].ToString(), picMax) + "</figcaption>";
                            break;
                    }
                    
                    Exh.Text += "<button>前往行程→</button>";
                    Exh.Text += "<a href='" + reader["URL"].ToString() + "' target='_blank'></a>";
                    Exh.Text += "</figure>";
                }
            }

            comm.Dispose();
            reader.Close();
        }
        finally { conn.Close(); conn = null; 
        }
        
    }
    // 判斷有無團體，若沒有團體的話，就不需顯示此行程
    protected string grop(string strTripNo, string strArea, string sgcn)
    {

        //ClassifyProduct.aspx?l=l&RadDatePicker1=2019/10/16&RadDatePicker2=2020/4/16&area=6&sgcn=43
        string strSql = "";
        strSql = " select top 1 Grop_Numb,Grop_Name,Grop_Depa,Grop_Day,Grop_Liner";
        strSql += " ,Grop_Expect,Grop_Visa,Grop_Tax,Grop_Tour,Grop_Intro";
        strSql += " ,area.Area_Name,grop.Trip_No,Grop_Number,reg_ok,reg_standby";
        strSql += " ,reg_checkok,Area.area_no,grop_pdf,grop_close,grop_ok";
        strSql += " ,IsNull(pak,0) as pak,Group_Name,Grop.Grop_JoinTour,reg_reserve,grop.Group_Category_No,Van_Number";
        strSql += " From Grop";
        strSql += " LEFT JOIN trip on trip.trip_no = grop.trip_no";
        strSql += " LEFT JOIN Area ON Area.Area_ID = GROP.Area_Code";
        strSql += " LEFT JOIN Group_Name ON Group_Name.Group_Name_No = Grop.Group_Name_No";
        strSql += " where 1=1";
        strSql += " and isnull(hidden,'') <> 'y'";
        strSql += " and Trip.Trip_Hide=0";

        strSql += " and Grop_Depa >= @Grop_Depa";

        if (!string.IsNullOrEmpty(strArea)) strSql += " and Area.Area_no = @Area_No";
        if (!string.IsNullOrEmpty(sgcn)) strSql += " and SecClass_No = @SecClass_No";

        if (!string.IsNullOrEmpty(strTripNo))
        {
            strSql += "    AND (";
            strSql += "        EXISTS (SELECT 1 FROM Trip Trip_2 WHERE Trip_2.Trip_No = Grop.Trip_No and Trip_2.Trip_Classify = @Trip_Classify)";
            strSql += "        OR Grop.Trip_No = @Trip_Classify";
            strSql += "    )";
        }

        // 因中國大陸的行程，目前行程資料，故先設定在2099年，在搜尋時也一樣要列出 20140115
        if (strArea == "37")
        { strSql += " OR (Grop_Depa >= '2099-1-1' AND Grop_Depa <= '2099-12-31' AND Trip.Area_No = 37)"; }
        strSql += " ORDER BY Grop_Depa";


        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand cmd = new SqlCommand(strSql, connect);
        cmd.Parameters.Add(new SqlParameter("@Grop_Depa", DateTime.Today));
        if (!string.IsNullOrEmpty(strArea)) cmd.Parameters.Add(new SqlParameter("@Area_No", strArea));
        if (!string.IsNullOrEmpty(sgcn)) cmd.Parameters.Add(new SqlParameter("@SecClass_No", sgcn));
        // 搜尋團名&行程標題
        if (!string.IsNullOrEmpty(Request["tp"]))
        {
            cmd.Parameters.Add(new SqlParameter("@TripValue_Title", "%" + Request["tp"] + "%"));
            cmd.Parameters.Add(new SqlParameter("@Grop_Name", "%" + Request["tp"] + "%"));
        }
        if (!string.IsNullOrEmpty(strTripNo))
        {
            cmd.Parameters.Add(new SqlParameter("@Trip_Classify", strTripNo));
        }
        SqlDataReader reader = cmd.ExecuteReader();
        string strRtnValue = "0";
        if (reader.Read())
        {
            strRtnValue = "1";
        }
        cmd.Dispose();
        reader.Dispose();
        connect.Close();
        return strRtnValue;
    }
    private string word_over(string word, int max)   //文字刪減
    {
        if (word.Length > max) {return word.Substring(0, max) + "...";}
        else {return word;}
    }
    
    protected string fn_GetGrop_Tour(string strTrip_No)     // 回傳最低價
    {
        string strTripNo = fn_RtnTripNo(strTrip_No);

        string strGrop_Tour = "0";
        string strSql = "";
        strSql += " SELECT IsNull(Min(Case When IsNull(Grop.Grop_Tour,0) > 9000 Then (Grop_Tour) End),0) AS Grop_Tour";
        strSql += " FROM Trip";
        strSql += " LEFT JOIN Grop ON Trip.Trip_no = grop.trip_no";
        strSql += " WHERE 1=1";
        strSql += " AND tourtype <> 'ITF'";
        if (!string.IsNullOrEmpty(strTripNo))
        { strSql += " AND Grop.Trip_no in (" + strTripNo + ")"; }
        strSql += " AND (isnull(hidden,'') <> 'y')";
        strSql += " AND (Grop.TourType = N'典藏' OR Trip.Trip_Hide=0)";
        strSql += " AND Grop_Depa >= @Grop_Depa";
        strSql += " AND IsNull(Grop.Grop_Tour,0) > 9000";
        strSql += " AND Grop.CANC_PEOL = ''";
        strSql += " AND Trip.Trip_Hide = 0";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@Grop_Depa", DateTime.Today));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                strGrop_Tour = reader["Grop_Tour"].ToString();
            }
            reader.Close();
            comm.Dispose();
        }
        finally
        {
            connect.Close();
        }

        return strGrop_Tour;
    }
    protected string fn_RtnTripNo(string strTripNo)
    {
        string strSql = "";
        strSql = "SELECT trip_no FROM trip";
        strSql += " WHERE 1=1";
        strSql += " AND Trip.Trip_Hide=0";
        if (!string.IsNullOrEmpty(strTripNo))
           strSql += " AND Trip.Trip_Classify = @Trip_Classify";

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

        return strTripNo;
    }
}
