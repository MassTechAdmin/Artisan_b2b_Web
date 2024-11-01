using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Org.BouncyCastle.Asn1.X509;

public partial class Index_demo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsFunction.Check.CheckSession();
        if (IsPostBack) return;
        Counter.Counter.fn_Account();
        txtDatePicker1.Value = DateTime.Today.ToString("yyyy/MM/dd");
        txtDatePicker2.Value = DateTime.Today.AddMonths(6).ToString("yyyy/MM/dd");

        fn_DropDownList_Area();
        fn_DropDownList_Group_Category();
        fn_Show_Sales_Data();
        fn_Banner();
        fn_Special_Banner();
        fn_Show_Gold_Sales();
        fn_Show_News();
        fn_HomeNews();
        fn_HomeBanner();
        //getAdMask();
        Recommend_sale();
    }

    #region === 特惠促銷 ===
    string pic_url = "http://b2b.artisan.com.tw/";
    protected void fn_Banner()
    {
        string strSql = "";
        strSql += " select * from Banner";
        strSql += " where 1=1";
        strSql += " order by banner_order";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            Int32 intItem = 0;
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader["banner_sec"].ToString() == "1")
                    {
                        intItem += 1;

                        // 左邊圖片
                        //litBannerLeft.Text += "<div class=\"item\">";
                        //litBannerLeft.Text += "<a href=\"" + reader["banner_link"].ToString() + "\">";
                        //litBannerLeft.Text += "<img src=\"" + pic_url + "ad1/img/" + reader["banner_pic"].ToString() + "\" alt=\"" + reader["banner_Title"].ToString() + "\">";
                        //litBannerLeft.Text += "</a>";
                        //litBannerLeft.Text += "</div>";

                        // 右邊文字
                        //litBannerRight.Text += "<li class=\"loacl_" + intItem.ToString() + " " + (intItem == 1 ? "on" : "") + "\">" + reader["banner_Title"].ToString() + "</li>";
                        litBannerRight.Text += "<li><a href=\"" + reader["banner_link"].ToString() + "\">" + reader["banner_Title"].ToString() + "</a></li>";
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
    #endregion

    #region === 搜尋選項 ===
    protected void fn_DropDownList_Area()
    {
        string strSql = "";
        strSql += " SELECT [Area_No], [Area_Name] FROM [Area] ORDER BY [Area_No]";
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

        DropDownList1.Items.Insert(0, new ListItem("全選", "0"));
        //DropDownList1.SelectedIndex = 15;
    }

    protected void fn_DropDownList_Group_Category()
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

        DropDownList2.Items.Insert(0, new ListItem("全選", "0"));
        DropDownList2.SelectedIndex = 0;
    }

    protected void imgButton_Click(object sender, EventArgs e)
    {

        string strOutDate = "";
        string strOutDate2 = "";
        string strArea = "";
        string strCountry = "";
        string tt = "";
        string Url = "ClassifyProduct.aspx?l=l";

        //if (Convert.ToString(RadDatePicker1.SelectedDate) != null)
        //{
        //    if (RadDatePicker1.SelectedDate >= new DateTime(2012, 1, 1))
        //    {
        //        strOutDate = Convert.ToString(RadDatePicker1.SelectedDate.ToShortDateString());
        //        Url += "&RadDatePicker1=" + strOutDate;
        //    }
        //}

        //if (Convert.ToString(RadDatePicker2.SelectedDate) != null)
        //{
        //    if (RadDatePicker2.SelectedDate >= new DateTime(2012, 1, 1))
        //    {
        //        strOutDate2 = Convert.ToString(RadDatePicker2.SelectedDate.ToShortDateString());
        //        Url += "&RadDatePicker2=" + strOutDate2;
        //    }
        //}

        if (clsFunction.Check.IsDate(txtDatePicker1.Value.Trim()))
        {
            if (Convert.ToDateTime(txtDatePicker1.Value.Trim()) >= new DateTime(2012, 1, 1))
            {
                strOutDate = txtDatePicker1.Value.Trim();
                Url += "&RadDatePicker1=" + strOutDate;
            }
        }

        if (clsFunction.Check.IsDate(txtDatePicker2.Value.Trim()))
        {
            if (Convert.ToDateTime(txtDatePicker2.Value.Trim()) >= new DateTime(2012, 1, 1))
            {
                strOutDate2 = txtDatePicker2.Value.Trim();
                Url += "&RadDatePicker2=" + strOutDate2;
            }
        }

        if (string.IsNullOrEmpty(txbKey.Text.Trim()))
        {
            if (DropDownList1.SelectedValue != null && DropDownList1.SelectedIndex != 0)
            {
                strArea = DropDownList1.SelectedValue;
                Url += "&area=" + strArea;
            }

            //if (DropDownList2.SelectedValue != "" && DropDownList2.SelectedIndex != 0)
            //{
            //    strCountry = DropDownList2.SelectedValue;
            //    Url += "&sgcn=" + hidArea_no.Value;
            //}

            if (hidArea_no.Value != "" && hidArea_no.Value != "0")
            {
                strCountry = DropDownList2.SelectedValue;
                Url += "&sgcn=" + hidArea_no.Value;
            }
        }
        else
        {
            tt = HttpUtility.UrlEncode(txbKey.Text.Trim().Replace("'", ""));
            Url += "&tp=" + tt;
        }

        if (ddlAirport.SelectedValue != "")
        {
            Url += "&airport=" + ddlAirport.SelectedValue;
        }

        switch (DropDownList0.SelectedValue)
        {
            case "巨匠":
                Url += "&tourtype=0";
                break;
            case "新視界":
                Url += "&tourtype=1";
                break;
            case "典藏":
                Url += "&tourtype=2";
                break;
            case "珍藏":
                Url += "&tourtype=3";
                break;
        }

        if (chkOK.Checked && chkSign.Checked)
        { Url += "&signtype=1,2"; }
        else if (chkOK.Checked)
        { Url += "&signtype=1"; }
        else if (chkSign.Checked)
        { Url += "&signtype=2"; }

        if (chkB2B_Only.Checked)
        { Url += "&show=2"; }

        if (Url == "ClassifyProduct.aspx?l=l")
        {
            Url = "ClassifyProduct.aspx";
        }

        Response.Redirect(Url);
    }
    #endregion

    #region === 登入資訊 ===
    protected void fn_Show_Sales_Data()
    {
        string strSql = "";
        strSql += " SELECT AGENT_M.AGT_NAME2,AGT_CONT,AGT_IDNo,Person.name AS SALE_NAME,PerNO";
        strSql += " ,CONT_ZONE,CONT_TEL,AGENT_L.CFAX_ZONE,AGENT_L.CONT_FAX,CONT_CELL";
        strSql += " ,CONT_MAIL,AGENT_M.TEL1,Person.Pager,Person.E_Mail,FAX_ZONE,AGENT_M.FAX,compno";
        strSql += " FROM AGENT_L";
        strSql += " LEFT JOIN AGENT_M ON AGENT_L.AGT_NAME1 = AGENT_M.AGT_NAME1";
        strSql += " LEFT JOIN Person ON Person.perno = AGENT_L.SALE_CODE";
        strSql += " WHERE AGENT_L.AGT_IDNo = @AGT_IDNo";

        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(constring);
        try
        {
            connect.Open();
            SqlCommand command = new SqlCommand(strSql, connect);
            command.Parameters.Add(new SqlParameter("@AGT_IDNo", Session["PERNO"]));
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                //<div class="user-info-list">負責業務：<span>VAN</span></div>
                litUserInfo.Text += "<div class='user-info-list'>負責業務：<span>" + reader["SALE_NAME"].ToString() + "</span></div>";
                litUserInfo.Text += "<div class='user-info-list'>電子郵件：<span>" + reader["E_Mail"].ToString() + "</span></div>";
                switch (reader["compno"].ToString())
                {
                    case "B": // 高雄
                        litUserInfo.Text += "<div class='user-info-list'>公司電話：<span>07-241-9888</span></div>";
                        litUserInfo.Text += "<div class='user-info-list'>傳真：<span>07-215-4690</span></div>";
                        break;
                    case "C":  // 台中
                        litUserInfo.Text += "<div class='user-info-list'>公司電話：<span>04-2255-1168</span></div>";
                        litUserInfo.Text += "<div class='user-info-list'>傳真：<span>04-2255-1169</span></div>";
                        break;
                    case "D": // 凱旋-桃園
                        litUserInfo.Text += "<div class='user-info-list'>公司電話：<span>03-332-1251</span></div>";
                        litUserInfo.Text += "<div class='user-info-list'>傳真：<span>03-332-1315</span></div>";
                        break;
                    case "F": // 台南
                        litUserInfo.Text += "<div class='user-info-list'>公司電話：<span>06-222-6736</span></div>";
                        litUserInfo.Text += "<div class='user-info-list'>傳真：<span>06-222-6731</span></div>";
                        break;
                    case "H": // 新竹
                        litUserInfo.Text += "<div class='user-info-list'>公司電話：<span>03-5283-088</span></div>";
                        litUserInfo.Text += "<div class='user-info-list'>傳真：<span>03-5283-389</span></div>";
                        break;
                    default:
                        litUserInfo.Text += "<div class='user-info-list'>公司電話：<span>02-2518-0011</span></div>";
                        litUserInfo.Text += "<div class='user-info-list'>傳真：<span>02-2518-5488</span></div>";
                        break;
                }

                // litUserInfo.Text += "<td>手機：" + reader["Pager"].ToString() + "</td>";
                string strPager = reader["Pager"].ToString();
                string strTel = "";
                strPager = strPager.Replace("-", "");
                if (strPager.Length >= 4) strTel += strPager.Substring(0, 4);
                if (strPager.Length >= 7) strTel += (strTel == "" ? "" : "-") + strPager.Substring(4, 3);
                if (strPager.Length >= 10) strTel += (strTel == "" ? "" : "-") + strPager.Substring(7, 3);
                litUserInfo.Text += "<div class='user-info-list'>手機：<span>" + strTel + "</span></div>";

            }
            reader.Close();
            command.Dispose();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            connect.Close();
        }
    }
    #endregion

    #region === 超低優惠．熱門搶購 ===
    protected void fn_Special_Banner()
    {
        string strSql = "";
        strSql += " select * from Special_Banner";
        strSql += " order by SB_Order";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    litSB1.Text += "<li>";
                    litSB1.Text += "<a href=\"" + reader["SB_Link"].ToString() + "\" target=\"_blank\">";
                    litSB1.Text += "<img src=\"" + pic_url + reader["SB_Pic"].ToString() + "\" alt=\"\" width=\"100%\">";
                    litSB1.Text += "</a>";
                    litSB1.Text += "</li>";

                    //litSB2.Text += "<div class=\"item\">";
                    //litSB2.Text += "<a href=\"" + reader["SB_Link"].ToString() + "\" target=\"_blank\">";
                    //litSB2.Text += "<img src=\"" + pic_url + reader["SB_Pic"].ToString() + "\" alt=\"\" width=\"100%\">";
                    //litSB2.Text += "</a>";
                    //litSB2.Text += "</div>";
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
    #endregion

    #region === 下方行程列表 ===
    protected void fn_Show_Gold_Sales()
    {
        string strSql = "";
        strSql += " select *" +
                " ,ROW_NUMBER() OVER (PARTITION BY Area_Name ORDER BY Area_B2B_OrderBy" +
                " ,Gold_Area,Gold_Order) as order_row_id" + 
                " ,COUNT(1) over(PARTITION BY Area_Name) order_row_cnt" +
                " from (" +

                "     select Gold_Sales.*,Area_B2B_OrderBy" +
                "     ,(case when Gold_Sales.Gold_Area = '90000' then '新視界'" +
                "            when Gold_Sales.Gold_Area = '90001' then '典藏'" +
                "            when Gold_Sales.Gold_Area = '90002' then '中國'" +
                //20200327
                "            when Gold_Sales.Gold_Area = '90003' then '日本'" +
                "            else Area.Area_Name end) as Area_Name" +
                "     from Gold_Sales" +
                "     left join trip.dbo.area area on area.area_no = Gold_Sales.Gold_Area" +
                "     where Gold_Sales.Gold_Area <> '90000' and Gold_Sales.Gold_Area <> '90001'" +
                // 中國線要換成美加線
                // 言臻，南歐 北歐 下排中間插入< 中亞.蒙古國 >、<中國> by roger 20240110 
                "     and Area.Area_ID in ('Area1','Area2','Area3','Area4','Area6','Area7','Area11','Area10','Area21','Area18')" +
                " ) tablea" +
                " order by  Area_B2B_OrderBy,Gold_Area,Gold_Order ";
        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(constring);
        try
        {
            connect.Open();
            SqlCommand command = new SqlCommand(strSql, connect);
            SqlDataReader reader = command.ExecuteReader();
            //string strAreaNo = "";
            while (reader.Read())
            {
                //if (strAreaNo != reader["Gold_Area"].ToString() && strAreaNo != "")
                //{
                //    litNot.Text += "</ul>";
                //    litNot.Text += "</div>";
                //}

                //if (strAreaNo != reader["Gold_Area"].ToString())
                //{
                //    litNot.Text += "<div class=\"trip-box\">";

                //    if (reader["Gold_Area"].ToString() == "90002")  //中國，Area_No=37
                //    { litNot.Text += "<a href=\"exh/Exhibition.aspx?tourtype=2&area=37 \" class=\"trip-box-more\" target='_bank'>更多行程+</a>"; }

                //    else if (reader["Gold_Area"].ToString() == "90003")
                //    { litNot.Text += "<a href=\"exh/Exhibition.aspx?tourtype=2&area=35 \" class=\"trip-box-more\" target='_bank'>更多行程+</a>"; }

                //    else
                //    { litNot.Text += "<a href=\"exh/Exhibition.aspx?area=" + reader["Gold_Area"].ToString() + "\" class=\"trip-box-more\" target='_bank'>更多行程+</a>"; }

                //    litNot.Text += "<div id=\"west-europe\" class=\"trip-box-tittle\">";
                //    litNot.Text += "<h3>" + reader["Area_Name"].ToString() + "</h3>";
                //    litNot.Text += "</div>";

                //    litNot.Text += "<ul>";
                //}

                //litNot.Text += "<li>";
                //litNot.Text += "<a href=\"" + reader["Gold_Link"].ToString() + "\" target='_bank'>";
                //if (clsFunction.Check.IsNumeric(reader["Gold_Pirce"].ToString()))
                //{ litNot.Text += "<p class=\"ellipsis\">" + reader["Gold_Title"].ToString() + "</p><span class=\"price\">" + reader["Gold_Pirce"].ToString() + "</span>"; }
                //else
                //{ litNot.Text += "<p class=\"ellipsis\">" + reader["Gold_Title"].ToString() + "</p><span class=\"price_txt\">" + reader["Gold_Pirce"].ToString() + "</span>"; }
                //litNot.Text += "</a>";
                //litNot.Text += "</li>";

                //strAreaNo = reader["Gold_Area"].ToString();

                if (reader["order_row_id"].ToString() == "1")
                {
                    litNot.Text += "<div class=\"trip-box animation__el in\">";
                    //litNot.Text += "<a href = \"exh/Exhibition.aspx?area=1\" class=\"trip-box-more\" target='_bank'>更多行程+</a>";
                    if (reader["Gold_Area"].ToString() == "90002")  //中國，Area_No=37
                    { litNot.Text += "<a href=\"exh/Exhibition.aspx?tourtype=2&area=37 \" class=\"trip-box-more\" target='_bank'>更多行程+</a>"; }

                    else if (reader["Gold_Area"].ToString() == "90003")
                    { litNot.Text += "<a href=\"exh/Exhibition.aspx?tourtype=2&area=35 \" class=\"trip-box-more\" target='_bank'>更多行程+</a>"; }

                    else
                    { litNot.Text += "<a href=\"exh/Exhibition.aspx?area=" + reader["Gold_Area"].ToString() + "\" class=\"trip-box-more\" target='_bank'>更多行程+</a>"; }

                    litNot.Text += "<div id = \"west-europe\" class=\"trip-box-tittle\">";
                    litNot.Text += "<h3>" + reader["Area_Name"].ToString() + "</h3>";
                    litNot.Text += "</div>";

                    litNot.Text += "<ul>";
                }

                litNot.Text += "<li><a href = \"" + reader["Gold_Link"].ToString() + "\" target='_bank'>";
                //litNot.Text += "<p class=\"ellipsis\">瑞士 懸崖奇景菲斯特 二大遊船 三大纜車 四大觀景列車 五大名峰 13天</p>";
                if (clsFunction.Check.IsNumeric(reader["Gold_Pirce"].ToString()))
                { litNot.Text += "<p class=\"ellipsis\">" + reader["Gold_Title"].ToString() + "</p><span class=\"price\">" + reader["Gold_Pirce"].ToString() + "</span>"; }
                else
                { litNot.Text += "<p class=\"ellipsis\">" + reader["Gold_Title"].ToString() + "</p><span class=\"price_txt\">" + reader["Gold_Pirce"].ToString() + "</span>"; }
                //litNot.Text += "<span class=\"price\">" + reader["Gold_Pirce"].ToString() + "</span>";
                litNot.Text += "</a></li>";

                //litNot.Text += "<li><a href = \"https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20231213000002\" target='_bank'>";
                //litNot.Text += "<p class=\"ellipsis\">黃金德瑞 景觀列車 雙峰 13天</p>";
                //litNot.Text += "<span class=\"price\">148,900</span>";
                //litNot.Text += "</a></li>";

                //litNot.Text += "<li><a href = \"https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20230818000005\" target='_bank'>";
                //litNot.Text += "<p class=\"ellipsis\">小英倫 湖區彼得兔 巨石陣 OUTLET趣 9 天</p>";
                //litNot.Text += "<span class=\"price\">94,900</span>";
                //litNot.Text += "</a></li>";

                if (reader["order_row_id"].ToString() == reader["order_row_cnt"].ToString())
                {
                    litNot.Text += "</ul>";

                    litNot.Text += "</div>";
                }
            }

            //if (litNot.Text.Trim() != "")
            //{
            //    litNot.Text += "</ul>";
            //    litNot.Text += "</div>";
            //}
            reader.Close();
            command.Dispose();
        }
        catch (Exception ex) { }
        finally
        {
            connect.Close();
        }
    }
    #endregion

    #region === 跑馬燈 ===
    protected void fn_Show_News()
    {
        //最新消息
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection conn = new SqlConnection(strConnString);
        conn.Open();

        string strsql = "";
        int i = 1;

        strsql = "SELECT * FROM New_Web_four ";
        strsql += " where tag ='1' ";
        strsql += " ORDER BY cast(OrderBy as int)";

        SqlCommand comm = new SqlCommand(strsql, conn);
        SqlDataReader reader = comm.ExecuteReader();

        while (reader.Read())
        {
            litnews.Text += "<a href='" + reader["URL"].ToString().Replace("www.", "b2b.") + "'>" + i + ". ";
            litnews.Text += reader["Title"].ToString() + "</a>";
            litnews.Text += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            i++;
        }

        comm.Dispose();
        reader.Close();

        conn.Close();
        conn = null;
    }
    #endregion

    #region === 最新公告訊息 ===
    protected void fn_HomeNews()
    {
        string strSql = "";
        strSql += " select * from Home_News";
        strSql += " where 1=1";
        strSql += " order by HN_Orderby";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                litHomeNews.Text += "<div class=\"b2b-alert-list\">";
                while (reader.Read())
                {
                    if (reader["HN_Url"].ToString() == "")
                    {
                        litHomeNews.Text += "<div>" + reader["HN_MSG"].ToString() + "<span class=\"alert-list-btn\">X</span></div>";
                    }
                    else
                    {
                        litHomeNews.Text += "<div><a href='" + reader["HN_Url"].ToString() + "' target='_bank'>" + reader["HN_MSG"].ToString() + "</a><span class=\"alert-list-btn\">X</span></div>";
                    }
                }
                litHomeNews.Text += "</div>";
            }
            reader.Close();
            comm.Dispose();
        }
        finally
        {
            connect.Close();
        }
    }
    #endregion

    #region === 大廣告 ===
    //private void getAdMask()
    //{
    //litAD.Text = "";
    //string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
    //SqlConnection conn = new SqlConnection(strConnString);

    //try
    //{
    //conn.Open();

    //string strsql = "";
    //strsql = " select top 10 * from AD_mask";
    //strsql += " order by OrderBY,Number desc";

    //SqlCommand comm = new SqlCommand(strsql, conn);
    //SqlDataReader reader = comm.ExecuteReader();

    //if (reader.HasRows)
    //{
    //while (reader.Read())
    //{
    //litAD.Text += "<a href='" + reader["url"].ToString() + "'><img src='" + reader["pic"].ToString() + "' class='AD_img'></a>";

    //adnum.Value = reader["setTime"].ToString() + "000";
    //}
    //}

    //comm.Dispose();
    //reader.Close();
    //}
    //catch { }
    //finally { conn.Close(); conn = null; }
    //}
    #endregion

    protected void fn_HomeBanner()
    {
        string strSql = "";
        strSql += " select * from Home_Banner";
        strSql += " where 1=1";
        strSql += " order by HB_Order";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                //litHomeBanner.Text += "<div class=\"b2b-alert-AD\">";
                //litHomeBanner.Text += "<div class=\"owl-carousel owl-theme\" id=\"B2bAD-owl-carousel\">";
                //while (reader.Read())
                //{
                //    litHomeBanner.Text += "<a href='" + reader["HB_Link"].ToString() + "' target='_bank'><img src=\"" + reader["HB_Pic"].ToString() + "\" alt=\"\"></a>";
                //}
                //litHomeBanner.Text += "</div>";
                //litHomeBanner.Text += "<span class=\"alert-AD-btn\">收合▲</span>";
                //litHomeBanner.Text += "</div>";


                litHomeBanner.Text += "<div class=\"b2b-alert-AD\">";
                litHomeBanner.Text += "<div class=\"owl-carousel owl-theme\" id=\"B2bAD-owl-carousel\">";
                while (reader.Read())
                {
                    litHomeBanner.Text += "<a href='" + reader["HB_Link"].ToString() + "' target='_bank'><img src=\"" + reader["HB_Pic"].ToString() + "\" alt=\"\"></a>" + "\r\n";
                }
                litHomeBanner.Text += "</div>";
                litHomeBanner.Text += "<span class=\"alert-AD-btn\">收合▲</span>";
                litHomeBanner.Text += "</div>";
            }
            reader.Close();
            comm.Dispose();
        }
        finally
        {
            connect.Close();
        }
    }
    //巨匠系列推薦行程
    protected void Recommend_sale()
    {
        string strSql = "";
        strSql = " select TOP 8 RecommendSales.*";
        strSql += " ,(case when RecommendSales.Rec_Area = '90002' then '中國' else Area.Area_Name end) as Area_Name";
        strSql += " from RecommendSales";
        strSql += " left join trip.dbo.area on area.area_no = RecommendSales.Rec_Area";
        strSql += " where Rec_GroupName = N'1'";
        strSql += " order by Rec_Order";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    RecSale.Text += "<li><a href=\"" + reader["Rec_Link"].ToString() + "\">" + reader["Rec_Title"].ToString() + "</a></li>";
                }
            }
            reader.Close();
            comm.Dispose();
        }
        finally
        {
            connect.Close();
        }


        strSql = " select TOP 8 RecommendSales.*";
        strSql += " ,(case when RecommendSales.Rec_Area = '90002' then '中國' else Area.Area_Name end) as Area_Name";
        strSql += " from RecommendSales";
        strSql += " left join trip.dbo.area on area.area_no = RecommendSales.Rec_Area";
        strSql += " where Rec_GroupName = N'2'";
        strSql += " order by Rec_Order";
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    RecSale2.Text += "<li><a href=\"" + reader["Rec_Link"].ToString() + "\">" + reader["Rec_Title"].ToString() + "</a></li>";
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
}