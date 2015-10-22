<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThriftyBooks.aspx.cs" Inherits="ThriftyBooks.ThriftyBooks" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <link href="ThriftyBooksStyleSheet.css" rel="stylesheet" type="text/css" />
<head runat="server" aria-checked="false">
    <title>Thrifty Books</title>
    </head>
<body style="height: 264px">
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <center>
    <form id="form1" runat="server">
    <div style="height: 241px; width: 253px" id="searchDiv">
        <asp:TextBox ID="txtbxSearchTerm" runat="server" style="margin-bottom: 20px"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="SearchEvent" style="margin-bottom: 20px"/>
        <br />
        <asp:Button ID="btnExpandRent2" runat="server" OnClick="btnExpandRent" Text="See more" Visible ="false" style="margin-bottom: 20px"/>
        <br />
        <asp:Button ID="btnExpandUsed2" runat="server" OnClick="btnExpandUsed" Text="See more" Visible ="false" style="margin-bottom: 20px"/>
        <br />
        <asp:Button ID="btnExpandNew2" runat="server" OnClick="btnExpandNew" Text="See more" Visible ="false"/>
    </div>
    </form>
    </center>
</body>
</html>
