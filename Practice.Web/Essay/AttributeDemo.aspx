<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="Practice.Web.Sample" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Attribute Demo</title>
	<link href="<%=ResolveUrl("~/Content/Styles/Site.css") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
    	<h3>Attribute Demo</h3>
    	<%=AttributeDemo.TestMethod().Replace("\n", "<br/>") %>
    </div>
    </form>
</body>
</html>
