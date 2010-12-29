// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChangePassword.aspx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the ChangePassword type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using FoxwallDashboard.Database;
using FoxwallDashboard.Handlers;

namespace FoxwallDashboard
{
    public partial class ChangePassword : BasePage
    {
        private readonly Repository _repo;

        protected override Button DefaultButton
        {
            get { return ChangeButton; }
        }

        protected override Control DefaultFocus
        {
            get { return OldPasswordBox; }
        }
        
        public ChangePassword()
        {
            _repo = new Repository();
        }

        protected void ChangeButtonClick(object sender, EventArgs e)
        {
            // This validation can't be done by a validator because ASP forgets the password on postback
            try
            {
                PasswordMatchError.Visible = false;
                PasswordError.Visible = false;
                OldPasswordError.Visible = false;

                var handler = new ChangePasswordHandler(_repo);
                handler.Handle(CurrentUser.ID, PasswordBox.Text, ConfirmPasswordBox.Text, OldPasswordBox.Text);

                NoticeLabel.Text = "Password saved!";
            }
            // Password exceptions shouldn't trigger our big error notice, just alittle warning.
            catch (InvalidPasswordException)
            {
                PasswordError.Visible = true;
            }
            catch (PasswordsDontMatchException)
            {
                PasswordMatchError.Visible = true;
            }
            catch (PasswordEntryInvalidException)
            {
                OldPasswordError.Visible = true;
            }
            catch (Exception ex)
            {
                NoticeLabel.Text = "Error changing password.  Please try again. " +
                                   "If the problem persists, contact support and mention the following message: " + ex.Message;
            }
        }
    }
}