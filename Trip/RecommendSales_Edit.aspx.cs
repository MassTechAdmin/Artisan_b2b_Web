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

public partial class RecommendSales_Edit : System.Web.UI.Page
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
            fn_DropDownList_Area();
            fn_show();
        }
    }
    protected void fn_show()
    {
        string strSql = "";
        strSql += " select * from RecommendSales";
        strSql += " where 1=1";
        strSql += " and Rec_No=@Rec_No";
        strSql += " order by Rec_Order";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("Rec_No", HidNo.Value));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                TextBox1.Text = reader["Rec_Title"].ToString();
                //TextBox3.Text = reader["Rec_Pirce"].ToString();
                TextBox2.Text = reader["Rec_Link"].ToString();
                //TextBox4.Text = reader["Gold_Area"].ToString();
                DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(reader["Rec_Area"].ToString()));
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
        strSql += " UPDATE RecommendSales SET";
        strSql += " [Rec_Title] = @Rec_Title";
        strSql += " ,[Rec_Link] = @Rec_Link";
        //strSql += " ,[Rec_Pirce] = @Rec_Pirce";
        strSql += " ,[Rec_Area] = @Rec_g_area";
        strSql += " where 1=1";
        strSql += " and Rec_No=@Rec_No";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand comm = new SqlCommand(strSql, connect);
        comm.Parameters.Add(new SqlParameter("@Rec_Title", TextBox1.Text.Trim()));
        comm.Parameters.Add(new SqlParameter("@Rec_Link", TextBox2.Text.Trim()));
        //comm.Parameters.Add(new SqlParameter("@Rec_Pirce", TextBox3.Text.Trim()));
        comm.Parameters.Add(new SqlParameter("@Rec_g_area", DropDownList1.SelectedValue));
        comm.Parameters.Add(new SqlParameter("@Rec_No", HidNo.Value));

        comm.ExecuteNonQuery();
        connect.Close();

        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('編輯成功！'); window.location = 'RecommendSales.aspx';", true);
    }
    private string getpath()
    {
        string strpath = Server.MapPath(@"~\App_Data\ForStr.txt");

        return strpath;
    }
    protected void fn_DropDownList_Area()
    {
        string strSql = "";
        strSql += " SELECT [Area_No], [Area_Name] FROM [Area] WHERE Array <> 0 ORDER BY [Area_No]";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlDataAdapter da = new SqlDataAdapter(strSql, strConnString);
        DataTable dt = new DataTable();
        da.Fill(dt);
        DropDownList1.DataSource = dt;
        DropDownList1.DataValueField = "Area_No";
        DropDownList1.DataTextField = "Area_Name";
        DropDownList1.DataBind();
        connect.Close();

        //DropDownList1.Items.Insert(0, new ListItem("全選", "0"));
        //DropDownList1.Items.Insert(1, new ListItem("新世界", "90000"));
        //DropDownList1.Items.Insert(2, new ListItem("典藏", "90001"));
        //DropDownList1.Items.Insert(1, new ListItem("中國", "90002"));
        DropDownList1.SelectedIndex = 0;
    }
}
