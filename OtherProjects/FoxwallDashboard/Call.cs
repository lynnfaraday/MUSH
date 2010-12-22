// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Call.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the Call type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Data.Linq.Mapping;

namespace FoxwallDashboard
{
    [Table(Name = "Calls")]
    public class Call
    {
        public static Call NewCall()
        {
            return new Call
            {
                Age = 0,
                AgeUnits = "years",
                Borough = "",
                CallID = new Guid(),
                Dispatched = DateTime.Now,
                StateNumber = "",
                IncidentNumber = 0
            };
        }

        public void UpdateFrom(Call otherCall)
        {
            CallID = otherCall.CallID;
            Location = otherCall.Location;
            Dispatched = otherCall.Dispatched;
            Age = otherCall.Age;
            AgeUnits = otherCall.AgeUnits;
            Borough = otherCall.Borough;
            ChiefComplaint = otherCall.ChiefComplaint;
            Disposition = otherCall.Disposition;
            StateNumber = otherCall.StateNumber;
            IncidentNumber = otherCall.IncidentNumber;
            ALS = otherCall.ALS;
        }

        [Column(IsPrimaryKey = true)]
        public Guid CallID { get; set; }

        [Column]
        public string Location { get; set; }

        [Column]
        public DateTime Dispatched { get; set; }

        [Column]
        public int Age { get; set; }

        [Column]
        public string AgeUnits { get; set; }

        [Column]
        public string Borough { get; set; }

        [Column]
        public string ChiefComplaint { get; set; }

        [Column]
        public string Disposition { get; set; }

        [Column]
        public string StateNumber { get; set; }

        [Column]
        public int IncidentNumber { get; set; }

        [Column]
// ReSharper disable InconsistentNaming
        public bool ALS { get; set; }
// ReSharper restore InconsistentNaming

        public bool IsNew
        {
            get
            {
                return CallID == new Guid();
            }
        }
    }
}