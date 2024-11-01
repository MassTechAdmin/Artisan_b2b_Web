using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebControl_Header_Menu_17 : System.Web.UI.UserControl
{
    string indexUrl = "../";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            get_menu_all();
            get_menu();
        }
        if (!string.IsNullOrEmpty(Convert.ToString(Session["PERNO"]) + ""))
        {
            login1.Visible = false;
            login3.Visible = true;
            login2.Visible = true;
        }
        else
        {
            login1.Visible = true;
            login3.Visible = false;
            login2.Visible = false;
        }
    }
    //總覽
    protected void get_menu_all()
    {
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection conn = new SqlConnection(strConnString);
        conn.Open();

        string strsql = "";
        int intIndex = 0;
        strsql = " SELECT Area_No,Area_Name,Array,Area_ID,Area_ENG_Name FROM Area WHERE Array != '0' and Area_ID <> 'Area999' ORDER BY Area_no ";

        SqlCommand comm = new SqlCommand(strsql, conn);
        SqlDataReader reader = comm.ExecuteReader();

        //main_all.Text += "|<li class='nav_color'><a href='" + indexUrl + "ClassifyProduct.aspx'>團體總表</a></li>";
        main_all.Text += "<li class='nav_color'>總覽";
        main_all.Text += "<ul style='display: none;'><li class='navTool'><div class='main_navTool'>";
        while (reader.Read())
        {
            intIndex++;
            main_all.Text += "<div class='main_navTool_area'>" + reader["Area_Name"].ToString();
            get_menu2(reader["Area_ID"].ToString(), "");
            main_all.Text += "</div>";
        }
       // main_all.Text += "<div class='cancel_button'><span></span><span></span></div>";
        main_all.Text += "</div></li></ul></li>|";

        comm.Dispose();
        reader.Close();

        conn.Close();
        conn = null;
    }

    //選單第一層
    protected void get_menu()
    {
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection conn = new SqlConnection(strConnString);
        conn.Open();

        string strsql = "";
        int intIndex = 0;
        strsql = " SELECT Area_No,Area_Name,Array,Area_ID,Area_ENG_Name FROM Area WHERE Array != '0' and Area_ID <> 'Area999' ORDER BY Area_no ";

        SqlCommand comm = new SqlCommand(strsql, conn);
        SqlDataReader reader = comm.ExecuteReader();

        while (reader.Read())
        {
            intIndex++;

            main_all.Text += "<li>";
            main_all.Text += reader["Area_Name"].ToString().Trim();

            main_all.Text += "<ul style='display: none;'><li class='navTool'><div class='main_navTool2' id='main" + intIndex + "'>";
            main_all.Text += "<div class='main_navTool_area2'>";
            get_menu2(reader["Area_ID"].ToString(), "");
            // 埃及跟希臘在網頁上也要在古文明內一併顯示(BOSS 說的) by roger 20180829
            if (reader["Area_ID"].ToString() == "Area6")
            {
                get_menu2("Area9", "163");
                get_menu2("Area3", "162");
            }
            main_all.Text += "</div>";

            main_all.Text += "<div class='navTool_line'><img src='/images/navTool_line.png' alt=''></div>";
            main_all.Text += "<div class='recommendation_tool'><h3>OUR RECOMMENDATION</h3>";
            get_ourRec(reader["Area_ID"].ToString());

            main_all.Text += "</div></li></ul>";
            main_all.Text += "</li>|";
        }

        main_all.Text += "<li><a href='";
        if (!string.IsNullOrEmpty(Convert.ToString(Session["PERNO"]) + "")) 
        { main_all.Text += "/FitTrip/FitTrip.aspx"; }
        else 
        { main_all.Text += "/FitTrip/FitTrip.aspx"; }
        main_all.Text += "'>機 + 酒</a></li>";

        comm.Dispose();
        reader.Close();

        conn.Close();
        conn = null;
    }
    //選單第二層
    protected void get_menu2(string strarea, string strareano)
    {
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection conn = new SqlConnection(strConnString);
        conn.Open();

        string strsql = "";
        strsql = " SELECT  Group_Category_No,Group_Category_Name,GCEng_Name,Glb_id,GC_OrderBy,area.area_no as area FROM Group_Category ";
        strsql += " left join Area on Area.Area_ID = Group_Category.Glb_id ";
        strsql += " WHERE Glb_id = @Glb_id ";
        strsql += " AND MultiCountry <> 0";
        // 針對額外條件所設定的次選單
        if (!string.IsNullOrEmpty(strareano))
        {
            strsql += " AND Group_Category_No = @Group_Category_No";
        }
        strsql += " ORDER BY cast(GC_OrderBy as int)";

        SqlCommand comm = new SqlCommand(strsql, conn);
        comm.Parameters.Add(new SqlParameter("@Glb_id", strarea));
        // 針對額外條件所設定的次選單
        if (!string.IsNullOrEmpty(strareano))
        {
            comm.Parameters.Add(new SqlParameter("@Group_Category_No", strareano));
        }
        SqlDataReader reader = comm.ExecuteReader();

        while (reader.Read())
        {
            main_all.Text += " <a href='";
            if (!string.IsNullOrEmpty(Convert.ToString(Session["PERNO"]) + ""))
            {
                main_all.Text += indexUrl + "ClassifyProduct.aspx?area_no=" + reader["area"].ToString() + "&sgcn=" + reader["Group_Category_No"].ToString();
            }
            else
            {
                main_all.Text += "#";
            }
            main_all.Text += "'>" + reader["Group_Category_Name"].ToString() + "</a>";
        }

        comm.Dispose();
        reader.Close();

        conn.Close();
    }
    //特別推薦
    protected void get_ourRec(string strareano)
    {
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection conn = new SqlConnection(strConnString);
        conn.Open();

        string strsql = "";
        strsql += " SELECT Top 2 *";
        strsql += " FROM New_Web_Menu";
        strsql += " WHERE area_no = @area_no ";
        strsql += " ORDER BY number desc";

        SqlCommand comm = new SqlCommand(strsql, conn);
        comm.Parameters.Add(new SqlParameter("@area_no", strareano));
        SqlDataReader reader = comm.ExecuteReader();

        while (reader.Read())
        {
            main_all.Text += "<figure><a href='";
            if (!string.IsNullOrEmpty(Convert.ToString(Session["PERNO"]) + ""))
            {
                main_all.Text += reader["Rec_URL"].ToString().Replace("https://www.artisan.com.tw/", "http://b2b.artisan.com.tw/");
            }
            else
            {
                main_all.Text += "#";
            }
            main_all.Text += "'><img src='https://www.artisan.com.tw/Zupload16/new_web/" + reader["Rec_Pic"].ToString() + "'";
            main_all.Text += " alt='' width='220' height='180' border='0'></a>";
            main_all.Text += "<figcaption>" + reader["Rec_Title"].ToString() + "</figcaption></figure>";
        }

        comm.Dispose();
        reader.Close();

        conn.Close();
    }
}