<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThriftyBooks.aspx.cs" Inherits="ThriftyBooks.ThriftyBooks" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" aria-checked="false">
    <title></title>
    </head>
<body style="height: 264px">
    <center>
    <form id="form1" runat="server">
    <div style="height: 241px; width: 253px">
        <asp:TextBox ID="txtbxSearchTerm" runat="server" style="margin-bottom: 20px"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="SearchEvent" style="margin-bottom: 20px" />
        <br />
        <asp:Table ID="tblRent" runat="server" BorderStyle="Solid" BorderWidth="3px" CellPadding="3" CellSpacing="0" Height="16px" Width="181px" style="top: -39px; left: 0px; margin-top: 0px; margin-bottom: 15px;">
            <asp:TableRow runat="server" BorderStyle="Solid" BorderWidth="3px">
            </asp:TableRow>
        </asp:Table>
        <asp:Table ID="tblUsed" runat="server" BorderStyle="Solid" BorderWidth="3px" CellPadding="3" CellSpacing="0" style="top: 44px; left: 0px; margin-bottom: 15px" Width="181px">
        </asp:Table>
        <asp:Table ID="tblNew" runat="server" BorderStyle="Solid" BorderWidth="3px" CellPadding="3" CellSpacing="0" style="top: 106px; left: 0px" Width="181px">
        </asp:Table>
    </div>
    </form>
    </center>
</body>
</html>
