using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebControl_Header_Menu_19 : System.Web.UI.UserControl
{
    string indexUrl = "../";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["PERNO"]) + ""))
        {
            login1.HRef = "http://b2b.artisan.com.tw/Rraveler.aspx";
            login2.HRef = "http://b2b.artisan.com.tw/order_list.aspx";
            login3.Visible = true;
            menubtn.Visible = true;
        }
        else
        {
            login1.HRef = "";
            login2.HRef = "";
            login3.Visible = false;
            menubtn.Visible = false;
        }
            get_menu_all();
    }
    protected void get_menu_all()
    {
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection conn = new SqlConnection(strConnString);
        conn.Open();

        string strsql = "";
        //int intIndex = 0;
        strsql = " SELECT Area_No,Area_Name,Array,Area_ID,Area_ENG_Name FROM Area WHERE Array != '0' and Area_ID <> 'Area999' ORDER BY Area_no ";

        SqlCommand comm = new SqlCommand(strsql, conn);
        SqlDataReader reader = comm.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                if (reader["Area_No"].ToString() == "31")
                {
                    litAreaTW.Text += " <li class=\"pushy-submenu\">";
                    litAreaTW.Text += "<button onclick=\"return false\">" + reader["Area_Name"].ToString() + "</button>";
                    get_menu2_all(reader["Area_ID"].ToString(), reader["Area_Name"].ToString(), reader["Area_No"].ToString(), litAreaTW);
                    litAreaTW.Text += "</li>";
                }
                else
                {
                    //intIndex++;
                    main_all.Text += " <li class=\"pushy-submenu\">";
                    main_all.Text += "<button onclick=\"return false\">" + reader["Area_Name"].ToString() + "</button>";
                    get_menu2_all(reader["Area_ID"].ToString(), reader["Area_Name"].ToString(), reader["Area_No"].ToString(), main_all);
                    main_all.Text += "</li>";
                }
            }
        }
        comm.Dispose();
        reader.Close();

        conn.Close();
        conn = null;
    }
    protected void get_menu2_all(String AreaID, String AreaName, String AreaNo, Literal litTemp)
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
            litTemp.Text += " <ul>";
            litTemp.Text += "<li class=\"pushy-link\"><a href=\"/exh/Exhibition.aspx?area=" + AreaNo + "\" class=\"exh-title\">" + AreaName + "全覽</a></li>";
            while (reader.Read())
            {
                litTemp.Text += "<li class=\"pushy-link\"><a href=\"/ClassifyProduct.aspx?l=l&area=" + AreaNo + "&sgcn=" + reader["Group_Category_No"].ToString() + "\">" + reader["Group_Category_Name"].ToString() + "</a></li>";
            }
            litTemp.Text += "</ul>";
        }
        comm.Dispose();
        reader.Close();

        conn.Close();
        conn = null;
    }
}