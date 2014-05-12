<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin_login.aspx.cs" Inherits="Admin_login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>邵逸夫医院护理进修管理页面</title>
</head>

<body style="background-image:url(Image/login_back.jpg); margin:0px; text-align:center;">
<form runat="server" id="form1">
    <div style="height:342px; margin:auto; padding-top:250px; width:1000px; background-image:url(Image/login_bl.jpg); text-align:left;">
    	<div style="padding-left:450px; height:20px;">
            <asp:TextBox ID="tb_userDept" runat="server" Width="150px"></asp:TextBox>
    	</div>
    	<div style=" height:20px;"></div> 
    	<div style="padding-left:450px; height:20px;">
            <asp:TextBox ID="tb_UserId" runat="server" Width="150px"></asp:TextBox>
    	</div>
        <div style=" height:20px;"></div> 
        <div style="padding-left:450px; height:20px;">
            <asp:TextBox ID="tb_UserPsw" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
        </div>
        <div style=" height:14px;"></div> 
        <div style="padding-left:430px; height:20px;">
        	<div style=" height:20px; float:left; width:80px;">
            <asp:Button ID="btn_Login" runat="server" Text="登录" onclick="btn_Login_Click" />
            </div>
            <div style=" height:20px; float:left; ">
                <input id="Reset1" type="reset" value="重置" />
            </div>
        </div>
     </div>
</form>
</body>
</html>
