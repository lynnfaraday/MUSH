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

namespace FoxwallDashboard
{
    public class FoxwallDb : DataContext
    {
        private const string ConnectionString = "Data Source=(local);Initial Catalog=test;user=test;password=test;";

        [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Required for SQL binding.")]
        public Table<Call> Calls;

        [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Required for SQL binding.")]
        public Table<IncidentNumber> IncidentNumbers;

        public FoxwallDb() : base(new SqlConnection(ConnectionString))
        {
        }
    }
}