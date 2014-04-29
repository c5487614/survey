<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Patient_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>邵逸夫医院住院病人满意度调查表</title>
    <link href="../CSS/jquery-ui-1.8.custom.css" rel="Stylesheet" type="text/css" />
    <link href="../CSS/Medi.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../JavaScript/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../JavaScript/jquery-ui-1.8.custom.min.js"></script>
    <script type="text/javascript" language="javascript" src="../JavaScript/Caculate.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function(){
                $("#regDate").datepicker({
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
                   $("#tbox_reportDate").datepicker({
                   	dateFormat: 'yy-mm-dd',  //日期格式，自己设置 
                   	buttonImage: '../CSS/images/calendar.gif',  //按钮的图片路径，自己设置 
                   	buttonImageOnly: true,  //Show an image trigger without any button. 
                   	showOn: 'both', //触发条件，both表示点击文本域和图片按钮都生效 
                   	yearRange: '1999:2012', //年份范围 
                   	clearText: '清除', //下面都是些文本设置 
                   	closeText: '关闭',
                   	prevText: '前一月',
                   	nextText: '后一月',
                   	currentText: ' ',
                   	monthNames: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月']
                });
                $("#GridView1 table").click(function(){
                      var inputs=$("#"+this.id+" input").css('backgroundColor','');
                      for(var i=0;i<inputs.length;i++)
                      {
                        if(inputs[i].checked) inputs[i].style.backgroundColor="yellow";
                      }
                });
              $("#myulli tr:first input").click(function(){
				 $("#myulli tr:first input").attr("checked", "");
                    this.checked=true;
                });
                $("#myulli tr:eq(1) input").click(function(){
                    $("#myulli tr:eq(1) input").attr("checked","");
                    this.checked=true;
                   });
                $("#myulli tr:eq(2) input").click(function() {
                   	$("#myulli tr:eq(2) input").attr("checked", "");
                   	this.checked = true;
                   });
               $("#myulli tr:eq(3) input").click(function() {
               		$("#myulli tr:eq(3) input").attr("checked", "");
               		this.checked = true;
               });
                 $("#myulli tr:eq(4) input").click(function() {
                   	$("#myulli tr:eq(4) input").attr("checked", "");
                   	this.checked = true;
                   });
                 $("#myulli tr:eq(5) input").click(function(){
                    $("#myulli tr:eq(5) input").attr("checked","");
                    this.checked=true;
                 });
                   $("#myulli tr:eq(6) input").click(function() {
                   	$("#myulli tr:eq(6) input").attr("checked", "");
                   	this.checked = true;
                   });
               $("#myulli tr:eq(7) input").click(function() {
               	$("#myulli tr:eq(7) input").attr("checked", "");
               	this.checked = true;
               });
        });
    </script>
</head>
<body style="background-image:url(../Image/bg_01.gif); background-repeat:repeat-x">
    <form id="form1" runat="server">
    <asp:HiddenField ID="hf_hidden_test" runat="server" />
    <div class="mainDiv">
        <div style="text-align:center">
         <h2>邵逸夫医院住院病人满意度调查表</h2>
        </div>
        <div>
			
		<table id="myulli">
			<tr>
				<!--report date-->
				<td>调查表时间：</td>
				<td><asp:TextBox CssClass="tbox" ID="tbox_reportDate" runat="server" ></asp:TextBox></td>
			</tr>
			<tr>
				<td>填表人：</td>
				<td><asp:CheckBox ID="cbself" runat="server" Text="病患本人"  /></td>
				<td><asp:CheckBox Checked="true" ID="cbfamily" runat="server" Text="家属"  /></td>
				<td><asp:CheckBox ID="cbfriend" runat="server" Text="朋友"  /></td>
				<td><asp:CheckBox ID="cb_fillperson_absent" runat="server" Text="缺项"  /></td>
			</tr>
			<tr>
				<td>请问病人是否初次入住本院：</td>
				<td><asp:CheckBox Checked="true" ID="cbonce" runat="server" Text="是"  /></td>
				<td><asp:CheckBox ID="cbnotonce" runat="server" Text="否"  /></td>
				<td><asp:CheckBox ID="cbonce_absent" runat="server" Text="缺项"  /></td>
			</tr>
			<tr>
				<!--other1-->
				<td>病人这次住院属于哪一种情况？</td>
				<td><asp:CheckBox Checked="true" ID="cbox_mzh" runat="server" Text="门诊后住院" /></td>
				<td><asp:CheckBox ID="cbox_jz" runat="server" Text="急诊后住院" /></td>
				<td><asp:CheckBox ID="cbox_qtyyzr" runat="server" Text="其他医院转入" /></td>
				<td><asp:CheckBox ID="cbox_zyqk_absent" runat="server" Text="缺项" /></td>
				<td><asp:CheckBox ID="cbox_qt" runat="server" Text="其他" /><asp:TextBox ID="tbox_qt" runat="server" CssClass="tbox" ></asp:TextBox></td>
				
			</tr>
			<tr>
				<td>病人性别：</td>
				<td><asp:CheckBox Checked="true" ID="cbmale" runat="server" Text="男"  /></td>
				<td><asp:CheckBox ID="cbfemale" runat="server" Text="女"  /></td>
				<td><asp:CheckBox ID="cbsex_absent" runat="server" Text="缺项"  /></td>
			</tr>
			<tr>
				<td>病人入院时间：</td>
				<td><asp:TextBox CssClass="tbox" ID="regDate" runat="server" ></asp:TextBox></td>
				<td>病人入住科室：</td>
				<td><asp:DropDownList CssClass="tbox" ID="ddl_dept" runat="server"></asp:DropDownList></td>
				<td>病人入住楼层：</td>
				<td><asp:DropDownList CssClass="tbox" ID="ddl_floor" runat="server"></asp:DropDownList></td>
			</tr>
			<tr>
				<td>病人职业：</td>
				<td><asp:CheckBox Checked="true" ID="cbox_job1" runat="server" Text="农民"  /></td>
				<td><asp:CheckBox ID="cbox_job2" runat="server" Text="工人"  /></td>
				<td><asp:CheckBox ID="cbox_job3" runat="server" Text="技术人员"  /></td>
				<td><asp:CheckBox ID="cbox_job4" runat="server" Text="行政管理"  /></td>
				<td><asp:CheckBox ID="cbox_job5" runat="server" Text="学生"  /></td>
				<td><asp:CheckBox ID="cbox_job_absent" runat="server" Text="缺项"  /></td>
				<td><asp:CheckBox ID="cbox_job6" runat="server" Text="其他"  /><asp:TextBox ID="tbox_job6" runat="server" CssClass="tbox" ></asp:TextBox></td>
				
			</tr>
			<tr>
				<td>病人的医疗费用支付方式：</td>
				<td><asp:CheckBox ID="cbox_pay1" runat="server" Text="公费（含大病统筹）"  /></td>
				<td><asp:CheckBox Checked="true" ID="cbox_pay2" runat="server" Text="医保"  /></td>
				<td><asp:CheckBox ID="cbox_pay3" runat="server" Text="自费"  /></td>
				<td><asp:CheckBox ID="cbox_pay_absent" runat="server" Text="缺项"  /></td>
				<td><asp:CheckBox ID="cbox_pay4" runat="server" Text="其他"  /><asp:TextBox ID="tbox_pay4" runat="server" CssClass="tbox" ></asp:TextBox></td>
				
			</tr>
		</table>
				
        </div>
        <div>
            <asp:GridView ID="GridView1" runat="server" BackColor="White"  Width="100%"
                BorderColor="#999999" BorderWidth="1px" CellPadding="3" Font-Size="Medium"
                GridLines="Vertical" AutoGenerateColumns="False" BorderStyle="None" 
				 OnRowCreated="GridView1_RowCreated" >
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                        <div style=" float:left; width:450px; ">
                            <asp:Label ID="Label1" runat="server"  ForeColor="#FFF"
                                Text='<%# "["+Eval("largeItemName")+"]" %>'></asp:Label>
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("SortId") + "." %>'></asp:Label>
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("smallItemName") %>'></asp:Label>
                        </div>
                            <%--<br />--%>
                        <div style=" float:left; width:300px">
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server"  RepeatLayout="Flow"
                                RepeatDirection="Horizontal" Width="550px">
                                <asp:ListItem Value="-1">无此需要</asp:ListItem>
                                <asp:ListItem Selected="True" Value="10">满意</asp:ListItem>
                                <asp:ListItem Value="7">比较满意</asp:ListItem>
                                <asp:ListItem Value="5">一般</asp:ListItem>
                                <asp:ListItem Value="0">不满意</asp:ListItem>
                                <asp:ListItem Value="-2">缺项</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                            <asp:HiddenField ID="hf_largeItemRdn" Value='<%#Eval("largeItemRdn") %>' runat="server" />
                            <asp:HiddenField ID="hf_smallItemRdn" Value='<%#Eval("smallItemRdn") %>' runat="server" />
                            <asp:HiddenField ID="hf_sortId" Value='<%#Eval("SortId") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <PagerStyle BackColor="#999999" ForeColor="Black" 
                    HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#008A8C" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="#DCDCDC" />
            </asp:GridView>
            <div>
                <asp:HiddenField ID="HiddenField1" runat="server" />
            </div>
            <div>
            <table>
				<tr>
					<td><a style=" color:Red">医疗工作</a>您最满意的人和事</td>
					<td>
						<asp:TextBox ID="tbox_promote_yl"  CssClass="tbox_advice"  TextMode="MultiLine" runat="server"></asp:TextBox>
					</td>
					<td><a style=" color:Red">医疗工作</a>最需要改进的方面</td>
					<td>
						<asp:TextBox ID="tbox_needImprove_yl" CssClass="tbox_advice" TextMode="MultiLine" runat="server"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td><a style=" color:Red">护理工作</a>最满意的人和事</td>
					<td>
						<asp:TextBox ID="tbox_promote_hl" CssClass="tbox_advice"  TextMode="MultiLine" runat="server"></asp:TextBox>
					</td>
					<td><a style=" color:Red">护理工作</a>最需要改进的方面</td>
					<td>
						<asp:TextBox ID="tbox_needImprove_hl" CssClass="tbox_advice" TextMode="MultiLine" runat="server"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td><a style=" color:Red">医技工作</a>您最满意的人和事</td>
					<td>
						<asp:TextBox ID="tbox_promote_yj" CssClass="tbox_advice"  TextMode="MultiLine" runat="server"></asp:TextBox>
					</td>
					<td><a style=" color:Red">医技工作</a>最需要改进的方面</td>
					<td>
						<asp:TextBox ID="tbox_needImprove_yj" CssClass="tbox_advice" TextMode="MultiLine" runat="server"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td><a style=" color:Red">后勤工作</a>您最满意的人和事</td>
					<td>
						<asp:TextBox ID="tbox_promote_hq" CssClass="tbox_advice"  TextMode="MultiLine" runat="server"></asp:TextBox>
					</td>
					<td><a style=" color:Red">后勤工作</a>最需要改进的方面</td>
					<td>
						<asp:TextBox ID="tbox_needImprove_hq" CssClass="tbox_advice" TextMode="MultiLine" runat="server"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td><a style=" color:Red">住院费用相关</a>您最满意的人和事</td>
					<td>
						<asp:TextBox ID="tbox_promote_zy" CssClass="tbox_advice"  TextMode="MultiLine" runat="server"></asp:TextBox>
					</td>
					<td><a style=" color:Red">住院费用相关</a>最需要改进的方面</td>
					<td>
						<asp:TextBox ID="tbox_needImprove_zy" CssClass="tbox_advice" TextMode="MultiLine" runat="server"></asp:TextBox>
					</td>
				</tr>
            </table>
            </div>
        </div>
        
        <div>
            <asp:Button ID="Button1" runat="server" Text="确定" onclick="Button1_Click1"   OnClientClick="getAdd(this,'radio');" />
            <asp:Label ID="Label3" runat="server" Text="" Width="150px"></asp:Label>
            <input id="Reset1" type="reset" value="重置" />
        </div>
    </div>
    </form>
</body>
</html>
