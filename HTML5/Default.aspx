<%@ Page Language="C#" %>

<!DOCTYPE html>

<script runat="server">
</script> 

<html>
<head>
    <meta charset="utf-8" />
    <title> Game Swap Shop</title>
    <link href="GameSwap.css" rel="stylesheet" />
   
</head>
<body>
  
    <header>
        <h1>
        Husain's Game Swap
        </h1>
        <h2>
            Swapping Games since 2021
        </h2>
    </header>
    <article>
       
        Search
        <input type="search" name=" Search" />
        <input type="button" value="Search" />
        <input type="button" value=" Clear" />
        
        
        <div class="MainTable">
       <%
        //create an instance of the collection
        clsGameCollection MyGame = new clsGameCollection();
        //create a index inialised at 0
        Int32 Index = 0;
        //get the counts of record
        Int32 RecordCount = MyGame.Count;
        //loop thrugh each game
        %> <table border="1"><%
        %><tr><%
        %><th>GameNo</th><%
        %><th>GameTitle</th><%
        %></tr><%
      

        
        while (Index < RecordCount)
        {
            %><tr><%       
            %><td><a href="aGame.aspx?GameNo=<%        
            //write gameno to the browser
            Response.Write(MyGame.GameList[Index].GameNo);
            %>"><%
             //write the dvd number to the broswer
            Response.Write(MyGame.GameList[Index].GameNo);
            %></a></td><%
            %><td><%
            //write the game title to the browser
            Response.Write(MyGame.GameList[Index].Title);
            %></td><%
            %></tr><% 
           
            //increemnt the index
            Index++;
        }
        %></table><%

   %>
     </div>
        <div id ="EnterSwap">
            <form method="post" action="SwapProcessor.aspx">
                Title of the game you wish to offer me
                <br />
                <input type="text" name="txtTitle" size="80" />
                <br />
                Description of the Game
                <textarea name="txtDescription" cols="80" rows="8"></textarea>
                <br />
                <input type="submit" name="btnSubmit" value="Submit Offer" />
                </form>
        </div>
       
    </article>


    
    <form id="Form1" method="post" action="DefaultProcessor.aspx">
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
        <br />
        <a href="signup.aspx">Sign up</a>
        <br />

    </form>

   <a href="Default2.html">Second</a>
    
</body>
</html>