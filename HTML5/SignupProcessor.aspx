<%@ Page Language="C#" %>

<!DOCTYPE html>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        //var to stre the email address
        string Email = Request.Form["txtEmail"];
        //var to store the passowrd
        string Password = Request.Form["txtPassword"];
        //var to store the password confirmation
        string PasswordConfirm = Request.Form["txtConfirmPassword"];
        //crate an instance of the secruity class
        clsSecurity Sec = new clsSecurity();
        //execute the sign up method
        string Error = Sec.SignUp(Email, Password, PasswordConfirm);
        //if no errors
        if (Error == "")
        {
            Response.Redirect("SignUpSuccess.html");
        }
        else
        {
            Response.Write("Password does not match");
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

