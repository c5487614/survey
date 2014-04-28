 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="picView.aspx.cs" Inherits="MediRecord_picView" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <script type="text/javascript" language="javascript" src="../ActiveX/dynaload.js"></script>
    <script type="text/javascript" language="javascript" src="../JavaScript/Calendar.js"></script>
    <script type="text/javascript" language="javascript" src="../JavaScript/loadData.js"></script>
    <script type="text/javascript" language="javascript">
     function sendRequest()
     {
        var tbox1=document.getElementById("date");
        if(tbox1.value==""){alert("请选择时间!");return ;}
        var reg=/\d{4}-\d{1,2}-\d{1,2}/;
        if(!reg.test(tbox1.value)) {alert("时间格式不对!");return ;}
        loaddata("测试",tbox1.value);
     }
    </script>
</head>
<body>
    <div style="height:500px; width:900px">
        <input id="date" type="text" onclick="return Calendar('date');" />
    <input id="Button1" type="button" value="button" onclick="sendRequest();" />
    <script type="text/javascript">
    insertChart('AF', 'bordercolor=#4499ff;borderWidth=2;borderRound=23;backcolor=#fff5ea,#f0e0d0');
    </script>

        
    </div>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
    <script type="text/javascript" language="javascript">
    var tbox1=document.getElementById("date");
    var now=new Date();
    tbox1.value=now.getFullYear()+"-"+(parseInt(now.getMonth())+1)+"-"+now.getDate();
    </script>
</body>
</html>
