using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class signup : System.Web.UI.Page
{ 
    //create an instance of the site infor class to store important data about the site
    clsSiteInfo ThisSite = new clsSiteInfo();
    //used to store the details of the current user
    clsUser TheCurrentUser = new clsUser();
    //this variable is used to store the password for a new account
    string NewPassword;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if this is the first time the page has loaded
        if (IsPostBack == false)
        {
            //generate a password for the new account
            NewPassword = GeneratePassword();
            //get the site info
            ThisSite = (clsSiteInfo)Session["ThisSite"];
        }
        else
        {
            //if it is a page reload then get the already existing password from the session object
            NewPassword = (string)Session["NewPassword"];
        }
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        //save the password on page unload in the session object
        Session["NewPassword"] = NewPassword;
    }

    private string GeneratePassword()
    {
        //this function generates a new password
        //the password is returned as a string
        //
        //var to store the password
        string APassword;
        //generate the password by calling GetWord twice concatenating the results
        APassword = GetWord() + GetWord();
        //return the password
        return APassword;
    }

    private string GetWord()
    {
        //this function returns a single word from the file passwords.txt
        //
        //store the path of the application in DBPath
        string DbPath = System.AppDomain.CurrentDomain.BaseDirectory;
        //create a connection to passwords.txt
        System.IO.StreamReader MyFile = new System.IO.StreamReader(DbPath + "\\passwords.txt");
        //declare a new arraylist for the words in the file
        List<string> Passwords = new List<string>();
        //var to store the one word
        string AWord;
        //var to store the random index of the selected word
        Int32 PWNo;
        //var to store the count of words in the list
        Int32 WordCount;
        //initialise the random number seed
        Random rnd = new Random();        
        //initialise the word var
        AWord = "";
        //while there are words to read in the text file
        while (MyFile.Peek() >= 0)
        {
            //read in a single line (word)
            AWord = MyFile.ReadLine();
            //add the word to the array list
            Passwords.Add(AWord);
            //End While
        }
        //close the text file
        MyFile.Close();
        //get the number of words in the list
        WordCount = Passwords.Count - 1;
        //generate a random number between 0 and no of words less one
        //PWNo = Rnd() * WordCount
        PWNo = rnd.Next(0, WordCount);
        //get the word at that index
        AWord = Passwords[PWNo];
        //return the word
        return AWord;
        //End Function
    }


    protected void btnPassword_Click(object sender, EventArgs e)
    {
        //this event sends a password for the new account once all of the details are validated for the 
        //new user
        //
        //var to store the first name
        string FirstName;
        //var to store the last name
        string LastName;
        //var to store the email address
        string EMail;
        //var to store any error messages
        string ErrorMessage;
        //var to flag successful send of password
        Boolean PasswordSent;
        //clear the error message label
        lblError.Text = "";
        //check to make sure the data is valid
        ErrorMessage = DetailsValid();
        //if no error message created by the above line
        if (ErrorMessage == "")
        {
            //get the first name
            FirstName = txtFirstName.Text;
            //get the last name
            LastName = txtLastName.Text;
            //get the email address
            EMail = txtEMail.Text;
            //try to send the password to the new user
            PasswordSent = SendPassword(FirstName, LastName, EMail, NewPassword);
            //if the password was sent ok
            if (PasswordSent == true)
            {
                //stop the user changing their name
                txtFirstName.Enabled = false;
                txtLastName.Enabled = false;
                //stop the user changing their email address
                txtEMail.Enabled = false;
                //disable the send password button
                btnPassword.Enabled = false;
                //display the password text box
                txtPassword.Visible = true;
                //display the create account button
                btnCreate.Visible = true;
                //show further instructions
                lblError.Text = "DO NOT CLOSE THIS PAGE! An email has been sent to your account with your password.  To create your account type it into the box above.";
            }
            else
            {
                //show an error
                lblError.Text = "There was a problem sending your password.  Please check your email address and try again.";
            }
        }
        else
        {
            //display the error message
            lblError.Text = ErrorMessage;
        }
    }

    private Boolean SendPassword(string FirstName, string LastName, string EMail, string Password)
    {
        //this function sends the password for the new account to the user
        //if the email is sent ok then it returns true, else false
        //it accepts four parameters FirstName, LastName, EMail and Password
        //
        //create a new instance of the MyEmail object
        clsEMail AnEmail = new clsEMail();
        //var to record success of email send
        Boolean AllOk;
        //send the email to the user containing the password
        AllOk = AnEmail.SendEMail("noreply@dmu.ac.uk", EMail, "Your DVD swap shop password", FirstName + " Your password is " + Password);
        //return the status of the action
        return AllOk;
    }

    private string DetailsValid()
    {
        //this function checks the details entered by the new user
        //and checks if they are valid
        //it returns any related error messages as a string
        //
        //var to store main error message
        string ErrorMessage;
        //clear the error message
        ErrorMessage = "";
        //if the first name is blank then 
        if (txtFirstName.Text == "")
        {
            //store the error
            ErrorMessage = ErrorMessage + "You must enter your first name. ";
        }
        //if the last name is blank then 
        if (txtLastName.Text == "")
            {
            //store the error
            ErrorMessage = ErrorMessage + "You must enter your last name. ";
        }
        //return the error message
        return ErrorMessage;
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        //this event creates a new account so long as the user
        //enters the correct password to validate their email address
        //
        //var to store first name
        string FirstName;
        //var to store last name
        string LastName;
        //var to store email address
        string EMail;
        //var to store password
        string Password;
        //clear the error message label control
        lblError.Text = "";
        //get the first name
        FirstName = txtFirstName.Text;
        //get the last name
        LastName = txtLastName.Text;
        //get the email address
        EMail = txtEMail.Text;
        //get the password
        Password = txtPassword.Text;
        //if the internal password is the same as the one on the form
        if (NewPassword == txtPassword.Text)
        {
            //create the account
            CreateAccount(FirstName, LastName, EMail, Password);
            //tell the user the account has been created
            lblError.Text = "Your account has been created.";
            //disable the password text box
            txtPassword.Enabled = false;
            //hide the create account button so they cannot press it again
            btnCreate.Visible = false;
        }
        else
        {
            //display an error message
            lblError.Text = "There was a problem sending your password.  Please check your email address and try again.";
        }
    }

    private void CreateAccount(string FirstName, string LastName, string EMail, string Password)
    {
        //this sub creates a new account for the user
        //accepts four parameters FirstName LastName EMail and Password
        //
        //create a connection to the database table users
        clsDataConnection Users = new clsDataConnection("select * from users");
        //store the first name in the new record object
        Users.NewRecord["FirstName"] = FirstName;
        //store the last name in the new record object
        Users.NewRecord["LastName"] = LastName;
        //stor the email in the new record object
        Users.NewRecord["EMail"] = EMail;
        //store the password
        Users.NewRecord["UserPassword"] = Password;
        //add the new record
        Users.AddNewRecord();
        //save the changes (the new user is added to the database)
        Users.SaveChanges();
    }

    protected void btnDone_Click(object sender, EventArgs e)
    {
        //redirect back to the main page
        Response.Redirect("Default.aspx");
    }
}