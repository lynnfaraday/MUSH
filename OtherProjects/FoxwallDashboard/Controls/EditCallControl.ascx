<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditCallControl.ascx.cs"
    Inherits="FoxwallDashboard.Controls.EditCallControl" %>
<asp:Panel ID="ContentPanel" runat="server">
    <table>
        <tr>
            <td>
                <asp:Label ID="Label9" runat="server" CssClass="Incidentnumber" Text="Incident Number:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="IncidentNumberValue" runat="server" CssClass="Incidentnumber"></asp:Label>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" CSSClass="labelTitle" Text="EMS Charts PRID #:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="StateNumberBox" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="State number must be a number."
                    ControlToValidate="StateNumberBox" Operator="DataTypeCheck" Type="Integer" CSSClass="errorMessage" />
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Label ID="Label2" runat="server" CSSClass="labelTitle" Text="Dispatch Date:"></asp:Label>
            </td>
            <td>
                <asp:Calendar ID="DispatchedCalendar" runat="server" OnDayRender="RenderCalendar">
                </asp:Calendar>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" CSSClass="labelTitle" Text="Dispatch Time (HH:MM):"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="DispatchTimeBox" runat="server" MaxLength="5">
                </asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="DispatchTimeBox" CSSClass="errorMessage">
            Dispatch Time is required.
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="DispatchTimeBox"
                    ErrorMessage="Dispatch time should be HH:MM in military time.  For example: 18:24."
                    ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" CSSClass="errorMessage" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" CSSClass="labelTitle" Text="Location:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="LocationBox" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="LocationBox" CSSClass="errorMessage">
  Location is required.
                </asp:RequiredFieldValidator>
            </td> 
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" CSSClass="labelTitle" Text="Borough:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="BoroughSelection" runat="server">
                    <asp:ListItem Value="--Select One--" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="Aspinwall"></asp:ListItem>
                    <asp:ListItem Value="Blawnox"></asp:ListItem>
                    <asp:ListItem Value="Fox Chapel"></asp:ListItem>
                    <asp:ListItem Value="Indiana Twp"></asp:ListItem>
                    <asp:ListItem Value="Oakmont"></asp:ListItem>
                    <asp:ListItem Value="O'Hara"></asp:ListItem>
                    <asp:ListItem Value="Pittsburgh (City Of)"></asp:ListItem>
                    <asp:ListItem Value="Sharpsburg"></asp:ListItem>
                    <asp:ListItem Value="Other"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="You must select a borough."
                    ControlToValidate="BoroughSelection" InitialValue="--Select One--" CSSClass="errorMessage">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" CSSClass="labelTitle" Text="Chief Complaint/Dispatch:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="ChiefComplaintBox" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ChiefComplaintBox" CSSClass="errorMessage">
  Chief Complaint / Summary is required.
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" CSSClass="labelTitle" Text="Age:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="AgeBox" runat="server"></asp:TextBox>
                <asp:DropDownList ID="AgeUnitsSelection" runat="server">
                    <asp:ListItem Value="Years"></asp:ListItem>
                    <asp:ListItem Value="Months"></asp:ListItem>
                    <asp:ListItem Value="Days"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Age must be a number."
                    ControlToValidate="AgeBox" Operator="DataTypeCheck" Type="Integer" CSSClass="errorMessage"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label8" runat="server" CSSClass="labelTitle" Text="Disposition:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DispositionSelection" runat="server">
                    <asp:ListItem Value="--Select One--" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="Allegheny General"></asp:ListItem>
                    <asp:ListItem Value="Allegheny Valley"></asp:ListItem>
                    <asp:ListItem Value="Cancelled"></asp:ListItem>
                    <asp:ListItem Value="Childrens' Hospital"></asp:ListItem>
                    <asp:ListItem Value="False Call"></asp:ListItem>
                    <asp:ListItem Value="Magee"></asp:ListItem>
                    <asp:ListItem Value="Mercy"></asp:ListItem>
                    <asp:ListItem Value="Passavant"></asp:ListItem>
                    <asp:ListItem Value="Presbyterian"></asp:ListItem>
                    <asp:ListItem Value="Refusal"></asp:ListItem>
                    <asp:ListItem Value="Shadyside"></asp:ListItem>
                    <asp:ListItem Value="St. Margaret's"></asp:ListItem>
                    <asp:ListItem Value="Standby"></asp:ListItem>
                    <asp:ListItem Value="Other"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="You must select a disposition."
                    ControlToValidate="DispositionSelection" InitialValue="--Select One--" CSSClass="errorMessage">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Label ID="Label11" runat="server" CSSClass="labelTitle" Text="Crew Members:"></asp:Label>
            </td>
            <td>
               <asp:Panel ScrollBars="Auto" Height="100" runat="server" BorderColor="LightGray" BorderWidth="1" >
                <asp:CheckBoxList ID="CrewList" runat="server" Width="200">
                </asp:CheckBoxList>
               </asp:Panel>
            </td>
            <td>
                <asp:CustomValidator runat="server" OnServerValidate="VerifyCrewPresent" 
                ErrorMessage="You must select at least one crew member." CSSClass="errorMessage" />
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
