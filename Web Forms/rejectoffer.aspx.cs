using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class rejectoffer : System.Web.UI.Page
{
    //create an instance of the site info class to store important data about the site
    clsSiteInfo ThisSite = new clsSiteInfo();
    //used to store the details of the current user
    clsUser TheCurrentUser;
    //used to store the offer no of the offer to be rejected
    Int32 OfferNo;

    protected void Page_Load(object sender, EventArgs e)
    {
        //make sure that this page is being accessed by a logged in user
        CheckLogin();
        //get the offer number of the offer to be rejected
        OfferNo = (Int32)Session["OfferNo"];
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



    protected void btnNo_Click(object sender, EventArgs e)
    {
        //redirect back to the swap manager
        Response.Redirect("swapmanager.aspx");
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        //reject the offer
        RejectOffer();
        //redirect back to the swap manager
        Response.Redirect("swapmanager.aspx");
    }

    private void RejectOffer()
    {
        //sends an email to the person making the offer and delete the offer from the database
        //
        //var to store the email address of the person making the offer
        string OfferEMail;
        //var to store the user name of the person making the offer
        string OfferUserName;
        //var to store the title of the offer
        string OfferTitle;
        //declare an instance of my email object
        clsEMail AnEMail = new clsEMail();
        //var to store successful sending of the email
        Boolean Success;
        //get the email address of the person making the offer
        OfferEMail = GetOfferEMail(OfferNo);
        //get the user name of the person making the offer
        OfferUserName = GetOfferUserName(OfferNo);
        //get the title of the item on offer
        OfferTitle = GetOfferTitle(OfferNo);
        //send a rejection email to the person who made the offer
        Success = AnEMail.SendEMail("dvd.mdb", OfferEMail, "Your swap shop offer", ThisSite.SiteOwner + " has declined your offer of " + OfferTitle);
        //find the record for this offer in the database
        clsDataConnection AnOffer = new clsDataConnection("select * from offer where offerno=" + OfferNo);
        //if the record is found
        if (AnOffer.Count == 1)
        {
            //delete it
            AnOffer.RemoveRecord(0);
            //save the changes
            AnOffer.SaveChanges();
        }
    }

    private string GetOfferTitle(int offerNo)
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

    private string GetOfferUserName(int offerNo)
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
                FullName = AUser.DataTable.Rows[0]["FirstName"].ToString() + " " + AUser.DataTable.Rows[0]["LastName"];
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

    private string GetOfferEMail(Int32 OfferNo)
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
}