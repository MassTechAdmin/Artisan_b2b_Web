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

public partial class folder_FolderDelete : System.Web.UI.Page
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
                case "FolderDelete"://刪除資料夾
                    if (FolderID == "")
                    { Response.Write("<script language='javascript' type='text/javascript'>alert('你刪除的東西好像有點問題喔！'); history.back();</script>"); }
                    else
                    {
                        checkLU1(FolderID);
                    }
                    break;
                case "ImgDelete"://刪除圖片
                    if (IMGID == "")
                    { Response.Write("<script language='javascript' type='text/javascript'>alert('你刪除的東西好像有點問題喔！'); history.back();</script>"); }
                    else
                    {
                        checkLU2(IMGID);
                    }
                    break;
                default:
                    Response.Write("<script language='javascript' type='text/javascript'>alert('有點問題喔！'); history.back();</script>");
                    break;
            }

        }
        else
        {
            Response.Write("<script language='javascript' type='text/javascript'>alert('有點問題喔！'); history.back();</script>");
        }
    }
    private void checkLU1(string FolderID)//檢查資料夾
    {
        bool check = true;
        string strSql = "select count(TRIMGFILE.Folder_ID) as lu1 , folder_nm  ";
        strSql += " ,(select count(1) from trimgfdb where pdfolder_id = @FolderID ) as foldercount  ";//計算資料夾數量
        strSql += " from trimgfDb  ";
        strSql += " left join TRIMGFILE on TRIMGFILE.LINK_URL like '%/' + TRIMGFDB.Folder_ID + '/%'";//判斷路徑來計算檔案數量
        strSql += " where trimgfdb.folder_id = @FolderID";
        strSql += " group by folder_nm ";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            //comm.Parameters.Add(new SqlParameter("@folder_nm", TxtFolderNM.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@FolderID", FolderID ));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                if (Convert.ToInt32(reader["lu1"].ToString()) > 0)
                {
                    Response.Write("<script language='javascript' type='text/javascript'>alert('資料夾裡面還有圖片！'); history.back();</script>");
                    check = false;
                }
                if (Convert.ToInt32(reader["foldercount"].ToString()) > 0)
                {
                    Response.Write("<script language='javascript' type='text/javascript'>alert('資料夾裡面還有其他資料夾！'); history.back();</script>");
                    check = false;
                }
            }
            else
            {
                Response.Write("<script language='javascript' type='text/javascript'>alert('無此資料夾！'); history.back();</script>");
                check = false;
            }
        }
        catch
        {
            Response.Write("<script language='javascript' type='text/javascript'>alert('網站或資料庫維護中，請稍侯在試！'); history.back();</script>");
            check = false;
        }
        finally
        {
            connect.Close();
        }
        if (check == true)
        {
            LU1(FolderID);
        }
        //else
        //{
        //    Response.Write("<script language='javascript' type='text/javascript'>alert('資料夾有問題喔，請稍侯在試！'); history.back();</script>");
        //}
    }
    private void LU1(string FolderID)//刪除資料夾
    {
        string pdfolder_id = "";
        string folder = FolderID;
        string strSql = "select pdfolder_id from trimgfdb where folder_id = @FolderID";//尋找上層資料夾
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            //comm.Parameters.Add(new SqlParameter("@folder_nm", TxtFolderNM.Text.Trim()));
            comm.Parameters.Add(new SqlParameter("@FolderID", FolderID));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                pdfolder_id = reader["pdfolder_id"].ToString();
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
                Response.Write("<script language='javascript' type='text/javascript'>alert('網站或資料庫維護中，請稍侯在試！'); history.back();</script>");
            }
            finally
            {
                connect1.Close();
            }
        }
        strSql = "delete trimgfdb where folder_id = @folder_id ";
        string strConnString2 = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect2 = new SqlConnection(strConnString2);
        try
        {      
            connect2.Open();
            SqlCommand comm2 = new SqlCommand(strSql, connect2);
            comm2.Parameters.Add(new SqlParameter("@folder_id", folder));
            comm2.ExecuteNonQuery();
            comm2.Dispose();
        }
        catch
        {
            Response.Write("<script language='javascript' type='text/javascript'>alert('網站或資料庫維護中，請稍侯在試！'); history.back();</script>");
        }
        finally
        {
            connect2.Close();
            Response.Write("<script language='javascript' type='text/javascript'>alert('刪除成功！'); window.location='fileManager.aspx?FolderID=" + pdfolder_id + "';</script>");
        }
        if (Directory.Exists(Server.MapPath(@"~/upload" + folderUrl)) == true)//資料夾存在才刪除
        {
            Directory.Delete(Server.MapPath(@"~/upload" + folderUrl));
        }
        
    }

    private void checkLU2(string IMGID)
    {
        bool check = true;
        string FolderID = "";
        string strSql = "select folder_id from trimgfile where img_id = @IMGID ";
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
                FolderID = reader["Folder_id"].ToString();
            }
            else
            {
                Response.Write("<script language='javascript' type='text/javascript'>alert('無此相片！'); history.back();</script>");
                check = false;
            }
        }
        catch
        {
            Response.Write("<script language='javascript' type='text/javascript'>alert('網站或資料庫維護中，請稍侯在試！'); history.back();</script>");
            check = false;
        }
        finally
        {
            connect.Close();
        }
        if (check == true)
        {
            LU2(IMGID,FolderID);
        }
    }
    private void LU2(string IMGID, string FolderID)
    {
        string strSql = "select Link_url from trimgfile where img_id =@IMGID delete trimgfile where img_id =@IMGID";
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
                string LINK = reader["Link_url"].ToString();
                File.Delete(Server.MapPath("~/") + LINK);
            }
        }
        catch
        {
            Response.Write("<script language='javascript' type='text/javascript'>alert('網站或資料庫維護中，請稍侯在試！'); history.back();</script>");
        }
        finally
        {
            connect.Close();
            Response.Write("<script language='javascript' type='text/javascript'>alert('刪除成功！'); window.location='fileManager.aspx?FolderID=" + FolderID + "';</script>");
            Session["TripName"] = Convert.ToString(Session["TripName"]);
        }
    }
}
