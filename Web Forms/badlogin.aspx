<%@ Page Language="C#" AutoEventWireup="true" CodeFile="badlogin.aspx.cs" Inherits="badlogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fred&#39;s Swap Shop</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lblError" runat="server" 
            style="z-index: 1; left: 126px; top: 117px; position: absolute; width: 674px" 
            Text="You have failed to login too many times. Please close your browser and try again."></asp:Label>
        
    
    </div>
    </form>
</body>
</html>
