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

public partial class TripList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
	Counter.Counter.fn_Account();
        clsFunction.Check.CheckSession();
        getotherlit();
        string abc = Convert.ToString(Request["btn"]);
        if (!string.IsNullOrEmpty(abc))
        {
            getbtn(Convert.ToString(Request["btn"].ToString()));
        }
        else
        {
            getbtn("1");
        }
        getfeat();
        getplan();
        getmaintour();
        ClassFunction clsFun = new ClassFunction();
        clsFun.CheckWebAddress();

        fn_Show_Data();

        if (this.IsPostBack == false)
        {
            gettwopic();
            gettourblog();
            getartblog();
            //account();
            gettriplist();
            fnSale_Point();
            getApplies();
        }
    }
    protected void fn_Show_Data()
    {
        string strTrip_IsNewWindows = "N"; //檢查是否開啟新視窗
        string tripfile = "";
        string strsql = "";
        strsql = "select Trip_Name,Area.Area_Name,SecClass.SecClass_Name,Trip_Intro,Trip_titlepic";
        strsql += ",isnull(flash_name,'') as flash_name,headpic_name,mappic_name2,trip_file,Trip_Early_Bird_Show";
        strsql += ",Trip_Early_Bird_URL,Trip_IsNewWindows";
        strsql += " from trip";
        strsql += " left join SecClass on SecClass.SecClass_No = trip.SecClass_No";
        strsql += " left join Area on Area.Area_No = trip.Area_No";
        strsql += " left join trip_flash on trip.flash_no = trip_flash.flash_no";
        strsql += " left join headpic on trip.headpic_no = headpic.headpic_no";
        strsql += " left join mappic on trip.mappic_no = mappic.mappic_no";
        strsql += " where Trip_No='" + Convert.ToString(Request.QueryString["TripNo"]) + "'";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand comm = new SqlCommand(strsql, connect);
        SqlDataReader reader = comm.ExecuteReader();

        if (reader.Read())
        {
            // 先判斷這一個頁是否有重新確認並編輯過資料
            // 有重新編輯過資料就導到新的頁面
            if (reader["Trip_IsNewWindows"].ToString() == "True")
            {
                strTrip_IsNewWindows = "Y";
            }
            else
            {
                if (reader.GetValue(5).ToString().Length < 15)
                {
                    //flashlit.Text = " <script type=\"text/javascript\"> ";
                    //flashlit.Text += " AC_FL_RunContent( 'codebase','http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0','width','752','height','240','src','EDayFlash/" + reader.GetValue(5).ToString().Split('.').GetValue(0) + "','quality','high','pluginspage','http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash','movie','EDayFlash/" + reader.GetValue(5).ToString().Split('.').GetValue(0) + "' );  ";
                    //flashlit.Text += " </script><noscript><object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0\" width=\"752\" height=\"240\"> ";
                    //flashlit.Text += " <param name=\"movie\" value=\"EDayFlash/" + reader.GetValue(5).ToString() + "\" /> ";
                    //flashlit.Text += " <param name=\"quality\" value=\"high\" /> ";
                    //flashlit.Text += " <embed src=\"EDayFlash/" + reader.GetValue(5).ToString() + "\" quality=\"high\" pluginspage=\"http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash\" type=\"application/x-shockwave-flash\" width=\"752\" height=\"240\"></embed> ";
                    //flashlit.Text += " </object></noscript>";
                }
                else
                {
                    string aaabcd = reader.GetValue(5).ToString().Substring(15, 3);
                    if (aaabcd == "jpg")
                    {
                        flashlit.Text = "<img src=\"EDayFlash/" + reader.GetValue(5).ToString() + "\" border=\"0\" width=\"752\" height=\"240\"/>";
                    }
                    else
                    {
                        flashlit.Text = " <script type=\"text/javascript\"> ";
                        flashlit.Text += " AC_FL_RunContent( 'codebase','http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0','width','752','height','240','src','EDayFlash/" + reader.GetValue(5).ToString().Split('.').GetValue(0) + "','quality','high','pluginspage','http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash','movie','EDayFlash/" + reader.GetValue(5).ToString().Split('.').GetValue(0) + "' );  ";
                        flashlit.Text += " </script><noscript><object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0\" width=\"752\" height=\"240\"> ";
                        flashlit.Text += " <param name=\"movie\" value=\"EDayFlash/" + reader.GetValue(5).ToString() + "\" /> ";
                        flashlit.Text += " <param name=\"quality\" value=\"high\" /> ";
                        flashlit.Text += " <embed src=\"EDayFlash/" + reader.GetValue(5).ToString() + "\" quality=\"high\" pluginspage=\"http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash\" type=\"application/x-shockwave-flash\" width=\"752\" height=\"240\"></embed> ";
                        flashlit.Text += " </object></noscript>";
                    }
                }
                tripfile = reader.GetValue(8).ToString();
                if (tripfile.Length > 0)
                {
                    printlit.Text = "<a href=\"TripFile/" + tripfile + "\" target=\"_blank\" ><img src='images/old/print.jpg' border=\"0\" width=\"178\" height=\"45\"/></a> ";
                }
                Page.Title = "凱旋旅行社(巨匠旅遊) -> " + reader["Trip_Name"].ToString();
                HtmlMeta tag = new HtmlMeta();
                tag.Name = "AreaName";
                tag.Content = reader.GetValue(1).ToString();
                Header.Controls.Add(tag);
                tag = new HtmlMeta();
                tag.Name = "SecName";
                tag.Content = reader.GetValue(2).ToString();
                Header.Controls.Add(tag);
                tag = new HtmlMeta();
                tag.Name = "KeyWord";
                tag.Content = reader.GetValue(3).ToString().Replace("&nbsp;", "").Replace("\r\n", "");
                Header.Controls.Add(tag);
                titlepiclit.Text = "<img src=\"EDayPic/" + reader.GetValue(4).ToString() + "\" width=\"320\" height=\"102\" />";//左
                if (reader.GetValue(7).ToString().Length > 1)
                {
                    maplit.Text = "<img src=\"MapPic/" + reader.GetValue(7).ToString() + "\"   />";
                }
                string HeadPic_Name = reader.GetValue(6).ToString();
                if (HeadPic_Name.IndexOf(".") > 0)
                {
                    HeadPic_Name = HeadPic_Name.Split((char)'.').GetValue(0).ToString() + "_Fr." + HeadPic_Name.Split((char)'.').GetValue(1).ToString();
                }
                titlepiclit.Text += "<img src=\"HeadPic/" + HeadPic_Name + "\" width=\"807\" height=\"102\" />";//右   

                Lit_early_bird.Text = "";
                if (reader["Trip_Early_Bird_Show"].ToString() == "y")
                {
                    //<a href="#"><img src="img/early_bird.gif" width="250" height="32" border="0"></a>
                    Lit_early_bird.Text += "<a href=\"" + reader["Trip_Early_Bird_URL"].ToString() + "\" target=\"_blank\"><img src=\"img/early_bird.gif\" width=\"250\" height=\"32\" border=\"0\"></a>";
                }
            }
        }
        comm.Dispose();
        reader.Close();
        connect.Close();

        if (strTrip_IsNewWindows == "Y")
        {
            Response.Redirect("TripIntroduction.aspx?TripNo=" + Request["TripNo"] + "&Date=" + Request["Date"] + "&type=" + Request["type"] + "");
        }
    }

    protected void getotherlit()
    {
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        string strsql = "";
        strsql = " select OtherTrip.Trip_No, Trip_Name from OtherTrip left join Trip on Trip.Trip_No = OtherTrip.Trip_No where Trip.Trip_No = '" + Request["TripNo"].ToString() + "'";
        SqlCommand comm = new SqlCommand(strsql, connect);
        SqlDataReader rd = comm.ExecuteReader();
        int abcdefg = 0;
        while (rd.Read())
        {
            abcdefg += 1;
            otherlit.Text += "<span style=\"color:#990000;\">&diams;</span><a href=\"TripList_New.aspx?btn=1&TripNo=" + rd.GetValue(0).ToString() + "\"> " + rd.GetValue(1).ToString();
        }

        strsql = "select Trip_No, Link_Title, Link_Link from Trip_Link where Trip_No ='" + Request["TripNo"].ToString() + "'";
        rd.Close();
        comm = new SqlCommand(strsql, connect);
        SqlDataReader rd2 = comm.ExecuteReader();
        while (rd2.Read())
        {
            abcdefg += 1;
            otherlit.Text += "<span style=\"color:#990000;\">&diams;</span><a href=\"" + rd2.GetValue(2).ToString() + "\" target=\"_blank\" > " + rd2.GetValue(1).ToString();
        }

        if (abcdefg > 0)
        {

        }
        else
        {
            otherlit.Text = "<span style=\"color:#990000;\">&diams;</span> 暫無其他相關行程資料。<br>";
        }

        rd.Dispose();
        comm.Dispose();
        rd2.Close();
        connect.Close();
    }

    protected void getplan()
    {
        string gropnumb = "";//團號
        string strsql = "";
        strsql = " select Grop_Depa,Grop_Numb";
        strsql += " from Grop";
        strsql += " where Trip_No ='" + Convert.ToString(Request.QueryString["TripNo"]) + "'";
        strsql += " and Grop_Depa = '" + Request["Date"].ToString() + "'";
        //strsql += " and Grop_Numb like '%" + Request["grop"].ToString() + "%'";
        strsql += " and Grop_liner like '%" + Request["type"].ToString() + "%'";
        //strsql += " and Grop_liner = '" + Request["type"].ToString() + "'";
        strsql += " order by Grop_Depa";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand commtopair = new SqlCommand(strsql, connect);
        SqlDataReader readertopair = commtopair.ExecuteReader();

        if (readertopair.Read() == true)
        {
            gropnumb = readertopair.GetValue(1).ToString();
        }

        commtopair.Dispose();
        readertopair.Close();
        getplandetail(gropnumb);
    }

    protected void getplandetail(string gpnum)
    {
        string strsql = " select Aire_StartConutry,Aire_EndCountry,Aire_StartTime,Aire_EndTime,Aire_AddTime,Aire_AirLine,Aire_Flight";
        strsql += " from Aire";
        strsql += " where Grop_Numb='" + gpnum + "' and Aire_Journey=1";
        strsql += " order by Aire_Type";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand comm = new SqlCommand(strsql, connect);
        SqlDataReader reader = comm.ExecuteReader();
        //go
        while (reader.Read())
        {
            plango.Text += "<table width=\"150\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";
            plango.Text += "<tr>";
            plango.Text += " <td>" + reader.GetValue(0).ToString() + "</td>";
            plango.Text += " <td>&rarr;</td>";
            plango.Text += " <td align=\"center\">" + reader.GetValue(1).ToString() + "</td>";
            plango.Text += " </tr>";
            plango.Text += "  <tr>";
            plango.Text += " <td>" + reader.GetValue(5).ToString() + reader.GetValue(6).ToString() + "</td>";
            plango.Text += "<td></td>";
            plango.Text += "<td align=\"center\">" + reader.GetValue(2).ToString() + " - " + reader.GetValue(3).ToString() + " " + reader.GetValue(4).ToString() + "</td>";
            plango.Text += "</tr>";
            plango.Text += "</table>";
        }
        reader.Close();
        comm.Dispose();
        //come
        strsql = " select Aire_StartConutry,Aire_EndCountry,Aire_StartTime,Aire_EndTime,Aire_AddTime,Aire_AirLine,Aire_Flight";
        strsql += " from Aire";
        strsql += " where Grop_Numb='" + gpnum + "' and Aire_Journey=2";
        strsql += " order by Aire_Type";

        SqlCommand comm2 = new SqlCommand(strsql, connect);
        SqlDataReader reader2 = comm2.ExecuteReader();
        while (reader2.Read())
        {
            plancome.Text += "<table width=\"150\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";
            plancome.Text += "<tr>";
            plancome.Text += " <td>" + reader2.GetValue(0).ToString() + "</td>";
            plancome.Text += " <td>&rarr;</td>";
            plancome.Text += " <td align=\"center\">" + reader2.GetValue(1).ToString() + "</td>";
            plancome.Text += " </tr>";
            plancome.Text += "  <tr>";
            plancome.Text += " <td>" + reader2.GetValue(5).ToString() + reader2.GetValue(6).ToString() + "</td>";
            plancome.Text += "<td></td>";
            plancome.Text += "<td align=\"center\">" + reader2.GetValue(2).ToString() + "-" + reader2.GetValue(3).ToString() + reader2.GetValue(4).ToString() + "</td>";
            plancome.Text += "</tr>";
            plancome.Text += "</table>";
        }
        reader2.Close();
        comm2.Dispose();
        connect.Close();
    }

    protected void getbtn(string btn)
    {
        switch (Convert.ToUInt32(btn))
        {
            case 1:
                btnlit.Text = "<div class=\"button\">";
                btnlit.Text += "<a href=\"TripList.aspx?btn=1&TripNo=" + Convert.ToString(Request.QueryString["TripNo"]) + "&date=" + Convert.ToString(Request.QueryString["Date"]) + "&type=" + Convert.ToString(Request.QueryString["type"]) + "\">";
                btnlit.Text += " <img src=\"img/btn1c.jpg\" width=\"192\" height=\"67\" border=\"0\" alt=\"行程介紹\" name=\"bt1\" /></a></div>";

                btnlit.Text += "<div class=\"button\">";
                btnlit.Text += "<a href=\"Trip_hotel.aspx?btn=2&TripNo=" + Convert.ToString(Request.QueryString["TripNo"]) + "&date=" + Convert.ToString(Request.QueryString["Date"]) + "&type=" + Convert.ToString(Request.QueryString["type"]) + "\"";
                btnlit.Text += " onmouseout=\"document.bt2.src='img/btn2a.jpg'\" onmouseover=\"document.bt2.src='img/btn2b.jpg'";
                btnlit.Text += " \">";
                btnlit.Text += "<img src=\"img/btn2a.jpg\" width=\"192\" height=\"67\" border=\"0\" alt=\"嚴選飯店\" name=\"bt2\" /></a></div>";

                btnlit.Text += "<div class=\"button\">";
                btnlit.Text += "<a href=\"Trip_Meal.aspx?btn=3&TripNo=" + Convert.ToString(Request.QueryString["TripNo"]) + "&date=" + Convert.ToString(Request.QueryString["Date"]) + "&type=" + Convert.ToString(Request.QueryString["type"]) + "\"";
                btnlit.Text += " onmouseout=\"document.bt3.src='img/btn3a.jpg'\" onmouseover=\"document.bt3.src='img/btn3b.jpg'";
                btnlit.Text += " \">";
                btnlit.Text += "<img src=\"img/btn3a.jpg\" width=\"192\" height=\"67\" border=\"0\" alt=\"餐食介紹\" name=\"bt3\" /></a></div>";

                btnlit.Text += "<div class=\"button\">";
                btnlit.Text += "<a href=\"http://www.artisan.com.tw/2011_DM/notes.htm\" target='blank'";
                btnlit.Text += " onmouseout=\"document.bt4.src='img/btn4a.jpg'\" onmouseover=\"document.bt4.src='img/btn4b.jpg'";
                btnlit.Text += " \">";
                btnlit.Text += "<img src=\"img/btn4a.jpg\" width=\"192\" height=\"67\" border=\"0\" alt=\"精選相簿\" name=\"bt4\" /></a></div>";

                btnlit.Text += "<div class=\"button\">";
                btnlit.Text += "<a href=\"ClassifyProduct.aspx?btn=5&TripNo=" + Convert.ToString(Request.QueryString["TripNo"]) + "\"";
                btnlit.Text += " onmouseout=\"document.bt5.src='img/btn5a.jpg'\" onmouseover=\"document.bt5.src='img/btn5b.jpg';\">";
                btnlit.Text += "<img src=\"img/btn5a.jpg\" width=\"192\" height=\"67\" border=\"0\" alt=\"線上報名\" name=\"bt5\" /></a></div>";

                break;
            case 2:
                btnlit.Text = "<div class=\"button\">";
                btnlit.Text += "<a href=\"TripList.aspx?btn=1&TripNo=" + Convert.ToString(Request.QueryString["TripNo"]) + "&date=" + Convert.ToString(Request.QueryString["Date"]) + "&type=" + Convert.ToString(Request.QueryString["type"]) + "\"";
                btnlit.Text += " onmouseout=\"document.bt1.src='img/btn1a.jpg'\" onmouseover=\"document.bt1.src='img/btn1b.jpg'";
                btnlit.Text += " \">";
                btnlit.Text += "<img src=\"img/btn1a.jpg\" width=\"192\" height=\"67\" border=\"0\" alt=\"行程介紹\" name=\"bt1\" /></a></div>";

                btnlit.Text += "<div class=\"button\">";
                btnlit.Text += "<a href=\"Trip_hotel.aspx?btn=2&TripNo=" + Convert.ToString(Request.QueryString["TripNo"]) + "&date=" + Convert.ToString(Request.QueryString["Date"]) + "&type=" + Convert.ToString(Request.QueryString["type"]) + "\">";
                btnlit.Text += " <img src=\"img/btn2c.jpg\" width=\"192\" height=\"67\" border=\"0\" alt=\"嚴選飯店\" name=\"bt2\" /></a></div>";

                btnlit.Text += "<div class=\"button\">";
                btnlit.Text += "<a href=\"Trip_Meal.aspx?btn=3&TripNo=" + Convert.ToString(Request.QueryString["TripNo"]) + "&date=" + Convert.ToString(Request.QueryString["Date"]) + "&type=" + Convert.ToString(Request.QueryString["type"]) + "\"";
                btnlit.Text += " onmouseout=\"document.bt3.src='img/btn3a.jpg'\" onmouseover=\"document.bt3.src='img/btn3b.jpg'";
                btnlit.Text += " \">";
                btnlit.Text += "<img src=\"img/btn3a.jpg\" width=\"192\" height=\"67\" border=\"0\" alt=\"餐食介紹\" name=\"bt3\" /></a></div>";

                btnlit.Text += "<div class=\"button\">";
                btnlit.Text += "<a href=\"http://www.artisan.com.tw/2011_DM/notes.htm\" target='blank'";
                btnlit.Text += " onmouseout=\"document.bt4.src='img/btn4a.jpg'\" onmouseover=\"document.bt4.src='img/btn4b.jpg'";
                btnlit.Text += " \">";
                btnlit.Text += "<img src=\"img/btn4a.jpg\" width=\"192\" height=\"67\" border=\"0\" alt=\"精選相簿\" name=\"bt4\" /></a></div>";

                btnlit.Text += "<div class=\"button\">";
                btnlit.Text += "<a href=\"ClassifyProduct.aspx?btn=5&TripNo=" + Convert.ToString(Request.QueryString["TripNo"]) + "\"";
                btnlit.Text += " onmouseout=\"document.bt5.src='img/btn5a.jpg'\" onmouseover=\"document.bt5.src='img/btn5b.jpg';\">";
                btnlit.Text += "<img src=\"img/btn5a.jpg\" width=\"192\" height=\"67\" border=\"0\" alt=\"線上報名\" name=\"bt5\" /></a></div>";

                break;
            case 3:

                btnlit.Text = "<div class=\"button\">";
                btnlit.Text += "<a href=\"TripList.aspx?btn=1&TripNo=" + Convert.ToString(Request.QueryString["TripNo"]) + "&date=" + Convert.ToString(Request.QueryString["Date"]) + "&type=" + Convert.ToString(Request.QueryString["type"]) + "\"";
                btnlit.Text += " onmouseout=\"document.bt1.src='img/btn1a.jpg'\" onmouseover=\"document.bt1.src='img/btn1b.jpg'";
                btnlit.Text += " \">";
                btnlit.Text += "<img src=\"img/btn1a.jpg\" width=\"192\" height=\"67\" border=\"0\" alt=\"行程介紹\" name=\"bt1\" /></a></div>";

                btnlit.Text += "<div class=\"button\">";
                btnlit.Text += "<a href=\"Trip_hotel.aspx?btn=2&TripNo=" + Convert.ToString(Request.QueryString["TripNo"]) + "&date=" + Convert.ToString(Request.QueryString["Date"]) + "&type=" + Convert.ToString(Request.QueryString["type"]) + "\"";
                btnlit.Text += " onmouseout=\"document.bt2.src='img/btn2a.jpg'\" onmouseover=\"document.bt2.src='img/btn2b.jpg'";
                btnlit.Text += " \">";
                btnlit.Text += "<img src=\"img/btn2a.jpg\" width=\"192\" height=\"67\" border=\"0\" alt=\"嚴選飯店\" name=\"bt2\" /></a></div>";

                btnlit.Text += "<div class=\"button\">";
                btnlit.Text += "<a href=\"Trip_Meal.aspx?btn=3&TripNo=" + Convert.ToString(Request.QueryString["TripNo"]) + "&date=" + Convert.ToString(Request.QueryString["Date"]) + "&type=" + Convert.ToString(Request.QueryString["type"]) + "\">";
                btnlit.Text += " <img src=\"img/btn3c.jpg\" width=\"192\" height=\"67\" border=\"0\" alt=\"餐食介紹\" name=\"bt3\" /></a></div>";

                btnlit.Text += "<div class=\"button\">";
                btnlit.Text += "<a href=\"http://www.artisan.com.tw/2011_DM/notes.htm\" target='blank'";
                btnlit.Text += " onmouseout=\"document.bt4.src='img/btn4a.jpg'\" onmouseover=\"document.bt4.src='img/btn4b.jpg'";
                btnlit.Text += " \">";
                btnlit.Text += "<img src=\"img/btn4a.jpg\" width=\"192\" height=\"67\" border=\"0\" alt=\"精選相簿\" name=\"bt4\" /></a></div>";

                btnlit.Text += "<div class=\"button\">";
                btnlit.Text += "<a href=\"ClassifyProduct.aspx?btn=5&TripNo=" + Convert.ToString(Request.QueryString["TripNo"]) + "\"";
                btnlit.Text += " onmouseout=\"document.bt5.src='img/btn5a.jpg'\" onmouseover=\"document.bt5.src='img/btn5b.jpg';\">";
                btnlit.Text += "<img src=\"img/btn5a.jpg\" width=\"192\" height=\"67\" border=\"0\" alt=\"線上報名\" name=\"bt5\" /></a></div>";

                break;
            case 4:
                btnlit.Text = "<div class=\"button\">";
                btnlit.Text += "<a href=\"TripList.aspx?btn=1&TripNo=" + Convert.ToString(Request.QueryString["TripNo"]) + "&date=" + Convert.ToString(Request.QueryString["Date"]) + "&type=" + Convert.ToString(Request.QueryString["type"]) + "\"";
                btnlit.Text += " onmouseout=\"document.bt1.src='img/btn1a.jpg'\" onmouseover=\"document.bt1.src='img/btn1b.jpg'";
                btnlit.Text += " \">";
                btnlit.Text += "<img src=\"img/btn1a.jpg\" width=\"192\" height=\"67\" border=\"0\" alt=\"行程介紹\" name=\"bt1\" /></a></div>";

                btnlit.Text += "<div class=\"button\">";
                btnlit.Text += "<a href=\"Trip_hotel.aspx?btn=2&TripNo=" + Convert.ToString(Request.QueryString["TripNo"]) + "&date=" + Convert.ToString(Request.QueryString["Date"]) + "&type=" + Convert.ToString(Request.QueryString["type"]) + "\"";
                btnlit.Text += " onmouseout=\"document.bt2.src='img/btn2a.jpg'\" onmouseover=\"document.bt2.src='img/btn2b.jpg'";
                btnlit.Text += " \">";
                btnlit.Text += "<img src=\"img/btn2a.jpg\" width=\"192\" height=\"67\" border=\"0\" alt=\"嚴選飯店\" name=\"bt2\" /></a></div>";

                btnlit.Text += "<div class=\"button\">";
                btnlit.Text += "<a href=\"Trip_Meal.aspx?btn=3&TripNo=" + Convert.ToString(Request.QueryString["TripNo"]) + "&date=" + Convert.ToString(Request.QueryString["Date"]) + "&type=" + Convert.ToString(Request.QueryString["type"]) + "\"";
                btnlit.Text += " onmouseout=\"document.bt3.src='img/btn3a.jpg'\" onmouseover=\"document.bt3.src='img/btn3b.jpg'";
                btnlit.Text += " \">";
                btnlit.Text += "<img src=\"img/btn3a.jpg\" width=\"192\" height=\"67\" border=\"0\" alt=\"餐食介紹\" name=\"bt3\" /></a></div>";

                btnlit.Text += "<div class=\"button\">";
                btnlit.Text += "<a href=\"http://www.artisan.com.tw/2011_DM/notes.htm\" target='blank'";
                btnlit.Text += " <img src=\"img/btn4c.jpg\" width=\"192\" height=\"67\" border=\"0\" alt=\"精選相簿\" name=\"bt4\" /></a></div>";

                btnlit.Text += "<div class=\"button\">";
                btnlit.Text += "<a href=\"ClassifyProduct.aspx?btn=5&TripNo=" + Convert.ToString(Request.QueryString["TripNo"]) + "\"";
                btnlit.Text += " onmouseout=\"document.bt5.src='img/btn5a.jpg'\" onmouseover=\"document.bt5.src='img/btn5b.jpg';\">";
                btnlit.Text += "<img src=\"img/btn5a.jpg\" width=\"192\" height=\"67\" border=\"0\" alt=\"線上報名\" name=\"bt5\" /></a></div>";
                break;
            case 5:
                btnlit.Text = "<div class=\"button\">";
                btnlit.Text += "<a href=\"TripList.aspx?btn=1&TripNo=" + Convert.ToString(Request.QueryString["TripNo"]) + "&date=" + Convert.ToString(Request.QueryString["Date"]) + "&type=" + Convert.ToString(Request.QueryString["type"]) + "\"";
                btnlit.Text += " onmouseout=\"document.bt1.src='img/btn1a.jpg'\" onmouseover=\"document.bt1.src='img/btn1b.jpg'";
                btnlit.Text += " \">";
                btnlit.Text += "<img src=\"img/btn1a.jpg\" width=\"192\" height=\"67\" border=\"0\" alt=\"行程介紹\" name=\"bt1\" /></a></div>";

                btnlit.Text += "<div class=\"button\">";
                btnlit.Text += "<a href=\"Trip_hotel.aspx?btn=2&TripNo=" + Convert.ToString(Request.QueryString["TripNo"]) + "&date=" + Convert.ToString(Request.QueryString["Date"]) + "&type=" + Convert.ToString(Request.QueryString["type"]) + "\"";
                btnlit.Text += " onmouseout=\"document.bt2.src='img/btn3a.jpg'\" onmouseover=\"document.bt2.src='img/btn2b.jpg'";
                btnlit.Text += " \">";
                btnlit.Text += "<img src=\"img/btn2a.jpg\" width=\"192\" height=\"67\" border=\"0\" alt=\"嚴選飯店\" name=\"bt2\" /></a></div>";

                btnlit.Text += "<div class=\"button\">";
                btnlit.Text += "<a href=\"Trip_Meal.aspx?btn=3&TripNo=" + Convert.ToString(Request.QueryString["TripNo"]) + "&date=" + Convert.ToString(Request.QueryString["Date"]) + "&type=" + Convert.ToString(Request.QueryString["type"]) + "\"";
                btnlit.Text += " onmouseout=\"document.bt3.src='img/btn3a.jpg'\" onmouseover=\"document.bt3.src='img/btn3b.jpg'";
                btnlit.Text += " \">";
                btnlit.Text += "<img src=\"img/btn3a.jpg\" width=\"192\" height=\"67\" border=\"0\" alt=\"餐食介紹\" name=\"bt3\" /></a></div>";

                btnlit.Text += "<div class=\"button\">";
                btnlit.Text += "<a href=\"http://www.artisan.com.tw/2011_DM/notes.htm\" target='blank'";
                btnlit.Text += " onmouseout=\"document.bt4.src='img/btn4a.jpg'\" onmouseover=\"document.bt4.src='img/btn4b.jpg';\">";
                btnlit.Text += "<img src=\"img/btn4a.jpg\" width=\"192\" height=\"67\" border=\"0\" alt=\"精選相簿\" name=\"bt4\" /></a></div>";

                btnlit.Text += "<div class=\"button\">";
                btnlit.Text += "<a href=\"ClassifyProduct.aspx?TripNo=" + Convert.ToString(Request.QueryString["TripNo"]) + "\">";
                btnlit.Text += " <img src=\"img/btn5c.jpg\" width=\"192\" height=\"67\" border=\"0\" alt=\"線上報名\" name=\"bt5\" /></a></div>";

                break;
        }
    }

    protected void getmaintour()
    {
        string strsql = "";
        strsql = " select TripValue_Day,TripValue_Title";
        strsql += " ,TripValue_Breakfast,b.LodgeMeal_Title as Breakfast_Name";
        strsql += " ,TripValue_Lunch,c.LodgeMeal_Title as Lunch_Name";
        strsql += " ,TripValue_Dinner,d.LodgeMeal_Title as Dinner_Name";
        strsql += " ,TripValue_Lodging,e.LodgeMeal_Title as Lodging_Name";
        strsql += " ,isnull(EDayPic_Name,'') as EDayPic_Name";
        strsql += " ,ValueSec_Intro";
        strsql += " ,TripValue_Lodging2,f.LodgeMeal_Title as Lodging_Name2";
        strsql += " ,e.LodgeMeal_Url as Lurl1,f.LodgeMeal_Url as Lurl2";
        strsql += " ,left(b.LodgeMeal_Intro,10) as Breakfast_Intro";//16
        strsql += " ,left(c.LodgeMeal_Intro,10) as Lunch_Intro";
        strsql += " ,left(d.LodgeMeal_Intro,10) as Dinner_Intro";
        strsql += " ,left(e.LodgeMeal_Intro,10) as Lodging_Intro";//19
        strsql += " ,left(f.LodgeMeal_Intro,10) as Lodging2_Intro";
        strsql += " ,g.LodgeMeal_Title as Lodging_Name3";
        strsql += " ,g.LodgeMeal_Url as Lurl3,TripValue_Lodging3";
        strsql += " ,left(g.LodgeMeal_Intro,10) as Lodging3_Intro";
        strsql += " from TripValue";
        strsql += " left join LodgeMeal b on b.LodgeMeal_No = TripValue_Breakfast";
        strsql += " left join LodgeMeal c on c.LodgeMeal_No = TripValue_Lunch";
        strsql += " left join LodgeMeal d on d.LodgeMeal_No = TripValue_Dinner";
        strsql += " left join LodgeMeal e on e.LodgeMeal_No = TripValue_Lodging";
        strsql += " left join LodgeMeal f on f.LodgeMeal_No = TripValue_Lodging2";
        strsql += " left join LodgeMeal g on g.LodgeMeal_No = TripValue_Lodging3";
        strsql += " left join EDayPic on EDayPic.EDayPic_No = TripValue.EDayPic_No";
        strsql += " left join ValueSec on ValueSec.ValueSec_No = TripValue.ValueSec_No";
        strsql += " where TripValue.Trip_No='" + Convert.ToString(Request.QueryString["TripNo"]) + "'";
        strsql += " order by TripValue_Day";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand comm6 = new SqlCommand(strsql, connect);
        SqlDataReader reader6 = comm6.ExecuteReader();


        while (reader6.Read())
        {
            maintour.Text += " <div class=\"tour_characteristic_day\">";
            maintour.Text += " <table>";
            maintour.Text += " <tr>";
            //day pic
            maintour.Text += " <td class=\"day_pic\"><img src=\"img/day" + reader6.GetValue(0).ToString() + ".jpg\" width=\"96\" height=\"26\" /></td>";
            maintour.Text += " <td class=\"tour_characteristic_day_info\">" + Format.Replace(reader6.GetValue(1).ToString(), getpath()) + "</td>";
            maintour.Text += " </tr>";
            maintour.Text += " </table>";
            maintour.Text += " </div>";
            maintour.Text += " <div class=\"tour_characteristic_tool\">" + opFormat.Replace(Format.Replace(reader6.GetValue(11).ToString(), getpath())).Replace("/NewTrip/fckEditor/editor/filemanager", "http://www.artisan.com.tw/NewTrip/fckEditor/editor/filemanager") + "</div>";
            //maintour.Text += " <div class=\"tour_characteristic_hotel\"><span class=\"tour_characteristic_text4\">Hotel  </span>"+ "<a href =" + Format.Replace(reader6.GetValue(14).ToString(), getpath()) + ">" + Format.Replace(reader6.GetValue(9).ToString(), getpath()) +"</a>";
            maintour.Text += " <div class=\"tour_characteristic_hotel\"><span class=\"tour_characteristic_text4\">Hotel  </span>";

            if (reader6.GetValue(14).ToString().Length > 2 && reader6.GetValue(9).ToString().Length > 2)
            {
                maintour.Text += "<a href =" + Format.Replace(reader6.GetValue(14).ToString(), getpath()) + " target =\"_blank\">" + Format.Replace(reader6.GetValue(9).ToString(), getpath()) + "</a>";
            }
            else
            {
                maintour.Text += Format.Replace(reader6.GetValue(9).ToString(), getpath());
            }
            if (reader6.GetValue(13).ToString().Length > 2)
            {
                if (reader6.GetValue(15).ToString().Length > 2)
                {
                    //maintour.Text += " 或 " + reader6.GetValue(13).ToString();
                    maintour.Text += " 或 " + "<a href =" + Format.Replace(reader6.GetValue(15).ToString(), getpath()) + " target='_blank'>" + reader6.GetValue(13).ToString() + "</a>";
                }
                else
                {
                    maintour.Text += " 或 " + Format.Replace(reader6.GetValue(13).ToString(), getpath());
                }
            }

            if (reader6.GetValue(21).ToString().Length > 2)
            {
                if (reader6.GetValue(22).ToString().Length > 2)
                {
                    //第三家飯店跳到第二行顯示&nbsp切齊第一家飯店
                    maintour.Text += " 或 " + "<br>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a href =" + Format.Replace(reader6.GetValue(22).ToString(), getpath()) + " target='_blank'>" + reader6.GetValue(21).ToString() + "</a>";
                }
                else
                {
                    maintour.Text += " 或 " + "<br>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp" + Format.Replace(reader6.GetValue(21).ToString(), getpath());
                }
            }

            if (reader6.GetValue(9).ToString() != "溫暖的家")
            {
                maintour.Text += " 或同等級";
            }
            maintour.Text += " <br><span class=\"tour_characteristic_text4\">早餐</span> " + getself(Format.Replace(reader6.GetValue(3).ToString(), getpath()));
            maintour.Text += " <span class=\"tour_characteristic_text4\"> 午餐</span> " + getself(Format.Replace(reader6.GetValue(5).ToString(), getpath()));
            maintour.Text += " <span class=\"tour_characteristic_text4\"> 晚餐</span> " + getself(Format.Replace(reader6.GetValue(7).ToString(), getpath())) + "</div>";
            maintour.Text += " <div class=\"tour_divide_link\"></div>";
        }
        comm6.Dispose();
        reader6.Close();
    }

    protected string getself(string gg)
    {
        if (gg.Length < 2)
        {
            return "敬請自理";
        }
        else
        {
            return gg;
        }
    }

    //行程特色
    protected void getfeat()
    {
        string strsql = "";
        strsql = " select TripFeat_Title,TripFeat_Intro";
        strsql += " from TripFeat";
        strsql += " where Trip_No='" + Convert.ToString(Request.QueryString["TripNo"]) + "'";
        strsql += " order by TripFeat_No";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand comm3 = new SqlCommand(strsql, connect);
        SqlDataReader reader3 = comm3.ExecuteReader();
        while (reader3.Read())
        {
            //  TripFeat.Text += "<div id=\"fb-root\"></div><script src=\"http://connect.facebook.net/zh_TW/all.js#xfbml=1\"></script><fb:like href=http://www.artisan.com.tw"+Request.RawUrl+" send=\"true\" width=\"450\" show_faces=\"true\" font=\"\"></fb:like>";

            TripFeat.Text += "<div class=\"tour_characteristic_tool\"><span class=\"tour_characteristic_text\">";
            TripFeat.Text += Format.Replace(reader3.GetValue(0).ToString(), getpath()) + "</span><br />";
            TripFeat.Text += opFormat.Replace(Format.Replace(reader3.GetValue(1).ToString(), getpath())).Replace("/fckEditor/editor/filemanager", "http://www.artisan.com.tw/fckEditor/editor/filemanager") + "</div>";
            TripFeat.Text += "<div class=\"tour_divide_link\"></div>";
        }
    }
    // 銷售重點
    protected void fnSale_Point()
    {
        string strSql = "";
        strSql += " select top 1 Trip_No,Sale_Point from TripValue";
        strSql += " where Trip_No = @Trip_No";
        strSql += " AND ISNULL(Sale_Point,'')<>''";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand comm = new SqlCommand(strSql, connect);
        comm.Parameters.Add(new SqlParameter("@Trip_No", Convert.ToString(Request.QueryString["TripNo"])));
        SqlDataReader reader = comm.ExecuteReader();
        if (reader.Read())
        {
            LitSale_point.Text += reader["Sale_Point"].ToString();
        }
        reader.Close();
        comm.Dispose();
        connect.Close();
    }
    //適用活動
    protected void getApplies()
    {
        string strsql = "";
        strsql = " select *";
        strsql += " from Applies";
        strsql += " where Ap_Trip_No='" + Convert.ToString(Request.QueryString["TripNo"]) + "'";
        strsql += " order by Ap_Order";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand comm = new SqlCommand(strsql, connect);
        SqlDataReader reader = comm.ExecuteReader();
        if (reader.HasRows != false)
        {
            litAp.Text += "<div id='top_event_tool_old'>";
            litAp.Text += " <table width='587' height='266' border='0' cellpadding='0' cellspacing='0'>";
        }
        while (reader.Read())
        {
            litAp.Text += "<tr>";
            litAp.Text += "<td width='199' style='border-bottom:solid 1px #dddddd;'><a href='" + reader["Ap_Link"].ToString() + "'><img src='http://www.artisan.com.tw/images/Applies/" + reader["Ap_Pic"].ToString() + "' width='187' height='54' /></a></td>";
            litAp.Text += "<td width='388' style='border-bottom:solid 1px #dddddd;'><a href='" + reader["Ap_Link"].ToString() + "'><span class='style_top_event_tool_old'>■ " + reader["Ap_Title"].ToString() + "</span></a><br />";
            litAp.Text += "<span class='style_top_event_gray_old'>" + reader["Ap_Subject"].ToString() + "</span></td>";
            litAp.Text += "</tr>";
        }
        if (reader.HasRows != false)
        {
            litAp.Text += "</table>";
            litAp.Text += "</div>";
        }
    }
    public string getpath()
    {
        string strpath = Server.MapPath(@"~\App_Data\ForStr.txt");

        return strpath;
    }

    protected void gettourblog()
    {
        string strsql = "";
        strsql += "select BlogName,BlogIntro,BlogUrl,BlogPic,BlogNew,BlogType,TbNumber from Tripblog ";
        strsql += "where TripNo='" + Convert.ToString(Request.QueryString["TripNo"]) + "' and BlogType = 'tourist' ";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand cmd = new SqlCommand(strsql, connect);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        DataTable dt = new DataTable();

        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Image img = new Image();
                img.ID = "Imgblog" + (i.ToString() + 1);
                img.ImageUrl = @"~/tripblog/" + dt.Rows[i]["BlogPic"].ToString();
                img.Width = System.Web.UI.WebControls.Unit.Pixel(160);
                img.Height = System.Web.UI.WebControls.Unit.Pixel(115);
                //this.Panel1.Controls.Add(img);
                this.Panel1.Controls.Add(new LiteralControl("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"160\" class=\"text_height\"><tr>"));
                this.Panel1.Controls.Add(new LiteralControl("<td valign=\"top\" style=\"height: 29px\"><a href='" + dt.Rows[i]["BlogUrl"].ToString() + "' target='_blank'>"));
                this.Panel1.Controls.Add(img);
                this.Panel1.Controls.Add(new LiteralControl("</a></td>"));
                //this.Panel1.Controls.Add(new LiteralControl("</tr></table>"));

                this.Panel1.Controls.Add(new LiteralControl("<br/>"));


                //<img src="img/tourist_blog_new.jpg" width="35" height="12" />
                //<a href="#"><span class="text_color">甜蜜攜手旅行去</span><br>2007.10.12義大利</a>
                //this.Panel1.Controls.Add(new LiteralControl("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"160\" class=\"text_height\"><tr>"));
                //this.Panel1.Controls.Add(new LiteralControl("<td valign=\"top\" style=\"height: 29px\"><img src=\"img/tourist_blog-3.jpg\" width=\"7\" height=\"8\" /></td> "));
                //this.Panel1.Controls.Add(new LiteralControl("<td valign=\"top\" style=\"height: 29px\"><a href='" + dt.Rows[i]["BlogUrl"].ToString() + "'><span class=\"text_color\">" + dt.Rows[i]["BlogName"].ToString() + "</span><br>" + dt.Rows[i]["BlogIntro"].ToString() + "</a></td>"));
                //if (dt.Rows[i]["BlogNew"].ToString() == "y")
                //{
                //    this.Panel1.Controls.Add(new LiteralControl("<td valign=\"top\" align=\"right\" width=\"35\" style=\"height: 29px\"><img src=\"img/tourist_blog_new.jpg\" width=\"35\" height=\"12\" /></td>"));
                //}
                this.Panel1.Controls.Add(new LiteralControl("</tr></table>"));
                this.Panel1.Controls.Add(new LiteralControl("<br/>"));
                this.Panel1.Controls.Add(new LiteralControl("<br/>"));
            }

        }

        dt.Dispose();
        da.Dispose();
        connect.Close();
        connect = null;
    }

    protected void getartblog()
    {
        string strsql = "";
        strsql += "select BlogName,BlogIntro,BlogUrl,BlogPic,BlogNew,BlogType,TbNumber from Tripblog ";
        strsql += "where TripNo='" + Convert.ToString(Request.QueryString["TripNO"]) + "' and BlogType = 'artisan'  ";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand cmd = new SqlCommand(strsql, connect);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        DataTable dt = new DataTable();

        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Image imga = new Image();
                imga.ID = "Imgbloga" + (i.ToString() + 1);
                imga.ImageUrl = @"~/tripblog/" + dt.Rows[i]["BlogPic"].ToString();
                imga.Width = System.Web.UI.WebControls.Unit.Pixel(160);
                imga.Height = System.Web.UI.WebControls.Unit.Pixel(115);
                // this.Panel2.Controls.Add(imga);

                // this.Panel2.Controls.Add(new LiteralControl("<br/>"));
                this.Panel2.Controls.Add(new LiteralControl("<br/>"));

                this.Panel2.Controls.Add(new LiteralControl("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"178\"><tr>"));
                this.Panel2.Controls.Add(new LiteralControl("<td width=\"47\"><a href='" + dt.Rows[i]["BlogUrl"].ToString() + "'target='_blank' >"));
                this.Panel2.Controls.Add(imga);
                this.Panel2.Controls.Add(new LiteralControl("</a></td><td></td> "));
                this.Panel2.Controls.Add(new LiteralControl("<td><a href='" + dt.Rows[i]["BlogUrl"].ToString() + "'><span class=\"text_color2\">" + " " + dt.Rows[i]["BlogName"].ToString() + "</span><br>" + " " + dt.Rows[i]["BlogIntro"].ToString() + "</a></td></tr></table>"));
                //if (dt.Rows[i]["BlogNew"].ToString() == "y")
                //{
                //    this.Panel2.Controls.Add(new LiteralControl("<td valign=\"top\" align=\"right\" width=\"35\" style=\"height: 29px\"><img src=\"img/tourist_blog_new.jpg\" width=\"35\" height=\"12\" /></td>"));
                //}
                this.Panel2.Controls.Add(new LiteralControl("</tr></table>"));
                this.Panel2.Controls.Add(new LiteralControl("<br/>"));
                this.Panel2.Controls.Add(new LiteralControl("<br/>"));
            }

        }
        dt.Dispose();
        da.Dispose();
        connect.Close();
        connect = null;
    }
    protected void gettwopic()
    {
        string strsql = " select * from twopic ";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand commNew = new SqlCommand(strsql, connect);
        SqlDataReader readerNew = commNew.ExecuteReader();
        while (readerNew.Read())
        {
            twopic.Text += "<div class=\"push_tour_tool\">";
            //twopic.Text += "<a href=\"" + readerNew.GetValue(4).ToString() + "\"><img src=\"NewPageFile/" + readerNew.GetValue(1).ToString() + "\" width=\"158\" height=\"127\" border=\"0\" /></a><br>";           
            twopic.Text += "<a href=\"#\" onClick=\"window.open('" + readerNew.GetValue(4).ToString() + "','','menubar=no,status=no,scrollbars=yes,top=200,left=200,toolbar=no,width=1024,height=490')\"><img src=\"NewPageFile/" + readerNew.GetValue(1).ToString() + "\" width=\"158\" height=\"127\" border=\"0\" /></a>";
            twopic.Text += "<div class=\"push_tour_data\"> " + readerNew.GetValue(2).ToString() + "<br><a href=\"#\" onClick=\"window.open('" + readerNew.GetValue(4).ToString() + "','','menubar=no,status=no,scrollbars=yes,top=200,left=200,toolbar=no,width=1024,height=490')\" class=\"push_tour_red\">";
            twopic.Text += readerNew.GetValue(3).ToString() + "</a></div><div class=\"push_tour_pic\"><a href=\"#\" onClick=\"window.open('" + readerNew.GetValue(4).ToString() + "','','menubar=no,status=no,scrollbars=yes,top=200,left=200,toolbar=no,width=1024,height=490')\"><img src=\"img/index6.jpg\" width=\"16\" height=\"16\" border=\"0\" /></a></div></div>";

        }
        commNew.Dispose();
        connect.Close();
        readerNew.Dispose();
    }

    protected void gettriplist()
    {
        string strsql = "";
        lit_triplist.Text = "";
        strsql += " select * from triplist_only ";
        strsql += " where trip_no='" + Convert.ToString(Request.QueryString["TripNO"]) + "'";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand commNew = new SqlCommand(strsql, connect);
        SqlDataReader readerNew = commNew.ExecuteReader();
        while (readerNew.Read())
        {
            lit_triplist.Text += "<div class=\"TripList_links\"><a href ='" + readerNew["Triplist_URL"].ToString() + "' target='_blank' >" + readerNew["Triplist_title"].ToString() + "</a><a href='" + readerNew["Triplist_URL"].ToString() + "' target='_blank' ><img src=\"img/index6.jpg\"></a>";
            lit_triplist.Text += "</div>";
        }
        commNew.Dispose();
        connect.Close();
        readerNew.Dispose();
    }
}
