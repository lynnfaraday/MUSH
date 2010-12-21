// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IncidentNumber.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the IncidentNumber type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Data.Linq.Mapping;
using System.Linq;

namespace FoxwallDashboard
{
    [Table(Name = "IncidentNumbers")]
    public class IncidentNumber
    {
        [Column(IsPrimaryKey = true)]
        public int Year { get; set; }

        [Column]
        public int LastIncident { get; set; }

        static readonly object Padlock = new object();

        // This method assigns an incident number.  Assignment is permanent, so once the incident number
        // is assigned that number will never be used again.
        public static int UpdateOrAssignIncidentNumber(Call call)
        {
            var year = call.Dispatched.Year;
            var oldIncidentNumber = call.IncidentNumber;
            var db = new FoxwallDb();
            IncidentNumber yearEntry;

            // If the year of the call matches the year of the incident number, we're good.  Return.
            if (oldIncidentNumber.ToString().StartsWith(year.ToString()))
            {
                return oldIncidentNumber;
            }

            // Otherwise we'll need a new one, so proceed.
            // Lock in the unlikely event that two people try to save an incident at the same moment.
            lock (Padlock)
            {
                // See if we already have an incident record for this year.
                yearEntry = db.IncidentNumbers.Where(i => i.Year == year).FirstOrDefault();

                // If it doesn't exist, create it.
                if (yearEntry == null)
                {
                    // Multiplying by 10,000 will make the year something like 20110000
                    yearEntry = new IncidentNumber {LastIncident = year * 10000, Year = year};
                    db.IncidentNumbers.InsertOnSubmit(yearEntry);
                }

                // Increment the last entry.
                yearEntry.LastIncident++;

                db.SubmitChanges();
            }
            return yearEntry.LastIncident;
        }
    }
}