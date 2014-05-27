<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Password.aspx.cs" Inherits="Patient_Manage_Password" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>邵逸夫医院</title>
<style type="text/css">
* {
	font-size:14px;
	color:#333;
}
body {
	margin:0px;
	text-align:center;
}
ul {
	float:left;
	margin:0px;
	padding:0px;
}
li {
	float:left;
	list-style:none;
	border-bottom:1px solid #ccc;
	border-right:1px solid #ccc;
	height:24px;
	line-height:24px;
	padding-left:10px;
	overflow:hidden;
}
.clear {
	height:0px;
	clear:left;
	font-size:0px;
	line-height:0px;
}
.tits {
	height:36px;
	float:left;
	padding-left:10px;
	line-height:36px;
	font-size:16px;
	font-weight:bold;
	color:#333;
	border-bottom:1px solid #ccc;
	border-right:1px solid #ccc;
}
.txts {
	float:left;
	width:880px;
	padding:10px;
	text-align:left;
}
.txts div {
	width:870px;
	padding-left:10px;
	line-height:22px;
}
.ccbak{
	background-color:#eee;
}
.senb{
	background:#fff;
	color:#333;
}

</style>
</head>
<body style=" background-image:url(../../Image/bg.jpg);">
<div style="margin:auto; width:1000px;">
<div style="padding:0px 20px; float:left;">
	<div style="height:144px; padding:0px 30px; background:url(../../Image/manbg.jpg) no-repeat;"></div>
  <div style="padding:10px 10px; width:900px; padding:12px 30px; float:left; background:url(../../Image/mbg.jpg)">
    <div style=" margin:auto; height:100px; width:500px; border:1px solid #ccc;text-align:left; height:500px;">
    <form runat="server">
      <table>
        <tr>
            <td>原密码：</td>
            <td><input runat="server" type="password" id="tbox_oldPassword" /></td>
            <td></td>
        </tr>
        <tr>
            <td>新密码：</td>
            <td><input runat="server" type="password" id="tbox_newPassword1"  /></td>
            <td></td>
        </tr>
        <tr>
            <td>确认密码：</td>
            <td><input runat="server" type="password" id="tbox_newPassword2"  /></td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btn_changePsw" OnClick="btn_changePsw_Click" runat="server" Text="确定" />
            </td>
            <td>
                <input type="button" value="取消" onclick="javascript:history.back(-1)" />
            </td>
            <td>
            </td>
        </tr>
      </table>
      </form>
    </div>
    
    
  </div>
  <div class="clear"></div>
  <div style="height:90px; padding:0px 30px; background:url(../../Image/manbg.jpg) no-repeat 0px -700px;"></div>
  </div>
</div>
</body>
</html>