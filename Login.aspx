<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="javascript/modernizr.custom.63321.js" type = "text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>מאסטרו אלומיניום</title>
</head>
<body>
    <div class="container">
        <div class = "cntr">
            <img src="images/MaestroLogo.png" />
        </div>
        <section class="main">
            <form class="form-1">
            <p class="field">
                <input type="text" name="login" placeholder="Username">
                <i class="icon-user icon-large"></i>
            </p>
            <p class="field">
                <input type="password" name="password" placeholder="Password">
                <i class="icon-lock icon-large"></i>
            </p>
            <p class="submit">
                <button type="submit" name="submit">
                    <i class="icon-arrow-right icon-large"></i>
                </button>
            </p>
            </form>
        </section>
    </div>
</body>
</html>
