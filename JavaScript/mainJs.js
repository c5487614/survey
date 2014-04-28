function setColor(obj)
{
    if(obj.checked)
    {
        obj.style.backgroundColor="#1fe7ee";
    }
    else obj.style.backgroundColor="";
}

function clearMe()
{
    var i;
    for(i=0;i<document.all.length;i++)
    {
        //alert(document.all[i].type);
        if(document.all[i].type=="checkbox") 
        {
            var obj= document.all[i];
            obj.style.backgroundColor="";
        }
    }
}

function changeMe(obj)
{
    var chobj1=document.getElementById("hcb1");
    var chobj2=document.getElementById("hcb2");
    if(obj.checked) 
    {
        chobj1.checked=false;
        chobj2.checked=false;
        obj.checked=true;
    }
}

