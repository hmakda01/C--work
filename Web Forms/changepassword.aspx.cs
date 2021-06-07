using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class changepassword : System.Web.UI.Page
{
    //create an instance of the site info class to store important data about the site
    clsSiteInfo ThisSite = new clsSiteInfo();
    //used to store the details of the current user
    clsUser TheCurrentUser = new clsUser();

    protected void Page_Load(object sender, EventArgs e)
    {
        //this event runs when the page is loaded
        //check that the current user has logged in
        CheckLogin();
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

    void ChangePassword(string EMail, string NewPW)
    {
        //this sub changes the password for the user specified by their email address
        //find that user in the database
        clsDataConnection AUser = new clsDataConnection("select * from Users where EMail='" + EMail + "'");
        //if the user has been found
        if (AUser.Count == 1)
        {
            //change the password
            AUser.DataTable.Rows[0]["UserPassword"] = NewPW;
            //save the changes
            AUser.SaveChanges();
        }
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        //var to store any error messages
        string ErrorMsg;
        //var to store new password 1 entered by the user
        string NewPW1;
        //var to store the email address of current user
        string EMail;
        //clear the error message label
        lblError.Text = "";
        //get the email address of the current user
        EMail = TheCurrentUser.EMail;
        //validate the form
        ErrorMsg = FormValid();
        //if there are no errors
        if (ErrorMsg == "")
        {
            //read in the new password 1
            NewPW1 = txtNewPW1.Text;
            //change the password
            ChangePassword(EMail, NewPW1);
            //tell the user this has been done
            lblError.Text = "Your password has been changed";
        }
        else
        {
            //display any errors
            lblError.Text = ErrorMsg;
        }
    }

    private string FormValid()
    {
        //this function validates the contents of all of the fields
        //var to store any error messages
        string ErrMsg="";
        //used to validate the password entered by the user
        clsUser TempUser;
        //if the two new passwords do not match
        if (txtNewPW1.Text != txtNewPW2.Text)
        {
            ErrMsg = ErrMsg + " the new passwords do not match ";
        }
        //try to log in using the old password entered
        TempUser = new clsUser(TheCurrentUser.EMail, txtOldPW.Text);
        //if it cant authenticate
        if (TempUser.Authenticated == false)
        {
            ErrMsg = ErrMsg + " the old password is not correct ";
        }
        //return any error messages 
        return ErrMsg;
    }

    protected void btnDone_Click(object sender, EventArgs e)
    {
        //go back to the main page
        Response.Redirect("default.aspx");
    }
}