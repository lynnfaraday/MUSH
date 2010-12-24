// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Help.aspx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the Help type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FoxwallDashboard
{
    public partial class Help : BasePage
    {
        protected override void DoCustomPageLoad()
        {
            // Help is always visible, even when not logged in.
            MainContent.Visible = true;
            NotLoggedInPanel.Visible = false;
        }
    }
}
