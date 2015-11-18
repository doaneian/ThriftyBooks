<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ThriftyBooks.aspx.cs" Inherits="Thrifty.ThriftyBooks" MaintainScrollPositionOnPostback ="true" EnableEventValidation ="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <link href="ThriftyBooksStyleSheet.css" rel="stylesheet" type="text/css" />
<head runat="server" aria-checked="false">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="icon" type="image/png" href="laptopbook.png" />
    <title>Thrifty Books</title>
    <style type="text/css">
        .auto-style10 {
            margin-top: 20px;
            cursor: pointer;
            margin-right: 9px;
        }
        </style>
    </head>
<body class="body">
    <table style="top: 20px;" border="0">
    <tr>
        <td colspan="4">
            <img src="header_image.jpg" style="height: 200px; width: 100%; min-width: 1200px; opacity: .6" />
            <p id="title" style="font-size: 80px; font-family: 'Arial Rounded MT'; left: 20px; top: -20px">THRIFTYBOOKS.com</p>
            <p id="subtitle" style="left: 800px; top: 140px; font-size: 20px; font-family: 'Segoe Script'; width: 350px"><i>The right place to find books</i></p>
        </td>
    </tr>    
    <tr>
        <td style="height: 285px; width: 226px; vertical-align:top">
            <table>
                <tr>
                    <td><asp:Image ID="Image1" runat="server" ImageUrl="laptopbook.png" Width="200px" style="margin-left: 20px" /></td>
                </tr>
            </table>             
        </td>
        <td style="width: 137px; height: 285px"></td>
        <td style= "width: 981px; vertical-align: top">
        <form id="form1" runat="server">
        <div style="margin-top: 0px; align-content:center" id="searchDiv" width: "800px">
            <asp:table runat="server" id="searchBarTable" Width="800px" CellPadding="0">
                <asp:TableRow  runat="server" HorizontalAlign ="Center">
                    <asp:TableCell runat="server">
                        <asp:TextBox ID="txtbxSearchTerm" runat="server" Text="" style="margin-bottom: 5px; " Width="720px"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="SearchEvent" style="margin-bottom: 5px" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow  runat="server">
                    <asp:TableCell runat="server">
                        <asp:RadioButton ID="rbISBN" runat="server" GroupName="searchType" Text="ISBN"  Checked ="true" style="margin-right: 5px" />
                        <asp:RadioButton ID="rbTitle" runat="server" GroupName="searchType" Text="Title" style="margin-right: 5px" />
                        <asp:RadioButton ID="rbAuthor" runat="server" GroupName="searchType" Text="Author" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:table>   
            <asp:table ID="bookResult" runat="server" Visible="false" Width="500px" GridLines="Horizontal" BackColor="#FFFFFF" CellPadding="5" BorderWidth="1px" Height="101px" CssClass="auto-style10">
                <asp:TableRow ID="bookResultRow1" onclick="bookTableClick()">
                    <asp:TableCell Width="130px"><asp:Image ID="bookImage1" Width="120px" runat="server" /></asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:Label ID="lTitle1" runat="server" Text="Label" Font-Size="20" ></asp:Label>
                        <br />
                        <asp:Label ID="lAuthor1" runat="server" Text="Label" Font-Size="12"></asp:Label>
                        <br />
                        <asp:Label ID="lEdition1" runat="server" Text="Label" Font-Size="12"></asp:Label>
                        <br />
                        <asp:Label ID="lISBN1" runat="server" Text="Label" Font-Size="12px"></asp:Label>
                        <br />
                        <asp:Button ID="moreResultsButton" runat="server" Text="More Results" style="margin-top: 20px" OnClientClick="bookTableClick()" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="bookResultRow2" onclick="selectBookClick(1)">
                    <asp:TableCell Width="130px"><asp:Image ID="bookImage2" Width="120px" runat="server" /></asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:Label ID="lTitle2" runat="server" Text="Label" Font-Size="20"></asp:Label>
                        <br />
                        <asp:Label ID="lAuthor2" runat="server" Text="Label" Font-Size="12"></asp:Label>
                        <br />
                        <asp:Label ID="lEdition2" runat="server" Text="Label" Font-Size="12"></asp:Label>
                        <br />
                        <asp:Label ID="lISBN2" runat="server" Text="Label" Font-Size="12px"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="bookResultRow3" onclick="selectBookClick(2)">
                    <asp:TableCell Width="130px"><asp:Image ID="bookImage3" Width="120px" runat="server" /></asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:Label ID="lTitle3" runat="server" Text="Label" Font-Size="20" ></asp:Label>
                        <br />
                        <asp:Label ID="lAuthor3" runat="server" Text="Label" Font-Size="12"></asp:Label>
                        <br />
                        <asp:Label ID="lEdition3" runat="server" Text="Label" Font-Size="12"></asp:Label>
                        <br />
                        <asp:Label ID="lISBN3" runat="server" Text="Label" Font-Size="12px"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="bookResultRow4" onclick="selectBookClick(3)">
                    <asp:TableCell Width="130px"><asp:Image ID="bookImage4" Width="120px" runat="server" /></asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:Label ID="lTitle4" runat="server" Text="Label" Font-Size="20" ></asp:Label>
                        <br />
                        <asp:Label ID="lAuthor4" runat="server" Text="Label" Font-Size="12"></asp:Label>
                        <br />
                        <asp:Label ID="lEdition4" runat="server" Text="Label" Font-Size="12"></asp:Label>
                        <br />
                        <asp:Label ID="lISBN4" runat="server" Text="Label" Font-Size="12px"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="bookResultRow5" onclick="selectBookClick(4)">
                    <asp:TableCell Width="130px"><asp:Image ID="bookImage5" Width="120px" runat="server" /></asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:Label ID="lTitle5" runat="server" Text="Label" Font-Size="20" ></asp:Label>
                        <br />
                        <asp:Label ID="lAuthor5" runat="server" Text="Label" Font-Size="12"></asp:Label>
                        <br />
                        <asp:Label ID="lEdition5" runat="server" Text="Label" Font-Size="12"></asp:Label>
                        <br />
                        <asp:Label ID="lISBN5" runat="server" Text="Label" Font-Size="12px"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="bookResultRow6">
                    <asp:TableCell Width="130px"><asp:Image ID="bookImage6" Width="120px" runat="server" /></asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:Label ID="lTitle6" runat="server" Text="Label" Font-Size="20" onclick="selectBookClick(5)"></asp:Label>
                        <br />
                        <asp:Label ID="lAuthor6" runat="server" Text="Label" Font-Size="12"></asp:Label>
                        <br />
                        <asp:Label ID="lEdition6" runat="server" Text="Label" Font-Size="12"></asp:Label>
                        <br />
                        <asp:Label ID="lISBN6" runat="server" Text="Label" Font-Size="12px"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:table>
            <br />
            <asp:Button ID="btnExpandRent2" runat="server" OnClick="btnExpandRent" Text="See more" Visible ="false" style="margin-bottom: 20px; margin-top: 20px" />
            <br />
            <asp:Button ID="btnExpandUsed2" runat="server" OnClick="btnExpandUsed" Text="See more" Visible ="false" style="margin-bottom: 20px; margin-top: 20px" />
            <br />
            <asp:Button ID="btnExpandNew2" runat="server" OnClick="btnExpandNew" Text="See more" Visible ="false" style="margin-bottom: 20px; margin-top: 20px" />
        </div>
        <div id="bookResults" runat="server" Visible="false">
        </div>
        </form>
        </td>
        <td style="width: 387px; height: 285px; vertical-align:top">
            <table border="0">
                <tr>
                    <td><a href="http://www.facebook.com/ThriftyBooks" target="_blank"><asp:Image ID="Facebook" runat="server" ImageUrl="~/facebook.jpg" Height="55px" /></a></td>
                    <td><a href="http://www.instagram.com/Thrifty.Books" target="_blank"><asp:Image ID="Instagram" runat="server" ImageURL="~/instagram.jpg" Height="55px" /></a></td>
                    <td><a href="http://www.twitter.com/ThriftyBooks" target="_blank"><asp:Image ID="Twitter" runat="server" ImageUrl="Twitter.png" Height="55px" /></a></td>
                </tr>
            </table>
        </td>
    </tr>
    </table>
</body>
</html>
<script type="text/javascript">
    function bookTableClick()
    {
        __doPostBack('bookResult', "click");
    }

    function selectBookClick(i)
    {
        if(i == 1)
        {
            __doPostBack('selectResult1', "click");
        }
        if (i == 2)
        {
            __doPostBack('selectResult2', "click");
        }
        if (i == 3)
        {
            __doPostBack('selectResult3', "click");
        }
        if (i == 4)
        {
            __doPostBack('selectResult4', "click");
        }
        if (i == 5)
        {
            __doPostBack('selectResult5', "click");
        }
    }
</script>
