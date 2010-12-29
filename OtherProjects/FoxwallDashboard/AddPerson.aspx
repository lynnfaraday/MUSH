<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddPerson.aspx.cs" Inherits="FoxwallDashboard.AddPerson" %>
<%@ Register TagPrefix="foxwall" Src="~/Controls/EditPersonControl.ascx" TagName="EditPersonControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<asp:LinkButton ID="LinkButton1" runat="server" OnClick="AddPersonClick" CausesValidation="false">[ Add Another Person ]</asp:LinkButton>
<asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/People.aspx" CausesValidation="false">[ Manage People ]</asp:LinkButton>
    
<h1>Add a Person</h1>

<asp:Panel runat="server" ID="EditPanel">
   <foxwall:EditPersonControl runat="server" ID="PersonControl" /> 
</asp:Panel>

<br />
<asp:Label ID="NoticeLabel" runat="server" CSSClass="errorMessage"></asp:Label>   

</asp:Content>
