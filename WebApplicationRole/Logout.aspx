<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="WebApplicationRole.Logout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Logging Out</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Logging out...</h2>
            <asp:Literal ID="lblMessage" runat="server" Text="You are being logged out. Please wait..." />
        </div>
    </form>
</body>
</html>
