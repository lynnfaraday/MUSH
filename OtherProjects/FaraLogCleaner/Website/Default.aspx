<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" validateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Faraday's MU* Log Cleaner</title>
    <link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:scriptmanager ID="Scriptmanager1" runat="server"></asp:scriptmanager>

<h2>Faraday's RP Log Cleaner</h2>    
<p><font color="red"><i>This tool is in beta test.  It is suggested that you keep copies of your raw log files, just in case.</i></font>
    <br />
    Read the <a href="Help.aspx"><b>instructions</b></a> and introduction to the tool.</p>

    <asp:Label ID="Label1" runat="server" Text="Select a Log File" Font-Bold="true"></asp:Label>
    <br />
    <asp:FileUpload ID="LogFileUploader" runat="server" Width="397px" />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server"> <ContentTemplate>
    
   
    <br /><br />
    
    <asp:Label ID="Label2" runat="server" Text="Select Formatting Rules" 
            Font-Bold="True"></asp:Label>
    <br />
    <br />
    <asp:DropDownList ID="LogFormatSelector" runat="server" OnSelectedIndexChanged="HandleFormatSelectionChanged" AutoPostBack="true">
    </asp:DropDownList>
    <br /><br />
<!--        <br />
            <br /> -->

    <!-- <br /> -->
        <asp:Label ID="ErrorMsg" ForeColor="Red" Font-Bold="true" runat="server"></asp:Label>   
        <asp:TextBox ID="LogFormatText" runat="server" Width="600" Height="20px" 
            TextMode="MultiLine" Visible="false"></asp:TextBox>

    </ContentTemplate></asp:UpdatePanel>
    
        <br />
    <asp:Button ID="CleanButton" runat="server" Text="Clean Log" OnClick="HandleCleanLog" />
    <asp:Button ID="SaveButton" runat="server" Text="Save Log" OnClick="HandleSaveLog" />
    
    <br /> <hr />   <br />
    <asp:Label ID="Label4" runat="server" Text="Preview Cleaned Log" Font-Bold="true"></asp:Label>
    <br />
    <asp:TextBox ID="CleanedLog" runat="server" Width="600" Height="400" TextMode="MultiLine"></asp:TextBox>
    
    
    <asp:Label ID="DebugMessages" ForeColor="gray" Font-Italic="true" Font-Size="Smaller" runat="server"></asp:Label>   
    </form>
</body>
</html>
