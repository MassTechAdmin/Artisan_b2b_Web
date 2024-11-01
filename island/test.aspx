<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="island_test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title>test</title>
    <link rel="stylesheet" type="text/css" href="../css/test/style.css"  />
</head>
<script type="text/javascript">
(function(){
	var vari={
		width:960,
		pics:document.getElementById("pics"),
		prev:document.getElementById("prev"),
		next:document.getElementById("next"),
		len:document.getElementById("pics").getElementsByTagName("li").length,
		intro:document.getElementById("pics").getElementsByTagName("p"),
		now:1,
		step:5,
		dir:null,
		span:null,
		span2:null,
		begin:null,
		begin2:null,
		end2:null,
		move:function(){
			if(parseInt(vari.pics.style.left,10)>vari.dir*vari.now*vari.width&&vari.dir==-1){
				vari.step=(vari.step<2)?1:(parseInt(vari.pics.style.left,10)-vari.dir*vari.now*vari.width)/5;
				vari.pics.style.left=parseInt(vari.pics.style.left,10)+vari.dir*vari.step+"px";
			}
			else if(parseInt(vari.pics.style.left,10)<-vari.dir*(vari.now-2)*vari.width&&vari.dir==1){
				vari.step=(vari.step<2)?1:(-vari.dir*(vari.now-2)*vari.width-parseInt(vari.pics.style.left,10))/5;
				vari.pics.style.left=parseInt(vari.pics.style.left,10)+vari.dir*vari.step+"px";
			}
			else{
				vari.now=vari.now-vari.dir;
				clearInterval(vari.begin);
				vari.begin=null;
				vari.step=5;
				vari.width=960;
			}
		},
		scr:function(){
			if(parseInt(vari.span.style.top,10)>-31){
				vari.span.style.top=parseInt(vari.span.style.top,10)-5+"px";
			}
			else{
				clearInterval(vari.begin2);
				vari.begin2=null;
			}
		},
		stp:function(){
			if(parseInt(vari.span2.style.top,10)<0){
				vari.span2.style.top=parseInt(vari.span2.style.top,10)+10+"px";
			}
			else{
				clearInterval(vari.end2);
				vari.end2=null;
			}
		}
	};
	vari.prev.onclick=function(){
		if(!vari.begin&&vari.now!=1){
			vari.dir=1;
			vari.begin=setInterval(vari.move,20);
		}
		else if(!vari.begin&&vari.now==1){
			vari.dir=-1
			vari.width*=vari.len-1;
			vari.begin=setInterval(vari.move,20);
		};
	};
	vari.next.onclick=function(){
		if(!vari.begin&&vari.now!=vari.len){
			vari.dir=-1;
			vari.begin=setInterval(vari.move,20);
		}
		else if(!vari.begin&&vari.now==vari.len){
			vari.dir=1
			vari.width*=vari.len-1;
			vari.begin=setInterval(vari.move,20);
		};
	};
	for(var i=0;i<vari.intro.length;i++){
		vari.intro[i].onmouseover=function(){
			vari.span=this.getElementsByTagName("span")[0];
			vari.span.style.top=0+"px";
			if(vari.begin2){clearInterval(vari.begin2);}
			vari.begin2=setInterval(vari.scr,20);
		};
		vari.intro[i].onmouseout=function(){
			vari.span2=this.getElementsByTagName("span")[0];
			if(vari.begin2){clearInterval(vari.begin2);}
			if(vari.end2){clearInterval(vari.end2);}
			vari.end2=setInterval(vari.stp,5);
		};
	}
})();
</script>
<body>
    <form id="form1" runat="server">
    <center>
    <div id="swap_pic">
	    <div id="prev" class="scroll">PREV</div>
    <div class="box">
    <ul style="LEFT: 0px" id="pics" class="pics">
      <li>
      <p><img alt="" src="../images/island/test/lol.jpg"></p>
      <p><img alt="" src="../images/island/test/dsz.jpg"></p>
      <p><img alt="" src="../images/island/test/c9.jpg"></p>
      </li>
      <li>
      <p><img alt="" src="../images/island/test/lrtk.jpg"></p>
      <p><img alt="" src="../images/island/test/speed1.jpg"></p>
      <p><img alt="" src="../images/island/test/qqxy.jpg"></p>
      </li>
      </ul>
      </div>
	    <div id="next" class="scroll">NEXT</div>
    </div>
    </center>   
    </form>
</body>
</html>
