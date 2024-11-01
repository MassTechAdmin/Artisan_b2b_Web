using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class testcookie : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        
        HttpCookie cookie = new HttpCookie("name");
        cookie.Value = "value";
        Response.AppendCookie(cookie);

        cookie = new HttpCookie("name");
        cookie.Value = "value1";
        Response.AppendCookie(cookie);

        // Response.SetCookie 更新Cookies集合中第一個同key的cookie，如果找不到同key的cookie，則添加一個cookie到Cookies集合中。
        cookie = new HttpCookie("name1");
        cookie.Value = "value1";
        Response.SetCookie(cookie);

        // Response.Cookies.Add 同key的cookie可以重複添加
        cookie.Value = "value2";
        Response.SetCookie(cookie);
        Response.Cookies.Add(cookie);

        cookie = new HttpCookie("name");
        cookie.Value = "value3";
        Response.Cookies.Add(cookie);

        cookie = new HttpCookie("name");
        cookie.Value = "value4";
        Response.Cookies.Add(cookie); //Response.Flush();//取消注釋後面代碼將會引發異常

        cookie = new HttpCookie("name");
        cookie.Value = "value5";
        Response.Cookies.Add(cookie);


        //cookie = new HttpCookie("name");
        //cookie.Value = "value1";
        //Response.AppendCookie(cookie);

        //cookie = new HttpCookie("name");
        //cookie.Value = "value5";
        //Response.SetCookie(cookie); //注意這裡儘管它在最後，影響的卻只是cookies集合中第一個key為“name”的cookie.
    }
}