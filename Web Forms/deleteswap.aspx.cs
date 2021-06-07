using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class deleteswap : System.Web.UI.Page
{
    //create an instance of the site infor class to store important data about the site
    clsSiteInfo ThisSite = new clsSiteInfo();
    //used to store the details of the current user
    clsUser TheCurrentUser = new clsUser();
    //var to store the swap number
    Int32 SwapNo;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        //this event runs when the page loads
        //check that the user is logged in
        CheckLogin();
        //get the swap no for deletion
        SwapNo = (Int32)Session["SwapNo"];
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
        //delete the swap
        DeleteSwap();
        //clear the list box indexes
        ClearIndexes();
        //redirect back to the swap manager
        Response.Redirect("swapmanager.aspx");
    }

    private void ClearIndexes()
    {
        //this sub clears the indexes stored in the session object
        Session["SwapIndex"] = -1;
        Session["OfferIndex"] = -1;
    }

    private void DeleteSwap()
    {
        //this sub deletes the swap identified by swapno
        //
        //find the record in the database
        clsDataConnection ASwap = new clsDataConnection("select * from swap where swapno=" + SwapNo);
        //if the record is found
        if (ASwap.Count == 1)
        {
            //remove the record
            ASwap.RemoveRecord(0);
            //save the changes
            ASwap.SaveChanges();
        }
    }
}