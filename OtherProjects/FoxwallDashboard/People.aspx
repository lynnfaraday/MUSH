<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="People.aspx.cs" Inherits="FoxwallDashboard.People" %>
<%@ Register TagPrefix="foxwall" Src="~/Controls/PersonListDisplayControl.ascx" TagName="PersonListDisplayControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<foxwall:PersonListDisplayControl runat="server" ID="PersonListControl" /> 
<asp:Label ID="NoticeLabel" runat="server" CSSClass="Error"></asp:Label>

</asp:Content>
