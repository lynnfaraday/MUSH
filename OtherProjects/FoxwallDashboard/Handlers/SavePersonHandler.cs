// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SavePersonHandler.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the SavePersonHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using FoxwallDashboard.Database;
using FoxwallDashboard.Models;

namespace FoxwallDashboard.Handlers
{
    public class SavePersonHandler
    {
        private readonly IRepository _repo;

        public SavePersonHandler(IRepository repo)
        {
            _repo = repo;
        }

        public Person Save(Person rawPersonData)
        {
            // Make sure username does not already exist when creating a new person.
            CheckForDuplicateUsername(rawPersonData);

            Person person = null;

            // If updating an existing person, we have to actually query for the object /from/ the database - it's
            // not enough just to have our own call object with the same ID because it's detached somehow from
            // the data store.
            // Save a trip to the DB if we know the call is new and isn't in there.
            if (!rawPersonData.IsNew)
            {
                person = _repo.FindPersonByID(rawPersonData.ID);
            }

            // If we can't find the call, we're going to treat it like a new one even though it might have
            // had an ID.  Could be a previous save attempt failed.
            if (person == null)
            {
                rawPersonData.ID = new Guid();
                person = Person.New();
            }

            person.UpdateFrom(rawPersonData);

            return _repo.SavePerson(person);
        }

        private void CheckForDuplicateUsername(Person rawPersonData)
        {
            if (!string.IsNullOrEmpty(rawPersonData.Username))
            {
                var existingUser = _repo.FindPerson(p => p.Username == rawPersonData.Username);
                if (existingUser != null && existingUser.ID != rawPersonData.ID)
                {
                    throw new DuplicateUsernameException(rawPersonData.Username);
                }
            }
        }
    }

    public class DuplicateUsernameException : Exception
    {
        private readonly string _username;

        public DuplicateUsernameException(string username)
        {
            _username = username;                                 
        }

        public override string Message
        {
            get
            {
                return string.Format("Username {0} is already in use.  Try another one.", _username);
            }
        }
    }
}