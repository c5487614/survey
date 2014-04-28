// JScript 文件
// Dictionaryajax.js

var XMLHttpReq;
var completeDiv;
var inputField;
var completeTable;
var completeBody;
//创建XMLHttpRequest对象      
function createXMLHttpRequest() {
       if(window.XMLHttpRequest) { //Mozilla 浏览器
              XMLHttpReq = new XMLHttpRequest();
       }
       else if (window.ActiveXObject) { // IE浏览器
              try {
                     XMLHttpReq = new ActiveXObject("Msxml2.XMLHTTP");
              } catch(e){
                     try{
                          XMLHttpReq = new ActiveXObject("Microsoft.XMLHTTP");
                     } catch (e) {}
              }
       }
}

//发送匹配请求函数
function findNames(add,wz) {
      inputField = document.getElementById(wz);           
      completeTable = document.getElementById("complete_table");
      completeDiv = document.getElementById("popup");
      completeBody = document.getElementById("complete_body");
      if (inputField.value.length > 0) {
             createXMLHttpRequest();           
             var url = add + escape(inputField.value);    //
             XMLHttpReq.open("GET", url, true);
             XMLHttpReq.onreadystatechange = processMatchResponse;//指定响应函数
             XMLHttpReq.send(null); // 发送请求
      } else {
             clearNames();
      }
}

// 处理返回匹配信息函数
function processMatchResponse() {
      if (XMLHttpReq.readyState == 4) { // 判断对象状态
          if (XMLHttpReq.status == 200) { // 信息已经成功返回，开始处理信息
                   setNames(XMLHttpReq.responseXML.getElementsByTagName("res"));
          }else { //页面不正常
               alert("您所请求的页面有异常。");
          }
      }
}

//生成与输入内容匹配行
function setNames(names) {           
      clearNames();
      var size = names.length-1;
      setOffsets();
      var row, cell, txtNode,footer,footercell;
      for (var i = 0; i < size; i++) {
          var nextNode = names[i].firstChild.nodeValue;
          row = document.createElement("tr");
          cell = document.createElement("td");    
          cell.onmouseout = function() {
              this.style.backgroundColor='#cceeff';
              //this.className='mouseOver';
          };
          cell.onmouseover = function() {
              this.style.backgroundColor='#cccccc';
              //this.className='mouseOut';
          };
          cell.setAttribute("bgcolor", "#ffffff");
          cell.setAttribute("border", "0");
          cell.onclick = function() { 
              completeField(this); 
          } ;
          txtNode = document.createTextNode(nextNode);
          cell.appendChild(txtNode);
          row.appendChild(cell);
          completeBody.appendChild(row);
      }
      var nextNode = names[size].firstChild.nodeValue;
      footer = document.createElement("tr");
      footercell = document.createElement("td");
      footercell.setAttribute("bgcolor", "#ffddcc");
      footercell.setAttribute("border", "0");
      footercell.appendChild(document.createTextNode(nextNode));
      footer.appendChild(footercell);
      completeBody.appendChild(footer);
}



//设置显示位置                
function setOffsets() {
      completeTable.style.width = "auto";    //显示自动完成的提示框宽度自动伸展或缩小
      var left = calculateOffset(inputField, "offsetLeft");
      var top = calculateOffset(inputField, "offsetTop") + inputField. offsetHeight;
      completeDiv.style.border = "black 1px solid";
      completeDiv.style.left = left + "px";
      completeDiv.style.top = top + "px";
}

//计算显示位置
function calculateOffset(field, attr) {
      var offset = 0;
      while(field) {
          offset += field[attr];
          field = field.offsetParent;
      }
      return offset;
}

//填写输入框
function completeField(cell) {      
      inputField.value = cell.firstChild.nodeValue;      
      clearNames();
}

//清除自动完成行
function clearNames() {
      var ind = completeBody.childNodes.length;
      for (var i = ind - 1; i >= 0 ; i--) {
          completeBody.removeChild(completeBody.childNodes[i]);
      }
      completeDiv.style.border = "none";
      //document.getElementById("divCalendar").style.display="none";
}
document.onclick=clearNames;



