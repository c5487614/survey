<%@ Page Language="C#" AutoEventWireup="true" CodeFile="deptView.aspx.cs" Inherits="MediRecord_deptView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>科室查看页面</title>
    <link href="../CSS/Medi.css" rel="Stylesheet" type="text/css" />
    <link href="../CSS/jquery.jqplot.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../JavaScript/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../JavaScript/dist/excanvas.min.js"></script>
    <script type="text/javascript" language="javascript" src="../JavaScript/dist/jquery.jqplot.min.js"></script>
    <script type="text/javascript" language="javascript" src="../JavaScript/dist/jqplot.barRenderer.min.js"></script>
    <script type="text/javascript" language="javascript" src="../JavaScript/dist/jqplot.categoryAxisRenderer.min.js"></script>
    <script type="text/javascript">
    $(document).ready(function(){
            var url="data.aspx?ts="+Math.random();
            $.get(url,callBack1);
    }) 
    var isIE = false;
    if(navigator.userAgent.indexOf("MSIE")>0) isIE=true;
    function drawLines(lineText,lineData)
    {
        //var describeData=["病历1","病历2","病历3","病历4","病历5","病历6",]
         //   var data = [89,99,55,70,88,98];
			plot1 = $.jqplot('chartShow', [lineData], {
				title:"<h3>部门病历分数表</h3>",
				axes: {
					xaxis: {renderer: $.jqplot.CategoryAxisRenderer,
					        ticks: lineText
					        },
					yaxis: {min:50, max:100,tickInterval: 5, tickOptions:{formatString:'%2d'}}
				},
				
				grid: { borderWidth: 1.5 },
				seriesDefaults: {
					//color: '#F90',
					renderer: $.jqplot.BarRenderer,
					shadow: true
				}
			});
    }
    function callBack1(data)
    {
        var docs=data.getElementsByTagName("docs");
        var scores=data.getElementsByTagName("scores");
        if(isIE)
        {
            var docName=docs[0].text.split(';');
            var score=scores[0].text.split(';');
           drawLines(docName,score);
        }
        else
        {
             var docName=docs[0].textContent.split(';');
            var score=scores[0].textContent.split(';');
           drawLines(docName,score);
            
        }
    }
    </script>
</head>

<body style="background-image:url(../Image/bg_01.gif); background-repeat:no-repeat">
    <form id="form1" runat="server">
    <div style= "width:850px; text-align:left; margin:auto">
        <div style="text-align:center">
            <h2>
            <asp:Label ID="Label1" runat="server" Text="XX部门20104月份病历检查结果"></asp:Label>
            </h2> 
        </div>
        <div>
        日期：<asp:TextBox ID="tboxdate" runat="server"  CssClass="tbox" ></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="查看" onclick="Button1_Click" />
        </div>
        <div>
        <asp:GridView ID="GridView1" runat="server" Width="100%" CellPadding="4" 
                ForeColor="#333333">
            <RowStyle BackColor="#E3EAEB" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#7C6F57" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        </div>
        <!--图表显示 -->
        <div id="chartShow" style="height:500px">
        
        </div>
        <!--文字显示 -->
         <div>
         <br />
             <div style="text-align:center">
             <h4>存在的问题：</h4>
            </div>
        </div>
        <div>
            <asp:GridView ID="GridView2" runat="server" BackColor="White"  Width="100%"
                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                ForeColor="Black" GridLines="Vertical">
                <RowStyle BackColor="#F7F7DE" />
                <FooterStyle BackColor="#CCCC99" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
