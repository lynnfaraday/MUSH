<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PhotoList.aspx.cs" Inherits="PhotoList" %>

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
        <asp:Label ID="FilesMessage" runat="server" Text=""></asp:Label>
       <br />
        <asp:ListBox ID="FilesList" runat="server" SelectionMode="Single"></asp:ListBox>
        <br />
        <asp:Button ID="DownloadButton" Text="Download" BackColor="Green" runat="server" OnClick="DownloadButtonClick"/>
        <asp:Button ID="DeleteButton" Text="Delete" BackColor="Red" runat="server" OnClick="DeleteButtonClick"/>
        <br />
        <br />
        <asp:LinkButton ID="UplaodButton" runat="server" Text="Upload a new file." PostBackUrl="Upload.aspx" />
        </asp:Panel>
        </div>
    </form>
</body>
</html>
