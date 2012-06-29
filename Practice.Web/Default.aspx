<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Practice Home</title>
	<link href="<%=ResolveUrl("~/Content/Styles/Site.css") %>" rel="stylesheet" type="text/css" />
	<style type="text/css">
		div.row {
			margin: 8px 0 0 15px;
		}
		.row span {
			font-size: 16px;
			font-weight: bold;
		}
	</style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
		<h1>Index Page</h1>
			<div class="row has_children">
				<span>GridView Sample</span>
				<div class="row">
					<a href="Essay/GridVeiwDemo.aspx" title="GridVeiw Demo">GridVeiw Demo</a>
				</div>
			</div>
			<div class="row has_children">
				<span>Attribute Sampe</span>
				<div class="row">
					<a href="Essay/AttributeDemo.aspx" title="Attribute Demo">Attribute Demo</a>
				</div>
			</div>
			<div class="row has_children">
				<span>AdventrueWorks DB</span>
				<div class="row">
					<a href="AdventureWorks/Contact.aspx" title="Contract Table">Contract Table</a>
				</div>
			</div>
    </div>
    </form>
</body>
</html>
