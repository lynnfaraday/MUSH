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

        public Person Save(Person person)
        {
            // Make sure username does not already exist when creating a new person.
            CheckForDuplicateUsername(person);
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