<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Acc_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../CSS/jquery-ui-1.8.custom.css" rel="Stylesheet" />
    <link href="../CSS/accident.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../JavaScript/mainJs.js"></script>
    <script type="text/javascript" language="javascript" src="../JavaScript/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../JavaScript/jquery-ui-1.8.custom.min.js"></script>
    <script type="text/javascript" >
    function cbSex(obj)
    {
        var obj1=document.getElementById("cbSex1");
        var obj2=document.getElementById("cbSex2");
        obj1.checked=false;
        obj2.checked=false;
        obj.checked=true;
    }
    function cbType(obj)
    {
        var obj1=document.getElementById("hcb1");
        var obj2=document.getElementById("hcb2");
        obj1.checked=false;
        obj2.checked=false;
        obj.checked=true;
    }
    $(document).ready(function(){
            $("#hoccurDate").datepicker({
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
                });
            $("#GridView1 input:checkbox").click(function(){
                    if(this.checked)
                    {
                        this.style.backgroundColor="red";
                    }
                    else
                    {
                        this.style.backgroundColor="";
                    }
            })
            $("#addContent").focus(function(){
                    this.select();
            })
    })
    </script>
</head>
<body style="background-image:url(../Image/bg_01.gif); background-repeat:repeat-x">
    <form id="form1" runat="server">
    <div class="mainDiv">
        <div style="text-align:center; font-size:18px; font-family::@仿宋_GB2312">
            <asp:Label ID="reportTitle" runat="server" Text="" ></asp:Label>
        </div>
        <div>
        <div>
            <div class="upperLeft">
            <div>
             <asp:Label ID="Label1" runat="server" Text="病人姓名："  Width="60px"></asp:Label>
             <asp:TextBox  runat="server" ID="pName" CssClass="tbox"></asp:TextBox> 
             <asp:Label ID="Label6" runat="server" Text="病人性别："  Width="60px"></asp:Label>
             <asp:CheckBox ID="cbSex1" runat="server" Text="男" onclick="cbSex(this)"  />
             <asp:CheckBox ID="cbSex2" runat="server" Text="女" onclick="cbSex(this)" />
            </div>
            <div>
                <asp:Label ID="Label2" runat="server" Text="病历号：" Width="60px" Height="16px"></asp:Label>
                
                <asp:TextBox  runat="server" ID="pID" CssClass="tbox"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="Label3" runat="server" Text="年龄：" Width="60px"></asp:Label>
                <asp:TextBox  runat="server" ID="ppage" CssClass="tbox"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="Label4" runat="server" Text="手术：" Width="60px"></asp:Label>
                <asp:TextBox  runat="server" ID="pOper" CssClass="tbox"></asp:TextBox>
            </div>
            
            </div>
            <div class="upperRight">
                <div>
                上报员工：<asp:TextBox  runat="server" ID="hEmp" CssClass="tbox"></asp:TextBox>       
                部门：<asp:TextBox  runat="server" ID="hDept" CssClass="tbox"></asp:TextBox>
               
                </div>
                <div>
                 电话号码：<asp:TextBox  runat="server" ID="hPhone" CssClass="tbox"></asp:TextBox>
                </div>
                <div>
                发生日期：<asp:TextBox  runat="server" ID="hoccurDate" CssClass="tbox" ></asp:TextBox>
                时间：<asp:TextBox  runat="server" ID="hoccurTime" CssClass="tbox"></asp:TextBox>(格式：####)
                </div>
                <div>
                    <asp:CheckBox ID="hcb1" runat="server"  Text="住院病人" onclick="cbType(this)"/>
                    <asp:CheckBox ID="hcb2" runat="server"  Text="门诊病人" onclick="cbType(this)"/>
                </div>
            </div>
            
        </div>
        </div>
        <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  
                Width="100%">
            <Columns>
                <asp:BoundField HeaderText="类别" >
                    <ItemStyle Width="20%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="详细信息">
                    <ItemStyle Width="80%" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </div>
        <div>
            <div style="color:Red"><br />注意：如有实际情况未包括在选项里，请在下面的文本框里写入备注！</div>
            <div>
                <asp:TextBox ID="addContent" runat="server" TextMode="MultiLine" Height="62px"  Text="请在这里写入备注"
                    Width="794px" ></asp:TextBox>
            </div>
            <div class="leftDiv"> 
            事情经过(Who，When，Where， What，Why，How)：
            <br />
            <br />
                <asp:TextBox ID="hDescribe" runat="server" TextMode="MultiLine" Width="350px" Height="250px" ></asp:TextBox>
            </div>
            <div class="rightDiv">
             纠正措施：
            <br />
            <br />
                <asp:TextBox ID="hMeasure" runat="server" TextMode="MultiLine" Width="350px" Height="250px"></asp:TextBox>
            </div>
        </div>
        <div class="hDiv">
        </div>
        <div style=" padding-top:0px; text-align:center">
           <asp:Button ID="btsubmit" runat="server" Text="上 报" onclick="btsubmit_Click"  />
           <asp:Label ID="Label5" runat="server" Text="" Width="60px"></asp:Label>
            <input id="Reset1" type="reset" value="重 置"  onclick="clearMe()"/>
        </div>
    </div>
    </form>
</body>
</html>
