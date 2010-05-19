<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Help.aspx.cs" Inherits="FormatInstructions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Faraday's MU* Log Cleaner</title>
    <link rel="Stylesheet" type="text/css" href="StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h2>Faraday's RP Log Cleaner Instructions</h2>
    
    <p>The log cleaner takes raw log files (presumably captured by your favorite MU* client)
    and strips out junk such as:</p>
    <ul>
    <li>Pages</li>
    <li>Channels</li>
    <li>OOC Chatter</li>
    <li>Useless coming/going messages like <i>Bob has arrived.</i></li>
    <li>Some server output, like <i>Huh?</i> and <i>Set.</i></li>
    <li>A very limited subset of <a href="#softcode">softcoded commands</a>.</li>
    </ul>
    
        <p>
            It also adds blank lines (if necessary) so that the log lines will be spaced 
            out.&nbsp; If there are already blank lines in the log, it won&#39;t add new ones.</p>
    
    <h3>Bugs and Suggestions</h3>
    <p>Bugs and suggestions can be reported via either the <a href="http://groups.google.com/group/faramushcode">Mailing List</a>
    or <a href="http://code.google.com/p/faramushcode/issues/list">Bug Tracker</a>.
    </p>
    
    <h3>How it works</h3>
    <p>It's easy.</p>
    
    <ol>
    <li>Click the<i> Browse</i> button to find a RP log file on your computer.</li>
    <li>Select the desired <a href="#options">formatting options</a>.</li>
    <li>Click <i>Clean</i>.  The cleaned up log will appear in the preview window.</li>
    <li>Click <i>Save</i> to save the log as a file back to your hard disk <i>--or--</i></li>
    <li>Copy/paste the cleaned up text from the preview window into another location, such as a wiki editor.</li>
    </ol>
    
    <a name="options"></a>
    <h3>Formatting options</h3>
    <p>A drop-down list allows you to select a set of formatting rules.  These determine what 
    lines will be included and excluded from your log file during the cleaning process, and may
    also format certain log lines with colors or other markup. For now, only a limited set of formatting rules are available.  Eventually you'll be able to 
    make your own.  The current rule sets include:</p>
    <ul>
    <li><b>MUSH</b> - Excludes MUSH-style channels.  <i>&lt;Public&gt; Faraday says, "Hello."</i></li>
    <li><b>MUX</b> - Excludes MUX-style channels.  <i>[Public] Faraday says, "Hello."</i></li>
    <li><b>TGG Battle</b> - A special log format for battle logs from <a href="http://warisunlimited.wikidot.com/">The Greatest Generation MUX</a>, designed to 
    exclude spammy commands and OOC chatter, but preserve the important combat messages.</li>
    <li><b>TGG Battle Wiki</b> - Similar to the <i>TGG Battle</i> format, but also formats combat messages for pretty display on the wiki.</li>
    </ul>
    
        <p>
            All of the formats exclude the standard things mentioned above, like pages and 
            coming/going messages.&nbsp;&nbsp;&nbsp;
        </p>
    
    <a name="softcode"></a>
    <h3>Softcode Cleanup</h3>
    <p>The log cleaner will clean up some softcoded command output.  
    Primarily it strips off log lines starting with <i>&lt;</i>.  This covers many of the 
    <a href="http://wordsmyth.org/aresmush/index.php?title=Faraday_Softcode">Faraday Softcode</a> modules,
    including FS3 skills and combat.&nbsp; Note:
    The <i>TGG Battle</i> rules don't strip off all <i>&lt;</i> lines in order to preserve 
    combat command output.
    </p>
    <p>
    What the log cleaner <i>doesn't</i> handle is multi-line softcode output, like +who
    and +finger commands.  Those things you'll have to strip out manually.
    </p>
    
    <h3>Disclaimer n'at</h3>
    The program and all documentation are copyright 2010 by Linda Naughton.
    There is NO WARRANTY for this program, express or implied. You accept it as-is, and agree that the author is not responsible for any defects in operation or lost logs.
    </div>
    </form>
</body>
</html>
