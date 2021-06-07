<%@ Page Language="C#" %>

<!DOCTYPE html>

<script runat="server">

    protected void Page_Load(Object sender, EventArgs e)
    {
        //cretae an instance
        clsSecurity Sec = new clsSecurity();
        //declare a variable to store the password
        string Password;
        //declare a variable to store the email
        string Email;
       
        //request the contents from the text box to the form
        Password = Request.Form["txtPassword"];
        Email = Request.Form["txtEmail"];
        if (Sec.Login(Email, Password) ==true)
        {
            Response.Write("Login Successfull");
        }
        else
        {
            Response.Write("Login Failed");
        }

    }
</script>
<head>
    <meta charset="utf-8" />
    <title> Game Swap Shop</title>
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
 </body>





