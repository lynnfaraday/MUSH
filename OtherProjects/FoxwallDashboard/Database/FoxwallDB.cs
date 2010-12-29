// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FoxwallDB.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the FoxwallDb type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Data.Linq;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using FoxwallDashboard.Models;

namespace FoxwallDashboard.Database
{
    public class FoxwallDb : DataContext
    {
        [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Required for SQL binding.")]
        public Table<Call> Calls;

        [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Required for SQL binding.")]
        public Table<YearlyIncidentRecord> YearlyIncidents;

        [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Required for SQL binding.")]
        public Table<Person> People;

        [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Required for SQL binding.")]
        public Table<CallPersonAssociation> CallPersonAssociations;

        [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Required for SQL binding.")]
        public Table<Preference> Preferences;

        public FoxwallDb() : base(new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Foxwall"].ConnectionString))
        {
        }
    }
}