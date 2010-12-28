// --------------------------------------------------------------------------------------------------------------------
// <copyright file="People.aspx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the People type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using FoxwallDashboard.Database;

namespace FoxwallDashboard
{
    public partial class People : BasePage
    {
        protected override void DoCustomPageLoad()
        {
            try
            {
                var repo = new Repository();
                PersonListControl.People = repo.AllPeople();
            }
            catch (Exception ex)
            {
                NoticeLabel.Text = "Error loading people.  Please try again. " +
                                   "If the problem persists, contact support and mention the following message: " + ex.Message;
            }
        }
    }
}