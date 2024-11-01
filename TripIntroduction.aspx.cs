using ExpertPdf.HtmlToPdf.PdfDocument;
using iTextSharp.text;
using iTextSharp.text.pdf;

using System;
using System.Activities.Expressions;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

public partial class TripIntroduction : System.Web.UI.Page
{
    string url = "";
    string Group_Name_no = "";
    string _strTripNo = "";
    string _strDate = "";
    string _strType = "";
    string _strIsFIT = "";
    string _strTC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        clsFunction.Check.CheckSession();
        _strTripNo = Convert.ToString(Request["TripNo"] + "").Trim();
        _strDate = Convert.ToString(Request["Date"] + "").Trim();
        _strType = Convert.ToString(Request["type"] + "").Trim();
        _strTC = Convert.ToString(Request["tc"] + "").Trim();

        if (string.IsNullOrEmpty(_strTripNo))
        {
            Response.Redirect("~/");
            return;
        }

        // 判斷是不是為自由行，若是自由行，不需要判斷日期和航班的參數
        if (fnRtnIsFit() == "")
        {
            if (string.IsNullOrEmpty(_strTripNo) || string.IsNullOrEmpty(_strDate) || string.IsNullOrEmpty(_strType))
            {
                Response.Redirect("~/");
                return;
            }
            if (!clsFunction.Check.IsDate(_strDate))
            {
                Response.Redirect("~/");
                return;
            }
        }

        if (Convert.ToString(Request.QueryString["isprint"]) != "true")
        {
            clsFunction.Check.CheckSession();
        }
        if (IsPostBack) return;
        nav_GroupPrice.Attributes.Add("data-target", "#" + section_detail.ClientID);
        nav_FitPrice.Attributes.Add("data-target", "#" + section_FitPriceList.ClientID);

        getSource();
        GetData();
        getFitCostList();
        GetTourDay(Group_Name_no);
        GetTripIntro();
        GetTripFeat2020();

        if (litTripFeat.Text == "" && litHighlights.Text == "" && litIntro.Text == "")
        {
            GetTripFeat2019();
        }

        if (!IsPostBack)
        {
            Session["TripNo_s"] = _strTripNo;

            if (!string.IsNullOrEmpty(_strTC))
            {
                //分享出去的資料，需隱藏公司相關資訊
                Page.Title = litTitle.Text.Trim();
                Header_Menu1.Visible = false;
                Foot.Visible = false;
                litTitleUrl.Visible = false;
            }
            else
            {
                description.Text = "<meta name='description' content='【旅遊看巨匠，世界不一樣】歐洲、遊輪、紐澳、南亞、中東、美洲、非洲、海島、南亞、日本、中國、機+酒，盡心提供最符合您期待的旅遊規畫，是我們的責任。' />";
            }
        }
    }

    #region === Get ===
    private void GetData()
    {
        string Grop_numb = "";
        string Van_Number = "";
        string strSql = "";
        strSql = "select Area.Area_Name,Group_Category.Group_Category_Name,Group_Name.Group_Name,Area.Area_No,Group_Category.Group_Category_No";
        strSql += " ,Grop_Expect,Visa,TourTax,Grop_numb,mappic_name2,Grop_Tour,TourType,isnull(Grop_Close,'') as Grop_Close,Tour_Kind";
        strSql += " ,Trip.Group_Name_No,Grop.Van_Number,Trip_Day,Down_Payment,TourTip,AREA_CODE,isnull(Trip_Remark,'') as Trip_Remark ,Trip_Pic,Home_Title_Name";
        strSql += " ,Trip.Trip_FIT_Price,Grop.Grop_Intro";
        strSql += " from trip";
        strSql += " left join Grop on Trip.Trip_no = Grop.Trip_no and grop.grop_depa = @grop_depa and Grop_Liner = @Grop_Liner and grop.hidden <> 'y'";
        strSql += " left join Area on Area.Area_No = Trip.Area_No";
        strSql += " left join Group_Category on Group_Category.Group_Category_No = Trip.SecClass_No";
        strSql += " left join Group_Name on Group_Name.Group_Name_No = Trip.Group_Name_No";
        strSql += " left join mappic on Trip.mappic_no = mappic.mappic_no";
        strSql += " where Trip.Trip_No = @Trip_No";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {

            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@Trip_No", _strTripNo));
            comm.Parameters.Add(new SqlParameter("@grop_depa", _strDate));
            comm.Parameters.Add(new SqlParameter("@Grop_Liner", (_strType == "none" ? "" : _strType)));
            SqlDataReader reader = comm.ExecuteReader();

            if (reader.Read())
            {
                hidGropName.Value = reader["Group_Name"].ToString();
                hidGropNumb.Value = reader["Grop_numb"].ToString();
                Grop_numb = reader["Grop_numb"].ToString();
                Van_Number = reader["Van_Number"].ToString();
                Group_Name_no = reader["Group_Name_no"].ToString();

                /* 網站資料 */
                litTitleUrl.Text += "<a href='/index.aspx'>首頁</a><a href='/exh/Exhibition.aspx?area=" + reader["Area_No"].ToString() + "'>" + reader["Area_Name"].ToString() + "</a>";
                litTitleUrl.Text += "<a href='/exh/Exhibition.aspx?area=" + reader["Area_No"].ToString() + "&no=" + reader["Group_Category_No"].ToString() + "'>" + reader["Group_Category_Name"].ToString() + "</a>";
                litTitleUrl.Text += "<a href='/ClassifyProduct.aspx?TripNo=" + _strTripNo + "'>其他出發日</a>";

                litTopGrop.Text += "<li>機位數：<span class='seat'>" + reader["Grop_Expect"].ToString() + "</span>/位</li>";
                litTopGrop.Text += "<li>班機：<span class='plant'>" + _strType + "</span></li>";
                litTopGrop.Text += "<li>簽證：<span class='visa'>" + GetVisa(reader["Visa"].ToString()) + "</span></li>";
                litTopGrop.Text += "<li>稅金：<span class='tax'>" + GetTax(reader["TourTax"].ToString()) + "</span></li>";

                litTitle.Text = reader["Group_Name"].ToString();
                //行程優惠
                if (reader["Trip_Remark"].ToString().Trim() != "")
                {
                    litDiscount.Text = "<div class='TripMain-discount'><h4>行程優惠</h4><p>";
                    litDiscount.Text += reader["Trip_Remark"].ToString() + "</p></div>";
                }
                //Map
                if (reader["mappic_name2"].ToString().Length > 1)
                {
                    litMap.Text = "<div class='TripMain-list map-hide-pc TPO6'><a href='" + String.Format(url + "WaterMark.ashx?ImageUrl={0}&ImageComment={1}&Icon={2}", url + "MapPic/" + reader["mappic_name2"].ToString() + "", "", "") + "' target='_blank'>行程地圖</a></div>";
                    litMap2.Text = "<section id='section-map'><h3 class='main-title'>行程地圖</h3>";
                    litMap2.Text += "<div class='container'><div class='section-map-pic'>";
                    litMap2.Text += "<a href='" + String.Format(url + "WaterMark.ashx?ImageUrl={0}&ImageComment={1}&Icon={2}", url + "MapPic/" + reader["mappic_name2"].ToString() + "", "", "") + "' target='_blank'>";
                    litMap2.Text += "<img src='" + String.Format(url + "WaterMark.ashx?ImageUrl={0}&ImageComment={1}&Icon={2}", url + "MapPic/" + reader["mappic_name2"].ToString() + "", "", "") + "' alt=''></a>";
                    litMap2.Text += "</div></div></section>";
                }
                //日期價錢報名
                litJoin.Text += "<div class='TripMain-orderTool'>";
                if (_strDate != "")
                { litJoin.Text += "<div class='TripMain-date'>" + _strDate + "出發</div>"; }
                litJoin.Text += "<div class='TripMain-price'>" + GetGrop_Tour(GetAgentPrice(_strTripNo, _strDate).ToString(), reader["TourType"].ToString(), reader["Group_Category_No"].ToString(), reader["Area_No"].ToString(), reader["Trip_FIT_Price"].ToString()) + "</div>";

                litJoin.Text += "<div class='web-share-bttom' id='CopyURL'><a href='" + getJoinUrl(_strTripNo) + "'>複製網址</a></div>";

                if (CheckJoin(reader["Grop_Close"].ToString(), _strDate, reader["Group_Category_No"].ToString(), reader["Tour_Kind"].ToString(), reader["Group_Name_No"].ToString(), reader["TourType"].ToString()))
                {
                    if (_strIsFIT == "Y")
                    {
                        litJoin.Text += "<div class='TripMain-btn'><a href='" + getJoinUrl(_strTripNo) + "'>點我報名</a></div>";
                    }
                    else
                    {
                        litJoin.Text += "<div class='TripMain-btn'><a href='" + getJoinUrl(reader["Van_Number"].ToString()) + "'>點我報名</a></div>";
                    }
                }
                else if (_strIsFIT == "Y")
                {
                    litJoin.Text += "<div class='TripMain-btn'><a href='" + getJoinUrl(_strTripNo) + "'>點我報名</a></div>";
                }
                litJoin.Text += "</div>";
                //手機價錢報名
                litJoin.Text += "<div class='orderTool-m'>";
                litJoin.Text += "<div class='orderTool-price'>" + GetGrop_Tour(reader["Grop_Tour"].ToString(), reader["TourType"].ToString(), reader["Group_Category_No"].ToString(), reader["Area_No"].ToString(), reader["Trip_FIT_Price"].ToString()).Replace("/人", "") + "</div>";
                if (CheckJoin(reader["Grop_Close"].ToString(), _strDate, reader["Group_Category_No"].ToString(), reader["Tour_Kind"].ToString(), reader["Group_Name_No"].ToString(), reader["TourType"].ToString()))
                {
                    if (_strIsFIT == "Y")
                    {
                        litJoin.Text += "<div class='TripMain-btn'><a href='" + getJoinUrl(_strTripNo) + "'>點我報名</a></div>";
                    }
                    else
                    {
                        litJoin.Text += "<div class='orderTool-btn'><a href='" + getJoinUrl(reader["Van_Number"].ToString()) + "'>報名</a></div>";
                    }
                }
                else if (_strIsFIT == "Y")
                {
                    litJoin.Text += "<div class='orderTool-btn'><a href='" + getJoinUrl(_strTripNo) + "'>報名</a></div>";
                }
                litJoin.Text += "</div>";

                for (int i = 1; i <= Convert.ToInt32(reader["Trip_Day"]); i++)
                {
                    if (i == 0) { litDay.Text += "<a href='javascript:;' class='godays bt_active' data-target='#day" + i.ToString("00") + "'>" + i.ToString("00") + "</a>"; }
                    else { litDay.Text += "<a href='javascript:;' class='godays' data-target='#day" + i.ToString("00") + "'>" + i.ToString("00") + "</a>"; }
                }

                litRemind.Text = getRemind(reader["Area_No"].ToString(), reader["Group_Category_No"].ToString());

                litGrop.Text += "<div class='TP_block'><div class='TP_title'>◆行程名稱：</div>";
                litGrop.Text += "<span id='TP_groupNumber'>" + reader["Grop_numb"].ToString() + "</span>";
                litGrop.Text += "<span id='TP_Name'>" + reader["Group_Name"].ToString() + "</span>";
                litGrop.Text += "</div>";
                litGrop.Text += "<div class='TP_block'>";
                if (_strDate != "")
                {
                    litGrop.Text += "<div class='TP_title'>◆出發日期：</div>";
                    litGrop.Text += "<span class='TP_date'>" + _strDate + "~";
                    litGrop.Text += Convert.ToDateTime(_strDate).AddDays(Convert.ToInt32(reader["Trip_Day"]) - 1).ToString("yyyy/MM/dd");
                    litGrop.Text += " 共" + reader["Trip_Day"].ToString() + "天</span>";
                    litGrop.Text += "</div>";
                }
                litGrop.Text += "<div class='TP_block'><div class='TP_title'>◆每人訂金：</div>";
                litGrop.Text += "<span class='TP_deposit'>$" + String.Format("{0:0,0}", reader["Down_Payment"]) + "</span>";
                litGrop.Text += "</div>";
                //包機場稅燃油費，不含小費
                if (reader["TourTax"].ToString().ToUpper() == "Y") { litVisa.Text = "包機場稅燃油費"; }
                else { litVisa.Text = "不含機場稅燃油費"; }
                if (reader["TourTip"].ToString().ToUpper() == "Y") { litVisa.Text += "，含小費"; }
                else { litVisa.Text += "，不含小費"; }

                if (reader["AREA_CODE"].ToString() == "Area18") { litContent.Text = getContent("1"); }
                else { litContent.Text = getContent(""); }

                string strSalePrice = "";
                Format obj = new Format();
                if (obj.IsNumeric(reader["Grop_Tour"].ToString()))
                {
                    int intSalePrice = Convert.ToInt32(reader["Grop_Tour"].ToString());
                    if (intSalePrice > 5000)
                    {
                        litPay1.Text = "<div class=\"TP_left_money_all\">$" + intSalePrice.ToString("N0") + "</div>";
                        strSalePrice = "$" + intSalePrice.ToString("N0") + "";
                    }
                    else if (reader["Group_Category_No"].ToString() == "86" && intSalePrice > 3000) // 基隆起航，大於3000元就顯示
                    {
                        litPay1.Text = "<div class=\"TP_left_money_all\">$" + intSalePrice.ToString("N0") + "</div>";
                        strSalePrice = "$" + intSalePrice.ToString("N0") + "";
                    }
                    else if (reader["Area_No"].ToString() == "31" && intSalePrice > 300) // 台灣館，大於300元就顯示
                    {
                        litPay1.Text = "<div class=\"TP_left_money_all\">$" + intSalePrice.ToString("N0") + "</div>";
                        strSalePrice = "$" + intSalePrice.ToString("N0") + "";
                    }
                    else if (intSalePrice <= 5000 && intSalePrice > 0)
                    {
                        litPay1.Text = "<div class=\"TP_left_money_none\">來電洽詢</div>";
                        strSalePrice = "來電洽詢";
                    }
                    else
                    {
                        litPay1.Text = "<div class=\"TP_left_money_none\">不提供</div>";
                        strSalePrice = "不提供";
                    }
                }

                if (reader["Grop_numb"].ToString() == "")
                {
                    section_detail.Visible = false;
                    nav_GroupPrice.Visible = false;
                }
                //行程重點
                GetTripHighlights(reader["Home_Title_Name"].ToString(), reader["Trip_Pic"].ToString());


                // 複製網址 
                string strGrop_Tour = reader["Grop_Tour"].ToString();
                litTourCopyData.Text = "";
                litTourCopyData.Text += "<div class=\"form-group\">";
                litTourCopyData.Text += "【" + hidGropName.Value + "】";
                litTourCopyData.Text += "</div>";
                litTourCopyData.Text += "<div class=\"form-group\">";
                litTourCopyData.Text += RemoveHTMLTag(reader["Grop_Intro"].ToString());
                litTourCopyData.Text += "</div>";
                litTourCopyData.Text += "<div class=\"form-group\">";
                litTourCopyData.Text += "<br />";
                litTourCopyData.Text += "</div>";
                litTourCopyData.Text += "<div class=\"form-group\">";
                litTourCopyData.Text += "日期：" + _strDate + "";
                litTourCopyData.Text += "</div>";
                litTourCopyData.Text += "<div class=\"form-group\">";
                litTourCopyData.Text += "售價：" + strGrop_Tour + "(每席訂金 $" + String.Format("{0:0,0}", reader["Down_Payment"]) + "元)";
                litTourCopyData.Text += "</div>";
                litTourCopyData.Text += "<div class=\"form-group\">";
                litTourCopyData.Text += "<br />";
                litTourCopyData.Text += "</div>";
                litTourCopyData.Text += "<div class=\"form-group\">";
                litTourCopyData.Text += "點我瞭解更多";
                litTourCopyData.Text += "</div>";
                litTourCopyData.Text += "<div class=\"form-group\">";
                litTourCopyData.Text += "" + Request.Url.AbsoluteUri.Replace("b2b.artisan.com.tw", "sharetour.grp.com.tw") + "&tc=y";
                litTourCopyData.Text += "</div>";
                litTourCopyData.Text += "<div class=\"form-group\">";
                litTourCopyData.Text += "<br />";
                litTourCopyData.Text += "</div>";
            }
            comm.Dispose();
            reader.Close();

            GetPlan(Grop_numb);
            Get_TourPrice(Van_Number);
        }
        catch { }
        finally { connect.Close(); }
    }
    //行程重點
    private void GetTripHighlights(string title,string pic)
    {
        DataTable dt = new DataTable();
        string strSql = "";
        strSql = "select Area.Area_Name,Group_Category.Group_Category_Name,Group_Name.Group_Name,Area.Area_No,Group_Category.Group_Category_No";
        strSql += " ,Grop_Expect,Visa,TourTax,Grop_numb,mappic_name2,Grop_Tour,TourType,isnull(Grop_Close,'') as Grop_Close,Tour_Kind";
        strSql += " ,Trip.Group_Name_No,Grop.Van_Number,Trip_Day,Down_Payment,TourTip,AREA_CODE,isnull(Trip_Remark,'') as Trip_Remark ,Trip_Pic,Home_Title_Name";
        strSql += " ,Trip.Trip_FIT_Price";
        strSql += " from trip";
        strSql += " left join Grop on Trip.Trip_no = Grop.Trip_no and grop.grop_depa = @grop_depa and Grop_Liner = @Grop_Liner and grop.hidden <> 'y'";
        strSql += " left join Area on Area.Area_No = Trip.Area_No";
        strSql += " left join Group_Category on Group_Category.Group_Category_No = Trip.SecClass_No";
        strSql += " left join Group_Name on Group_Name.Group_Name_No = Trip.Group_Name_No";
        strSql += " left join mappic on Trip.mappic_no = mappic.mappic_no";
        strSql += " where Trip.Trip_No = @Trip_No";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            connect.Open();

            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@Trip_No", _strTripNo));
            SqlDataReader reader = comm.ExecuteReader();

            if (reader.HasRows)
            {
                litHighlights.Text += "<section id='section-highlights'><div class='container'><div class='highlights_tool'><div class='highlights_pic'><div class='img_tool'>";
                litHighlights.Text += "<img src='" + pic + "' alt=''>";
                litHighlights.Text += "<div class='PIC_description'>" + title + "</div>";
                litHighlights.Text += "</div></div><div class='highlights_content'>";
                litHighlights.Text += "<h2>行程重點<span>HIGHLIGHTS</span></h2><div class='highlights_list'>";
                while (reader.Read())
                {
                    litHighlights.Text += "<p>" + reader["title"].ToString() + "</p>";
                }
                comm.Dispose();
                reader.Close();
                litHighlights.Text += "</div>";
                litHighlights.Text += "</div></div></div></section>";
            }
            else
            {
                if(title != "" || pic != "")
                {
                    litHighlights.Text += "<section id='section-highlights'><div class='container'><div class='highlights_tool'><div class='highlights_pic'><div class='img_tool'>";
                    litHighlights.Text += "<img src='" + pic + "' alt=''>";
                    litHighlights.Text += "<div class='PIC_description'>" + title + "</div>";
                    litHighlights.Text += "</div></div><div class='highlights_content'>";
                    litHighlights.Text += "</div></div></div></section>";
                }
            }
        }
        catch { }
        finally { connect.Close(); }
    }
    //行程特色2019
    private void GetTripFeat2019()
    {
        string TripFeat_Intro = "";
        string strSql = "";
        strSql = " select TripFeat_Title,TripFeat_Intro";
        strSql += " from TripFeat";
        strSql += " where Trip_No = @Trip_No";
        strSql += " order by TripFeat_No";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand comm3 = new SqlCommand(strSql, connect);
        comm3.Parameters.Add(new SqlParameter("@Trip_No", _strTripNo));
        SqlDataReader reader3 = comm3.ExecuteReader();

        if (reader3.HasRows)
        {
            TripFeat.Text += "<section id='section-featureOld' ><div class='container'><div class='trip_feature'><div class='tour_characteristic_tool_Intro'>";
            while (reader3.Read())
            {
                TripFeat_Intro = reader3["TripFeat_Intro"].ToString();
                TripFeat_Intro = Regex.Replace(TripFeat_Intro, "src=\"http://www.artisan.com.tw", "src=\"", RegexOptions.IgnoreCase);
                TripFeat_Intro = Regex.Replace(TripFeat_Intro, "src=\"/fckeditor/editor/filemanager", "src=\"http://www.artisan.com.tw/fckEditor/editor/filemanager", RegexOptions.IgnoreCase);
                TripFeat_Intro = Regex.Replace(TripFeat_Intro, "src=\"/newtrip/fckeditor/editor/filemanager", "src=\"http://www.artisan.com.tw/NewTrip/fckEditor/editor/filemanager", RegexOptions.IgnoreCase);
                TripFeat_Intro = Regex.Replace(TripFeat_Intro, "src=\"/zupload/", "src=\"http://www.artisan.com.tw/zupload/", RegexOptions.IgnoreCase);
                TripFeat.Text += TripFeat_Intro;

                //TripFeat.Text += reader3["TripFeat_Intro"].ToString();

            }
            TripFeat.Text += "</div></div></div></section>";

        }
        else
        {
            sectionfeaturedata.Visible = false;
        }

        reader3.Close();
        comm3.Dispose();
        connect.Close();
    }
    //行程特色2020
    private void GetTripFeat2020()
    {
        DataTable dt = new DataTable();
        string strSql = "";
        strSql = " select * from TripFeat2020";
        strSql += " where Trip_No = @Trip_No";
        strSql += " order by orderby";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            connect.Open();

            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@Trip_No", _strTripNo));
            SqlDataReader reader = comm.ExecuteReader();

            if (reader.HasRows)
            {
                litTripFeat.Text += "<section id='section-tripBanner'><div class='container'><div class='owl-carousel owl-theme' id='TripBanner-owl-carousel'>";

                while (reader.Read())
                {
                    if (reader["isPic"].ToString() == "True")
                    {
                        litTripFeat.Text += "<figure class='tripBanner-box'><div class='box_container' style='background-image: url(" + reader["Url"].ToString() + ");'></div><figcaption class='tripBanner-flex'>";
                        litTripFeat.Text += "<h2>" + reader["Title"].ToString() + "<span>SPECIAL PLAN</span></h2>";
                        litTripFeat.Text += "<p>" + reader["Intro"].ToString() + "</p>";
                        litTripFeat.Text += "</figcaption></figure>";
                    }
                    else
                    {
                        litTripFeat.Text += "<figure class='tripBanner-video'><div class='video_container'>";
                        litTripFeat.Text += "<iframe width='100%' height='600' src='" + reader["Url"].ToString() + "' frameborder='0' allow='accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture' allowfullscreen=''></iframe>";
                        litTripFeat.Text += "</div><figcaption class='tripBanner-flex'>";
                        litTripFeat.Text += "<h2>" + reader["Title"].ToString() + "<span>SPECIAL PLAN</span></h2>";
                        litTripFeat.Text += "<p>" + reader["Intro"].ToString() + "</p>";
                        litTripFeat.Text += "</figcaption></figure>";
                    }
                }
                comm.Dispose();
                reader.Close();

                litTripFeat.Text += "</div></div></section>";
            }
        }
        catch { }
        finally { connect.Close(); }
    }
    //行程介紹
    private void GetTripIntro()
    {
        DataTable dt = new DataTable();
        string strSql = "";
        strSql = " select TripIntro.Title,TripIntro.Intro,PictureUrl,TripIntro.TripType as title2,TripIntro.Number from TripIntro";
        strSql += " where Trip_No = @Trip_No";
        strSql += " and MainNumber = 0";
        //strSql += " and (ISNULL(TripIntro.Title,'') <> '' or ISNULL(TripIntro.Intro,'') <> '')";
        strSql += " order by OrderBy,MainNumber,TripType";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            connect.Open();

            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@Trip_No", _strTripNo));
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                litIntro.Text += "<section class='section-spec'><h4 class='spec-title'><span>" + reader["title2"].ToString() + "</span></h4>";
                litIntro.Text += "<div class='spec-content'><figure class='spec-photo'>";
                if (reader["PictureUrl"].ToString() == "") { litIntro.Text += "<div class='spec-photo-pic' style='background-image: url(https://api.fnkr.net/testimg/1300x600/00CED1/FFF/?text=img+placeholder);'></div>"; }
                else { litIntro.Text += "<div class='spec-photo-pic' style='background-image: url(" + reader["PictureUrl"].ToString() + ");'></div>"; }

                if (reader["Title"].ToString() != "" && reader["Intro"].ToString() != "")
                {
                    litIntro.Text += "<figcaption>";
                    if (reader["Title"].ToString() != "")
                    { litIntro.Text += "<h5>" + reader["Title"].ToString() + "</h5>"; }
                    if (reader["Intro"].ToString() != "")
                    { litIntro.Text += "<p>" + reader["Intro"].ToString() + "</p>"; }
                    litIntro.Text += "</figcaption>";
                }

                litIntro.Text += "</figure>";

                GetTripIntroItem(reader["Number"].ToString());

                litIntro.Text += "</div></section>";
            }
            comm.Dispose();
            reader.Close();
        }
        //catch { }
        finally { connect.Close(); }
    }
    //行程介紹-項目
    private void GetTripIntroItem(string Number)
    {
        DataTable dt = new DataTable();
        string strSql = "";
        strSql = " select * from TripIntro";
        strSql += " where Trip_No = @Trip_No";
        strSql += " and MainNumber = @MainNumber ";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            connect.Open();

            SqlDataAdapter da = new SqlDataAdapter(strSql, strConnString);
            da.SelectCommand.Parameters.Add(new SqlParameter("@Trip_No", _strTripNo));
            da.SelectCommand.Parameters.Add(new SqlParameter("@MainNumber", Number));
            da.Fill(dt);
        }
        catch { }
        finally { connect.Close(); }

        if (dt.Rows.Count > 0)
        {
            litIntro.Text += "<div class='spec-card'><div class='owl-carousel owl-theme Spec-owl-carousel'>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["PictureUrl"].ToString() == "") { litIntro.Text += "<figure class='spec-box'><img src='https://api.fnkr.net/testimg/400x270/00CED1/FFF/?text=img+placeholder'><figcaption>"; }
                else { litIntro.Text += "<figure class='spec-box'><img src='" + dt.Rows[i]["PictureUrl"].ToString() + "'><figcaption>"; }
                litIntro.Text += "<h5>" + dt.Rows[i]["Title"].ToString() + "</h5>";
                litIntro.Text += "<p>" + dt.Rows[i]["Intro"].ToString() + "</p>";
                litIntro.Text += "</figcaption></figure>";
            }
            litIntro.Text += "</div></div>";
        }
    }
    // 行程特色(天數)
    private void GetTourDay(string Group_Name_no)
    {
        string strSql = "";
        strSql = " select TripValue_Day,TripValue_Title,TripValue_Breakfast,b.LodgeMeal_Title as Breakfast_Name,TripValue_Lunch";
        strSql += " ,c.LodgeMeal_Title as Lunch_Name,TripValue_Dinner,d.LodgeMeal_Title as Dinner_Name,TripValue_Lodging,e.LodgeMeal_Title as Lodging_Name";
        strSql += " ,isnull(EDayPic_Name,'') as EDayPic_Name,ValueSec_Intro,TripValue_Lodging2,f.LodgeMeal_Title as Lodging_Name2,e.LodgeMeal_Url as Lurl1";
        strSql += " ,f.LodgeMeal_Url as Lurl2";
        strSql += " ,left(b.LodgeMeal_Intro,10) as Breakfast_Intro";//16
        strSql += " ,left(c.LodgeMeal_Intro,10) as Lunch_Intro";
        strSql += " ,left(d.LodgeMeal_Intro,10) as Dinner_Intro";
        strSql += " ,left(e.LodgeMeal_Intro,10) as Lodging_Intro";//19

        strSql += " ,left(f.LodgeMeal_Intro,10) as Lodging2_Intro";
        strSql += " ,g.LodgeMeal_Title as Lodging_Name3";
        strSql += " ,g.LodgeMeal_Url as Lurl3";
        strSql += " ,TripValue_Lodging3";
        strSql += " ,left(g.LodgeMeal_Intro,10) as Lodging3_Intro";
        strSql += " from TripValue";
        strSql += " left join LodgeMeal b on b.LodgeMeal_No = TripValue_Breakfast";
        strSql += " left join LodgeMeal c on c.LodgeMeal_No = TripValue_Lunch";
        strSql += " left join LodgeMeal d on d.LodgeMeal_No = TripValue_Dinner";
        strSql += " left join LodgeMeal e on e.LodgeMeal_No = TripValue_Lodging";
        strSql += " left join LodgeMeal f on f.LodgeMeal_No = TripValue_Lodging2";
        strSql += " left join LodgeMeal g on g.LodgeMeal_No = TripValue_Lodging3";
        strSql += " left join EDayPic on EDayPic.EDayPic_No = TripValue.EDayPic_No";
        strSql += " left join ValueSec on ValueSec.ValueSec_No = TripValue.ValueSec_No";
        strSql += " where TripValue.Trip_No=@Trip_No";
        strSql += " order by TripValue_Day";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand comm6 = new SqlCommand(strSql, connect);
        comm6.Parameters.Add(new SqlParameter("@Trip_No", _strTripNo));
        SqlDataReader reader6 = comm6.ExecuteReader();
        while (reader6.Read())
        {
            maintour.Text += "<div class='trip_day' id='day" + Convert.ToInt32(reader6["TripValue_Day"]).ToString("00") + "'>";
            maintour.Text += "<div class='trip_day_title trip_day_title_close'>";
            maintour.Text += "<div class='trip_day_number'>DAY<span>" + Convert.ToInt32(reader6["TripValue_Day"]).ToString("00") + "</span></div>";
            maintour.Text += "<h4>" + reader6["TripValue_Title"].ToString() + "</h4></div>";
            maintour.Text += "<div class='trip_day_content'>";
            maintour.Text += " " + reader6["ValueSec_Intro"].ToString().ToString().ToLower().Replace("src=\"http://www.artisan.com.tw", "src=\"").Replace("src=\"/fckeditor/editor/filemanager", "src=\"http://www.artisan.com.tw/fckEditor/editor/filemanager").Replace("src=\"/NewTrip/fckEditor/editor/filemanager", "src=\"http://www.artisan.com.tw/NewTrip/fckEditor/editor/filemanager").Replace("src=\"/zupload/", "src=\"http://www.artisan.com.tw/zupload/") + " ";

            maintour.Text += "<div class='trip_day_info'>";
            //餐食
            maintour.Text += "<div class='trip_day_info_food'><div class='trip_day_info_food_title'><img src='https://www.artisan.com.tw/images/20_tripDay_food.png'><h5>餐食</h5></div>";
            maintour.Text += "<div class='trip_day_info_food_content'>";
            maintour.Text += "早餐 " + reader6.GetValue(3).ToString() + "<br/>";
            maintour.Text += "午餐 " + reader6.GetValue(5).ToString() + "<br/>";
            maintour.Text += "晚餐 " + reader6.GetValue(7).ToString();
            maintour.Text += "</div></div>";
            //住宿
            maintour.Text += "<div class='trip_day_info_hotel'><div class='trip_day_info_hotel_title'><img src='https://www.artisan.com.tw/images/20_tripDay_hotel.png'><h5>住宿</h5></div>";
            maintour.Text += "<div class='trip_day_info_hotel_content'>";

            if (reader6.GetValue(14).ToString().Length > 2 && reader6.GetValue(9).ToString().Length > 2)
            {
                maintour.Text += " <a href =" + Format.Replace(reader6.GetValue(14).ToString(), getpath()) + " target =\"_blank\">" + Format.Replace(reader6.GetValue(9).ToString(), getpath()) + "</a> 或 <br/>";
            }
            else
            {
                maintour.Text += " " + Format.Replace(reader6.GetValue(9).ToString(), getpath()) + " 或 <br/>";
            }
            if (reader6.GetValue(13).ToString().Length > 2)
            {
                if (reader6.GetValue(15).ToString().Length > 2)
                {
                    maintour.Text += " <a href =" + Format.Replace(reader6.GetValue(15).ToString(), getpath()) + " target='_blank'>" + reader6.GetValue(13).ToString() + "</a> 或 <br/>";
                }
                else
                {
                    maintour.Text += Format.Replace(reader6.GetValue(13).ToString(), getpath()) + " 或 <br/>";
                }
            }

            if (reader6.GetValue(21).ToString().Length > 2)
            {
                if (reader6.GetValue(22).ToString().Length > 2)
                {
                    maintour.Text += " <a href =" + Format.Replace(reader6.GetValue(22).ToString(), getpath()) + " target='_blank'>" + reader6.GetValue(21).ToString() + "</a> 或 <br/>";
                }
                else
                {
                    maintour.Text += " " + "" + Format.Replace(reader6.GetValue(21).ToString(), getpath()) + " 或 <br/>";
                }
            }

            if (reader6.GetValue(9).ToString() != "溫暖的家")
            {
                maintour.Text += " 同等級 ";
                
                //20190802
                if (Group_Name_no == "14615" || Group_Name_no == "14643")
                {
                    maintour.Text = maintour.Text.Replace("或  同等級 ", "").Replace("或  同等級", "").Replace("同等級", "");
                }   
            }
            maintour.Text += "</div></div></div></div></div>";

            maintour.Text = maintour.Text.Replace("溫暖的家 或", "溫暖的家");
        }
        comm6.Dispose();
        reader6.Close();
        connect.Close();
    }
    // 簽證
    private string GetVisa(string strGrop_Visa)
    {
        switch (strGrop_Visa)
        {
            case "Y":
                return "含";
            case "N":
                return "不含";
            case "C":
                return "免簽";
            default:
                return strGrop_Visa;
        }
    }
    // 稅金
    private string GetTax(string strGrop_Tax)
    {
        switch (strGrop_Tax)
        {
            case "Y":
                return "含";
            case "N":
                return "不含";
            default:
                return "不含";
        }
    }
    // 團費
    private string GetGrop_Tour(string strGrop_Tour, string strTourType, string strGroup_Category_No, string strArea_No, string strTrip_FIT_Price)
    {
        if (strTourType.ToUpper() == "ITF1")
        { return "<span class='price-client'>旅展特惠價</span>"; }

        Format obj = new Format();
        if (strGrop_Tour == "" && strTourType == "" && obj.IsNumeric(strTrip_FIT_Price))
        { return "NT.<span class='price-client'>" + String.Format("{0:0,0}", Convert.ToDouble(strTrip_FIT_Price)) + "</span>起/人"; }

        if (obj.IsNumeric(strGrop_Tour))
        {
            double dblGrop_Tour = Convert.ToDouble(strGrop_Tour);

            if (dblGrop_Tour > 5000)
            {
                return "NT.<span class='price-client'>" + String.Format("{0:0,0}", dblGrop_Tour) + "</span>起/人";
            }
            else if (strGroup_Category_No == "86" && dblGrop_Tour > 3000) // 基隆起航，大於3000元就顯示
            {
                return "NT.<span class='price-client'>" + String.Format("{0:0,0}", dblGrop_Tour) + "</span>起/人";
            }
            else if (strArea_No == "31" && dblGrop_Tour > 300) // 台灣館，大於300元就顯示
            {
                return "NT.<span class='price-client'>" + String.Format("{0:0,0}", dblGrop_Tour) + "</span>起/人";
            }
            else
            { return "<span class='price-none'>來電洽詢</span>"; }
        }

        return "";
    }
    // 去程/回程
    protected void GetPlan(string gpnum)
    {
        string strSql = "";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            connect.Open();

            strSql = " select Aire_StartConutry,Aire_EndCountry,Aire_StartTime,Aire_EndTime,Aire_AddTime";
            strSql += " ,Aire_AirLine,Aire_Flight,Aire_Journey";
            strSql += " from Aire";
            strSql += " where Grop_Numb = @Grop_Numb";
            strSql += " AND ISNULL(Grop_Numb,'') <> ''";
            strSql += " order by Aire_Journey,Aire_Type";

            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@Grop_Numb", gpnum));
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                if (reader["Aire_Journey"].ToString() == "1")
                {
                    litPlanGo.Text += "<div class='plant-list'>";
                    litPlanGo.Text += "<span>" + reader["Aire_StartConutry"].ToString() + " " + reader["Aire_StartTime"].ToString() + " &rarr; ";
                    litPlanGo.Text += reader["Aire_EndCountry"].ToString() + " " + reader["Aire_EndTime"].ToString() + " " + reader["Aire_AddTime"].ToString() + "</span>";
                    litPlanGo.Text += "<span>" + reader["Aire_AirLine"].ToString() + reader["Aire_Flight"].ToString() + "</span>";
                    litPlanGo.Text += "</div>";
                }
                if (reader["Aire_Journey"].ToString() == "2")
                {
                    litPlanEnd.Text += "<div class='plant-list'>";
                    litPlanEnd.Text += "<span>" + reader["Aire_StartConutry"].ToString() + " " + reader["Aire_StartTime"].ToString() + " &rarr; ";
                    litPlanEnd.Text += reader["Aire_EndCountry"].ToString() + " " + reader["Aire_EndTime"].ToString() + " " + reader["Aire_AddTime"].ToString() + "</span>";
                    litPlanEnd.Text += "<span>" + reader["Aire_AirLine"].ToString() + reader["Aire_Flight"].ToString() + "</span>";
                    litPlanEnd.Text += "</div>";
                }
            }
            reader.Close();
            comm.Dispose();
        }
        catch { }
        finally { connect.Close(); }
    }

    private void getSource()
    {
        string strSql = "";
        strSql += " SELECT Source_Agent_No FROM [Trip]";
        strSql += " WHERE Trip_No = @Trip_No";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@Trip_No", _strTripNo));
            SqlDataReader reader = comm.ExecuteReader();

            if (reader.Read())
            {
                switch (reader["Source_Agent_No"].ToString())
                {
                    case "A0001"://凱旋旅行社
                        url = "https://www.artisan.com.tw/";
                        break;
                    case "A0002"://巨大旅行社
                        url = "https://www.gianttour.com.tw/";
                        break;
                    case "A0003"://遠捷旅行社   
                        break;
                    case "A0004"://典華旅行社
                        url = "http://www.luxetravel.com.tw/";
                        break;
                    case "A0000": //測試的資料庫
                        break;
                    default:
                        url = "https://www.artisan.com.tw/";
                        break;
                }
                sa.Value = reader["Source_Agent_No"].ToString();
            }
            reader.Close();
            comm.Dispose();
        }
        catch { }
        finally { connect.Close(); }
    }

    private string getJoinUrl(string no)
    {
        if (Session["PERNO"] != null)
        {
            if (!string.IsNullOrEmpty(Session["PERNO"].ToString()))
            {
                if (_strIsFIT == "Y")
                    return "/FitApply/step1.aspx?n=" + no;
                else
                    return "/OLApply/Apply1.aspx?n=" + no;
            }
            else
            {
                return "/";
            }
        }
        else
        {
            return "/";
        }
    }

    private Boolean CheckJoin(string strGrop_Close, string strGrop_Depa, string strGroup_Category_No, string strTour_Kind, string strGroup_Name_No, string strTourType)
    {
        // 典藏的不能線上報名
        //if (strTourType.ToUpper() == "典藏") { return false; }
        // 旅展特惠的不能線上報名
        if (strTourType.ToUpper() == "ITF1") { return false; }
        // 結團不能再報名
        if (strGrop_Close == "True") { return false; }
        //郵輪不能線上報名
        //if (DropDownList1.SelectedValue == "7") { return false; }
        //if (strArea_No == "7") { return false; }
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
    }

    private Boolean fn_XML_Cruises(string strGroup_Name_No)
    {
        try
        {
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
        }
        catch { return false; }

        return false;
    }

    private string getpath()
    {
        string strpath = Server.MapPath(@"~\App_Data\ForStr.txt");

        return strpath;
    }
    //貼心提醒
    private string getRemind(string sel, string strGC_No)
    {
        if (strGC_No == "86") return "";

        string str = "";

        switch (sel)
        {
            case "11":
                #region === 北美 ===
                str = "<h3>旅客篇</h3>";
                str += "<ol class='trip_remind_area'>";
                str += "<li>未滿20歲之未成年人，未與法定代理人一同報名參加旅遊行程時，須得法定代理人之同意，報名始為有效！為確認您的報名有徵得法定代理人之同意，請您記得將旅行社所給旅遊定型化契約書或同意書，提供給您的法定代理人簽名後並繳回，報名手續始有效完成！</li>";
                str += "<li>團體旅遊時間安排需團員相互配合，為顧及全體旅客之權益，若有嬰幼兒同行時，可能無法妥適兼顧，所以請貴賓於報名時，需多方考量帶嬰幼兒同行可能產生的不便，以避免造成您和其他團員的不悅與困擾。</li>";
                str += "<li>為考量旅客自身之旅遊安全，並顧及同團其他旅客之旅遊權益，凡年滿70歲以上或行動不便之貴賓，建議須有家人或友人同行，若無陪同者，請在預訂前如實告知敝公司，讓我們可以為您提供您最適合的建議，並做出相應安排(以安全為優先)。</li>";
                str += "<li>飯店房型為兩人一室，若無同行者一同報名參加，本公司得協助安排與當團其它同性別團員或領隊進行分房（但無法保證）。若個人需指定單人入住，請於報名時主動告知業務人員，並按房型補足單人房價差，實際價差費用，悉以當團說明公布為準。</li>";
                str += "<li>團體安排之房型皆為禁菸房，請勿在房內抽菸；若逕行於房內抽菸，經查獲須配合各旅館規範繳交鉅額罰金，通常金額約在300美金起跳，詳細金額依照各旅館索賠為主。</li>";
                str += "<li>如於行程中有特殊之需求，例如兒童餐、忌葷或一張大床等，請於出發前15日提出以利作業。</li>";
                str += "<li>素食：因各地風俗民情不同，國外的素食習慣大多是可以食用蔥、薑、蒜、蛋、奶等，除華僑開設的中華料理餐廳外，多數僅能以蔬菜、豆腐等食材料理為主；若為飯店內用餐或一般餐廳使用自助餐，亦多數以蔬菜、漬物、水果等佐以白飯或麵食類。故敬告素食貴賓，海外團體素食餐之安排，無法如同在台灣般豐富且多變化，故建議素食貴賓能多多見諒並自行準備素食罐頭或泡麵等，以備不時之需。</li>";
                str += "<li>行程無法延長住宿天數，更改日期及航班，旅客若中途脫隊，視同自願放棄，恕不另外退費。</li>";
                str += "<li>行程於國外如遇塞車時，請貴賓們稍加耐心等候。如塞車情形嚴重，而會影響到行程或餐食的安排時，為維護旅遊品質及貴賓們的權益，我們將為您斟酌調整並妥善安排旅遊行程，敬請貴賓們諒解。</li>";
                str += "<li>行前或是旅途中，因天候因素、航班變更(取消)、交通工具取消或道路阻斷等不可抗力或不可歸責與旅行社之事由，導致無法依預定之旅程、交通、食宿或遊覽項目等履行時，為維護旅遊團體之安全利益，旅行社保留調整、變更行程之權利。</li>";
                str += "<li>北美地區國家公園以及山區氣候較不穩定，若遇暴風雪或天氣不佳的狀況，有些景點將會不預期關閉，或有些路面結冰或積雪，為了安全考量，若遇以上情形將會取消前往景點，不便之處，敬請見諒！</li>";
                str += "</ol>";
                str += "<h3>簽證篇</h3>";
                str += "<ol class='trip_remind_area'>";
                str += "本行程所載之護照、簽證相關規定，對象均為持中華民國護照之旅客，內載有國民身分證統一編號的有效中華民國護照(護照需有半年以上效期)，若貴客擁有雙重國籍或持他國護照，報名時並請告知您的服務人員，則須再查明相關規定。<br>";
                str += "★加拿大ETA(電子旅行証)計劃從2016年11月10日入境日起開始實施★<br>";
                str += "ETA是對免簽證的外籍人士乘搭飛機進入加拿大時的一項新入境要求。此旅行証以電子方式與您的護照相聯，有效期為五年或到護照失效日期爲止，以先到期者為准。<br>";
                str += "<li>旅客持有之台灣護照且具備國民身分證號碼及其發照機關為MINISTRY OF FOREIGN AFFAIRS。<br>(護照需在出發日當日起算有效期六個月以上)</li>";
                str += "<li>加拿大永久居民乘搭飛機進入加拿大無需持有ETA，但仍需持有加拿大永久居民卡入境，否則將可能無法登上前往加拿大的飛機。</li>";
                str += "<li>美國永久居民乘搭飛機進入加拿大需持有ETA(以及美國綠卡)，如經陸路或海路進入加拿大則不需要ETA。</li>";
                str += "<li>申請ETA費用$7加幣(若未含ETA行程，辦理將酌收手續費，實際費用請詢問業務人員)，若ETA申辦未核准，此費用無法退費。</li>";
                str += "註1：本行程所載之護照、簽證相關規定，對象均為持中華民國護照之旅客，若貴客擁有雙重國籍或持他國護照，請先自行查明相關規定，報名時並請告知您的服務人員<br>";
                str += "註2：內政部入出國及移民署全球資訊網臺灣地區無戶籍國民申請在臺灣地區定居～未滿20歲設立戶籍後首次出國，應先向外交部申請含有國民身分證字號之我國護照，始可持憑出國。<br>";
                str += "註3：免簽證並不代表您自動獲准入境加拿大，如果您過去曾簽證被拒，現在仍有可能被拒絕進入加拿大，建議您可先跟移民簽證組討論您的狀況。<br>";
                str += "<br>";
                str += "ETA資訊網站 http://www.cic.gc.ca/english/visit/eta.asp<br>";
                str += "查詢是否有需要申請ETA網站 http://www.cic.gc.ca/english/visit/visas.asp<br>";
                str += "<br>";
                str += "詳細資料請參考加拿大註台北貿易辦事處。<br>";
                str += "加拿大駐台北貿易辦事處(CTOT)<br>";
                str += "台北市信義區11047松智路1號6樓<br>";
                str += "電話(02)8723-3000  傳真(02)8723-3592<br>";
                str += "<br>";
                str += "★美國免簽證計劃從2012年11月1日起開始實施★<br>";
                str += "<br>";
                str += "美國於2012年10月2日宣布台灣加入免簽證計劃(簡稱VWP)。從2012年11月1日起符合資格之台灣護照持有人，若滿足特定條件，即可赴美從事觀光或商務達90天，無需辦理美簽。<br>";
                str += "<br>";
                str += "一、資格如下：<br>";
                str += "<li>旅客持有之台灣護照為2008年12月29日當日或以後核發之生物辨識電子護照(晶片護照)，且具備國民身分證號碼。(護照需在出發日當日起算有效期六個月以上)</li>";
                str += "<li>旅客前往美國洽商或觀光，並且停留不超過90天。(過境美國通常也適用)</li>";
                str += "<li>倘以飛機或船舶入境，須為核准之運輸業者(檢視核准之運輸業者名單)，並且有前往他國目的地之回程票。</li>";
                str += "<li>旅客已透過旅行許可電子系統(ESTA)取得以VWP入境之授權許可。</li>";
                str += "註：以下職業工作者及申請者，不得申請ESTA以VMP方式入境美國。<br>";
                str += "欲在美國工作者(無論有給職或無給職)，包括新聞與媒體工作者、寄宿幫傭、實習生、音樂家、以及其他特定職種，欲在美國求學(F簽證)或參加交換訪客計劃者(J簽證)<br>";
                str += "申請ESTA費用$14美金(若未含ETA行程，辦理將酌收手續費，實際費用請詢問業務人員)。旅客只需填寫中文表格(向業務人員索取)及提供護照影本。<br>";
                str += "若是由我們為您代為申請ESTA，若沒被核准，費用無法退還。<br>";
                str += "曾被拒絕ESTA授權許可，或有其他違反移民法事項者，也不得申請VMP方式入境美國。<br>";
                str += "<br>";
                str += "註1：本行程所載之護照、簽證相關規定，對象均為持中華民國護照之旅客，若貴客擁有雙重國籍或持他國護照，請先自行查明相關規定，報名時並請告知您的服務人員。<br>";
                str += "註2：內政部入出國及移民署全球資訊網 臺灣地區無戶籍國民申請在臺灣地區定居～未滿20歲設立戶籍後首次出國，應先向外交部申請含有國民身分證字號之我國護照，始可持憑出國。<br>";
                str += "美國國土安全部於2009年1月12日開始實施「旅行許可電子系統」(Electronic System for Travel Authorization, ESTA)的登錄方案。旅行許可電子系統是一個國土安全部新設的線上系統，也是入境美國免簽證專案 (Visa Waiver  Program, VWP) 裏的一項基本要件。<br>";
                str += "所有以免簽證專案赴美的會員國國民或公民必須在登機或登船前往美國72小時之前取得一份核准的ESTA<br>";
                str += "新加入免簽證專案的會員國則自加入免簽證專案當天起就適用ESTA的規定。<br>";
                str += "<br>";
                str += "持下列護照進入美國需先上網申請ESTA，目前有38個國家：若有增減免簽之國家以AIT網站公布為主。<br>";
                str += "安道爾，澳大利亞，奧地利，比利時，汶萊，捷克共和國，丹麥，愛沙尼亞，芬蘭，法國，德國，匈牙利，冰島，愛爾蘭，義大利，日本，拉脫維亞，列支敦士登，立陶宛，盧森堡，馬耳他，摩納哥，荷蘭，紐西蘭，挪威，葡萄牙，大韓民國，聖馬力諾，新加坡，斯洛伐克，斯洛維尼亞，西班牙，瑞典，瑞士，英國，希臘，台灣等37國。<br>";
                str += "二、下列旅客不適用VWP，而必須申請非移民簽證：<br>";
                str += "欲在美國工作者（無論有給職或無給職），包括新聞與媒體工作者、寄宿幫傭、實習生、音樂家、以及其他特定職種。<br>";
                str += "欲在美國求學（F簽證）或參加交換訪客計劃者（J簽證）。<br>";
                str += "前往其他國家而途中需要過境美國的空勤或航海組員(C1/D簽證) 。<br>";
                str += "欲在美國求學（F簽證）或參加交換訪客計劃者（J簽證）。<br>";
                str += "以私家飛機或私人遊艇入境美國者。<br>";
                str += "美國依據《2015年改善免簽證計畫暨防範恐怖份子旅行法案》(此法)實施新規定。<br>";
                str += "適用免簽計畫38個國家國民，★曾在2011年3月1日以後赴伊朗、伊拉克、蘇丹、敘利亞、利比亞、索馬利亞或葉門國籍或旅行，或在此七個國家停留的免簽證計畫參與會員之公民其中一國，不再有免簽資格(自2016年1月21日起其ESTA旅行許可將自動失效)，上述類別旅客仍可依照一般簽證程序在美國大使館或領事館申請赴美簽證。<br>";
                str += "<br>";
                str += "旅遊許可電子系統(ESTA)線上說明(Q&A)<br>";
                str += "★美國即日起依據《2015年改善免簽證計畫暨防範恐怖份子旅行法案》適用美國免簽計劃38個國家公民，曾在2011年3月1日以後赴伊朗、伊拉克、蘇丹、敘利亞、利比亞、索馬利亞或葉門國籍或旅行或在此七國國家停留的免簽證計畫參與會員之公民(不包括為了免簽證計畫參與會員因外交或軍事目的赴以上七國之旅行)。上述類別的旅客仍然可以依照一般簽證程序在美國大使館或領事館申請赴美簽證。<br>";
                str += "<br>";
                str += "若ESTA沒被核准，則必須透過面試方式另外辦理【美國觀光簽證】：<br>";
                str += "費用(下列費用以AIT網站公告為主)<br>";
                str += "<li>美國非移民簽證費用(依美國在台協會網站公告)<br>";
                str += "調整後的台幣費率(2012年4月13日起生效)，新台幣$4,960元(恕不退費，實際費用將因匯率調動，請同業務人員確認)<br>";
                str += "戶名:(依美國在台協會網站公告)<br>";
                str += "註：AIT費用說明內會註明當時換算成新台幣的費用。(因美金匯率而做上下調整)</li>";
                str += "<li>面談好後，護照證件由快遞公司送還，再另行自付新台幣220元快捷費用。</li>";
                str += "</ol>";
                str += "<h3>航空篇</h3>";
                str += "<ol class='trip_remind_area'>";
                str += "<li>航空公司座位安排：本行程全程使用『團體經濟艙』機票，不適用於出發前預先選位，也無法事先確認座位相關需求（如，靠窗、靠走道..等），且機上座位是航空公司依照乘客英文姓名之字母順序做統一安排，因此同行者有可能無法安排在一起，敬請參團貴賓了解。</li>";
                str += "<li>如您所參加行程需經中站轉機飛往行程目的地者，溫馨提醒：中站轉機務必配合當團領隊的引導，順利登上轉機的航段。依據IATA(國際航空運輸協會)規定，所有搭機人務必按照所開立的機票，依序搭乘使用，倘若中途有任一航段未順利搭乘，將被航空公司視同持票人放棄本次旅程，並且後續的所有機票航段（包含回程機票）將全數被作廢失效。若此，後續所有的旅程航段將必須自費重新購買新機票，費用通常在數萬元以上，敬請注意。</li>";
                str += "<li>因應航空公司機場的團體作業規定，請於出發七天前繳交護照予旅行社。</li>";
                str += "<li>北美線機票皆依所使用航空公司的團體票規定作業，凡機票一經開票則無退、換票價值(無法退機票款)，依國外旅遊定型化契約規定，如該機票款超出解約賠償之金額，亦須由旅客負擔全額機票款，特此提醒您留意。</li>";
                str += "<li>部分行程依航空公司規定，於香港機場轉機時，旅客必須於香港機場提領行李並重新辦理登機手續。因此，請旅客務必於出發前十五日提供護照影本供本公司辦理《香港電子簽證》，或是自行攜帶效期內《台胞證》，以利於香港機場轉機時提領行李並重新辦理登機手續。<br>";
                str += "<span>辦理香港電子簽證相關說明：<br>";
                str += "●	護照影本一份(護照效期需滿6個月)<br>";
                str += "●	出生地需為台灣<br>";
                str += "註：香港簽證也會有申請被拒絕的狀況，若有先例請主動告知，並請另持有效期限之台胞證。";
                str += "</span></li>";
                str += "</ol>";
                str += "<h3>住宿篇</h3>";
                str += "<ol class='trip_remind_area'>";
                str += "<li>北美團體安排之房型一般為兩雙人床房型，但若在古城區，城堡系列飯店，國家公園區域，黃刀鎮區域皆為以一大床房型為主，若需求兩床房型並無法保證能提供；北美因消防法規，大部分飯店並無法加床，請勿需求加床以及避免三人同房。<br>";
                str += "<span>備註：如旅客需求入住單人房時應繳交單人房差費用，北美需求單人房一般提供雙人大床房供單人使用。</span></li>";
                str += "<li>團體安排之房型無法指定連通房、同行親友若指定在同樓層或鄰近房間，我們將向飯店提出您的需求，但無法保證飯店一定提供，敬請見諒。</li>";
                str += "<li>飯店房型為兩人一室，若無同行者一同報名參加，本公司得協助安排與當團其它同性別團員或領隊進行分房；單人配房由本公司自行採隨機分配，恕無法指定或變更，出發後若欲補價差變更為單人住宿，需依飯店實際房況為準，非均可變更，若有造成不便之處，尚請見諒。若個人需指定單人入住，請於報名時主動告知業務人員，並按房型補足單人房價差，實際價差費用，悉以當團說明公布為準。<br>";
                str += "<span>備註：單人房差(本行程所指單人房為單人房單人床房型。)</span></li>";
                str += "<li>北美於國家公園區域、山區、古城區以及城堡系列飯店等房間格局有時大小會有些差異，一般以飯店提供的分房為主安排客人入住。</li>";
                str += "<li>若遇商展或節慶期間，飯店常會遇滿房或是房間數不夠，本公司擁有變更或調整行程之權利，將會尋找同等級替代飯店及住宿點；尤其島上之住宿酒店，因房間數有限，旺季期間，若同一家酒店房數不足，將使用兩家同等級鄰近之酒店於同一團體敬請知悉!</li>";
                str += "<li>北美部分地區(加拿大及國家公園區域)因環保及氣候特性，冬季寒冷時間較長，夏季炎熱時間較短，且夏季日夜溫差大夜晚涼爽，因此部分飯店房間僅設置冬天必備之暖氣設備，無冷氣設備，敬請諒解。</li>";
                str += "<li>北美山區小鎮、國家公園等區域因環保及常住人口稀少，大多旅館設施條件與規模無法與大城市相比，敬請見諒！</li>";
                str += "<li>若遇行程有國家公園、古城區或城堡飯店，有可能因訂房不易，所以會為了配合旅館，行程順序有可能會更改，敬請見諒！</li>";
                str += "</ol>";
                #endregion
                break;
            case "8":
                #region === 紐澳 ===
                str = "<h2>【參團須知-紐澳篇】</h2>";
                str += "<ol class='trip_remind_area'>";
                str += "	入境紐澳如持台灣護照貴賓，皆需辦理線上電子簽證，所有護照資料需與出團時所申請的資料相符，請貴賓於出發1個月前提供正確的簽證所需資料。(紐西蘭於2019年10月起強制辦理電子簽證。)<br/>";
                str += "	紐澳部分地區食材原料有限，餐食變化不大，不如台灣精緻多元。如有餐食禁忌等特殊需求，敬請貴賓於報名時向業務員提供資訊。<br/>";
                str += "	西方人對素食定義認知與我國大不同，為尊重台灣素食貴賓的飲食習慣，在避免使用蔥、蒜、辣椒、奶蛋甚至魚肉等食材的前提下，當地餐廳多以蔬果、奶蛋、起司等材料相互搭配料理。然而當地全素食品不如台灣豐富、方便取得，故餐食重複性高且變化少，敬請茹素貴賓理解。建議您可準備平常喜歡食用的素食食品（如：素食罐頭、泡麵等），以防不時之需。<br/>";
                str += "	本系列行程所預定之旅館，皆以抵達景點之距離、市區之段落地點作為考量。住宿選擇為「精緻旅館」等級，雖不比連鎖飯店規模，但均提供整潔且親切的接待服務，並結合溫馨舒適的氛圍，提供貴賓舒適的環境。各團出發日期不同，各城市若遇節慶、會展事等特殊活動造成滿房情況，將更改住宿同等級旅館或行程調整。<br/>";
                str += "	各大旅館均不提供個人衛生用品：如牙膏、牙刷、刮鬍刀及拖鞋…..等，務請自備！但提供洗髮精、肥皂。多數旅館亦提供吹風機、煮水壺。紐澳氣候較乾燥，護唇膏亦請準備，個人日常之病痛或預防用成藥亦請酌量備妥，另可準備暈車藥、腸胃藥及感冒藥等。<br/>";
                str += "	紐澳房型絕多數是兩小床雙人房，較少提供一張大床（常以兩張小床合併）。若需求一大床或是三人房之貴賓，煩請事先提出需求，需求後將依每間旅館狀況而定，不保證一定有大床。<br/>";
                str += "	單人房加價指「單人指定入住一間單人房之房價差」，並非付房價差即入住雙人房型，單人房為一人使用空間，通常較雙人房小。另飯店的團體房無法指定連通房、同行親友指定住在同樓層或鄰近房間，會向飯店提出您的需求，但無法保證飯店一定會提供，敬請見諒。<br/>";
                str += "	多數紐澳飯店房間規格不盡相同，住房分配是旅館本身在團體入住前就已經先排好房號，再發給領隊人員，倘部分標準房獲飯店善意升等，非本公司所能掌控，本公司及領隊人員皆無差別待遇，敬請貴賓理解。<br/>";
                str += "	團體安排之房型皆為禁菸房，請勿在房內抽菸；若逕行於房內抽菸，經查獲須配合各旅館規範繳交鉅額罰金，通常金額約在200-800紐幣或澳幣不等，詳細金額依照各旅館索賠為主。<br/>";
                str += "	為顧及其他貴賓之權益，本旅遊公司有權利拒絕70歲(含)以上無親友同行及特殊身體與精神狀況之旅客報名參加，敬請見諒。若自身有慢性疾病、行動不便等狀況，也請主動告知；若因未告知而衍生其他問題，敬請貴賓自行負責。<br/>";
                str += "	產品團費包含：全程機票、住宿、餐食（自理除外）、行程中安排之入內參觀景點門票以及各種機場燃油等稅金及團體旅遊履約責任保險500萬、意外傷害醫療險20萬保險。<br/>";
                str += "	產品團費不含：司機領隊導遊小費、床頭與行李搬運等禮貌性質小費、自費行程及個人支出。<br/>";
                str += "	旅行社出團之機位為團體機位，領隊僅能依照航空公司所給予的位子進行調整，並不能保證親友一定會坐在一起或皆有走道位。紐澳的國內線班機由於人力精簡所有機場從刷登機證到託運行李有些為個人自助化作業，關於操作方面有任何疑問領隊都會從旁協助。機上向空服人員要咖啡、茶、酒…，大部分是要額外付費的，敬請理解。<br/>";
                str += "	因應航空公司機場的團體作業規定，請於出發七天前繳交護照予旅行社。<br/>";
                str += "	使用外籍航空，機票皆依所使用航空公司的團體票規定作業，凡機票一經開票則無退、換票價值(無法退機票款)，依國外旅遊定型化契約規定，如該機票款超出解約賠償之金額，亦須由旅客負擔全額機票款，特此提醒您留意。<br/>";
                str += "	本行程視飯店及航空公司之確認及當地節慶保有調整之權利，若遇旺季或節慶，將會尋找同等級替代飯店及住宿點，敬請包涵，行程／航班／飯店／餐食以「行前說明會」書冊所列資料為準。<br/>";
                str += "	旅遊前或是旅途中，因天候因素、航班變更(取消)、交通工具取消或道路阻斷等不可抗力或不可歸責與旅行社之事由，導致無法依預定之旅程、交通、食宿或遊覽項目等履行時，為維護旅遊團體之安全利益，旅行社保留調整、變更行程之權利。<br/>";
                str += "	最低組團人數: 16（含）人。如低於 15（含）人，本公司保有最後出團之決定權。<br/>";
                str += "	其它未盡事項請依照「國外旅遊定型化契約書」之相關條款規定為主。<br/>";
                str += "</ol>";
                #endregion
                break;
            case "9":
                #region === 南亞 ===
                str = "<h2>【參團須知-南亞篇】</h2>";
                str += "<ol class='trip_remind_area'>";
                str += "	入境南亞地區如持台灣護照貴賓，皆需辦理簽證，所有護照資料需與出團時所申請的資料相符，請貴賓於出發1個月前提供正確的簽證所需資料。<br/>";
                str += "	南亞部分地區食材原料有限，餐食變化不大，不如台灣精緻多元。如有餐食禁忌等特殊需求，敬請貴賓於報名時向業務員提供資訊。<br/>";
                str += "	西方人對素食定義認知與我國大不同，為尊重台灣素食貴賓的飲食習慣，在避免使用蔥、蒜、辣椒、奶蛋甚至魚肉等食材的前提下，當地餐廳多以蔬果、奶蛋、起司、辛香料等材料相互搭配料理。然而當地全素食品不如台灣豐富、方便取得，故餐食重複性高且變化少，敬請茹素貴賓理解。建議您可準備平常喜歡食用的素食食品（如：素食罐頭、泡麵等），以防不時之需。<br/>";
                str += "	本系列行程所預定之旅館，皆以抵達景點之距離、市區之段落地點作為考量。住宿選擇為「精緻旅館」等級，雖不比連鎖飯店規模，但均提供整潔且親切的接待服務，並結合溫馨舒適的氛圍，提供貴賓舒適的環境。各團出發日期不同，各城市若遇節慶、會展事等特殊活動造成滿房情況，將更改住宿同等級旅館或行程調整。<br/>";
                str += "	各大旅館均不提供個人衛生用品：如牙膏、牙刷、刮鬍刀及拖鞋…..等，務請自備！但提供洗髮精、肥皂。多數旅館亦提供吹風機、煮水壺。紐澳氣候較乾燥，護唇膏亦請準備，個人日常之病痛或預防用成藥亦請酌量備妥，另可準備暈車藥、腸胃藥及感冒藥等。<br/>";
                str += "	南亞房型絕多數是兩小床雙人房，較少提供一張大床（常以兩張小床合併）。若需求一大床或是三人房之貴賓，煩請事先提出需求，需求後將依每間旅館狀況而定，不保證一定有大床。<br/>";
                str += "	單人房加價指「單人指定入住一間單人房之房價差」，並非付房價差即入住雙人房型，單人房為一人使用空間，通常較雙人房小。另飯店的團體房無法指定連通房、同行親友指定住在同樓層或鄰近房間，會向飯店提出您的需求，但無法保證飯店一定會提供，敬請見諒。<br/>";
                str += "	部分南亞飯店房間規格不盡相同(尤其是特色皇宮飯店)，住房分配是旅館本身在團體入住前就已經先排好房號，再發給領隊人員，倘部分標準房獲飯店善意升等，非本公司所能掌控，本公司及領隊人員皆無差別待遇，敬請貴賓理解。<br/>";
                str += "	團體安排之房型皆為禁菸房，請勿在房內抽菸；若逕行於房內抽菸，經查獲須配合各旅館規範繳交鉅額罰金，通常金額約在100-500美金不等，詳細金額依照各旅館索賠為主。<br/>";
                str += "	為顧及其他貴賓之權益，本旅遊公司有權利拒絕70歲(含)以上無親友同行及特殊身體與精神狀況之旅客報名參加，敬請見諒。若自身有慢性疾病、行動不便等狀況，也請主動告知；若因未告知而衍生其他問題，敬請貴賓自行負責。<br/>";
                str += "	產品團費包含：全程機票、住宿、餐食（自理除外）、行程中安排之入內參觀景點門票以及各種機場燃油等稅金及團體旅遊履約責任保險500萬、意外傷害醫療險20萬保險。<br/>";
                str += "	產品團費不含：司機領隊導遊小費、床頭與行李搬運等禮貌性質小費、自費行程及個人支出。<br/>";
                str += "	在南亞地區適時給予領隊、導遊及服務人員些許小費，是對他們服務態度的肯定，是一種國際禮儀，也是一種實質性鼓勵與讚許；請各位貴賓對表現良好的領隊、導遊及服務人員給予小費，讓他們良好的服務態度受到肯定及鼓勵。<br/>";
                str += "    ＊ 進出飯店之行李小費：以件數為單位，1件美金1元為原則。<br/>";
                str += "    ＊ 房間清潔小費：以房間為單位，1間房間美金1元為原則。<br/>";
                str += "    ＊ 坐吉普車小費：每人美金１元。<br/>";
                str += "    ＊ 導遊、司機、領隊小費：綜合三項的小費，一天以美金10元為原則。<br/>";
                str += "    ＊ 其他服務小費，因地區及服務性質不同，可事先徵詢導遊或領隊之意見，再決定付小費之多寡。<br/>";
                str += "	旅行社出團之機位為團體機位，領隊僅能依照航空公司所給予的位子進行調整，並不能保證親友一定會坐在一起或皆有走道位。紐澳的國內線班機由於人力精簡所有機場從刷登機證到託運行李有些為個人自助化作業，關於操作方面有任何疑問領隊都會從旁協助。機上向空服人員要咖啡、茶、酒…，大部分是要額外付費的，敬請理解。<br/>";
                str += "	高港高接駁：純為服務性質，本公司會盡最大努力協助訂位，但不保證一定有機位。<br/>";
                str += "	因應航空公司機場的團體作業規定，請於出發七天前繳交護照予旅行社。<br/>";
                str += "	如使用外籍航空，機票皆依所使用航空公司的團體票規定作業，凡機票一經開票則無退、換票價值(無法退機票款)，依國外旅遊定型化契約規定，如該機票款超出解約賠償之金額，亦須由旅客負擔全額機票款，特此提醒您留意。<br/>";
                str += "	部分行程依航空公司規定，於香港機場轉機時，旅客必須於香港機場提領行李並重新辦理登機手續。因此，請旅客務必於出發前十五日提供護照影本供本公司辦理《香港電子簽證》，或是自行攜帶效期內《台胞證》，以利於香港機場轉機時提領行李並重新辦理登機手續。<br/>";
                str += "    辦理香港電子簽證相關說明：<br/>";
                str += "    ● 護照影本一份(護照效期需滿6個月)<br/>";
                str += "    ● 出生地需為台灣<br/>";
                str += "    註：香港簽證也會有申請被拒絕的狀況，若有先例請主動告知，並請另持有效期限之台胞證。<br/>";
                str += "	本行程視飯店及航空公司之確認及當地節慶保有調整之權利，若遇旺季或節慶，將會尋找同等級替代飯店及住宿點，敬請包涵，行程／航班／飯店／餐食以「行前說明會」書冊所列資料為準。<br/>";
                str += "	旅遊前或是旅途中，因天候因素、航班變更(取消)、交通工具取消或道路阻斷等不可抗力或不可歸責與旅行社之事由，導致無法依預定之旅程、交通、食宿或遊覽項目等履行時，為維護旅遊團體之安全利益，旅行社保留調整、變更行程之權利。<br/>";
                str += "	最低組團人數: 16（含）人。如低於 15（含）人，本公司保有最後出團之決定權。<br/>";
                str += "	其它未盡事項請依照「國外旅遊定型化契約書」之相關條款規定為主。<br/>";
                str += "</ol>";
                #endregion
                break;
            case "37":
                #region === 大陸 ===
                str = "<h2>簽證說明</h2>";
                str += "<ol class='trip_remind_area'>";
                str += "A.申辦台胞證者:<br/>";
                str += "● 護照正本(有效期需半年以上)<br/>";
                str += "● 護照影本一份<br/>";
                str += "● 身份證正反影印本一份(未滿14歲的小孩請提供三個月內全戶戶籍騰本正本一份，戶籍謄本記事欄不可空白)<br/>";
                str += "● 兩吋白底彩色照片一張(6個月內的近期照，人像自頭頂至下顎之長度介於3.2至3.6公分，與護照規格相同。最好不可與護照及身份證相同，敬請注意！) <br/>";
                str += "● 未滿16歲申請者，需附監護人身分證影本，並備註關係(另請提供三個月內全戶戶籍謄本正本一份，戶籍謄本記事欄不可空白)<br/>";
                str += "● 辦件費用NT1,700元<br/>";
                str += "● 工作天約8天<br/>";
                str += "【落地單次新證】(2015/07/01公佈之相關規定)<br/>";
                str += "請注意，並非所有大陸機場都有開放落地簽證服務。請務必提早與客服人員確認以利作業。<br/>";
                str += "● 需攜帶近期二吋白底彩色脫帽照片二張。<br/>";
                str += "● 落地單次新證費用：人民幣50元。<br/>";
                str += "● 開放申請時間：以中國大陸各機場的開放申請時間為主。<br/>";
                str += "● 兒童沒有中華民國身分證者，需備齊護照及戶籍謄本正本或可以證明與父母關係相關文件。<br/>";
                str += "● 需攜帶中華民國國民身分證。<br/>";
                str += "● 此規定若有任何變更，請以該國簽證部門公告為主。<br/>";
                str += "本行程所載之護照、簽證相關規定，對象均為持中華民國護照之旅客，若貴客擁有雙重國籍或持他國護照，請先自行查明相關規定，報名時並請告知您的服務人員。<br/>";
                str += "相關規定請參考網站：<a style='background:top; color:blue; width:auto;' href='https://www.immigration.gov.tw/welcome.htm'>內政部移民署全球資訊網</a><br/>";
                str += "</ol>";

                str += "<h2>行程規定</h2>";
                str += "<ol class='trip_remind_area'>";
                str += "<li>本行程設定為團體旅遊行程，故為顧及旅客於出遊期間之人身安全及相關問題，於旅遊行程期間，恕無法接受脫隊之要求；若因此而無法滿足您的旅遊需求，建議您另行選購團體自由行或航空公司套裝自由行，不便之處，尚祈鑒諒。</li>";
                str += "<li>本行程最低出團人數為16人以上(含)；最多為40人以下(含)，台灣地區將派遣合格領隊隨行服務。</li>";
                str += "<li>貼心提醒：我們為維護旅遊品質及貴賓們的權益，在不變更行程內容之前提下，將依飯店具體確認回覆的結果，再綜合當地實際交通等情況，為貴賓們斟酌調整並妥善安排旅遊行程、飯店入住之先後順序或旅遊路線，請以說明會或最後確認的行程說明資料為準。</li>";
                str += "<li>中國地區飯店規則說明：<br/>";
                str += "　　1.中國地區飯店評等以【星級】為準則，最高為五星、最低為一星。<br/>";
                str += "　　2.近年來中國地區逐步發展迅速，飯店如雨後春筍中增加，故會有一種【準星級】名詞出現，針對此現象提出說明如下：<br/>";
                str += "　　A、新酒店成立需經試營業一年後才能進行星級評等，但各酒店在建立時會依各星級之規格建造設定，故會有準五星、準四星…等等名詞產生。<br/>";
                str += "　　B、有些酒店未參與星級評等，但酒店在建立時會依各星級之規格建造設定，故會有準五星、準四星…等等名詞產生。";
                str += "</li>";
                str += "<li>若有特殊餐食者，最少請於出發前二天（不含假日）告知承辨人員，為您處理。</li>";
                str += "<li>本行程酒店住宿皆為2人1室，大陸地區有部份四、五星級酒店房間內無法採用加床方式住宿，若有貴賓指定要用加床方式住宿，遇到上述情況尚請諒解。</li>";
                str += "<li>上述行程及餐食將視情況而前後有所變動，但行程景點絕不減少，敬請諒察。</li>";
                str += "<li>貴重物品以及個人重要物品請自行保管並隨身攜帶，請勿放置行李箱內、飯店房間內或著車上。</li>";
                str += "<li>本優惠行程不適用當地台商或大陸人士。</li>";
                str += "</ol>";

                str += "<h2>小費說明</h2>";
                str += "<ol class='trip_remind_area'>";
                str += "<li>小費：每人每日新台幣300元小費給予領隊，(西藏及稻城亞丁每人每日新台幣400元小費給予領隊)由領隊統籌付給當地的導遊及司機。</li>";
                str += "<li>房間小費以及行李小費敬請自付，行李小費以一件行李計算，提送一次建議人民幣5元/件，入住及離開酒店共2次提送，建議人民幣10元/件。</li>";
                str += "<li>國際電話、飯店洗衣、房間內冰箱的食物，各服務人員的小費敬請自理。</li>";
                str += "</ol>";

                str += "<h2>旅遊錦囊</h2>";
                str += "<ol class='trip_remind_area'>";
                str += "<li>電話：<br/>";
                str += "人在台灣，打電話到中國大陸：台灣國際冠碼＋大陸國碼＋當地區域號碼＋電話號碼<br/>";
                str += "002 ＋ 86 ＋ 當地區域號碼＋電話號碼<br/>";
                str += "例：從台灣打電話到上海為002 ＋ 86 ＋ 21 ＋ 上海電話號碼<br/>";
                str += "人在中國大陸，打電話回台北：大陸國際冠碼＋台灣國碼＋台北區域號碼＋台北家中電話<br/>";
                str += "00＋ 886 ＋ 2 ＋台北家中電話<br/>";
                str += "例：00 ＋ 886 ＋ 2 ＋ 25221771<br/>";
                str += "人在中國大陸，打電話回台灣的行動電話：大陸國際冠碼＋台灣國碼＋行動電話0以後的號碼</li>";
                str += "<li>電壓：<br/>";
                str += "》中國大陸電壓為220伏特，插座為2孔圓形、2孔扁型，（建議可自帶轉換插頭）。<br/>";
                str += "》在很多中、高檔酒店洗手間裝有供刮鬍刀、吹風機用的110伏特的電源插座。</li>";
                str += "<li>時差：<br/>";
                str += "》中國大陸跟台灣無時差，格林威治標準時間+8小時。<br/>";
                str += "》唯有新疆維吾爾族自治區作息時間晚中原時間2小時(因時區的關係)，但全中國統一為一個中原標準時間。</li>";
                str += "<li>氣溫：<br/>";
                str += "中國氣候具有大陸性季風型氣候和氣候複雜多樣兩大特徵。冬季盛行偏北風，夏季盛行偏南風，四季分明，雨熱同季。每年9月到次年4月間，乾寒的冬季風從西伯利亞和蒙古高原吹來，由北向南勢力逐漸減弱，形成寒冷乾燥、南北溫差很大的狀況。夏季風影響時間較短，每年的4至9月，暖濕氣流從海洋上吹來，形成普遍高溫多雨、南北溫差很小的狀況。<br/>";
                str += "參考網站：<a style='background:top; color:blue; width:auto;' href='https://www.cwb.gov.tw/V8/C/index.html'>中央氣象局</a>、<a style='background:top; color:blue; width:auto;' href='https://tw.news.yahoo.com/weather/'>雅虎氣象網站</a></li>";
                str += "<li>貨幣匯率：<br/>";
                str += "》人民幣：紙幣分別為100元、50元、10元、5元、2元、1元。硬幣分別為1、2、5角，分的面額有1、2、5分。<br/>";
                str += "》外匯券現已不使用。<br/>";
                str += "》人民幣元的縮寫符號是RMB￥。<br/>";
                str += "》大約美金US1：人民幣6.8(2017年5月)。參考網站：<a style='background:top; color:blue; width:auto;' href='http://www.boc.cn/index.html'>中國銀行</a></li>";
                str += "<li>飲水衛生：<br/>";
                str += "》在中國自來水是不可以直接飲用的，瓶裝礦泉水隨處可以買到。<br/>";
                str += "》在大中旅遊城市的街頭或遊覽場所，一般均設有免費以及收費洗手間(每次收2角、3角)不等。機場、大型購物商場的洗手間，均為免費。<br/>";
                str += "》本行程所設定的餐廳皆為中國政府相關單位所審查合格的餐廳，用餐環境、軟硬體設備以及待客服務方面已逐漸步入國際水準。</li>";
                str += "<li>語言宗教：<br/>";
                str += "》中國是多民族、多語言、多文種的國家，有56個民族，80種以上語言，約30種文字。<br/>";
                str += "》官方語言普通話，在臺灣稱為國語，在新加坡、馬來西亞稱為華語，大陸稱為普通話。<br/>";
                str += "》中國是有多宗教的國家，主要有佛教、道教、伊斯蘭教、天主教、基督教、藏傳佛教等。</li>";
                str += "<li>風俗民情：<br/>";
                str += "自古以來中國就是一個統一的多民族國家。新中國成立後，通過識別並經中央政府確認的民族共有56個。由於漢族以外的55個民族相對漢族人口較少，習慣上被稱為少數民族，中國大陸各個省或是地區都有些不同的風俗民情，可參考以下網站了解更多有趣的民間風俗。<br/>";
                str += "參考網站：<a style='background:top; color:blue; width:auto;' href='http://www.chinesefolklore.com/'>中國民俗網</a>、<a style='background:top; color:blue; width:auto;' href='http://www.cnta.com/'>中國旅遊網</a></li>";
                str += "</ol>";

                str += "<h2>安全守則</h2>";
                str += "<ol class='trip_remind_area'>";
                str += "為了您在本次旅遊途中本身的安全，請您務必遵守下列事項，這是我們應盡告知的責任，也是保障您的權益。<br/>";
                str += "【出境及搭機前】<br/>";
                str += "<li>請於班機起飛前二小時抵達機場，以免擁擠及延遲辦理登機手續。</li>";
                str += "<li>領隊將於機場團體櫃台前接待團員，辦理登機手續及行李托運之後將護照發還給團員。</li>";
                str += "<li>團員領取護照後請一一檢查護照套內之各種簽證是否齊全，若有不齊請通知領隊。</li>";
                str += "<li>依各航空公司規定行李託運公斤數如下(超過規定重量需自付超重費)：<br/>";
                str += "*長榮(立榮)航班、中華(華信)航空、國泰(港龍)航空、山東航空，託運行李以每人一件(30公斤)為限。<br/>";
                str += "*東方(上海)航空、南方航空、四川航空、海南航空、中國國際航空，託運行李以每人一件(23公斤)為限，行李長寬高總和不得超過158公分。<br/>";
                str += "*廈門航空，託運行李以每人二件(合計共23公斤)為限，單件行李長寬高總和不得超過158公分。</li>";
                str += "<li>每人攜帶外幣現金以總值美金一萬元為限，旅行支票不在此限。</li>";
                str += "<li>進入海關後，如購買免稅物品，請把握時間按登機證上說明前往登機門登機。</li>";
                str += "【注意事項及說明】<br/>";
                str += "<li>大陸地區氣溫較台灣低，可斟酌加帶禦寒衣物，冬季請帶保暖外套，尤其山區晚間較冷，如可能請攜帶雨具，避免風寒。提醒您:因中國大陸航空安全法所規範，暖暖包可攜帶入境，但不可攜帶出境。請旅客酌量攜帶，避免損失!</li>";
                str += "<li>大陸地區旅遊在食的方面，早餐都在住宿酒店內使用(除香港、廣州、深圳外)，其他午、晚餐均安排在各地餐館使用中式餐食。</li>";
                str += "<li>各旅遊地區遊客眾多，請特別小心隨身攜帶之物品，以免被竊。</li>";
                str += "<li>貴重物品以及個人重要物品請自行保管並隨身攜帶，請勿放置行李箱內、飯店房間內或著車上。</li>";
                str += "<li>大陸地區均使用人民弊，請在銀行兌換，切勿在外與不明人士兌換。</li>";
                str += "<li>行李：原則上一大一小，小的使用肩帶式(名牌、標式)，大的堅固加鎖鑰。</li>";
                str += "<li>部分飯店房內有提供電熱水壺供入住旅客煮水用，此熱水壺僅供煮水使用，不可放入任何物品(EX：咖啡粉、茶葉、火鍋料、泡麵不等)，並請注意切勿將熱水壺置於電磁爐上加熱，如有使用方式上之問題請務必詢問帶團領隊或飯店人員，否則造成危安問題或毀損飯店設備時，可能須負相關責任。</li>";
                str += "<li>衣服(A)冬季：帽子、圍巾、毛套、毛襪、衛生內衣褲、套頭毛衣、禦寒大衣、一件口罩。<br/>";
                str += "　　(B)夏季：與台灣夏天天氣一樣。</li>";
                str += "<li>鞋子：女士盡量不要穿高跟鞋，最好穿平底休閒鞋，儘可能不要穿新鞋。</li>";
                str += "<li>藥物：胃腸藥、感冒藥、暈車藥、私人習慣性藥物。</li>";
                str += "<li>自備物品：牙膏、牙刷、洗髮精、洗衣粉、面霜、眼鏡、陽傘、水壺、水果刀、刮鬍刀、計算機、照相機、底片、生理用品。</li>";
                str += "<li>大陸部分地區機場、車站設備較為落後，造成經常性誤點或班機取消，航空公司不負責賠償責任，若導致行程延誤，短缺概不得退費，但團體會在條件允許下，儘量安排走完行程，若有不便敬請見諒。</li>";
                str += "<li>禁止事項：不得嫖妓，不得攜帶水果、古董及報紙入出境，儘量不要談論政治及相關話題避免無謂之麻煩。</li>";
                str += "<li>購物時請務必注意幣值兌換。</li>";
                str += "<li>大陸餐廳、廁所休息站，皆賣絲綢字畫、雕刻品茶壺、茶葉…等等藝術品或當地土特產，並非特定購物點(SHOPPING站)，這種情形為全大陸性。</li>";
                str += "<li>大陸遊覽車狀況絕不能與歐美、紐澳及東南亞線相提並論，因大陸經濟特殊且文化背景較一般不同，對觀光事業的認知及重視有限。</li>";
                str += "<li>請旅客前往大陸旅遊時，請先調適自己，並對該地區環境先作認識，這樣一來相信您會有美好的旅程，並記得要入境隨俗哦。</li>";
                str += "<li>政府規定自87.10.01起，不得自海外攜帶新鮮水果入境，若違反規定除水果被沒收外，將處3萬至5萬元罰款。</li>";
                str += "<li>依「大陸海關總署公告之2005年第23號公文」中指出，自2005年7月1日起，凡進出大陸地區之旅客皆需於入出境時填寫海關申報單。航空公司將於澳門或香港前往大陸各航點之班機上進行申報單之發送及機上廣播之宣導，請旅客特別注意。旅客通關時請確實申報所攜帶之行李及物品，以免遭受當地海關予以徵稅。</li>";
                str += "【國人旅遊購物免稅額規定】<br/>";
                str += "<li>每人可攜回新台幣2萬元免稅額購物商品。海關查出逾額將處分沒收或罰鍰。<br/>";
                str += "	o	每人可再攜回五部3C產品為上限，海關查出未申報超出量將處分沒收或罰鍰。</li>";
                str += "【旅客攜帶動植物及其產品入境檢疫須知】<br/>";
                str += "哪些東西不能帶？為了您通關順利，請勿攜帶動植物及其產品禁止旅客攜帶的動物及其產品：<br/>";
                str += "》活動物：犬、貓、兔、禽鳥、鼠等。<br/>";
                str += "》動物產品：生鮮、冷凍、冷藏肉類及其製品（如香腸、肉乾、貢丸、餛飩、烤鴨等）、含肉加工品（速食麵、雞湯、含肉晶粉等）、蛋品、鹿茸、血清等生物樣材等，包含已煮熟、乾燥、加工、真空包裝處理之產品。<br/>";
                str += "》新鮮水果：附著土壤或有害活生物之植物。活昆蟲或有害生物。自疫區轉運之植物及植物產品。<br/>";
                str += "自97年10月1日起，入境旅客攜帶動植物或其產品，如未主動向關稅局申報或未向本局申請檢疫而被查獲者，除處新臺幣3,000元以上罰鍰外，如有違規情節重大者並將移送法辦。<br/>";
                str += "下機至出關途中請旅客主動將動植物產品丟入農畜產品棄置箱，配合檢疫偵測犬隊執行行李檢查。<br/>";
                str += "旅客檢疫相關規定，可參考網頁【行政院農業委員會動植物防疫檢疫局】之「出入境旅客檢疫注意事項」專區。<br/>";
                str += "為維護飛航安全，自2007年3月1日起，凡我國搭乘國際線班機(含國際包機)之出境、轉機及過境旅客所攜帶之液體、膠狀及噴霧類物品實施管制。<br/>";
                str += "A.所有旅客隨身攜帶之液體膠狀及噴霧類物品其體積不得超過100毫升，並要放入不超過1公升且可重覆密封之透明塑膠袋內。袋子需能完全密封。<br/>";
                str += "B.旅客攜帶旅行中所必要但未符合前述限量規定之嬰兒奶粉(牛奶)、嬰兒食品、藥品、糖尿病或其他醫療所需之液體膠狀及噴霧類物品，經向安全檢查人員申報並獲得同意後，可不受前項規定的限制。<br/>";
                str += "C.出境或過境(轉機)旅客在機場管制區或前段航程於機艙內購買或取得前述物品可隨身上機，但需包裝於經籤封防止調包及顯示有效購買證明之塑膠袋內。<br/>";
                str += "D.為使安檢線之X光檢查儀有效，前述之塑膠袋應與其他手提行李、外套或手提電腦分開通過X光檢查。<br/>";
                str += "出國旅遊防疫安全<br/>";
                str += "<li>出國前，至疾病管制署全球資訊網查詢國際疫情資訊及防疫建議，或於出國前4至6週前往「旅遊醫 學門診」接受評估。</li>";
                str += "<li>旅途中或返國時，曾有發燒、腹瀉、出疹或呼吸道不適等疑似傳染病症狀，請於入境時主動告知機場檢疫人員；返國後21天內，若有身體不適，請盡速就醫，並告知醫師旅遊史及接觸史。</li>";
                str += "<li>傳染病預防措施：</li>";
                str += "（1） 用肥皂勤洗手、吃熟食、喝瓶裝水。<br/>";
                str += "（2） 有呼吸道症狀應配戴口罩。<br/>";
                str += "（3） 噴防蚊液，避免蚊蟲叮咬。<br/>";
                str += "（4） 不接觸禽鳥、犬貓及野生動物。<br/>";
                str += "※更多旅遊醫學相關資訊請查詢疾病管制署全球資訊網https://www.cdc.gov.tw「國際旅遊與健康」專區，或撥打防疫專線1922（國外可撥+886-800-001922）。<br/>";
                str += "</ol>";
                #endregion
                break;
            default:
                #region
                str = "<h3>旅客篇</h3>";
                str += "<ol class='trip_remind_area'>";
                str += "<li>未滿20歲之未成年人，未與法定代理人一同報名參加旅遊行程時，須得法定代理人之同意，報名始為有效！為確認您的報名有徵得法定代理人之同意，請您記得將旅行社所給旅遊定型化契約書或同意書，提供給您的法定代理人簽名後並繳回，報名手續始有效完成！</li>";
                str += "<li>團體旅遊時間安排需團員相互配合，為顧及全體旅客之權益，若有嬰幼兒同行時，可能無法妥適兼顧，所以請貴賓於報名時，需多方考量帶嬰幼兒同行可能產生的不便，以避免造成您和其他團員的不悅與困擾。</li>";
                str += "<li>為考量旅客自身之旅遊安全，並顧及同團其他旅客之旅遊權益，凡年滿70歲以上或行動不便之貴賓，建議須有家人或友人同行，若無陪同者，請在預訂前如實告知敝公司，讓我們可以為您提供您最適合的建議，並做出相應安排(以安全為優先)。</li>";
                str += "<li>飯店房型為兩人一室，若無同行者一同報名參加，本公司得協助安排與當團其它同性別團員或領隊進行分房（但無法保證）。若個人需指定單人入住，請於報名時主動告知業務人員，並按房型補足單人房價差，實際價差費用，悉以當團說明公布為準。</li>";
                str += "<li>團體安排之房型皆為禁菸房，請勿在房內抽菸；若逕行於房內抽菸，經查獲須配合各旅館規範繳交鉅額罰金，通常金額約在100-500歐元不等，詳細金額依照各旅館索賠為主。</li>";
                str += "<li>如於行程中有特殊之需求，例如兒童餐、忌葷或一張大床等，請於出發前15日提出以利作業。</li>";
                str += "<li>行程無法延長住宿天數，更改日期及航班，旅客若中途脫隊，視同自願放棄，恕不另外退費。</li>";
                str += "<li>行程於國外如遇塞車時，請貴賓們稍加耐心等候。如塞車情形嚴重，而會影響到行程或餐食的安排時，為維護旅遊品質及貴賓們的權益，我們將為您斟酌調整並妥善安排旅遊行程，敬請貴賓們諒解。</li>";
                str += "<li>本行程所載之護照、簽證相關規定，對象均為持中華民國護照之旅客，內載有國民身分證統一編號的有效中華民國護照(護照需有半年以上效期)，若貴客擁有雙重國籍或持他國護照，報名時並請告知您的服務人員，則須再查明相關規定。</li>";
                str += "<li>重申歐盟加強查緝仿冒品，旅客出入境攜帶或穿著或購買仿冒商品，可處四年有期徒刑及37萬3千美金罰鍰。</li>";
                str += "<li>旅遊前或是旅途中，因天候因素、航班變更(取消)、交通工具取消或道路阻斷等不可抗力或不可歸責與旅行社之事由，導致無法依預定之旅程、交通、食宿或遊覽項目等履行時，為維護旅遊團體之安全利益，旅行社保留調整、變更行程之權利。</li>";
                str += "</ol>";
                str += "<h3>航空篇</h3>";
                str += "<ol class='trip_remind_area'>";
                str += "<li>航空公司座位安排：本行程全程使用『團體經濟艙』機票，不適用於出發前預先選位，也無法事先確認座位相關需求（如，靠窗、靠走道..等），且機上座位是航空公司依照乘客英文姓名之字母順序做統一安排，因此同行者有可能無法安排在一起，敬請參團貴賓了解。</li>";
                str += "<li>如您所參加行程需經中站轉機飛往行程目的地者，溫馨提醒：中站轉機務必配合當團領隊的引導，順利登上轉機的航段。依據IATA(國際航空運輸協會)規定，所有搭機人務必按照所開立的機票，依序搭乘使用，倘若中途有任一航段未順利搭乘，將被航空公司視同持票人放棄本次旅程，並且後續的所有機票航段（包含回程機票）將全數被作廢失效。若此，後續所有的旅程航段將必須自費重新購買新機票，費用通常在數萬元以上，敬請注意。</li>";
                str += "<li>因應航空公司機場的團體作業規定，請於出發七天前繳交護照予旅行社。</li>";
                str += "<li>使用歐籍<和國泰>航空，機票皆依所使用航空公司的團體票規定作業，凡機票一經開票則無退、換票價值(無法退機票款)，依國外旅遊定型化契約規定，如該機票款超出解約賠償之金額，亦須由旅客負擔全額機票款，特此提醒您留意。</li>";
                str += "<li>部分行程依航空公司規定，於香港機場轉機時，旅客必須於香港機場提領行李並重新辦理登機手續。因此，請旅客務必於出發前十五日提供護照影本供本公司辦理《香港電子簽證》，或是自行攜帶效期內《台胞證》，以利於香港機場轉機時提領行李並重新辦理登機手續。<br>";
                str += "<span>辦理香港電子簽證相關說明：<br>";
                str += "●	護照影本一份(護照效期需滿6個月)<br>";
                str += "●	出生地需為台灣<br>";
                str += "註：香港簽證也會有申請被拒絕的狀況，若有先例請主動告知，並請另持有效期限之台胞證。";
                str += "</span></li>";
                str += "</ol>";
                str += "<h3>住宿篇</h3>";
                str += "<ol class='trip_remind_area'>";
                str += "<li>團體安排之房型均為兩小床房型，若您指定大床房型，需視飯店提供為主，無法保證一定有大床房型；如需求三人同房，大部份飯店無三張正常床型之三人房，通常為雙人房加一床，許多旅館只接受小孩(12歲以下)才能加床，一大二小或二大一小合住，加床大多為摺疊床、沙發床或行軍彈簧床，加上飯店房間空間有限，勢必影響住宿品質，故建議避免需求三人同房。<br>";
                str += "<span>備註：如遇飯店未有加床服務內容時，且旅客需入住單人房時應繳交單人房差費用。</span></li>";
                str += "<li>團體安排之房型無法指定連通房、同行親友若指定在同樓層或鄰近房間，我們將向飯店提出您的需求，但無法保證飯店一定提供，敬請見諒。</li>";
                str += "<li>三、	飯店房型為兩人一室，若無同行者一同報名參加，本公司得協助安排與當團其它同性別團員或領隊進行分房；單人配房由本公司自行採隨機分配，恕無法指定或變更，出發後若欲補價差變更為單人住宿，需依飯店實際房況為準，非均可變更，若有造成不便之處，尚請見諒。若個人需指定單人入住，請於報名時主動告知業務人員，並按房型補足單人房價差，實際價差費用，悉以當團說明公布為準。<br>";
                str += "<span>備註：單人房差(本行程所指單人房為單人房單人床房型。)</span></li>";
                str += "<li>部分特色飯店如番紅花城民宿、洞穴飯店等房間格局大小不一，隨團領隊會使用抽籤方式安排入住。</li>";
                str += "<li>若遇商展或節慶期間，飯店常會遇滿房或是房間數不夠，本公司擁有變更或調整行程之權利，將會尋找同等級替代飯店及住宿點；尤其島上之住宿酒店，因房間數有限，旺季期間，若同一家酒店房數不足，將使用兩家同等級鄰近之酒店於同一團體敬請知悉!</li>";
                str += "<li>歐洲因環保及氣候特性，冬季寒冷時間較長，夏季炎熱時間較短，且夏季日夜溫差大夜晚涼爽，因此部分飯店房間僅設置冬天必備之暖氣設備，無冷氣設備，敬請諒解。</li>";
                str += "</ol>";
                #endregion
                break;
        }

        return str;
    }

    private string GetAgentPrice(string tripNo, string grop_date)
    {
        string strSql = "";
        string res = "";
        //strSql = "  SELECT Trip.dbo.Grop.Agent_Tour";
        //strSql += " FROM Trip.dbo.Trip";
        //strSql += " LEFT JOIN Trip.dbo.Grop ON Trip.dbo.Trip.Trip_No = Trip.dbo.Grop.Trip_No AND Trip.dbo.Grop.Grop_Depa = @Grop_Depa";
        //strSql += " WHERE Trip.dbo.Trip.Trip_No = @Trip_No";
        strSql = "  SELECT Grop.Agent_Tour";
        strSql += " FROM Trip";
        strSql += " LEFT JOIN Grop ON Trip.Trip_No = Grop.Trip_No AND Grop.Grop_Depa = @Grop_Depa";
        strSql += " WHERE Trip.Trip_No = @Trip_No";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        try
        {
            using (SqlConnection connect = new SqlConnection(strConnString))
            {
                connect.Open();
                using (SqlCommand comm = new SqlCommand(strSql, connect))
                {
                    comm.Parameters.Add(new SqlParameter("@Trip_No", tripNo));
                    comm.Parameters.Add(new SqlParameter("@Grop_Depa", grop_date));
                    using (SqlDataReader reader = comm.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            res = reader["Agent_Tour"].ToString();
                        }

                    }
                }
            }
        }
        catch (Exception) { }
        return res;
    }
    #endregion

    #region === 團費詳細資訊 ===
    private void Get_TourPrice(string strNumber)
    {
        string strSql = "";
        strSql = " SELECT [Number],[Sequ_No],[Tick_Type],[Tour_Type],[Bed_Type]";
        strSql += " ,[Cruises_Type],IsNull([SalePrice],0) AS SalePrice,IsNull([AgentPrice],0) AS AgentPrice";
        strSql += " ,Grop.Group_Category_No,Area.Area_No";
        strSql += " FROM [Tour_Price]";
        strSql += " LEFT JOIN Grop ON Tour_Price.Number = Grop.Van_Number";
        strSql += " LEFT JOIN Area ON Area.Area_ID = GROP.Area_Code";
        strSql += " WHERE Number = @Number";
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
                int intSalePrice = Convert.ToInt32(reader["SalePrice"].ToString());

                switch (reader["Tick_Type"].ToString().ToUpper())
                {
                    case "A":
                    //    if (intSalePrice == 0) litPay1.Text = "<div class=\"TP_left_money_none\">不提供</div>";
                    //    if (intSalePrice <= 5000 && intSalePrice > 0) litPay1.Text = "<div class=\"TP_left_money_none\">來電洽詢</div>";
                    //    litPay1.Text = "<div class=\"TP_left_money_all\">$" + intSalePrice.ToString("N0") + "</div>";
                        break;
                    case "C":
                        switch (reader["Bed_Type"].ToString().ToUpper())
                        {
                            case "1": // 小孩 佔床

                                if (intSalePrice > 5000)
                                {
                                    litPay2.Text = "<div class=\"TP_left_money_all\">$" + intSalePrice.ToString("N0") + "</div>";
                                }
                                else if (reader["Group_Category_No"].ToString() == "86" && intSalePrice > 3000) // 基隆起航，大於3000元就顯示
                                {
                                    litPay2.Text = "<div class=\"TP_left_money_all\">$" + intSalePrice.ToString("N0") + "</div>";
                                }
                                else if (reader["Area_No"].ToString() == "31" && intSalePrice > 300) // 台灣館，大於300元就顯示
                                {
                                    litPay2.Text = "<div class=\"TP_left_money_all\">$" + intSalePrice.ToString("N0") + "</div>";
                                }
                                else if (intSalePrice <= 5000 && intSalePrice > 0)
                                {
                                    litPay2.Text = "<div class=\"TP_left_money_none\">來電洽詢</div>";
                                }
                                else
                                {
                                    litPay2.Text = "<div class=\"TP_left_money_none\">不提供</div>";
                                }

                                break;
                            case "2": // 小孩 不佔床
                                //if (intSalePrice == 0) { litPay3.Text = "<div class=\"TP_left_money_none\">不提供</div>"; break; }
                                //if (intSalePrice <= 5000 && intSalePrice > 0) { litPay3.Text = "<div class=\"TP_left_money_none\">來電洽詢</div>"; break; }
                                if (intSalePrice > 5000)
                                {
                                    litPay3.Text = "<div class=\"TP_left_money_all\">$" + intSalePrice.ToString("N0") + "</div>";
                                }
                                else if (reader["Group_Category_No"].ToString() == "86" && intSalePrice > 3000) // 基隆起航，大於3000元就顯示
                                {
                                    litPay3.Text = "<div class=\"TP_left_money_all\">$" + intSalePrice.ToString("N0") + "</div>";
                                }
                                else if (reader["Area_No"].ToString() == "31" && intSalePrice > 300) // 台灣館，大於300元就顯示
                                {
                                    litPay3.Text = "<div class=\"TP_left_money_all\">$" + intSalePrice.ToString("N0") + "</div>";
                                }
                                else if (intSalePrice <= 5000 && intSalePrice > 0)
                                {
                                    litPay3.Text = "<div class=\"TP_left_money_none\">來電洽詢</div>";
                                }
                                else
                                {
                                    litPay3.Text = "<div class=\"TP_left_money_none\">不提供</div>";
                                }
                                break;
                            case "3": // 小孩 加床
                                //if (intSalePrice == 0) { litPay4.Text = "<div class=\"TP_left_money_none\">不提供</div>"; break; }
                                //if (intSalePrice <= 5000 && intSalePrice > 0) { litPay4.Text = "<div class=\"TP_left_money_none\">來電洽詢</div>"; break; }
                                if (intSalePrice > 5000)
                                {
                                    litPay4.Text = "<div class=\"TP_left_money_all\">$" + intSalePrice.ToString("N0") + "</div>";
                                }
                                else if (reader["Group_Category_No"].ToString() == "86" && intSalePrice > 3000) // 基隆起航，大於3000元就顯示
                                {
                                    litPay4.Text = "<div class=\"TP_left_money_all\">$" + intSalePrice.ToString("N0") + "</div>";
                                }
                                else if (reader["Area_No"].ToString() == "31" && intSalePrice > 300) // 台灣館，大於300元就顯示
                                {
                                    litPay4.Text = "<div class=\"TP_left_money_all\">$" + intSalePrice.ToString("N0") + "</div>";
                                }
                                else if (intSalePrice <= 5000 && intSalePrice > 0)
                                {
                                    litPay4.Text = "<div class=\"TP_left_money_none\">來電洽詢</div>";
                                }
                                else
                                {
                                    litPay4.Text = "<div class=\"TP_left_money_none\">不提供</div>";
                                }
                                break;
                        }
                        break;
                    case "I":
                        //if (intSalePrice == 0) { litPay5.Text = "<div class=\"TP_left_money_none\">不提供</div>"; break; }
                        //if (intSalePrice <= 5000 && intSalePrice > 0) { litPay5.Text = "<div class=\"TP_left_money_none\">來電洽詢</div>"; break; }
                        if (intSalePrice > 5000)
                        {
                            litPay5.Text = "<div class=\"TP_left_money_all\">$" + intSalePrice.ToString("N0") + "</div>";
                        }
                        else if (reader["Group_Category_No"].ToString() == "86" && intSalePrice > 3000) // 基隆起航，大於3000元就顯示
                        {
                            litPay5.Text = "<div class=\"TP_left_money_all\">$" + intSalePrice.ToString("N0") + "</div>";
                        }
                        else if (reader["Area_No"].ToString() == "31" && intSalePrice > 300) // 台灣館，大於300元就顯示
                        {
                            litPay5.Text = "<div class=\"TP_left_money_all\">$" + intSalePrice.ToString("N0") + "</div>";
                        }
                        else if (intSalePrice <= 5000 && intSalePrice > 0)
                        {
                            litPay5.Text = "<div class=\"TP_left_money_none\">來電洽詢</div>";
                        }
                        else
                        {
                            litPay5.Text = "<div class=\"TP_left_money_none\">不提供</div>";
                        }
                        break;
                    case "J":
                        //if (intSalePrice == 0) { litPay6.Text = "<div class=\"TP_left_money_none\">不提供</div>"; break; }
                        //if (intSalePrice <= 5000 && intSalePrice > 0) { litPay6.Text = "<div class=\"TP_left_money_none\">來電洽詢</div>"; break; }
                        if (intSalePrice > 5000)
                        {
                            litPay6.Text = "<div class=\"TP_left_money_all\">$" + intSalePrice.ToString("N0") + "</div>";
                        }
                        else if (reader["Group_Category_No"].ToString() == "86" && intSalePrice > 3000) // 基隆起航，大於3000元就顯示
                        {
                            litPay6.Text = "<div class=\"TP_left_money_all\">$" + intSalePrice.ToString("N0") + "</div>";
                        }
                        else if (reader["Area_No"].ToString() == "31" && intSalePrice > 300) // 台灣館，大於300元就顯示
                        {
                            litPay6.Text = "<div class=\"TP_left_money_all\">$" + intSalePrice.ToString("N0") + "</div>";
                        }
                        else if (intSalePrice <= 5000 && intSalePrice > 0)
                        {
                            litPay6.Text = "<div class=\"TP_left_money_none\">來電洽詢</div>";
                        }
                        else
                        {
                            litPay6.Text = "<div class=\"TP_left_money_none\">不提供</div>";
                        }
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

    private string getContent(string sel)
    {
        string str = "";

        switch (sel)
        {
            case "1":
                str = "1.小費：領隊導遊司機小費：每人每日新台幣200元小費給予領隊，(西藏、稻城亞丁之行程，每人每日新台幣300元小費給予領隊)，由領隊統籌付給當地的導遊及司機。<br />";
                str += "2.如參加包銷班機行程，則依包銷班機航空公司作業條件，作業方式將不受國外旅遊定型化契約書中第二十七條規範，如因個人因素取消旅遊、變更日期或行程之旅行，則需支付該行程全額機票款。<br />";
                str += "3.團體機票需團進團出，一經開票不接受更名、改期、更改航班或轉讓，如搭乘中國民航體系之航班，亦無退票價值(其餘航空公司則依照航空公司規定辦理)";
                break;
            default:
                str = "1.團費報價以雙人房(2人一室)為主，歡迎您結伴參加。若單數報名，須酌收全程單人房差額，或由本公司協助安排同性團友共用一室，若能順利調整，則免收單人房差額。<br />";
                str += "2.單人房為一人房(Single for Single use)，非雙人房供一人使用(Double for Single use)，單人房空間通常較雙人房小。<br /> ";
                str += "3.三人房通常為雙人房加一床，只接受一大二小或二大一小合住，加床大多為摺疊床、沙發床或行軍彈簧床，房間空間本就不大，加上三人份的行李，勢必影響住宿品質，故建議避免住宿三人房。<br />";
                str += "4.為遵守歐洲消防法規，12歲以上成人，不可加床三人(含以上)同室。2~12歲小孩皆需佔床，2歲以下嬰兒可不佔床。";
                break;
        }

        return str;
    }
    #endregion

    #region === Control ===
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string strSql = "";
        strSql = " select [Trip_File] from [trip].[dbo].[Trip] ";
        strSql += " where Trip_No = @Trip_No";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand comm = new SqlCommand(strSql, connect);
        comm.Parameters.Add(new SqlParameter("@Trip_No", _strTripNo));
        SqlDataReader reader = comm.ExecuteReader();
        string FileName = "";
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                FileName += reader["Trip_File"].ToString();
            }
        }
        else
        {
            Response.Write("<script>alert('目前沒有資料！');</script>");
        }

        if (!string.IsNullOrEmpty(FileName))
        {
            System.Net.WebClient wb = new System.Net.WebClient();
            //檔案路徑
            string link = HttpContext.Current.Server.MapPath("/TripFile/" + FileName);
            link = url + "/TripFile/" + FileName;

            Response.ClearHeaders();
            Response.Clear();
            Response.Expires = 0;
            Response.Buffer = true;
            Response.AddHeader("Accept-Language", "zh-tw");
            Response.AddHeader("Content-Disposition", "Attachment;FileName=" + FileName);
            Response.ContentType = "Application/pdf";

            Response.BinaryWrite(wb.DownloadData(link));
            Response.End();
        }
        else
        {
            Response.Write("<script>alert('目前沒有資料！');</script>");
        }
        reader.Close();
        comm.Dispose();
        connect.Close();
    }

    protected void PrintScheduleSubmit_Click(object sender, EventArgs e)
    {
        string strDownFileName = string.Empty;
        string strSql = "";
        strSql = " select [Trip_File] from [Trip] ";
        strSql += " where Trip_No = @Trip_No";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand comm = new SqlCommand(strSql, connect);
        comm.Parameters.Add(new SqlParameter("@Trip_No", Convert.ToString(Request.QueryString["TripNo"])));
        SqlDataReader reader = comm.ExecuteReader();
        string FileName = "";
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                FileName += reader["Trip_File"].ToString();
            }
        }
        else
        {
            Response.Write("<script>alert('目前沒有資料！');history.back();</script>");
        }

        if (!string.IsNullOrEmpty(FileName))
        {
            /////////////////////////////////////////////////////////////////////////////
            string CompanyName_t = "";  //公司名稱
            CompanyName_t = CompanyName.Text.ToString();

            string ContactName_t = "";  //聯絡人姓名
            ContactName_t = ContactName.Text.ToString();

            string ContactPhone_t = ""; //聯絡人電話  
            ContactPhone_t = ContactPhone.Text.ToString();

            string ContactEmail_t = ""; //LINE ID
            ContactEmail_t = ContactEmail.Text.ToString();

            bool SuccessSignal = Printpdf(FileName, CompanyName_t, ContactName_t, ContactPhone_t, ContactEmail_t);
            ///////////////////////////////////////////////////////////////////////////
            if (SuccessSignal)
            {

                System.Net.WebClient wb = new System.Net.WebClient();
                //檔案路徑
                string link = HttpContext.Current.Server.MapPath("~/finalPDF/" + FileName);

                string strAttachmentFileName = hidGropNumb.Value + " " + hidGropName.Value + Path.GetExtension(FileName);

                Response.ClearHeaders();
                Response.Clear();
                Response.Expires = 0;
                Response.Buffer = true;
                Response.AddHeader("Accept-Language", "zh-tw");
                Response.AddHeader("Content-Disposition", "Attachment;FileName=" + strAttachmentFileName);
                Response.ContentType = "Application/pdf";

                Response.BinaryWrite(wb.DownloadData(link));
                string strSavePath = HttpContext.Current.Server.MapPath("~/finalPDF/");
                string[] files = Directory.GetFiles(strSavePath);

                foreach (string file in files)
                {
                    System.IO.File.SetAttributes(file, FileAttributes.Normal);
                    System.IO.File.Delete(file);
                }
                Response.End();
            }
            else
            {
                HttpContext.Current.Response.Write("<script>alert('頁首頁尾錯誤！');history.go(-2);</script>");
            }
        }
        else
        {
            Response.Write("<script>alert('目前沒有資料！');history.back();</script>");
        }
        reader.Close();
        comm.Dispose();
        connect.Close();

    }
    #endregion

    #region === 列印 ===
    public bool Printpdf(string fileName, string CompanyName, string ContactName, string ContactPhone, string ContactEmail)
    {
        if (Directory.Exists(HttpContext.Current.Server.MapPath("~/finalPDF/")))
        {
            //資料夾存在
        }
        else
        {
            //新增資料夾
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/finalPDF/"));
        }

        // PDF 產生新檔案的放置地方
        string path = HttpContext.Current.Server.MapPath("~/finalPDF/");

        // ****************************************************************************************************
        // 新增分頁，並在分頁上加上公司資訊
        try
        {
            string sourcePdfPath = "http://www.artisan.com.tw/TripFile/" + fileName;
            using (var destinationDocumentStream = new FileStream(path + "/" + fileName, FileMode.Create, FileAccess.ReadWrite))
            {
                PdfReader reader = new PdfReader(sourcePdfPath);
                PdfStamper stamper = new PdfStamper(reader, destinationDocumentStream);
                int total = reader.NumberOfPages + 1;
                stamper.InsertPage(total, iTextSharp.text.PageSize.A4); //新增空白頁

                // 最後一頁加上公司資料
                BaseFont baseFont = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\msjh.ttc,0", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED); //微軟正黑體
                iTextSharp.text.Rectangle pageSize = reader.GetPageSizeWithRotation(total);
                PdfContentByte pdfPageContents = stamper.GetOverContent(total); // 在內容上方加上浮水印
                //PdfContentByte pdfPageContents = stamper.GetUnderContent(i); // 在內容下方加上浮水印
                pdfPageContents.BeginText();
                pdfPageContents.SetFontAndSize(baseFont, 16);
                pdfPageContents.SetRGBColorFill(192, 0, 0);
                pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "業務聯絡資訊", pageSize.GetLeft(Utilities.MillimetersToPoints(30)), pageSize.GetTop(Utilities.MillimetersToPoints(15)), 0);
                pdfPageContents.EndText();

                pdfPageContents.BeginText();
                pdfPageContents.SetFontAndSize(baseFont, 13);
                pdfPageContents.SetRGBColorFill(0, 0, 0);
                //pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_CENTER, CompanyName, (pageSize.Left + pageSize.Right) / 2, pageSize.GetTop(Utilities.MillimetersToPoints(10)), 0);
                pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_LEFT, CompanyName, pageSize.GetLeft(Utilities.MillimetersToPoints(30)), pageSize.GetTop(Utilities.MillimetersToPoints(22)), 0);
                pdfPageContents.EndText();

                pdfPageContents.BeginText();
                pdfPageContents.SetFontAndSize(baseFont, 13);
                pdfPageContents.SetRGBColorFill(0, 0, 0);
                pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "業務代表：" + ContactName, pageSize.GetLeft(Utilities.MillimetersToPoints(30)), pageSize.GetTop(Utilities.MillimetersToPoints(29)), 0);
                pdfPageContents.EndText();

                pdfPageContents.BeginText();
                pdfPageContents.SetFontAndSize(baseFont, 13);
                pdfPageContents.SetRGBColorFill(0, 0, 0);
                pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "電話：" + ContactPhone, pageSize.GetLeft(Utilities.MillimetersToPoints(30)), pageSize.GetTop(Utilities.MillimetersToPoints(36)), 0);
                pdfPageContents.EndText();

                pdfPageContents.BeginText();
                pdfPageContents.SetFontAndSize(baseFont, 13);
                pdfPageContents.SetRGBColorFill(0, 0, 0);
                pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "LINE ID：" + ContactEmail, pageSize.GetLeft(Utilities.MillimetersToPoints(30)), pageSize.GetTop(Utilities.MillimetersToPoints(43)), 0);
                pdfPageContents.EndText();

                stamper.Close();
                reader.Close();
            }

            return true;
        }
        catch (Exception)
        {
            //throw;
        }

        // ****************************************************************************************************
        // 每一頁的右下方加上公司相關資訊。
        //try
        //{
        //    string sFileIn = "http://www.artisan.com.tw/TripFile/" + fileName;
        //    PdfReader reader = new PdfReader(sFileIn);
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        PdfStamper stamper = new PdfStamper(reader, ms);
        //        for (int i = 1; i <= reader.NumberOfPages; i++)
        //        {
        //            BaseFont baseFont = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\msjh.ttc,0", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        //            //iTextSharp.text.Font blue = new iTextSharp.text.Font(baseFont, 12);
        //            //Phrase p = new Phrase(CompanyName, blue);

        //            iTextSharp.text.Rectangle pageSize = reader.GetPageSizeWithRotation(i);
        //            PdfContentByte pdfPageContents = stamper.GetOverContent(i); // 在內容上方加上浮水印
        //            //PdfContentByte pdfPageContents = stamper.GetUnderContent(i); // 在內容下方加上浮水印
        //            pdfPageContents.BeginText();
        //            pdfPageContents.SetFontAndSize(baseFont, 12);
        //            pdfPageContents.SetRGBColorFill(0, 0, 255);
        //            pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, CompanyName, pageSize.GetRight(130), pageSize.GetBottom(25), 0);
        //            pdfPageContents.SetTextMatrix(pageSize.GetLeft(25), pageSize.GetBottom(30));
        //            pdfPageContents.EndText();

        //            pdfPageContents.BeginText();
        //            pdfPageContents.SetFontAndSize(baseFont, 12);
        //            pdfPageContents.SetRGBColorFill(0, 0, 255);
        //            pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, ContactName, pageSize.GetRight(40), pageSize.GetBottom(25), 0);
        //            pdfPageContents.EndText();

        //            pdfPageContents.BeginText();
        //            pdfPageContents.SetFontAndSize(baseFont, 12);
        //            pdfPageContents.SetRGBColorFill(0, 0, 255);
        //            pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, ContactPhone, pageSize.GetRight(130), pageSize.GetBottom(10), 0);
        //            pdfPageContents.EndText();

        //            pdfPageContents.BeginText();
        //            pdfPageContents.SetFontAndSize(baseFont, 12);
        //            pdfPageContents.SetRGBColorFill(0, 0, 255);
        //            pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, ContactEmail, pageSize.GetRight(40), pageSize.GetBottom(10), 0);
        //            pdfPageContents.EndText();
        //        }

        //        stamper.FormFlattening = true;
        //        stamper.Close();
        //        FileStream fs = new FileStream(path + "/" + fileName, FileMode.Create, FileAccess.ReadWrite);
        //        BinaryWriter bw = new BinaryWriter(fs);
        //        bw.Write(ms.ToArray());
        //        bw.Close();
        //        reader.Close();

        //        return true;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    HttpContext.Current.Response.Write("<script>alert('頁首頁尾錯誤！');history.go(-2);</script>");
        //}

        return false;
    }
    #endregion

    #region === FIT 費用表 ===
    // 判斷是否為自由行
    private string fnRtnIsFit()
    {
        string strTrip_No = "";
        string strSql = "";
        strSql = "select Trip_No,IsFIT,Trip_FIT_Price";
        strSql += " from trip";
        strSql += " where Trip.Trip_No = @Trip_No";
        strSql += " and IsFIT = 1";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@Trip_No", _strTripNo));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                strTrip_No = reader["Trip_No"].ToString();
                _strIsFIT = "Y";
            }
            reader.Close();
            comm.Dispose();
        }
        catch { }
        finally
        {
            connect.Close();
        }

        return strTrip_No;
    }
    //FIT 費用表
    private void getFitCostList()
    {
        //FitCost.Visible = true;
        //litFit2.Text = "<a href='javascript:;'><i class='fa fa-list-alt' aria-hidden='true'></i><span>詳細費用</span></a>";
        //litFit.Text = "<li><a href='javascript:;'><i class='fa fa-list-alt' aria-hidden='true'></i><span>詳細行程費用</span></a></li>";

        DataTable dt = new DataTable();
        string strSql = "";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            strSql = " SELECT Number,FName,FUnit,FPrice,row_number() OVER(ORDER BY orderby) as n FROM Trip_Fit";
            strSql += " WHERE IsSelfPay = 0";
            strSql += " AND Trip_No = @Trip_No";
            strSql += " ORDER BY orderby";

            SqlDataAdapter da = new SqlDataAdapter(strSql, connect);
            da.SelectCommand.Parameters.Add(new SqlParameter("@Trip_No", _strTripNo));
            da.Fill(dt);

            GridView1.DataSource = dt;
            GridView1.DataBind();


            strSql = " SELECT *,row_number() OVER(ORDER BY orderby) as n  FROM Trip_Fit";
            strSql += " WHERE IsSelfPay = 1";
            strSql += " AND Trip_No = @Trip_No";
            strSql += " ORDER BY orderby";

            da = new SqlDataAdapter(strSql, connect);
            da.SelectCommand.Parameters.Add(new SqlParameter("@Trip_No", _strTripNo));
            dt = new DataTable();
            da.Fill(dt);

            GridView2.DataSource = dt;
            GridView2.DataBind();

            if (GridView1.Rows.Count + GridView2.Rows.Count > 0)
            {
                section_FitPriceList.Visible = true;
                nav_FitPrice.Visible = true;
            }
            else
            {
                // 若自由行沒有資料的話，團費資訊的右邊不需要有灰色的直線。
                linkGroupPrice.Attributes.Add("class", "TripNav-noline");
            }
        }
        catch (Exception ex)
        {
            //例外的處理方法，如秀出警告
        }
        finally { connect.Close(); }
    }
    #endregion

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TableCellCollection cell = e.Row.Cells;
            cell[0].Attributes.Add("data-th", "行程內容包含:");
            cell[1].Attributes.Add("data-th", "單位:");
            cell[2].Attributes.Add("data-th", "費用:");
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TableCellCollection cell = e.Row.Cells;
            cell[0].Attributes.Add("data-th", "自費加購項目:");
            cell[1].Attributes.Add("data-th", "單位:");
            cell[2].Attributes.Add("data-th", "費用:");
        } 
    }

    #region === Function ===
    /// <summary>
    /// 移除html tag
    /// </summary>
    /// <param name="htmlSource"></param>
    /// <returns></returns>
    public string RemoveHTMLTag(string htmlSource)
    {
        //移除 javascript code.
        htmlSource = Regex.Replace(htmlSource, @"<script[\d\D]*?>[\d\D]*?</script>", String.Empty);

        //移除 html tag.
        htmlSource = Regex.Replace(htmlSource, @"<[^>]*>", delegate (Match m)
        {
            //如果查到的字串是以"<br"為開頭, 就不取代, 而回傳原本比對到的字串
            if (m.Value.StartsWith("<br", StringComparison.OrdinalIgnoreCase))
            {
                return m.Value;
            }
            else
            {
                //除了<br>以外的字串,都換成空字串
                return string.Empty;
            }
        });
        htmlSource = Regex.Replace(htmlSource, @"([\r\n])[\s]+", String.Empty);
        htmlSource = Regex.Replace(htmlSource, @"-->", String.Empty);
        htmlSource = Regex.Replace(htmlSource, @"<!--.*", String.Empty);
        htmlSource = Regex.Replace(htmlSource, @"&(quot|#34);", String.Empty);
        htmlSource = Regex.Replace(htmlSource, @"&(amp|#38);", String.Empty);
        htmlSource = Regex.Replace(htmlSource, @"&(lt|#60);", String.Empty);
        htmlSource = Regex.Replace(htmlSource, @"&(gt|#62);", String.Empty);
        htmlSource = Regex.Replace(htmlSource, @"&(nbsp|#160);", String.Empty);
        htmlSource = Regex.Replace(htmlSource, @"&(iexcl|#161);", String.Empty);
        htmlSource = Regex.Replace(htmlSource, @"&(cent|#162);", String.Empty);
        htmlSource = Regex.Replace(htmlSource, @"&(pound|#163);", String.Empty);
        htmlSource = Regex.Replace(htmlSource, @"&(copy|#169);", String.Empty);

        return htmlSource;
    }
    #endregion
}