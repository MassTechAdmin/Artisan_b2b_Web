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
using System.Collections.Generic;
using System.Threading;

public partial class folder_FolderEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsFunction.Check.Check_Login_ID_And_PW();
        if (Convert.ToString(Session["TripName"]) == "" || Convert.ToString(Session["TripName"]) == null)
        {
            Response.Write("<script language='javascript' type='text/javascript'>alert('您尚未登入！'); window.location='Login.aspx';</script>");
        }

        Session["TripName"] = Convert.ToString(Session["TripName"]);
        string FolderID = Convert.ToString(Request["FolderID"] + "");//抓取目錄位置
        string Fun = Convert.ToString(Request["Fun"] + "");//抓取動作指令
        string IMGID = Convert.ToString(Request["IMGID"] + "");//抓取是否圖片&圖片編號
        if (IsPostBack) return;
        if (Fun != "")
        {
            switch (Fun)
            {
                case "FolderAdd"://新增資料夾
                    if (FolderID == "")
                    { Response.Write("<script language='javascript' type='text/javascript'>alert('路徑好像有點問題喔！'); history.back();</script>"); }
                    else
                    {
                        BtnFolderAdd.Visible = true;
                        PanFolder.Visible = true;
                        Panel1.Visible = true;
                        LU1(FolderID);
                    }

                    break;
                case "FolderEdit"://修改資料夾
                    if (FolderID == "")
                    { Response.Write("<script language='javascript' type='text/javascript'>alert('路徑好像有點問題喔！'); history.back();</script>"); }
                    else
                    {
                        BtnFolderEdit.Visible = true;
                        PanFolder.Visible = true;
                        Panel1.Visible = true;
                        LU2(FolderID);
                    }

                    break;
                case "ImgAdd"://新增圖片
                    if (FolderID == "")
                    { Response.Write("<script language='javascript' type='text/javascript'>alert('路徑好像有點問題喔！'); history.back();</script>"); }
                    else
                    {
                        Panel2.Visible = true;
                        HtmlGenericControl body = (HtmlGenericControl)Master.FindControl("body1");//(HtmlGenericControl)Master.FindControl("body1");
                        body.Attributes.Add("onload", "addFileUploaderState()");
                        BtnImgAdd.Visible = true;
                        PanImg.Visible = true;
                        BtnImgAdd.Visible = false;
                        PanFile.Visible = true;
                        FileUpload1.Visible = false;
                        // PanImgEdit.Visible = true;
                        LU3(FolderID);
                        #region 檔案上傳新增JAVA
                        //for (int i = 1; i <= 5; i++)
                        //{
                        //    FileUpload myFL = new FileUpload();
                        //    //myFL = (FileUpload)Page.FindControl("FileUpload" + i);
                        //    //myFL.Attributes.Add("Onchange", "FileUpload" + i + "_change()");
                        //    //if (i != 1) { myFL.Attributes.Add("style", "display:none;"); }
                        //}
                        #endregion
                    }

                    break;
                case "ImgEdit"://修改圖片
                    if (IMGID == "")
                    { Response.Write("<script language='javascript' type='text/javascript'>alert('圖片代碼有問題喔！'); history.back();</script>"); }
                    else
                    {
                        PanImgEdit.Visible = true;
                        PanImg.Visible = true;
                        BtnImgEdit.Visible = true;
                        PanImgDR.Visible = true;
                        LU4(IMGID);
                        for (int i = 2; i <= 5; i++)
                        {
                            FileUpload myFL = new FileUpload();
                            //myFL = (FileUpload)Page.FindControl("FileUpload" + i);
                            //myFL.Visible = false;
                            // myFL.Attributes.Add("style", "display:none;");
                        }
                    }
                    break;
                default:
                    Response.Write("<script language='javascript' type='text/javascript'>alert('有點問題喔！'); history.back();</script>");
                    break;
            }
        }
    }

    #region 資料夾用
    private void LU1(string FolderID)//新增顯示上層用
    {
        string strSql = "select folder_nm from trimgfdb where folder_id = @FolderID";
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
                Literal1.Text = reader["folder_nm"].ToString();
            }
            reader.Close();
            comm.Dispose();
        }
        catch
        {
            Response.Write("<script language='javascript' type='text/javascript'>alert('網站或資料庫維護中，請稍侯在試！1'); history.back();</script>");
        }
        finally
        {
            connect.Close();
        }
        Literal1.Text = FolderID == "top" ? "根目錄" : Literal1.Text; //top直接把值換成根目錄
        string folderUrl = FolderID;
        while (FolderID != "")//撈出該層資料夾完整路徑
        {
            strSql = "select pdfolder_id from trimgfdb where folder_id = @FolderID"; //往上尋找上層資料夾
            string strConnString1 = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
            SqlConnection connect1 = new SqlConnection(strConnString1);
            try
            {
                connect1.Open();
                SqlCommand comm1 = new SqlCommand(strSql, connect1);
                comm1.Parameters.Add(new SqlParameter("@FolderID", FolderID));
                SqlDataReader reader1 = comm1.ExecuteReader();
                if (reader1.Read())
                {
                    FolderID = reader1["pdfolder_id"].ToString();//上層如果是根目錄此處將會是""
                    folderUrl = FolderID + "/" + folderUrl;//路徑字串疊加
                }
                else
                {
                    if (FolderID == "top")
                    {
                        FolderID = "";//避免輸入資料夾ID是錯誤的 讓迴圈自動跳出
                        folderUrl = "";
                    }
                    else
                    {
                        FolderID = "";//避免輸入資料夾ID是錯誤的 讓迴圈自動跳出
                        folderUrl = "";
                        Response.Write("<script language='javascript' type='text/javascript'>alert('有點問題喔！'); history.back();</script>");
                        BtnFolderAdd.Visible = false;
                        BtnFolderCancel.Visible = false;
                    }
                }
            }
            catch
            {
                Response.Write("<script language='javascript' type='text/javascript'>alert('網站或資料庫維護中，請稍侯在試！2'); history.back();</script>");
            }
            finally
            {
                connect1.Close();
            }
        }
        LbUrl.Text = "Zupload" + folderUrl;//把路徑記錄到LABEL上
    }
    private void LU2(string FolderID)//修改時顯示資料
    {
        string strSql = "select isnull(b.folder_nm,'根目錄') as folder_nm ,trimgfdb.folder_nm as folderName from trimgfdb ";
        strSql += " left join (select folder_nm,folder_id from trimgfdb )b on trimgfdb.pdfolder_id = b.folder_id ";
        strSql += " where trimgfdb.folder_id = @FolderID ";
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
                Literal1.Text = reader["folder_nm"].ToString();
                TxtFolderNM.Text = reader["folderName"].ToString();
            }
            else
            {
                Response.Write("<script language='javascript' type='text/javascript'>alert('有點問題喔！'); history.back();</script>");
                BtnFolderEdit.Visible = false;
                BtnFolderCancel.Visible = false;
            }
        }
        catch
        {
            Response.Write("<script language='javascript' type='text/javascript'>alert('網站或資料庫維護中，請稍侯在試！3'); history.back();</script>");
        }
        finally
        {
            connect.Close();
        }
        string folderUrl = FolderID;
        while (FolderID != "")//撈出該層資料夾完整路徑
        {
            strSql = "select pdfolder_id from trimgfdb where folder_id = @FolderID";//往上尋找上層資料夾
            string strConnString1 = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
            SqlConnection connect1 = new SqlConnection(strConnString1);
            try
            {
                connect1.Open();
                SqlCommand comm1 = new SqlCommand(strSql, connect1);
                comm1.Parameters.Add(new SqlParameter("@FolderID", FolderID));
                SqlDataReader reader1 = comm1.ExecuteReader();
                if (reader1.Read())
                {
                    FolderID = reader1["pdfolder_id"].ToString();//上層如果是根目錄此處將會是""
                    folderUrl = FolderID + "/" + folderUrl;//路徑字串疊加
                }
                else
                {
                    FolderID = "";//避免輸入資料夾ID是錯誤的 讓迴圈自動跳出
                    folderUrl = "";
                }
            }
            catch
            {
                Response.Write("<script language='javascript' type='text/javascript'>alert('網站或資料庫維護中，請稍侯在試！4'); history.back();</script>");
            }
            finally
            {
                connect1.Close();
            }
        }
        LbUrl.Text = "Zupload" + folderUrl;//把路徑記錄到LABEL上
    }

    private void AddChecK_Folder()//新增檢查資料
    {
        bool check = true;
        if ((TxtFolderNM.Text).Length <= 0)
        {
            check = false;
            this.Response.Write("<script language='javascript' type='text/javascript'>alert('資料夾標題，必須輸入！'); history.back();</script>");
        }
        if (check == true)
        {
            Add_Folder();
        }
    }
    private void Add_Folder()//新增資料庫及新建資料夾
    {
        string FolderID = Convert.ToString(Request["FolderID"] + "");//抓取目錄位置
        string strSql = "insert into trimgfdb (";
        strSql += "folder_id,folder_nm,pdfolder_id,upd_dtm,city_cd,natn_cd,creat_Emp,creat_dtm)";
        strSql += "select right('000000'+cast((isNull(right(max(folder_id),6),0)+1) as varchar),6) ,@folder_nm,@FolderID,getdate(),'','',@creat_Emp,getdate() from trimgfdb ";//最大資料夾ID+1
        strSql += "   select max(folder_id) as folder_id from trimgfdb";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@folder_nm", TxtFolderNM.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@FolderID", FolderID == "top" ? "" : FolderID));
            comm.Parameters.Add(new SqlParameter("@creat_Emp", Convert.ToString(Session["TripName"])));
            //comm.ExecuteNonQuery();
            //comm.Dispose();
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                LbUrl.Text += "/" + reader["folder_id"].ToString();//把新的資料夾ID路徑丟入LABEL記錄
                //FolderID = reader["folder_id"].ToString();//把新資料夾ID丟回給ID 讓他完成後自動轉至新頁面
            }
        }
        catch
        {
            Response.Write("<script language='javascript' type='text/javascript'>alert('網站或資料庫維護中，請稍侯在試！5'); history.back();</script>");
        }
        finally
        {
            connect.Close();
            if (Directory.Exists(Server.MapPath("~/") + LbUrl.Text) == false)//資料夾不存在才建立
            {
                Directory.CreateDirectory(Server.MapPath("~/") + LbUrl.Text);//建立資料夾
                //Directory.Move(Server.MapPath("~/upload/test123"), Server.MapPath("~/upload/test"));//改資料夾名稱
            }
            Response.Write("<script language='javascript' type='text/javascript'>alert('新增成功！'); window.location='fileManager.aspx?FolderID=" + FolderID + "';</script>");
        }
    }

    private void EditChecK_Folder()
    {
        bool check = true;
        if ((TxtFolderNM.Text).Length <= 0)
        {
            check = false;
            this.Response.Write("<script language='javascript' type='text/javascript'>alert('資料夾標題，必須輸入！'); history.back();</script>");
        }
        if (check == true)
        {
            Edit_Folder();
        }
    }
    private void Edit_Folder()//修改資料夾名稱 不動到SERVER端資料夾名稱
    {
        string FolderID = Convert.ToString(Request["FolderID"] + "");//抓取目錄位置
        string strSql = "update trimgfdb set ";
        strSql += " folder_nm = @folder_nm ";
        strSql += " ,UPD_DTM = getdate() ";
        strSql += " ,upd_EMP = @upd_EMP ";
        strSql += " where folder_id = @FolderID ";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@folder_nm", TxtFolderNM.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@FolderID", FolderID));
            comm.Parameters.Add(new SqlParameter("@upd_EMP", Convert.ToString(Session["TripName"])));
            comm.ExecuteNonQuery();
            comm.Dispose();
        }
        catch
        {
            Response.Write("<script language='javascript' type='text/javascript'>alert('網站或資料庫維護中，請稍侯在試！6'); history.back();</script>");
        }
        finally
        {
            connect.Close();
            Response.Write("<script language='javascript' type='text/javascript'>alert('修改成功！'); window.location='fileManager.aspx?FolderID=" + FolderID + "';</script>");
        }
    }
    #endregion

    #region 資料夾用控制元件
    protected void BtnFolderAdd_Click(object sender, EventArgs e)
    {
        AddChecK_Folder();
    }

    protected void BtnFolderEdit_Click(object sender, EventArgs e)
    {
        EditChecK_Folder();
    }

    protected void BtnFolderCancel_Click(object sender, EventArgs e)
    {
        string FolderID = Convert.ToString(Request["FolderID"] + "");//抓取目錄位置
        string Fun = Convert.ToString(Request["Fun"] + "");//抓取動作指令
        if (Fun == "FolderEdit")
        {
            string strSql = "select pdfolder_id from trimgfdb where folder_id = @FolderID";
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
                    FolderID = reader["pdfolder_id"].ToString();
                }
                reader.Close();
                comm.Dispose();
            }
            catch
            {
                Response.Write("<script language='javascript' type='text/javascript'>alert('網站或資料庫維護中，請稍侯在試！7'); history.back();</script>");
            }
            finally
            {
                connect.Close();
            }
        }
        FolderID = FolderID == "top" ? "" : FolderID;
        Response.Write("<script language='javascript' type='text/javascript'> window.location='fileManager.aspx?FolderID=" + FolderID + "'</script>");
    }
    #endregion

    #region 圖片用
    private void LU3(string FolderID)//圖片上傳新增使用
    {
        string strSql = "select folder_nm from trimgfdb where folder_id = @FolderID";
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
                Literal2.Text = reader["folder_nm"].ToString();
            }
            reader.Close();
            comm.Dispose();
        }
        catch
        {
            Response.Write("<script language='javascript' type='text/javascript'>alert('網站或資料庫維護中，請稍侯在試！8'); history.back();</script>");
        }
        finally
        {
            connect.Close();
        }
        Literal2.Text = FolderID == "top" ? "根目錄" : Literal2.Text; //top直接把值換成根目錄
        string folderUrl = FolderID;
        while (FolderID != "")//撈出該層資料夾完整路徑
        {
            strSql = "select pdfolder_id from trimgfdb where folder_id = @FolderID";//往上尋找上層資料夾
            string strConnString1 = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
            SqlConnection connect1 = new SqlConnection(strConnString1);
            try
            {
                connect1.Open();
                SqlCommand comm1 = new SqlCommand(strSql, connect1);
                comm1.Parameters.Add(new SqlParameter("@FolderID", FolderID));
                SqlDataReader reader1 = comm1.ExecuteReader();
                if (reader1.Read())
                {
                    FolderID = reader1["pdfolder_id"].ToString();//上層如果是根目錄此處將會是""
                    folderUrl = FolderID + "/" + folderUrl;//路徑字串疊加
                }
                else
                {
                    if (FolderID == "top")
                    {
                        FolderID = "";//避免無窮迴圈
                        folderUrl = "";
                    }
                    else
                    {
                        FolderID = "";//避免輸入資料夾ID是錯誤的 讓迴圈自動跳出
                        folderUrl = "";
                        Response.Write("<script language='javascript' type='text/javascript'>alert('路徑有點問題喔！'); history.back();</script>");
                        BtnImgAdd.Visible = false;
                        BtnImgCancel.Visible = false;
                    }
                }
            }
            catch
            {
                Response.Write("<script language='javascript' type='text/javascript'>alert('網站或資料庫維護中，請稍侯在試！9'); history.back();</script>");
            }
            finally
            {
                connect1.Close();
            }
        }
        LbImgUrl.Text = "Zupload" + folderUrl;//把路徑記錄到LABEL上
    }

    private void LU4(string IMGID)
    {
        string strSql = "select * from trimgfile  left join trimgfdb on trimgfile.folder_id = trimgfdb.folder_id where img_id = @IMGID";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@IMGID", IMGID));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                Literal2.Text = reader["folder_nm"].ToString();
                Image1.ImageUrl = "~/" + reader["link_url"].ToString();
                LbImgUrl.Text = reader["link_url"].ToString();
                TxtImgDR.Text = reader["img_dr"].ToString();
            }
            reader.Close();
            comm.Dispose();
        }
        catch
        {
            Response.Write("<script language='javascript' type='text/javascript'>alert('網站或資料庫維護中，請稍侯在試！10'); history.back();</script>");
        }
        finally
        {
            connect.Close();
        }
        //Literal2.Text = FolderID == "top" ? "根目錄" : Literal2.Text; //top直接把值換成根目錄
    }

    private void AddCheck_Img()
    {
        bool check = Request.Files.Count > 0 ? true : false;
        //if ((TxtImgDR.Text).Length <= 0)
        //{
        //    check = false;
        //    this.Response.Write("<script language='javascript' type='text/javascript'>alert('圖片標題，必須輸入！'); history.back();</script>");
        //}
        for (int i = 1; i <= Request.Files.Count; i++)
        {
            FileUpload myFL = new FileUpload();
            myFL = (FileUpload)Page.FindControl("FileUpload" + i);
            if ((myFL.FileName).IndexOf(".") <= 0)
            {
                if (i == 1)
                {
                    check = false;
                    this.Response.Write("<script language='javascript' type='text/javascript'>alert('圖片必須上傳！'); history.back();</script>");
                }
            }
            else
            {
                //string filetype = FileUpload1.FileName.Split('.').GetValue(1).ToString();
                string[] tempType = (myFL.PostedFile.ContentType.Split('/'));
                //if (tempType[0] != "image") { check = false; this.Response.Write("<script language='javascript' type='text/javascript'>alert('第" + i + "張圖片格式錯誤喔！'); history.back();</script>"); }
                Double FileSize = myFL.PostedFile.ContentLength / 1000;
                if (FileSize > 4000)
                { check = false; this.Response.Write("<script language='javascript' type='text/javascript'>alert('檔案太大了喔！'); history.back();</script>"); }
            }
        }
        if (check == true)
        {
            Add_img();
        }
    }
    private void Add_img()
    {
        string FolderID = Convert.ToString(Request["FolderID"] + "");//抓取目錄位置
        for (int i = 1; i <= Request.Files.Count; i++)
        {
            FileUpload myFL = new FileUpload();
            myFL = (FileUpload)Page.FindControl("FileUpload" + i);
            if ((myFL.FileName).IndexOf(".") > 0)
            {
                string strSql = "insert into trimgfile ";
                strSql += "(img_id,folder_id,img_DR,IMG_extn,link_url,upd_dtm)";
                strSql += "select right('00000000'+cast((isNull(right(max(img_id),8),0)+1) as varchar),8) ,@FolderID,@img_DR,@IMG_extn,@link_url + right('00000000'+cast((isNull(right(max(img_id),8),0)+1) as varchar),8) +'.'+ @IMG_extn,getdate() from trimgfile";
                strSql += " select top 1  img_id ,link_url  from trimgfile order by img_id desc";
                string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
                SqlConnection connect = new SqlConnection(strConnString);
                try
                {
                    connect.Open();
                    SqlCommand comm = new SqlCommand(strSql, connect);
                    comm.Parameters.Add(new SqlParameter("@img_DR", TxtImgDR.Text.Trim() == "" ? myFL.FileName.Split('.').GetValue(0) : TxtImgDR.Text.Trim()));
                    comm.Parameters.Add(new SqlParameter("@FolderID", FolderID == "top" ? "" : FolderID));
                    comm.Parameters.Add(new SqlParameter("@IMG_extn", myFL.FileName.Split('.').GetValue(1).ToString()));
                    comm.Parameters.Add(new SqlParameter("@link_url", LbImgUrl.Text + "/"));
                    SqlDataReader reader = comm.ExecuteReader();
                    if (reader.Read())
                    {
                        myFL.SaveAs(Server.MapPath(@"~\" + reader["link_url"].ToString()));
                    }
                }
                catch
                {
                    Response.Write("<script language='javascript' type='text/javascript'>alert('網站或資料庫維護中，請稍侯在試！11'); history.back();</script>");
                }
                finally
                {
                    connect.Close();
                    Response.Write("<script language='javascript' type='text/javascript'>alert('新增成功！'); window.location='fileManager.aspx?FolderID=" + FolderID + "';</script>");
                }
            }
        }

    }

    private void EditCheck_Img()
    {
        bool check = true;
        //if ((TxtImgDR.Text).Length <= 0)
        //{
        //    check = false;
        //    this.Response.Write("<script language='javascript' type='text/javascript'>alert('圖片標題，必須輸入！'); history.back();</script>");
        //}
        if ((FileUpload1.FileName).IndexOf(".") > 0)
        {
            //string filetype = FileUpload1.FileName.Split('.').GetValue(1).ToString();
            string[] tempType = (FileUpload1.PostedFile.ContentType.Split('/'));
            if (tempType[0] != "image") { check = false; this.Response.Write("<script language='javascript' type='text/javascript'>alert('圖片格式錯誤喔！'); history.back();</script>"); }
            Double FileSize = FileUpload1.PostedFile.ContentLength / 1000;
            if (FileSize > 4000)
            { check = false; this.Response.Write("<script language='javascript' type='text/javascript'>alert('檔案太大了喔！'); history.back();</script>"); }
        }

        if (check == true)
        {
            Edit_img();
        }
    }
    private void Edit_img()
    {
        string FolderID = Convert.ToString(Request["FolderID"] + "");//抓取目錄位置
        string IMGID = Convert.ToString(Request["IMGID"] + "");//抓取目錄位
        string strSql = "UPDATE trimgfile SET ";
        strSql += " img_DR = @img_DR ";
        strSql += " ,upd_emp = @upd_emp ";
        strSql += " ,upd_dtm = getdate() ";
        strSql += " where img_id = @IMGID";
        strSql += " select folder_id from trimgfile where img_id  = @IMGID ";
        if (FileUpload1.FileName.IndexOf(".") > 0)
        {
            FileUpload1.SaveAs(Server.MapPath(@"~/" + LbImgUrl.Text));
        }
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@img_DR", TxtImgDR.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@IMGID", IMGID));
            comm.Parameters.Add(new SqlParameter("@upd_emp", Convert.ToString(Session["TripName"])));
            //comm.Parameters.Add(new SqlParameter("@link_url", LbImgUrl.Text + "/"));
            //comm.Parameters.Add(new SqlParameter("@FolderID", FolderID == "top" ? "" : FolderID));
            //comm.ExecuteNonQuery();
            //comm.Dispose();
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                FolderID = reader["folder_id"].ToString();
            }
        }
        catch(SqlException ex)
        {
            Response.Write("<script language='javascript' type='text/javascript'>alert('網站或資料庫維護中，請稍侯在試！12');"  + ex.Message + " history.back();</script>");
        }
        finally
        {
            connect.Close();
            Response.Write("<script language='javascript' type='text/javascript'>alert('修改成功！'); window.location='fileManager.aspx?FolderID=" + FolderID + "';</script>");
        }

    }
    #endregion

    #region 圖片用控制元件

    protected void BtnImgAdd_Click(object sender, EventArgs e)
    {
        //AddCheck_Img();
        //int x = Request.Files.Count;
        //HttpPostedFile file = Request.Files["File1"];
        //Response.Write(file.FileName + x);
    }
    protected void BtnImgEdit_Click(object sender, EventArgs e)
    {
        EditCheck_Img();
    }
    protected void BtnImgCancel_Click(object sender, EventArgs e)
    {
        string FolderID = Convert.ToString(Request["FolderID"] + "");//抓取目錄位置
        string Fun = Convert.ToString(Request["Fun"] + "");//確認新增還是修改
        string IMGID = Convert.ToString(Request["IMGID"] + "");//確認新增還是修改
        if (Fun == "ImgEdit")
        {
            string strSql = "select folder_id from trimgfile where img_id = @IMGID";
            string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
            SqlConnection connect = new SqlConnection(strConnString);
            try
            {
                connect.Open();
                SqlCommand comm = new SqlCommand(strSql, connect);
                comm.Parameters.Add(new SqlParameter("@IMGID", IMGID));
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    FolderID = reader["folder_id"].ToString();
                }
                reader.Close();
                comm.Dispose();
            }
            catch
            {
                Response.Write("<script language='javascript' type='text/javascript'>alert('網站或資料庫維護中，請稍侯在試！13'); history.back();</script>");
            }
            finally
            {
                connect.Close();
            }
        }
        else
        {
            FolderID = FolderID == "top" ? "" : FolderID;
        }
        Response.Write("<script language='javascript' type='text/javascript'>window.location='filemanager.aspx?FolderID=" + FolderID + "';</script>");
    }

    #endregion

    int intItem = 0;
    protected void FileUploader1_FileReceived(object sender, com.flajaxian.FileReceivedEventArgs e)
    {
        intItem++;
        //Thread.Sleep(2000);
        //for (int ii = 0; ii <= 1000; ii++)
        //{/* 暫停時間使用 */ }

        string FolderID = HttpContext.Current.Request.Form["FolderID"];
        string folderUrl = FolderID;
        if (FolderID != null)
        {
            while (FolderID != "")//撈出該層資料夾完整路徑
            {
                string strSql = "select pdfolder_id from trimgfdb where folder_id = @FolderID";//往上尋找上層資料夾
                string strConnString1 = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
                SqlConnection connect1 = new SqlConnection(strConnString1);
                try
                {
                    connect1.Open();
                    SqlCommand comm1 = new SqlCommand(strSql, connect1);
                    comm1.Parameters.Add(new SqlParameter("@FolderID", FolderID));
                    SqlDataReader reader1 = comm1.ExecuteReader();
                    if (reader1.Read())
                    {
                        FolderID = reader1["pdfolder_id"].ToString();//上層如果是根目錄此處將會是""
                        folderUrl = FolderID + "/" + folderUrl;//路徑字串疊加
                    }
                    else
                    {
                        if (FolderID == "top")
                        {
                            FolderID = "";//避免無窮迴圈
                            folderUrl = "";
                        }
                        else
                        {
                            FolderID = "";//避免輸入資料夾ID是錯誤的 讓迴圈自動跳出
                            folderUrl = "";
                        }
                    }
                    reader1.Close();
                    comm1.Dispose();
                }
                finally
                {
                    connect1.Close();
                }
            }
            
            folderUrl = "Zupload" + folderUrl;
           
            if (e.isLast == true)
            {
                FolderID = HttpContext.Current.Request.Form["FolderID"];
                string strSql = "insert into trimgfile (";
                strSql += "img_id,folder_id,img_DR,IMG_extn,link_url";
                strSql += ",upd_dtm,creat_emp,creat_DTM";
                strSql += ")";
                strSql += " select right('00000000'+cast((isNull(right(max(img_id),8),0)+1) as varchar),8) ,@FolderID,@img_DR,@IMG_extn,@link_url + right('00000000'+cast((isNull(right(max(img_id),8),0)+1) as varchar),8) +'.'+ @IMG_extn,getdate(),@creat_emp,getdate() from trimgfile";
                strSql += " select top 1  img_id ,link_url  from trimgfile order by img_id desc";
                string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
                SqlConnection connect = new SqlConnection(strConnString);
                try
                {
                    connect.Open();
                    SqlCommand comm = new SqlCommand(strSql, connect);
                    comm.Parameters.Add(new SqlParameter("@img_DR", e.File.FileName.Split('.').GetValue(0).ToString()));
                    comm.Parameters.Add(new SqlParameter("@FolderID", FolderID == "top" ? "" : FolderID));
                    comm.Parameters.Add(new SqlParameter("@IMG_extn", e.File.FileName.Split('.').GetValue(1).ToString()));
                    comm.Parameters.Add(new SqlParameter("@link_url", folderUrl + "/"));
                    comm.Parameters.Add(new SqlParameter("@creat_emp", Convert.ToString(Session["TripName"])));
                    SqlDataReader reader = comm.ExecuteReader();
                    if (reader.Read())
                    {
                        e.File.SaveAs(Server.MapPath(@"~\" + reader["link_url"].ToString()));
                    }
                    reader.Close();
                    comm.Dispose();
                }
                finally
                {
                    connect.Close();
                }
                HttpContext.Current.Response.StatusCode = 550;
            }
            else
            {
                FolderID = HttpContext.Current.Request.Form["FolderID"];
                string strSql = "insert into trimgfile (";
                strSql += "img_id,folder_id,img_DR,IMG_extn,link_url";
                strSql += ",upd_dtm,creat_emp,creat_DTM";
                strSql += ")";
                strSql += " select right('00000000'+cast((isNull(right(max(img_id),8),0)+1) as varchar),8) ,@FolderID,@img_DR,@IMG_extn,@link_url + right('00000000'+cast((isNull(right(max(img_id),8),0)+1) as varchar),8) +'.'+ @IMG_extn,getdate(),@creat_emp,getdate() from trimgfile";
                strSql += " select top 1  img_id ,link_url  from trimgfile order by img_id desc";
                string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
                SqlConnection connect = new SqlConnection(strConnString);
                try
                {
                    connect.Open();
                    SqlCommand comm = new SqlCommand(strSql, connect);
                    comm.Parameters.Add(new SqlParameter("@img_DR", e.File.FileName.Split('.').GetValue(0).ToString()));
                    comm.Parameters.Add(new SqlParameter("@FolderID", FolderID == "top" ? "" : FolderID));
                    comm.Parameters.Add(new SqlParameter("@IMG_extn", e.File.FileName.Split('.').GetValue(1).ToString()));
                    comm.Parameters.Add(new SqlParameter("@link_url", folderUrl + "/"));
                    comm.Parameters.Add(new SqlParameter("@creat_emp", Convert.ToString(Session["TripName"])));
                    SqlDataReader reader = comm.ExecuteReader();
                    if (reader.Read())
                    {
                        e.File.SaveAs(Server.MapPath(@"~\" + reader["link_url"].ToString()));
                    }
                    reader.Close();
                    comm.Dispose();
                }
                finally
                {
                    connect.Close();
                }
            }
        }
        else
        {
            HttpContext.Current.Response.Write("QQ");
        }
    }

    protected void FileUploader1_dispose(object sender, com.flajaxian.FileReceivedEventArgs e)
    {
        //for (int ii = 0; ii <= intItem * 10000; ii++)
        //{ /* 暫停時間使用 */ }
        Thread.Sleep(intItem * 10000);
        Response.Write("<script language='javascript' type='text/javascript'>alert('上傳成功'); window.location='filemanager.aspx?FolderID=';</script>");
    }
}
     