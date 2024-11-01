using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

/// <summary>
/// TripDB 的摘要描述
/// </summary>
public class TripDB
{
	public TripDB()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}

    public static OleDbConnection CreateConnection1()
    {
        string strConnection = "provider=SQLOledb;Data Source=210.200.219.246,1902;Initial Catalog=B2B;Persist Security Info=True;User ID=sa;Password=joetime";
        //string strConnection = "provider=SQLOledb;Data Source=127.0.0.1;Initial Catalog=B2B;Persist Security Info=True;User ID=sa;Password=aaaa0000!";
        //string strConnection = "provider=SQLOledb;Data Source=220.130.163.148;Initial Catalog=Trip;Persist Security Info=True;User ID=sa;Password=joetime";
        //string strConnection = "provider=SQLOledb;Data Source=127.0.0.1;Initial Catalog=Trip;Persist Security Info=True;User ID=sa;Password=123456";

        OleDbConnection con = new OleDbConnection(strConnection);
        return con;
    }

    public static OleDbConnection CreateConnection()
    {
        string strConnection = "provider=SQLOledb;Data Source=210.200.219.246,1902;Initial Catalog=Trip;Persist Security Info=True;User ID=sa;Password=joetime";
        //string strConnection = "provider=SQLOledb;Data Source=127.0.0.1;Initial Catalog=Trip;Persist Security Info=True;User ID=sa;Password=aaaa0000!";
        //string strConnection = "provider=SQLOledb;Data Source=220.130.163.148;Initial Catalog=Trip;Persist Security Info=True;User ID=sa;Password=joetime";
        //string strConnection = "provider=SQLOledb;Data Source=127.0.0.1;Initial Catalog=Trip;Persist Security Info=True;User ID=sa;Password=123456";

        OleDbConnection con = new OleDbConnection(strConnection);
        return con;
    }

    public static OleDbConnection CreateConnectionCounter()
    {
        string strConnection = "provider=SQLOledb;Data Source=210.200.219.246,1902;Initial Catalog=artisan;Persist Security Info=True;User ID=sa;Password=joetime";
        //string strConnection = "provider=SQLOledb;Data Source=127.0.0.1;Initial Catalog=Trip;Persist Security Info=True;User ID=sa;Password=aaaa0000!";
        //string strConnection = "provider=SQLOledb;Data Source=220.130.163.148;Initial Catalog=artisan;Persist Security Info=True;User ID=sa;Password=joetime";
        //string strConnection = "provider=SQLOledb;Data Source=127.0.0.1;Initial Catalog=masscounter;Persist Security Info=True;User ID=sa;Password=123456";

        OleDbConnection con = new OleDbConnection(strConnection);
        return con;
    }
}
