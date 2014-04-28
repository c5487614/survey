////js计算病历得分
////
////
function getSum(tbboxsum,typeStr)
{
    var sum=100;
    var gv=document.getElementById("GridView1");
    var items=gv.getElementsByTagName("input");
    for(var i=0;i<items.length;i++)
    {
        if(items[i].type==typeStr)
        {
            if(items[i].checked)
            {
                sum-=parseFloat(items[i].value);
            } 
        }
    }
    tbboxsum.value=sum;
}

function getAdd(tbboxsum,typeStr)
{
    var sum=0;
    var gv=document.getElementById("GridView1");
    var items=gv.getElementsByTagName("input");
    for(var i=0;i<items.length;i++)
    {
        if(items[i].type==typeStr)
        {
            if(items[i].checked)
            {
                sum+=parseFloat(items[i].value);
            } 
        }
    }
    tbboxsum.value=sum;
    return true;
}