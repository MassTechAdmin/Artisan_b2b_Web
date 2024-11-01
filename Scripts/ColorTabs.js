function loadtab(n){
var BodyHeight=BoxHeight-32
var BodyWidth =BoxWidth-12
var TabBuilder="<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr>";

getCookie();


var theForm = document.forms['form1'];
if (!theForm) {
    theForm = document.form1;
}

CallServer('Area|'+theForm.DropDownList1.value,_span1);

for(var i=0;i<MyTabs.length;i++){
var TabText  = (MyTabs[i][0].replace(/^[\s]+/g,"")).replace(/[\s]+$/g,"");
var TabWidth = (MyTabs[i][1].replace(/^[\s]+/g,"")).replace(/[\s]+$/g,"");
var TabCss = (i==n)?"stylewhite":"styleblue";
TabBuilder=TabBuilder+"<td width=\"6\" height=\"15\"></td>";
TabBuilder=TabBuilder+"<td width=\""+TabWidth+"\" height=\"15\">";
TabBuilder=TabBuilder+"<span class=\""+TabCss+"\">";
TabBuilder=TabBuilder+"　　<a href=\"javascript:loadtab("+i+");\">"+TabText+"</a>";
TabBuilder=TabBuilder+"</span>";
TabBuilder=TabBuilder+"<br /><br />";
TabBuilder=TabBuilder+"</td>";
}
TabBuilder=TabBuilder+"</tr>";
TabBuilder=TabBuilder+"<tr>";
TabBuilder=TabBuilder+"<td width=\"6\" height=\""+BodyHeight+"\"><img width=\"6\" height=\"6\" src=\"Blank.gif\"></td>";
TabBuilder=TabBuilder+"<td width=\""+BodyWidth+"\" height=\""+BodyHeight+"\" colspan=\""+(MyTabs.length*2+1)+"\" valign=\"top\"><div class=\"stylenormal\">"+MyMsgs[n]+"</div></td>";
TabBuilder=TabBuilder+"<td width=\"6\" height=\""+BodyHeight+"\"><img width=\"6\" height=\"6\" src=\"Blank.gif\"></td>";
TabBuilder=TabBuilder+"</tr>";
TabBuilder=TabBuilder+"</table>";
if(document.all){
paper.innerHTML=TabBuilder;
}else{
//with(document.paper.document){
//open();
//document.write(TabBuilder);
//close();
//}
document.getElementById('paper').innerHTML = TabBuilder;
}
}
