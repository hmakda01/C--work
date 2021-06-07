<%@ Page Language="C#" AutoEventWireup="true" CodeFile="resend.aspx.cs" Inherits="resend" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resend Password</title>
</head>
<body>
    <form id="form1" runat="server">
    
        <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid" 
        style="top: 26px; left: 28px; position: absolute; height: 544px; width: 954px">
                    <h1>
                        Resend Password
                    </h1>
            <asp:Label ID="lblEMail" runat="server" 
                style="z-index: 1; left: 134px; top: 145px; position: absolute; width: 109px" 
                Text="E Mail Address"></asp:Label>
            <asp:TextBox ID="txtEMail" runat="server" 
                style="z-index: 1; left: 257px; top: 141px; position: absolute; width: 220px"></asp:TextBox>
            <asp:Button ID="btnPassword" runat="server" 
                style="z-index: 1; left: 258px; top: 170px; position: absolute; width: 159px" 
                Text="Send me my password" OnClick="btnPassword_Click" />
        
                        <asp:Button ID="btnDone" runat="server" 
                style="z-index: 1; left: 10px; top: 511px; position: absolute; width: 78px" 
                Text="Done" OnClick="btnDone_Click" />
            <asp:Label ID="lblError" runat="server" ForeColor="Red" 
                style="z-index: 1; left: 94px; top: 513px; position: absolute; width: 842px"></asp:Label>

        </asp:Panel>
        </form>
</body>
</html>
