<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddCall.aspx.cs" Inherits="FoxwallDashboard.AddCall" %>
<%@ Register TagPrefix="foxwall" Src="~/EditCallControl.ascx" TagName="EditCallControl" %>
<%@ Register TagPrefix="foxwall" Src="~/CallMenu.ascx" TagName="CallMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    
<foxwall:CallMenu runat="server" ID="CallMenu" /> 

<asp:Literal ID="TitleLabel" runat="server" />

<foxwall:EditCallControl runat="server" ID="CallControl" /> 

</asp:Content>
