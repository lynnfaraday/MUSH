<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="FoxwallDashboard.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h1>Change Password</h1>

<table>

        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" CSSClass="labelTitle" Text="Old Password:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="OldPasswordBox" runat="server" TextMode="Password">
                </asp:TextBox>
            </td>
            <td>            
                <asp:Label ID="OldPasswordError" runat="server" CSSClass="errorMessage" Visible="false"
                Text="Incorrect password."></asp:Label>                
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" CSSClass="labelTitle" Text="New Password:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="PasswordBox" runat="server" TextMode="Password">
                </asp:TextBox>
            </td>
            <td>            
                <asp:Label ID="PasswordError" runat="server" CSSClass="errorMessage" Visible="false"
                Text="Password must be at least 5 characters and contain at least 1 letter, 1 number and one special character (@#$%^&+=)."></asp:Label>                
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" CSSClass="labelTitle" Text="Confirm Password:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="ConfirmPasswordBox" runat="server" TextMode="Password">
                </asp:TextBox>
            </td>
            <td>            
                <asp:Label ID="PasswordMatchError" runat="server" CSSClass="errorMessage" Visible="false"
                Text="Passwords do not match."></asp:Label> 
            </td>
        </tr>
         <tr>
            <td>
                <asp:Button ID="ChangeButton" runat="server" OnClick="ChangeButtonClick" Text="Change Password" />
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
</table>
    
<asp:Label ID="NoticeLabel" runat="server" CSSClass="errorMessage"></asp:Label>

</asp:Content>
