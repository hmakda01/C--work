using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class resend : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //create an instance of the site infor class to store important data about the site
    clsSiteInfo ThisSite = new clsSiteInfo();
    //used to store the details of the current user
    clsUser TheCurrentUser = new clsUser();

    protected void btnPassword_Click(object sender, EventArgs e)
    {
        //this event is triggered when the user presses the send password button
        //var to store the email address
        string EMail;
        //var to store the password
        string Password;
        //var to flag if email sent ok
        Boolean Success;
        //clear the error message label
        lblError.Text = "";
        //get the email entered by the user
        EMail = txtEMail.Text;
        //get the password for this address
        Password = GetPassword(EMail);
        //if the password is not blank i.e. is found
        if (Password != "")
        {
            //try to send the password to the email address
            Success = SendPassword(EMail, Password);
            //if the email was sent
            if (Success == true)
            {
                //inform the user
                lblError.Text = "Your password has been sent to your email address.";
            }
            else
            {
                //otherwise display an error
                lblError.Text = "There was a problem sending your password.";
            }
        }
        else
        {
            //the email address was not found on the system
            lblError.Text = "Your email address could not be found on the system.";
        }
    }

    private bool SendPassword(string EMail, string Password)
    {
        //this function sends the password to the specified email address
        Boolean Success;
        //create an instance of my email object
        clsEMail AnEMail = new clsEMail();
        //send the email - success will contain true or fals depending on if it works or not
        Success = AnEMail.SendEMail("noreply@dmu.ac.uk", EMail, "Your swap shop password", "Your password is " + Password);
        //return success
        return Success;
    }

    private string GetPassword(string EMail)
    {
        //this function looks up the password for the specified email address
        //create a connection to the database table selecting records with this email address only
        clsDataConnection AUser = new clsDataConnection("select * from users where email='" + EMail + "'");
        //var to store the password
        string Password;
        //if one user is found i.e. only one record
        if (AUser.Count == 1)
        {
            //get the password from the database
            Password = (string)AUser.DataTable.Rows[0]["UserPassword"];
            //return the password
            return Password;
        }
        else
        {
            //password not found so return a blank string
            return "";
        }
    }

    protected void btnDone_Click(object sender, EventArgs e)
    {
        //redirect back to the main page
        Response.Redirect("default.aspx");
    }
}