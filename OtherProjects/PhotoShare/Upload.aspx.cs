// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Upload.aspx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the Upload type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web.UI.WebControls;
using App_Code;

public partial class Upload : BasePage
{
    protected void PageLoad(object sender, EventArgs e)
    {
        LoadPageForCurrentUser(LoginErrorButton, ErrorMessage, RealContent, WelcomeMessage);
    }

    protected override void LoadRealContent()
    {
        FileMessage.Text = "Select a ZIP or JPG file you'd like to share.";

        if (!Page.IsPostBack)
        {
            UsersList.Items.Clear();
            foreach (var user in WebUser.GetUsernames(DataDir))
            {
                // Don't add self to the list.
                if (user != CurrentUser.Username)
                {
                    UsersList.Items.Add(user);
                }
            }
        }
    }

    protected void UploadButtonClick(object sender, EventArgs e)
    {
        if (!FileUploader.HasFile)
        {
            FileMessage.Text = "You have to select a file first.";
            return;
        }

        if (!IsValidFileType(FileUploader.FileName))
        {
            FileMessage.Text = "Only ZIP files are allowed.";
            return;
        }

        var users = GetSelectedUsers();

        if (users.Count == 0)
        {
            FileMessage.Text = "You have to select someone to share with first.";
            return;
        }

        foreach (var username in users)
        {
            WebUser user = new WebUser(username, DataDir);
            string filename = Path.Combine(user.UserDir, FileUploader.FileName);
            if (File.Exists(filename))
            {
                FileMessage.Text = "That file already exists for someone.  Try another name.";
                return;
            }
        }

        // Yeah it sucks to have two loops but we really want to make sure the file doesn't exist before
        // uploading it to anyone.
        foreach (var username in users)
        {
            WebUser user = new WebUser(username, DataDir);
            string filename = Path.Combine(user.UserDir, FileUploader.FileName);
            FileUploader.SaveAs(filename);
            SendEmail(user, FileUploader.FileName);
        }

        FileMessage.Text = "File shared!";
    }

    private void SendEmail(WebUser user, string fileName)
    {
        string body = String.Format("{0} has shared some photos with you.  The file is {1}." +
                                      " Visit http://martiandreams.com/photos to get them.",
                                      CurrentUser.Username, fileName);

        var message = new MailMessage("donotreply@martiandreams.com", user.Email)
        {
            Subject = "You've Got Photos", 
            Body = body, 
            IsBodyHtml = false
        };

        // This will try to send the notification message using the webconfig mail settings.
        try
        {
            var smtp = new SmtpClient();
            smtp.Send(message);
        }
        catch (Exception)
        {
            ErrorMessage.Text = "There was a problem sending the notification email." +
                                " You may want to send one yourself.";
        }
    }

    bool IsValidFileType(string filename)
    {
        string[] extensions = {".zip", ".jpg", ".jpeg"};
        return extensions.Any(extension => filename.EndsWith(extension, false, null));
    }

    private List<string> GetSelectedUsers()
    {
        return (from ListItem item in UsersList.Items
                where item.Selected
                select item.Value).ToList();
    }
}
