// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CallPersonAssociation.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the CallPersonAssociation type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Data.Linq.Mapping;

namespace FoxwallDashboard.Models
{
    [Table(Name = "CallPersonAssociation")]
    public class CallPersonAssociation
    {
        [Column(IsPrimaryKey = true)]
        public Guid ID { get; set; }

        [Column]
        public Guid CallID { get; set; }

        [Column]
        public Guid PersonID { get; set; }

        public bool IsNew
        {
            get { return ID == NewCallPersonAssociationID; }
        }

        public static Guid NewCallPersonAssociationID
        {
            get { return new Guid(); }
        }

        public static CallPersonAssociation New()
        {
            return new CallPersonAssociation
            {
                ID = NewCallPersonAssociationID
            };
        }
    }
}