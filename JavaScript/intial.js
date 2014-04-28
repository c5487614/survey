
var dataSource = new Array();

    $(document).ready(function()
    {
        $("#attends input").keyup(function(){
                        var py=$(this).val();
                        var url="../AJAX/getNames.aspx?py="+py+"%&ts="+Math.random();
                        $.get(url,function(data){
                              var names=data.getElementsByTagName("name");
                              dataSource.length=0;
                              for(var i=0;i<names.length;i++)
                              {
                                   dataSource.push(names[i].text);
                                   //alert(names[i].text);
                              }
                       })
                     $("#attends input").autocomplete({source:dataSource});  
         });
         //$("#GridView1 tr").css("backgroundColor","");
         $("#GridView1 tr").mouseover(function(){
                           $(this).addClass("ui-state-hover");
         }).mouseout(function(){
                           $(this).removeClass("ui-state-hover");
         });
         $("#previewButton").toggle(hideOne,showOne) ;
  
    });
    function hideOne()
    {
         $("#GridView1 tr").each(function(){
                            //var checkBox1=$(this).children("input");
                            //alert(this.id);
                            var s=$(this).find("input:last");
                            if(!s.attr("checked"))
                            {
                                $(this).hide();
                            }
                       });
        $("#previewButton").val("取消预览");
        document.documentElement.scrollTop=0;
    }
     function showOne()
    {
         $("#GridView1 tr").each(function(){
                            $(this).show();
                       })
         $("#previewButton").val("预览");
         document.documentElement.scrollTop=0;
    }
   
