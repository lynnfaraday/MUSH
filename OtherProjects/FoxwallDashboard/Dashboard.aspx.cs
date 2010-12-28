// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Dashboard.aspx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the Dashboard type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Text;
using FoxwallDashboard.Database;

namespace FoxwallDashboard
{
    public partial class Dashboard : BasePage
    {
        private Repository _repo;

        public Dashboard()
        {
            _repo = new Repository();
        }
        protected override void DoCustomPageLoad()
        {
            try
            {
                var outstandingCalls = _repo.OutstandingCallsForUser(CurrentUser.ID);
                if (outstandingCalls.Any())
                {
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine("<b>You have oustanding tripsheets!");
                    builder.AppendLine("<ul>");
                    foreach (var call in outstandingCalls)
                    {
                        builder.AppendLine("<li><a href=\"EditCall.aspx?CallID=" + call.CallID + "\">" + call.IncidentNumber + "</a></li>");
                    }
                    builder.AppendLine("</ul>");
                    Notices.Text = builder.ToString();
                }
                else
                {
                    Notices.Text = "You have no outstanding tripsheets!";
                }
            }
            catch
            {
                Notices.Text = string.Empty;
            }
        }
    }
}