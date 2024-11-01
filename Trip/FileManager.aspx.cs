using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.IO;

public partial class floder_FileManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsFunction.Check.Check_Login_ID_And_PW();
        if (Convert.ToString(Session["TripName"]) == "" || Convert.ToString(Session["TripName"]) == null)
        {
            Response.Write("<script language='javascript' type='text/javascript'>alert('您尚未登入！'); window.location='Login.aspx';</script>");
        }

        Session["TripName"] = Convert.ToString(Session["TripName"]);
        if (IsPostBack) return;

        string FolderID = Convert.ToString(Request["FolderID"] + "");//抓取目錄位置
        if (FolderID == "" || FolderID == "top")//根目錄
        {
            //Folder0();
            thisFolder();
            LU1();
        }
        else
        {
            //Folder1(FolderID);
            Panel1.Visible = true;
            LU1();
            LU2();
            //ImgGet1(FolderID);
            thisFolder(FolderID);
        }
    }

    #region 抓取目前目錄資訊
    private void thisFolder() //根目錄抓取目前目錄資訊 BY Awho 2014/05/14 LitThis
    {
        string strSql = "select top 1 ";
        strSql += " (select count(1) from trimgfile )as filecount ";//計算檔案數量
        strSql += " ,(select count(1) from trimgfdb where pdfolder_id = '') as foldercount  ";//計算資料夾數量
        strSql += " from trimgfdb";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            //comm.Parameters.Add(new SqlParameter("@DMID", DMID));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                LitThis.Text = "<td width=\"30%\" class=\"style_tablefont\">目錄名稱：根目錄</td>";
                LitThis.Text += "<td width=\"20%\" class=\"style_tablefont\">下層目錄數：" + reader["foldercount"].ToString() + "</td>";
                LitThis.Text += "<td width=\"14%\" class=\"style_tablefont\">圖片數：" + reader["filecount"].ToString() + "</td>";
            }
            else
            {
                LitThis.Text = "<td width=\"30%\" class=\"style_tablefont\">目錄名稱：根目錄</td>";
                LitThis.Text += "<td width=\"20%\" class=\"style_tablefont\">下層目錄數：0</td>";
                LitThis.Text += "<td width=\"14%\" class=\"style_tablefont\">圖片數：0</td>";
            }
        }
        catch
        {
            Response.Write("<script language='javascript' type='text/javascript'>alert('網站或資料庫維護中，請稍侯在試！'); history.back();</script>");
        }
        finally
        {
            connect.Close();
        }
    }
    private void thisFolder(string FolderID) //非根目錄抓取目前目錄資訊 BY Awho 2014/05/14 LitThis
    {
        string strSql = "select  ";
        strSql += " (select COUNT(1) from trimgfile where folder_id = @FolderID )as filecount ";//計算檔案數量
        strSql += " ,(select count(1) from trimgfdb where pdfolder_id = @FolderID ) as foldercount  ";//計算資料夾數量
        strSql += " ,folder_nm";
        strSql += " from trimgfdb";
        strSql += " where  folder_id = @FolderID";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@FolderID", FolderID));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                LitThis.Text = "<td width=\"30%\" class=\"style_tablefont\">目錄名稱：" + reader["folder_nm"].ToString() + "</td>";
                LitThis.Text += "<td width=\"20%\" class=\"style_tablefont\">下層目錄數：" + reader["foldercount"].ToString() + "</td>";
                LitThis.Text += "<td width=\"14%\" class=\"style_tablefont\">圖片數：" + reader["filecount"].ToString() + "</td>";
            }
            else
            {
                Response.Write("<script language='javascript' type='text/javascript'>alert('路徑有問題喔！'); history.back();</script>");
            }
        }
        catch
        {
            Response.Write("<script language='javascript' type='text/javascript'>alert('網站或資料庫維護中，請稍侯在試！'); history.back();</script>");
        }
        finally
        {
            connect.Close();
        }
    }
    #endregion

    #region 抓取資料夾目錄
    #region gridview目錄使用

    protected void GridView1_RowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        DataRowView myrows = (DataRowView)e.Row.DataItem;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (myrows == null)
            {
            }

        }
        if (e.Row.RowType == DataControlRowType.DataRow | e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[5].Visible = false;
        }
    }
    private const int _firstEditCellIndex = 2;
    private void LU1()
    {
        string FolderID = Convert.ToString(Request["FolderID"] + "");
        string strSql = "";
        if (FolderID == "" || FolderID == "top")//根目錄
        {
            strSql = "select TRIMGFDB.Folder_ID AS 編號 , TRIMGFDB.Folder_NM AS 下層目錄,count(TRIMGFILE.Folder_ID) as 圖片數 from TRIMGFDB";
            strSql += " left join TRIMGFILE on TRIMGFILE.LINK_URL like '%/' + TRIMGFDB.Folder_ID + '/%'";//判斷路徑來計算檔案數量
            strSql += " where isnull(TRIMGFDB.PDFolder_ID,'') = ''";
            strSql += " group by TRIMGFDB.Folder_ID,TRIMGFDB.Folder_NM";
        }
        else
        {
            strSql = "select TRIMGFDB.Folder_ID AS 編號 , TRIMGFDB.Folder_NM AS 下層目錄,count(TRIMGFILE.Folder_ID) as 圖片數 from TRIMGFDB";
            strSql += " left join TRIMGFILE on TRIMGFILE.LINK_URL like '%/' + TRIMGFDB.Folder_ID + '/%'";//判斷路徑來計算檔案數量
            strSql += " where TRIMGFDB.PDFolder_ID = @FolderID ";
            strSql += " group by TRIMGFDB.Folder_ID,TRIMGFDB.Folder_NM";
        }
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@FolderID", FolderID));
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = comm;
            DataTable dt = new DataTable();
            da.Fill(dt);
            comm.Dispose();
            connect.Close();

            //this.GridView1.Width = "100%";
            //this.GridView1.AllowPaging = true;
            //this.GridView1.AllowSorting = true;
            //this.GridView1.PageSize = 30;
            this.GridView1.AutoGenerateColumns = true;

            this.GridView1.DataSource = dt;
            this.GridView1.DataKeyNames = new string[] { "編號" };
            this.GridView1.DataBind();
        }
        catch
        {
            Response.Write("<script language='javascript' type='text/javascript'>alert('網站或資料庫維護中，請稍侯在試！'); history.back();</script>");
        }
        finally
        {
            connect.Close();
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        switch (e.CommandName)
        {
            case ("SingleClick"):

                int _rowIndex = int.Parse(e.CommandArgument.ToString());

                int _columnIndex = int.Parse(Request.Form["__EVENTARGUMENT"]);
                _gridView.SelectedIndex = _rowIndex;
                Control _editControl = _gridView.SelectedRow.Cells[1].Controls[1];
                string aa = ((Label)_editControl).Text;
                Response.Redirect("FileManager.aspx?FolderID=" + aa);
                break;
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton _singleClickButton = (LinkButton)e.Row.Cells[0].Controls[0];
            string _jsSingle = ClientScript.GetPostBackClientHyperlink(_singleClickButton, "");
            e.Row.Attributes.Add("onMouseOver", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#DDDDDD';");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=currentcolor;");
            for (int columnIndex = _firstEditCellIndex; columnIndex < e.Row.Cells.Count - 5; columnIndex++)
            {
                string js = _jsSingle.Insert(_jsSingle.Length - 2, columnIndex.ToString());
                e.Row.Cells[columnIndex].Attributes["onclick"] = js;
                e.Row.Cells[columnIndex].Attributes["style"] += "cursor:pointer;cursor:hand;";
            }

        }
    }
    protected override void Render(HtmlTextWriter writer)
    {
        foreach (GridViewRow r in GridView1.Rows)
        {
            if (r.RowType == DataControlRowType.DataRow)
            {
                for (int columnIndex = _firstEditCellIndex; columnIndex < r.Cells.Count; columnIndex++)
                {
                    Page.ClientScript.RegisterForEventValidation(r.UniqueID + "$ctl00", columnIndex.ToString());
                }
            }
        }
        foreach (GridViewRow r in GridView2.Rows)
        {
            if (r.RowType == DataControlRowType.DataRow)
            {
                for (int columnIndex = _firstEditCellIndex; columnIndex < r.Cells.Count; columnIndex++)
                {
                    Page.ClientScript.RegisterForEventValidation(r.UniqueID + "$ctl00", columnIndex.ToString());
                }
            }
        }
        base.Render(writer);
    }
    private string GridViewSortExpression
    {
        get { return ViewState["SortExpression"] as string ?? string.Empty; }
        set { ViewState["SortExpression"] = value; }
    }
    private string GridViewSortDirection
    {
        get { return ViewState["SortDirection"] as string ?? "ASC"; }
        set { ViewState["SortDirection"] = value; }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        LU1();
        GridView1.DataSource = SortDataTable(GridView1.DataSource as DataTable, true);
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        LU1();
        GridViewSortExpression = e.SortExpression;
        int pageIndex = GridView1.PageIndex;
        GridView1.DataSource = SortDataTable(GridView1.DataSource as DataTable, false);
        GridView1.DataBind();
        GridView1.PageIndex = pageIndex;
    }
    protected DataView SortDataTable(DataTable dataTable, bool isPageIndexChanging)
    {
        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            if (GridViewSortExpression != string.Empty)
            {
                if (isPageIndexChanging)
                {
                    dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GridViewSortDirection);
                }
                else
                {
                    dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GetSortDirection());
                }
            }
            return dataView;
        }
        else
        {
            return new DataView();
        }
    }
    private string GetSortDirection()
    {
        switch (GridViewSortDirection)
        {
            case "ASC":
                GridViewSortDirection = "DESC";
                break;
            case "DESC":
                GridViewSortDirection = "ASC";
                break;
        }
        return GridViewSortDirection;
    }

    #endregion
    #endregion

    #region 抓取圖片資料
    #region GRIDVIEW 圖片使用
    protected void GridView2_RowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        DataRowView myrows = (DataRowView)e.Row.DataItem;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (myrows != null)
            {

            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow | e.Row.RowType == DataControlRowType.Header)
        {
            //e.Row.Cells[10].Visible = false;
            //e.Row.Cells[7].Visible = false;
            //e.Row.Cells[8].Visible = false;
            //e.Row.Cells[9].Visible = false;
            BtnDelete.Visible = true;
        }
    }
    private void LU2()
    {
        string http = "http://b2b.artisan.com.tw/";
        string FolderID = Convert.ToString(Request["FolderID"] + "");//抓取目錄位置
        string strSql = "";
        strSql += " SELECT IMG_DR as 圖片名稱 , img_id as 編號 ,@http + link_url as 圖片路徑 ,'~/'+ link_url as 路徑";
        strSql += " FROM TRIMGFILE";
        strSql += " WHERE Folder_ID = @FolderID";
        strSql += " AND IMG_DR LIKE @IMG_DR";
        strSql += " ORDER BY IMG_ID DESC";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@http", http));
            comm.Parameters.Add(new SqlParameter("@FolderID", FolderID));
            comm.Parameters.Add(new SqlParameter("@IMG_DR", "%" + txbSearch.Text.Trim() + "%"));
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = comm;
            DataTable dt = new DataTable();
            da.Fill(dt);
            comm.Dispose();
            connect.Close();

            //this.GridView2.AllowPaging = true;
            //this.GridView2.AllowSorting = true;
            //this.GridView2.PageSize = 500; //20140827

            this.GridView2.DataSource = dt;
            this.GridView2.DataKeyNames = new string[] { "編號" };
            this.GridView2.DataBind();
        }
        catch
        {
            Response.Write("<script language='javascript' type='text/javascript'>alert('網站或資料庫維護中，請稍侯在試！'); history.back();</script>");
        }
        finally
        {
            connect.Close();
        }
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;

        //switch (e.CommandName)
        //{
        //    case ("SingleClick"):

        //        int _rowIndex = int.Parse(e.CommandArgument.ToString());

        //        int _columnIndex = int.Parse(Request.Form["__EVENTARGUMENT"]);
        //        _gridView.SelectedIndex = _rowIndex;




        //        Control _editControl = _gridView.SelectedRow.Cells[4].Controls[1];

        //        string aa = ((Label)_editControl).Text;



        //        //Response.Redirect( aa);
        //        //Response.Write("<script language='javascript' type='text/javascript'>window.open('" + aa + "');</script>");

        //        break;
        //}
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //LinkButton _singleClickButton = (LinkButton)e.Row.Cells[0].Controls[0];
            //string _jsSingle = ClientScript.GetPostBackClientHyperlink(_singleClickButton, "");


            e.Row.Attributes.Add("onMouseOver", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#DDDDDD';");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=currentcolor;");




            for (int columnIndex = _firstEditCellIndex; columnIndex < e.Row.Cells.Count; columnIndex++)
            {
                //string js = _jsSingle.Insert(_jsSingle.Length - 2, columnIndex.ToString());
                //e.Row.Cells[columnIndex].Attributes["onclick"] = js;
                //e.Row.Cells[columnIndex].Attributes["style"] += "cursor:pointer;cursor:hand;";
            }
        }
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        LU2();
        GridView2.DataSource = SortDataTable(GridView2.DataSource as DataTable, true);
        GridView2.PageIndex = e.NewPageIndex;
        GridView2.DataBind();
    }
    protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
    {
        LU2();
        GridViewSortExpression = e.SortExpression;
        int pageIndex = GridView2.PageIndex;
        GridView2.DataSource = SortDataTable(GridView2.DataSource as DataTable, false);
        GridView2.DataBind();
        GridView2.PageIndex = pageIndex;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        LU2();
    }
    // 搜尋
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LU2();
    }
    #endregion

    private void deleteImgFun(string url, string IMGID)//批次刪除圖片使用
    {
        string strSql = "delete trimgfile where img_id =@IMGID";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@IMGID", IMGID));
            comm.ExecuteNonQuery();
            comm.Dispose();
            if (File.Exists(Server.MapPath(url)) == true)//圖片存在才刪除
            {
                File.Delete(Server.MapPath(url));
            }
        }
        catch
        {
            Response.Write("<script language='javascript' type='text/javascript'>alert('網站或資料庫維護中，請稍侯在試！'); history.back();</script>");
        }
        finally
        {
            connect.Close();
        }
    }
    #endregion

    #region 控制元件BTN
    protected void AddFolder_Click(object sender, EventArgs e)
    {
        string FolderID = Convert.ToString(Request["FolderID"] + "");//抓取目錄位置
        FolderID = FolderID == "" || FolderID == "top" ? "top" : FolderID;
        Response.Write("<script language='javascript' type='text/javascript'>window.location='FolderEdit.aspx?FolderID=" + FolderID + "&Fun=FolderAdd';</script>");
    }
    protected void BtnPDfolder_Click(object sender, EventArgs e)
    {
        string FolderID = Convert.ToString(Request["FolderID"] + "");//抓取目錄位置
        string strSql = "select pdfolder_id from TRIMGFDB where folder_id = @FolderID";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@FolderID", FolderID));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                if (reader["pdfolder_id"].ToString() != "")
                { Response.Write("<script language='javascript' type='text/javascript'>window.location='fileManager.aspx?FolderID=" + reader["pdfolder_id"].ToString() + "'</script>"); }
                else
                { Response.Write("<script language='javascript' type='text/javascript'>window.location='fileManager.aspx'</script>"); }
            }
        }
        catch
        {
            Response.Write("<script language='javascript' type='text/javascript'>alert('網站或資料庫維護中，請稍侯在試！'); history.back();</script>");
        }
        finally
        {
            connect.Close();
        }
    }
    protected void BtnAddIMG_Click(object sender, EventArgs e)
    {
        string FolderID = Convert.ToString(Request["FolderID"] + "");//抓取目錄位置
        FolderID = FolderID == "" || FolderID == "top" ? "" : FolderID;
        Response.Write("<script language='javascript' type='text/javascript'>window.location='folderEdit.aspx?FolderID=" + FolderID + "&Fun=ImgAdd'</script>");
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        int i;
        int x = 0;
        for (i = 0; i < this.GridView2.Rows.Count; i++)
        {
            if (((CheckBox)GridView2.Rows[i].FindControl("CheckBox2")).Checked)
            {
                deleteImgFun(((Label)GridView2.Rows[i].FindControl("LBurl")).Text, GridView2.DataKeys[i].Value.ToString());//刪除圖片(路徑,IMGID)
                x++;
            }
        }
        Response.Write("<script language='javascript' type='text/javascript'>alert('刪除了" + x + "張圖片！'); </script>");
        LU2();
        string FolderID = Convert.ToString(Request["FolderID"] + "");//抓取目錄位置
        if (FolderID != "" || FolderID != "top")
        {
            thisFolder(FolderID);
        }
        else
        {
            thisFolder();
        }
    }
    #endregion
}
