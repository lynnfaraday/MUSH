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
        private static readonly string NewGuidString = new Guid().ToString();

        [Column(IsPrimaryKey = true)]
        public Guid CallID { get; set; }

        [Column]
        public string Location { get; set; }

        [Column]
        public DateTime Dispatched { get; set; }

        [Column]
        public int Age { get; set; }

        [Column]
        public string Borough { get; set; }

        [Column]
        public string ChiefComplaint { get; set; }

        [Column]
        public string Disposition { get; set; }

        [Column]
        public int? StateNumber { get; set; }

        [Column]
// ReSharper disable InconsistentNaming
        public bool ALS { get; set; }
// ReSharper restore InconsistentNaming

        public bool IsNew
        {
            get
            {
                return CallID.ToString() == NewGuidString;
            }
        }
    }
}