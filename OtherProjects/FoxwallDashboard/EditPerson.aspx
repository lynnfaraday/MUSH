<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditPerson.aspx.cs" Inherits="FoxwallDashboard.EditPerson" %>
<%@ Register TagPrefix="foxwall" Src="~/Controls/EditPersonControl.ascx" TagName="EditPersonControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
<asp:Literal ID="TitleLabel" runat="server" />

<foxwall:EditPersonControl runat="server" ID="PersonControl" /> 

</asp:Content>
