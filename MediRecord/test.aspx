<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="MediRecord_test" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>����</title>
    
     <%--<script type="text/javascript" language="javascript" src="../ActiveX/dynaload.js"></script>--%>
     <script type="text/javascript" language="javascript" src="../JavaScript/jquery-ui-1.8.custom.min.js"></script>
     <link rel="Stylesheet" href="../CSS/jquery-ui-1.8.custom.css" />
    <script type="text/javascript" language="javascript">
        function OnReady(id)
         {
         alert('xx');
          var series = "����ͳ��ͼ";
          var names = "����\t�Ϻ�\t����\t����\t����\t���\t����";
          alert(names);
          var datas = "130\t33\t312\t134\t290\t90\t122";
          AF.func("AddSeries", series +"\r\n"+ names +"\r\n"+ datas); 
         }
         $(function(){
            $("#TextBox1").datepicker();
         }
         );

    </script>
</head>
<body>
    <div>
    <%--<script>insertChart('AF', 'bordercolor=#4499ff;borderWidth=2;borderRound=23;backcolor=#fff5ea,#f0e0d0');</script>--%>
    </div>
    <form id="form1" runat="server">
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </form>
</body>
</html>
