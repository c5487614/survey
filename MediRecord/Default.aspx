<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="MediRecord_Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 2.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="../CSS/jquery-ui-1.8.custom.css" rel="Stylesheet" />
<link href="../CSS/Medi.css" rel="Stylesheet" type="text/css" />

<script type="text/javascript" language="javascript" src="../JavaScript/mainJs.js"></script>
<script type="text/javascript" language="javascript" src="../JavaScript/Caculate.js"></script>
<script type="text/javascript" language="javascript" src="../JavaScript/Calendar.js"></script>
<script type="text/javascript" language="javascript" src="../JavaScript/jquery-1.4.2.min.js"></script>
<script type="text/javascript" language="javascript" src="../JavaScript/jquery-ui-1.8.custom.min.js"></script>
<script type="text/javascript" language="javascript" src="../JavaScript/intial.js"></script>
    <title>邵逸夫医院</title>
    <style type="text/css">
#GridView1
{
}
#GridView1 tr
{
	background-color:#eee;
	height:25px;
	border:solid 1px #eee ;
}
#GridView1 th
{
	background-color:#e1e1e1;
	height:20px;
	border:solid 1px #333;
	color:Black;
}
#GridView1 td
{
	height:20px;
	border:solid 1px #333 ;
}
.highLight
{
	background-color:Yellow;
}
    </style>
</head>
<body style="background-image:url(../Image/bg_01.gif); background-repeat:repeat-x">
    <form id="form1" runat="server">
    <div class="mainDiv">
        <div style="text-align:center">
        <img alt="邵逸夫医院质量管理系统" src="../Image/srrshJci.gif"  width="950px" />
        </div>
        <div style="text-align:center">
        <h2>病人病历上报系统</h2>
        </div>
        <div>
            <ul style=" list-style-type:none; margin:0px 0px 0px 0px">
            <li>医生科室：<asp:DropDownList ID="ddldept" runat="server" CssClass="tbox">
            </asp:DropDownList>
            </li>
            <li>检查科室：<asp:TextBox ID="checkDept" runat="server" CssClass="tbox" >JCI</asp:TextBox></li>
            <li>检查时间：<asp:TextBox ID="checkTime" runat="server"  CssClass="tbox" onclick="return Calendar('checkTime','')" ></asp:TextBox></li>
            <li>&nbsp;&nbsp;&nbsp; 病历号：<asp:TextBox ID="mrn" runat="server" CssClass="tbox" ></asp:TextBox>
                姓名：<asp:TextBox ID="pName" runat="server" CssClass="tbox" ></asp:TextBox>
            </li>
            <li id="attends">Attending：<asp:TextBox ID="attend1" runat="server"  CssClass="tbox"></asp:TextBox>
                <asp:TextBox ID="attend2" runat="server"  CssClass="tbox" ></asp:TextBox>
                <asp:TextBox ID="attend3" runat="server"  CssClass="tbox" ></asp:TextBox>
                <asp:TextBox ID="attend4" runat="server"  CssClass="tbox" ></asp:TextBox>
                <asp:TextBox ID="attend5" runat="server"  CssClass="tbox" ></asp:TextBox>
            </li>
            <li id="fellows">&nbsp;&nbsp;&nbsp;&nbsp; Fellow：<asp:TextBox ID="fellow1" runat="server"  CssClass="tbox"></asp:TextBox>
                <asp:TextBox ID="fellow2" runat="server"  CssClass="tbox" ></asp:TextBox>
                <asp:TextBox ID="fellow3" runat="server"  CssClass="tbox" ></asp:TextBox>
                <asp:TextBox ID="fellow4" runat="server"  CssClass="tbox" ></asp:TextBox>
                <asp:TextBox ID="fellow5" runat="server"  CssClass="tbox" ></asp:TextBox>
                        
            </li>
            </ul>
        </div>
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  
                Width="90%"  Font-Size="14px" >
                <Columns>
                    <asp:BoundField HeaderText="项目" >
                    </asp:BoundField>
                    <asp:BoundField HeaderText="分数" >
                        <ItemStyle Wrap="False" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        <div id="caculate" >
        病历总分：<asp:TextBox ID="sumup" runat="server" CssClass="tbox"></asp:TextBox>
        <input id="Button2" type="button" value="计算" onclick="getSum(sumup,'checkbox');" />
        
        </div>
        <div>
         <asp:Button ID="Button1" runat="server" Text="确定" onclick="Button1_Click" />
         <asp:Label ID="Label1" runat="server" Text="" Width="150px"></asp:Label>
         <input id="Reset1" type="reset" value="取消" />
        </div>
    </div>
    <div style="position:absolute; bottom:0px; left:0px; float:left" id="floatDiv"><input id="previewButton" type="button" value="预览"  /></div>
    <script type="text/javascript">
    var floatDiv=document.getElementById("floatDiv");
    function setposition()
    {
        floatDiv.style.top=document.documentElement.scrollTop+650;
    }
    setInterval(setposition,10);
    </script>
    </form>
        
</body>
</html>
