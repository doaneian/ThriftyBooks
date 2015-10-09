<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThriftyBooks.aspx.cs" Inherits="ThriftyBooks.ThriftyBooks" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" aria-checked="false">
    <title></title>
    </head>
<body style="height: 264px">
    <center>
    <form id="form1" runat="server">
    <div style="height: 241px; width: 253px" id="searchDiv">
        <asp:TextBox ID="txtbxSearchTerm" runat="server" style="margin-bottom: 20px"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="SearchEvent" style="margin-bottom: 20px" />
        <br />
        <asp:Button ID="btnExpandUsed2" runat="server" OnClick="btnExpandUsed" Text="See more" />
    </div>
    </form>
    </center>
</body>
</html>
