// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SaveCallHandlerTests.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Tests for the save call handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using FoxwallDashboard;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

// Disabling per unit test conventions.
// ReSharper disable InconsistentNaming

namespace Tests
{
    [TestClass]
    public class SaveCallHandlerTests
    {
        private IAssignIncidentNumbers _incidentAssigner;
        private IRepository _repo;
        private SaveCallHandler _saveHandler;

        [TestInitialize]
        public void Init()
        {
            _repo = MockRepository.GenerateMock<IRepository>();
            _incidentAssigner = MockRepository.GenerateMock<IAssignIncidentNumbers>();
            _saveHandler = new SaveCallHandler(_repo, _incidentAssigner);
        }

        [TestMethod]
        public void handler_does_not_query_if_call_is_new()
        {
            var call = Call.NewCall();
            _saveHandler.SaveCall(call);
            _repo.AssertWasNotCalled(r => r.FindCallByID(new Guid()));
        }

        [TestMethod]
        public void handler_queries_for_existing_call()
        {
            var call = Call.NewCall();
            var guid = Guid.NewGuid();
            call.CallID = guid;
            _saveHandler.SaveCall(call);
            _repo.AssertWasCalled(r => r.FindCallByID(guid));
        }

        [TestMethod]
        public void handler_treats_as_new_call_if_it_cannot_find_existing_one()
        {
            var call = Call.NewCall();
            var guid = Guid.NewGuid();
            call.CallID = guid;
            _repo.Expect(r => r.FindCallByID(guid)).Return(null);
            _repo.Expect(r => r.SaveCall(Arg<Call>.Matches(c => c.CallID == new Guid()))).Return(call);
            _saveHandler.SaveCall(call);
            _repo.VerifyAllExpectations();
        }

        [TestMethod]
        public void handler_triggers_incident_number_assigner()
        {
            var call = Call.NewCall();
            var guid = Guid.NewGuid();
            call.CallID = guid; 
            
            const int incidentNumber = 123;

            // Set it up so that the repo returns our original call so we can use it in other expectations.
            _repo.Expect(r => r.FindCallByID(guid)).Return(call);
            _repo.Expect(r => r.SaveCall(call)).Return(call);
            _incidentAssigner.Expect(a => a.UpdateOrAssignIncidentNumber(call)).Return(incidentNumber);

            _saveHandler.SaveCall(call);
            
            _repo.VerifyAllExpectations();
            _incidentAssigner.VerifyAllExpectations();
            Assert.AreEqual(incidentNumber, call.IncidentNumber);
        }

        [TestMethod]
        public void handler_updates_fields_from_raw_call_data_for_existing_call()
        {
            var call = Call.NewCall();
            var guid = Guid.NewGuid();
            call.CallID = guid;

            // Not bothering to check EVERY field here, just a sampling.
            call.ALS = true;
            call.ChiefComplaint = "My complaint.";
            call.Disposition = "Standby";

            var oldCall = Call.NewCall();
            oldCall.CallID = guid;
            oldCall.ALS = false;
            oldCall.ChiefComplaint = "X";
            oldCall.Disposition = "Presby";

            _repo.Expect(r => r.FindCallByID(guid)).Return(oldCall);
            _repo.Expect(r => r.SaveCall(Arg<Call>.Matches(c =>
                                                           c.CallID == guid &&
                                                           c.ALS  &&
                                                           c.ChiefComplaint == "My complaint." &&
                                                           c.Disposition == "Standby"
                                             ))).Return(oldCall);
            _incidentAssigner.Expect(a => a.UpdateOrAssignIncidentNumber(oldCall)).Return(123);

            _saveHandler.SaveCall(call);

            _repo.VerifyAllExpectations();
            _incidentAssigner.VerifyAllExpectations();            
        }

        [TestMethod]
        public void handler_updates_fields_from_raw_call_data_for_new_call()
        {
            var call = Call.NewCall();

            // Not bothering to check EVERY field here, just a sampling.
            call.ALS = true;
            call.ChiefComplaint = "My complaint.";
            call.Disposition = "Standby";

            _repo.Expect(r => r.SaveCall(Arg<Call>.Matches(c =>
                                                           c.CallID == new Guid() &&
                                                           c.ALS &&
                                                           c.ChiefComplaint == "My complaint." &&
                                                           c.Disposition == "Standby"
                                             ))).Return(call);
            _incidentAssigner.Expect(a => a.UpdateOrAssignIncidentNumber(Arg<Call>.Is.Anything)).Return(123);

            _saveHandler.SaveCall(call);

            _repo.VerifyAllExpectations();
            _incidentAssigner.VerifyAllExpectations();


        }
    }
}// ReSharper restore InconsistentNaming