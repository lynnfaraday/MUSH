// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PersonListDisplayControl.ascx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the PersonListDisplayControl type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using FoxwallDashboard.Models;

namespace FoxwallDashboard.Controls
{
    public partial class PersonListDisplayControl : System.Web.UI.UserControl
    {
        public IEnumerable<Person> People
        {
            set
            {
                StringBuilder builder = new StringBuilder();

                builder.AppendLine("<table border=\"1\">");

                builder.AppendLine("<tr>");
                builder.AppendLine("<td><b>Username</b></td>");
                builder.AppendLine("<td><b>Name</b></td>");
                builder.AppendLine("<td><b>Active</b></td>");
                builder.AppendLine("<td>&nbsp;</td>");

                builder.AppendLine("</tr>");

                foreach (var person in value)
                {
                    try
                    {
                        builder.AppendLine("<tr>");
                        AddColumn(builder, person.Username);
                        AddColumn(builder, person.DisplayName);
                        AddColumn(builder, person.Active.ToString());

                        builder.AppendLine("<td><a href=\"EditPerson.aspx?PersonID=" + person.ID + "\">[ Edit ]</a></td>");
                        builder.AppendLine("</tr>");
                    }
                    catch (Exception ex)
                    {
                        builder.AppendLine(string.Format("<tr><td colspan=8>Unable to load person {0}: {1}</td></tr>", person.ID, ex.Message));
                    }

                }

                builder.AppendLine("<tr><td><a href=\"AddPerson.aspx\">[ Add ]</a></td></tr>");
                builder.AppendLine("</table>");

                PersonList.Text = builder.ToString();
            }
        }

        private static void AddColumn(StringBuilder builder, string columnValue)
        {
            builder.AppendLine("<td>" + (string.IsNullOrEmpty(columnValue) ? "&nbsp;" : columnValue) + "</td>");
        }
    }
}