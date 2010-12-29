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

namespace FoxwallDashboard.Models
{
    [Table(Name = "Calls")]
    public class Call
    {
        [Column(IsPrimaryKey = true)]
        public Guid CallID { get; set; }

        [Column]
        public string Location { get; set; }

        // This is stored in UTC time.
        [Column]
        private DateTime Dispatched { get; set; }
        
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
        
        public bool IsNew
        {
            get { return CallID == NewCallID; }
        }

        public static Guid NewCallID
        {
            get { return new Guid(); }
        }

        public static Call New()
        {
            return new Call
            {
                Age = 0,
                AgeUnits = "years",
                CallID = NewCallID,
                Dispatched = DateTime.UtcNow,
                IncidentNumber = 0                
            };
        }
        
        // Used to access the dispatch time as a local time from the server's perspective.
        private DateTime ServerDispatchedTime
        {
            get { return Dispatched.ToLocalTime(); }
            set { Dispatched = value.ToUniversalTime(); }
        }

        public DateTime GetLocalDispatchedTime(int serverToLocalOffsetHours)
        {
            return ServerDispatchedTime.AddHours(-serverToLocalOffsetHours);
        }

        public void SetLocalDispatchedTime(DateTime localTime, int serverToLocalOffsetHours)
        {
            ServerDispatchedTime = localTime.AddHours(serverToLocalOffsetHours);
        }

    }
}