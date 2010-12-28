<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListCalls.aspx.cs" Inherits="FoxwallDashboard.ListCalls" %>

<%@ Register TagPrefix="foxwall" Src="~/Controls/CallMenu.ascx" TagName="CallMenu" %>
<%@ Register TagPrefix="foxwall" Src="~/Controls/CallListDisplayControl.ascx" TagName="CallListDisplayControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
<foxwall:CallMenu runat="server" ID="CallMenu" /> 
    
<asp:Literal ID="TitleLabel" runat="server" />

<foxwall:CallListDisplayControl runat="server" ID="CallList" /> 
<asp:Label ID="NoticeLabel" runat="server" CSSClass="errorMessage"></asp:Label>

</asp:Content>
