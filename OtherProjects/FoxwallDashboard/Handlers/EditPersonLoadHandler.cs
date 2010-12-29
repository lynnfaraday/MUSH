// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditPersonLoadHandler.cs" company="Wordsmyth Games">
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
    public class EditPersonLoadHandler
    {
        public const string QueryStringPersonID = "PersonID";
        private readonly IRepository _repo;

        public EditPersonLoadHandler(IRepository repo)
        {
            _repo = repo;
        }

        public Person Handle(NameValueCollection queryString)
        {
            Person personData = Person.New();

            // If we're being asked to load an old call, try to do so.
            if (queryString.AllKeys.Contains(QueryStringPersonID))
            {
                var personToLoad = new Guid(queryString[QueryStringPersonID]);
                personData = _repo.FindPersonByID(personToLoad);

                if (personData == null)
                {
                    throw new ApplicationException("Could not find person with ID " + personToLoad + ".");
                }
            }

            return personData;
        }
    }
}