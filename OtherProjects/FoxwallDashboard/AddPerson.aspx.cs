// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddPerson.aspx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the AddPerson type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace FoxwallDashboard
{
    public partial class AddPerson : BasePage
    {
        protected override System.Web.UI.WebControls.Button DefaultButton
        {
            get { return PersonControl.DefaultButton; }
        }
        protected override System.Web.UI.Control DefaultFocus
        {
            get { return PersonControl.DefaultFocus; }
        }

        protected override void DoCustomPageLoad()
        {
            try
            {
                if (!CurrentUser.Administrator)
                {
                    EditPanel.Visible = false;
                    NoticeLabel.Text = "Only administrators may manage people.";
                }
            }
            catch (Exception ex)
            {
                NoticeLabel.Text = "Error loading people management page.  Please try again. " +
                                  "If the problem persists, contact support and mention the following message: " + ex.Message;
            }
        }

        protected void AddPersonClick(object sender, EventArgs e)
        {
            // Can't use a postback because postback keeps you editing the same person.
            Response.Redirect("AddPerson.aspx");
        }
    }
}