<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="Practice.Data.MsSql" %>

<script runat="server">
protected void Page_Load(object sender, EventArgs e)
{
    if (!IsPostBack)
    {
        BindContact();
    }
}

private void BindContact()
{
    string queryString = "SELECT TOP 20 ContactID,FirstName,LastName,EmailAddress FROM Person.Contact";
    DataTable table;
    using (var cmd = new SqlCommand(queryString))
    {
        table = new MsSqlDbAccess().GetData(cmd, true);
    }
    gvContact.AutoGenerateColumns = false;
    gvContact.DataBinding += BindingContactGridView;
    gvContact.DataSource = table;
    gvContact.DataBind();
}

private void BindingContactGridView(object o, EventArgs e)
{
    var gv = o as GridView;
    var fieldId = new BoundField { HeaderText = "ID", DataField = "ContactID" };
    var fieldFirstName = new BoundField { HeaderText = "名字", DataField = "FirstName" };
    var fieldLastName = new BoundField { HeaderText = "姓氏", DataField = "LastName" };

    var button = new ButtonField();
    button.Text = "删除";
    button.HeaderText = "操作";
    gv.Columns.Add(fieldId);
    gv.Columns.Add(fieldFirstName);
    gv.Columns.Add(fieldLastName);
    gv.Columns.Add(button);
}
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AdventrueWorks Contact</title>
	<link href="<%=ResolveUrl("~/Content/Styles/Site.css") %>" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .timeQueries {
            margin: 6px 0;
        }
        .cmd {
            margin-left: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
    	<h3>AdventrueWorks Contact</h3>
        <div class="timeQueries">
            <p> Processed in <%=HttpContext.Current.Items["TimeQueries"] ?? 0 %> second(s), <%=HttpContext.Current.Items["NumQueries"] ?? 0 %> queries. </p>
            <p>Command:</p>
            <p class="cmd"><%=HttpContext.Current.Items["CmdQueries"] ?? "" %></p>
        </div>
		<asp:GridView ID="gvContact" runat="server">
		</asp:GridView>
    </div>
    </form>
</body>
</html>
