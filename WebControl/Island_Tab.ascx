<%@ Control Language="C#" ClassName="Island_Tab" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) { return; }

        string strURI = Request.Url.AbsoluteUri;
        if (strURI.IndexOf("/island/hotel/") > 0)
        { strURI = "../../ClassifyProduct.aspx?area=27&sgcn=142"; }
        else
        { strURI = "../ClassifyProduct.aspx?area=27&sgcn=142"; }

        Literal1.Text = "<a href=\"" + strURI + "\">";
        Literal1.Text += "<div class=\"island_menu_list\"><span class=\"island_exh_menu01_change\">帛琉</span><span class=\"island_exh_menu02\"></span><br />";
        Literal1.Text += "<span class=\"island_exh_menu03_change\">Palau</span></div></a>";
    }
</script>

<div id="island_menu_tool">
    <a href="../marldives.aspx">
    <div class="island_menu_list_change"><span class="island_exh_menu01_change">馬爾地夫</span><span class="island_exh_menu02"></span><br />
    <span class="island_exh_menu03_change">Maldives</span></div></a>
    <div class="island_menugrip01"><img src="../../images/exh/exh_menugrip01_right.jpg"/></div>
    <div class="island_menugrip01"></div>
    <div class="island_menugrip01"></div>


    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    <div class="island_menugrip01"><img src="http://www.artisan.com.tw/images/exh/exh_menugrip01.jpg"></div>
    <div class="island_menugrip01"></div>
    <div class="island_menugrip01"></div>
</div>
