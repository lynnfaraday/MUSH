// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Repository.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace FoxwallDashboard
{
    public interface IRepository
    {
        Call FindCallByID(Guid id);
        Call FindCall(Func<Call, bool> predicate);
        Call SaveCall(Call call);
        YearlyIncidentRecord SaveIncidentRecord(YearlyIncidentRecord record);
        YearlyIncidentRecord FindIncidentRecordByYear(int year);
    }

    public class Repository : IRepository
    {
        readonly FoxwallDb _db = new FoxwallDb();

        public Call FindCallByID(Guid id)
        {
            return FindCall(c => c.CallID == id);
        }

        public Call FindCall(Func<Call, bool> predicate)
        {
            return _db.Calls.Where(predicate).FirstOrDefault();
        }

        // Will add a new call to the database if needed.
        public Call SaveCall(Call call)
        {
            // Assign a new GUID if needed and add to the database.
            if (call.IsNew)
            {
                call.CallID = Guid.NewGuid();
                _db.Calls.InsertOnSubmit(call);
            }           
            _db.SubmitChanges();
            return call;
        }

        public YearlyIncidentRecord FindIncidentRecordByYear(int year)
        {
            return _db.IncidentNumbers.Where(i => i.Year == year).FirstOrDefault();
        }

        public YearlyIncidentRecord SaveIncidentRecord(YearlyIncidentRecord record)
        {
            if (record.IsNew)
            {
                record.ID = Guid.NewGuid();
                _db.IncidentNumbers.InsertOnSubmit(record);
            }
            _db.SubmitChanges();
            return record;
        }
    }
}