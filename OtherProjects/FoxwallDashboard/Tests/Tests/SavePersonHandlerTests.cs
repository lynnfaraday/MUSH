// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SavePersonHandlerTests.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the SavePersonHandlerTests type.
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
    public class SavePersonHandlerTests
    {
        private IRepository _repo;
        private SavePersonHandler _handler;

        [TestInitialize]
        public void Init()
        {
            _repo = MockRepository.GenerateMock<IRepository>();
            _handler = new SavePersonHandler(_repo);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateUsernameException))]
        public void handler_will_reject_duplicate_usernames()
        {
            var person = new Person {Username = "test"};
            var existingUser = new Person { ID = Guid.NewGuid() };
            _repo.Expect(r => r.FindPerson(Arg<Func<Person, bool>>.Is.Anything)).Return(existingUser);
            
            _handler.Save(person);
            
            _repo.VerifyAllExpectations();
            _repo.AssertWasNotCalled(r => r.SavePerson(person));
        }

        [TestMethod]
        public void handler_will_allow_saving_existing_user_with_same_username()
        {
            var person = Person.New();
            var guid = Guid.NewGuid();
            person.Username = "test";
            person.ID = guid;

            _repo.Expect(r => r.FindPerson(Arg<Func<Person, bool>>.Is.Anything)).Return(person);
            ExpectPersonToBeSaved(person);
            ExpectChangesToBeCommitted();

            _handler.Save(person);

            _repo.VerifyAllExpectations();
        }

        [TestMethod]
        public void handler_will_save_person_with_no_username()
        {
            var person = Person.New();
            ExpectPersonToBeSaved(person);
            ExpectChangesToBeCommitted();

            _handler.Save(person);
            
            _repo.AssertWasNotCalled(r => r.FindPerson(Arg<Func<Person, bool>>.Is.Anything));
            _repo.VerifyAllExpectations();
        }

        [TestMethod]
        public void handler_will_save_person_with_new_username()
        {
            var person = Person.New();
            person.Username = "test";

            _repo.Expect(r => r.FindPerson(Arg<Func<Person, bool>>.Is.Anything)).Return(null);
            ExpectPersonToBeSaved(person);
            ExpectChangesToBeCommitted();

            _handler.Save(person);

            _repo.VerifyAllExpectations();
        }

        private void ExpectPersonToBeSaved(Person person)
        {
            _repo.Expect(r => r.SavePerson(person)).Return(person);
        }

        private void ExpectChangesToBeCommitted()
        {
            _repo.Expect(r => r.CommitChanges());
        }
    }
}
// ReSharper restore InconsistentNaming