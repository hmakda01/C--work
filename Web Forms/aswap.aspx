<%@ Page Language="C#" AutoEventWireup="true" CodeFile="aswap.aspx.cs" Inherits="aswap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Swap Editor</title>
</head>
<body>
    <form id="form1" runat="server">
    
        <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid" 
            
            
        style="top: 26px; left: 28px; position: absolute; height: 544px; width: 954px">
            <asp:Label ID="lblPictureName" runat="server" 
                style="z-index: 1; left: 250px; top: 41px; position: absolute; width: 279px" 
                Text="Picture File"></asp:Label>
            <asp:TextBox ID="txtPictureFile" runat="server" Enabled="False" 
                style="z-index: 1; left: 251px; top: 63px; position: absolute; width: 396px"></asp:TextBox>
            <asp:FileUpload ID="fupPicture" runat="server" 
                
                style="z-index: 1; left: 250px; top: 14px; position: absolute; width: 568px" />
            <asp:Button ID="btnSet" runat="server" 
                style="z-index: 1; left: 834px; top: 12px; position: absolute; width: 79px" 
                Text="Set" OnClick="btnSet_Click" />
            <asp:Label ID="lblTitle" runat="server" 
                style="z-index: 1; left: 250px; top: 93px; position: absolute; width: 279px" 
                Text="DVD Title"></asp:Label>
            <asp:TextBox ID="txtTitle" runat="server" 
                style="z-index: 1; left: 250px; top: 120px; position: absolute; width: 660px"></asp:TextBox>
            <asp:Label ID="lblDescription" runat="server" 
                style="z-index: 1; left: 250px; top: 151px; position: absolute; width: 279px" 
                Text="DVD Description"></asp:Label>
            <asp:TextBox ID="txtDescription" runat="server" 
                style="z-index: 1; left: 250px; top: 177px; position: absolute; width: 660px; height: 248px" 
                TextMode="MultiLine"></asp:TextBox>
            <asp:Button ID="btnSave" runat="server" 
                style="z-index: 1; left: 831px; top: 435px; position: absolute; width: 68px; height: 26px;" 
                Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnDone" runat="server" 
                style="z-index: 1; left: 10px; top: 511px; position: absolute; width: 78px" 
                Text="Done" OnClick="btnDone_Click" />
            <asp:Image ID="imgSwap" runat="server" ImageUrl="~/nopicture.jpg" 
                
                style="z-index: 1; left: 21px; top: 22px; position: absolute; height: 252px" 
                Height="133px" Width="200px" />        
        
        
            <asp:Label ID="lblError" runat="server" ForeColor="Red" 
                
                
                style="z-index: 1; left: 94px; top: 513px; position: absolute; width: 842px"></asp:Label>
            
        </asp:Panel>
        </form>
</body>
</html>
