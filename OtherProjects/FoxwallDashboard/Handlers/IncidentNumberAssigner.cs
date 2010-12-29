// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IncidentNumberAssigner.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the IncidentNumberAssigner type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using FoxwallDashboard.Database;
using FoxwallDashboard.Models;

namespace FoxwallDashboard.Handlers
{
    public interface IAssignIncidentNumbers
    {
        int UpdateOrAssignIncidentNumber(Call call);
    }

    public class IncidentNumberAssigner : IAssignIncidentNumbers
    {
        private readonly IRepository _repo;
       
        public IncidentNumberAssigner(IRepository repo)
        {
            _repo = repo;
        }

        // This method assigns an incident number.  NOTE: This does NOT commit changes to the database; it is assumed
        // that whoever's requesting the incident number assignment will do so.
        public int UpdateOrAssignIncidentNumber(Call call)
        {
            var year = call.LocalDispatchedTime.Year;
            var oldIncidentNumber = call.IncidentNumber;
            YearlyIncidentRecord record;

            // If the year of the call matches the year of the incident number, we're good.  Return.
            if (oldIncidentNumber.ToString().StartsWith(year.ToString()))
            {
                return oldIncidentNumber;
            }

            // Otherwise we'll need a new one, so proceed.
            // See if we already have an incident record for this year.

            record = _repo.FindIncidentRecordByYear(year);

            // If it doesn't exist, create it.
            if (record == null)
            {
                record = YearlyIncidentRecord.New(year);
            }

            // Increment the last entry.
            record.LastIncident++;

            record = _repo.SaveIncidentRecord(record);

            return record.LastIncident;
        }
    }
}