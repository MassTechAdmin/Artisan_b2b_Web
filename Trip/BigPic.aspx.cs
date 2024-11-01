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

public partial class Trip_BigPic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsFunction.Check.Check_Login_ID_And_PW();
        if (!IsPostBack)
        {
            LblTitle.Text = "首頁大圖";
            fn_Bind();
        }
    }
    protected void fn_Bind()
    {
        string strSql = "";
        strSql = " SELECT * FROM Banner WHERE banner_sec = 1 ORDER BY banner_order";

        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(constring);
        connect.Open();
        SqlCommand cmd = new SqlCommand(strSql, connect);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        DataTable dt = new DataTable();
        da.Fill(dt);
        cmd.Dispose();
        connect.Close();

        GridView1.DataSource = dt;
        GridView1.DataKeyNames = new string[] { "banner_no" };
        GridView1.DataBind();
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        fn_Bind();
    }
    protected void DdlPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        DdlPage2.SelectedValue = DdlPage.SelectedValue;
        GridView1.PageSize = int.Parse(DdlPage.SelectedValue);
        fn_Bind();
    }
    protected void DdlPage2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DdlPage.SelectedValue = DdlPage2.SelectedValue;
        GridView1.PageSize = int.Parse(DdlPage2.SelectedValue);
        fn_Bind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        fn_Bind();
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strKey = GridView1.DataKeys[e.RowIndex].Value.ToString();
        string strSql = " delete from Banner where banner_no= @banner_no";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@banner_no", strKey));
            comm.ExecuteNonQuery();
            comm.Dispose();
        }
        finally
        {
            connect.Close();
        }

        fn_Bind();
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("BigPic_Region.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(constring);
        try
        {
            connect.Open();
            for (int ii = 0; ii < GridView1.Rows.Count; ii++)
            {
                TextBox txbchose_order = (TextBox)GridView1.Rows[ii].FindControl("txbchose_order");
                HiddenField hidChose_No = (HiddenField)GridView1.Rows[ii].FindControl("hidChose_No");
                string strSql = "Update Banner set banner_order= @banner_order";
                strSql += " Where banner_no=@banner_no";
                SqlCommand cmd = new SqlCommand(strSql, connect);
                cmd.Parameters.Add(new SqlParameter("@banner_order", txbchose_order.Text.Trim()));
                cmd.Parameters.Add(new SqlParameter("@banner_no", hidChose_No.Value));
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            fn_Bind();
        }
        finally
        {
            connect.Close();
        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改成功！');", true);
        return;
    }
}
