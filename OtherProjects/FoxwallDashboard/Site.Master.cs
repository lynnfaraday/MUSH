// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Site.Master.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the SiteMaster type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace FoxwallDashboard
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
// ReSharper disable InconsistentNaming
        protected void Page_Load(object sender, EventArgs e)
// ReSharper restore InconsistentNaming
        {

        }

        protected void LogoutClick(object sender, EventArgs e)
        {
            Session[BasePage.UserIDSessionKey] = null;
            Response.Redirect("~/Default.aspx");
        }
    }
}
