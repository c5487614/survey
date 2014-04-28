<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Acc_Manage_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>意外事件审核</title>
    <link href="../../CSS/jquery-ui-1.8.custom.css" rel="Stylesheet" />
    <link href="../../CSS/accident.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../../JavaScript/mainJs.js"></script>
    <script type="text/javascript" language="javascript" src="../../JavaScript/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../JavaScript/jquery-ui-1.8.custom.min.js"></script>
    <script type="text/javascript">
    $(document).ready(function(){
            $("#GridView1 tr").mouseover(function(){
                    $(this).css("backgroundColor","Yellow");
            }).mouseout(function(){
                    $(this).css("backgroundColor","");
            })
             $("#GridView1 td:contains('未')").css("backgroundColor","Red");
             $("#dateDiv input:text").datepicker({
                    dateFormat: 'yy-mm-dd',  //日期格式，自己设置 
                    //buttonImage: '../CSS/images/calendar.gif',  //按钮的图片路径，自己设置 
                    //buttonImageOnly: true,  //Show an image trigger without any button. 
                    //showOn: 'both',//触发条件，both表示点击文本域和图片按钮都生效 
                    yearRange: '1999:2012',//年份范围 
                    clearText:'清除',//下面都是些文本设置 
                    closeText:'关闭', 
                    prevText:'前一月', 
                    nextText:'后一月', 
                    currentText:' ', 
                    monthNames:['1月','2月','3月','4月','5月','6月','7月','8月','9月','10月','11月','12月']
                }).addClass("tbox");
    })
    </script>
</head>
<body style="background-image:url(../../Image/bg_01.gif); background-repeat:repeat-x">
    <form id="form1" runat="server">
    <div class="mainDiv">
        <div>
        意外事件列表
        </div>
        <div id="dateDiv">
            开始时间：<asp:TextBox ID="tbDate1" runat="server"></asp:TextBox>
            结束时间：<asp:TextBox ID="tbDate2" runat="server"></asp:TextBox>
            
        </div>
        <div id="typeDiv">
            事件类别：<asp:DropDownList ID="typeList" runat="server"></asp:DropDownList>
            <asp:Button ID="Button1" runat="server" Text="查 询" onclick="Button1_Click" />
        </div>
        <div>
            <asp:GridView ID="GridView1" runat="server"  Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="详细">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" 
                                Text='<%# "<a href=\"accDetailInfo.aspx?id="+Eval("编号")+"\">详细<a>" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="删除">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" Text='删除' CommandArgument='<%# Eval("编号") %>' OnCommand='deleteBtn' OnClientClick="return confirm('确定删除吗？');" ></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
