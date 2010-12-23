<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditPersonControl.ascx.cs" Inherits="FoxwallDashboard.EditPersonControl" %>

<asp:Panel ID="ContentPanel" runat="server">
    <table>
        <tr>
            <td>
                <asp:Label ID="Label9" runat="server" CssClass="LabelTitle" Text="First Name:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="FirstNameBox" runat="server"></asp:TextBox>
            </td>
            <td>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="FirstNameBox">
            First name is required.
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" CssClass="LabelTitle" Text="Last Name:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="LastNameBox" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="LastNameBox">
            Last name is required.
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" CssClass="LabelTitle" Text="Username:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="UsernameBox" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" CssClass="LabelTitle" Text="Password:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="PasswordBox" runat="server" TextMode="Password">
                </asp:TextBox>
            </td>
            <td>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="PasswordBox"
                    ErrorMessage="Password must be at least 5 characters and contain at least 1 letter, 1 number and one special character (@#$%^&+=)."
                     OnServerValidate="ValidatePassword" ValidateEmptyText="true"
                     ></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" CssClass="LabelTitle" Text="Is Active?"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="IsActiveBox" runat="server" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="SaveButton" runat="server" OnClick="SaveButtonClick" Text="Save" />
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Panel>

<asp:Label ID="NoticeLabel" runat="server" CssClass="Error"></asp:Label>
