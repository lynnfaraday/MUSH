// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CallMenu.ascx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the CallMenu type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace FoxwallDashboard.Controls
{
    public partial class CallMenu : System.Web.UI.UserControl
    {
        protected void AddCallClick(object sender, EventArgs e)
        {
            // Can't use a postback because postback keeps you editing the same call.
            Response.Redirect("AddCall.aspx");
        }
    }
}