<%@ Page Language="C#" %>

<!DOCTYPE html>
<script runat="server">
    protected void Page_Load(Object sender, EventArgs e)
    {

        string SomeText = Request.Form["txtDescription"];
        string Text = Request.Form["txtTitle"];
        //create an intsance for thesercuity class
        clsSecurity Sec = new clsSecurity();
        Boolean Error = Sec.ValidateInput(SomeText, Text);
        if (Sec.ValidateInput(SomeText, Text) ==true)
        {
            Response.Write("Your offer has been sent");
        }
        else
        {
            Response.Write("Your offer has not been sent");
        }

    }
 

</script>



<html xmlns="http://www.w3.org/1999/xhtml">
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
</html>
