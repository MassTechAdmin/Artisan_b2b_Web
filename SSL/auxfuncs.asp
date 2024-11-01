<%
'// HexIcmp error codes
const hexIcmpErrSuccess = 0
const hexIcmpErrLicenseFileNotFound = 2
const hexIcmpErrCouldNotOpenLicenseFile = 3
const hexIcmpErrCorruptLicenseFile = 4
const hexIcmpErrWrongProductLicense = 5
const hexIcmpErrWrongVersionLicense = 6
const hexIcmpErrUnlicensedProcessors = 7
const hexIcmpErrBufTooSmall = 11001
const hexIcmpErrDestNetUnreachable = 11002
const hexIcmpErrDestHostUnreachable = 11003
const hexIcmpErrDestProtUnreachable = 11004
const hexIcmpErrDestPortUnreachable = 11005
const hexIcmpErrNoResources = 11006
const hexIcmpErrBadOption = 11007
const hexIcmpErrHwError = 11008
const hexIcmpErrPacketTooBig = 11009
const hexIcmpErrReqTimedOut = 11010
const hexIcmpErrBadReq = 11011
const hexIcmpErrBadRoute = 11012
const hexIcmpErrTtlExpiredTransit = 11013
const hexIcmpErrTtlExpiredReassm = 11014
const hexIcmpErrParamProblem = 11015
const hexIcmpErrSourceQuench = 11016
const hexIcmpErrOptionTooBig = 11017
const hexIcmpErrBadDestination = 11018
const hexIcmpErrAddrDeleted = 11019
const hexIcmpErrSpecMtuChange = 11020
const hexIcmpErrMtuChange = 11021
const hexIcmpErrUnload = 11022
const hexIcmpErrAddrAdded = 11023
const hexIcmpErrGeneralFailure = 11050


'// HexLookup error codes
const hexLuErrSuccess = 0
const hexLuErrLicenseFileNotFound = 2
const hexLuErrCouldNotOpenLicenseFile = 3
const hexLuErrCorruptLicenseFile = 4
const hexLuErrWrongProductLicense = 5
const hexLuErrWrongVersionLicense = 6
const hexLuErrUnlicensedProcessors = 7
const hexLuErrNetworkDown = 10050
const hexLuErrHostNotFound = 11001
const hexLuErrTryAgain = 11002
const hexLuErrNoRecovery = 11003
const hexLuErrNoData = 11004


'// HexTcpQuery well-known port numbers
const hexTcpqPortEcho = 7
const hexTcpqPortDiscard = 9
const hexTcpqPortSystat = 11
const hexTcpqPortDaytime = 13
const hexTcpqPortNetstat = 15
const hexTcpqPortQotd = 17
const hexTcpqPortChargen = 19
const hexTcpqPortFtp = 21
const hexTcpqPortTelnet = 23
const hexTcpqPortSmtp = 25
const hexTcpqPortTime = 37
const hexTcpqPortWhois = 43
const hexTcpqPortFinger = 79
const hexTcpqPortHttp = 80
const hexTcpqPortPop3 = 110
const hexTcpqPortNntp = 119


'// HexTcpQuery error codes
const hexTcpqErrSuccess = 0
const hexTcpqErrLicenseFileNotFound = 2
const hexTcpqErrCouldNotOpenLicenseFile = 3
const hexTcpqErrCorruptLicenseFile = 4
const hexTcpqErrWrongProductLicense = 5
const hexTcpqErrWrongVersionLicense = 6
const hexTcpqErrUnlicensedProcessors = 7
const hexTcpqErrAddressNotAvailable = 10049
const hexTcpqErrNetworkDown = 10050
const hexTcpqErrNetworkUnreachable = 10051
const hexTcpqErrConnectionAborted = 10053
const hexTcpqErrConnectionReset = 10054
const hexTcpqErrTimedOut = 10060
const hexTcpqErrConnectionRefused = 10061
const hexTcpqErrHostUnreachable = 10065


function GetLicenseType( lLicProcessors )
	select case lLicProcessors
		case -1
			GetLicenseType = "Site"
		case -2
			GetLicenseType = "Enterprise"
		case else
			GetLicenseType = lLicProcessors & "-processor"
	end select
end function


function GetLicenseErrorString( lErr )
	select case lErr
		case 0
			GetLicenseErrorString = "Success"
		case 2
			GetLicenseErrorString = "License file not found"
		case 3
			GetLicenseErrorString = "Could not open license file"
		case 4
			GetLicenseErrorString = "License file is corrupt"
		case 5
			GetLicenseErrorString = "License is for wrong product"
		case 6
			GetLicenseErrorString = "License is for wrong version"
		case 7
			GetLicenseErrorString = "Running on machine with unlicensed processors"
		case else
			GetLicenseErrorString = "Unknown error: " & lErr
	end select
end function


sub WriteLicenseRow( sName, obj )
	Response.Write "<tr>" & vbcrlf
	Response.Write "<td valign=""top""><font size=""2""><b>" & sName & "</b> " & obj.Version & "</font></td>" & vbcrlf
	Response.Write "<td valign=""top"">" & vbcrlf

	if 0 <> obj.Error then
		Response.Write "<font size=""2"">" & vbcrlf
		Response.Write GetLicenseErrorString( obj.Error ) & "<br>" & vbcrlf
		Response.Write "Evaluation expires " & obj.Expires & vbcrlf
		Response.Write "</font>" & vbcrlf
	else
		Response.Write "<font size=""2"">" & vbcrlf
		Response.Write GetLicenseType( obj.LicensedProcessors ) & " license<br>" & vbcrlf
		Response.Write obj.LicensedUser & vbcrlf
		Response.Write "</font>" & vbcrlf
	end if
	Response.Write "</td></tr>" & vbcrlf
end sub
    

sub WriteLicenseTable( obj, lLicError )
%>
	<table cellpadding="2">
	<tr><td align="right"><font size="2">powered by</font></td>
	<td><font size="2"><strong><a href="http://www.hexillion.com/">Hexillion</a></strong></font></td></tr>
	<tr><td align="right"><font size="2">component version</font></td>
	<td><font size="2"><strong><%= obj.Version %></font></strong></td></tr>
	<%
	if 0 = lLicError then
	%>
	<tr><td align="right"><font size="2">licensed to</font></td>
	<td><font size="2"><strong><%= Server.HTMLEncode( obj.LicensedUser ) %></strong></font></td></tr>
	<tr><td align="right"><font size="2">license type</font></td>
	<td><font size="2"><strong><%= GetLicenseType( obj.LicensedProcessors ) %></strong></font></td></tr>
	<%
	else
	%>
	<tr><td align="right"><font size="2">evaluation expires</font></td>
	<td><font size="2"><strong><%= FormatDateTime( obj.Expires ) %></strong></font></td></tr>
	<tr><td align="right"><font size="2">license error</font></td>
	<td><font size="2"><strong><%= GetLicenseErrorString( lLicError ) %></strong></font></td></tr>
	<%
	end if
	%>
	</table>
<%
end sub


function GetIcmpErrorString( lErr )
	select case lErr

		case hexIcmpErrReqTimedOut
			GetIcmpErrorString = "Timed out"

		case hexIcmpErrTtlExpiredTransit
			GetIcmpErrorString = "TTL expired in transit"

		case hexIcmpErrDestNetUnreachable
			GetIcmpErrorString = "Destination network unreachable"

		case hexIcmpErrDestHostUnreachable
			GetIcmpErrorString = "Destination host unreachable"

		case hexIcmpErrDestProtUnreachable
			GetIcmpErrorString = "Destination protocol unreachable"

		case hexIcmpErrDestPortUnreachable
			GetIcmpErrorString = "Destination port unreachable"

		case hexIcmpErrSourceQuench
			GetIcmpErrorString = "Source quench"

		case hexIcmpErrPacketTooBig
			GetIcmpErrorString = "Packet too big"

		case hexIcmpErrBufTooSmall
			GetIcmpErrorString = "Buffer too small"

		case hexIcmpErrNoResources
			GetIcmpErrorString = "No resources"

		case hexIcmpErrHwError
			GetIcmpErrorString = "Hardware error"

		case hexIcmpErrBadReq
			GetIcmpErrorString = "Bad request"

		case hexIcmpErrTtlExpiredReassm
			GetIcmpErrorString = "TTL expired in reassembly"

		case hexIcmpErrParamProblem
			GetIcmpErrorString = "Parameter problem"

		case hexIcmpErrBadDestination
			GetIcmpErrorString = "Bad destination"

		case hexIcmpErrGeneralFailure
			GetIcmpErrorString = "General failure"

		case else
			GetIcmpErrorString = GetLicenseErrorString( lErr )
	end select
end function


function GetLkupErrorString( lErr )
	select case lErr

		case hexLuErrHostNotFound
			GetLkupErrorString = "Host not found"

		case hexLuErrTryAgain
			GetLkupErrorString = "Try again"

		case hexLuErrNetworkDown
			GetLkupErrorString = "Network down"

		case hexLuErrNoRecovery
			GetLkupErrorString = "No recovery"

		case hexLuErrNoData
			GetLkupErrorString = "No data"

		case else
			GetLkupErrorString = GetLicenseErrorString( lErr )
	end select
end function


function GetTcpqErrorString( lErr )
	select case lErr

		case hexTcpqErrTimedOut
			GetTcpqErrorString = "Timed out"

		case hexTcpqErrConnectionRefused
			GetTcpqErrorString = "Connection refused"

		case hexTcpqErrConnectionReset
			GetTcpqErrorString = "Connection reset"

		case hexTcpqErrHostUnreachable
			GetTcpqErrorString = "Host unreachable"

		case hexTcpqErrAddressNotAvailable
			GetTcpqErrorString = "Address not available"

		case hexTcpqErrNetworkDown
			GetTcpqErrorString = "Network down"

		case hexTcpqErrNetworkUnreachable
			GetTcpqErrorString = "Network unreachable"

		case hexTcpqErrConnectionAborted
			GetTcpqErrorString = "Connection aborted"

		case else
			GetTcpqErrorString = GetLicenseErrorString( lErr )
	end select
end function
%>

<script language="jscript" runat="server">
function GetUtcString()
	{
	var d;
	d = new Date();
	return d.toUTCString();
	}
</script>
