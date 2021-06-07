using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    //used to store the details of the current user
    clsUser TheCurrentUser;
    //var to store the number of login attempts
    Int32 Attempts;
    //create an instance of the site info class to store important data about the site
    clsSiteInfo ThisSite = new clsSiteInfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        string Filter;
        //this event runs when the page is loaded at the server
        //if this is the first time the page has loaded
        if (IsPostBack == false)
        {
            //set the number of login attempts to zero
            Attempts = 0;
            //get the search filter
            Filter = txtSearch.Text;
            //display list of swaps
            lblResultsCount.Text = ListMySwaps(Filter);
        }
        else
        {
            //if it is a page reload then get the current value from the session object
            Attempts = Convert.ToInt32(Session["Attempts"]);
        }
        //get the current user from the session object
        TheCurrentUser = (clsUser)Session["TheCurrentUser"];
        if (TheCurrentUser == null)
        {
            //initialise the current user
            TheCurrentUser = new clsUser();
        }
        //if there have been too many failed login attempts
        if (Attempts >= 2)
        {
            //redirect to the badlogin page
            Response.Redirect("badlogin.aspx");
        }
        //manage the interface
        ManageInterface();
    }

    private string ListMySwaps(string Filter)
    {
        //this function lists swaps in the list box
        //it takes one parameter which is a filter
        //it returns a string indicating how many swaps were found
        //
        //var to store the SwapNo (unique identifier)
        Int32 SwapNo;
        //var to stor the swap title
        string Title;
        //create a connection to the database table applying the filter
        clsDataConnection MySwaps = new clsDataConnection("select * from swap where title like('" + Filter + "%') order by title");
        //var to store the number of swaps found
        Int32 SwapCount;
        //get the number of swaps found
        SwapCount = MySwaps.Count;
        //clear the list box
        lstSwaps.Items.Clear();
        Int32 Index = 0;
        //loop through each swap
        while (Index < SwapCount)
        {
            //get the SwapNo from the database
            SwapNo =Convert.ToInt32( MySwaps.DataTable.Rows[Index]["SwapNo"]);
            //get the Title of the swap from the database
            Title =Convert.ToString( MySwaps.DataTable.Rows[Index]["Title"]);
            //add the title to the list
            lstSwaps.Items.Add(Title);
            //set the value of the item in the list to the SwapNo 
            lstSwaps.Items[Index].Value = SwapNo.ToString();
            Index++;
        }
        //if records were found
        if (MySwaps.Count > 0)
        {
            //return how many records were found
            return MySwaps.Count + " record(s) found";
        }
        else
        {
            //return no records found
            return "No records found";
        }
    }


    protected void Page_UnLoad(object sender, EventArgs e)
    {
        //this code runs each time the page is unloaded at the server
        //save the current user details in the session object
        Session["TheCurrentUser"] = TheCurrentUser;
        //save the number of login attempts in the session object
        Session["Attempts"] = Attempts;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //var to store the error message
        string ErrMsg;
        //var to store the title of the swap
        string OfferTitle;
        //var to store the swap title
        string SwapTitle;
        //var to store the description of the swap
        string Description;
        //var to store the unique identifier of the swap
        Int32 SwapNo;
        //var to store the unique identifier of the current user
        Int32 Userno;
        //var to store the email address of the current user
        string EMail;
        //var to store the username of the current user
        string UserName;
        //var to flagg the successful send of the email
        Boolean Success;
        //validate the offer by calling the offer valid function
        ErrMsg = OfferValid();
        //clear the error message label
        lblError.Text = "";
        //if the error message is blank
        if (ErrMsg == "")
        {
            //get the title of the offer
            OfferTitle = txtOfferTitle.Text;
            //get the description of the offer
            Description = txtOfferDescription.Text;
            //get the full name of the current user
            UserName = TheCurrentUser.FirstName + " " + TheCurrentUser.LastName;
            //get the email address of the current user
            EMail = TheCurrentUser.EMail;
            //get the user number of the current user
            Userno = TheCurrentUser.UserNo;
            //get the unique identifier of the selected swap
            SwapNo = Convert.ToInt32(lstSwaps.SelectedValue);
            //save the offer
            SaveOffer(SwapNo, Userno, OfferTitle, Description);
            //get the swap title
            SwapTitle = lstSwaps.SelectedItem.Text;
            //send an email to the site owner
            Success = EMailOwner(UserName, EMail, SwapTitle);
            //if the email was sent successfully
            if (Success == true)
            {
                //display a message to the user
                lblError.Text = "An Email has been sent to the site owner notifying them of your offer.";
                //clear the offer title
                txtOfferTitle.Text = "";
                //clear the offer description
                txtOfferDescription.Text = "";
            }
            else
            {
                //show a message that the email could not be sent
                lblError.Text = "There was a problem sending the notification email to the site owner.";
            }
        }
        else
        {
            //display the error message
            lblError.Text = ErrMsg;
        }
    }

    private bool EMailOwner(string UserName, string EMail, string OfferTitle)
    {
        //this function sends an email to the site owner
        //it returns true or false if the email was sent or not
        //
        //var to store success
        Boolean Success;
        //instance of the email object
        clsEMail AnEmail = new clsEMail();
        //send the email
        Success = AnEmail.SendEMail(EMail, ThisSite.OwnerEMail, "An offer has been made on one of your swaps", UserName + " has made an offer on " + OfferTitle);
        //return the outcome
        return Success;
    }

    private string OfferValid()
    {
        //this function validates the offer made and returns an error message as a string
        //if there are problems
        //
        //var to store the error message
        string ErrMsg;
        //initialise the error message
        ErrMsg = "";
        //if no swap has been selected
        if (lstSwaps.SelectedIndex == -1)
        {
            //add error message
            ErrMsg = ErrMsg + " you must select an item that you want ";
        }
        //if the title is blank
        if (txtOfferTitle.Text == "")
        {
            //add an error message
            ErrMsg = ErrMsg + " you must state the title of your offer ";
            //End If
        }
        //if description is blank
        if (txtOfferDescription.Text == "")
        {
            //add an error message
            ErrMsg = ErrMsg + " you must state the description of your offer ";
        }
        //return the error message
        return ErrMsg;
    }

    private void SaveOffer(int SwapNo, int UserNo, string OfferTitle, string OfferDescription)
    {
        //this sub saves the offer to the database
        //
        //open a connection to the database table
        clsDataConnection Offers = new clsDataConnection("select * from offer");
        //save the swap no to the new record
        Offers.NewRecord["SwapNo"] = SwapNo;
        //save the user no to the new record
        Offers.NewRecord["UserNo"] = UserNo;
        //save the offer title to the new record
        Offers.NewRecord["Title"] = OfferTitle;
        //save the offer description to the new record
        Offers.NewRecord["Description"] = OfferDescription;
        //add the new record
        Offers.AddNewRecord();
        //save the new record
        Offers.SaveChanges();
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //this code checks the user name and password and logs them in if they are correct
        //var to store the users email address
        string EMail;
        //var to store the users password
        string Password;
        //clear the error message
        lblError.Text = "";
        //get the EMail of the user
        EMail = txtEMail.Text;
        //get the password
        Password = txtPassword.Text;
        //create a new current user object using this user name and password
        TheCurrentUser = new clsUser(EMail, Password);
        //if the email and password combination are correct then authenticated should be true
        if (TheCurrentUser.Authenticated == true)
        {
            //call the sub to manage the interface
            ManageInterface();
            //clear the email address
            txtEMail.Text = "";
            //clear the password
            txtPassword.Text = "";
        }
        else
        {
            //the email / password combination were not valid
            //increase the number of attempts by one
            Attempts++;
            //display an error message
            lblError.Text = "You must enter a valid user name and password.";
        }
    }

    private void ManageInterface()
    {
        //this sub makes controls visible/invisible depending on if the current user is logged in
        //and also if the current user is an administrator
        //var to store if the current user is logged in
        Boolean LoggedIn = TheCurrentUser.Authenticated;
        //var to store if the current user is an administrator
        Boolean IsAdmin = TheCurrentUser.Administrator;
        //if logged in then show the panel allowing people to make swap offers
        panSwap.Visible = LoggedIn;
        //if logged in then show the change password button
        btnChange.Visible = LoggedIn;
        //if logged in then show the logout button
        btnLogout.Visible = LoggedIn;
        //if logged in then hide the panel allowing users to login
        panLogin.Visible = !LoggedIn;
        //if the current user is an administrator then display the manage swaps button
        btnManageSwaps.Visible = IsAdmin;
        //if no swap is selected
        if (lstSwaps.SelectedIndex == -1)
        {
            //hide the swap description label
            lblSwapDescription.Visible = false;
            //hide the swap picture
            picImageFile.Visible = false;
        }
        else
        {
            //display the description
            lblSwapDescription.Visible = true;
            //display the picture
            picImageFile.Visible = true;
        }
        //if the user is not logged in
        if (LoggedIn == false)
        {
            //clear the offer title text box
            txtOfferTitle.Text = "";
            //clear the offer description text box
            txtOfferDescription.Text = "";
        }
    }

    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        //redirect to the sign up page
        Response.Redirect("signup.aspx");
    }

    protected void btnManageSwaps_Click(object sender, EventArgs e)
    {
        //redirect to the swap manager page
        Response.Redirect("swapmanager.aspx");
    }

    protected void btnWishList_Click(object sender, EventArgs e)
    {
        //redirect to the wishlist page
        Response.Redirect("wishlist.aspx");
    }

    protected void btnResend_Click(object sender, EventArgs e)
    {
        //redirect to the resend password page
        Response.Redirect("resend.aspx");
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        //redirect to the change password page
        Response.Redirect("changepassword.aspx");
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        //this logs the user out of the system
        //clear the current user details
        TheCurrentUser = new clsUser();
        //call the sub to manage the interface
        ManageInterface();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //apply the filter
        ApplyFilter(txtSearch.Text);
    }

    private void ApplyFilter(string Filter)
    {
        //this sub applies the filter 
        //var to store the SwapID
        Int32 SwapID=0;
        //if a swap is selected in the list
        if (lstSwaps.SelectedIndex != -1)
        {
            //get the SwapID
            SwapID =Convert.ToInt32( lstSwaps.SelectedValue);
        }
        //apply the filter
        lblResultsCount.Text = ListMySwaps(Filter);
        //if the SwapID is still in the list
        SwapListed(SwapID);
        //update the interface
        ManageInterface();
    }

    private void SwapListed(int SwapID)
    {
        //this sub looks up the SwapId in the swap list and makes the item selected if found
        //
        //var to flag found
        Boolean Found;
        //Index for the loop
        Int32 Index;
        //var to store the number of swaps in the list
        Int32 SwapCount;
        //used to store the SwapId from the list box
        Int32 ListedSwapID=0;
        //initialise found as false
        Found = false;
        //get the number of swaps in the list
        SwapCount = lstSwaps.Items.Count;
        //initialise the Index
        Index = 0;
        //loop through each record until all records are done or we find what we want
        while (Found == false & Index < SwapCount)
        {
            //get the id for this swap in the list
            ListedSwapID =Convert.ToInt32( lstSwaps.Items[Index].Value);
            //if this is the same ias the id passed as a parameter
            if (ListedSwapID == SwapID)
            {
                //make this entry in the list the selected one
                lstSwaps.SelectedIndex = Index;
                //flag found as true
                Found = true;
            }
            //Else
            else
            {
                //increment the Index
                Index = Index + 1;
            }
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        //clear the filter
        txtSearch.Text = "";
        //apply the filter
        ApplyFilter(txtSearch.Text);
    }

    protected void lstSwaps_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this code runs when the user clicks on a swap in the list
        //var to store the SwapNo
        Int32 SwapNo;
        //get the SwapNo of the item clicked on
        SwapNo =Convert.ToInt32( lstSwaps.SelectedValue);
        //display the image for that record
        picImageFile.ImageUrl = GetSwapImage(SwapNo);
        //display the description for that record
        lblSwapDescription.Text = GetSwapDescription(SwapNo);
        //update the interface
        ManageInterface();
    }

    private string GetSwapDescription(int SwapNo)
    {
        //this function looks up the description of the swap for SwapNo
        //it returns the description of the swap
        //
        //var to store the description
        string Description="";
        //find the record for this swap
        clsDataConnection MySwaps = new clsDataConnection("select * from swap where swapno=" + SwapNo);
        //if the record has been found
        if (MySwaps.Count == 1)
        {
            //get the description
            Description = MySwaps.DataTable.Rows[0]["Description"].ToString();
            //return the description
            return Description;
        }

        else
        {
            //otherwise return a blank string
            return "";
        }
    }

    private string GetSwapImage(int SwapNo)
    {
        //this function looks up the image name for the SwapNo
        //it returns the name of the image
        //
        //var to store the name of the image
        string ImageFile="";
        //find the record for this swap
        clsDataConnection MySwaps = new clsDataConnection("select * from swap where swapno=" + SwapNo);
        //if the record has been found
        if (MySwaps.Count == 1)
        {
            //get the name of the image
            ImageFile = "../Images/" + MySwaps.DataTable.Rows[0]["ImageFile"].ToString();
            //return the image name
            return ImageFile;
        }
        else
        {
            //otherwise return a blank string
            return "";
        }
    }

    protected void btnAbout_Click(object sender, EventArgs e)
    {
        //show the about page
        Response.Redirect("About.aspx");
    }
}