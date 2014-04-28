<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="OutPatient_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>门诊病人满意度调查</title>
    <link href="../CSS/forall.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../JavaScript/Calendar.js"></script>
    <script type="text/javascript" language="javascript" src="../JavaScript/Caculate.js"></script>
    <script type="text/javascript" language="javascript" src="../JavaScript/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../JavaScript/jquery-ui-1.8.custom.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="text-align:center">
        <h2>门诊病人满意度调查表</h2>
        </div>
        <div class="mainDiv">
             <div>
                <asp:GridView ID="GridView1" runat="server" BackColor="White"  Width="100%"
                    BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                    GridLines="Horizontal" AutoGenerateColumns="False">
                    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                    <Columns>
                        <asp:TemplateField HeaderText="项目">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" ForeColor="Red" 
                                    Text='<%# "["+Eval("largeItemName")+"]" %>'></asp:Label>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("smallItemName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="分数">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" Text='<%# Eval("sItemValue") %>' />
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
            <div>
                <asp:Button ID="Button1" runat="server" Text="确定" onclick="Button1_Click" />
            </div>
        </div>
        
        
    </div>
    </form>
</body>
</html>
