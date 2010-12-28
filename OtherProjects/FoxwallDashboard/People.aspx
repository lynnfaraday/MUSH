<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="People.aspx.cs" Inherits="FoxwallDashboard.People" %>
<%@ Register TagPrefix="foxwall" Src="~/Controls/PersonListDisplayControl.ascx" TagName="PersonListDisplayControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h1>Manage People</h1>
<i>Note: Someday this will be locked to only administrators.</i>
<br /><br />
<foxwall:PersonListDisplayControl runat="server" ID="PersonListControl" /> 
<asp:Label ID="NoticeLabel" runat="server" CSSClass="errorMessage"></asp:Label>

</asp:Content>
