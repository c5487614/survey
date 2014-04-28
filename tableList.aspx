<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tableList.aspx.cs" Inherits="tableList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>邵逸夫医院质量上报系统</title>
    <link href="CSS/forall.css" rel="Stylesheet" type="text/css" />
</head>

<body style="background-image:url(/Image/bg_01.gif)">
    <form id="form1" runat="server" method="get">
    <div class="mainDiv">
        <div style="text-align:center">
        <img alt="邵逸夫医院质量管理系统" src="Image/srrshJci.gif"  width="950px" />
        </div>
        <div>
            <a href="Acc/Manage/Default.aspx?type=1">已审核</a>
            <asp:Label ID="LblisPass" runat="server" Text="Label" ForeColor="Red"></asp:Label>
            <a href="Acc/Manage/Default.aspx?type=0">未审核</a>
            <asp:Label ID="LblnotPass" runat="server" Text="Label" ForeColor="Red"></asp:Label>
        </div>
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  
                Width="100%" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" 
                BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                <Columns>
                    <asp:BoundField HeaderText="序号"  DataField="tableIndex"/>
                    <asp:TemplateField HeaderText="名称1">
                        <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server"
                            CommandArgument='<%# Eval("rdn")+","+Eval("tableName1")+","+Eval("tableType") %>' 
                                Text='<%# Eval("tableName1") %>' OnCommand="LinkButton1_Command" >
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="名称2"  DataField="tableName2"/>
                    <asp:TemplateField HeaderText="链接">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" 
                                Text='<%# "<a href="+Eval("tableType")+"/Default.aspx?id="+Eval("rdn")+","+Eval("tableName1")+","+Eval("tableType")+" >"+Eval("tableName1") %> '></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                <AlternatingRowStyle BackColor="#F7F7F7" />
            </asp:GridView>
        </div>
    
    </div>
    </form>
</body>
</html>
