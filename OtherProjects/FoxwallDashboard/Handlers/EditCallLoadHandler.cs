// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditCallLoadHandler.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the EditCallLoadHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Specialized;
using System.Linq;
using FoxwallDashboard.Database;
using FoxwallDashboard.Models;

namespace FoxwallDashboard.Handlers
{
    public class EditCallLoadHandler
    {
        public const string QueryStringCallID = "CallID";
        private readonly IRepository _repo;

        public EditCallLoadHandler(IRepository repo)
        {
            _repo = repo;
        }

        public Call Handle(NameValueCollection queryString)
        {
            Call callData = Call.New();

            // If we're being asked to load an old call, try to do so.
            if (queryString.AllKeys.Contains(QueryStringCallID))
            {
                var callToLoad = new Guid(queryString[QueryStringCallID]);
                callData = _repo.FindCallByID(callToLoad);

                if (callData == null)
                {
                    throw new ApplicationException("Could not find call with ID " + callToLoad + ".");
                }
            }

            return callData;
        }
    }
}