<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wishlist.aspx.cs" Inherits="wishlist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fred&#39;s Wish List</title>
</head>
<body>
    <form id="form1" runat="server">
    
        <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid" 
        style="top: 26px; left: 28px; position: absolute; height: 544px; width: 954px; text-align: left;">
        
            <h1>
                Fred&#39;s Wish List<br />
            </h1>
            <br />
            <asp:Label ID="lblWishList" runat="server" 
                style="z-index: 1; left: 30px; top: 93px; position: absolute; width: 475px" 
                Text="Below is a list of films I am looking for"></asp:Label>
            <asp:ListBox ID="lstWishList" runat="server" 
                
                style="z-index: 1; left: 32px; top: 116px; position: absolute; height: 368px; width: 887px">
            </asp:ListBox>
            
            <asp:Label ID="lblError" runat="server" ForeColor="Red" 
                
                
                style="z-index: 1; left: 93px; top: 513px; position: absolute; width: 843px"></asp:Label>
                        <asp:Button ID="btnDone" runat="server" 
                style="z-index: 1; left: 10px; top: 511px; position: absolute; width: 78px; height: 26px;" 
                Text="Done" OnClick="btnDone_Click" />
        </asp:Panel>
        </form>
</body>
</html>
