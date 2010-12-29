// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListCallsLoadHandlerTests.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the ListCallsLoadHandlerTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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
    public class ListCallsLoadHandlerTests
    {
        private IRepository _repo;
        private ListCallsLoadHandler _handler;
        private List<Call> _defaultCallList;
        private Person _defaultUser;

        [TestInitialize]
        public void Init()
        {
            _repo = MockRepository.GenerateMock<IRepository>();
            _handler = new ListCallsLoadHandler(_repo);
            _defaultCallList = new List<Call>
            {
                new Call {CallID = Guid.NewGuid()},
                new Call {CallID = Guid.NewGuid()}
            };
            _defaultUser = new Person {ID = Guid.NewGuid()};
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void handler_throws_exception_for_unrecognized_list_type()
        {
            _handler.Handle(new NameValueCollection(), _defaultUser, null);
        }

        [TestMethod]
        public void handler_loads_recent_calls()
        {
            _repo.Expect(r => r.RecentCalls()).Return(_defaultCallList);
            var result = _handler.Handle(BuildQueryStringForType(ListCallsResult.CallListEnum.Recent), _defaultUser, null);
            _repo.VerifyAllExpectations();
            Assert.AreEqual(ListCallsResult.CallListEnum.Recent, result.ListType);
            Assert.AreEqual(_defaultCallList, result.CallList);
        }

        [TestMethod]
        public void handler_loads_outstanding_calls()
        {
            _repo.Expect(r => r.OutstandingCalls()).Return(_defaultCallList);
            var result = _handler.Handle(BuildQueryStringForType(ListCallsResult.CallListEnum.Outstanding), _defaultUser, null);
            _repo.VerifyAllExpectations();
            Assert.AreEqual(ListCallsResult.CallListEnum.Outstanding, result.ListType);
            Assert.AreEqual(_defaultCallList, result.CallList);
        }

        [TestMethod]
        public void handler_loads_my_calls()
        {
            _repo.Expect(r => r.FindCallsForPerson(_defaultUser.ID)).Return(_defaultCallList.Select(c => c.CallID));
            // Not great, but until the query is refactored this is the best we can do without going crazy.
            _repo.Expect(r => r.FindCallByID(Arg<Guid>.Is.Anything)).Return(Call.New()).Repeat.Twice();
            var result = _handler.Handle(BuildQueryStringForType(ListCallsResult.CallListEnum.Mine), _defaultUser, null);
            _repo.VerifyAllExpectations();
            Assert.AreEqual(ListCallsResult.CallListEnum.Mine, result.ListType);
            Assert.AreEqual(_defaultCallList.Count, result.CallList.ToList().Count);
        }

        [TestMethod]
        public void handler_loads_search_results()
        {
            var criteria = new SearchCriteria {IncidentNumber = "123"};

            _repo.Expect(r => r.FindCalls(criteria)).Return(_defaultCallList);
            var result = _handler.Handle(BuildQueryStringForType(ListCallsResult.CallListEnum.Search), _defaultUser, criteria);
            _repo.VerifyAllExpectations();
            Assert.AreEqual(ListCallsResult.CallListEnum.Search, result.ListType);
            Assert.AreEqual(_defaultCallList.Count, result.CallList.ToList().Count);
        }

        private static NameValueCollection BuildQueryStringForType(ListCallsResult.CallListEnum listType)
        {
            return new NameValueCollection {{ListCallsLoadHandler.QueryStringListType, listType.ToString()}};
        }
    }
}
// ReSharper restore InconsistentNaming