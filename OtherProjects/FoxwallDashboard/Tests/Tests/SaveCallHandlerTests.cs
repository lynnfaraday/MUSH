// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SaveCallHandlerTests.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Tests for the save call handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
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
        public void handler_triggers_repository_save()
        {
            var call = Call.New();
            ExpectNoAssociatedPeople();
            ExpectCallToBeSaved(call);
            ExpectChangesToBeCommitted();
            _saveHandler.Save(call, new List<Guid>());
            _repo.VerifyAllExpectations();            
        }
        
        [TestMethod]
        public void handler_triggers_incident_number_assigner()
        {
            var call = Call.New();
            var guid = Guid.NewGuid();
            call.CallID = guid; 
            
            const int incidentNumber = 123;

            // Set it up so that the repo returns our original call so we can use it in other expectations.
            ExpectCallToBeSaved(call);
            ExpectNoAssociatedPeople();
            ExpectIncidentNumberToBeReturned(call, incidentNumber);
            ExpectChangesToBeCommitted();

            _saveHandler.Save(call, new List<Guid>());
            
            _repo.VerifyAllExpectations();
            _incidentAssigner.VerifyAllExpectations();
            Assert.AreEqual(incidentNumber, call.IncidentNumber);
        }
        
        [TestMethod]
        public void handler_removes_old_call_associations()
        {
            var call = Call.New();
            var association1 = new CallPersonAssociation {ID = Guid.NewGuid()};
            var association2 = new CallPersonAssociation {ID = Guid.NewGuid()};
            List<CallPersonAssociation> associationList = new List<CallPersonAssociation>
            {
                association1, association2                
            };

            ExpectCallToBeSaved(call);
            ExpectChangesToBeCommitted();
            _repo.Expect(r => r.FindAssociationsForCall(call.CallID)).Return(associationList);
            _repo.Expect(r => r.DeleteAssociation(association1));
            _repo.Expect(r => r.DeleteAssociation(association2));

            _saveHandler.Save(call, new List<Guid>());

            _repo.VerifyAllExpectations();
        }

        [TestMethod]
        public void handler_adds_call_associations()
        {
            var call = Call.New();
            call.CallID = Guid.NewGuid();
            
            var personID1 = Guid.NewGuid();
            var personID2 = Guid.NewGuid();
            List<Guid> peopleOnCall = new List<Guid> {personID1, personID2};

            ExpectCallToBeSaved(call);
            ExpectChangesToBeCommitted();
            ExpectNoAssociatedPeople();
            _repo.Expect(r => r.SaveAssociation(Arg<CallPersonAssociation>.Matches(
                a => a.PersonID == personID1 && a.CallID == call.CallID && a.IsNew))).Return(CallPersonAssociation.New());
            _repo.Expect(r => r.SaveAssociation(Arg<CallPersonAssociation>.Matches(
                a => a.PersonID == personID2 && a.CallID == call.CallID && a.IsNew))).Return(CallPersonAssociation.New());

            _saveHandler.Save(call, peopleOnCall);

            _repo.VerifyAllExpectations();
        }
        
        private void ExpectCallToBeSaved(Call call)
        {
            _repo.Expect(c => c.SaveCall(call)).Return(call);
        }

        private void ExpectIncidentNumberToBeReturned(Call call, int incidentNumber)
        {
            _incidentAssigner.Expect(a => a.UpdateOrAssignIncidentNumber(call)).Return(incidentNumber);
        }

        private void ExpectNoAssociatedPeople()
        {
            _repo.Expect(r => r.FindAssociationsForCall(Arg<Guid>.Is.Anything)).Return(new List<CallPersonAssociation>());
        }

        private void ExpectChangesToBeCommitted()
        {
            _repo.Expect(r => r.CommitChanges());
        }
    }
}// ReSharper restore InconsistentNaming