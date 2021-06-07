using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class swapmanager : System.Web.UI.Page
{
    //create an instance of the site info class to store important data about the site
    clsSiteInfo ThisSite = new clsSiteInfo();
    //used to store the details of the current user
    clsUser TheCurrentUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        //check that a user is logged in
        CheckLogin();
        //if this is the first time the page has been displayed
        if (IsPostBack == false)
        {
            //display the swaps
            DisplaySwaps();
            //set the list boxes to their old values if required
            SetLists();
        }
    }

    private void SetLists()
    {
        //this sub attempts to set the list indexes of the two list 
        //boxes back to the value they had when the page was last used
        //
        //var to store the swap index 
        string SwapIndex;
        //var to store the offer index
        string OfferIndex;
        //var to store the swap no
        Int32 SwapNo=0;
        //var to store the offer no
        Int32 OfferNo;
        //if the swap index is a number and not -1
        try
        {
            //get the index of the swap from the session object
            SwapIndex = (string)Session["SwapIndex"];
            if (SwapIndex !=Convert.ToString( -1))
            {
                //set the index of the list box
                lstSwaps.SelectedIndex =Convert.ToInt32( SwapIndex);
                //get the unique identifier of the selected swap
                SwapNo =Convert.ToInt32( lstSwaps.SelectedValue);
                //if the index of the list is set
                if (lstSwaps.SelectedIndex != -1)
                {
                    //display the edit button
                    btnEdit.Visible = true;
                    //display the delete button
                    btnDelete.Visible = true;
                }
            }
        }
        catch { }
        //if the offer index is a number and not = -1
        try
        {
            //get the index of the offer from the session object
            OfferIndex = (string)Session["OfferIndex"];
            if (OfferIndex != Convert.ToString(-1)) ;
            {
                //display the offers for the selected swap
                DisplayOffers(SwapNo);
                //set the index of the offer list box
                lstOffers.SelectedIndex =Convert.ToInt32( OfferIndex);
                try
                {
                    //try to set the index
                    OfferNo = Convert.ToInt32(lstOffers.SelectedValue);
                    //get the details of the selected offer
                    lblDescription.Text = GetFullDetails(OfferNo);
                }
                catch { }
            }
        }
        catch { }
    }

    private void DisplaySwaps()
    {
        //this sub displays the swaps in the swaps list box
        //
        //open a connection to the database
        clsDataConnection MySwaps = new clsDataConnection("select * from swap order by title");
        //var to store the count of swaps
        Int32 SwapCount;
        //var for the loop
        Int32 Index=0;
        //var for the swap title
        string Title;
        //var for the unique identifier of the swap
        Int32 SwapNo;
        //var to store the number of offers on a swap
        Int32 OfferCount;
        //get the number of swaps
        SwapCount = MySwaps.Count;
        //loop through each swap
        while (Index < SwapCount)
        {
            //get the swap number for this swap
            SwapNo = (Int32)MySwaps.DataTable.Rows[Index]["SwapNo"];
            //get the title for this swap
            Title = (string)MySwaps.DataTable.Rows[Index]["Title"];
            //get the number of offers on this swap
            OfferCount = GetOfferCount(SwapNo);
            //if there are no offers
            if (OfferCount == 0)
            {
                //then just add the title
                lstSwaps.Items.Add(Title);
            }
            else
            {
                //else add the title with the number of offers
                lstSwaps.Items.Add(Title + " " + OfferCount + " offer(s)");
                //End If
            }
            //set the value property for this entry in the list box
            lstSwaps.Items[Index].Value = SwapNo.ToString();
            Index++;
        }
    }

    private void CheckLogin()
    {
        //this sub tests that the user has logged in correctly
        //if not then they are redirected to the main page
        //get the details of the current user from the session object
        TheCurrentUser = (clsUser)Session["TheCurrentUser"];
        //if the current user object is not set up correctly
        if (TheCurrentUser == null)
        {
            //go back to the main page
            Response.Redirect("Default.aspx");
        }
        //if the current user is not authenticated
        if (TheCurrentUser.Authenticated == false)
        {
            //go back to the main page
            Response.Redirect("Default.aspx");
        }
    }
    protected void btnWishList_Click(object sender, EventArgs e)
    {
        //save the indexes of both lists
        SaveIndexes();
        //redirect to the wish list manager
        Response.Redirect("wishlistmanager.aspx");
    }

    private void SaveIndexes()
    {
        //this sub saves the indexes for both lists into the session object
        Session["SwapIndex"] = lstSwaps.SelectedIndex;
        Session["OfferIndex"] = lstOffers.SelectedIndex;
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        //clear the current user
        Session["TheCurrentUser"] = null;
        //redirect to the main page
        Response.Redirect("default.aspx");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //var for offer count
        Int32 OfferCount;
        //var to store the unique identifier of the swap
        Int32 SwapNo;
        //get the uinque identifier of the swap selected in the list
        SwapNo = Convert.ToInt32(lstSwaps.SelectedValue);
        //get the number of offers on the swap
        OfferCount = GetOfferCount(SwapNo);
        //if there are no offers against this swap
        if (OfferCount == 0)
        {
            //store the swap number in the session variable
            Session["SwapNo"] = SwapNo;
            //save the indexes of both lists
            SaveIndexes();
            //redirect to the delete swap page
            Response.Redirect("deleteswap.aspx");
        }
        else
        {
            lblError.Text = "You must clear all offers against this swap before you may delete it.";
        }
    }

    private int GetOfferCount(int SwapNo)
    {
        //this function looks up records with this swap number
        //and returns the number of offers made on it
        //
        //open a connection to the database
        clsDataConnection Offers = new clsDataConnection("select * from offer where SwapNo=" + SwapNo + " order by title");
        //var to store the count of offers
        Int32 OfferCount;
        //get the count of offers for this swap
        OfferCount = Offers.Count;
        //return the offer count
        return OfferCount;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //set the session variable to -1
        Session["SwapNo"] = -1;
        //redirect to the add swap page
        Response.Redirect("aswap.aspx");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        //var for offer count
        Int32 OfferCount;
        //var to store the unique identifier of the swap
        Int32 SwapNo;
        //get the uinque identifier of the swap selected in the list
        SwapNo =Convert.ToInt32( lstSwaps.SelectedValue);
        //get the number of offers on the swap
        OfferCount = GetOfferCount(SwapNo);
        //if there are no offers against this swap
        if (OfferCount == 0)
        {
            //store the swap number in the session variable
            Session["SwapNo"] = SwapNo;
            //save the indexes of both lists
            SaveIndexes();
            //redirect to the edit swap page
            Response.Redirect("aswap.aspx");
        }
        else
        {
            lblError.Text = "You cannot edit a swap while there are offers against it.";
        }
    }

    protected void btnDone_Click(object sender, EventArgs e)
    {
        //clear the indexes of the list boxes
        ClearIndexes();
        //go back to the main page
        Response.Redirect("default.aspx");
    }

    private void ClearIndexes()
    {
        //this sub clears the indexes stored in the session object
        Session["SwapIndex"] = -1;
        Session["OfferIndex"] = -1;
    }

    void DisplayOffers(Int32 SwapNo)
    {
        //this sub displays the offers for a swap specified by SwapNo
        //
        //open a connection to the database
        clsDataConnection Offers = new clsDataConnection("select * from offer where SwapNo=" + SwapNo + " order by title");
        //var to stor the count of offers
        Int32 OfferCount;
        //var for the loop
        Int32 Index=0;
        //var for the unique id of the offer
        Int32 OfferNo;
        //var for the title of the offer
        string Title;
        //var for the acceptance date of the offer
        string AcceptanceDate;
        //get the number of offers
        OfferCount = Offers.Count;
        //clear the offers list box
        lstOffers.Items.Clear();
        //hide the accept offer button
        btnAccept.Visible = false;
        //hide the reject offer button
        btnReject.Visible = false;
        //display the edit swap button
        btnEdit.Visible = true;
        //display the delete swap button
        btnDelete.Visible = true;
        //clear the offer description label
        lblDescription.Text = "";
        //loop through each offer
        while (Index < OfferCount)
        {
            //get the title of the offer
            Title = (string)Offers.DataTable.Rows[Index]["Title"];
            //get the acceptance date of the offer
            try
            {
                //assume the field doesn't contain a null value
                AcceptanceDate = (string)Offers.DataTable.Rows[Index]["AcceptanceDate"];
            }
            catch
            {
                //if it is null then set AcceptanceDate to a blank string
                AcceptanceDate = "";
            }
            //get the unique identifier of the offer
            OfferNo = (Int32)Offers.DataTable.Rows[Index]["OfferNo"];
            //add the offer to the list
            //if the acceptance date is blank
            if (AcceptanceDate == "")
            {
                //just add the title
                lstOffers.Items.Add(Title);
            }
            else
            {
                //otherwise add the title and the acceptance date
                lstOffers.Items.Add(Title + " Offer accepted " + AcceptanceDate);
            }
            //set the value property for this list entry
            lstOffers.Items[Index].Value = OfferNo.ToString();
            Index++;
        }
    }

    protected void lstSwaps_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this event runs when a swap is clicked on the list box
        //var to store the swap number
        Int32 SwapNo;
        //get the swap number from th list
        SwapNo =Convert.ToInt32( lstSwaps.SelectedValue);
        //display the offers on this swap
        DisplayOffers(SwapNo);
    }

    protected void lstOffers_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this event runs when the user clicks on the offers list box
        //var to store the offer no
        Int32 OfferNo;
        //get the offer no
        OfferNo =Convert.ToInt32( lstOffers.SelectedValue);
        //get the full details of the offer and display in lblDescription
        lblDescription.Text = GetFullDetails(OfferNo);
    }

    private string GetFullDetails(int OfferNo)
    {
        //this function returns the full details as an html formatted string of the offer and the person making the offer
        //it accepts one parameter which is the offer number
        //
        //open a connection to the database
        clsDataConnection Offers = new clsDataConnection("select * from offer where OfferNo=" + OfferNo);
        //var to store the title of the offer
        string OfferTitle;
        //var to store the description of the offer
        string OfferDescription;
        //var to store the unique identifier of the person making the offer
        Int32 UserNo;
        //var to store the first name of the person making the offer
        string FirstName;
        //var to store the last name of the person making the offer
        string LastName;
        //var to store the email of the person making the offer
        string EMail;
        //var to store the full details of the offer and the person making it
        string FullDetails;
        //make the accept offer button visible
        btnAccept.Visible = true;
        //make the reject offer button visible
        btnReject.Visible = true;
        //if the offer is found
        if (Offers.Count == 1)
        {
            //get the user no for the person making the offer
            UserNo = (Int32)Offers.DataTable.Rows[0]["UserNo"];
            //get the title of the offer
            OfferTitle = (string)Offers.DataTable.Rows[0]["Title"];
            //get the description of the offer
            OfferDescription = (string)Offers.DataTable.Rows[0]["Description"];
            //open a connection to the database to find the details of the person making the offer
            clsDataConnection Users = new clsDataConnection("select * from Users where UserNo=" + UserNo);
            //if the person is found
            if (Users.Count == 1)
            {
                //get their first name
                FirstName = (string)Users.DataTable.Rows[0]["FirstName"];
                //get their last name
                LastName = (string)Users.DataTable.Rows[0]["LastName"];
                //get their email
                EMail = (string)Users.DataTable.Rows[0]["EMail"];
                //concatenate the values to produce the formatted string
                FullDetails = "<b>Offer Details</b><br>" + FirstName + " " + LastName + "<br>" + EMail + "<br>" + OfferTitle + "<br>" + OfferDescription;
                //return the formatted string
                return FullDetails;
                //Else
            }
            else
            {
                //return a blank string
                return "";
            }
        }
        else
        {
            //return a blank string
            return "";
        }
    }

    protected void btnAccept_Click(object sender, EventArgs e)
    {
        //this event runs when a swap is accepted by pressing the accept swap 
        //
        //var to store the unique identifier of the selected swap
        Int32 SwapNo;
        //ver to store the uniqui identifier of the selected offer
        Int32 OfferNo;
        //get the swap number
        SwapNo =Convert.ToInt32( lstSwaps.SelectedValue);
        //get the offer number
        OfferNo = Convert.ToInt32(lstOffers.SelectedValue);
        //accept the offer for this swap
        AcceptOffer(SwapNo, OfferNo);
        //update the list of offers
        DisplayOffers(SwapNo);
    }

    private void AcceptOffer(int SwapNo, int OfferNo)
    {
        //this sub accepst an offer made on a swap
        //it accepts two parameters the swap number and the offer number
        //it sends an email to the person making the offer
        //and another email to the site owner
        //the offer is date stamped so that the site owner knows when the offer was accepted
        //
        //my email object used to send emails
        clsEMail AnEmail = new clsEMail();
        //var to store the email address of the person making the offer
        string OfferEMail;
        //var to store the title of the offered item
        string OfferTitle;
        //var to store the title of the swap
        string SwapTitle;
        //var to store the name of the person making the offer
        string OfferUserName;
        //get the email address of the person making the offer
        OfferEMail = GetOfferEMail(OfferNo);
        //get the title of the item being swapped
        SwapTitle = GetSwapTitle(SwapNo);
        //get the title of the item being offered
        OfferTitle = GetOfferTitle(OfferNo);
        //get the name of the person making the offer
        OfferUserName = GetOfferUserName(OfferNo);
        //construct and send an acceptance email to the person making the offer
        AnEmail.SendEMail(ThisSite.OwnerEMail, OfferEMail, "Your offer has been accepted", ThisSite.SiteOwner + " has accepted your offer of " + OfferTitle + " for " + SwapTitle + " please reply to this email to arrange the exchange.");
        //construct and send an email to the owner of the site
        AnEmail.SendEMail(OfferEMail, ThisSite.OwnerEMail, "You have accepted my offer", OfferUserName + " is going to swap " + OfferTitle + " for " + SwapTitle + " reply to this message to arrange an exchange.");
        //date stamp the swap
        //open a connection to the database finding the record for this offer
        clsDataConnection AnOffer = new clsDataConnection("select * from Offer where OfferNo = " + OfferNo);
        //date stamp the offer
        AnOffer.DataTable.Rows[0]["AcceptanceDate"] = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
        //save the changes
        AnOffer.SaveChanges();
    }

    private string GetOfferUserName(int OfferNo)
    {
        //this function returns the full name of the person making an offer
        //it acceptes one parameter which is the offer number
        //it first looks up the offer in the offer table to find the user no of the person making the offer
        //it next looks in the users table to find the name of the person
        //
        //var to store the user number of the person making the offer
        Int32 UserNo;
        //var to store the full name of the person making the offer
        string FullName;
        //open the database locating the record for this offer
        clsDataConnection AnOffer = new clsDataConnection("select * from offer where offerno=" + OfferNo);
        //if the offer is found
        if (AnOffer.Count == 1)
        {
            //get the id of the user making the offer
            UserNo = (Int32)AnOffer.DataTable.Rows[0]["UserNo"];
            //open the database locating the user record for this person
            clsDataConnection AUser = new clsDataConnection("select * from users where userno=" + UserNo);
            //if the record is found
            if (AUser.Count == 1)
            {
                //get the full name of the person making the offer
                FullName = AUser.DataTable.Rows[0]["FirstName"].ToString() + " " + AUser.DataTable.Rows[0]["LastName"].ToString();
                //retuurn the full name
                return FullName;
            }
            else
            {
                //return a blank string
                return "";
            }
        }
        else
        {
            //return a blank string
            return "";
        }
    }

    private string GetOfferTitle(int OfferNo)
    {
        //this function looks up the title for an offer based on the offer no
        //
        //var to store the title of the offer
        string Title;
        //look up the offer in the database
        clsDataConnection AnOffer = new clsDataConnection("select * from offer where offerno=" + OfferNo);
        //if the record is found
        if (AnOffer.Count == 1)
        {
            //get the title
            Title = (string)AnOffer.DataTable.Rows[0]["Title"];
            //return the title
            return Title;
        }
        else
        {
            //return the title
            return "";
        }
    }

    private string GetSwapTitle(int SwapNo)
    {
        //this function looks up and returns the title of a swap based on the swap number
        //
        //var to store the title of the swap
        string Title;
        //look up the swap in the database
        clsDataConnection AnSwap = new clsDataConnection("select * from swap where swapno=" + SwapNo);
        //if the record is found
        if (AnSwap.Count == 1)
        {
            //get the title of the swap
            Title = (string)AnSwap.DataTable.Rows[0]["Title"];
            //return the title
            return Title;
        }
        else
        {
            //return a blank string
            return "";
        }
    }

    private string GetOfferEMail(object OfferNo)
    {
        //this function looks up the offer no in the offer table to find the user who made the offer
        //it then looks up that person's user no in the users table to find their email address
        //it returns their email address
        //
        //var to store the user no of the person who made the offer
        Int32 UserNo;
        //var to store the email address of the person who made the offer
        string EMail;
        //open the database to find the record for this offer
        clsDataConnection AnOffer = new clsDataConnection("select * from offer where offerno=" + OfferNo);
        //if the record is found
        if (AnOffer.Count == 1)
        {
            //get the user no of the person making the offer
            UserNo = (Int32)AnOffer.DataTable.Rows[0]["UserNo"];
            //get the record for this user
            clsDataConnection AUser = new clsDataConnection("select * from users where userno=" + UserNo);
            //if the user is found
            if (AUser.Count == 1)
            {
                //get their email address
                EMail = (string)AUser.DataTable.Rows[0]["EMail"];
                //return the email address
                return EMail;
            }
            else
            {
                //return a blank string
                return "";
            }
        }
        else
        {
            //return a blank string
            return "";
        }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        //Protected Sub btnReject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReject.Click
        //this event is triggered when the user presses the reject offer button
        Int32 OfferNo;
        //get the unique identifier of the offer to be rejected
        OfferNo =Convert.ToInt32( lstOffers.SelectedValue);
        //store the offer no to be removed in the session object
        Session["OfferNo"] = OfferNo;
        //save the indexes of both lists
        SaveIndexes();
        //display the reject offer page
        Response.Redirect("rejectoffer.aspx");
    }
}