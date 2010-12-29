// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PreferenceManagerTests.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the PreferenceManagerTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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
    public class PreferenceManagerTests
    {
        private IRepository _repo;
        private PreferenceManager _manager;

        [TestInitialize]
        public void Init()
        {
            _repo = MockRepository.GenerateMock<IRepository>();
            _manager = new PreferenceManager(_repo);
        }

        [TestMethod]
        public void manager_asks_repository_for_preference_value()
        {
            const int Offset = 2;
            _repo.Expect(r => r.FindPreference("ServerToLocalOffsetHours")).Return(
                new Preference {Name = "ServerToLocalOffsetHours", Value = Offset.ToString()});

            var value = _manager.ServerToLocalOffsetHours;

            Assert.AreEqual(Offset, value);
            _repo.VerifyAllExpectations();            
        }

        [TestMethod]
        public void manager_saves_preference_to_repository()
        {
            const int Offset = 2;
            Preference savedPreference = new Preference { Name = "ServerToLocalOffsetHours", Value = Offset.ToString() };

            _repo.Expect(r => r.SavePreference(Arg<Preference>.Matches(
                p => p.IsNew && p.Name == "ServerToLocalOffsetHours" && p.Value == Offset.ToString()
                                                   ))).Return(
                                                       savedPreference);
            _repo.Expect(r => r.CommitChanges());

            _manager.ServerToLocalOffsetHours = Offset;
            
            _repo.VerifyAllExpectations();  
        }
    }
}
// ReSharper restore InconsistentNaming