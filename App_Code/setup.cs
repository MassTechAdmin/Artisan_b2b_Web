using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// setup 的摘要描述
/// </summary>
public class setup
{
    public string web_site = "http://www.artisan.com.tw/";//網址
    public string entity_directory = "d:/artisan/";
    public string pic_directory = "album_pic";//資料夾目錄
    public string pic_error = "../img/default.jpg";//預設圖片

    //目錄
    public string pic_path()
    {
        return web_site + "/" + pic_directory + "/";//網址目錄
    }
    public string pic_web_path()
    {
        return "../" + pic_directory + "/";//虛擬目錄
    }
    public string pic_entity()
    {
        return entity_directory + "/" + pic_directory + "/";//實體目錄
    }

	public setup()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}
}
