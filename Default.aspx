<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default_b2b" %>
    <%@ Register Src="WebControl/Foot_19.ascx" TagName="Foot" TagPrefix="uc2" %>
        <%@ Register Src="WebControl/Header_Menu_19.ascx" TagName="Header" TagPrefix="uc1" %>

            <!DOCTYPE html
                PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
            <html xmlns="http://www.w3.org/1999/xhtml">

            <head runat="server">
                <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
                <meta content="width=device-width, initial-scale=1, user-scalable=0" name="viewport">
                <title>凱旋旅行社(巨匠旅遊)同業網</title>
                <link rel="stylesheet" href="css/layout_b2b.css">
                <script type="text/javascript" src="js/jquery-2.1.3.min.js"></script>
                <script type="text/javascript" src="js/b2b.js"></script>
            </head>

            <body>

                <form id="form1" runat="server">
                    <div id="wrapper">
                        <!--天開始-->
                        <!--替換b2b天-->
                        <header>
                            <nav>
                                <uc1:Header ID="Header_Menu1" runat="server" />
                            </nav>
                        </header>
                        <!--天結束-->

                        <div id="b2b-content-default">
                            <div id="b2b-login">
                                <div class="login-box">
                                    <asp:Panel ID="Panel1" runat="server" DefaultButton="ImageButtonLogin">
                                        <table>
                                            <tr>
                                                <td colspan="2" valign="top">
                                                    <asp:Image runat="server" src="images/b2b_login_title.png" alt="" class="login-title" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="login-title">帳號：</td>
                                                <td>
                                                    <asp:TextBox ID="TextBox1" runat="server" class="login-textBox" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="login-title">密碼：</td>
                                                <td>
                                                    <asp:TextBox ID="TextBox2" runat="server" class="login-textBox" TextMode="Password" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="login-title">驗證碼：</td>
                                                <td valign="bottom">
                                                    <asp:TextBox ID="TextBox3" runat="server" MaxLength="5" class="login-textBox login-textBox-code" size="12" />
                                                    <img id="MzImgExpPwd" class="login-box-code" align="absmiddle" name="MzImgExpPwd" onclick="javascript:document.getElementById('MzImgExpPwd').src='gif.aspx?temp='+ (new Date().getTime().toString(36));  return false" src="gif.aspx" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">看不清楚？請重新點擊圖</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <span class="login_box_red"><a href="A_Join.aspx">申請帳號</a> | <a
                                                            href="forgetPW.aspx">忘記密碼</a></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Button ID="ImageButtonLogin" runat="server" Text="登入" OnClick="ImageButtonLogin_Click" class="login_box_btn" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                                <div class="login-info">
                                    巨匠旅遊B2B2C同業專業旅遊網站 <span style="font-size:14px;">正式開放申請</span><br> 委由
                                    <span class="info-link-name"><strong>麥斯科技</strong></span>處理網站架設與所有相關維護<br> 網站功能: 1.專屬網站. 2.行程同步. 3.網站維護.<br> 全頁版型:
                                    <!--<span class="info-link"><a href="http://sample_a.grp.com.tw/" target="_blank">A版型</a></span>、
                    <span class="info-link"><a href="http://sample_b.grp.com.tw/" target="_blank">B版型</a></span>、
                    <span class="info-link"><a href="http://sample_d.grp.com.tw/" target="_blank">D版型</a></span>、
                    <span class="info-link"><a href="http://sample_e.grp.com.tw/" target="_blank">E版型</a></span>、
                    <span class="info-link"><a href="http://sample_f.grp.com.tw/" target="_blank">F版型</a></span>、
                    <span class="info-link"><a href="http://sample_i.grp.com.tw/" target="_blank">I版型</a></span>、
                    <span class="info-link"><a href="http://sample_j.grp.com.tw/" target="_blank">J版型</a></span>、-->
                                    <span class="info-link"><a href="http://sample_rwd_a.grp.com.tw/"
                                            target="_blank">RWDA</a></span>、
                                    <span class="info-link"><a href="http://sample_rwd_b.grp.com.tw/"
                                            target="_blank">RWDB</a></span>、
                                    <span class="info-link"><a href="http://sample_rwd_c.grp.com.tw/"
                                            target="_blank">RWDC</a></span>、
                                    <span class="info-link"><a href="http://sample_rwd_d.grp.com.tw/"
                                            target="_blank">RWDD</a></span>、
                                    <span class="info-link"><a href="http://sample_rwd_e.grp.com.tw/"
                                            target="_blank">RWDE</a></span>、
                                    <span class="info-link"><a href="http://sample_rwd_f.grp.com.tw/"
                                            target="_blank">RWDF</a></span>、
                                    <span class="info-link"><a href="http://sample_rwd_g.grp.com.tw/"
                                            target="_blank">RWDG</a></span>
                                    <br> 單頁版型:
                                    <!-- <span class="info-link"><a href="http://sample.grp.com.tw/single_0/" target="_blank">範例</a></span>、 -->
                                    <span class="info-link"><a href="http://sample_rwd_0.grp.com.tw/"
                                            target="_blank">RWD單頁</a></span>
                                    <br> 申請方式: <span class="info-link"><a
                                            href="http://b2b.artisan.com.tw/B2B2C/001.html"
                                            target="_blank">詳情簡介</a></span><br> 電話:02-2506-3038 或將貴公司的聯絡資料E-MAIL至麥斯科技
                                </div>
                            </div>

                            <div id="b2b-bannerBar" style='display:none;'>
                                <asp:Literal ID="litAds" runat="server"></asp:Literal>
                            </div>

                            <div id="b2b-link" style='display:none;'>
                                <div class="b2b-vision">
                                    <h1></h1>
                                    <ul>
                                        <asp:Literal runat="server" ID="litVision" />
                                    </ul>
                                </div>
                                <div class="b2b-exceptional">
                                    <div class="b2b-exceptional-left">
                                        <h1></h1>
                                        <asp:Literal runat="server" ID="litExImg" />
                                        <div class="b2b-exceptional-button">
                                            <asp:Literal runat="server" ID="litSpan" />
                                        </div>
                                    </div>
                                    <div class="b2b-exceptional-right">
                                        <asp:Literal runat="server" ID="litExData" />
                                    </div>
                                </div>
                            </div>

                            <div class="DMBanner_tool" style="display:none;">
                                <asp:Literal runat="server" ID="litDM_banner" />
                            </div>
                        </div>


                        <!--地開始-->
                        <!--替換b2b地-->
                        <uc2:Foot ID="Foot1" runat="server" />
                        <!--地結束-->
                    </div>
                    <script type="text/javascript" src="js/jquery.elastislide.js"></script>
                    <script type="text/javascript">
                        $('#b2b-bannerBar').elastislide({
                            imageW: 260,
                            onClick: true,
                        });

                        $(window).on('load', function() {
                            // $('.loading_mask').fadeOut(300);
                            //公告圖
                            var nowDate = new Date();
                            //公告關閉時間
                            //月份從0開始
                            var endDate = new Date(2021, 4, 13);
                            if ((endDate - nowDate) > 0) {
                                $('.billboard').fadeIn(500);
                                $(".billboard_close,.billboard").on('click', function() {
                                    $('.billboard').fadeOut(500);
                                });
                            }
                        });
                    </script>
                </form>
            </body>

            </html>