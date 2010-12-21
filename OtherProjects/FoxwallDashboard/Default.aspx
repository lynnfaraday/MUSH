<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="FoxwallDashboard.DefaultPage" %>
<%@ Register TagPrefix="foxwall" Src="~/EditCallControl.ascx" TagName="EditCallControl" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
<asp:Button ID="LoadButton" runat="server" Text="Load" OnClick="LoadButtonClick"  CausesValidation="False" />

<foxwall:EditCallControl runat="server" ID="CallControl" />   
    
</asp:Content>
