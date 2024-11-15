﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class OnSale : System.Web.UI.Page
{
   protected void Page_Load(object sender, EventArgs e)
    {
        clsFunction.Check.CheckSession();
        Show_Area();
        Show_Trip();
    }

    #region "===地區==="
    protected void Show_Area()
    {
        string strsql = "";

        strsql += " SELECT distinct Area_Name,Area.Area_No FROM Trip";
        strsql += " LEFT JOIN Grop on Grop.Trip_No = Trip.Trip_No";
        strsql += " LEFT JOIN Area on Grop.AREA_CODE = Area.Area_ID ";
        strsql += " WHERE TourType2 = N'特惠團'";
        strsql += " and isnull(hidden,'') <> 'y'";
        strsql += " and Trip.Trip_Hide=0";
        strsql += " AND Grop.CANC_PEOL = ''";
        strsql += " ORDER BY Area.Area_No";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["tripConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand cmd = new SqlCommand(strsql, connect);

        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Area.Text += "<ul id='menu'>";
            Area.Text += "<li class='bttn-pill bttn-md'>" + reader["Area_Name"].ToString() + "</li>";
            while (reader.Read())
            {
                Area.Text += "<li class='bttn-pill bttn-md'>" + reader["Area_Name"].ToString() + "</li>";
            }
            Area.Text += "</ul>";
        }
        reader.Close();
        cmd.Dispose();
        connect.Close();
    }

    #endregion

    #region "===行程、價錢==="

    protected void Show_Trip()
    {
        string strsql = "";

        strsql += " SELECT distinct Area_Name,Area.Area_No,Grop.AREA_CODE FROM Trip  ";
        strsql += " LEFT JOIN Grop on Grop.Trip_No = Trip.Trip_No ";
        strsql += " LEFT JOIN Area on Grop.AREA_CODE = Area.Area_ID ";
        strsql += " WHERE TourType2 = N'特惠團' ";
        strsql += " and isnull(hidden,'') <> 'y'";
        strsql += " and Trip.Trip_Hide=0";
        strsql += " AND Grop.CANC_PEOL = ''";
        strsql += " ORDER BY Area.Area_No ";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["tripConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlDataAdapter da = new SqlDataAdapter(strsql, strConnString);
        DataTable dt = new DataTable();
        da.Fill(dt);
        connect.Close();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Literal1.Text += "<section>";
            Literal1.Text += "<h1>" + dt.Rows[i]["Area_Name"].ToString() + "</h1>";          
            Show_GC(dt.Rows[i]["AREA_CODE"].ToString());          
            Literal1.Text += "</section>";
        }
    }

    protected void Show_GC(string AREA_CODE)
    {
        string strsql = "";

        strsql += " SELECT distinct Trip.Trip_No,Grop_Name,Area_Name,Area.Area_No FROM Trip ";
        strsql += " LEFT JOIN Grop on Grop.Trip_No = Trip.Trip_No ";
        strsql += " LEFT JOIN Area on Grop.AREA_CODE = Area.Area_ID ";
        strsql += " WHERE TourType2 = N'特惠團' ";
        strsql += " and Grop_Depa >= getdate()";
        strsql += " and Grop.AREA_CODE = @AREA_CODE";
        strsql += " and isnull(hidden,'') <> 'y'";
        strsql += " and Trip.Trip_Hide=0";
        strsql += " AND Grop.CANC_PEOL = ''";
        strsql += " ORDER BY Area.Area_No ";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["tripConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand cmd = new SqlCommand(strsql, connect);
        cmd.Parameters.Add(new SqlParameter("@AREA_CODE", AREA_CODE));
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Literal1.Text += "<div class='list'>";
            Literal1.Text += "<h3><a href=\"https://www.artisan.com.tw/ClassifyProduct.aspx?TripNo=" + reader["Trip_No"].ToString() + "\">" + reader["Grop_Name"] + "</a></h3>";
            Literal1.Text += "<div class='date'>";
            Show_Date(reader["Trip_No"].ToString());
            Literal1.Text += "</div>";
            Show_Price(reader["Trip_No"].ToString());
            Literal1.Text += "</div>";
        }
        reader.Close();
        cmd.Dispose();
        connect.Close();
    }


    protected void Show_Price(string trip_no)
    {
        string strsql = "";

        strsql += " SELECT TOP 1 Agent_Tour FROM Grop ";
        strsql += " WHERE TourType2 = N'特惠團' ";
        strsql += " AND Grop_Depa >= getdate() ";
        strsql += " AND Trip_No = @Trip_No ";
        strsql += " and isnull(hidden,'') <> 'y'";
        strsql += " AND Grop.CANC_PEOL = ''";
        strsql += " ORDER BY Grop_Tour ";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["tripConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand cmd = new SqlCommand(strsql, connect);

        cmd.Parameters.Add(new SqlParameter("@Trip_No", trip_no));
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Literal1.Text += "<div class='price'>" + reader["Agent_Tour"].ToString() + "</div>";
        }
        reader.Close();
        cmd.Dispose();
        connect.Close();
    }
    protected void Show_Date(string trip_no)
    {
        int count = 0;
        string mm = "";
        string strsql = "";
        strsql += " SELECT distinct Grop_Depa FROM Grop ";
        strsql += " WHERE TourType2 = N'特惠團' ";
        strsql += " AND Grop_Depa >= getdate() ";
        strsql += " AND Trip_No = @Trip_No ";
        strsql += " and isnull(hidden,'') <> 'y'";
        strsql += " AND Grop.CANC_PEOL = ''";
        strsql += " ORDER BY Grop_Depa ";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["tripConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand cmd = new SqlCommand(strsql, connect);

        cmd.Parameters.Add(new SqlParameter("@Trip_No", trip_no));
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            if (count == 0)
            {
                Literal1.Text += Convert.ToDateTime(reader["Grop_Depa"]).ToString("MM/dd");
                mm = Convert.ToDateTime(reader["Grop_Depa"]).ToString("MM");
            }
            else
            {
                if(mm == Convert.ToDateTime(reader["Grop_Depa"]).ToString("MM"))
                {
                    Literal1.Text += "." + Convert.ToDateTime(reader["Grop_Depa"]).ToString("dd");
                    mm = Convert.ToDateTime(reader["Grop_Depa"]).ToString("MM");
                }
                else
                {
                    Literal1.Text += "、" + Convert.ToDateTime(reader["Grop_Depa"]).ToString("MM/dd");
                    mm = Convert.ToDateTime(reader["Grop_Depa"]).ToString("MM");
                } 
            }
            count++;
        }
        reader.Close();
        cmd.Dispose();
        connect.Close();
    }

    #endregion

    //protected void get_data() 
    //{
    //    string strSql = "";
    //    strSql += "select pic_add from fk_pic where pic_num = @pic_num";
    //    string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
    //    SqlConnection connect = new SqlConnection(strConnString);
    //    connect.Open();
    //    SqlCommand comm = new SqlCommand(strSql, connect);
    //    comm.Parameters.Add(new SqlParameter("@pic_num", Request ["no"]));
    //    SqlDataReader reader = comm.ExecuteReader();
    //    if (reader.Read())
    //    {
    //        Literal1.Text = reader["pic_add"].ToString();
    //    }
    //    reader.Close();
    //    comm.Dispose();
    //    connect.Close();
    //}
}