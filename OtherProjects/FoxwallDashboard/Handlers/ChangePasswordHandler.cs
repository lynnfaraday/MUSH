// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChangePasswordHandler.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the ChangePasswordHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using FoxwallDashboard.Database;
using FoxwallDashboard.Models;

namespace FoxwallDashboard.Handlers
{
    public class ChangePasswordHandler
    {
        private readonly IRepository _repo;

        public ChangePasswordHandler(IRepository repo)
        {
            _repo = repo;
        }

        public void Handle(Guid personID, string newPassword, string confirmPassword, string oldPassword)
        {
            var person = _repo.FindPersonByID(personID);
            PasswordValidator.Validate(person.Username, newPassword, confirmPassword);

            Password securePassword = new Password(oldPassword, person.Salt);
            if (securePassword.ComputeSaltedHash() != person.Password)
            {
                throw new PasswordEntryInvalidException();
            }
            person.SaveSecuredPassword(newPassword);
            _repo.CommitChanges();
        }
    }

    public class PasswordEntryInvalidException : Exception {}
}