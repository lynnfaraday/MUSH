<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditPersonControl.ascx.cs" Inherits="FoxwallDashboard.Controls.EditPersonControl" %>

<asp:Panel ID="ContentPanel" runat="server">
    <table>
        <tr>
            <td>
                <asp:Label ID="Label9" runat="server" CSSClass="labelTitle" Text="First Name:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="FirstNameBox" runat="server"></asp:TextBox>
            </td>
            <td>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="FirstNameBox" CSSClass="errorMessage">
            First name is required.
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" CSSClass="labelTitle" Text="Last Name:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="LastNameBox" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="LastNameBox" CSSClass="errorMessage">
            Last name is required.
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" CSSClass="labelTitle" Text="Username:"></asp:Label>
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
                <asp:Label ID="Label3" runat="server" CSSClass="labelTitle" Text="Password:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="PasswordBox" runat="server" TextMode="Password">
                </asp:TextBox>
            </td>
            <td>            
                <asp:Label ID="KeepOldPassword" runat="server" Text="Leave blank to keep the old password." />
                <asp:Label ID="PasswordError" runat="server" CSSClass="errorMessage" Visible="false"
                Text="Password must be at least 5 characters and contain at least 1 letter, 1 number and one special character (@#$%^&+=)."></asp:Label>                
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" CSSClass="labelTitle" Text="Confirm Password:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="ConfirmPasswordBox" runat="server" TextMode="Password">
                </asp:TextBox>
            </td>
            <td>            
                <asp:Label ID="PasswordMatchError" runat="server" CSSClass="errorMessage" Visible="false"
                Text="Passwords do not match."></asp:Label> 
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" CSSClass="labelTitle" Text="Is Active?"></asp:Label>
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
                <asp:Label ID="Label6" runat="server" CSSClass="labelTitle" Text="Is Administartor?"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="IsAdminBox" runat="server" />
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

<asp:Label ID="NoticeLabel" runat="server" CSSClass="errorMessage"></asp:Label>
