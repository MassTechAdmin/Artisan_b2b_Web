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

public partial class Trip_Hot_Local_Region : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsFunction.Check.Check_Login_ID_And_PW();
        if (IsPostBack) return;

        fn_DropDownList_Sales_News();
        fn_Show_Sales_News();
    }
    protected void fn_DropDownList_Sales_News()
    {
        string strSql = "";
        strSql += " select Glb_Id,Descrip from GLB_CODE";
        strSql += " where Glb_Code = 'Hot_Local'";
        strSql += " order by glb_Order";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlDataAdapter da = new SqlDataAdapter(strSql, strConnString);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlHot_Local.DataSource = dt;
        ddlHot_Local.DataValueField = "Glb_Id";
        ddlHot_Local.DataTextField = "Descrip";
        ddlHot_Local.DataBind();
        connect.Close();
    }

    protected void fn_Show_Sales_News()
    {
        string strSql = "";
        strSql += " SELECT [HL_No],[HL_Type],[HL_Content] FROM [Hot_Local]";
        strSql += " WHERE [HL_Type] = @HL_Type";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@HL_Type", ddlHot_Local.SelectedValue));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                fckHL_Content.Value = reader["HL_Content"].ToString();
            }
            reader.Close();
            comm.Dispose();
        }
        finally
        {
            connect.Close();
        }
    }

    protected void ddlHot_Local_SelectedIndexChanged(object sender, EventArgs e)
    {
        fn_Show_Sales_News();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        fn_Save();
    }

    protected void fn_Save()
    {
        int intReturnValue = -1;

        string strSql = "";
        strSql += " UPDATE [Hot_Local] SET";
        strSql += " [HL_Content] = @HL_Content";
        strSql += " ,[Modify_Date] = @Modify_Date";
        strSql += " ,[Modify_User] = @Modify_User";
        strSql += " WHERE [HL_Type] = @HL_Type";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@HL_Content", fckHL_Content.Value));
            comm.Parameters.Add(new SqlParameter("@Modify_Date", DateTime.Now));
            comm.Parameters.Add(new SqlParameter("@Modify_User", ""));
            comm.Parameters.Add(new SqlParameter("@HL_Type", ddlHot_Local.SelectedValue));
            intReturnValue = comm.ExecuteNonQuery();
            comm.Dispose();
        }
        finally
        {
            connect.Close();
        }

        if (intReturnValue > -1)
        {
            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('「存檔成功」');", true);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('「存檔失敗」');", true);
        }
    }
}
