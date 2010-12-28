// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YearlyIncidentRecord.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the YearlyIncidentRecord type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Data.Linq.Mapping;

namespace FoxwallDashboard.Models
{
    [Table(Name = "YearlyIncidents")]
    public class YearlyIncidentRecord
    {
        [Column(IsPrimaryKey = true)]
        public Guid ID { get; set; }

        [Column]
        public int Year { get; set; }

        [Column]
        public int LastIncident { get; set; }

        public static YearlyIncidentRecord New(int year)
        {
            // Multiplying by 10,000 will make the year something like 20110000
            return new YearlyIncidentRecord { ID = NewYearlyIncidentRecordID, LastIncident = year * 10000, Year = year };
        }

        public static Guid NewYearlyIncidentRecordID
        {
            get { return new Guid(); }
        }

        public bool IsNew
        {
            get { return ID == NewYearlyIncidentRecordID; }
        }        
    }
}