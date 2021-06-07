<%@ Page Language="C#" AutoEventWireup="true" CodeFile="signup.aspx.cs" Inherits="signup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign up for a new Account</title>
</head>
<body>
    <form id="form1" runat="server">
    
        <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid" 
        style="top: 26px; left: 28px; position: absolute; height: 544px; width: 954px">
            <h1>
                New Account Sign up
            </h1>
            <asp:Label ID="lblFirstName" runat="server" 
                style="z-index: 1; left: 134px; top: 100px; position: absolute; width: 98px" 
                Text="First Name"></asp:Label>
            <asp:TextBox ID="txtFirstName" runat="server" 
                
                style="z-index: 1; left: 257px; top: 99px; position: absolute; width: 220px"></asp:TextBox>
            <asp:Label ID="lblLastName" runat="server" 
                style="z-index: 1; left: 134px; top: 141px; position: absolute; width: 102px" 
                Text="Last Name"></asp:Label>
            <asp:TextBox ID="txtLastName" runat="server" 
                
                style="z-index: 1; left: 257px; top: 138px; position: absolute; width: 220px"></asp:TextBox>
            <asp:Label ID="lblEMail" runat="server" 
                style="z-index: 1; left: 134px; top: 185px; position: absolute; width: 109px" 
                Text="E Mail Address"></asp:Label>
            <asp:TextBox ID="txtEMail" runat="server" 
                
                style="z-index: 1; left: 257px; top: 181px; position: absolute; width: 220px"></asp:TextBox>
            <asp:Button ID="btnPassword" runat="server" 
                style="z-index: 1; left: 485px; top: 180px; position: absolute; width: 214px" 
                Text="Send me my password" OnClick="btnPassword_Click" />
            <asp:Label ID="lblPassword" runat="server" 
                style="z-index: 1; left: 134px; top: 227px; position: absolute; width: 104px" 
                Text="Password" Visible="False"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" 
                style="z-index: 1; left: 257px; top: 224px; position: absolute; width: 220px" 
                Visible="False" TextMode="Password"></asp:TextBox>
            <asp:Button ID="btnCreate" runat="server" 
                style="z-index: 1; left: 260px; top: 270px; position: absolute; width: 126px" 
                Text="Create Account" Visible="False" OnClick="btnCreate_Click" />
                        <asp:Button ID="btnDone" runat="server" 
                style="z-index: 1; left: 10px; top: 511px; position: absolute; width: 78px" 
                Text="Done" OnClick="btnDone_Click" />
            <asp:Label ID="lblError" runat="server" ForeColor="Red" 
                
                
                style="z-index: 1; left: 94px; top: 503px; position: absolute; width: 844px"></asp:Label>
        </asp:Panel>
        </form>
</body>
</html>
