// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChangePasswordHandlerTests.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the ChangePasswordHandlerTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using FoxwallDashboard.Database;
using FoxwallDashboard.Handlers;
using FoxwallDashboard.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

// Disabling per unit test conventions.
// ReSharper disable InconsistentNaming

namespace Tests
{
    [TestClass]
    public class ChangePasswordHandlerTests
    {
        private IRepository _repo;
        private ChangePasswordHandler _handler;
        private Person _defaultPerson;
        private const string OldPassword = "t1";
        private const int Salt = 123;

        [TestInitialize]
        public void Init()
        {
            _repo = MockRepository.GenerateMock<IRepository>();
            _handler = new ChangePasswordHandler(_repo);
            Password password = new Password(OldPassword, Salt);
            _defaultPerson = new Person {ID = Guid.NewGuid(), Username = "bob", Salt = Salt, Password = password.ComputeSaltedHash()};
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPasswordException))]
        public void handler_checks_for_invalid_password()
        {
            SetupFindPersonExpectation();
            _handler.Handle(_defaultPerson.ID, "t2", "t2", OldPassword);
            _repo.VerifyAllExpectations();
        }

        [TestMethod]
        [ExpectedException(typeof(PasswordsDontMatchException))]
        public void handler_checks_for_mismatched_passwords()
        {
            SetupFindPersonExpectation();
            _handler.Handle(_defaultPerson.ID, "test2$%", "t2", OldPassword);
            _repo.VerifyAllExpectations();
        }

        [TestMethod]
        public void handler_updates_secure_password()
        {
            const string PasswordString = "test2$%";
            const int Salt = 123;
            _defaultPerson.Salt = Salt;

            SetupFindPersonExpectation();
            _handler.Handle(_defaultPerson.ID, PasswordString, PasswordString, OldPassword);
            Password pw = new Password(PasswordString, Salt);
            Assert.AreEqual(pw.ComputeSaltedHash(), _defaultPerson.Password);
            _repo.AssertWasCalled(r => r.CommitChanges());
            _repo.VerifyAllExpectations();
        }

        [TestMethod]
        [ExpectedException(typeof(PasswordEntryInvalidException))]
        public void handler_checks_for_invalid_old_password()
        {
            SetupFindPersonExpectation();
            _handler.Handle(_defaultPerson.ID, "test2$%", "test2$%", "WRONG");
            _repo.VerifyAllExpectations();
        }

        private void SetupFindPersonExpectation()
        {
            _repo.Expect(r => r.FindPersonByID(_defaultPerson.ID)).Return(_defaultPerson);
        }

    }
}
// ReSharper restore InconsistentNaming