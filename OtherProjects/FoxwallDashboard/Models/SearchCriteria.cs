// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchCriteria.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the SearchCriteria type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FoxwallDashboard.Models
{
    public class SearchCriteria
    {
        public const string SearchCriteriaSessionKey = "SearchCriteria";

        public string IncidentNumber { get; set; }
    }
}