<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="QA_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="../CSS/jquery-ui-1.8.custom.css" rel="Stylesheet" />
    <title>QA表格</title>
    <link href="../CSS/forall.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../JavaScript/Calendar.js"></script>
    <script type="text/javascript" language="javascript" src="../JavaScript/Caculate.js"></script>
    <script type="text/javascript" language="javascript" src="../JavaScript/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../JavaScript/jquery-ui-1.8.custom.min.js"></script>
    <script type="text/ecmascript">
    var Content=new Array();
    $(document).ready(function(){
         $("#checkdate").datepicker({
                    dateFormat: 'yy-mm-dd',  //日期格式，自己设置 
                    buttonImage: '../CSS/images/calendar.gif',  //按钮的图片路径，自己设置 
                    buttonImageOnly: true,  //Show an image trigger without any button. 
                    showOn: 'both',//触发条件，both表示点击文本域和图片按钮都生效 
                    yearRange: '1999:2012',//年份范围 
                    clearText:'清除',//下面都是些文本设置 
                    closeText:'关闭', 
                    prevText:'前一月', 
                    nextText:'后一月', 
                    currentText:' ', 
                    monthNames:['1月','2月','3月','4月','5月','6月','7月','8月','9月','10月','11月','12月']
                });
                
         $("#GridView1 table").click(function(){
                      var inputs=$("#"+this.id+" input").css('backgroundColor','');
                      for(var i=0;i<inputs.length;i++)
                      {
                        if(inputs[i].checked) 
                        {
                            $(inputs[i]).css("background-color","yellow");
                        }
                      }
                });
         $("#GridView1 .tboxL").each(function(){
                $(this).keyup(function(){
                            if(this.value=="") return ;
                           var url="../ajax/getDicForQA.aspx?keys="+this.value;
                           $.get(url,"1",callBack1)
                        })
        $("#GridView1 .tboxL").autocomplete({source:Content});
         })
    })
    var isIE = false;
    if(navigator.userAgent.indexOf("MSIE")>0) isIE=true;
    function callBack1(data)
    {
        try
        { 
            if(isIE)
            {
                 var names=data.getElementsByTagName("content");
                 if(names==null) return;
                  Content.length=0;
                  for(var i=0;i<names.length;i++)
                  {
                    Content.push(names[i].text);
                  }
             }
             else
             {
                var names=data.getElementsByTagName("content");
                 if(names==null) return;
                  Content.length=0;
                  for(var i=0;i<names.length;i++)
                  {
                    Content.push(names[i].textContent);
                  }
             }
        }
        catch(e)
        {
        }
    }
    function checkSum()
    {
        var obj1=document.getElementById("tableScore");
        if(obj1.value=="")
        {
            alert("请先计算得分！");
            return false;
        }
        return true;
    }
    </script>
    <style type="text/css">
    .tboxL
    {
    	width:250px
    }
    </style>
</head>
<body style="background-image:url(../Image/bg_01.gif); background-repeat:repeat-x">
    <form id="form1" runat="server">
    <div class="mainDiv">
        <div style="text-align:center">
            <asp:Label ID="lbltitle" runat="server" Text=""></asp:Label>
        </div>
        <div style="text-align:left">
            <ul class="myul">
                <li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 楼层：
                    <asp:TextBox ID="checkStair" runat="server" CssClass="tbox" ></asp:TextBox>
                </li>
                <li>&nbsp;&nbsp;&nbsp; 护士长：
                    <asp:TextBox ID="doc" runat="server" CssClass="tbox" ></asp:TextBox>
                </li>
                <li>检查日期：
                    <asp:TextBox ID="checkdate" runat="server" CssClass="tbox" ></asp:TextBox>
                </li>
            </ul>
        </div>
        <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  Width="100%"
                BackColor="White" BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="1px" 
                CellPadding="3">
            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" BorderStyle="Solid" 
                BorderWidth="1px" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server"  ForeColor="BlueViolet"
                            Text='<%# "["+Eval("largeItemName")+"]" %>'></asp:Label>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("smallItemName") %>'></asp:Label>
                        <br />
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server"  
                            RepeatDirection="Horizontal">
                            <asp:ListItem Value="5" >5分</asp:ListItem>
                            <asp:ListItem Value="4">4分</asp:ListItem>
                            <asp:ListItem Value="3">3分</asp:ListItem>
                            <asp:ListItem Value="2">2分</asp:ListItem>
                            <asp:ListItem Value="1">1分</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:HiddenField ID="HiddenField1" runat="server" 
                            Value='<%# Eval("largeItemRdn") %>' />
                        <asp:HiddenField ID="HiddenField2" runat="server" 
                            Value='<%# Eval("smallItemRdn") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="备注">
                    <ItemTemplate>
                        备注：<asp:TextBox ID="TextBox1" runat="server" Width="361px" class="tboxL"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" 
                BorderColor="#66FF33" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
            <AlternatingRowStyle BackColor="#F7F7F7" />
        </asp:GridView>
        </div>
        <div>
            
        得分：<asp:TextBox ID="tableScore" runat="server"></asp:TextBox>
        <input id="Button2" type="button" value="计算" onclick="getAdd(tableScore,'radio')" />
        </div>
        <div>
            
            <asp:Button ID="Button1" runat="server" Text="确定" onclick="Button1_Click" OnClientClick="return  checkSum();" />
            <asp:Label ID="Label3" runat="server" Text="" Width="150px" ></asp:Label>
            <input id="Reset1" type="reset" value="重置" />
        </div>
    </div>
    </form>
</body>
</html>
