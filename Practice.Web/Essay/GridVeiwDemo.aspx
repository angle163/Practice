<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="Practice.Extension" %>
<%@ Import Namespace="Practice.Web.FakeData" %>
<%@ Import Namespace="Practice.Web.Helper" %>
<%@ Import Namespace="Practice.Web.Model" %>

<script runat="server">
private IList<User> _users;

protected void Page_Load(object sender, EventArgs e)
{
	_users = AppFakeData.Users;
	TipAddUser.InnerHtml = null;
	TipUserGV.InnerHtml = null;
	if (!IsPostBack)
	{
		BindViewUser();
	}
}

private void BindViewUser()
{
	PageHelper.GridViewBindData(gvUser, _users);
}

protected void gvUser_RowEditing(object sender, GridViewEditEventArgs e)
{
	GridView gv = sender as GridView;
	gv.EditIndex = e.NewEditIndex;
	BindViewUser();
}

protected void gvUser_RowUpdating(object sender, GridViewUpdateEventArgs e)
{
	GridView gv = sender as GridView;
	GridViewRow row = gv.Rows[e.RowIndex];
	User user = new User
	(
		new Guid(PageHelper.GetControlViaRow<Label>(row, 0, 1).Text),
		PageHelper.GetControlViaRow<TextBox>(row, 1, 1).Text
	);
	UserDao dao = new UserDao();
	User user2 = dao.Get(user.Username);
	if (user2 != null && user2.Id != user.Id)
	{
		MessageWarn(TipUserGV, "用户名已存在。");
		return;
	}

	bool flag = dao.Update(user);
	if (flag)
	{
		MessageHint(TipUserGV, "用户修改成功。");
	}
	else
	{
		MessageWarn(TipUserGV, "用户修改失败。");
	}
	gvUser.EditIndex = -1;
	BindViewUser();
}

protected void gvUser_RowDeleting(object sender, GridViewDeleteEventArgs e)
{
	GridView gv = sender as GridView;
	GridViewRow row = gv.Rows[e.RowIndex];
	Guid id = new Guid(PageHelper.GetControlViaRow<Label>(row, 0, 1).Text);
	bool flag = new UserDao().Delete(id);
	if (flag)
	{
		MessageHint(TipUserGV, "用户删除成功。");
	}
	else
	{
		MessageWarn(TipUserGV, "用户删除失败。");
	}

	BindViewUser();
	e.Cancel = true;
}

protected void gvUser_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
{
	GridView gv = sender as GridView;
	gv.EditIndex = -1;
	BindViewUser();
}

protected void btnAddUser_Click(object sender, EventArgs e)
{
	string username = txtUsername.Text;
	UserDao dao = new UserDao();
	if (username.IsNotSet())
	{
		MessageWarn(TipAddUser, "请填写用户名!");
		return;
	}

	if (dao.IsExisted(username))
	{
		MessageWarn(TipAddUser, "用户名已存在。");
		return;
	}

	User user = new User(username);
	if (dao.Add(user))
	{
		MessageHint(TipAddUser, "用户添加成功。");
		BindViewUser();
		return;
	}
	MessageWarn(TipAddUser, "用户添加失败。");
}

private void MessageWarn(HtmlGenericControl tag, string message)
{
	//提示警告，显示红色字
	tag.InnerHtml = message;
	PageHelper.RemoveCssClass(tag, "green");
	PageHelper.AddCssClass(tag, "red");
}

private void MessageHint(HtmlGenericControl tag, string message)
{
	//暗示，显示绿色字
	tag.InnerHtml = message;
	PageHelper.RemoveCssClass(tag, "red");
	PageHelper.AddCssClass(tag, "green");
}
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GridView Demo</title>
	<link href="<%=ResolveUrl("~/Content/Styles/Site.css") %>" rel="stylesheet" type="text/css" />
	<style type="text/css">
		p.arow {
			padding: 12px 0 0 20px;
		}
		.red {
			color: red;
		}
		.green {
			color: green;
		}
	</style>
</head>
<body>
	<div id="container">
		<form id="form1" runat="server">
		<div>
		<h1>GridView Demo</h1>
		<h3>添加用户</h3>
		<div>
			<p class="arow">
				<span id="TipAddUser" runat="server"></span>
			</p>
			<p class="arow">
				<label for='<%=txtUsername.ClientID %>'>用户名:</label>
				<asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
				<asp:Button ID="btnAddUser" Text="添加用户" runat="server" onclick="btnAddUser_Click"/>
			</p>
		</div>
		<h3>用户列表</h3>
		<p>
			<span id="TipUserGV" runat="server"></span>
		</p>
		<asp:GridView ID="gvUser" runat="server"
			AutoGenerateColumns="False" 
			onrowediting="gvUser_RowEditing"
			onrowupdating="gvUser_RowUpdating"
			onrowdeleting="gvUser_RowDeleting"
			onrowcancelingedit="gvUser_RowCancelingEdit">
			<Columns>
				<asp:TemplateField HeaderText="编号">
					<EditItemTemplate>
						<asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
					</EditItemTemplate>
					<ItemTemplate>
						<asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="用户名">
					<EditItemTemplate>
						<asp:TextBox ID="txtUsername" runat="server" Text='<%# Bind("Username") %>'></asp:TextBox>
					</EditItemTemplate>
					<ItemTemplate>
						<asp:Label ID="txtUsername" runat="server" Text='<%# Bind("Username") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField ShowHeader="False" HeaderText="操作">
					<EditItemTemplate>
						<asp:LinkButton ID="lbUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="更新"></asp:LinkButton>
						<asp:LinkButton ID="lbCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消"></asp:LinkButton>
					</EditItemTemplate>
					<ItemTemplate>
						<asp:LinkButton ID="lbEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="编辑"></asp:LinkButton>
						<asp:LinkButton ID="lbDelete" CausesValidation="False " CommandName="Delete" Text="删除" runat="server"></asp:LinkButton>
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
			</asp:GridView>
		</div>
		</form>
	</div>
</body>
</html>
