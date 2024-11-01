using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class DMShow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsFunction.Check.CheckSession();
        if (!IsPostBack) 
        {
            get_data();
        }
    }

    protected void get_data() 
    {
        string strSql = "";
        strSql += "select pic_add from fk_pic where pic_num = @pic_num";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand comm = new SqlCommand(strSql, connect);
        comm.Parameters.Add(new SqlParameter("@pic_num", Request ["no"]));
        SqlDataReader reader = comm.ExecuteReader();
        if (reader.Read())
        {
            Literal1.Text = reader["pic_add"].ToString();
        }
        reader.Close();
        comm.Dispose();
        connect.Close();
    }
}