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

public partial class WebControl_Country_List : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

        fn_Show_SubMenu();
    }

    // 次選單
    protected void fn_Show_SubMenu()
    {
        string strSql = "";
        strSql += " SELECT [Group_Category_No],[Group_Category_Name],[GCEng_Name],[Glb_id],[MultiCountry]";
        strSql += " FROM [Group_Category]";
        strSql += " LEFT JOIN [Area] ON [Area].[Area_ID] = [Group_Category].[Glb_id]";
        strSql += " WHERE Area_No = @Area_No";
        strSql += " AND MultiCountry <> 0";
        //strSql += " ORDER BY MultiCountry";
        strSql += " ORDER BY Group_Category.GC_OrderBy,MultiCountry";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            string strData = "";
            string strMultiCountry2 = ""; //雙國
            string strMultiCountry3 = ""; //多國
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@Area_No", Request["area_no"]));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                int intRowIndex = 0; //計算到第幾筆資料，若大於7時都歸到相關行程
                while (reader.Read())
                {
                    intRowIndex++;
                    if (intRowIndex > 6)
                    {
                        strMultiCountry2 += "<li><a href=\"exhMini.aspx?area_no=" + Request["area_no"] + "&no=" + reader["Group_Category_No"].ToString() + "\">" + reader["Group_Category_Name"].ToString() + "</a></li>";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(strData)) { strData += "<div class=\"exh_menugrip01\"><img src=\"../images/exh/exh_menugrip01.jpg\"/></div>"; }
                        strData += "<div class=\"exh_menu_list\">";
                        strData += "<a href=\"exhMini.aspx?area_no=" + Request["area_no"] + "&no=" + reader["Group_Category_No"].ToString() + "\">";
                        strData += "<span class=\"style_exh_menu01\">" + reader["Group_Category_Name"].ToString() + "</span>";
                        strData += "<span class=\"style_exh_menu02\">旅遊專區</span><br />";
                        if (!string.IsNullOrEmpty(reader["GCEng_Name"].ToString()))
                        { strData += "<span class=\"style_exh_menu03\">" + reader["GCEng_Name"].ToString() + "</span>"; }
                        strData += "</a>";
                        strData += "</div>";
                    }
                    //switch (reader["MultiCountry"].ToString())
                    //{
                    //    case "1":
                    //        if (!string.IsNullOrEmpty(strData)) { strData += "<div class=\"exh_menugrip01\"><img src=\"../images/exh/exh_menugrip01.jpg\"/></div>"; }
                    //        strData += "<div class=\"exh_menu_list\">";
                    //        strData += "<a href=\"exhMini.aspx?area_no=" + Request["area_no"] + "&no=" + reader["Group_Category_No"].ToString() + "\">";
                    //        strData += "<span class=\"style_exh_menu01\">" + reader["Group_Category_Name"].ToString() + "</span>";
                    //        strData += "<span class=\"style_exh_menu02\">旅遊專區</span><br />";
                    //        if (!string.IsNullOrEmpty(reader["GCEng_Name"].ToString()))
                    //        { strData += "<span class=\"style_exh_menu03\">" + reader["GCEng_Name"].ToString() + " Explored</span>"; }
                    //        strData += "</a>";
                    //        strData += "</div>";
                    //        break;
                    //    case "2":
                    //        strMultiCountry2 += "<li><a href=\"exhMini.aspx?area_no=" + Request["area_no"] + "&no=" + reader["Group_Category_No"].ToString() + "\">" + reader["Group_Category_Name"].ToString() + "</a></li>";
                    //        break;
                    //    default:
                    //        strMultiCountry3 += "<li><a href=\"exhMini.aspx?area_no=" + Request["area_no"] + "&no=" + reader["Group_Category_No"].ToString() + "\">" + reader["Group_Category_Name"].ToString() + "</a></li>";
                    //        break;
                    //}
                }

                // 雙國
                if (!string.IsNullOrEmpty(strMultiCountry2))
                {
                    strData += "<div class=\"exh_menugrip01\"><img src=\"../images/exh/exh_menugrip01.jpg\"/></div>";
                    strData += "<div class=\"exh_menu_list\" style=\"padding-top:20px;height:31px; background-image:url(../images/exh/exh_menugrip02.jpg); background-repeat:no-repeat; background-position:right top;\">";
                    strData += "<div class=\"dropdown\">";
                    strData += "<a class=\"account\" ><span><span class=\"style_exh_menu01\">多國</span><span class=\"style_exh_menu02\">旅遊專區</span></span></a>";
                    strData += "<div class=\"submenu\" style=\"display: none; \">";
                    strData += "<ul class=\"root\">";
                    strData += strMultiCountry2;
                    strData += "</ul>";
                    strData += "</div>";
                    strData += "</div>";
                    strData += "</div>";
                }
                // 多國
                //if (!string.IsNullOrEmpty(strMultiCountry3))
                //{
                //    strData += "<div class=\"exh_menugrip01\"><img src=\"../images/exh/exh_menugrip01.jpg\"/></div>";
                //    strData += "<div class=\"exh_menu_list\" style=\"padding-top:20px;height:31px; background-image:url(../images/exh/exh_menugrip02.jpg); background-repeat:no-repeat; background-position:right top;\">";
                //    strData += "<div class=\"dropdown\">";
                //    strData += "<a class=\"account2\" ><span><span class=\"style_exh_menu01\">多國</span><span class=\"style_exh_menu02\">旅遊專區</span></span></a>";
                //    strData += "<div class=\"submenu2\" style=\"display: none; \">";
                //    strData += "<ul class=\"root\">";
                //    strData += strMultiCountry3;
                //    strData += "</ul>";
                //    strData += "</div>";
                //    strData += "</div>";
                //    strData += "</div>";
                //}

                litSubMenu.Text = strData;
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
