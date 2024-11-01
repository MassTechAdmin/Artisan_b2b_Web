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

public partial class WebControl_Header : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

        fn_Show_Area_List("Area1");
        fn_Show_Area_List("Area8");
        fn_Show_Area_List("Area6");
        fn_Show_Area_List("Area7");
        fn_Show_Area_List("Area9");

        fn_Show_Area_List("Area10");
        fn_Show_Area_List("Area11");
        fn_Show_Area_List("Area14");
        fn_Show_Area_List("Area18");
        fn_Show_Area_List("Area16");
        DisplayRemainPoint();
        fn_Show_Area_List("Area9999"); // 無此地區編號(自由行)

        string strThisPage = System.IO.Path.GetFileName(Request.PhysicalPath).ToLower();
        //if (strThisPage == "exhibition.aspx") { strPage = ""; }
        //if (strThisPage == "exhmini.aspx") { strPage = ""; }
        string strData = "";
        //strData += "<a href=\"" + strPage + "Exhibition.aspx?area_no=10\" target=\"_top\" id=\"n3\" class=\"menu_link\">中 東</a>";
        //strData += "<a href=\"" + strPage + "Exhibition.aspx?area_no=8\" target=\"_top\" id=\"n4\" class=\"menu_link\">紐 澳</a>";
        //strData += "<a href=\"" + strPage + "Exhibition.aspx?area_no=12\" target=\"_top\" id=\"n5\" class=\"menu_link\">非 洲</a>";
        //strData += "<a href=\"" + strPage + "Exhibition.aspx?area_no=11\" target=\"_top\" id=\"n6\" class=\"menu_link\">美 洲</a>";
        //strData += "<a href=\"" + strPage + "Exhibition.aspx?area_no=7\" target=\"_top\" id=\"n7\" class=\"menu_link\">遊 輪</a>";
        //strData += "<a href=\"" + strPage + "Exhibition.aspx?area_no=27\" target=\"_top\" id=\"n8\" class=\"menu_link\">海 島</a>";
        if (!string.IsNullOrEmpty(Convert.ToString(Session["PERNO"])) || !string.IsNullOrEmpty(Convert.ToString(Session["Compno"])) || !string.IsNullOrEmpty(Convert.ToString(Session["PerName"])) || !string.IsNullOrEmpty(Convert.ToString(Session["PerIDNo"])))
        {
            strData += "<a href=\"#\" target=\"_top\" class=\"actived menu_link\" id=\"n1\">歐 洲</a>";
            strData += "<a href=\"http://b2b.artisan.com.tw/ClassifyProduct.aspx?area=11\" target=\"_top\" id=\"n6\" class=\"menu_link\">美 洲</a>";
            strData += "<a href=\"http://b2b.artisan.com.tw/ClassifyProduct.aspx?area=10\" target=\"_top\" id=\"n3\" class=\"menu_link\">中 東</a>";
            strData += "<a href=\"http://b2b.artisan.com.tw/ClassifyProduct.aspx?area=8\" target=\"_top\" id=\"n4\" class=\"menu_link\">紐 澳</a>";
            strData += "<a href=\"http://b2b.artisan.com.tw/ClassifyProduct.aspx?area=12\" target=\"_top\" id=\"n5\" class=\"menu_link\">非 洲</a>";
            //strData += "<a href=\"#\" target=\"_top\" id=\"n2\" class=\"menu_link\">亞 洲</a>";
            strData += "<a href=\"http://b2b.artisan.com.tw/ClassifyProduct.aspx?area=9\" target=\"_top\" id=\"n2\" class=\"menu_link\">南 亞</a>";
            strData += "<a href=\"http://b2b.artisan.com.tw/ClassifyProduct.aspx?area=37\" target=\"_top\" id=\"n9\" class=\"menu_link\">中 國</a>";
            strData += "<a href=\"http://b2b.artisan.com.tw/ClassifyProduct.aspx?area=35\" target=\"_top\" id=\"n10\" class=\"menu_link\">日 本</a>";
            strData += "<a href=\"http://b2b.artisan.com.tw/ClassifyProduct.aspx?area=7\" target=\"_top\" id=\"n7\" class=\"menu_link\">遊 輪</a>";
            strData += "<a href=\"http://b2b.artisan.com.tw/ClassifyProduct.aspx?area=27\" target=\"_top\" id=\"n8\" class=\"menu_link\">海 島</a>";
            //strData += "<a href=\"/island/marldives.aspx\" target=\"_top\" id=\"n8\" class=\"menu_link\">海 島</a>";
            //strData += "<a href=\"/island/marldives.aspx\" target=\"_top\" id=\"n8\" class=\"menu_link\">海 島</a>";
        }
        else
        {
            strData += "<a href=\"#\" target=\"_top\" class=\"actived menu_link\" id=\"n1\">歐 洲</a>";
            strData += "<a href=\"#\" target=\"_top\" id=\"n6\" class=\"menu_link\">美 洲</a>";
            strData += "<a href=\"#\" target=\"_top\" id=\"n3\" class=\"menu_link\">中 東</a>";
            strData += "<a href=\"#\" target=\"_top\" id=\"n4\" class=\"menu_link\">紐 澳</a>";
            strData += "<a href=\"#\" target=\"_top\" id=\"n5\" class=\"menu_link\">非 洲</a>";
            strData += "<a href=\"#\" target=\"_top\" id=\"n2\" class=\"menu_link\">南 亞</a>";
            strData += "<a href=\"#\" target=\"_top\" id=\"n9\" class=\"menu_link\">中 國</a>";
            strData += "<a href=\"#\" target=\"_top\" id=\"n10\" class=\"menu_link\">日 本</a>";
            strData += "<a href=\"#\" target=\"_top\" id=\"n7\" class=\"menu_link\">遊 輪</a>";
            strData += "<a href=\"#\" target=\"_top\" id=\"n8\" class=\"menu_link\">海 島</a>";
        }
        litAreaCountry.Text = strData;

        HyperLink6.NavigateUrl = "http://www.artisan.com.tw/fit/fit_index.aspx";
    }

    int mintItem = 0;
    protected void fn_Show_Area_List(string strGlb_ID)
    {
        if (string.IsNullOrEmpty(strGlb_ID)) { strGlb_ID = "Area1"; }
        //string strThisPage = System.IO.Path.GetFileName(Request.PhysicalPath).ToLower();
        //if (strThisPage == "exhibition.aspx") { strPage = ""; }
        //if (strThisPage == "exhmini.aspx") { strPage = ""; }

        string strData = "";
        switch (strGlb_ID)
        {
            case "Area1":
                mintItem++;
                if (!string.IsNullOrEmpty(Convert.ToString(Session["PERNO"])) || !string.IsNullOrEmpty(Convert.ToString(Session["Compno"])) || !string.IsNullOrEmpty(Convert.ToString(Session["PerName"])) || !string.IsNullOrEmpty(Convert.ToString(Session["PerIDNo"])))
                {
                    strData += "<div id=\"subNav" + mintItem + "\" class=\"subNav" + mintItem + "\" style=\"display: none;\">";
                    strData += (string.IsNullOrEmpty(strData) ? "" : " ｜ ") + "<a href=\"http://b2b.artisan.com.tw/ClassifyProduct.aspx?area=1\"><span class=\"style_mainmenu\">西歐</span></a>";
                    strData += (string.IsNullOrEmpty(strData) ? "" : " ｜ ") + "<a href=\"http://b2b.artisan.com.tw/ClassifyProduct.aspx?area=2\"><span class=\"style_mainmenu\">東歐</span></a>";
                    strData += (string.IsNullOrEmpty(strData) ? "" : " ｜ ") + "<a href=\"http://b2b.artisan.com.tw/ClassifyProduct.aspx?area=5\"><span class=\"style_mainmenu\">南歐</span></a>";
                    strData += (string.IsNullOrEmpty(strData) ? "" : " ｜ ") + "<a href=\"http://b2b.artisan.com.tw/ClassifyProduct.aspx?area=6\"><span class=\"style_mainmenu\">北歐</span></a>";
                    strData += "</div>";
                }
                else
                {
                    strData += "<div id=\"subNav" + mintItem + "\" class=\"subNav" + mintItem + "\" style=\"display: none;\">";
                    strData += (string.IsNullOrEmpty(strData) ? "" : " ｜ ") + "<span class=\"style_mainmenu\">西歐</span>";
                    strData += (string.IsNullOrEmpty(strData) ? "" : " ｜ ") + "<span class=\"style_mainmenu\">東歐</span>";
                    strData += (string.IsNullOrEmpty(strData) ? "" : " ｜ ") + "<span class=\"style_mainmenu\">南歐</span>";
                    strData += (string.IsNullOrEmpty(strData) ? "" : " ｜ ") + "<span class=\"style_mainmenu\">北歐</span>";
                    strData += "</div>";
                }
                break;
            case "Area14999":
                mintItem++;
                strData += "<div id=\"subNav" + mintItem + "\" class=\"subNav" + mintItem + "\" style=\"display: none;\">";
                strData += "<span class=\"destination_city\">";
                strData += "<a href=\"/island/marldives.aspx\">馬爾地夫</span></a>   ";
                strData += "<a href=\"/ClassifyProduct.aspx?area=27&sgcn=142\">帛硫</span>";
                strData += "</a>";
                strData += "</div>";
                break;
            //case "Area5":
            //    mintItem++;
            //    if (!string.IsNullOrEmpty(Convert.ToString(Session["PERNO"])) || !string.IsNullOrEmpty(Convert.ToString(Session["Compno"])) || !string.IsNullOrEmpty(Convert.ToString(Session["PerName"])) || !string.IsNullOrEmpty(Convert.ToString(Session["PerIDNo"])))
            //    {
            //        strData += "<div id=\"subNav" + mintItem + "\" class=\"subNav" + mintItem + "\" style=\"display: none;\">";
            //        //strData += (string.IsNullOrEmpty(strData) ? "" : " ｜ ") + "<a href=\"Exhibition.aspx?area_no=13\"><span class=\"style_mainmenu\">東北亞</span></a>";
            //        strData += (string.IsNullOrEmpty(strData) ? "" : " ｜ ") + "<a href=\"http://b2b.artisan.com.tw/ClassifyProduct.aspx?area=35\"><span class=\"style_mainmenu\">日本</span></a>";
            //        strData += (string.IsNullOrEmpty(strData) ? "" : " ｜ ") + "<a href=\"http://b2b.artisan.com.tw/ClassifyProduct.aspx?area=36\"><span class=\"style_mainmenu\">韓國</span></a>";
            //        strData += (string.IsNullOrEmpty(strData) ? "" : " ｜ ") + "<a href=\"http://b2b.artisan.com.tw/ClassifyProduct.aspx?area=9\"><span class=\"style_mainmenu\">南亞</span></a>";
            //        strData += (string.IsNullOrEmpty(strData) ? "" : " ｜ ") + "<a href=\"http://b2b.artisan.com.tw/ClassifyProduct.aspx?area=31\"><span class=\"style_mainmenu\">台港中</span></a>";
            //        strData += (string.IsNullOrEmpty(strData) ? "" : " ｜ ") + "<a href=\"http://b2b.artisan.com.tw/ClassifyProduct.aspx?area=32\"><span class=\"style_mainmenu\">東南亞</span></a>";
            //        strData += "</div>";
            //    }
            //    else
            //    {
            //        strData += "<div id=\"subNav" + mintItem + "\" class=\"subNav" + mintItem + "\" style=\"display: none;\">";
            //        //strData += (string.IsNullOrEmpty(strData) ? "" : " ｜ ") + "<a href=\"/" + strPage + "Exhibition.aspx?area_no=13\"><span class=\"style_mainmenu\">東北亞</span></a>";
            //        strData += (string.IsNullOrEmpty(strData) ? "" : " ｜ ") + "<span class=\"style_mainmenu\">日本</span>";
            //        strData += (string.IsNullOrEmpty(strData) ? "" : " ｜ ") + "<span class=\"style_mainmenu\">韓國</span>";
            //        strData += (string.IsNullOrEmpty(strData) ? "" : " ｜ ") + "<span class=\"style_mainmenu\">南亞</span>";
            //        strData += (string.IsNullOrEmpty(strData) ? "" : " ｜ ") + "<span class=\"style_mainmenu\">台港中</span>";
            //        strData += (string.IsNullOrEmpty(strData) ? "" : " ｜ ") + "<span class=\"style_mainmenu\">東南亞</span>";
            //        strData += "</div>";
            //    }
            //    break;
            default:
                string strSql = "";
                strSql += " SELECT Group_Category_No,Group_Category_Name,Glb_Id,Area_No FROM Group_Category";
                strSql += " LEFT JOIN Area ON Area.Area_Id = Group_Category.Glb_Id";
                strSql += " WHERE Glb_ID = @Glb_ID";
                strSql += " AND MultiCountry <> 0";
                strSql += " ORDER BY Area_No,GC_OrderBy";
                string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
                SqlConnection connect = new SqlConnection(strConnString);
                try
                {
                    string strTemp = "";
                    connect.Open();
                    SqlCommand comm = new SqlCommand(strSql, connect);
                    comm.Parameters.Add(new SqlParameter("@Glb_ID", strGlb_ID));
                    SqlDataReader reader = comm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(Session["PERNO"])) || !string.IsNullOrEmpty(Convert.ToString(Session["Compno"])) || !string.IsNullOrEmpty(Convert.ToString(Session["PerName"])) || !string.IsNullOrEmpty(Convert.ToString(Session["PerIDNo"])))
                            {
                                switch (reader["Group_Category_No"].ToString())
                                {
                                    case "143":
                                        strTemp += (string.IsNullOrEmpty(strTemp) ? "" : "　") + "<a href=\"http://b2b.artisan.com.tw/island/marldives.aspx\">" + reader["Group_Category_Name"].ToString() + "</a>";
                                        break;
                                    default:
                                        strTemp += (string.IsNullOrEmpty(strTemp) ? "" : "　") + "<a href=\"http://b2b.artisan.com.tw/ClassifyProduct.aspx?area=" + reader["Area_No"].ToString() + "&sgcn=" + reader["Group_Category_No"].ToString() + "\">" + reader["Group_Category_Name"].ToString() + "</a>";
                                        break;
                                }
                            }
                            else
                            {
                                 strTemp += (string.IsNullOrEmpty(strTemp) ? "" : "　") + reader["Group_Category_Name"].ToString() +"";
                            }
                        }

                        mintItem++;
                        strData += "<div id=\"subNav" + mintItem + "\" class=\"subNav" + mintItem + "\" style=\"display: none;\">";
                        strData += "<span class=\"destination_city\">";
                        strData += strTemp;
                        strData += "</span>";
                        strData += "</div>";
                    }
                    reader.Close();
                    comm.Dispose();
                }
                finally
                {
                    connect.Close();
                }
                break;
        }

        if (!string.IsNullOrEmpty(strData))
            listAreaSub.Text += strData;
    }
    protected void LinkLoginOut_Click(object sender, EventArgs e)
    {
        Session["PERNO"] = null; //帳號
        Session["Compno"] = null; //公司編號
        Session["PerName"] = null; //員工姓名
        Session["PerIDNo"] = null; //員工身份證字號

        Response.Redirect("~/Default.aspx");
    }

    protected void DisplayRemainPoint()
    {
        try
        {
            Literal1.Text = "";

            string strsql3 = "";
            string connstr3 = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ConnectionString;
            SqlConnection conn3 = new SqlConnection(connstr3);
            conn3.Open();
            strsql3 += " SELECT TOP 1 point_balance FROM Point_Recode";
            //strsql3 += " WHERE COMP_NO=@CompNo AND Cust_Idno=@CustIdno";
            strsql3 += " WHERE Cust_Idno=@CustIdno";
            strsql3 += " ORDER BY crea_date DESC";
            SqlCommand comm3 = new SqlCommand(strsql3, conn3);
            //comm3.Parameters.Add(new SqlParameter("@CompNo", CompNo));
            comm3.Parameters.Add(new SqlParameter("@CustIdno", Convert.ToString(Session["PerIDNo"])));
            SqlDataReader reader3 = comm3.ExecuteReader();
            if (reader3.HasRows)
            {
                while (reader3.Read())
                {
                    Literal1.Text = "紅利積點剩餘：" + Convert.ToString(reader3["point_balance"]) + "點 ｜";
                }
            }
            else
            {
                Literal1.Text = "";
            }
            reader3.Close();
            comm3.Dispose();
            conn3.Close();

        }
        catch (Exception ex)
        { }
    }
}
