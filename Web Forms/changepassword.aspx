<%@ Page Language="C#" AutoEventWireup="true" CodeFile="changepassword.aspx.cs" Inherits="changepassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change your password</title>
</head>
<body>
    <form id="form1" runat="server">
    
        <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid" 
        style="top: 26px; left: 28px; position: absolute; height: 544px; width: 954px">
                    <h1>
                        Change your password
                    </h1>
            <asp:Label ID="lblOldPW" runat="server" 
                style="z-index: 1; left: 134px; top: 101px; position: absolute; width: 117px" 
                Text="Old Password"></asp:Label>
            <asp:TextBox ID="txtOldPW" runat="server" 
                style="z-index: 1; left: 257px; top: 98px; position: absolute; width: 220px" 
                        TextMode="Password"></asp:TextBox>
            <asp:Label ID="lblNewPW1" runat="server" 
                style="z-index: 1; left: 134px; top: 145px; position: absolute; width: 117px" 
                Text="New Password"></asp:Label>
            <asp:TextBox ID="txtNewPW1" runat="server" 
                style="z-index: 1; left: 257px; top: 141px; position: absolute; width: 220px" 
                        TextMode="Password"></asp:TextBox>
            <asp:Label ID="lblNewPW2" runat="server" 
                style="z-index: 1; left: 134px; top: 187px; position: absolute; width: 120px" 
                Text="New Password"></asp:Label>
            <asp:TextBox ID="txtNewPW2" runat="server" 
                style="z-index: 1; left: 257px; top: 184px; position: absolute; width: 220px" 
                        TextMode="Password"></asp:TextBox>
            <asp:Button ID="btnChange" runat="server" 
                style="z-index: 1; left: 260px; top: 230px; position: absolute; width: 126px" 
                Text="Change Password" OnClick="btnChange_Click" />
                        <asp:Button ID="btnDone" runat="server" 
                style="z-index: 1; left: 10px; top: 511px; position: absolute; width: 78px" 
                Text="Done" OnClick="btnDone_Click" />


            <asp:Label ID="lblError" runat="server" ForeColor="Red" 
                style="z-index: 1; left: 91px; top: 505px; position: absolute; width: 847px"></asp:Label>

        </asp:Panel>
        </form>
</body>
</html>
