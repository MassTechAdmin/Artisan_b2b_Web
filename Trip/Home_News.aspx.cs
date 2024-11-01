using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Trip_Home_News : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Convert.ToString(Session["TripName"]) == "" || Convert.ToString(Session["TripName"]) == null)
        {
            Response.Write("<script language='javascript' type='text/javascript'>alert('您尚未登入！'); window.location='Login.aspx';</script>");
            Response.End();
        }

        if (!IsPostBack)
        {
            getData();
        }
    }

    #region === Get ===
    private void getData()
    {
        string strSql = "";
        strSql = " select * from Home_News ";
        strSql += " where 1=1";
        strSql += " order by HN_Orderby, HN_Number desc";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            connect.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSql, strConnString);
            DataTable dt = new DataTable();
            da.Fill(dt);

            this.GridView1.DataKeyNames = new String[] { "HN_Number" };
            this.GridView1.AllowPaging = true;
            this.GridView1.AllowSorting = true;
            this.GridView1.PageSize = 10;
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();

            if (dt.Rows.Count > 0)
            {
                Panel2.Visible = false;
                Panel1.Visible = true;
                Button4.Visible = true;
            }
            else
            {
                Panel2.Visible = true;
                Panel1.Visible = false;
                txbUrl.Text = "";
                txtOrderby.Text = "0";
                txtMSG.Text = "";
                Button3.Visible = false;
                Button4.Visible = false;
            }
            dt.Dispose();
        }
        catch { }
        finally { connect.Close(); }
    }
    #endregion

    #region === Add ===
    private void addData()
    {
        string strsql = "";
        strsql = " insert into Home_News  (";
        strsql += " HN_Pic, HN_Url, HN_Orderby, HN_MSG";
        strsql += " ) values (";
        strsql += " @HN_Pic, @HN_Url, @HN_Orderby, @HN_MSG";
        strsql += " )";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strsql, connect);
            comm.Parameters.Add(new SqlParameter("@HN_Pic", ""));
            comm.Parameters.Add(new SqlParameter("@HN_Url", txbUrl.Text));
            comm.Parameters.Add(new SqlParameter("@HN_Orderby", txtOrderby.Text));
            comm.Parameters.Add(new SqlParameter("@HN_MSG", txtMSG.Text));
            comm.ExecuteNonQuery();
            comm.Dispose();

            this.Response.Write("<script language='javascript' type='text/javascript'>alert('新增成功！');</script>");
        }
        catch { }
        finally { connect.Close(); }
    }
    #endregion

    #region === Edit ===
    private void editData()
    {
        string strsql = "";
        string num = "";
        string[] input = new string[4];

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                num = GridView1.DataKeys[i].Value.ToString();
                input[0] = "";
                input[1] = ((TextBox)GridView1.Rows[i].FindControl("TextBox5")).Text;
                input[2] = ((TextBox)GridView1.Rows[i].FindControl("TextBox6")).Text;
                input[3] = ((TextBox)GridView1.Rows[i].FindControl("TextBox7")).Text;

                strsql = " update Home_News set";
                strsql += "  HN_Pic = @HN_Pic";
                strsql += " ,HN_Url = @HN_Url";
                strsql += " ,HN_Orderby = @HN_Orderby";
                strsql += " ,HN_MSG = @HN_MSG";
                strsql += " where HN_Number=@HN_Number";

                try
                {
                    connect.Open();
                    SqlCommand comm = new SqlCommand(strsql, connect);
                    comm.Parameters.Add(new SqlParameter("@HN_Pic", input[0]));
                    comm.Parameters.Add(new SqlParameter("@HN_Url", input[1]));
                    comm.Parameters.Add(new SqlParameter("@HN_Orderby", input[2]));
                    comm.Parameters.Add(new SqlParameter("@HN_MSG", input[3]));
                    comm.Parameters.Add(new SqlParameter("@HN_Number", num));
                    comm.ExecuteNonQuery();
                    comm.Dispose();
                }
                catch { }
                finally { connect.Close(); }
            }
            this.Response.Write("<script language='javascript' type='text/javascript'>alert('更新成功！');</script>");
        }
        catch { }

    }
    #endregion

    #region === Delete ===
    private void delData(string num)
    {
        string strsql = "";
        strsql = " delete from Home_News ";
        strsql += " where HN_Number = @HN_Number";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strsql, connect);
            comm.Parameters.Add(new SqlParameter("@HN_Number", num));
            comm.ExecuteNonQuery();
            comm.Dispose();

            this.Response.Write("<script language='javascript' type='text/javascript'>alert('刪除成功！');</script>");
        }
        catch { }
        finally { connect.Close(); }
    }
    #endregion

    #region === GridView ===
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string num = GridView1.DataKeys[e.RowIndex].Value.ToString();

        delData(num);
        getData();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        getData();
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }
    #endregion

    #region === 控制項 ===
    protected void Button1_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = true;
        Button3.Visible = true;
        Button4.Visible = false;
        txbUrl.Text = "";
        txtOrderby.Text = "0";
        txtMSG.Text = "";
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        addData();
        getData();
        Button4.Visible = true;
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Panel2.Visible = false;
        Panel1.Visible = true;
        getData();
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        editData();
        getData();
    }
    #endregion




    
}