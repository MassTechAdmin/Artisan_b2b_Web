using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //getAdMask();
    }
    //#region === 大廣告 ===
    //private void getAdMask()
    //{
    //    litAD.Text = "";
    //    string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
    //    SqlConnection conn = new SqlConnection(strConnString);

    //    try
    //    {
    //        conn.Open();

    //        string strsql = "";
    //        strsql = " select top 10 * from AD_mask";
    //        strsql += " order by OrderBY,Number desc";

    //        SqlCommand comm = new SqlCommand(strsql, conn);
    //        SqlDataReader reader = comm.ExecuteReader();

    //        if (reader.HasRows)
    //        {
    //            litAD.Text += "<div class='pics'>";
    //            while (reader.Read())
    //            {

    //                litAD.Text += "<a href='" + reader["url"].ToString() + "'><img src='" + reader["pic"].ToString() + "' class='AD_img'></a>";

    //                //litAD.Text += "<script type='text/javascript'>$('#AD').slideUp(300).fadeOut(0);}, 10000);</script>";
    //                //adnum.Value = reader["setTime"].ToString() + "000";
    //            }
    //            litAD.Text += "</div>";
    //            //AD.Style["display"] = "";
    //        }

    //        comm.Dispose();
    //        reader.Close();
    //    }
    //    catch { }
    //    finally { conn.Close(); conn = null; }
    //}
    //#endregion
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    txtName.Text = "aaaa-" + DateTime.Now.ToString("HH:mm:ss");
    //}

    #region === 列印 ===
    public bool Printpdf(string fileName, string CompanyName, string ContactName, string ContactPhone, string ContactEmail)
    {
        // ****************************************************************************************************
        // PDF 產生新檔案的放置地方
        // 新增資料夾
        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/finalPDF/")))
        {
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/finalPDF/"));
        }

        string path = HttpContext.Current.Server.MapPath("~/finalPDF/");

        // ****************************************************************************************************
        // 新增分頁
        string sourcePdfPath = "http://www.artisan.com.tw/TripFile/" + fileName;
        string outputPdfPath = path + "/" + fileName;

        //FileStream fs = new FileStream(path + "/" + fileName, FileMode.Create, FileAccess.ReadWrite);
        //FileStream sourceDocumentStream = new FileStream(sourcePdfPath, FileMode.Open);
        //FileStream destinationDocumentStream = new FileStream(outputPdfPath, FileMode.Create, FileAccess.ReadWrite);
        //Document document = new Document();

        try
        {
            using (var destinationDocumentStream = new FileStream(outputPdfPath, FileMode.Create, FileAccess.ReadWrite))
            {
                PdfReader reader = new PdfReader(sourcePdfPath);
                PdfStamper stamper = new PdfStamper(reader, destinationDocumentStream);
                int total = reader.NumberOfPages + 1;
                stamper.InsertPage(total, PageSize.A4); //新增空白頁

                // 最後一頁加上公司資料
                BaseFont baseFont = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\msjh.ttc,0", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Rectangle pageSize = reader.GetPageSizeWithRotation(total);
                PdfContentByte pdfPageContents = stamper.GetOverContent(total); // 在內容上方加上浮水印
                                                                                //PdfContentByte pdfPageContents = stamper.GetUnderContent(i); // 在內容下方加上浮水印
                pdfPageContents.BeginText();
                pdfPageContents.SetFontAndSize(baseFont, 12);
                pdfPageContents.SetRGBColorFill(0, 0, 255);
                //pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_CENTER, CompanyName, (pageSize.Left + pageSize.Right) / 2, pageSize.GetTop(Utilities.MillimetersToPoints(10)), 0);
                pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_LEFT, CompanyName, pageSize.GetLeft(Utilities.MillimetersToPoints(80)), pageSize.GetTop(Utilities.MillimetersToPoints(10)), 0);
                pdfPageContents.EndText();

                pdfPageContents.BeginText();
                pdfPageContents.SetFontAndSize(baseFont, 12);
                pdfPageContents.SetRGBColorFill(0, 0, 255);
                pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ContactName, pageSize.GetLeft(Utilities.MillimetersToPoints(80)), pageSize.GetTop(Utilities.MillimetersToPoints(20)), 0);
                pdfPageContents.EndText();

                pdfPageContents.BeginText();
                pdfPageContents.SetFontAndSize(baseFont, 12);
                pdfPageContents.SetRGBColorFill(0, 0, 255);
                pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ContactPhone, pageSize.GetLeft(Utilities.MillimetersToPoints(80)), pageSize.GetTop(Utilities.MillimetersToPoints(30)), 0);
                pdfPageContents.EndText();

                pdfPageContents.BeginText();
                pdfPageContents.SetFontAndSize(baseFont, 12);
                pdfPageContents.SetRGBColorFill(0, 0, 255);
                pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ContactEmail, pageSize.GetLeft(Utilities.MillimetersToPoints(80)), pageSize.GetTop(Utilities.MillimetersToPoints(40)), 0);
                pdfPageContents.EndText();

                stamper.Close();
                reader.Close();
            }

            return true;
        }
        catch (Exception)
        {
            throw;
        }
        return false;

        //// ****************************************************************************************************
        //// 每一頁的右下方加上公司相關資訊。
        //try
        //{
        //    string sFileIn = "http://www.artisan.com.tw/TripFile/" + fileName;
        //    PdfReader reader = new PdfReader(sFileIn);
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        PdfStamper stamper = new PdfStamper(reader, ms);
        //        for (int i = 1; i <= reader.NumberOfPages; i++)
        //        {
        //            BaseFont baseFont = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\msjh.ttc,0", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        //            //iTextSharp.text.Font blue = new iTextSharp.text.Font(baseFont, 12);
        //            //Phrase p = new Phrase(CompanyName, blue);

        //            iTextSharp.text.Rectangle pageSize = reader.GetPageSizeWithRotation(i);
        //            PdfContentByte pdfPageContents = stamper.GetOverContent(i); // 在內容上方加上浮水印
        //            //PdfContentByte pdfPageContents = stamper.GetUnderContent(i); // 在內容下方加上浮水印
        //            pdfPageContents.BeginText();
        //            pdfPageContents.SetFontAndSize(baseFont, 12);
        //            pdfPageContents.SetRGBColorFill(0, 0, 255);
        //            pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, CompanyName, pageSize.GetRight(130), pageSize.GetBottom(25), 0);
        //            pdfPageContents.SetTextMatrix(pageSize.GetLeft(25), pageSize.GetBottom(30));
        //            pdfPageContents.EndText();

        //            pdfPageContents.BeginText();
        //            pdfPageContents.SetFontAndSize(baseFont, 12);
        //            pdfPageContents.SetRGBColorFill(0, 0, 255);
        //            pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, ContactName, pageSize.GetRight(40), pageSize.GetBottom(25), 0);
        //            pdfPageContents.EndText();

        //            pdfPageContents.BeginText();
        //            pdfPageContents.SetFontAndSize(baseFont, 12);
        //            pdfPageContents.SetRGBColorFill(0, 0, 255);
        //            pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, ContactPhone, pageSize.GetRight(130), pageSize.GetBottom(10), 0);
        //            pdfPageContents.EndText();

        //            pdfPageContents.BeginText();
        //            pdfPageContents.SetFontAndSize(baseFont, 12);
        //            pdfPageContents.SetRGBColorFill(0, 0, 255);
        //            pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, ContactEmail, pageSize.GetRight(40), pageSize.GetBottom(10), 0);
        //            pdfPageContents.EndText();
        //        }

        //        stamper.FormFlattening = true;
        //        stamper.Close();
        //        FileStream fs = new FileStream(path + "/" + fileName, FileMode.Create, FileAccess.ReadWrite);
        //        BinaryWriter bw = new BinaryWriter(fs);
        //        bw.Write(ms.ToArray());
        //        bw.Close();
        //        reader.Close();

        //        return true;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    HttpContext.Current.Response.Write("<script>alert('頁首頁尾錯誤！');history.go(-2);</script>");
        //}

        //return false;
    }
    #endregion

    protected void Button1_Click(object sender, EventArgs e)
    {
        string CompanyName_t = "公司名稱";    //公司名稱
        string ContactName_t = "聯絡人姓名";  //聯絡人姓名
        string ContactPhone_t = "聯絡人電話"; //聯絡人電話  
        string ContactEmail_t = "LINE ID";    //LINE ID

        string FileName = "0fcdd475-d303-49be-89c8-4bf8b59f1fdf.pdf";
        bool SuccessSignal = Printpdf(FileName, CompanyName_t, ContactName_t, ContactPhone_t, ContactEmail_t);
        ///////////////////////////////////////////////////////////////////////////
        if (SuccessSignal)
        {
            System.Net.WebClient wb = new System.Net.WebClient();
            //檔案路徑
            string link = HttpContext.Current.Server.MapPath("~/finalPDF/" + FileName);

            string strAttachmentFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + " " + "_01" + Path.GetExtension(FileName);

            Response.ClearHeaders();
            Response.Clear();
            Response.Expires = 0;
            Response.Buffer = true;
            Response.AddHeader("Accept-Language", "zh-tw");
            Response.AddHeader("Content-Disposition", "Attachment;FileName=" + strAttachmentFileName);
            Response.ContentType = "Application/pdf";

            Response.BinaryWrite(wb.DownloadData(link));
            string strSavePath = HttpContext.Current.Server.MapPath("~/finalPDF/");
            string[] files = Directory.GetFiles(strSavePath);

            foreach (string file in files)
            {
                System.IO.File.SetAttributes(file, FileAttributes.Normal);
                System.IO.File.Delete(file);
            }
            Response.End();
        }
        else
        {
            HttpContext.Current.Response.Write("<script>alert('頁首頁尾錯誤！');history.go(-2);</script>");
        }
    }
}