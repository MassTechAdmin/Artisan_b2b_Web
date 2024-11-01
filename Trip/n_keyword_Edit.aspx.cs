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

public partial class Trip_n_keyword_Edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsFunction.Check.Check_Login_ID_And_PW();
        if (!string.IsNullOrEmpty(Request["no"]))
        {
            HidNo.Value = Request["no"];
        }
        if (!IsPostBack)
        {
            fn_show();
        }
    }
    protected void fn_show()
    {
        string strSql = "";
        strSql += " select * from n_keyword";
        strSql += " where 1=1";
        strSql += " and n_key_No=@n_key_No";
        strSql += " order by n_key_Order";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("n_key_No", HidNo.Value));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                TextBox1.Text = reader["n_key_Title"].ToString();
                TextBox2.Text = reader["n_key_Link"].ToString();
            }
            reader.Close();
            comm.Dispose();
        }
        finally
        {
            connect.Close();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        fn_Edit();
    }
    protected void fn_Edit()
    {

        string strSql = "";
        strSql += " UPDATE [n_keyword] SET";
        strSql += " [n_key_Title] = @n_key_Title";
        strSql += " ,[n_key_Link] = @n_key_Link";
        strSql += " where 1=1";
        strSql += " and n_key_No=@n_key_No";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand comm = new SqlCommand(strSql, connect);
        comm.Parameters.Add(new SqlParameter("@n_key_Title", TextBox1.Text.Trim()));
        comm.Parameters.Add(new SqlParameter("@n_key_Link", TextBox2.Text.Trim()));
        comm.Parameters.Add(new SqlParameter("@n_key_No", HidNo.Value));

        comm.ExecuteNonQuery();
        connect.Close();

        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('編輯成功！'); window.location = 'n_keyword.aspx';", true);
    }
    private string getpath()
    {
        string strpath = Server.MapPath(@"~\App_Data\ForStr.txt");

        return strpath;
    }
}