using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class wishlistmanager : System.Web.UI.Page
{

    //create an instance of the site info class to store important data about the site
    clsSiteInfo ThisSite = new clsSiteInfo();
    //used to store the details of the current user
    clsUser TheCurrentUser = new clsUser();

    protected void Page_Load(object sender, EventArgs e)
    {
        //check that the user is logged in
        CheckLogin();
        //get the site info
        ThisSite = (clsSiteInfo)Session["ThisSite"];
        //if this is the first time the page has loaded
        if (IsPostBack == false)
        {
            //display the wish list
            DisplayWishList();
        }
    }

    private void DisplayWishList()
    {
        //open a connection to the wish list table
        clsDataConnection WishList = new clsDataConnection("select * from wishlist order by description");
        //var to store a single wish list item
        string AnItem;
        //var to store the wish list number
        Int32 WishListNo;
        //var to store the number of items in the wish list
        Int32 ListCount;
        //Index for the loop
        Int32 Index=0;
        //clear the list box
        lstWishList.Items.Clear();
        //get the number of items in the wish list table
        ListCount = WishList.Count;
        //loop through each item
        while (Index < ListCount)
        {
            //get the description of the item
            AnItem = (string)WishList.DataTable.Rows[Index]["Description"];
            //get the primary key of the item
            WishListNo = (Int32)WishList.DataTable.Rows[Index]["WishListNo"];
            //add it to the list
            lstWishList.Items.Add(AnItem);
            //set the primary key on the list
            lstWishList.Items[Index].Value = WishListNo.ToString();
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

    protected void btnDone_Click(object sender, EventArgs e)
    {
        //redirect back to the swap manager
        Response.Redirect("swapmanager.aspx");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //var to store the wish list item to be added
        string WishListItem;
        //get the wish list item
        WishListItem = txtWishListItem.Text;
        //clear the error label
        lblError.Text = "";
        //if it isn't blank
        if (WishListItem != "")
        {
            //add the wish list item
            AddWishListItem(WishListItem);
            //update the wish list
            DisplayWishList();
            //clear the wish list text box
            txtWishListItem.Text = "";
        }
        else
        {
            //display an error
            lblError.Text = "You must type a description of the item.";
        }
    }

    private void AddWishListItem(string WishListItem)
    {
        //this sub adds a wish list item to the database
        //
        //open the wish list table
        clsDataConnection WishList = new clsDataConnection("select * from wishlist");
        //set the description
        WishList.NewRecord["Description"] = WishListItem;
        //add the record
        WishList.AddNewRecord();
        //save the changes
        WishList.SaveChanges();
    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        //var to store the wish list index
        Int32 WishListIndex;
        //var to store the wish list id
        Int32 WishListID;
        //get the index of the selected entry in the list
        WishListIndex = lstWishList.SelectedIndex;
        //clear the error label
        lblError.Text = "";
        //if an item has been selected 
        if (WishListIndex != -1)
        {
            //get the primary key of this item
            WishListID =Convert.ToInt32( lstWishList.SelectedValue);
            //remove the item
            RemoveWishListItem(WishListID);
            //update the wish list
            DisplayWishList();
        }
        else
        {
            //display an error
            lblError.Text = "You must select an item from the list first.";
        }
    }

    private void RemoveWishListItem(int WishListID)
    {
        //this sub removes a wish list item identified by wish list id (primary key)
        //
        //open a connection to the wish list table
        clsDataConnection WishList = new clsDataConnection("select * from wishlist where wishlistno=" + WishListID);
        //if the record is found
        if (WishList.Count == 1)
        {
            //remove the record
            WishList.RemoveRecord(0);
            //save the changes
            WishList.SaveChanges();
        }
    }
}