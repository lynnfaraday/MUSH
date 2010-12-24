<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CallMenu.ascx.cs" Inherits="FoxwallDashboard.Controls.CallMenu" %>

    <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/AddCall.aspx" CausesValidation="false">[ Add a Call ]</asp:LinkButton>
    <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/SearchForCall.aspx" CausesValidation="false">[ Search for a Call ]</asp:LinkButton>
    <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl="~/ListCalls.aspx?List=Recent" CausesValidation="false">[ Recent Calls ]</asp:LinkButton>