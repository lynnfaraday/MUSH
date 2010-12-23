// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SaveCallHandler.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the SaveCallHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using FoxwallDashboard.Database;
using FoxwallDashboard.Models;

namespace FoxwallDashboard.Handlers
{
    public class SaveCallHandler
    {
        private readonly IRepository _repo;
        private readonly IAssignIncidentNumbers _incidentNumberAssigner;
        static readonly object Padlock = new object();

        public SaveCallHandler(IRepository repo, IAssignIncidentNumbers incidentNumberAssigner)
        {
            _repo = repo;
            _incidentNumberAssigner = incidentNumberAssigner;
        }

        public Call Save(Call call, List<Guid> peopleOnCall)
        {
            // Lock in the unlikely event that two people try to save calls at the same moment.
            lock (Padlock)
            {
                call.IncidentNumber = _incidentNumberAssigner.UpdateOrAssignIncidentNumber(call);
                call = _repo.SaveCall(call);

                // Do this AFTER the call is saved so we ensure we get the right Call ID.
                UpdateCallAssociations(call, peopleOnCall);
                _repo.CommitChanges();
                return call;
            }
        }

        private void UpdateCallAssociations(Call call, IEnumerable<Guid> peopleOnCall)
        {
            // Remove any previous call associations.
            var oldAssociations = _repo.FindAssociationsForCall(call.CallID);
            foreach (var association in oldAssociations)
            {
                _repo.DeleteAssociation(association);
            }

            // Save our call associations.
            foreach (var personID in peopleOnCall)
            {
                var association = CallPersonAssociation.New();
                association.CallID = call.CallID;
                association.PersonID = personID;
                
                _repo.SaveAssociation(association);
            }
        }
    }
}