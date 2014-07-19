<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>מאסטרו אלומיניום</title>
    <link rel="shortcut icon" type="image/x-icon" href="~/images/favicon.ico" />
    <script src="javascript/modernizr.custom.63321.js" type="text/javascript"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/style.css" type="text/css" />
    <link href="css/font.css" rel="stylesheet" type="text/css" />
    <script src="javascript/Maestro.js" type="text/javascript"></script>
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
</head>
<body>
    <div class="container">
        <div class="cntr">
            <img src="images/MaestroLogo.png" />
        </div>
        <section class="main">
            <form class="form-1" runat="server">
            <asp:Button ID="LoginBTN" runat="server" Style="display: none;" OnClick="LoginBTN_Click" />
            <p class="field">
                <input id="UserNameTB" type="text" name="login" placeholder="Username" runat="server">
                <i class="icon-user icon-large"></i>
            </p>
            <p class="field">
                <input id="PasswordTB" type="password" name="password" placeholder="Password" runat="server">
                <i class="icon-lock icon-large"></i>
            </p>
            <p class="submit">
                <button type="button" onclick="ClickLoginBTN()">
                    <i class="icon-arrow-right icon-large"></i>
                </button>
            </p>
            <span id="ErrorLBL" runat="server"></span>
            </form>
        </section>
    </div>
</body>
</html>
