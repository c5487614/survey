<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Patient_Manage_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>test</title>
    <link href="../../CSS/jquery-ui-1.8.custom.css" rel="Stylesheet" type="text/css" />
    <link href="../../CSS/Medi.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../../JavaScript/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../JavaScript/jquery-ui-1.8.custom.min.js"></script>
    <script>
    	$(document).ready(function() {
    		$("#tbox_beginDate").datepicker({
    			dateFormat: 'yy-mm-dd',  //日期格式，自己设置 
    			buttonImage: '../../CSS/images/calendar.gif',  //按钮的图片路径，自己设置 
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
    		$("#tbox_endDate").datepicker({
    			dateFormat: 'yy-mm-dd',  //日期格式，自己设置 
    			buttonImage: '../../CSS/images/calendar.gif',  //按钮的图片路径，自己设置 
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
    	});
    </script>
</head>
<body style="background-image:url(../../Image/bg_01.gif); background-repeat:repeat-x">
    <form id="form1" runat="server">
		<div class="mainDiv">
			<div style="text-align:center">
			 <h2>邵逸夫医院住院病人满意度调查管理</h2>
			</div>
			<table>
				<tr>
					<td>开始时间：</td>
					<td><asp:TextBox ID="tbox_beginDate" runat="server"></asp:TextBox></td>
					<td>结束时间：</td>
					<td><asp:TextBox ID="tbox_endDate" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td>科室：</td>
					<td><asp:DropDownList CssClass="tbox" ID="ddl_dept" runat="server"></asp:DropDownList></td>
					<td>类别：</td>
					<td><asp:DropDownList CssClass="tbox" ID="ddl_category1" runat="server"></asp:DropDownList></td>
					<td><asp:Button ID="btn_export_dept" runat="server" OnClick="btn_export_dept_Click" Text="导出科室" /></td>
				</tr>
				<tr>
					<td>楼层：</td>
					<td><asp:DropDownList CssClass="tbox" ID="ddl_floor" runat="server"></asp:DropDownList></td>
					<td>类别：</td>
					<td><asp:DropDownList CssClass="tbox" ID="ddl_category2" runat="server"></asp:DropDownList></td>
					<td><asp:Button ID="btn_export_floor" runat="server" OnClick="btn_export_floor_Click" Text="导出楼层" /></td>
				</tr>
			</table>
			<div>
				<asp:Button ID="btn_export" runat="server" OnClick="btn_export_Click" Text="导出医疗" />
				<asp:Button ID="btn_export_hl" runat="server" OnClick="btn_export_hl_Click" Text="导出护理" />
				<asp:Button ID="btn_export_yj" runat="server" OnClick="btn_export_yj_Click" Text="导出医技" />
				<asp:Button ID="btn_export_hq" runat="server" OnClick="btn_export_hq_Click" Text="导出后勤" />
				<asp:Button ID="btn_export_zy" runat="server" OnClick="btn_export_zy_Click" Text="导出 行政-收费" />
				<asp:Button ID="btn_export_all" runat="server" OnClick="btn_export_all_Click" Text="导出数据" />
			</div>
			<div>
			<asp:Repeater ID="rpt_patient" runat="server">
				<HeaderTemplate>
					<table>
						<tr>
							<td style=" width:50px">编号</td>
							<td style=" width:150px">科室</td>
							<td style=" width:150px">楼层</td>
							<td style=" width:50px">分数</td>
							<td style=" width:2000px">操作</td>
						</tr>
				</HeaderTemplate>
				<ItemTemplate>
					<tr>
					<td><%#Eval("rdn") %></td>
					<td><%#Eval("dept") %></td>
					<td><%#Eval("floorName") %></td>
					<td><%#Eval("tableScore") %></td>
					<td><%--<a href="javascript:void(0)">删除</a>--%>
						<asp:LinkButton ID="btn_delete" runat="server" OnCommand="btn_delete_Click" CommandArgument='<%#Eval("rdn") %>'>删除</asp:LinkButton>
						<a target="_blank" href='<%# "PreView.aspx?rdn=" +Eval("rdn") %> '>预览</a>
					</td>
					</tr>
				</ItemTemplate>
				<FooterTemplate>
					</table>
				</FooterTemplate>
			</asp:Repeater>
		</div>
		</div>
		
    </form>
</body>
</html>
