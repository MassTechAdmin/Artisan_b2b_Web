using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class DMdownload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsFunction.Check.CheckSession();
        if (!IsPostBack)
        {
            DropDownList_Area();
            get_Area();
            DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue("31"));
        }
    }
    protected void DropDownList_Area()
    {
        string strSql = "";
        strSql += " SELECT [Area_No], [Area_Name] FROM [Area] where Array <> '0' and Area_ID <> 'Area999' ORDER BY [Area_No]";
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

        DropDownList1.Items.Insert(0, new ListItem("全選", "0"));
        DropDownList1.SelectedIndex = 0;
    }
    protected void get_Area()
    {
        string strSql = "";
        Literal1.Text = "";
        strSql += " SELECT Area_No, Area_Name FROM Area where Array <> '0' and Area_ID <> 'Area999'";
        if(DropDownList1.SelectedValue != "0")
        { 
            strSql += " and Area_No = @Area_No"; 
        }
        strSql += " ORDER BY Area_No";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand comm = new SqlCommand(strSql, connect);
        comm.Parameters.Add(new SqlParameter("@Area_No", DropDownList1.SelectedValue));
        SqlDataReader reader = comm.ExecuteReader();
        while (reader.Read())
        {
            //Literal1.Text += "<table class=\"dm-table\" style=\"border-style:solid; \">";
            //Literal1.Text += "<tr>";
            //Literal1.Text += "<th>" + reader["Area_Name"].ToString() + "</th>";
            //Literal1.Text += "<th>DM名稱</th>";
            //Literal1.Text += "</tr>";
            //getDM(reader["Area_No"].ToString());
            //Literal1.Text += "</table>";

            getDM(reader["Area_No"].ToString(), reader["Area_Name"].ToString());
        }
        reader.Close();
        comm.Dispose();
        connect.Close();
    }
    protected void getDM(string area_no, string strArea_Name)
    {
        string strSql = "";
        int SN = 0;
        strSql += "select DM_Title,DM_File,Area_Name from DMdownload where Area_No = @Area_No and DM_EndDate >= @DM_EndDate";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand comm = new SqlCommand(strSql, connect);
        comm.Parameters.Add(new SqlParameter("@Area_No", area_no));
        comm.Parameters.Add(new SqlParameter("@DM_EndDate", DateTime.Today));
        SqlDataReader reader = comm.ExecuteReader();
        if (reader.HasRows)
        {
            Literal1.Text += "<table class=\"dm-table\" style=\"border-style:solid; \">";
            Literal1.Text += "<tr>";
            Literal1.Text += "<th>" + strArea_Name + "</th>";
            Literal1.Text += "<th>DM名稱</th>";
            Literal1.Text += "</tr>";

            while (reader.Read())
            {
                SN++;
                Literal1.Text += "<tr>";
                Literal1.Text += "<td>" + SN + "</td>";
                Literal1.Text += "<td><a download href =\"../Zupload/DMdownload/" + reader["DM_File"].ToString() + " \">" + reader["DM_Title"].ToString() + "</a></td>";
                Literal1.Text += "</tr>";
            }

            Literal1.Text += "</table>";
        }
        reader.Close();
        comm.Dispose();
        connect.Close();
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_Area();
    }
}