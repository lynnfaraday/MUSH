// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Default.aspx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the DefaultPage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using FoxwallDashboard.Database;

namespace FoxwallDashboard
{
    public partial class DefaultPage : System.Web.UI.Page
    {
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

            Response.Redirect("~/AddCall.aspx");
        }
    }

    
}
