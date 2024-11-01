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
/// opFormat 的摘要描述
/// </summary>
public class opFormat
{
	public opFormat()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}


    public static string Replace(object input)
    {
        string data = input.ToString();
        data = data.Replace("&amp;", "&");
        data = data.Replace("&quot;", "/");
        data = data.Replace("&qapos;", "'");

        return data;
    }


}
