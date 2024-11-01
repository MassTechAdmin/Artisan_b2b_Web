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

public partial class WebControl_Search : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "var TabbedPanels1 = new Spry.Widget.TabbedPanels(\"TabbedPanels1\");", true);


        if (IsPostBack) return;

        fn_DropDownList_Area();
    }

    #region === 搜尋選項 ===
    protected void fn_DropDownList_Area()
    {
        string strSql = "";
        strSql += " SELECT [Area_No], [Area_Name] FROM [Area] ORDER BY [Area_No]";
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

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strSql = "";
        strSql += " SELECT DISTINCT Trip.SecClass_No, SecClass.SecClass_Name";
        strSql += " FROM Trip";
        strSql += " LEFT JOIN SecClass ON SecClass.SecClass_No = Trip.SecClass_No";
        strSql += " WHERE isnull(SecClass.SecClass_Name,'') <> ''";
        strSql += " AND (Trip.Area_No = " + DropDownList1.SelectedValue + ")";
        strSql += " AND (SecClass.SecClass_Name <> '')";
        strSql += " ORDER BY Trip.SecClass_No";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlDataAdapter da = new SqlDataAdapter(strSql, strConnString);
        DataTable dt = new DataTable();
        da.Fill(dt);
        DropDownList2.DataSource = dt;
        DropDownList2.DataValueField = "SecClass_No";
        DropDownList2.DataTextField = "SecClass_Name";
        DropDownList2.DataBind();
        connect.Close();

        DropDownList2.Items.Insert(0, new ListItem("全選", "0"));
        DropDownList2.SelectedIndex = 0;
    }
    // 團體旅遊
    protected void imgButton_Click(object sender, ImageClickEventArgs e)
    {
        string strOutDate = "";
        string strOutDate2 = "";
        string strArea = "";
        string strCountry = "";
        string tt = "";
        string Url = "ClassifyProduct.aspx?l=l";

        if (Convert.ToString(RadDatePicker1.SelectedDate) != null)
        {
            if (RadDatePicker1.SelectedDate >= new DateTime(2012, 1, 1))
            {
                strOutDate = Convert.ToString(RadDatePicker1.SelectedDate.ToShortDateString());
                Url += "&RadDatePicker1=" + strOutDate;
            }
        }

        if (Convert.ToString(RadDatePicker2.SelectedDate) != null)
        {
            if (RadDatePicker2.SelectedDate >= new DateTime(2012, 1, 1))
            {
                strOutDate2 = Convert.ToString(RadDatePicker2.SelectedDate.ToShortDateString());
                Url += "&RadDatePicker2=" + strOutDate2;
            }
        }
        if (string.IsNullOrEmpty(txbKey.Text.Trim()))
        {
            if (DropDownList1.SelectedValue != null && DropDownList1.SelectedIndex != 0)
            {
                strArea = DropDownList1.SelectedValue;
                Url += "&area=" + strArea;
            }

            if (DropDownList2.SelectedValue != "" && DropDownList2.SelectedIndex != 0)
            {
                strCountry = DropDownList2.SelectedValue;
                Url += "&secclassno=" + strCountry;
            }
        }
        else
        {
            tt = HttpUtility.UrlEncode(txbKey.Text.Trim().Replace("'", ""));
            Url += "&tp=" + tt;
        }

        if (Url == "ClassifyProduct.aspx?l=l")
        {
            Url = "ClassifyProduct.aspx";
        }

        Response.Redirect("~/" + Url);
    }
    #endregion
}
