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
using System.Text;

public partial class Index_DDL_JSON : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string DropDownList1 = Request.Form["DropDownList1"];
        //string DropDownList2 = "";
        //string Country = "";
        string strSql = "";
        strSql += " select Group_Category_No,Group_Category_Name,Glb_Id from Group_Category";
        strSql += " LEFT JOIN Area ON Area.Area_Id=Group_Category.Glb_Id";
        strSql += " where Area_No = '" + DropDownList1 + "'";
        strSql += " and MultiCountry <> 0";
        strSql += " order by GC_OrderBy";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand comm = new SqlCommand(strSql, connect);
        SqlDataReader reader = comm.ExecuteReader();
        //if (reader.Read())
        string strJSON = "";
        while (reader.Read())
        {
            //DropDownList2 = reader["Group_Category_No"].ToString();
            //Country = reader["Group_Category_Name"].ToString(); 

            strJSON = strJSON + (strJSON == "" ? "" : ",") + "{ \"code\": \"" + reader["Group_Category_No"].ToString() + "\", \"name\": \"" + reader["Group_Category_Name"].ToString() + "\"}";
            //strJSON += ",{ \"code\": \"111\", \"name\": \"" 日本"\"}";
            //strJSON += ",{ \"code\": \"222\", \"name\": \"中國\"}";
            //strJSON += ",{ \"55\": \"日本\"}";
            //strJSON += ",{ \"66\": \"中國\"}";
        }
        reader.Close();
        comm.Dispose();
        connect.Close();

        strJSON = "[" + strJSON + "]";
        Response.Write(strJSON);
    }
}