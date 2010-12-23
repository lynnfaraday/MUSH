<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchForCall.aspx.cs" Inherits="FoxwallDashboard.SearchForCall" %>
<%@ Register TagPrefix="foxwall" Src="~/CallMenu.ascx" TagName="CallMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
<foxwall:CallMenu runat="server" ID="CallMenu" /> 

<h1>Search for a Call</h1>
    <asp:Label ID="Label1" runat="server" Text="Incident Number:" CssClass="LabelTitle"></asp:Label>
    <asp:TextBox ID="IncidentNumberBox" runat="server"></asp:TextBox>
     <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Incident number must be a number."
                    ControlToValidate="IncidentNumberBox" Operator="DataTypeCheck" Type="Integer" CssClass="Error"/>
    <br />
    <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButtonClick" />
    <br />
    <asp:Label ID="NoticeLabel" runat="server" CssClass="Error"></asp:Label>
</asp:Content>
