// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListCalls.aspx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the ListCalls type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using FoxwallDashboard.Database;
using FoxwallDashboard.Models;

namespace FoxwallDashboard
{
    public partial class ListCalls : BasePage
    {
        protected override void DoCustomPageLoad()
        {
            try
            {
                // TODO : Make handlers for this and other page loads.
                var repo = new Repository();
                var listType = Request.QueryString["List"];
                switch (listType)
                {
                    case "Recent":
                        TitleLabel.Text = "<h1>Recent Calls</h1>";
                        CallList.Calls = repo.RecentCalls();
                        CallMenu.Visible = true;
                        break;
                    case "Search":
                        var incidentNumber = (string)Session["SearchCriteria"]; // TODO - make object
                        CallList.Calls = repo.FindCalls(c => c.IncidentNumber.ToString().Contains(incidentNumber));
                        TitleLabel.Text = "<h1>Search Results</h1>";
                        CallMenu.Visible = true;
                        break;
                    case "Outstanding":
                        TitleLabel.Text = "<h1>Outstanding Calls</h1>";
                        CallList.Calls = repo.OutstandingCalls();
                        CallMenu.Visible = false;
                        break;
                    case "Mine":
                        TitleLabel.Text = "<h1>My Calls</h1>";
                        var userID = (Guid)Session[UserIDSessionKey];
                        var callIDs = repo.FindCallsForPerson(userID);
                        List<Call> calls = callIDs.Select(repo.FindCallByID).ToList();
                        calls.OrderByDescending(c => c.IncidentNumber);
                        CallList.Calls = calls;
                        CallMenu.Visible = false;
                        break;
                    default:
                        throw new Exception("Unrecognized list type " + listType);
                }
            }
            catch (Exception ex)
            {
                NoticeLabel.Text = "Error loading call report.  Please try again. " +
                                   "If the problem persists, contact support and mention the following message: " + ex.Message;
            }
        }
    }
}