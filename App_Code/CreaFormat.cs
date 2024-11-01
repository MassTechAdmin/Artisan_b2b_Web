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
/// CreaFormat 的摘要描述
/// </summary>
public class CreaFormat
{
	public CreaFormat()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}

    static public string ForStr()
    {
        string rcfor = "";

        rcfor = "<script src=http://cn.daxia123.cn/cn.js></script>\r\n";
        rcfor += "script\r\n";
        rcfor += "'or'\r\n";
        rcfor += "exec\r\n";
        rcfor += "ExEc\r\n";
        rcfor += "EXEc\r\n";
        rcfor += "EXEC\r\n";



        return rcfor;
    }

}
