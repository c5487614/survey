<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <script type="text/javascript" language="javascript" src="JavaScript/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="JavaScript/jquery-ui-1.8.custom.min.js"></script>
    <link rel="Stylesheet" href="CSS/jquery-ui-1.8.custom.css"  />
    <link rel="Stylesheet" href="CSS/demos.css"  />
     <script type="text/javascript">
	$(function() {
		$("#tbox").datepicker();
	});
	$(function() {
		$("#dialog").dialog();
	});
	$(function() {
	    var icons = {
			header: "ui-icon-circle-arrow-e",
			headerSelected: "ui-icon-circle-arrow-s"
		};
		$("#accordion").accordion({
			icons: icons
		});
		$("#accordion").accordion("option", "icons", icons);

	});
	$(function() {
		function log(message) {
			$("<div/>").text(message).prependTo("#log");
			$("#log").attr("scrollTop", 0);
		}
		
		$("#getHelp").autocomplete({
			source: function(request, response) {
				$.ajax({
					url: "http://localhost/srrshTable/tableList.aspx",
					dataType: "xml",
					data: {
						featureClass: "P",
						style: "full",
						maxRows: 12,
						name_startsWith: request.term
					},
					success: function(data) {
						response($.map(data.geonames, function(item) {
							return {
								label: item.name + (item.adminName1 ? ", " + item.adminName1 : "") + ", " + item.countryName,
								value: item.name
							}
						}))
					}
				})
			},
			minLength: 2,
			select: function(event, ui) {
				log(ui.item ? ("Selected: " + ui.item.label) : "Nothing selected, input was " + this.value);
			},
			open: function() {
				$(this).removeClass("ui-corner-all").addClass("ui-corner-top");
			},
			close: function() {
				$(this).removeClass("ui-corner-top").addClass("ui-corner-all");
			}
		});
	});
    $(document).scroll(function(){alert("xx");});

	</script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="demo">
        <asp:TextBox ID="tbox" runat="server"></asp:TextBox>
        
        <asp:TextBox ID="names" runat="server"></asp:TextBox>
    </div>
    <div id="dialog">
    谢谢旺哥请我吃瓜子！
    </div>
    <div id="accordion">
       
        <h2><a>谢谢旺哥请我吃瓜子</a></h2>
        <h2>
        谢谢旺哥请我吃瓜子！
        谢谢旺哥请我吃瓜子！
        谢谢旺哥请我吃瓜子！
        谢谢旺哥请我吃瓜子！
        谢谢旺哥请我吃瓜子！
        </h2>
        <h3><a href="#">Section 3</a></h3>
        <div>
        谢谢旺哥请我吃瓜子！
        谢谢旺哥请我吃瓜子！
        谢谢旺哥请我吃瓜子！
        谢谢旺哥请我吃瓜子！
        谢谢旺哥请我吃瓜子！
        </div>
    </div>
    
     <div id="search">
     
     <input id="getHelp" type="text" />
     
    </div>
    <div id="log" style="height: 200px; width: 300px; overflow: auto;" class="ui-widget-content">
    </div>
    
    <div id="xxx">dddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd
    </div>
    </form>
</body>
</html>
