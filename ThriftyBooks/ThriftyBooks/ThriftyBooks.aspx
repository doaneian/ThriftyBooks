<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ThriftyBooks.aspx.cs" Inherits="Thrifty.ThriftyBooks" MaintainScrollPositionOnPostback ="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">


    <link href="ThriftyBooksStyleSheet.css" rel="stylesheet" type="text/css" />
<head runat="server" aria-checked="false">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Thrifty Books</title>
    </head>
<header class="mainheader">
    <p id="title">THRIFTYBOOKS.com<p>
    <p id="subtitle"><i>Right place to find books</i></p>
    <nav>
        <a href = "http://www.google.com">Home</a>
        <a href="http://www.facebook.com">Login</a>
    </nav>
</header>
<body class="body">
    <center>
    <form id="form1" runat="server">
    <br />
    <div style="height: 241px; width: 253px; margin-top: 0px;" id="searchDiv">
        <asp:TextBox ID="txtbxSearchTerm" runat="server" Text="" style="margin-bottom: 5px"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="SearchEvent" style="margin-bottom: 5px"/>
        <br />
        <asp:RadioButton ID="rbISBN" runat="server" GroupName="searchType" Text="ISBN"  Checked ="true"/>
        <asp:RadioButton ID="rbTitle" runat="server" GroupName="searchType" Text="Title" />
        <asp:RadioButton ID="rbAuthor" runat="server" GroupName="searchType" Text="Author" />
        <br />
        <asp:Button ID="btnExpandRent2" runat="server" OnClick="btnExpandRent" Text="See more" Visible ="false" style="margin-bottom: 20px"/>
        <br />
        <asp:Button ID="btnExpandUsed2" runat="server" OnClick="btnExpandUsed" Text="See more" Visible ="false" style="margin-bottom: 20px"/>
        <br />
        <asp:Button ID="btnExpandNew2" runat="server" OnClick="btnExpandNew" Text="See more" Visible ="false" style="margin-bottom: 20px"/>
    </div>
    </form>
    </center>
    <img src="book.png" id="book_img">
</body>
</html>
