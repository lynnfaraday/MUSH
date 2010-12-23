// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecentCalls.aspx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the RecentCalls type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Text;
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

            StringBuilder builder = new StringBuilder();

            builder.AppendLine("<table border=\"1\">");

            builder.AppendLine("<tr>");
            builder.AppendLine("<td>Incident Number</td>");
            builder.AppendLine("<td>State Number</td>");
            builder.AppendLine("<td>Dispatched</td>");
            builder.AppendLine("<td>Chief Complaint</td>");
            builder.AppendLine("<td>Location</td>");
            builder.AppendLine("<td>Borough</td>");
            builder.AppendLine("<td>Disposition</td>");
            builder.AppendLine("<td>Crew</td>");
            builder.AppendLine("<td>&nbsp;</td>");

            builder.AppendLine("</tr>");

            // TODO: Clean up display.  Add error checking.  Fix empty columns.
            foreach (var call in calls)
            {
                var people = repo.FindPeopleForCall(call.CallID);

                builder.AppendLine("<tr>");
                builder.AppendLine("<td>" + call.IncidentNumber + "</td>");
                builder.AppendLine("<td>" + call.StateNumber + "</td>");
                builder.AppendLine("<td>" + call.Dispatched + "</td>");
                builder.AppendLine("<td>" + call.ChiefComplaint + "</td>");
                builder.AppendLine("<td>" + call.Location + "</td>");
                builder.AppendLine("<td>" + call.Borough + "</td>");
                builder.AppendLine("<td>" + call.Disposition + "</td>");
                builder.AppendLine("<td>");
                
                foreach (var personID in people)
                {
                    var person = repo.FindPersonByID(personID);
                    builder.AppendLine(person.DisplayName + "<br/>");
                }
                builder.AppendLine("</td>");
                builder.AppendLine("<td><a href=\"~/AddCall.aspx?CallID=" + call.CallID + "\">[ Edit ]</a></td>");
                
                builder.AppendLine("</tr>");
            }

            builder.AppendLine("</table>");

            CallList.Text = builder.ToString();
        }
    }
}