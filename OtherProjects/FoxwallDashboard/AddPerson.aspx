<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddPerson.aspx.cs" Inherits="FoxwallDashboard.AddPerson" %>
<%@ Register TagPrefix="foxwall" Src="~/Controls/EditPersonControl.ascx" TagName="EditPersonControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/People.aspx" CausesValidation="false">[ Manage People ]</asp:LinkButton>
    
<h1>Add a Person</h1>

<foxwall:EditPersonControl runat="server" ID="PersonControl" /> 
    
<br />

</asp:Content>
