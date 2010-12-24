// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchForCall.aspx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the SearchForCall type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace FoxwallDashboard
{
    public partial class SearchForCall : BasePage
    {      
        protected void SearchButtonClick(object sender, EventArgs e)
        {
            Session["SearchCriteria"] = int.Parse(IncidentNumberBox.Text); // TODO: Make this an object
            Response.Redirect("~/ListCalls.aspx?List=Search");
        }
    }
}