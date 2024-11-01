<%@ EnableSessionState=False Language="VBScript" %>
<%
Option explicit
Response.Buffer = true
%>
<!--
'// AutoWhois
'//
'// Gets Whois records automatically for domains worldwide
'//
'// Copyright (C) 2000 Hexillion Technologies. All rights reserved.
'//
-->
<!-- #include file="AuxFuncs.asp" -->

<html>

<head>
<title>Hexillion AutoWhois</title>
</head>

<body bgcolor="#FFFFFF" text="#000000" vlink="#808080" link="#0000FF">

<%
'// AutoWhois
'// version 2003-09-01
'// 
'// Gets Whois records for any domain by querying a Whois
'// server for the domain's top-level domain (TLD).
'//
'// Inputs:
'// domain - the domain name for which to get records
'//
'// Outputs:
'// In addition to displaying the Whois output, this script
'// caches the whois server list using two Application variables:
'// wiServers
'// wiLastUpdate
'//
'// File dependencies:
'// AuxFuncs.asp
'// WhoisList - a list of Whois servers by TLD maintained by 
'//     GeekTools at http://www.geektools.com/software.html
'//     It's the list for the 3.x proxy.
'//
'// 
'// History:
'// 2003-09-01 Fixed: Whois server response wasn't HTML-encoded
'// 2003-01-27 Changed special case handling for .ORG
'// 2001-05-03 Fixed: missing CRLF termination of queries
'// 2000-10-05 Initial version
'//
'// Copyright 2000 Hexillion Technologies. All rights reserved.
'// 
'// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
'// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
'// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTIBILITY AND/OR
'// FITNESS FOR A PARTICULAR PURPOSE.


const sDefaultDom = "excite.com"  '// default domain to place in form
const lTimeout = 15000            '// time limit for Whois queries
const sRegWhoisTag = "Whois Server:"  '// text in Internic output that
                                      '// precedes registrar whois server

const sCacheName = "wiServers"    '// App variable name for server table
const sCacheDate = "wiLastUpdate" '// App variable name for last update time
const sCacheInterval = "d"        '// These two control time between
const lCacheInterval = 1          '// updates of the Whois server list
                                  '// cache. They're parms for DateAdd.
                                  
const lCacheSize = 401            '// Size of hash table used to cache
                                  '// whois server list. Should be prime
                                  '// number comfortably larger than
                                  '// number of list entries.

dim sListPath                     '// Pathname of Whois server list
sListPath = server.MapPath( "WhoisList" )

dim oLkup, oTcpq
dim sDomain, bExec

'// Create objects
set oLkup = Server.CreateObject( "Hexillion.HexLookup" )
set oTcpq = Server.CreateObject( "Hexillion.HexTcpQuery" )

'// Check for form input, set defaults
bExec = true  '// default to executing full script
sDomain = Request( "domain" )
if 0 = len( sDomain ) then
	sDomain = sDefaultDom
	bExec = false  '// using default, so only show form
end if
%>

<table cellpadding="5" width="100%"><tr>
<td colspan="2" bgcolor="#E1EFFF"><font face="Arial" size="5"><strong>AutoWhois</strong></font>
<br>Gets Whois records automatically for domains worldwide</td></tr>
<tr>
<td valign="top" bgcolor="#E1EFFF">
<form method="POST" action="<%= request( "SCRIPT_NAME" ) %>" id=form1 name=form1>
	<table cellpadding="5">
		<tr>
			<td>www. <input type="text" name="domain" size="30" value="<%= server.HTMLEncode( sDomain ) %>"> 
			<input type="submit" value="Go" name="B1"></td>
		</tr>
	</table>
</form>
</td>

<td valign="top" bgcolor="#E1EFFF">
<table border="0" cellspacing="0" cellpadding="7">
<tr>
	<td colspan="2">powered by <b><a href="http://www.hexillion.com/software/" target="_top">HexGadgets</a></b>
	<br><font size="-1">
	<a href="http://www.hexillion.com/samples/view_src.asp?name=AutoWhois.vbs.asp" target="_blank">view source</a>
	&nbsp; |&nbsp; <a href="http://www.hexillion.com/samples/" target="_top">download</a>
	</font></td>
</tr>
<%
WriteLicenseRow "HexTcpQuery", oTcpq
WriteLicenseRow "HexLookup", oLkup
%>
</table>
</td>
</tr></table>

<%
Response.Flush

if bExec then
	Main
end if

set oLkup = nothing
set oTcpq = nothing

'// End of global script

sub Main()
	'// Condition input
	sDomain = trim( sDomain )
	
	'// Get TLD
	dim sTLD
	sTLD = GetTLD( sDomain )
	
	'// Look up Whois server for this TLD
	dim sServer
	sServer = GetServer( sTLD )
	
	'// If we didn't find a server in the list...
	if 0 = len( sServer ) then
		Response.Write "<p>Could not find a Whois server for <tt><b>"
		Response.Write Server.HTMLEncode( sTLD )
		Response.Write "</b></tt> in the server list.<br>Not all TLD "
		Response.Write "registries provide Whois servers.</p>" & vbcrlf
		exit sub
	end if
	
	'// Handle special cases
	dim sQuery, bTwoQueryTLD
	
	if "com" = sTLD or "net" = sTLD or "edu" = sTLD then
		sQuery = "dom " & sDomain
		bTwoQueryTLD = true
		
	elseif "org" = sTLD then
		sQuery = sDomain
		bTwoQueryTLD = true
	else
		sQuery = sDomain
	end if

	'// Do the query
	dim sResponse
	sResponse = QueryWhois( sServer, sQuery )
	
	'// If that was a successful InterNIC query...
	if bTwoQueryTLD and 0 <> len( sResponse ) then
	
		'// Find the registrar's whois server in the response
		dim lPos, lEnd
		lPos = instr( 1, sResponse, sRegWhoisTag )
		
		'// If it's there...
		if lPos > 0 then
			
			'// Pull it out
			lPos = lPos + len( sRegWhoisTag )
			lEnd = instr( lPos, sResponse, vbcrlf )
			if lEnd <= 0 then lEnd = len( sResponse ) + 1
			sServer = trim( mid( sResponse, lPos, lEnd - lPos )	)
		
			'// Query registrar's whois server
			QueryWhois sServer, sDomain	
		end if
	end if
	
	'// Tell user we're finished
	Response.Write "<p><tt><b>-- end --</b></tt></p>"		
end sub

'// Pulls TLD out of a domain name
function GetTLD( sDomain )
	dim lLen, lPrev

	lLen = len( sDomain )
	lPrev = instrrev( sDomain, "." )
	
	if 0 = lPrev then
		GetTLD = sDomain
	else
		GetTLD = lcase( right( sDomain, lLen - lPrev ) )
	end if	
end function

'// Gets the Whois server for a TLD
'// The Whois server list is loaded from a file and
'// stored in a hash table in an Application variable
function GetServer( sTLD )
	GetServer = ""	
	dim bCacheExists, aServers, dtLastUpdate
	
	'// Look for Whois server list cache
	Application.Lock
	aServers = application( sCacheName )
	dtLastUpdate = application( sCacheDate )
	Application.UnLock
	bCacheExists = IsArray( aServers )
	
	'// If cache hasn't been loaded or is out of date, reload it
	if not bCacheExists or _
	   (bCacheExists and _ 
	   now() > DateAdd( sCacheInterval, lCacheInterval, dtLastUpdate ) ) then
		
'		Response.Write "<p>Reloading cache...</p>" & vbcrlf
'		Response.Flush
		
		if not ReloadCache() then exit function
		Application.Lock
		aServers = application( sCacheName )
		Application.UnLock
	end if
	
	'// Look for TLD in cached hash table
	dim x, y
	x = Hash1( sTLD )
	y = Hash2( sTLD )
	do while "" <> aServers( x, 0 ) and sTLD <> aServers( x, 0 )
		x = (x + y) mod lCacheSize
	loop
	if "" <> aServers( x, 0 ) then GetServer = aServers( x, 1 )
end function

'// Reloads the Whois server list from a file, creates a
'// hash table, and stores it in an Application variable
function ReloadCache()
	ReloadCache = false
	
	on error resume next
	dim fs, ts
	set fs = Server.CreateObject( "Scripting.FileSystemObject" )	
	set ts = fs.OpenTextFile( sListPath )
	
	if Err then
		Response.Write "<p>Error reading Whois server list: <tt><b>"
		Response.Write Err.description
		Response.Write "</b></tt><br>List name and path: <tt><b>"
		Response.Write sListPath & "</b></tt>" & vbcrlf
		Response.Flush
		exit function
	end if
	
	redim aServers( lCacheSize - 1, 1 )
	dim sLine, sTLD, sServer, lPos, lNext, x, y
	do while not ts.AtEndOfStream
		sLine = ts.ReadLine()
		if Err then exit function
		
		'// Parse out TLD
		lPos = instr( 1, sLine, "|" )
		if lPos <= 0 then lPos = len( sLine ) + 1
		sTLD = mid( sLine, 1, lPos - 1 )
		
		'// Parse out whois server
		lNext = lPos + 1
		lPos = instr( lNext, sLine, "|" )
		if lPos <= 0 then lPos = len( sLine ) + 1
		sServer = mid( sLine, lNext, lPos - lNext )
		
		if "NONE" <> sServer and "WEB" <> sServer then
			'// Insert into hash table
			x = Hash1( sTLD )
			y = Hash2( sTLD )
			do while "" <> aServers( x, 0 )
				x = (x + y) mod lCacheSize
			loop
			aServers( x, 0 ) = sTLD
			aServers( x, 1 ) = sServer
		end if
	loop
		
	ts.close
	set ts = nothing
	set fs = nothing
	
	'// Set the application variables
	Application.Lock
	Application( sCacheName ) = aServers
	Application( sCacheDate ) = now()
	Application.UnLock
	ReloadCache = true
end function

'// Creates hash value for an input string (a TLD in this case)
function Hash1( s )
	dim lHash, i, iMax
	iMax = len( s )
	lHash = 0
	for i = 1 to iMax
		lHash = (128 * lHash + Asc( mid( s, i, 1 ) )) mod lCacheSize
	next
	Hash1 = lHash
end function

'// Creates a small hash value for reducing clustering in the hash table
function Hash2( s )
	dim lHash, i, iMax
	iMax = len( s )
	lHash = 0
	for i = 1 to iMax
		lHash = 16 - ((128 * lHash + Asc( mid( s, i, 1 ) )) mod 16)
	next
	Hash2 = lHash
end function

'// Perfoms a Whois query, displays the results, and 
'// returns the results as a string
function QueryWhois( sServer, sQuery )
	QueryWhois = ""

	'// Get IP address for server
	dim lAddr
	lAddr = oLkup.LookUp( sServer )
	
	'// If no IP addr...
	if 0 = lAddr then
		Response.Write "<p>DNS lookup for <tt><b>"
		Response.Write Server.HTMLEncode( sServer )
		Response.Write "</b></tt> failed:&nbsp; <b>"
		Response.Write GetLkupErrorString( oLkup.Error )
		Response.Write "</b></p>" & vbcrlf
		Response.Flush
		exit function
	end if
	
	'// Give the user some feedback
	Response.Write "<p>Querying <tt><b>"
	Response.Write Server.HTMLEncode( sServer )
	Response.Write " ["
	Response.Write oLkup.AddrToString( lAddr )
	Response.Write "]</b></tt>...</p>" & vbcrlf
	Response.Flush
	
	'// Do the query
	dim sResponse
	oTcpq.RemoteAddr = lAddr
	oTcpq.RemotePort = hexTcpqPortWhois
	oTcpq.Timeout = lTimeout
	sResponse = oTcpq.Query( sQuery & vbcrlf )
	
	'// Write output
	Response.Write "<pre>"
	Response.Write Server.HtmlEncode( sResponse )
	Response.Write "</pre>" & vbcrlf
	
	'// Check for an error
	if hexTcpqErrSuccess <> oTcpq.Error then
		Response.Write "<p>The query returned an error: <b>"
		Response.Write GetTcpqErrorString( oTcpq.Error )
		Response.Write "</b>" & vbcrlf		
	end if
	
	Response.Flush
	QueryWhois = sResponse
end function

'// A brute force check for a prime number
'// Use to ensure lCacheSize is prime
function IsPrime( iCandidate )
	IsPrime = false
	if iCandidate <= 0 then exit function
	
	dim i, iMax
	iMax = iCandidate \ 2
	for i = 3 to iMax step 2
		if 0 = (iCandidate mod i) then exit function
	next
	IsPrime = true
end function
%>
</body>
</html>