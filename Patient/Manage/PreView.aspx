<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PreView.aspx.cs" Inherits="Patient_Manage_PreView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>邵逸夫医院住院病人满意度调查表管理</title>
    <link href="../../CSS/jquery-ui-1.8.custom.css" rel="Stylesheet" type="text/css" />
    <link href="../../CSS/Medi.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../../JavaScript/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../JavaScript/jquery-ui-1.8.custom.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../JavaScript/Caculate.js"></script>
    <script type="text/javascript" language="javascript">
    </script>
    <style type="text/css">
* {
	font-size:14px;
	color:#333;
}
body {
	margin:0px;
	text-align:center;
}
ul {
	float:left;
	margin:0px;
	padding:0px;
}
li {
	float:left;
	list-style:none;
	border-bottom:1px solid #ccc;
	border-right:1px solid #ccc;
	height:24px;
	line-height:24px;
	padding-left:10px;
	overflow:hidden;
}
.clear {
	height:0px;
	clear:left;
	font-size:0px;
	line-height:0px;
}
.tits {
	height:36px;
	float:left;
	padding-left:10px;
	line-height:36px;
	font-size:16px;
	font-weight:bold;
	color:#333;
	border-bottom:1px solid #ccc;
	border-right:1px solid #ccc;
}
.txts {
	float:left;
	width:880px;
	padding:10px;
	text-align:left;
}
.txts div {
	width:870px;
	padding-left:10px;
	line-height:22px;
}
.ccbak{
	background-color:#eee;
}
.senb{
	background:#fff;
	color:#333;
}

</style>
</head>
<body style=" background-image:url(../../Image/bg.jpg);">
<div style="margin:auto; width:1000px;">
<div style="padding:0px 20px; float:left;">
	<div style="height:144px; padding:0px 30px; background:url(../../Image/manbg.jpg) no-repeat;"></div>
  <div style="padding:10px 10px; width:900px; padding:12px 30px; float:left; background:url(../../Image/mbg.jpg)">
    <div style="float:left;  width:898px; border:1px solid #ccc;text-align:left; height:1700px;">
    <form id="form1" runat="server">
    <div class="mainDiv">
        <div style="text-align:center">
         <h2>邵逸夫医院住院病人满意度调查表</h2>
        </div>
        <div>
		
		<table id="myulli">
			<tr>
				<!--report date-->
				<td>调查表时间：</td>
				<td><asp:Label ID="tbox_reportDate" runat="server" Text="Label"></asp:Label>
				<%--<asp:TextBox CssClass="tbox" ID="tbox_reportDate" runat="server" ></asp:TextBox>--%></td>
			</tr>
			<tr>
				<td>填表人：</td>
				<td><asp:Label ID="tbox_fillPerson" runat="server" Text="Label"></asp:Label></td>
				<%--<td><asp:CheckBox Checked="true" ID="cbself" runat="server" Text="病患本人"  /></td>
				<td><asp:CheckBox ID="cbfamily" runat="server" Text="家属"  /></td>
				<td><asp:CheckBox ID="cbfriend" runat="server" Text="朋友"  /></td>--%>
			</tr>
			<tr>
				<td>请问病人是否初次入住本院：</td>
				<td><asp:Label ID="tbox_once" runat="server" Text="Label"></asp:Label></td>
				<%--<td><asp:CheckBox Checked="true" ID="cbonce" runat="server" Text="是"  /></td>
				<td><asp:CheckBox ID="cbnotonce" runat="server" Text="否"  /></td>--%>
			</tr>
			<tr>
				<!--other1-->
				<td>病人这次住院属于哪一种情况？</td>
				<td><asp:Label ID="tbox_other1" runat="server" Text="Label"></asp:Label></td>
				<%--<td><asp:CheckBox Checked="true" ID="cbox_mzh" runat="server" Text="门诊后住院" /></td>
				<td><asp:CheckBox ID="cbox_jz" runat="server" Text="急诊后住院" /></td>
				<td><asp:CheckBox ID="cbox_qtyyzr" runat="server" Text="其他医院转入" /></td>
				<td><asp:CheckBox ID="cbox_qt" runat="server" Text="其他" /><asp:TextBox ID="tbox_qt" runat="server" CssClass="tbox" ></asp:TextBox></td>--%>
			</tr>
			<tr>
				<td>病人性别：</td>
				<td><asp:Label ID="tbox_personSex" runat="server" Text="Label"></asp:Label></td>
				<%--<td><asp:CheckBox Checked="true" ID="cbmale" runat="server" Text="男"  /></td>
				<td><asp:CheckBox ID="cbfemale" runat="server" Text="女"  /></td>--%>
			</tr>
			<tr>
				<td>病人入院时间：</td>
				<td><asp:Label ID="tbox_regDate" runat="server" Text="Label"></asp:Label></td>
				<%--<td><asp:TextBox CssClass="tbox" ID="regDate" runat="server" ></asp:TextBox></td>--%>
				<td>病人入住科室：</td>
				<td><asp:Label ID="tbox_ddl_dept" runat="server" Text="Label"></asp:Label></td>
				<%--<td><asp:DropDownList CssClass="tbox" ID="ddl_dept" runat="server"></asp:DropDownList></td>--%>
				<td>病人入住楼层：</td>
				<td><asp:Label ID="tbox_ddl_floor" runat="server" Text="Label"></asp:Label></td>
				<%--<td><asp:DropDownList CssClass="tbox" ID="ddl_floor" runat="server"></asp:DropDownList></td>--%>
			</tr>
			<tr>
				<td>病人职业：</td>
				<td><asp:Label ID="tbox_job" runat="server" Text="Label"></asp:Label></td>
				<%--<td><asp:CheckBox Checked="true" ID="cbox_job1" runat="server" Text="农民"  /></td>
				<td><asp:CheckBox ID="cbox_job2" runat="server" Text="工人"  /></td>
				<td><asp:CheckBox ID="cbox_job3" runat="server" Text="技术人员"  /></td>
				<td><asp:CheckBox ID="cbox_job4" runat="server" Text="行政管理"  /></td>
				<td><asp:CheckBox ID="cbox_job5" runat="server" Text="学生"  /></td>
				<td><asp:CheckBox ID="cbox_job6" runat="server" Text="其他"  /><asp:TextBox ID="tbox_job6" runat="server" CssClass="tbox" ></asp:TextBox></td>--%>
			</tr>
			<tr>
				<td>病人的医疗费用支付方式：</td>
				<td><asp:Label ID="tbox_pay" runat="server" Text="Label"></asp:Label></td>
				<%--<td><asp:CheckBox Checked="true" ID="cbox_pay1" runat="server" Text="公费（含大病统筹）"  /></td>
				<td><asp:CheckBox ID="cbox_pay2" runat="server" Text="医保"  /></td>
				<td><asp:CheckBox ID="cbox_pay3" runat="server" Text="自费"  /></td>
				<td><asp:CheckBox ID="cbox_pay4" runat="server" Text="其他"  /><asp:TextBox ID="tbox_pay4" runat="server" CssClass="tbox" ></asp:TextBox></td>--%>
			</tr>
		</table>
        </div>
        <div>
            <asp:GridView ID="GridView1" runat="server" BackColor="White"  Width="100%"
                BorderColor="#999999" BorderWidth="1px" CellPadding="3"
                GridLines="Vertical" AutoGenerateColumns="False" BorderStyle="None" 
				 onrowcreated="GridView1_RowCreated">
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                        <div style=" float:left; width:450px">
                            <asp:Label ID="Label1" runat="server"  ForeColor="#FFF"
                                Text='<%# "["+Eval("largeItemName")+"]" %>'></asp:Label>
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("SortId") + "." %>'></asp:Label>
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("smallItemName") %>'></asp:Label>
                        </div>
                            <%--<br />--%>
                        <div style=" float:right; width:200px">
							<asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                        </div>
                            <asp:HiddenField ID="hf_largeItemRdn" Value='<%#Eval("largeItemRdn") %>' runat="server" />
                            <asp:HiddenField ID="hf_smallItemRdn" Value='<%#Eval("smallItemRdn") %>' runat="server" />
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
            <%--<asp:Button ID="Button1" runat="server" Text="确定" onclick="Button1_Click1"   OnClientClick="getAdd(this,'radio');" />
            <asp:Label ID="Label3" runat="server" Text="" Width="150px"></asp:Label>
            <input id="Reset1" type="reset" value="重置" />--%>
        </div>
    </div>
    </form>
    </div>
    
    
  </div>
  <div class="clear"></div>
  <div style="height:90px; padding:0px 30px; background:url(../../Image/manbg.jpg) no-repeat 0px -700px;"></div>
  </div>
</div>
</body>
</html>
