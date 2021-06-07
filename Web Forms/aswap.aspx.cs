using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class aswap : System.Web.UI.Page
{
    //create an instance of the site info class to store important data about the site
    clsSiteInfo ThisSite = new clsSiteInfo();
    //used to store the details of the current user
    clsUser TheCurrentUser = new clsUser();

    //var to store the swap number of the current swap
    //if this is set to -1 then it indicates a new swap
    //if it is set to >=0 then it indicates a swap for editing
    Int32 SwapNo;

    protected void Page_Load(object sender, EventArgs e)
    {
        //this event runs when the page is loaded
        //
        //check that the current user is logged in correctly
        CheckLogin();
        //get the swap number from the session object
        SwapNo = (Int32)Session["SwapNo"];
        //if this is the first time the page has loaded
        if (IsPostBack == false)
        {
            //if the swap number is not -1 we need to edit an existing swap
            if (SwapNo != -1)
            {
                //load the details for this swap
                LoadSwap();
            }
        }
    }

    private void LoadSwap()
    {
        //this sub loads an existing swap to the page
        //
        //open a connection to the table finding this swap record
        clsDataConnection ASwap = new clsDataConnection("select * from swap where SwapNo = " + SwapNo);
        //if the record is found
        if (ASwap.Count == 1)
        {
            //get the swap title
            txtTitle.Text = ASwap.DataTable.Rows[0]["Title"].ToString();
            //get the swap description
            txtDescription.Text = ASwap.DataTable.Rows[0]["Description"].ToString();
            //get the image file
            txtPictureFile.Text = ASwap.DataTable.Rows[0]["ImageFile"].ToString();
            //set the picture 
            imgSwap.ImageUrl = ASwap.DataTable.Rows[0]["ImageFile"].ToString();
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //this event runs when the save button is pressed
        //
        //var to store the name of the image file
        string ImageFile;
        //var to store the swap description
        string SwapDescription;
        //var to store the swap title
        string SwapTitle;
        //var to store the error message
        string ErrorMsg;
        //get the image file
        ImageFile = txtPictureFile.Text;
        //get the swap description
        SwapDescription = txtDescription.Text;
        //get the swap title
        SwapTitle = txtTitle.Text;
        //validate the form getting any error messages
        ErrorMsg = SwapValid();
        //if there are no errors
        if (ErrorMsg == "")
        {
            //if the current swap = -1 this is a new record
            if (SwapNo == -1)
            {
                //call the add swap sub procedure
                AddSwap(SwapTitle, SwapDescription, ImageFile);
            }
            else
            {
                //else we have been editing an existing record
                UpdateSwap(SwapTitle, SwapDescription, ImageFile);
            }
            //redirect to the swap manager
            Response.Redirect("swapmanager.aspx");
        }
        else
        {
            //display any error messages
            lblError.Text = ErrorMsg;
            //End If
        }
    }

    private void UpdateSwap(string SwapTitle, string SwapDescription, string ImageFile)
    {
        //this sub adds a new swap to the swap table
        //
        //open a connection to the swap table
        clsDataConnection Swaps = new clsDataConnection("select * from swap where swapno=" + SwapNo);
        if (Swaps.Count == 1)
        {
            //set the title
            Swaps.DataTable.Rows[0]["Title"] = SwapTitle;
            //set the description
            Swaps.DataTable.Rows[0]["Description"] = SwapDescription;
            //set the name of the image file
            Swaps.DataTable.Rows[0]["ImageFile"] = ImageFile;
            //save the changes
            Swaps.SaveChanges();
        }
    }

    private void AddSwap(string SwapTitle, string SwapDescription, string ImageFile)
    {
        //this sub adds a new swap to the swap table
        //
        //open a connection to the swap table
        clsDataConnection Swaps = new clsDataConnection("select * from swap");
        //set the title
        Swaps.NewRecord["Title"] = SwapTitle;
        //set the description
        Swaps.NewRecord["Description"] = SwapDescription;
        //set the name of the image file
        Swaps.NewRecord["ImageFile"] = ImageFile;
        //add the record
        Swaps.AddNewRecord();
        //save the changes
        Swaps.SaveChanges();
    }

    private string SwapValid()
    {
        //this function validates the swap form
        //it returns an error message if there are problems as a string
        //
        //var to store the error message
        string ErrMsg;
        //set the error message to blank
        ErrMsg = "";
        //if there is no title
        if (txtTitle.Text == "")
        {
            //set the error message
            ErrMsg = ErrMsg + " you must specify a title ";
        }
        //if there is no description
        if (txtDescription.Text == "")
        {
            //set the error message
            ErrMsg = ErrMsg + " you must write a description ";
        }
        //if there is no image
        if (txtPictureFile.Text == "")
        {
            //set the error message
            ErrMsg = ErrMsg + " you need to upload an image ";
        }
        return ErrMsg;
    }

    protected void btnDone_Click(object sender, EventArgs e)
    {
        //redirect to the swap manager page
        Response.Redirect("swapmanager.aspx");
    }

    protected void btnSet_Click(object sender, EventArgs e)
    {
        //this event runs when a picture file is uploaded
        //upload the file
        Upload();
    }

    private void Upload()
    {
        //this sub uploads an image file to the site 
        //it accepts one parameter (the pnumber) which is only used on the trading floor
        //
        //var to store the file name
        string FileName;
        //var to store the file path
        string FilePath;
        //var to store the path of the app on the server
        string BaseDir = System.AppDomain.CurrentDomain.BaseDirectory;
        //clear the error label
        lblError.Text = "";
        //if a file has been selected
        if (fupPicture.HasFile == true)
        {
            //get the file name
            FileName = fupPicture.FileName;
            //if the file is of the correct type
            if (CorrectType(FileName))
            {
                FilePath = BaseDir + "\\" + FileName;
                //save the file to the server
                fupPicture.SaveAs(FilePath);
                //set the file name of the record to this file
                txtPictureFile.Text = FileName;
                //display the image file
                imgSwap.ImageUrl = FileName;
            }
            else
            {
                //state that this is not ana allowed file type
                lblError.Text = "That is not an allowed file type.";
            }
        }
        else
        {
            //state that no file has been selected
            lblError.Text = "You must select a file to upload.";
        }
    }

    private bool CorrectType(string FileName)
    {
        //this function checks a file for upload to make sure it is of an allowed type
        Boolean OK = false;
        //var to store the extension of the file
        string Ext = System.IO.Path.GetExtension(FileName).ToLower();
        //check the file extension
        //Select Case Ext.ToLower
        switch (Ext)
        {
            //if a jpg
            case ".jpg":
                OK = true;
                break;
            //if a jpeg
            case ".jpeg":
                OK = true;
                break;
            //if a gif
            case ".gif":
                OK = true;
                break;
            //if a png
            case ".png":
                OK = true;
                break;
            default://otherwise return false
                OK = false;
                break;
        }
        return OK;
    }
}