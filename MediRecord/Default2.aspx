<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="MediRecord_Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ajax</title>
    <link href="../CSS/jquery-ui-1.8.custom.css" rel="Stylesheet" />
    <script type="text/javascript" language="javascript" src="../JavaScript/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../JavaScript/jquery-ui-1.8.custom.min.js"></script>
    var dataSource = new Array();
    $(document).ready(function(){
        $("#tags").keyup(function(){
                $.get("getNames.aspx",function(data){
                    var names=data.getElementsByTagName("name");
                    dataSource.length=0;
                    for(var i=0;i<names.length;i++)
                    {
                        dataSource.push(names[i].text);
                    }
                })
         $("#tags").autocomplete({source:dataSource})
        });
       
    })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="tags" runat="server"></asp:TextBox>
    </div>
    <div>
        <input id="Button1" type="button" value="button" onclick="showMe();" />
    </div>
    </form>
</body>
</html>
