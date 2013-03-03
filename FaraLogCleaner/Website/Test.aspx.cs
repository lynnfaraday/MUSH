using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Literal1.Text = "";
        TestLineFormatParsing();
        TestLineParsing();
    }

    private void TestLineParsing()
    {
        AppendText("Executing line parse test", "blue");

        LogLineFormat lineFormat = new LogLineFormat("^.*test.*$", LogLineFormat.ActionType.Replace, LogLineFormat.MatchType.Regex, "x$&y");
        List<LogLineFormat> lineFormats = new List<LogLineFormat>();
        lineFormats.Add(lineFormat);
        LogFormat format = new LogFormat(lineFormats);               
        StringBuilder builder = new StringBuilder();
        format.Parse("this is a test", builder);
        string gotString = builder.ToString();

        if (gotString == "xthis is a testy\r\n")
        {
            AppendText("PASSED", "green");
        }
        else
        {
            AppendText("Failed line parsing.  Got: " + gotString, "red");
        }
    }

    private void TestLineFormatParsing()
    {
        CheckLineFormat("Comment is omitted.", "# Comment");
        CheckLineFormat("Blank line is omitted.", "");
        CheckLineFormat("Bad number of args is omitted.", "x|y|z|a|b");
        CheckLineFormat("Defaults to omit", "<", LogLineFormat.ActionType.Omit, LogLineFormat.MatchType.Regex, "<", String.Empty);
        CheckLineFormat("Simple omit test", "Omit|<", LogLineFormat.ActionType.Omit, LogLineFormat.MatchType.Regex, "<", String.Empty);
        CheckLineFormat("Simple include test", "Include|<Radio>", LogLineFormat.ActionType.Include, LogLineFormat.MatchType.Regex, "<Radio>", String.Empty);
        CheckLineFormat("Simple replace test", "Replace|suffers damage|x suffers damage y", LogLineFormat.ActionType.Replace, LogLineFormat.MatchType.Regex, "suffers damage", "x suffers damage y");
    }

    private void CheckLineFormat(string textTitle, string parseString)
    {
        AppendText("Executing test " + textTitle, "blue");
        LogLineFormat format = LogFormat.ParseLogLineFormat(parseString);
        if (format != null)
        {
            AppendText("Format was not null", "red");
        }
        AppendText("PASSED", "green");
    }
    private void CheckLineFormat(string textTitle, string parseString,  
        LogLineFormat.ActionType expectAction, LogLineFormat.MatchType expectMatch, string expectRegex, string expectReplace)
    {
        AppendText("Executing test " + textTitle, "blue");
        LogLineFormat lineFormat = LogFormat.ParseLogLineFormat(parseString);

        if (lineFormat == null)
        {
            AppendText("Format not parsed successfully.", "red");
            return;
        }
        if (lineFormat.Action != expectAction)
        {
            AppendText(String.Format("Action wrong: Expected {0} Got {1}", expectAction, lineFormat.Action), "red");
            return;
        }
        if (lineFormat.Match != expectMatch)
        {
            AppendText(String.Format("Match type wrong: Expected {0} Got {1}", expectMatch, lineFormat.Match), "red");
            return;
        }
        if (lineFormat.RegexString != expectRegex)
        {
            AppendText(String.Format("Regex wrong: Expected {0} Got {1}", expectRegex, lineFormat.RegexString), "red");
            return;
        }
        if (lineFormat.ReplaceText != expectReplace)
        {
            AppendText(String.Format("Replace text wrong: Expected {0} Got {1}", expectReplace, lineFormat.ReplaceText), "red");
            return;
        }
        AppendText("PASSED", "green");
    }

    private void AppendText(string message, string color)
    {
        Literal1.Text += "<br><font color=\"" + color + "\">" + message + "</font>";
    }
}
