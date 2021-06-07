<%@ Page Language="C#" %>

<!DOCTYPE html>

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link href="GameSwap3.css" rel="stylesheet" />
    <title>Sign up</title>
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
    <h1>Sign up</h1>
    <form method="post" action="SignupProcessor.aspx">
        Email 
        <br />
        <input type="text" name="txtEmail" />
        <br />
        Password
        <br />
        <input type="password" name="txtPassword" />
        <br />
        Confirm Password
        <br />
        <input type="password" name="txtConfirmPassword" />
        <input type="submit" value="Submit" /> 
    </form>
        
</body>
</html>
