// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PhotoList.aspx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the PhotoList type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Web.UI.WebControls;
using App_Code;

public partial class PhotoList : BasePage
{
    protected void PageLoad(object sender, EventArgs e)
    {
        LoadPageForCurrentUser(LoginErrorButton, ErrorMessage, RealContent, WelcomeMessage);
    }

    protected override void LoadRealContent()
    {
        var files = CurrentUser.GetFiles();
        if (files.Count == 0)
        {
            ErrorMessage.Visible = true;
            FilesList.Visible = false;
            FilesMessage.Text = "Sorry, you have no files today.";
            DownloadButton.Visible = false;
            DeleteButton.Visible = false;
        }
        else
        {
            ErrorMessage.Visible = false;
            FilesList.Visible = true;
            DownloadButton.Visible = true;
            DeleteButton.Visible = true;
            FilesMessage.Text = "Here are the files waiting for you.  Select one to download or delete.";

            if (!Page.IsPostBack)
            {
                FilesList.Items.Clear();
                foreach (var file in files)
                {
                    FilesList.Items.Add(new ListItem(file.Name));
                }
            }
        }
    }


    protected void DeleteButtonClick(object sender, EventArgs e)
    {
        if (FilesList.SelectedItem == null)
        {
            FilesMessage.Text = "You have to select a file first!";
            return;
        }

        File.Delete(GetSelectedFilePath());
        FilesList.Items.Remove(FilesList.SelectedItem);
    }

    protected void DownloadButtonClick(object sender, EventArgs e)
    {
        if (FilesList.SelectedItem == null)
        {
            FilesMessage.Text = "You have to select a file first!";
            return;
        }

        string filePath = GetSelectedFilePath();
        Response.ContentType = "application/zip";
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + FilesList.SelectedValue);
        Response.TransmitFile(filePath);
        Response.End();
    }

    private string GetSelectedFilePath()
    {
        string dir = Path.Combine(DataDir, CurrentUser.Username);
        return Path.Combine(dir, FilesList.SelectedValue);
    }
}
