// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SaveCallHandler.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the SaveCallHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using FoxwallDashboard.Database;
using FoxwallDashboard.Models;

namespace FoxwallDashboard.Handlers
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

        public Call Save(Call call)
        {
            call.IncidentNumber = _incidentNumberAssigner.UpdateOrAssignIncidentNumber(call);
            return _repo.SaveCall(call);
        }
    }
}