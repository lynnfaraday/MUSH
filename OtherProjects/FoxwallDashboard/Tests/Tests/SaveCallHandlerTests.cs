// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SaveCallHandlerTests.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Tests for the save call handler.
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
            _repo.Expect(c => c.SaveCall(call)).Return(call);
            _saveHandler.Save(call);
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
            _repo.Expect(r => r.SaveCall(call)).Return(call);
            _incidentAssigner.Expect(a => a.UpdateOrAssignIncidentNumber(call)).Return(incidentNumber);

            _saveHandler.Save(call);
            
            _repo.VerifyAllExpectations();
            _incidentAssigner.VerifyAllExpectations();
            Assert.AreEqual(incidentNumber, call.IncidentNumber);
        }

    }
}// ReSharper restore InconsistentNaming