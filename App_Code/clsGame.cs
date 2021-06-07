using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsGame
/// </summary>
public class clsGame
{
    public clsGame()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private Int32 mGameNo;
    public Int32 GameNo
    {
        get
        {
            return mGameNo;
        }
        set
        {
            mGameNo = value;
        }
    }

    public string mTitle;
    public string Title
    {
        get
        {
            return mTitle;
        }
        set
        {
            mTitle = value;
        }
    }

    public string mImage;
    public string Image
    {
        get
        {
            return mImage;

        }
        set
        {
            mImage = value;
        }
    }

    public Boolean Find(Int32 GameNo)
    {
        List<clsGame> mGameList = new List<clsGame>();
        //crate a single game
        clsGame SomeGame = new clsGame();
        SomeGame.GameNo = 1;
        SomeGame.Title = "Fifa 19";
        SomeGame.Image = "Fifa19.jpg";
        //add it to the array list
        mGameList.Add(SomeGame);
        //CREATE ABNOTHER ONE
        SomeGame = new clsGame();
        SomeGame.GameNo = 2;
        SomeGame.Title = "Call Of Duty";
        SomeGame.Image = "CallOfDuty.jpg";
        //add it to the list
        mGameList.Add(SomeGame);
        //another game
        SomeGame = new clsGame();
        SomeGame.GameNo = 3;
        SomeGame.Title = "Fortnite";
        SomeGame.Image = "Fortnite.jpg";
        //add it to the list
        mGameList.Add(SomeGame);
        // is the value to find between 1 to 3
        if  (GameNo >= 1 & GameNo <= 3 )
        {
            //subtarct 1 off the primary key so that it maps to the index of the array
            GameNo--;
            //copy the data from the arry list to the private member variables
            mGameNo = mGameList[GameNo].GameNo;
            mTitle = mGameList[GameNo].Title;
            mImage = mGameList[GameNo].Image;
            //reurn true
            return true;
        }
        else
        {
            //retur false
            return false;
        }
    }


}

