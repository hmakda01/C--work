using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsGameCollection
/// </summary>
public class clsGameCollection
{
    public clsGameCollection()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public List<clsGame> GameList
    {
        get
        {
            //cretae an array list of games
            List<clsGame> mGameList = new List<clsGame>();
            //crate a single game
            clsGame SomeGame = new clsGame();
            SomeGame.GameNo = 1;
            SomeGame.Title = "Fifa 19";
            //add it to the array list
            mGameList.Add(SomeGame);
            //CREATE ANOTHER ONE
            SomeGame = new clsGame();
            SomeGame.GameNo = 2;
            SomeGame.Title = "Call Of Duty";
            //add it to the list
            mGameList.Add(SomeGame);
            //another game
            SomeGame = new clsGame();
            SomeGame.GameNo = 3;
            SomeGame.Title = "Fortnite";
            //add it to the list
            mGameList.Add(SomeGame);
            //return the populated list
            return mGameList;

        }
    }
    public Int32 Count
    {
        //hrd coded count propert
        get
        {
            return 3;
        }
    }
}