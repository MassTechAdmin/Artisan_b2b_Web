using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.IO;
using System.Data;
using System.Data.SqlClient;

public partial class DMdownload_Edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getarea();

            getdata();
        }
    }

    protected void getarea()
    {
        string strSql = "";
        strSql += " SELECT [Area_No], [Area_Name] FROM [Area] ";
        strSql += " WHERE Array <> 0 and Area_ID <> 'Area999'";
        strSql += " ORDER BY [Area_No]";
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
        DropDownList1.SelectedIndex = 0;
    }
    
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void getdata()
    {
        string strSql = "";
        strSql += "select SN,DM_Title,DM_File,Area_Name,Area_No,DM_EndDate from DMdownload ";
        strSql += " WHERE SN = @SN";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand comm = new SqlCommand(strSql, connect);
        comm.Parameters.Add(new SqlParameter("@SN", Request["Aid"]));
        SqlDataReader reader = comm.ExecuteReader();

        if (reader.Read())
        {
            DropDownList1.SelectedValue = reader["Area_No"].ToString();
            TextBox1.Text = reader["DM_Title"].ToString();
            FileName.Text = reader["DM_File"].ToString();

            DateTime result;
            if (DateTime.TryParse(reader["DM_EndDate"].ToString(), out result))
            {
                RadDatePicker1.DbSelectedDate = Convert.ToDateTime(reader["DM_EndDate"].ToString());
            }
        }
        reader.Close();
        comm.Dispose();
        connect.Close();
    }
    
    protected void upd_data()
    {
        string exhpic = "";
        string filepath = "";
        if (this.FileUpload1.HasFile)
        {
            exhpic = Guid.NewGuid().ToString("N") + Path.GetExtension(this.FileUpload1.FileName);
            //exhpic = DateTime.Now.ToString("yyyyMMddHHmmss") + "." + this.FileUpload1.FileName.Split('.').GetValue(1);

            string uppath = Server.MapPath(@"~\Zupload\DMdownload\" + exhpic);

            ///-----------------------上傳新的檔案
            if (File.Exists(uppath) == false)
            {
                this.FileUpload1.SaveAs(uppath);
            }
            //刪除原來的檔案
            if (FileName.Text.Length > 0)
            {
                filepath = Server.MapPath(@"~\Zupload\DMdownload\" + FileName.Text);

                if (File.Exists(filepath) == true)
                {
                    File.Delete(filepath);
                }
            }
        }

        string strsql = "";
        strsql += " update DMdownload set";
        strsql += " Area_No=@Area_No, Area_Name=@Area_Name, DM_Title=@DM_Title, DM_EndDate=@DM_EndDate";
        if (FileUpload1.HasFile == true)
        {
            strsql += " ,DM_File=@DM_File";
        }
        strsql += " where SN=@SN";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        
        connect.Open();
        
        SqlCommand comm = new SqlCommand(strsql, connect);
        comm.Parameters.Add(new SqlParameter("@Area_NO", DropDownList1.SelectedValue));
        comm.Parameters.Add(new SqlParameter("@Area_Name", DropDownList1.SelectedItem.Text));
        comm.Parameters.Add(new SqlParameter("@DM_Title", TextBox1.Text));
        comm.Parameters.Add(new SqlParameter("@DM_EndDate", RadDatePicker1.DbSelectedDate));
        if (FileUpload1.HasFile == true)
        {
            comm.Parameters.Add(new SqlParameter("@DM_File", exhpic));
        }
        comm.Parameters.Add(new SqlParameter("@SN", Request["Aid"]));
        comm.ExecuteNonQuery();
        comm.Dispose();
        connect.Close();
        this.Response.Write("<script language='javascript' type='text/javascript'>alert('更新成功！');  window.location='DMdownload_Select.aspx';</script>");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        upd_data();
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string filepath = "";
        if (FileName.Text.Length > 0)
        {
            filepath = Server.MapPath(@"~\Zupload\DMdownload\" + FileName.Text);

            if (File.Exists(filepath) == true)
            {
                File.Delete(filepath);
            }
        }
        string strsql = "";
        strsql += " delete from DMdownload where SN = @SN";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        
        connect.Open();
        
        SqlCommand comm = new SqlCommand(strsql, connect);
        comm.Parameters.Add(new SqlParameter("@SN", Request["Aid"]));

        comm.ExecuteNonQuery();
        comm.Dispose();
        connect.Close();
        this.Response.Write("<script language='javascript' type='text/javascript'>alert('刪除成功！');  window.location='DMdownload_Select.aspx';</script>");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string filepath = "";
        if (FileName.Text.Length > 0)
        {
            filepath = Server.MapPath(@"~\Zupload\DMdownload\" + FileName.Text);

            if (File.Exists(filepath) == true)
            {
                File.Delete(filepath);
            }
        }
        string strsql = "";
        strsql += " update DMdownload set DM_File = '' where SN = @SN";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlCommand comm = new SqlCommand(strsql, connect);
        comm.Parameters.Add(new SqlParameter("@SN", Request["Aid"]));
        comm.ExecuteNonQuery();
        comm.Dispose();
        connect.Close();

        Response.Redirect("DMdownload_Edit.aspx?Aid=" + Request["Aid"]);
    }
}