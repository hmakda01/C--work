using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class wishlist : System.Web.UI.Page
{
    //create an instance of the site infor class to store important data about the site
    clsSiteInfo ThisSite = new clsSiteInfo();
    //used to store the details of the current user
    clsUser TheCurrentUser = new clsUser();


    protected void Page_Load(object sender, EventArgs e)
    {
        //when the page loads display the wish list
        DisplayWishList();
        //get the site info
        ThisSite = (clsSiteInfo)Session["ThisSite"];
    }

    private void DisplayWishList()
    {
        //open a connection to the wish list table
        clsDataConnection WishList = new clsDataConnection("select * from wishlist order by description");
        //var to store a single wish list item
        string AnItem;
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
            //add it to the list
            lstWishList.Items.Add(AnItem);
            Index++;
        }
    }

    protected void btnDone_Click(object sender, EventArgs e)
    {
        //redirect back to the main page
        Response.Redirect("default.aspx");
    }
}