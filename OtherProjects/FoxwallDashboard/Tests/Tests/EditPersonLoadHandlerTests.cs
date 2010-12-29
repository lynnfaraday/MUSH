// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditPersonLoadHandlerTests.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the EditPersonLoadHandlerTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Specialized;
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
    public class EditPersonLoadHandlerTests
    {
        private IRepository _repo;
        private EditPersonLoadHandler _handler;

        [TestInitialize]
        public void Init()
        {
            _repo = MockRepository.GenerateMock<IRepository>();
            _handler = new EditPersonLoadHandler(_repo);
        }

        [TestMethod]
        public void handler_generates_new_person_if_no_query_string()
        {
            var person = _handler.Handle(new NameValueCollection());
            _repo.AssertWasNotCalled(r => r.FindPersonByID(Arg<Guid>.Is.Anything));
            Assert.IsTrue(person.IsNew);
        }

        [TestMethod]
        public void handler_loads_person_if_query_string_specified()
        {
            var collection = new NameValueCollection();
            Guid personID = Guid.NewGuid();
            collection.Add(EditPersonLoadHandler.QueryStringPersonID, personID.ToString());

            _repo.Expect(r => r.FindPersonByID(personID)).Return(new Person { ID = personID });
            var person = _handler.Handle(collection);

            Assert.AreEqual(person.ID, personID);
            _repo.VerifyAllExpectations();
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void handler_throws_exception_if_person_cannot_be_found()
        {
            var collection = new NameValueCollection();
            Guid personID = Guid.NewGuid();
            collection.Add(EditPersonLoadHandler.QueryStringPersonID, personID.ToString());

            _repo.Expect(r => r.FindPersonByID(personID)).Return(null);
            _handler.Handle(collection);
            // Expect exception at this point.
        }
    }
}
// ReSharper restore InconsistentNaming