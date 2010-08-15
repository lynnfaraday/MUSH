<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="Upload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" onload="PageLoad">
    <div>
        <asp:Label ID="ErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
        <asp:LinkButton ID="LoginErrorButton" runat="server" Text="<br>Return to Login Page." PostBackUrl="Default.aspx" />

        <asp:Panel ID="RealContent" runat="server">
        
        <asp:Label ID="WelcomeMessage" runat="server" Text="" ForeColor="Blue"></asp:Label>
        <br />
        <asp:Label ID="FileMessage" runat="server" Text=""></asp:Label>
        <br />
        <asp:FileUpload ID="FileUploader" runat="server" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Select the people you want to share it with."></asp:Label>
        <br />
        <asp:ListBox ID="UsersList" runat="server" SelectionMode="Multiple"></asp:ListBox>
        <br />
        <br />
        <asp:Button ID="UploadButton" runat="server" Text="Share" OnClick="UploadButtonClick" />

        </asp:Panel>
    </div>
    </form>
</body>
</html>
