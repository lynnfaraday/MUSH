<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="FoxwallDashboard.DefaultPage" %>
<%@ Register TagPrefix="foxwall" Src="~/EditPersonControl.ascx" TagName="EditPersonControl" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
<foxwall:EditPersonControl runat="server" ID="PersonControl" /> 

    
</asp:Content>
