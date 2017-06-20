<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="BMH.EagleEye.page.test.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnTest" runat="server" Text="测试" OnClick="btnTest_Click" />
        <div style="background-color:#12bdce;padding:10px;margin-top:10px">设置redis</div>
        <div style="padding:20px;border:thin solid gray;margin-top:10px">
            <asp:Label ID="Label1" runat="server" Text="广告id"></asp:Label>
            <asp:TextBox ID="txtAdId" runat="server" ></asp:TextBox>
            <asp:Label ID="Label2" runat="server" Text="推送量"></asp:Label>
            <asp:TextBox ID="txtPutMax" runat="server"></asp:TextBox>
            <asp:Label ID="Label3" runat="server" Text="每日推送量"></asp:Label>
            <asp:TextBox ID="txtPutMaxByDay" runat="server"></asp:TextBox>
            <asp:Button ID="btnPutInfo" runat="server" Text="设置" OnClick="btnPutInfo_Click" />
        </div>
         <div style="padding:20px;border:thin solid gray;margin-top:10px">
            <asp:Label ID="Label4" runat="server" Text="广告主id"></asp:Label>
            <asp:TextBox ID="txtAdUId" runat="server" ></asp:TextBox>           
            <asp:Label ID="Label6" runat="server" Text="每日推送量"></asp:Label>
            <asp:TextBox ID="txtPutMaxByDayOfAdU" runat="server"></asp:TextBox>
            <asp:Button ID="btnPutInfoByAdU" runat="server" Text="设置" OnClick="btnPutInfoByAdU_Click"  />
        </div>

    </div>
    </form>
</body>
</html>
