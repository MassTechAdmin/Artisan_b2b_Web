<%@ Page Language="C#" AutoEventWireup="true" CodeFile="send_sms.aspx.cs" Inherits="send_sms" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <script type="text/javascript" src="../js/fancybox/jquery.min.js"></script>
    <script type="text/javascript" src="../js/fancybox/jquery.fancybox-1.3.4.js"></script>
    <link rel="stylesheet" type="text/css" href="../js/fancybox/jquery.fancybox-1.3.4.css" media="screen" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#Button1").click(function () {
                //window.parent.$.fancybox.close();
                $.fancybox.close();
//alert('4444');
                top.location = parent.location.href;
            })
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        <br /><br /><br />
        <input id="Button1" type="button" value="關閉" style='display:none;' />
    </div>
    </form>
</body>
</html>
