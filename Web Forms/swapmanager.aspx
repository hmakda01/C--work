<%@ Page Language="C#" AutoEventWireup="true" CodeFile="swapmanager.aspx.cs" Inherits="swapmanager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Swap Manager</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid" 
            
            style="top: 26px; left: 28px; position: absolute; height: 544px; width: 724px">
            <h1>
                Swap Manager<br />
            </h1>
            <asp:Label ID="lblSwaps" runat="server" 
                style="z-index: 1; left: 33px; top: 90px; position: absolute; width: 227px" 
                Text="My Swaps"></asp:Label>
            <asp:ListBox ID="lstSwaps" runat="server" 
                
                
                style="z-index: 1; left: 31px; top: 115px; position: absolute; height: 331px; width: 332px" 
                AutoPostBack="True" OnSelectedIndexChanged="lstSwaps_SelectedIndexChanged">
            </asp:ListBox>
            <asp:Button ID="btnAdd" runat="server" 
                style="z-index: 1; left: 33px; top: 448px; position: absolute; width: 100px" 
                Text="Add Swap" OnClick="btnAdd_Click" />
            <asp:Button ID="btnEdit" runat="server" 
                style="z-index: 1; left: 149px; top: 448px; position: absolute; width: 100px;" 
                Text="Edit Swap" Visible="False" OnClick="btnEdit_Click" />
            <asp:Button ID="btnDelete" runat="server" 
                style="z-index: 1; left: 262px; top: 448px; position: absolute; width: 100px;" 
                Text="Delete Swap" Visible="False" OnClick="btnDelete_Click" />
            <asp:Label ID="lblOffers" runat="server" 
                style="z-index: 1; left: 379px; top: 90px; position: absolute; width: 227px" 
                Text="Offers on this item"></asp:Label>
            <asp:ListBox ID="lstOffers" runat="server" 
                
                
                style="z-index: 1; left: 374px; top: 115px; position: absolute; width: 332px; height: 163px" 
                AutoPostBack="True" OnSelectedIndexChanged="lstOffers_SelectedIndexChanged">
            </asp:ListBox>
            <asp:Label ID="lblDescription" runat="server" BorderWidth="0px" 
                style="top: 287px; left: 379px; position: absolute; height: 152px; width: 322px; border-style: Solid"></asp:Label>
            <asp:Button ID="btnAccept" runat="server" 
                style="z-index: 1; left: 378px; top: 448px; position: absolute; width: 107px" 
                Text="Accept Offer" Visible="False" OnClick="btnAccept_Click" />
                        <asp:Button ID="btnDone" runat="server" 
                style="z-index: 1; left: 10px; top: 511px; position: absolute; width: 78px" 
                Text="Done" OnClick="btnDone_Click" />

            
            
            

            <asp:Button ID="btnReject" runat="server" 
                style="z-index: 1; left: 580px; top: 448px; position: absolute; width: 107px" 
                Text="Reject Offer" Visible="False" OnClick="btnReject_Click" />

            
            
            

            <asp:Label ID="lblError" runat="server" ForeColor="Red" 
                
                style="z-index: 1; left: 95px; top: 513px; position: absolute; width: 618px"></asp:Label>
        </asp:Panel>
        
        <asp:Panel ID="Panel2" runat="server" BorderStyle="Solid" 
            
            style="z-index: 1; left: 771px; top: 26px; position: absolute; height: 544px; width: 212px">
                <asp:Button ID="btnWishList" runat="server" 
    style="z-index: 1; left: 18px; top: 14px; position: absolute; width: 173px;" 
                Text="Edit Wish List" OnClick="btnWishList_Click" />
            <asp:Button ID="btnLogout" runat="server" 
                style="z-index: 1; left: 18px; top: 46px; position: absolute; width: 173px;" 
                Text="Logout" OnClick="btnLogout_Click" />
        </asp:Panel>
    
    </div>
    </form>
</body>
</html>
