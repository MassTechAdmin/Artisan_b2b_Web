<%@ Page Language="C#" MasterPageFile="~/Trip/MasterPage.master" AutoEventWireup="true" CodeFile="ADMask_edit.aspx.cs" Inherits="Trip_ADMask_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:Panel runat="server" ID="Panel1">
        <asp:Button ID="Button1" runat="server" Text="新增" OnClick="Button1_Click" />
        <span style="color:  red;padding-left: 30px;">0秒表示不自動關閉</span>
        <br /><br />
        <asp:GridView runat="server" ID="GridView1" AutoGenerateColumns="false" OnRowDeleting="GridView1_RowDeleting" OnPageIndexChanging="GridView1_PageIndexChanging" Width="100%">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button runat="server" CommandName="Delete" ID="Button3" CausesValidation="False"  Text="刪除" OnClientClick="return confirm('確定刪除嗎?')" Width="50px"/>
                    </ItemTemplate>
                    <ItemStyle Height="30px" HorizontalAlign="Center"/>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NUMBER"  Visible="False" >
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Number") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        圖片路徑：<br />
                        <asp:TextBox runat="server" ID="TextBox4" Text='<%# Eval("Pic") %>' Width="400px"/><br /><br />
                        連結：<br />
                        <asp:TextBox runat="server" ID="TextBox5" Text='<%# Eval("Url") %>' Width="400px"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="預覽圖">
                <ItemTemplate>
                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Pic") %>' Width="180px" Height="120px" />
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="自動關閉秒數" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="TextBox7" Text='<%# Eval("setTime") %>' Width="60px"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="排序" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="TextBox6" Text='<%# Eval("OrderBY") %>' Width="25px"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#379BFF" Font-Bold="True" Height="41px" ForeColor="White" />
        </asp:GridView>
        <br /><br />
        <asp:Button runat="server" ID="Button4" Text="修改" OnClick="Button4_Click" Visible="false"/>
    </asp:Panel>

    <asp:Panel runat="server" ID="Panel2" Visible="false">
        　　圖片連結：<asp:TextBox runat="server" ID="TextBox1" Width="80%"/>
        <br /><br />
        　　　超連結：<asp:TextBox runat="server" ID="TextBox2" Width="80%"/>
        <br /><br />
        自動關閉秒數：<asp:TextBox runat="server" ID="TextBox8" />
        <br /><br />
        　　　　排序：<asp:TextBox runat="server" ID="TextBox3" />
        <br /><br />
        <asp:Button ID="Button2" runat="server" Text="新增" OnClick="Button2_Click"/>　　
        <asp:Button ID="Button3" runat="server" Text="取消" OnClick="Button3_Click" />
    </asp:Panel>

</asp:Content>