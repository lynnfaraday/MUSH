// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchForCall.aspx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the SearchForCall type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using FoxwallDashboard.Models;

namespace FoxwallDashboard
{
    public partial class SearchForCall : BasePage
    {
        protected override System.Web.UI.WebControls.Button DefaultButton
        {
            get { return SearchButton; }
        }
        protected override System.Web.UI.Control DefaultFocus
        {
            get { return IncidentNumberBox; }
        }

        protected void SearchButtonClick(object sender, EventArgs e)
        {
            Session[SearchCriteria.SearchCriteriaSessionKey] = new SearchCriteria {IncidentNumber = IncidentNumberBox.Text};
            Response.Redirect("~/ListCalls.aspx?List=Search");
        }
    }
}