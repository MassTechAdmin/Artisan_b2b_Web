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
using System.Data.OleDb;
using System.IO;

public partial class Trip_B2B_DM_select : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsFunction.Check.Check_Login_ID_And_PW();
        if (!IsPostBack)
        {
            get_date();
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void get_date()
    {
        string strSql = "";
        strSql += " select pic_add, pic_num ,pic_name from fk_pic";
        strSql += " order by pic_num";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlDataAdapter da = new SqlDataAdapter(strSql, strConnString);

        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataKeyNames = new string[] { "pic_num" };
        GridView1.DataBind();
        connect.Close();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strSql = "";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlCommand cmd;
        SqlConnection connect;
        SqlDataReader reader;
        int rowIndex = ((GridViewRow)(((Button)(e.CommandSource)).NamingContainer)).RowIndex;

        switch (e.CommandName)
        {
            case "edit":

                break;
            case "del":
                strSql += " delete fk_pic where pic_num = @pic_num ";
                connect = new SqlConnection(strConnString);
                connect.Open();
                cmd = new SqlCommand(strSql, connect);
                cmd.Parameters.Add(new SqlParameter("@pic_num", e.CommandArgument.ToString()));
                cmd.ExecuteNonQuery();
                connect.Close();
                Response.Write("<script language='javascript' type='text/javascript'>alert('刪除成功！');window.location='B2B_DM_select.aspx'</script>");
                break;
        }
    }
}