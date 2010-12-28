<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="FoxwallDashboard.DefaultPage" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
     

<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">

<asp:Label ID="Label1" runat="server" Text="Username" CSSClass="labelTitle"></asp:Label>
<br />
<asp:TextBox ID="UsernameBox" runat="server"></asp:TextBox>
<asp:RequiredFieldValidator ControlToValidate="UsernameBox" ErrorMessage="*" CSSClass="errorMessage" runat="server"></asp:RequiredFieldValidator>

<br /><br />
<asp:Label ID="Label2" runat="server" Text="Password"  CSSClass="labelTitle"></asp:Label>
<br />
<asp:TextBox ID="PasswordBox" runat="server" TextMode="Password"></asp:TextBox>
<asp:RequiredFieldValidator ControlToValidate="UsernameBox" ErrorMessage="*" CSSClass="errorMessage" runat="server"></asp:RequiredFieldValidator>
<br /><br />

<asp:Button ID="LoginButton" runat="server" Text="Login" OnClick="LoginButtonClick" />
<br />
<asp:Label ID="NoticeLabel" runat="server" CSSClass="errorMessage"></asp:Label>   

</asp:Panel>


</asp:Content>

