<%@ Page Language="vb" AutoEventWireup="true" Inherits="Default" EnableViewState="false"
	ValidateRequest="false" CodeBehind="Default.aspx.vb" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v16.1, Version=16.1.17.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
	Namespace="DevExpress.ExpressApp.Web.Templates" TagPrefix="cc3" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v16.1, Version=16.1.17.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.ExpressApp.Web.Controls" TagPrefix="cc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Main Page</title>
	<meta http-equiv="Expires" content="0" />
</head>
<body class="VerticalTemplate">
	<form id="form2" runat="server">
	<cc3:XafUpdatePanel ID="UPPopupWindowControl" runat="server" UpdatePanelForASPxGridListCallback="False">
		<cc4:XafPopupWindowControl runat="server" ID="PopupWindowControl" />
	</cc3:XafUpdatePanel>
	<cc4:ASPxProgressControl ID="ProgressControl" runat="server" />
	<div runat="server" id="Content" />
	</form>
</body>
</html>