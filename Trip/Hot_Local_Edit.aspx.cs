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

public partial class Trip_Hot_Local_Edit : System.Web.UI.Page
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
        strSql += " select Glb_Id ,Descrip from GLB_CODE";
        strSql += " where Glb_Code = 'Hot_Local'";
        strSql += " and Glb_Id=@Glb_Id";
        strSql += " order by Glb_Id";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("Glb_Id", HidNo.Value));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                TextBox1.Text = reader["Descrip"].ToString();
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
        strSql += " UPDATE [GLB_CODE] SET";
        strSql += " [Descrip] = @Descrip";
        strSql += " where Glb_Code = 'Hot_Local'";
        strSql += " and Glb_Id=@Glb_Id";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand comm = new SqlCommand(strSql, connect);
        comm.Parameters.Add(new SqlParameter("@Descrip", TextBox1.Text.Trim()));
        comm.Parameters.Add(new SqlParameter("@Glb_Id", HidNo.Value));

        comm.ExecuteNonQuery();
        connect.Close();

        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('編輯成功！'); window.location = 'Hot_Local.aspx';", true);
    }
    private string getpath()
    {
        string strpath = Server.MapPath(@"~\App_Data\ForStr.txt");

        return strpath;
    }
}
