<%@ Page Language="C#" %>

<%@ Register Src="../../WebControl/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>

<%@ Register Src="../../WebControl/Header.ascx" TagName="Header" TagPrefix="uc1" %>

<%@ Register src="../../WebControl/Island_Tab.ascx" tagname="Island_Tab" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../../css/web.css" rel="stylesheet" type="text/css" />
    <link href="../../css/color.css" rel="stylesheet" type="text/css" />
    <script src="../../js/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="../../js/Artisan_SubMenu.js" type="text/javascript"></script>
    <link href="../../css/Artisan_SubMenu.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
function SetCwinHeight()
{
var iframeid=document.getElementById("mainframe"); //iframe id
  if (document.getElementById)
  {   
   if (iframeid && !window.opera)
   {   
    if (iframeid.contentDocument && iframeid.contentDocument.body.offsetHeight)
     {   
       iframeid.height = iframeid.contentDocument.body.offsetHeight;   
     }else if(iframeid.Document && iframeid.Document.body.scrollHeight)
     {   
       iframeid.height = iframeid.Document.body.scrollHeight;   
      }   
    }
   }
}
</script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">
        <uc1:Header ID="Header1" runat="server" />
        <div id="content">
            <iframe src="../../island/001.aspx" name="mainframe" width="100%" marginwidth="0" marginheight="0" onload="Javascript:SetCwinHeight()"  scrolling="No" frameborder="0" id="mainframe" ></iframe> 
        </div>
        <uc2:Foot ID="Foot1" runat="server" />
    </div>
    </form>
</body>
</html>
