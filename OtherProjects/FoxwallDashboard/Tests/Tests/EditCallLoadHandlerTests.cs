// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditCallLoadHandlerTests.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the EditCallLoadHandlerTests type.
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
    public class EditCallLoadHandlerTests
    {
        private IRepository _repo;
        private EditCallLoadHandler _handler;

        [TestInitialize]
        public void Init()
        {
            _repo = MockRepository.GenerateMock<IRepository>();
            _handler = new EditCallLoadHandler(_repo);
        }

        [TestMethod]
        public void handler_generates_new_call_if_no_query_string()
        {
            var call = _handler.Handle(new NameValueCollection());
            _repo.AssertWasNotCalled(r => r.FindCallByID(Arg<Guid>.Is.Anything));
            Assert.IsTrue(call.IsNew);
        }

        [TestMethod]
        public void handler_loads_call_if_query_string_specified()
        {
            var collection = new NameValueCollection();
            Guid callID = Guid.NewGuid();
            collection.Add(EditCallLoadHandler.QueryStringCallID, callID.ToString());

            _repo.Expect(r => r.FindCallByID(callID)).Return(new Call { CallID = callID});
            var call = _handler.Handle(collection);

            Assert.AreEqual(call.CallID, callID);
            _repo.VerifyAllExpectations();
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void handler_throws_exception_if_call_cannot_be_found()
        {
            var collection = new NameValueCollection();
            Guid callID = Guid.NewGuid();
            collection.Add(EditCallLoadHandler.QueryStringCallID, callID.ToString());

            _repo.Expect(r => r.FindCallByID(callID)).Return(null);
            _handler.Handle(collection);
            // Expect exception at this point.
        }
    }
}
// ReSharper restore InconsistentNaming