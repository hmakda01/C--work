<%@ Page Language="C#" AutoEventWireup="true" CodeFile="deleteswap.aspx.cs" Inherits="deleteswap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delete Swap</title>
</head>
<body>
    <form id="form1" runat="server">
    
        <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid" 
            
            
        
        style="top: 26px; left: 28px; position: absolute; height: 544px; width: 954px; text-align: center;">
            <asp:Label ID="lblError" runat="server" ForeColor="Red" 
                style="z-index: 1; left: 21px; top: 513px; position: absolute; width: 915px"></asp:Label>
            <asp:Button ID="btnYes" runat="server" 
                style="z-index: 1; left: 291px; top: 200px; position: absolute; width: 68px" 
                Text="Yes" OnClick="btnYes_Click" />                
            <asp:Button ID="btnNo" runat="server" 
                style="z-index: 1; left: 564px; top: 200px; position: absolute; width: 68px" 
                Text="No" OnClick="btnNo_Click" />
            
            <br />
            <br />
            <h1>
                Are you sure you want to delete this swap?</h1>
        </asp:Panel>
        </form>
</body>
</html>
