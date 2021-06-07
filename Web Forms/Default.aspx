<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fred&#39;s Swap Shop</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid" 
            
            style="top: 26px; left: 28px; position: absolute; height: 544px; width: 724px">
            
                        <h1>
                            Fred&#39;s DVD Swap Shop<br />
                        </h1>
            <br />
            <asp:Panel ID="Panel4" runat="server" 
                
                            style="z-index: 1; left: 42px; top: 79px; position: absolute; height: 189px; width: 632px">
                
                <asp:Label ID="lblSearch" runat="server" 
                    style="z-index: 1; left: 14px; top: 13px; position: absolute; width: 99px" 
                    Text="Search"></asp:Label>
                <asp:TextBox ID="txtSearch" runat="server" 
                    style="z-index: 1; left: 112px; top: 15px; position: absolute"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" 
                    style="z-index: 1; left: 269px; top: 13px; position: absolute; width: 65px" 
                    Text="Search" OnClick="btnSearch_Click" />
                <asp:Button ID="btnClear" runat="server" 
                    style="z-index: 1; left: 340px; top: 13px; position: absolute; width: 65px" 
                    Text="Clear" OnClick="btnClear_Click" />
                <asp:Label ID="lblResultsCount" runat="server" 
                    style="z-index: 1; left: 412px; top: 14px; position: absolute; width: 190px"></asp:Label>
                    <asp:ListBox ID="lstSwaps" runat="server" 
                            
                    
                    style="top: 57px; left: 11px; position: absolute; height: 161px; width: 249px; border-style: Solid" 
                    AutoPostBack="True" OnSelectedIndexChanged="lstSwaps_SelectedIndexChanged">
                        </asp:ListBox>
                <asp:Image ID="picImageFile" runat="server" ImageUrl="~/2.jpg" 
                    
                    
                    style="z-index: 1; left: 529px; top: 58px; position: absolute; height: 118px" 
                    Visible="False" />
                <asp:Label ID="lblSwapDescription" runat="server" 
                    style="z-index: 1; left: 270px; top: 61px; position: absolute; height: 154px; width: 248px" 
                    Visible="False"></asp:Label>
            </asp:Panel>
                        
        <asp:Panel ID="Panel2" runat="server" BorderStyle="Solid" 
                    
                            
                            style="z-index: 1; left: 744px; top: -3px; position: absolute; height: 544px; width: 212px">
            <asp:Panel ID="panLogin" runat="server" 
                style="top: 14px; left: 4px; position: absolute; height: 275px; width: 197px">
                <asp:Button ID="btnSignUp" runat="server" 
                    style="z-index: 1; left: 16px; top: 13px; position: absolute; width: 173px" 
                    Text="Sign up" OnClick="btnSignUp_Click" />
                <asp:Label ID="lblUserName" runat="server" 
                    style="z-index: 1; left: 14px; top: 50px; position: absolute; width: 173px" 
                    Text="EMail Address"></asp:Label>
                <asp:TextBox ID="txtEMail" runat="server" 
                    
                    style="z-index: 1; left: 14px; top: 75px; position: absolute; width: 173px" 
                    ></asp:TextBox>
                <asp:Label ID="lblPassword" runat="server" 
                    style="z-index: 1; left: 14px; top: 107px; position: absolute; width: 173px" 
                    >Password</asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" 
                    
                    
                    style="z-index: 1; left: 14px; top: 134px; position: absolute; width: 173px" TextMode="Password" 
                    ></asp:TextBox>
                <asp:Button ID="btnResend" runat="server" 
                    style="z-index: 1; left: 15px; top: 198px; position: absolute; width: 173px;" 
                    Text="Resend Password" OnClick="btnResend_Click" />
                <asp:Button ID="btnLogin" runat="server" 
                    style="z-index: 1; left: 14px; top: 165px; position: absolute; width: 173px;" 
                    Text="Login" OnClick="btnLogin_Click" />
            </asp:Panel>
                <asp:Button ID="btnChange" runat="server" 
                style="z-index: 1; left: 18px; top: 293px; position: absolute; width: 173px;" 
                Text="Change Password" OnClick="btnChange_Click" />
                <asp:Button ID="btnWishList" runat="server" 
    style="z-index: 1; left: 18px; top: 325px; position: absolute; width: 173px;" 
                Text="Fred's Wish List" OnClick="btnWishList_Click" />
            <asp:Button ID="btnManageSwaps" runat="server" 
                style="z-index: 1; left: 18px; top: 387px; position: absolute; width: 173px;" 
                Text="Manage Swaps" Visible="False" OnClick="btnManageSwaps_Click" />

            
            <asp:Button ID="btnLogout" runat="server" 
                style="z-index: 1; left: 18px; top: 419px; position: absolute; width: 173px;" 
                Text="Logout" Visible="False" OnClick="btnLogout_Click" />
        </asp:Panel>

            
            <asp:Panel ID="panSwap" runat="server" 
                
                            
                            style="z-index: 1; left: 29px; top: 304px; position: absolute; height: 199px; width: 642px; bottom: 41px;">
                <asp:Label ID="lblTitle" runat="server" 
                    style="z-index: 1; left: 23px; top: 8px; position: absolute; width: 419px;" 
                    Text="Title of the DVD you wish to offer me"></asp:Label>
                <asp:TextBox ID="txtOfferTitle" runat="server" 
                    
                    
                    style="z-index: 1; left: 23px; top: 32px; position: absolute; width: 483px"></asp:TextBox>
                <asp:Label ID="lblDescription" runat="server" 
                    style="z-index: 1; left: 23px; top: 61px; position: absolute; width: 271px" 
                    Text="Description of your offer"></asp:Label>
                <asp:TextBox ID="txtOfferDescription" runat="server" 
                    style="z-index: 1; left: 23px; top: 87px; position: absolute; width: 478px; height: 69px" 
                    TextMode="MultiLine"></asp:TextBox>
                <asp:Button ID="btnSubmit" runat="server" 
                    style="z-index: 1; left: 23px; top: 166px; position: absolute; width: 110px" 
                    Text="Submit Offer" OnClick="btnSubmit_Click" />
            </asp:Panel>
                        <asp:Label ID="lblError" runat="server" ForeColor="Red" 
                            
                            
                            style="z-index: 1; left: 16px; top: 506px; position: absolute; width: 694px"></asp:Label>
        </asp:Panel>
        
    
                <asp:Button ID="btnAbout" runat="server" 
    style="z-index: 1; left: 798px; top: 388px; position: absolute; width: 173px;" 
                Text="About" OnClick="btnAbout_Click" />
        
    
    </div>
    </form>
</body>
</html>
