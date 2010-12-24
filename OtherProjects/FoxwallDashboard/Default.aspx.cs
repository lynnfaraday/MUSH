// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Default.aspx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the DefaultPage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using FoxwallDashboard.Database;

namespace FoxwallDashboard
{
    public partial class DefaultPage : BasePage
    {
        // This is the login page so obviously we're not logged in yet.  Show the main content anyway.
        protected override void DoCustomPageLoad()
        {
            MainContent.Visible = true;
            NotLoggedInPanel.Visible = false;
        }

        protected void LoginButtonClick(object sender, EventArgs e)
        {
            var repo = new Repository();
            var user = repo.FindPerson(p => p.Username.ToLower() == UsernameBox.Text.ToLower());
            
            if (user == null)
            {
                NoticeLabel.Text = "User not found.";
                return;
            }

            if (user.Password != PasswordBox.Text) // TODO - hashing
            {
                NoticeLabel.Text = "Invalid password.";
                return;
            }

            Session["UserID"] = user.ID;
            Response.Redirect("~/AddCall.aspx");
        }
    }

    
}
