// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IncidentNumberAssignerTests.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the IncidentNumberAssignerTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using FoxwallDashboard;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

// Disabling per unit test conventions.
// ReSharper disable InconsistentNaming

namespace Tests
{
    [TestClass]
    public class IncidentNumberAssignerTests
    {
        private IncidentNumberAssigner _incidentAssigner;
        private IRepository _repo;
        private Call _call;
        private const int DefaultYear = 2010;
        private const int DefaultIncidentNumber = 20100001;

        [TestInitialize]
        public void Init()
        {
            _repo = MockRepository.GenerateMock<IRepository>();
            _incidentAssigner = new IncidentNumberAssigner(_repo);
            _call = Call.NewCall();
            _call.Dispatched = new DateTime(DefaultYear, 12, 24);
        }

        [TestMethod]
        public void assigner_returns_same_number_if_its_set_and_matches_year()
        {
            _call.IncidentNumber = DefaultIncidentNumber;
            var number = _incidentAssigner.UpdateOrAssignIncidentNumber(_call);
            Assert.AreEqual(_call.IncidentNumber, number);
        }

        [TestMethod]
        public void assigner_will_create_new_year_record_if_one_does_not_exist()
        {
            var newYearRecord = new YearlyIncidentRecord { LastIncident = DefaultIncidentNumber, Year = DefaultYear };

            _repo.Expect(r => r.FindIncidentRecordByYear(DefaultYear)).Return(null);
            _repo.Expect(r => r.SaveIncidentRecord(Arg<YearlyIncidentRecord>.Matches(
                y => y.Year == DefaultYear &&
                y.LastIncident == DefaultIncidentNumber
                ))).Return(newYearRecord);

            var number = _incidentAssigner.UpdateOrAssignIncidentNumber(_call);

            _repo.VerifyAllExpectations();
            Assert.AreEqual(DefaultIncidentNumber, number);
        }

        [TestMethod]
        public void assigner_will_use_next_incident_from_existing_record()
        {
            const int OldIncidentNumber = 2010200;
            var existingRecord = new YearlyIncidentRecord { LastIncident = OldIncidentNumber, Year = DefaultYear };

            _repo.Expect(r => r.FindIncidentRecordByYear(DefaultYear)).Return(existingRecord);

            _repo.Expect(r => r.SaveIncidentRecord(Arg<YearlyIncidentRecord>.Matches(
                y => y.LastIncident == (OldIncidentNumber + 1) &&
                     y.Year == DefaultYear
                ))).Return(existingRecord);

            var number = _incidentAssigner.UpdateOrAssignIncidentNumber(_call);

            _repo.VerifyAllExpectations();
            Assert.AreEqual(existingRecord.LastIncident, number);
        }

        [TestMethod]
        public void assigner_will_give_new_number_if_year_changes()
        {
            _call.Dispatched = new DateTime(2007, 12, 24);
            _call.IncidentNumber = 20100001;

            // We expect that internally the assigner will create a new incident record
            // and then update the last incident number, so we set up our expected result
            // to match.
            var newYearRecord = YearlyIncidentRecord.NewYearlyIncidentRecord(2007);
            newYearRecord.LastIncident++;

            _repo.Expect(r => r.FindIncidentRecordByYear(2007)).Return(null);

            _repo.Expect(r => r.SaveIncidentRecord(Arg<YearlyIncidentRecord>.Matches(
                y => y.LastIncident == newYearRecord.LastIncident &&
                     y.Year == newYearRecord.Year
                ))).Return(newYearRecord);

            var number = _incidentAssigner.UpdateOrAssignIncidentNumber(_call);

            _repo.VerifyAllExpectations();
            Assert.AreEqual(newYearRecord.LastIncident, number);
        }
    }
}
// ReSharper restore InconsistentNaming