<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wishlistmanager.aspx.cs" Inherits="wishlistmanager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Wish List Editor</title>    
</head>
<body>
    <form id="form1" runat="server">
    
        <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid" 
        style="top: 26px; left: 28px; position: absolute; height: 544px; width: 954px; text-align: left;">
        
            <h1>
                My Wish List<br />
            </h1>
            <asp:Label ID="lblWishList" runat="server" 
                style="z-index: 1; left: 37px; top: 83px; position: absolute; width: 878px" 
                Text="Wish List Items"></asp:Label>            
            <asp:ListBox ID="lstWishList" runat="server" 
                
                style="z-index: 1; left: 37px; top: 107px; position: absolute; height: 239px; width: 878px">
            </asp:ListBox>
            <asp:Label ID="lblWishListItem" runat="server" 
                style="z-index: 1; left: 37px; top: 350px; position: absolute; width: 878px" 
                Text="Description"></asp:Label>
            <asp:TextBox ID="txtWishListItem" runat="server" 
                
                style="z-index: 1; left: 37px; top: 372px; position: absolute; width: 878px"></asp:TextBox>
            <asp:Button ID="btnAdd" runat="server" 
                style="z-index: 1; left: 40px; top: 401px; position: absolute; width: 78px" 
                Text="Add" OnClick="btnAdd_Click" />
            <asp:Button ID="btnDone" runat="server" 
                style="z-index: 1; left: 10px; top: 511px; position: absolute; width: 78px" 
                Text="Done" OnClick="btnDone_Click" />
            <asp:Button ID="btnRemove" runat="server" 
                style="z-index: 1; left: 130px; top: 401px; position: absolute; width: 78px" 
                Text="Remove" OnClick="btnRemove_Click" />
            <asp:Label ID="lblError" runat="server" ForeColor="Red" 
                
                style="z-index: 1; left: 95px; top: 513px; position: absolute; width: 841px"></asp:Label>
        </asp:Panel>
        </form>
</body>
</html>
