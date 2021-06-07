<%@ Page Language="C#" %>

<!DOCTYPE html>

<script runat="server">
    //declare the variable o store the gme no
    Int32 GameNo;
    //declare AN INSTABC EOF CLSGAME
    clsGame AGame = new clsGame();



    protected void Page_load(object sender, EventArgs e)
    {
        //try to read in the string
        try
        {
            //use the request object to get the data
            GameNo = Convert.ToInt32(Request.QueryString["GameNo"]);
            AGame.Find(GameNo);
        }
        catch
        {
            //incase of an error do nothing
        }
    }

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Game Swap Shop</title>
    <meta charset="utf-8" />
    <link href="GameSwap.css" rel="stylesheet" />
    
</head>
<body>
    <header>
       
        <h1>
        <a href="Default.aspx">Husain's Game Swap</a>
        </h1>
        <h2>
            Swapping Games since 2019
        </h2>
    </header>
    <article>
        <form method="post" action="aGameProcessor.aspx">

        <table border ="1">
            <tr>
                <td>GameNo</td><td><input type="text" name="txtGameNo" value="<% Response.Write(AGame.GameNo); %>"/> </td>
            </tr>
             <tr>
                <td>Title</td><td><input type="text" name="txtTitle" value="<% Response.Write(AGame.Title); %>" /></td>
            </tr>
             <tr>
                <td>Image</td><td><img src="../Images/<% Response.Write(AGame.Image); %>"/></td>
            </tr>
             <tr>
                <td>View more details</td><td><input type="submit" value="view" /></td>
            </tr>
             <tr>
                <td></td><td></td>
            </tr>
            
        </table>
            
            </form>

    </article>
    <form id="Form1" method="post" action="DefaultProcessor.aspx">
        <input type="button" value="sign up"  />
        <br />
          Email
        <br />
          <input type="text" name="txtEmail" />
        <br /> 
        Password
        <br />
        <input type="password" name="txtPassword" />
        <br />
        <input type="submit" value="Login"  />
        <br />
        <a href="signup.aspx">Sign Up</a>

    </form>
   
</body>
</html>
