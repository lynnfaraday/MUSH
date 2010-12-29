// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListCalls.aspx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the ListCalls type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using FoxwallDashboard.Database;
using FoxwallDashboard.Handlers;
using FoxwallDashboard.Models;

namespace FoxwallDashboard
{
    public partial class ListCalls : BasePage
    {
        private readonly Repository _repo;

        public ListCalls()
        {
            _repo = new Repository();
        }

        protected override void DoCustomPageLoad()
        {
            try
            {
                var handler = new ListCallsLoadHandler(_repo);
                var result = handler.Handle(Request.QueryString, CurrentUser, (SearchCriteria)Session[SearchCriteria.SearchCriteriaSessionKey]); 

                switch (result.ListType)
                {
                    case ListCallsResult.CallListEnum.Search:
                        TitleLabel.Text = "<h1>Search Results</h1>";
                        break;
                    case ListCallsResult.CallListEnum.Mine:
                        TitleLabel.Text = "<h1>My Calls</h1>";
                        break;
                    case ListCallsResult.CallListEnum.Outstanding:
                        TitleLabel.Text = "<h1>Outstanding Tripsheets</h1>";
                        break;
                    case ListCallsResult.CallListEnum.Recent:
                        TitleLabel.Text = "<h1>Recent Calls</h1>";
                        break;
                    default:
                        TitleLabel.Text = "<h1>" + result.ListType + "</h1>";
                        break;
                }
                CallList.Calls = result.CallList;
            }
            catch (Exception ex)
            {
                NoticeLabel.Text = "Error loading call report.  Please try again. " +
                                   "If the problem persists, contact support and mention the following message: " + ex.Message;
            }
        }
    }
}