<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>JCI登陆</title>
    <link href="CSS/Medi.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
    function checkForm()
    {
        var obj1=document.getElementById("id");
        var obj2=document.getElementById("psw");
        if(obj1.value==""||obj2.value=="")
        {
            alert("请输入账号，密码！");
            return false;
        }
        return true;
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center; width:550px; margin:auto;">
        <div style="text-align:center">
            <div style="background-image:url(Image/login.png); background-repeat:no-repeat; height:550PX">
                <div style="height:280px"></div>
                <div style="height:30px">
                    <b>账号：</b><asp:TextBox ID="id" runat="server" CssClass="tbox" AutoCompleteType="None" Text="10025"></asp:TextBox>
                </div>
                <div style="height:30px">
                    <b>密码：</b><asp:TextBox ID="psw" runat="server" CssClass="tbox" TextMode="Password" AutoCompleteType="None" Text="123456"></asp:TextBox>
                </div>
                <div>
                    <asp:Button ID="Button1" runat="server" Text="确定" onclick="Button1_Click" OnClientClick="return checkForm();" />
                    <asp:Label ID="Label1" runat="server" Text="" Width="50px"></asp:Label>
                    <input id="Reset1" type="reset" value="重置" />
                    
                </div>
            </div>
        </div>
        
    </div>
    </form>
</body>
</html>
