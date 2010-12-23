// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecentCalls.aspx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the RecentCalls type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using FoxwallDashboard.Database;

namespace FoxwallDashboard
{
    public partial class RecentCalls : System.Web.UI.Page
    {
// ReSharper disable InconsistentNaming
        protected void Page_Load(object sender, EventArgs e)
// ReSharper restore InconsistentNaming
        {
            var repo = new Repository();
            var calls = repo.RecentCalls();

            // TODO: Clean up display.
            foreach (var call in calls)
            {
                var callString = call.IncidentNumber + " | " + call.Dispatched + " | " + call.Location;
                CallList.Items.Add(callString);
            }
        }
    }
}