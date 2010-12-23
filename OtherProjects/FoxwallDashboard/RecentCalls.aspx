<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecentCalls.aspx.cs" Inherits="FoxwallDashboard.RecentCalls" %>

<%@ Register TagPrefix="foxwall" Src="~/CallMenu.ascx" TagName="CallMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
<foxwall:CallMenu runat="server" ID="CallMenu" /> 
    
<h1>Recent Calls</h1>

<asp:ListBox ID="CallList" runat="server"></asp:ListBox>

</asp:Content>
