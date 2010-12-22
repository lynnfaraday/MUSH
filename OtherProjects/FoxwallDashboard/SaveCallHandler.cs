// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SaveCallHandler.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the SaveCallHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace FoxwallDashboard
{
    public class SaveCallHandler
    {
        private readonly IRepository _repo;
        private readonly IAssignIncidentNumbers _incidentNumberAssigner;

        public SaveCallHandler(IRepository repo, IAssignIncidentNumbers incidentNumberAssigner)
        {
            _repo = repo;
            _incidentNumberAssigner = incidentNumberAssigner;
        }

        public Call SaveCall(Call rawCallData)
        {
            Call call = null;
            // Save a trip to the DB if we know the call is new and isn't in there.
            if (!rawCallData.IsNew)
            {
                call = _repo.FindCallByID(rawCallData.CallID);
            }

            // If we can't find the call, we're going to treat it like a new one even though it might have
            // had an ID.  Could be a previous save attempt failed.
            if (call == null)
            {
                rawCallData.CallID = new Guid();
                call = Call.NewCall();
            }

            call.UpdateFrom(rawCallData);

            call.IncidentNumber = _incidentNumberAssigner.UpdateOrAssignIncidentNumber(call);
            return _repo.SaveCall(call);
        }
    }
}