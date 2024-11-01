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
/// ClassFunction 的摘要描述
/// </summary>
public class ClassFunction
{
	public ClassFunction()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}


    public void CheckWebAddress()
    {
        string strCheckData = "'|ox|;|and|exec|insert|select|delete|update|count|*|%|chr|mid|master|truncate|char|declare|aspx|asp|php";
        string[] strSplitData = strCheckData.Split((char)'|');

        int urlCount = System.Web.HttpContext.Current.Request.QueryString.Keys.Count;
        string[] str = new string[urlCount];

        for (int i = 0; i <= urlCount - 1; i++)
        {
            str[i] = System.Web.HttpContext.Current.Request.QueryString.Get(i).ToString();
            for (int intIndex = 0; intIndex <= strSplitData.Length - 1; intIndex++)
            {
                if (str[i].Trim().ToLower().IndexOf(strSplitData[intIndex]) > 0)
                {
                    System.Web.HttpContext.Current.Response.Redirect("http://www.google.com/");
                    System.Web.HttpContext.Current.Response.End();
                }

            }
        }
    }


}
