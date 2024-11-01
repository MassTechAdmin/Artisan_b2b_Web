<%@ Page Language="C#" MasterPageFile="~/trip/MasterPage.master" AutoEventWireup="true" CodeFile="FileManager.aspx.cs" Inherits="floder_FileManager"  %>

    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:Literal ID="Lithead" runat="server"></asp:Literal>
    <table width="770" height="28" border="1"   cellpadding="0" cellspacing="0" bordercolor="#CCCCCC" style="font-size:15px;" rules="cols">
    <tr>
    <td width="20%" height="28" bgcolor="#7e7e7e" class="style_tablefont" style="color: #FFF;">目前目錄資訊</td>
    <asp:Literal ID="LitThis" runat="server"></asp:Literal><td width="16%" align="right">
        <asp:Button ID="AddFolder"  runat="server" Text="新增資料夾" 
            onclick="AddFolder_Click" />
    </td>
    </tr>
    </table>
    <br />
    <asp:Literal ID="LitShow" runat="server"></asp:Literal>
    <asp:GridView ID="GridView1" runat="server" Width="770" BackColor="White" BorderColor="#CCCCCC"  OnRowCreated="GridView1_RowCreated"   OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand"
        BorderStyle="None" BorderWidth="1px" CellPadding="3"  GridLines="Both"   OnPageIndexChanging="GridView1_PageIndexChanging" OnSorting="GridView1_Sorting" AllowPaging="True" AllowSorting="True" PageSize="30"  >
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black"  />
        <RowStyle BackColor="White" ForeColor="Gray" />
        <Columns>
            <asp:ButtonField Text="SingleClick"  CommandName="SingleClick" Visible="False"/>
            <asp:TemplateField HeaderText="資料夾編號" Visible="False" SortExpression="資料夾編號">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Visible="false" Text='<%# Eval("編號") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="下層目錄"  SortExpression="下層目錄">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server"  Text='<%# Eval("下層目錄") %>'></asp:Label>
                </ItemTemplate>
                  <HeaderStyle Width="70%" />
            </asp:TemplateField>
              <asp:TemplateField HeaderText="圖片數"   SortExpression="圖片數">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server"  Text='<%# Eval("圖片數") %>'></asp:Label>
                </ItemTemplate>
                  <HeaderStyle Width="15%" />
                  <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
                <asp:TemplateField  HeaderText="管理" SortExpression="編號" >
                <ItemTemplate>
                    <asp:Button  ID="Button1"  runat="server" Text="修改"  Visible ="true"  PostBackUrl='<%# "FolderEdit.aspx?FolderID="+ Eval("編號") + "&Fun=FolderEdit" %>'  />
                    <asp:Button  ID="Button2"  runat="server" Text="刪除"  Visible ="true" PostBackUrl='<%# "FolderDELETE.aspx?FolderID="+ Eval("編號") + "&Fun=FolderDelete" %>'  OnClientClick="if(!confirm('確定要刪除嗎'))return false;"  /> 
                </ItemTemplate>
                    <HeaderStyle Width="15%" />
                    <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="#000222" />
        <HeaderStyle BackColor="#484848" Font-Size="15px" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#EEEEEE" />
    </asp:GridView>
    <br />
    <asp:Panel ID="Panel1" Visible="false" runat="server" >
        <div style="float: left;">
            <asp:Button ID="BtnPDfolder" runat="server" Text="上一層目錄" onclick="BtnPDfolder_Click" /> &nbsp;
            <asp:Button ID="BtnAddIMG" runat="server" Text="新增圖片" onclick="BtnAddIMG_Click" />
        </div>
        <div style="float: right;">
            圖片名稱：<asp:TextBox ID="txbSearch" runat="server"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="搜尋" OnClick="btnSearch_Click" />
        </div>
    </asp:Panel>
    <asp:GridView ID="GridView2" runat="server" BackColor="White" OnRowCreated="GridView2_RowCreated"
        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="800" Font-Size="15px"
        GridLines="Both" OnPageIndexChanging="GridView2_PageIndexChanging" 
        OnRowCommand="GridView2_RowCommand" OnRowDataBound="GridView2_RowDataBound" 
        OnSorting="GridView2_Sorting" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="50">
        <RowStyle BackColor="White" ForeColor="#000222" />
        <Columns>
            <asp:ButtonField CommandName="SingleClick" Text="SingleClick" Visible="False" />
            <asp:TemplateField HeaderText="編號" SortExpression="編號" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("編號") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  >
                <headertemplate> 
                    <asp:CheckBox ID="CheckAll1" runat="server" onclick="javascript: SelectAllCheckboxes(this);" ToolTip="按一次全選，再按一次取消全選" /> 
                </headertemplate>
                <itemtemplate> 
                    <asp:CheckBox ID="CheckBox2" runat="server" Text=""/> 
                </itemtemplate>
                <HeaderStyle Width="5%" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField> 
            <asp:TemplateField HeaderText="圖片名稱"  SortExpression="圖片名稱" >
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server"  Text='<%# Eval("圖片名稱") %>' ></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="30%" />
                <ItemStyle HorizontalAlign="left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="圖片"  >
                <ItemTemplate>
                    <asp:Image  ID="Image1" ImageUrl='<%# Eval("路徑") %>' Width="200px" runat="server" />
                    <asp:Label  ID="LBurl" runat="server" Visible="false"  Text='<%# Eval("路徑") %>' ></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="10%" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="圖片路徑" SortExpression="圖片路徑" >
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("圖片路徑") %>' ></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="45%" />
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="管理" SortExpression="編號" >
                <ItemTemplate>
                    <asp:Button  ID="Button1"  runat="server" Text="修改"  Visible ="true"  PostBackUrl='<%# "FolderEdit.aspx?IMGID="+ Eval("編號") + "&Fun=ImgEdit" %>'  /><br /><br />
                    <asp:Button  ID="Button2"  runat="server" Text="刪除"  Visible ="true" PostBackUrl='<%# "FolderDELETE.aspx?IMGID="+ Eval("編號") + "&Fun=ImgDelete" %>'  OnClientClick="if(!confirm('確定要刪除嗎'))return false;"  /> 
                </ItemTemplate>
                <HeaderStyle Width="10%" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#484848" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#EEEEEE" />
    </asp:GridView>
    <asp:Literal ID="LitIMG" runat="server"></asp:Literal>
    <asp:Button ID="BtnDelete" runat="server" Text="刪除勾選圖片" Visible="false" style="margin-right:50%;margin-Left:50%;" OnClientClick="if(!confirm('確定要刪除嗎'))return false;"  onclick="BtnDelete_Click" />
<script type="text/javascript">
    function SelectAllCheckboxes(spanChk)
    {
        elm=document.forms[0];
        for(i=0;i<= elm.length -1;i++)
        {
            if(elm[i].type=="checkbox" && elm[i].id!=spanChk.id)
            {
                if(elm.elements[i].checked!=spanChk.checked)
                    elm.elements[i].click();
            }
        }
    }
</script> 
</asp:Content>
