<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="People.aspx.cs" Inherits="FoxwallDashboard.People" %>
<%@ Register TagPrefix="foxwall" Src="~/Controls/EditPersonControl.ascx" TagName="EditPersonControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

Coming soon.

<br /><br /><br /><br /><br /><br />
<hr />

<foxwall:EditPersonControl runat="server" ID="PersonControl" /> 

</asp:Content>
