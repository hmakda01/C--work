<%@ Page Language="C#" %>

<!DOCTYPE html>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        String GameNo;
        GameNo = Request.Form["txtGameNo"];
        Response.Write("Your Game code is " + GameNo);

        String Title;
        Title = Request.Form["txtTitle"];
        Response.Write("Your Game is Called " + Title);
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
