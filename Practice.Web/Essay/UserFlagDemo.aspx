<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Import Namespace="Practice.Extension" %>
<%@ Import Namespace="Practice.Types.Flag" %>
<script type="text/C#" runat="server">
    
    public static UserFlag _userFlag = new UserFlag();

    private const string KeyOperation = "op";
    private const string KeyIsHostAdmin = "isHostAdmin";
    private const string KeyIsApproved = "isApproved";
    private const string KeyIsGuest = "isGuest";

    public void Page_Load()
    {
        if (!IsPostBack)
        {
            _userFlag[0] = true;
            _userFlag[1] = true;
            return;
        }

        string op = Request[KeyOperation] ?? "";
        if (op.IsSet() && op.Equals("u", StringComparison.InvariantCultureIgnoreCase))
        {
            UpdateFlag();
        }
    }

    private void UpdateFlag()
    {
        _userFlag.IsHostAdmin = CheckBoxValidate(KeyIsHostAdmin);
        _userFlag.IsApproved = CheckBoxValidate(KeyIsApproved);
        _userFlag.IsGuest = CheckBoxValidate(KeyIsGuest);
    }
    
    private bool CheckBoxValidate(string paramName)
    {
        return Request[paramName] != null && Request[paramName].Trim().Equals("on", StringComparison.InvariantCultureIgnoreCase);
    }

</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Flag Demo</title>
    <link href="<%=ResolveUrl("~/Content/Styles/Site.css") %>" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #editFlag p.row, #flagView p.row {
            margin: 4px 0;
        }
        #editFlag label, #flagView label {
            display: inline-block;
            *display: inline;
            text-align: right;
            width: 100px;
            zoom: 1;
        }
        #flagView {
            margin-top: 20px;
        }
        #editFlag fieldset{
            width: 300px;
        }
        #flagView fieldset {
            width: 300px;
        }
        #editFlag p.brow {
            padding-left: 220px;
        }
        #flagView p.brow {
            padding-left: 220px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <h1>User Flag Demo</h1>
        <div id="editFlag">
            <fieldset>
                <legend>Edit Flag</legend>
                <p class="row">
                    <label for="isHostAdmin">Is Host Admin:</label>
                    <input id="isHostAdmin" name="isHostAdmin" type="checkbox" <%=_userFlag.IsHostAdmin ? "checked='checked'" : "" %> />
                </p>
                <p class="row">
                    <label for="isApproved">Is Approed:</label>
                    <input id="isApproved" name="isApproved" type="checkbox" <%=_userFlag.IsApproved ? "checked='checked'" : "" %> />
                </p>
                <p class="row">
                    <label for="isGuest">Is Guest:</label>
                    <input id="isGuest" name="isGuest" type="checkbox" <%=_userFlag.IsGuest ? "checked='checked'" : "" %> />
                </p>
                <p class="brow">
                    <input id="btnUpdateFlag" type="button" value="更新标识" />
                </p>
            </fieldset>
        </div>
        <div id="flagView">
            <fieldset>
                <legend>Flag View</legend>
                <p class="brow">
                    <input id="btnRefresh" type="button" value="刷新" />
                </p>
                <p class="row">
                    <label>Is Host Admin:</label>
                    <span><%=_userFlag.IsHostAdmin %></span>
                </p>
                <p class="row">
                    <label>Is Approed:</label>
                    <span><%=_userFlag.IsApproved %></span>
                </p>
                <p class="row">
                    <label>Is Guest:</label>
                    <span><%=_userFlag.IsGuest %></span>
                </p>
            </fieldset>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        function updateFlag() {
            var f = document.getElementById("form1");
            var opUpdate = document.createElement("input");
            opUpdate.name = 'op';
            opUpdate.type = 'hidden';
            opUpdate.value = 'u';
            f.appendChild(opUpdate);
            f.submit();
        }

        function refreshPage() {
            var f = document.createElement('form');
            document.getElementsByTagName('body')[0].appendChild(f);
            f.submit();
        }

        (function () {
            document.getElementById('btnUpdateFlag').onclick = updateFlag;
            document.getElementById('btnRefresh').onclick = refreshPage;
        })();
    </script>
</body>
</html>
