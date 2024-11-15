﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Exh_Exhibition : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Format obj = new Format();
        //if (!obj.IsNumeric(Request["area_no"])) { Response.Redirect("~/"); }
        clsFunction.Check.CheckSession();

        if (!IsPostBack)
        {
            if (Convert.ToString(Request["area"]) == "6") { Page.Title += ",冰島旅遊,極光旅遊"; }
            fn_DropDownList_Area();
            string strClass = Convert.ToString(Request["tourtype"]);
            DropDownList0.SelectedIndex = DropDownList0.Items.IndexOf(DropDownList0.Items.FindByValue(strClass));   //下拉預設帶系列參數
            string strArea = Convert.ToString(Request["area"]);
            DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(strArea));  //下拉預設帶地區參數
        }
        String AreaID = getAreaID();
        getGroupCategoryName(AreaID);
        if (!IsPostBack)
        {
            DefaultAllSelect(); //載入時預設全選
        }
    }
    protected void DropDownList0_SelectedIndexChanged(object sender, EventArgs e)
    {
        TripLoacation();    //更新行程
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        String AreaID = getAreaID();
        getGroupCategoryName(AreaID);
        DefaultAllSelect(); //切換時預設全選
    }
    //地區下拉資料來源
    protected void fn_DropDownList_Area()
    {
        string strSql = "";
        strSql += " SELECT [Area_No], [Area_Name] FROM [Area] where Array <> '0' and Area_ID <> 'Area999' ORDER BY [Area_No]";
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

        // DropDownList1.Items.Insert(0, new ListItem("全選", "0"));
        DropDownList1.SelectedIndex = 0;
    }
    //取得地區的AreaID
    protected String getAreaID()
    {
        String AreaID = "";
        string connstr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(connstr);
        try
        {
            conn.Open();
            if (!string.IsNullOrEmpty(DropDownList1.SelectedValue))
            {
                string sqlstr = "";
                sqlstr += " SELECT Area_Name,Area_ID FROM Area";
                sqlstr += " WHERE Area_No=@Area_No";
                SqlCommand comm = new SqlCommand(sqlstr, conn);
                comm.Parameters.Add(new SqlParameter("@Area_No", DropDownList1.SelectedValue));
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AreaID = reader["Area_ID"].ToString();
                        Literal5.Text = "<h1 class=\"Exhibition_filter_h1\">" + reader["Area_Name"].ToString() + "</h1>";
                    }
                }
                reader.Close();
                comm.Dispose();
            }
        }
        finally
        {
            conn.Close();
        }
        return AreaID;
    }
    //透過AreaID產生CheckBoxList
    protected void getGroupCategoryName(string AreaID)
    {
        int TripLoacationCount = 2;
        PlaceHolder1.Controls.Clear();
        try
        {
            if (!String.IsNullOrEmpty(DropDownList1.SelectedValue))
            {
                string connstr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(connstr);
                conn.Open();
                string sqlstr = "";
                if (AreaID == "Area6") // 因希臘、埃及 要放在古文名內，所以就需要額外條件
                {
                    sqlstr += " SELECT Group_Category_No,Group_Category_Name";
                    sqlstr += " ,(case when Group_Category_No = '163' then 998 when Group_Category_No = '162' then 999 else GC_OrderBy end) as GC_OrderBy";
                    sqlstr += " FROM Group_Category";
                    sqlstr += " WHERE (Glb_id=@Glb_id OR Group_Category_No = '163' OR Group_Category_No = '162')";
                    sqlstr += " AND MultiCountry<>'0'";
                    sqlstr += " ORDER BY GC_OrderBy";
                }
                else
                {
                    sqlstr += " SELECT Group_Category_No,Group_Category_Name";
                    sqlstr += " FROM Group_Category";
                    sqlstr += " WHERE Glb_id=@Glb_id";
                    sqlstr += " AND MultiCountry<>'0'";
                    sqlstr += " ORDER BY GC_OrderBy";
                }

                SqlCommand comm = new SqlCommand(sqlstr, conn);
                comm.Parameters.Add(new SqlParameter("@Glb_id", AreaID));
                SqlDataReader reader = comm.ExecuteReader();

                if (reader.HasRows)
                {
                    LinkButton myLabel = new LinkButton();
                    LinkButton myLabel1 = new LinkButton();
                    CheckBox myCheckBox = new CheckBox();
                    CheckBox myCheckBox1 = new CheckBox();
                    myCheckBox.ID = "CheckBox1";
                    myCheckBox.Attributes.Add("style", "display:none");
                    myCheckBox.Checked = true;
                    PlaceHolder1.Controls.Add(myCheckBox);
                    myLabel.Text = "全選";
                    myLabel.ID = "Label1";
                    myLabel.Attributes.Add("CommandName", "0");
                    myLabel.Attributes.Add("class", "checked");
                    //myLabel.Attributes.Add("style","display:none");//全選CheckBox隱藏
                    PlaceHolder1.Controls.Add(myLabel);
                    myLabel.Click += new System.EventHandler(this.AllSelect);
                    while (reader.Read())
                    {
                        myCheckBox1 = new CheckBox();
                        myCheckBox1.ID = "CheckBox" + TripLoacationCount.ToString();
                        myCheckBox1.Attributes.Add("style", "display:none");
                        PlaceHolder1.Controls.Add(myCheckBox1);
                        myLabel1 = new LinkButton();
                        myLabel1.Text = reader["Group_Category_Name"].ToString();

                        myLabel1.ID = "Label" + TripLoacationCount.ToString();
                        myLabel1.CommandName = TripLoacationCount.ToString();
                        myLabel1.CommandArgument = reader["Group_Category_No"].ToString();
                        PlaceHolder1.Controls.Add(myLabel1);
                        myLabel1.Command += new CommandEventHandler(this.LinkButtonChecked);
                        TripLoacationCount++;

                        Page.Title += "," + reader["Group_Category_Name"].ToString();
                    }
                }
                reader.Close();
                comm.Dispose();
                conn.Close();
            }
        }
        catch
        {
        }
        TextBox1.Text = TripLoacationCount.ToString();  //記錄CheckBox數量
    }
    //預設全選
    private void DefaultAllSelect()
    {
        CheckBox CB = new CheckBox();
        CB = (CheckBox)PlaceHolder1.FindControl("CheckBox1");
        if (!String.IsNullOrEmpty(DropDownList1.SelectedValue))
        {
            if (CB.Checked)
            {
                CheckBox CB1 = new CheckBox();
                LinkButton LB1 = new LinkButton();
                for (int i = 1; i < Convert.ToInt32(TextBox1.Text); i++)
                {
                    LB1 = (LinkButton)PlaceHolder1.FindControl("Label" + i.ToString() + "");
                    LB1.Attributes.Add("class", "checked");
                    CB1 = (CheckBox)PlaceHolder1.FindControl("CheckBox" + i.ToString() + "");
                    CB1.Checked = true;
                }
            }
            TripLoacation();
        }
    }
    //全選or全不選
    private void AllSelect(object sender, EventArgs e)
    {
        CheckBox CB = new CheckBox();
        CB = (CheckBox)PlaceHolder1.FindControl("CheckBox1");
        if (!CB.Checked)
        {
            CheckBox CB1 = new CheckBox();
            LinkButton LB1 = new LinkButton();
            for (int i = 1; i < Convert.ToInt32(TextBox1.Text); i++)
            {
                LB1 = (LinkButton)PlaceHolder1.FindControl("Label" + i.ToString() + "");
                LB1.Attributes.Add("class", "checked");
                CB1 = (CheckBox)PlaceHolder1.FindControl("CheckBox" + i.ToString() + "");
                CB1.Checked = true;
            }
        }
        else
        {
            CheckBox CB2 = new CheckBox();
            LinkButton LB2 = new LinkButton();
            for (int j = 1; j < Convert.ToInt32(TextBox1.Text); j++)
            {
                LB2 = (LinkButton)PlaceHolder1.FindControl("Label" + j.ToString() + "");
                LB2.Attributes.Add("class", "");
                CB2 = (CheckBox)PlaceHolder1.FindControl("CheckBox" + j.ToString() + "");
                CB2.Checked = false;
            }
        }
        TripLoacation();
    }
    //某checkbox選or不選
    private void LinkButtonChecked(object sender, CommandEventArgs e)
    {
        CheckBox CB1 = new CheckBox();
        LinkButton LB1 = new LinkButton();
        LB1 = (LinkButton)sender;

        CB1 = (CheckBox)PlaceHolder1.FindControl("CheckBox" + e.CommandName.ToString() + "");
        if (CB1.Checked == false)
        {
            LB1.Attributes.Add("class", "checked");
            CB1.Checked = true;
        }
        else
        {
            LB1.Attributes.Add("class", "");
            CB1.Checked = false;
        }
        TripLoacation();
    }

    ArrayList LocationName;
    ArrayList GroupCategoryNo;

    private void TripLoacation()
    {
        LocationName = new ArrayList();
        GroupCategoryNo = new ArrayList();
        CheckBox CB1 = new CheckBox();
        LinkButton LB1 = new LinkButton();
        for (int i = 2; i < Convert.ToInt32(TextBox1.Text); i++)
        {
            LB1 = (LinkButton)PlaceHolder1.FindControl("Label" + i.ToString() + "");
            CB1 = (CheckBox)PlaceHolder1.FindControl("CheckBox" + i.ToString() + "");
            if (CB1.Checked == true)
            {
                LocationName.Add(LB1.Text);
                GroupCategoryNo.Add(LB1.CommandArgument);
            }
        }
        getExh();
    }


    //行程資料
    protected void getExh()
    {
        Literal1.Text = "";
        int picMax = 70;        //圖片簡介字數上限
        int introMax = 50;      //簡介字數上限
        string strGrop_Tour = "0";
        string TourType = "";
        switch (DropDownList0.SelectedValue)    //調整成b2b的系列
        {
            case "3": //巨匠
                TourType = "3";
                break;
            case "2": //新視界
                TourType = "2";
                break;
            case "1": //典藏
                TourType = "1";
                break;
            case "4": //珍藏
                TourType = "4";
                break;
        }
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection conn = new SqlConnection(strConnString);
        conn.Open();
        try
        {
            if (!String.IsNullOrEmpty(DropDownList1.SelectedValue))
            {
                if (LocationName.Count == GroupCategoryNo.Count)
                {
                    for (int i = 0; i < LocationName.Count; i++)
                    {
                        string strsql = "";
                        strsql = " SELECT Number,Super_Trip.Area_No,GC_NO,Trip_Name,URL,pic_Intro ";
                        strsql += " ,Class,Intro,Picture,Orderby,isnull(Price,'0') as Price ";
                        strsql += " ,isnull(Super_Trip.Trip_No,'') as Trip_No,Group_Name.Group_Name,isnull(dm_title,'') as dm_title ";
                        strsql += " FROM Super_Trip ";
                        strsql += " left join trip on trip.Trip_No = Super_Trip.Trip_No ";
                        strsql += " left join Group_Name on Group_Name.Group_Name_No = trip.Group_Name_No";
                        strsql += " where Super_Trip.Area_No = @Area_No";
                        strsql += " and GC_NO = @GC_NO";
                        strsql += " and Class = @Class";

                        strsql += " AND (";
                        strsql += " EXISTS (";
                        strsql += " SELECT 1 FROM Grop";
                        strsql += " LEFT JOIN Trip Trip_2 on Trip_2.Trip_No = Grop.Trip_No";
                        strsql += " WHERE Trip_2.Trip_No = Grop.Trip_No and (Trip_2.Trip_Classify = Super_Trip.Trip_No or Trip_2.Trip_No = Super_Trip.Trip_No)";
                        strsql += " and Grop.Grop_Depa >= @Grop_Depa";
                        strsql += " and isnull(Grop.hidden,'') <> 'y'";
                        strsql += " and Trip_2.Trip_Hide=0";
                        strsql += " and Grop.CANC_PEOL = ''";
                        //strsql += " and TourType = @TourType";
                        strsql += " ) or ISNULL(Dm_Title,'') <> ''";
                        strsql += " )";

                        strsql += " order by Orderby,number desc";
                        SqlCommand comm = new SqlCommand(strsql, conn);
                        switch (GroupCategoryNo[i].ToString())
                        {
                            case "162":
                                comm.Parameters.Add(new SqlParameter("@Area_No", 5));
                                break;
                            case "163":
                                comm.Parameters.Add(new SqlParameter("@Area_No", 12));
                                break;
                            default:
                                comm.Parameters.Add(new SqlParameter("@Area_No", DropDownList1.SelectedValue));
                                break;
                        }
                        comm.Parameters.Add(new SqlParameter("@Class", TourType));
                        //comm.Parameters.Add(new SqlParameter("@TourType", DropDownList0.SelectedItem.Text.Trim()));
                        comm.Parameters.Add(new SqlParameter("@GC_NO", GroupCategoryNo[i].ToString()));
                        comm.Parameters.Add(new SqlParameter("@Grop_Depa", DateTime.Today));
                        SqlDataReader reader = comm.ExecuteReader();
                        Literal1.Text += " <div class=\"Exhibition_filter_group\">";
                        Literal1.Text += " <h1>" + LocationName[i].ToString() + "<a href=\"http://b2b.artisan.com.tw/ClassifyProduct.aspx?area=" + DropDownList1.SelectedValue + "&sgcn=" + GroupCategoryNo[i].ToString() + "&tourtype=" + TourType + "\">MORE ></a></h1>";
                        while (reader.Read())
                        {
                            if (reader["Trip_Name"].ToString() == "")
                                continue;

                            //if (reader["Trip_No"].ToString() != "") //沒有TripNo就不顯示
                            {
                                if (reader["dm_title"].ToString() == "")
                                {
                                    strGrop_Tour = fn_GetGrop_Tour(reader["Trip_No"].ToString());
                                }
                                else
                                {
                                    strGrop_Tour = reader["Price"].ToString();
                                }

                                int price = Convert.ToInt32(reader["Price"].ToString());
                                Literal1.Text += " <div class=\"Exhibition_filter_list\">";

                                //Literal1.Text += " <h3><a href=\"http://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=" + reader["Trip_No"].ToString() + "\">" + word_over(reader["Group_Name"].ToString(), introMax) + "</a></h3>";
                                if (reader["dm_title"].ToString() != "")
                                {
                                    if (reader["Trip_No"].ToString() != "")
                                    {
                                        Literal1.Text += " <h3><a href=\"http://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=" + reader["Trip_No"].ToString() + "\">" + word_over(reader["dm_title"].ToString(), introMax) + "</a></h3>";
                                    }
                                    else
                                    {
                                        if (reader["URL"].ToString() != "")
                                        {
                                            Literal1.Text += " <h3><a href=\"" + reader["URL"].ToString().Replace("www.artisan.com.tw", "b2b.artisan.com.tw").Replace("https","http") + "\">" + word_over(reader["dm_title"].ToString(), introMax) + "</a></h3>";
                                        }
                                    }
                                }
                                else
                                {
                                    if (reader["Trip_No"].ToString() != "")
                                    {
                                        Literal1.Text += " <h3><a href=\"http://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=" + reader["Trip_No"].ToString() + "\">" + word_over(reader["Group_Name"].ToString(), introMax) + "</a></h3>";
                                    }
                                    else
                                    {
                                        if (reader["URL"].ToString() != "")
                                        {
                                            Literal1.Text += " <h3><a href=\"" + reader["URL"].ToString().Replace("www.artisan.com.tw", "b2b.artisan.com.tw").Replace("https","http") + "\">" + word_over(reader["Group_Name"].ToString(), introMax) + "</a></h3>";
                                        }
                                    }
                                }

                                if (!String.IsNullOrEmpty(reader["Intro"].ToString()))
                                {
                                    Literal1.Text += " <div class=\"remark\">" + word_over(reader["Intro"].ToString(), introMax) + "</div>";
                                }
                                if (reader["dm_title"].ToString() != "" && price >= 9000)
                                {
                                    Literal1.Text += " <div class=\"price\">NT <span>" + reader["Price"].ToString() + "</span> 起</div>";
                                }
                                else
                                {
                                    if (Convert.ToDouble(strGrop_Tour) <= 9000)
                                    {
                                        Literal1.Text += " <div class=\"price\"><span>來電洽詢</span></div>";
                                    }
                                    else
                                    {
                                        Literal1.Text += " <div class=\"price\">NT <span>" + strGrop_Tour + "</span> 起</div>";
                                    }
                                }
                                Literal1.Text += " </div>";
                            }
                        }
                        Literal1.Text += " </div>";
                        reader.Close();
                        comm.Dispose();
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
        conn.Close();
    }
    private string word_over(string word, int max)   //文字刪減
    {
        //if (word.Length > max) { return word.Substring(0, max) + "..."; }
        //else { return word; }

        return word;
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
        //{ strSql += " AND Grop.Trip_no in (@Trip_No) "; }
        { strSql += " AND Grop.Trip_no in (" + strTripNo + ")"; }
        strSql += " AND (isnull(hidden,'') <> 'y')";
        strSql += " AND (Grop.TourType = N'典藏' OR Trip.Trip_Hide=0)";
        strSql += " AND Grop_Depa >= @Grop_Depa";
        strSql += " AND IsNull(Grop.Grop_Tour,0) > 9000";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            //comm.Parameters.Add(new SqlParameter("@Trip_No", strTrip_No));
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