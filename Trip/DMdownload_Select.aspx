﻿<%@ Page Language="C#" MasterPageFile="~/Trip/MasterPage.master" AutoEventWireup="true" CodeFile="DMdownload_Select.aspx.cs" Inherits="DMdownload_Select" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
         <asp:Button ID="Button1" runat="server" Text="新增" OnClick="Button1_Click1" /><br />
         <asp:Label ID="Label20" runat="server" Text="請選取地區:"></asp:Label>
         <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" style="height: 19px" Font-Size="Small">
         </asp:DropDownList>
         <br />
         <asp:Label ID="Label21" runat="server" Text="請選取國家:" Visible="False"></asp:Label>
         <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" Visible="False" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
         </asp:DropDownList>
         <br />
         
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand" OnPageIndexChanging="GridView1_PageIndexChanging">
           <RowStyle BackColor="#B1CCE7" ForeColor="#333333" />
             <Columns>
                <asp:ButtonField Text="編輯"  CommandName="SingleClick" ButtonType="Button" Visible="True"/>
                <asp:TemplateField HeaderText="NUMBER"  Visible="False" >
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("SN") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>               
                <asp:TemplateField HeaderText="地區" >
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Width="45px" Text='<%# Eval("Area_Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DM標題" >
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Width="400px" Text='<%# Eval("DM_Title") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="檔案名稱" >
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("DM_File") %>'></asp:Label>
                        <%--<asp:Image ID="Image1" runat="server" ImageUrl='<%# "/Zupload/DMdownload/" + Eval("DM_File") %>' Width="200px" />--%>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="到期日" >
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Width="100px" Text='<%# Eval("DM_EndDate","{0:yyyy/MM/dd}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
             <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
         </asp:GridView>
   </asp:Content>
