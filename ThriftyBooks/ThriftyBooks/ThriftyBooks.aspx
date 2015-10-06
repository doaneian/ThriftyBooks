<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThriftyBooks.aspx.cs" Inherits="ThriftyBooks.ThriftyBooks" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 264px">
    <center>
    <form id="form1" runat="server">
    <div style="height: 173px; width: 253px">
        <asp:TextBox ID="txtbxSearchTerm" runat="server"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="SearchEvent" />
        <asp:Table ID="tblRent" runat="server" Height="30px" Width="180px">
        </asp:Table>
    </div>
    </form>
    </center>
</body>
</html>
