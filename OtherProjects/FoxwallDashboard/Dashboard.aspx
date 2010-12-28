<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="FoxwallDashboard.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" BackColor="LightGoldenrodYellow" Width="400">
     <h3>Notices</h3>
          <asp:Literal ID="Notices" runat="server"></asp:Literal>
    </asp:Panel>

<table cellpadding="5">
<tr>
<td><h2>Calls</h2></td>
<td><h2>People</h2></td>
</tr>

<tr>
<td><asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/AddCall.aspx">Add a Call</asp:LinkButton></td>
<td><asp:LinkButton ID="LinkButton7" runat="server" PostBackUrl="~/People.aspx">Manage People</asp:LinkButton></td>
</tr>

<tr>
<td><asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/SearchForCall.aspx">Search for a Call</asp:LinkButton></td>
<td>&nbsp;</td>
</tr>

<tr>
<td><asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl="~/ListCalls.aspx?List=Outstanding">All Outstanding Tripsheets</asp:LinkButton></td>
<td>&nbsp;</td>
</tr>


<tr>
<td><asp:LinkButton ID="LinkButton4" runat="server" PostBackUrl="~/Reports.aspx">Other Call Reports</asp:LinkButton></td>
<td>&nbsp;</td>
</tr>

</table>


</asp:Content>
