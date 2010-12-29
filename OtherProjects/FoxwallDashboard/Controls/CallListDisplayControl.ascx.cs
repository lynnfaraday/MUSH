// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CallListDisplayControl.ascx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the CallListDisplayControl type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoxwallDashboard.Database;
using FoxwallDashboard.Models;

namespace FoxwallDashboard.Controls
{
    public partial class CallListDisplayControl : System.Web.UI.UserControl
    {
        private readonly Repository _repo;

        public CallListDisplayControl()
        {
            _repo = new Repository();
        }
        public IEnumerable<Call> Calls
        {
            set
            {
                StringBuilder builder = new StringBuilder();

                builder.AppendLine("<table border=\"1\">");

                builder.AppendLine("<tr>");
                builder.AppendLine("<td><b>Incident Number</b></td>");
                builder.AppendLine("<td><b>State Number</b></td>");
                builder.AppendLine("<td><b>Dispatched</b></td>");
                builder.AppendLine("<td><b>Chief Complaint</b></td>");
                builder.AppendLine("<td><b>Location</b></td>");
                builder.AppendLine("<td><b>Borough</b></td>");
                builder.AppendLine("<td><b>Disposition</b></td>");
                builder.AppendLine("<td><b>Crew</b></td>");
                builder.AppendLine("<td>&nbsp;</td>");

                builder.AppendLine("</tr>");

                foreach (var call in value)
                {
                    try
                    {
                        var people = _repo.FindPeopleForCall(call.CallID);

                        builder.AppendLine("<tr>");
                        AddColumn(builder, call.IncidentNumber.ToString());
                        AddColumn(builder, call.StateNumber);
                        AddColumn(builder, call.LocalDispatchedTime.ToString());
                        AddColumn(builder, call.ChiefComplaint);
                        AddColumn(builder, call.Location);
                        AddColumn(builder, call.Borough);
                        AddColumn(builder, call.Disposition);

                        builder.AppendLine("<td>");

                        if (people.Any())
                        {
                            foreach (var personID in people)
                            {
                                var person = _repo.FindPersonByID(personID);
                                builder.AppendLine(person.DisplayName + "<br/>");
                            }
                        }
                        else
                        {
                            builder.Append("&nbsp;");
                        }
                        builder.AppendLine("</td>");
                        builder.AppendLine("<td><a href=\"EditCall.aspx?CallID=" + call.CallID + "\">[ Edit ]</a></td>");

                        builder.AppendLine("</tr>");
                    }
                    catch (Exception ex)
                    {
                        builder.AppendLine(string.Format("<tr><td colspan=8>Unable to load call {0}: {1}</td></tr>", call.CallID, ex.Message));
                    }

                }

                builder.AppendLine("</table>");

                CallList.Text = builder.ToString();
            }
        }

        private static void AddColumn(StringBuilder builder, string columnValue)
        {
            builder.AppendLine("<td>" + (string.IsNullOrEmpty(columnValue) ? "&nbsp;" : columnValue) + "</td>");
        }
    }
}