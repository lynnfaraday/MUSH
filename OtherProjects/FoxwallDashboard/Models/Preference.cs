// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Preference.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the Preference type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Data.Linq.Mapping;

namespace FoxwallDashboard.Models
{
    [Table(Name = "Preferences")]
    public class Preference
    {
        [Column(IsPrimaryKey = true)]
        public Guid ID { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Value { get; set; }

        public bool IsNew
        {
            get { return ID == NewPreferenceID; }
        }

        public static Guid NewPreferenceID
        {
            get { return new Guid(); }
        }

        public static Preference New()
        {
            return new Preference
            {
                ID = NewPreferenceID                
            };
        }       
    }
}