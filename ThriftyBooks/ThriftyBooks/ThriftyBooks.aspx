<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ThriftyBooks.aspx.cs" Inherits="Thrifty.ThriftyBooks" MaintainScrollPositionOnPostback ="true" EnableEventValidation ="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <link href="ThriftyBooksStyleSheet.css" rel="stylesheet" type="text/css" />
<head runat="server" aria-checked="false">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="icon" type="image/png" href="book.png" />
    <title>Thrifty Books</title>
    <style type="text/css">
        .auto-style2 {
            width: 137px;
            height: 285px;
        }
        .auto-style3 {
            width: 387px;
            height: 285px;
        }
        .auto-style4 {
            background-color: red;
            height: 150px;
            margin-top: -15px;
            position: relative;
            left: 0px;
            top: 0px;
        }
        .auto-style5 {
            height: 241px;
            width: 500px;
        }
        .auto-style7 {
            height: 285px;
        }
        .auto-style8 {
            height: 305px;
            margin-left: 20px;
        }
        .auto-style10 {
            margin-top: 20px;
        }
        .auto-style11 {
            margin-left: 0px;
        }
        .auto-style12 {
            height: 285px;
            width: 226px;
        }
        </style>
    </head>
<header class="auto-style4">
    <p id="title"><a href="http://www.google.com" > THRIFTYBOOKS.com</a><p>
    <p id="subtitle"><i>Right place to find books</i></p>
    
</header>
<body class="body">
    <center>
        <table class="auto-style8">
        <tr>
            <td class="auto-style12">
                <table>
                    <tr>
                        <td colspan="3"><asp:Image ID="Image1" runat="server" ImageUrl="laptopbook.png" Width="205px"  style="margin-top: 70px;" /></td>
                    </tr>
                    
                </table>             
            </td>
            <td class="auto-style2"></td>
            <td width="260" class="auto-style7">
            <form id="form1" runat="server">
            <div style="margin-top: 0px;" id="searchDiv" class="auto-style5">
                <asp:TextBox ID="txtbxSearchTerm" runat="server" Text="" style="margin-bottom: 5px" CssClass="auto-style11" Width="402px"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="SearchEvent" style="margin-bottom: 5px"/>
                <br />
                <asp:RadioButton ID="rbISBN" runat="server" GroupName="searchType" Text="ISBN"  Checked ="true"/>&nbsp
                <asp:RadioButton ID="rbTitle" runat="server" GroupName="searchType" Text="Title" />
                <asp:RadioButton ID="rbAuthor" runat="server" GroupName="searchType" Text="Author" />
                
                <asp:table ID="bookResult" runat="server" Visible="false" Width="478px" GridLines="Horizontal" BackColor="#FFFFFF" CellPadding="5" class="auto-style9" BorderWidth="1px" Height="101px" CssClass="auto-style10" style="cursor:pointer">
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
                <asp:Button ID="btnExpandRent2" runat="server" OnClick="btnExpandRent" Text="See more" Visible ="false" style="margin-bottom: 20px"/>
                <br />
                <asp:Button ID="btnExpandUsed2" runat="server" OnClick="btnExpandUsed" Text="See more" Visible ="false" style="margin-bottom: 20px"/>
                <br />
                <asp:Button ID="btnExpandNew2" runat="server" OnClick="btnExpandNew" Text="See more" Visible ="false" style="margin-bottom: 20px"/>
            </div>
            <div id="bookResults" runat="server" Visible="false">
            </div>
            </form>
            </td>
            <td class="auto-style3">
                
                        <a href="http://www.facebook.com" target="_blank"><asp:Image ID="Facebook" runat="server" ImageUrl="~/facebook.jpg" Width="55px" /></a>
                        <br/>
                        <a href="http://www.instagram.com" target="_blank"><asp:Image ID="Instagram" runat="server" ImageURL="~/instagram.jpg" Width="55px" Height="54px" /></a>
                        <br />
                        <a href="http://www.twitter.com" target="_blank"><asp:Image ID="Twitter" runat="server" ImageUrl="Twitter.png" Width="55px" /></a>
                    

            </td>
        </tr>
        </table>
    </center>
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
