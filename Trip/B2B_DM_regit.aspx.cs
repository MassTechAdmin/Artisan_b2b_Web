using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;

public partial class Trip_B2B_DM_regit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsFunction.Check.Check_Login_ID_And_PW();
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request["no"]))
            {

                Button3.Visible = false;
            }
            else
            {
                get_date();
                Button2.Visible = false;
            }
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string strSql = "";
        strSql += " update fk_pic set pic_add = @pic_add, pic_name =@pic_name  ";
        strSql += " where pic_num = @pic_num ";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand comm = new SqlCommand(strSql, connect);
        comm.Parameters.Add(new SqlParameter("@pic_num", Request["no"].ToString()));
        comm.Parameters.Add(new SqlParameter("@pic_add", TextBox2.Value));
        comm.Parameters.Add(new SqlParameter("@pic_name", TextBox3.Text));
        comm.ExecuteNonQuery();
        connect.Close();

        Response.Write("<script language='javascript' type='text/javascript'>alert('修改成功！');window.location='B2B_DM_select.aspx';</script>");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string strSql = "";
        strSql += " insert into fk_pic(pic_add,pic_name) values (@pic_add,@pic_name)";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand comm = new SqlCommand(strSql, connect);
        comm.Parameters.Add(new SqlParameter("@pic_add", TextBox2.Value));
        comm.Parameters.Add(new SqlParameter("@pic_name", TextBox3.Text));
        comm.ExecuteNonQuery();
        connect.Close();

        Response.Write("<script language='javascript' type='text/javascript'>alert('新增成功！');window.location='B2B_DM_select.aspx'</script>");
    }
    protected void get_date()
    {
        string strSql = "";
        strSql += " select pic_num, pic_name, pic_add from fk_pic ";
        strSql += " where pic_num = @pic_num ";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand comm = new SqlCommand(strSql, connect);
        comm.Parameters.Add(new SqlParameter("@pic_num", Request["no"]));
        SqlDataReader reader = comm.ExecuteReader();
        if (reader.Read())
        {
            TextBox2.Value = reader["pic_add"].ToString();
            TextBox3.Text = reader["pic_name"].ToString();
        }
        reader.Close();
        connect.Close();
    }
}