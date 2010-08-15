<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="Label3" runat="server" Text="Please log in..." Font-Bold="true" />
    <br />
        <asp:Label ID="Label1" runat="server" Text="Username:"></asp:Label>
        <asp:TextBox ID="Username" Width=200 runat="server"></asp:TextBox>
        <br>
        <asp:Label ID="Label2" runat="server" Text="Password:"></asp:Label>
        <asp:TextBox ID="PasswordEntry" Width=200 runat="server" TextMode="Password" />
        <br />
        <asp:Label ID="ErrorMessage" runat="server" ForeColor="Red"></asp:Label>
        <br />        
        <asp:Button ID="LoginButton" runat="server" Text="Login" 
            onclick="LoginButtonClick" />
    </div>
    </form>
</body>
</html>
