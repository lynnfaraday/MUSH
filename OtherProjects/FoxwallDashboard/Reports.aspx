<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="FoxwallDashboard.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Reports</h1>

<p>Please select a report:</p>

<asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/ListCalls.aspx?List=Outstanding">Outstanding Tripsheets</asp:LinkButton>
<br />
<asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/ListCalls.aspx?List=Mine">My Calls</asp:LinkButton>

</asp:Content>
