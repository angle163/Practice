<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="Practice.Helper" %>
<%@ Import Namespace="Practice.Web.Helper" %>

<script runat="server">
protected void Page_Load(object sender, EventArgs e)
{
	ResultMessage.InnerHtml = null;
}

protected void CaptchaValidateClick(object sender, EventArgs e)
{
	string captchaText = txtCaptcha.Text;
	if (!CaptchaHelper.IsValid(captchaText))
	{
		MessageWarn("验证码错误。");
		return;
	}
	MessageHint("验证码正确。");
}

private void MessageWarn(string msg)
{
	ResultMessage.InnerHtml = msg;
	PageHelper.RemoveCssClass(ResultMessage, "green");
	PageHelper.AddCssClass(ResultMessage, "red");
}

private void MessageHint(string msg)
{
	ResultMessage.InnerHtml = msg;
	PageHelper.RemoveCssClass(ResultMessage, "red");
	PageHelper.AddCssClass(ResultMessage, "green");
}
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Captcha Demo</title>
	<style type="text/css">
		body, p {
			margin: 0;
			padding: 0;
		}
		#container {
			margin: 0 auto;
			width: 980px;
		}
		p.arow {
			padding: 12px 0 0 20px;
		}		
		p.brow {
			margin-left: 63px;
		}
		.red {
			color: red;
		}
		.green {
			color: green;
		}
		.captcha {
			vertical-align: text-bottom;
		}
	</style>
	<script type="text/javascript">
		function changeCaptcha() {
			var pattern = /t=\d*/,
		        timestamp = new Date().getTime(),
				captcha = document.getElementById('captchaImage'),
			    captchaBox = document.getElementById('<%=txtCaptcha.ClientID %>');

			captcha.src = pattern.test(captcha.src)
								? captcha.src.replace(pattern, 't=' + timestamp)
								: captcha.src + '&t=' + timestamp;

			captchaBox.focus();
		}
	</script>
</head>
<body>
	<div id="container">
		<form id="form1" runat="server">
		<div>
			<h1>Captcha Test</h1>
			<h3>Validate Captcha</h3>
			<p class="arow">
				<span id="ResultMessage" runat="server"></span>
			</p>
			<p class="arow">
    			<img id="captchaImage" class="captcha" alt="验证码" src="Captcha.jpg?t=1" onclick="changeCaptcha();"/>
				<a href="#captcha" onclick="changeCaptcha();return false;">看不清? 换一张</a>
			</p>
    		<p class="arow">
				<label for='<%=txtCaptcha.ClientID %>'>验证码:</label>
				<asp:TextBox ID="txtCaptcha" runat="server"></asp:TextBox>
			</p>
			<p class="arow brow">
				<asp:Button ID="btnValidate" Text="Validate" runat="server" onclick="CaptchaValidateClick"/>
			</p>
		</div>
		</form>
	</div>
</body>
</html>
