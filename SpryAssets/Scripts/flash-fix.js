﻿if(typeof com == "undefined") var com = new Object();
if(typeof com.deconcept == "undefined") com.deconcept = new Object();
if(typeof com.deconcept.util == "undefined") com.deconcept.util = new Object();
if(typeof com.deconcept.FlashObjectUtil == "undefined") com.deconcept.FlashObjectUtil = new Object();

com.deconcept.FlashObject = function(swf, id, w, h, ver, c, useExpressInstall, quality, xiRedirectUrl, redirectUrl, detectKey){
  if (!document.createElement || !document.getElementById) return;
  this.DETECT_KEY = detectKey ? detectKey : 'detectflash';
  this.skipDetect = com.deconcept.util.getRequestParameter(this.DETECT_KEY);
  this.params = new Object();
  this.variables = new Object();
  this.attributes = new Array();
  this.useExpressInstall = useExpressInstall;

  if(swf) this.setAttribute('swf', swf);
  if(id) this.setAttribute('id', id);
  if(w) this.setAttribute('width', w);
  if(h) this.setAttribute('height', h);
  if(ver) this.setAttribute('version', new com.deconcept.PlayerVersion(ver.toString().split(".")));
  this.installedVer = com.deconcept.FlashObjectUtil.getPlayerVersion(this.getAttribute('version'), useExpressInstall);
  if(c) this.addParam('bgcolor', c);
  var q = quality ? quality : 'high';
  this.addParam('quality', q);
  var xir = (xiRedirectUrl) ? xiRedirectUrl : window.location;
  this.setAttribute('xiRedirectUrl', xir);
  this.setAttribute('redirectUrl', '');
  if(redirectUrl) this.setAttribute('redirectUrl', redirectUrl);
}

com.deconcept.FlashObject.prototype = {
  setAttribute: function(name, value){
    this.attributes[name] = value;
  },
  getAttribute: function(name){
    return this.attributes[name];
  },
  addParam: function(name, value){
    this.params[name] = value;
  },
  getParams: function(){
    return this.params;
  },
  addVariable: function(name, value){
    this.variables[name] = value;
  },
  getVariable: function(name){
    return this.variables[name];
  },
  getVariables: function(){
    return this.variables;
  },
  createParamTag: function(n, v){
    var p = document.createElement('param');
    p.setAttribute('name', n);
    p.setAttribute('value', v);
    return p;
  },
  getVariablePairs: function(){
    var variablePairs = new Array();
    var key;
    var variables = this.getVariables();
    for(key in variables){
      variablePairs.push(key +"="+ variables[key]);
    }
    return variablePairs;
  },
  getFlashHTML: function() {
    var flashNode = "";
    if (navigator.plugins && navigator.mimeTypes && navigator.mimeTypes.length) { // netscape plugin architecture
      if (this.getAttribute("doExpressInstall")) this.addVariable("MMplayerType", "PlugIn");
      flashNode = '<embed type="application/x-shockwave-flash" src="'+ this.getAttribute('swf') +'" width="'+ this.getAttribute('width') +'" height="'+ this.getAttribute('height') +'"';
      flashNode += ' id="'+ this.getAttribute('id') +'" name="'+ this.getAttribute('id') +'" ';
      var params = this.getParams();
       for(var key in params){ flashNode += [key] +'="'+ params[key] +'" '; }
      var pairs = this.getVariablePairs().join("&");
       if (pairs.length > 0){ flashNode += 'flashvars="'+ pairs +'"'; }
      flashNode += '/>';
    } else { // PC IE
      if (this.getAttribute("doExpressInstall")) this.addVariable("MMplayerType", "ActiveX");
      flashNode = '<object id="'+ this.getAttribute('id') +'" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="'+ this.getAttribute('width') +'" height="'+ this.getAttribute('height') +'">';
      flashNode += '<param name="movie" value="'+ this.getAttribute('swf') +'" />';
      var params = this.getParams();
      for(var key in params) {
       flashNode += '<param name="'+ key +'" value="'+ params[key] +'" />';
      }
      var pairs = this.getVariablePairs().join("&");
      if(pairs.length > 0) {flashNode += '<param name="flashvars" value="'+ pairs +'" />';}
      flashNode += "</object>";
    }
    return flashNode;
  },
  write2: function(elementId){
    if(this.useExpressInstall) {
     
      var expressInstallReqVer = new com.deconcept.PlayerVersion([6,0,65]);
      if (this.installedVer.versionIsValid(expressInstallReqVer) && !this.installedVer.versionIsValid(this.getAttribute('version'))) {
        this.setAttribute('doExpressInstall', true);
        this.addVariable("MMredirectURL", escape(this.getAttribute('xiRedirectUrl')));
        document.title = document.title.slice(0, 47) + " - Flash Player Installation";
        this.addVariable("MMdoctitle", document.title);
      }
    } else {
      this.setAttribute('doExpressInstall', false);
    }
    if(this.skipDetect || this.getAttribute('doExpressInstall') || this.installedVer.versionIsValid(this.getAttribute('version'))){
      var n = (typeof elementId == 'string') ? document.getElementById(elementId) : elementId;
      n.innerHTML = this.getFlashHTML();
    }else{
      if(this.getAttribute('redirectUrl') != "") {
        document.location.replace(this.getAttribute('redirectUrl'));
      }
    }
  }
}


com.deconcept.FlashObjectUtil.getPlayerVersion = function(reqVer, xiInstall){
  var PlayerVersion = new com.deconcept.PlayerVersion(0,0,0);
  if(navigator.plugins && navigator.mimeTypes.length){
    var x = navigator.plugins["Shockwave Flash"];
    if(x && x.description) {
      PlayerVersion = new com.deconcept.PlayerVersion(x.description.replace(/([a-z]|[A-Z]|\s)+/, "").replace(/(\s+r|\s+b[0-9]+)/, ".").split("."));
    }
  }else{
    try{
      var axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash");
      for (var i=3; axo!=null; i++) {
        axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash."+i);
        PlayerVersion = new com.deconcept.PlayerVersion([i,0,0]);
      }
    }catch(e){}
    if (reqVer && PlayerVersion.major > reqVer.major) return PlayerVersion; 
    if (!reqVer || ((reqVer.minor != 0 || reqVer.rev != 0) && PlayerVersion.major == reqVer.major) || PlayerVersion.major != 6 || xiInstall) {
      try{
        PlayerVersion = new com.deconcept.PlayerVersion(axo.GetVariable("$version").split(" ")[1].split(","));
      }catch(e){}
    }
  }
  return PlayerVersion;
}


com.deconcept.PlayerVersion = function(arrVersion){
  this.major = parseInt(arrVersion[0]) || 0;
  this.minor = parseInt(arrVersion[1]) || 0;
  this.rev = parseInt(arrVersion[2]) || 0;
}


com.deconcept.PlayerVersion.prototype.versionIsValid = function(fv){
  if(this.major < fv.major) return false;
  if(this.major > fv.major) return true;
  if(this.minor < fv.minor) return false;
  if(this.minor > fv.minor) return true;
  if(this.rev < fv.rev) return false;
  return true;
}

com.deconcept.util = {
  getRequestParameter: function(param){
    var q = document.location.search || document.location.hash;
    if(q){
      var startIndex = q.indexOf(param +"=");
      var endIndex = (q.indexOf("&", startIndex) > -1) ? q.indexOf("&", startIndex) : q.length;
      if (q.length > 1 && startIndex > -1) {
        return q.substring(q.indexOf("=", startIndex)+1, endIndex);
      }
    }
    return "";
  }
}


if (Array.prototype.push == null) { Array.prototype.push = function(item) { this[this.length] = item; return this.length; }}


var getQueryParamValue = com.deconcept.util.getRequestParameter;
var FlashObject = com.deconcept.FlashObject;