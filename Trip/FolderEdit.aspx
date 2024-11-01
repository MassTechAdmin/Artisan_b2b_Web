<%@ Page Language="C#" MasterPageFile="~/trip/MasterPage.master" AutoEventWireup="true" CodeFile="FolderEdit.aspx.cs" Inherits="folder_FolderEdit" %>

 <%@ Register TagPrefix="fjx" Namespace="com.flajaxian" Assembly="com.flajaxian.FileUploader" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>未命名頁面</title>
    
</head>
<body onload="addFileUploaderState()" >
    <form id="form1"   runat="server">--%>
    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function FileStateChanged(uploader, file, httpStatus, isLast) {
            var url=window.location.toString();
            var str="";
            var str_value="";
            if(url.indexOf("?")!=-1){
                var ary=url.split("?")[1].split("&");
                for(var i in ary){
                    str=ary[i].split("=")[0];
                    if (str == "FolderID") {
                        str_value = decodeURI(ary[i].split("=")[1]);
                        //Response.Write(i + " ");
                        //alert(i);
                    }
                }
            }

            sleep(500); //1000=1秒
            Flajaxian.fileStateChanged(uploader, file, httpStatus, isLast);

            if (file.state > Flajaxian.File_Uploading && isLast) {
                //alert('上傳成功');
                document.location.href = "filemanager.aspx?FolderID="+str_value
            }
        }
        function sleep(milliseconds) {
          var start = new Date().getTime();
          for (var i = 0; i < 1e7; i++) {
            if ((new Date().getTime() - start) > milliseconds){
              break;
            }
          }
        }
    </script>
        <asp:Panel ID="PanFolder"  runat="server" Visible="false">
            <table><tr><td style="padding-left:200px">上層目錄資訊：<asp:Literal ID="Literal1" runat="server"></asp:Literal></td></tr>
        <tr><td>
        <table><tr><td>資料夾名稱：</td><td><asp:TextBox ID="TxtFolderNM" runat="server" Width="214px" MaxLength="50"></asp:TextBox></td></tr>
            <asp:Panel ID="Panel1" runat="server" Visible="false"><tr><td>資料夾路徑：</td><td>
                <asp:Label ID="LbUrl" runat="server" Text="Label"></asp:Label></td></tr></asp:Panel>
                <tr><td>
                    <asp:Button ID="BtnFolderAdd" runat="server" Text="新增" Visible="false"
                        onclick="BtnFolderAdd_Click" />&nbsp;<asp:Button ID="BtnFolderEdit" Visible="false"
                        runat="server" Text="確認" onclick="BtnFolderEdit_Click" />&nbsp;<asp:Button 
                        ID="BtnFolderCancel" runat="server" Text="返回" onclick="BtnFolderCancel_Click" />
                    </td></tr>
        </table>
        </td></tr>
        
        </table>
        
        </asp:Panel>
        
        <asp:Panel ID="PanImg" runat="server" Visible="false">
        
         <table ><tr><td style="padding-left:200px">目前目錄資訊：<asp:Literal ID="Literal2" runat="server"></asp:Literal></td></tr>
        <tr><td>
            <table>
                <asp:Panel ID="PanImgEdit" runat="server" Visible="false">
                    <tr>
                        <td>
                            圖片路徑：
                        </td>
                        <td>
                            <asp:Label ID="LbImgUrl" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            圖　　片：
                        </td>
                        <td>
                            <asp:Image ID="Image1" runat="server" Height="500" Width="500" />
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td>
                        圖片上傳：
                    </td>
                    <td>
                    <%--<input type="file" multiple="multiple" Visible="false" name="File1" id="File1" accept="image/*" />--%>
                       <asp:FileUpload ID="FileUpload1" AllowMultiple="true"  runat="server" />
                      <asp:Panel ID="PanFile" runat="server" Visible="false">
                        <fjx:FileUploader ID="FileUploader1" runat="server" JsFunc_FileStateChanged="FileStateChanged" UseInsideUpdatePanel="true" onfilereceived="FileUploader1_FileReceived" >
                            <Adapters>
                                <fjx:FileSaverAdapter />
                            </Adapters>
                        </fjx:FileUploader>
                        </asp:Panel>
                        <asp:Panel ID="Panel2" Visible="false" runat="server">
                        
                        <script type="text/javascript">
                        
    function addFileUploaderState(){
    var url=window.location.toString();
    var str="";
    var str_value="";
    if(url.indexOf("?")!=-1){
        var ary=url.split("?")[1].split("&");
        for(var i in ary){
            str=ary[i].split("=")[0];
            if (str == "FolderID") {
                str_value = decodeURI(ary[i].split("=")[1]);
            }
        }
    }
        window.setTimeout(function(){
            <%= FileUploader1.ClientID %>.setStateVariable("FolderID", str_value );
        }, 100);
    }
    if (window.addEventListener) window.addEventListener('onload', addFileUploaderState, false); 
    else if (window.attachEvent) window.attachEvent('onload', addFileUploaderState);
    

</script>
            </asp:Panel>            

                    </td>
                </tr>
                <asp:Panel ID="PanImgDR" runat="server" Visible="false">
                <tr>
                    <td>
                        圖片名稱：
                    </td>
                    <td>
                        <asp:TextBox ID="TxtImgDR" runat="server" Width="214px" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                </asp:Panel>
                <tr>
                    <td>
                        <asp:Button ID="BtnImgAdd" runat="server" Text="新增" Visible="false" 
                            onclick="BtnImgAdd_Click"  />&nbsp;<asp:Button
                            ID="BtnImgEdit" Visible="false" runat="server" Text="確認" 
                            onclick="BtnImgEdit_Click"  />&nbsp;<asp:Button
                                ID="BtnImgCancel" runat="server" Text="返回" 
                            onclick="BtnImgCancel_Click"  />
                    </td>
                </tr>
            </table>
        </td></tr>
        
        </table>
        
        
        </asp:Panel>
        </asp:Content>

  <%--  </form>
</body>
</html>
--%>