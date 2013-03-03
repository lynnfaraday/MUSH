using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using App_Code;

public partial class _Default : System.Web.UI.Page 
{
    // These configurations are always added, regardless of config file.
    private const string CommonConfigFileName = "common.cfg";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(CleanedLog.Text))
            {
                SaveButton.Visible = false;
            }
            else
            {
                SaveButton.Visible = true;
            }
            if (!Page.IsPostBack)
            {
                ErrorMsg.Text = String.Empty;

                LogFormatSelector.Items.Clear();
                var formatDir = Server.MapPath("Formats");
                var files = Directory.GetFiles(formatDir);
                foreach (string filePath in files)
                {
                    FileInfo info = new FileInfo(filePath);
                    string fileName = info.Name;

                    // Skip the common file.
                    if (fileName != CommonConfigFileName)
                    {
                        LogFormatSelector.Items.Add(fileName);
                    }
                }

                LogFormatSelector.SelectedValue = "mux.cfg";
            }
            LoadLogFormat();
        }
        catch (Exception)
        {
            ErrorMsg.Text = "No log formats found.  Something must be wrong on the web server.  Please try back later.";
        }

    }

    protected void HandleFormatSelectionChanged(object sender, EventArgs e)
    {
        LoadLogFormat();
    }

    private void LoadLogFormat()
    {
        try
        {
            var formatDir = Server.MapPath("Formats");
            LogFormatText.Text = File.ReadAllText(Path.Combine(formatDir, CommonConfigFileName));
            LogFormatText.Text += File.ReadAllText(Path.Combine(formatDir, LogFormatSelector.SelectedValue));
        }
        catch (Exception)
        {
            ErrorMsg.Text = "Could not load format file.  Try a different one.";
        }
    }

    protected void HandleCleanLog(object sender, EventArgs args)
    {
        CleanedLog.Text = String.Empty;
        
        if (!LogFileUploader.HasFile)
        {
            ErrorMsg.Text = "You have to select a file first.";
            return;
        }

        if (LogFileUploader.FileBytes.GetUpperBound(0) > 500000)
        {
            ErrorMsg.Text = "That log file is too big.  Make sure it's actually a text file.";
            return;
        }
        //if (LogFileUploader.PostedFile.ContentType != "text/plain")
        //{
        //  ErrorMsg.Text = "You can only clean text files.  This file is " + LogFileUploader.PostedFile.ContentType;
        //  return;
        //}

        var lineFormats = LogFormat.Read(LogFormatText.Text);

        try
        {
            StreamReader reader = new StreamReader(LogFileUploader.FileContent);
            LogFormat format = new LogFormat(lineFormats);
            CleanedLog.Text = format.Parse(reader);
            ErrorMsg.Text = "";
            SaveButton.Visible = true;
            Session["lastLogName"] = GetLogName(LogFileUploader.FileName);
        }
        catch (Exception e)
        {
            ErrorMsg.Text = "Error parsing log: " + e;
            return;
        }
    }

    private string GetLogName(string filename)
    {
        filename = string.Format("{0:yyyy-MM-dd} {1}", DateTime.Now, filename);
        if (!filename.EndsWith(".txt"))
        {
            filename += ".txt";
        }
        filename = filename.Replace(' ', '_');
        return filename;
    }

    protected void HandleSaveLog(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(CleanedLog.Text))
        {
            ErrorMsg.Text = "You have to clean the log first.";
            return;
        }

        string logName;
        try
        {
            logName = (string)Session["lastLogName"];
        }
        catch (Exception ex)
        {
            DebugMessages.Text = ex.ToString();
            logName = "log";
        }
        Response.Clear();
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + logName);
        Response.Write(CleanedLog.Text);
        Response.End();
    }
}
