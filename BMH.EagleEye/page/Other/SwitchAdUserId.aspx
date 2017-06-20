<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SwitchAdUserId.aspx.cs" Inherits="BMH.EagleEye.page.Other.SwitchAdUserId" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">
        function chkddl(source, args) {
            if (args.Value == "0")
                args.IsValid = false;
            else
                args.IsValid = true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>切换广告主：</h1>
        </div>
        <div>
            当前广告主：<%=bindName %>
        </div>
        <div>
            广告主列表：
            <asp:DropDownList ID="ddlAdUser" runat="server">
                <asp:ListItem Value="0">请选择广告主</asp:ListItem>
            </asp:DropDownList>
            <asp:CustomValidator ID="cvAdUser" runat="server" ErrorMessage="*" ClientValidationFunction="chkddl"
                ControlToValidate="ddlAdUser" ForeColor="#ff0000" Display="Dynamic">←←←请选择广告主</asp:CustomValidator>

            <asp:Button ID="btnOk" runat="server" Text="切换" OnClick="btnOk_Click" />
        </div>

    </form>
</body>
</html>
