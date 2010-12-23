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
        }

        [TestMethod]
        public void handler_will_allow_saving_existing_user_with_same_username()
        {
            var person = Person.New();
            var guid = Guid.NewGuid();
            person.Username = "test";
            person.ID = guid;

            var existingUser = new Person {ID = guid, Username = "test"};
            _repo.Expect(r => r.FindPerson(Arg<Func<Person, bool>>.Is.Anything)).Return(existingUser);
            _repo.Expect(r => r.SavePerson(Arg<Person>.Is.Anything)).Return(person);

            _handler.Save(person);

            _repo.VerifyAllExpectations();
        }

        [TestMethod]
        public void handler_will_save_person_with_no_username()
        {
            var person = Person.New();
            _repo.Expect(r => r.SavePerson(Arg<Person>.Is.Anything)).Return(person);

            _handler.Save(person);
            
            _repo.AssertWasNotCalled(r => r.FindPerson(Arg<Func<Person, bool>>.Is.Anything));
            _repo.VerifyAllExpectations();
        }

        [TestMethod]
        public void handler_will_save_person_with_username()
        {
            var person = Person.New();
            person.Username = "test";

            _repo.Expect(r => r.FindPerson(Arg<Func<Person, bool>>.Is.Anything)).Return(null);
            _repo.Expect(r => r.SavePerson(Arg<Person>.Is.Anything)).Return(person);

            _handler.Save(person);

            _repo.VerifyAllExpectations();
        }

        [TestMethod]
        public void handler_does_not_query_if_person_is_new()
        {
            var person = Person.New();
            _handler.Save(person);
            _repo.AssertWasNotCalled(r => r.FindPersonByID(new Guid()));
        }

        [TestMethod]
        public void handler_queries_for_existing_person()
        {
            var person = Person.New();
            var guid = Guid.NewGuid();
            person.ID = guid;
            _handler.Save(person);
            _repo.AssertWasCalled(r => r.FindPersonByID(guid));
        }

        [TestMethod]
        public void handler_treats_as_a_new_person_if_it_cannot_find_existing_one()
        {
            var person = Person.New();
            var guid = Guid.NewGuid();
            person.ID = guid;
            _repo.Expect(r => r.FindPersonByID(guid)).Return(null);
            _repo.Expect(r => r.SavePerson(Arg<Person>.Matches(c => c.ID == new Guid()))).Return(person);
            _handler.Save(person);
            _repo.VerifyAllExpectations();
        }

        [TestMethod]
        public void handler_updates_fields_from_raw_person_data_for_existing_person()
        {
            var person = Person.New();
            var guid = Guid.NewGuid();
            person.ID = guid;

            // Not bothering to check EVERY field here, just a sampling.
            person.FirstName = "Bob";
            person.Username = "bsmith";
            person.Active = false;

            var oldPerson = Person.New();
            oldPerson.ID = guid;
            oldPerson.FirstName = "Jane";
            oldPerson.Username = "jdoe";
            oldPerson.Active = true;

            _repo.Expect(r => r.FindPersonByID(guid)).Return(oldPerson);
            _repo.Expect(r => r.SavePerson(Arg<Person>.Matches(p =>
                                                           p.ID == guid &&
                                                           p.FirstName == "Bob" &&
                                                           p.Username == "bsmith" &&
                                                           !p.Active
                                             ))).Return(oldPerson);

            _handler.Save(person);

            _repo.VerifyAllExpectations();
            
        }

        [TestMethod]
        public void handler_updates_fields_from_raw_person_data_for_new_person()
        {
            var person = Person.New();

            // Not bothering to check EVERY field here, just a sampling.
            person.FirstName = "Bob";
            person.Username = "bsmith";
            person.Active = false;

            _repo.Expect(r => r.SavePerson(Arg<Person>.Matches(p =>
                                                           p.FirstName == "Bob" &&
                                                           p.Username == "bsmith" &&
                                                           !p.Active
                                             ))).Return(person);

            _handler.Save(person);

            _repo.VerifyAllExpectations();

        }
    }
}
// ReSharper restore InconsistentNaming