////下载数据到图片
var XMLhttp;
function createHttpRequest()
{
   if(window.XMLHttpRequest) { //Mozilla 浏览器
              XMLhttp = new XMLHttpRequest();
       }
       else if (window.ActiveXObject) { // IE浏览器
              try {
                     XMLhttp = new ActiveXObject("Msxml2.XMLHTTP");
              } catch(e){
                     try{
                          XMLhttp = new ActiveXObject("Microsoft.XMLHTTP");
                     } catch (e) {}
              }
       }
    
}
function loaddata(dept,date1)
{

    createHttpRequest();
    var url="data.aspx?";
    var para="dept="+dept+"&date="+date1+"&ts="+Math.random().toString();
    url+=para;
    //alert(url);
    url=encodeURI(url);

    XMLhttp.onreadystatechange=getResponse;
    XMLhttp.open("GET", url, true);
    XMLhttp.send(null);
}
function getResponse()
{
    if (XMLhttp.readyState == 4) { // 判断对象状态
          if (XMLhttp.status == 200) { // 信息已经成功返回，开始处理信息
                   postdata(XMLhttp.responseXML.getElementsByTagName("datas"));
          }else { //页面不正常
               alert("您所请求的页面有异常。");
          }
      }
}
function postdata(result)
{
    var docs=result[0].childNodes[0].text;
    var scores=result[0].childNodes[1].text;
    if(docs==null||docs=="undefined"){alert("没有数据或网络出错。"); return ;}
    AF.func("SetProp","chart\r\n1");//柱状图
    AF.func("SetProp","textContent\r\n3");//数值
    AF.func("SetProp","textArrange\r\n1");//外部且对其
    AF.func("AddSeries", "病历分数图" +"\r\n"+ docs +"\r\n"+ scores); 
}