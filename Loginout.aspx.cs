using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Loginout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["PERNO"] = null; //帳號
        Session["Compno"] = null; //公司編號
        Session["PerName"] = null; //員工姓名
        Session["PerIDNo"] = null; //員工身份證字號

        Response.Redirect("Default.aspx");
    }
}