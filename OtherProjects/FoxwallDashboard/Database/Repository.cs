// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Repository.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using FoxwallDashboard.Models;

namespace FoxwallDashboard.Database
{
    public interface IRepository
    {
        Call FindCallByID(Guid id);
        Call FindCall(Func<Call, bool> predicate);
        IEnumerable<Call> FindCalls(Func<Call, bool> predicate);
        Call SaveCall(Call call);
        IEnumerable<Call> RecentCalls();
        IEnumerable<Call> OutstandingCalls();
        
        YearlyIncidentRecord SaveIncidentRecord(YearlyIncidentRecord record);
        YearlyIncidentRecord FindIncidentRecordByYear(int year);
        
        Person FindPersonByID(Guid id);
        Person FindPerson(Func<Person, bool> predicate);
        Person SavePerson(Person person);

        CallPersonAssociation FindAssociationByID(Guid id);
        IEnumerable<CallPersonAssociation> FindAssociationsForCall(Guid callID);
        IEnumerable<Guid> FindPeopleForCall(Guid callID);
        IEnumerable<Guid> FindCallsForPerson(Guid personID);
        CallPersonAssociation SaveAssociation(CallPersonAssociation association);
        IEnumerable<Call> OutstandingCallsForUser(Guid userID);

        // Sorted by lastname.
        IEnumerable<Person> AllPeople();
        void DeleteAssociation(CallPersonAssociation association);
        
        // NOTE: No changes are truly saved to the database until you commit them.
        void CommitChanges();
    }

    public class Repository : IRepository
    {
        readonly FoxwallDb _db = new FoxwallDb();

        public void CommitChanges()
        {
            _db.SubmitChanges();
        }

        #region Calls
        public Call FindCallByID(Guid id)
        {
            return FindCall(c => c.CallID == id);
        }

        public Call FindCall(Func<Call, bool> predicate)
        {
            return _db.Calls.Where(predicate).FirstOrDefault();
        }

        public IEnumerable<Call> FindCalls(Func<Call, bool> predicate)
        {
            return _db.Calls.Where(predicate).ToList().OrderByDescending(c => c.IncidentNumber);            
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
            return call;
        }

        public IEnumerable<Call> RecentCalls()
        {
            return _db.Calls.OrderByDescending(c => c.IncidentNumber).Take(25);
        }

        public IEnumerable<Call> OutstandingCalls()
        {
            // Can't use string.isnullorempty here because SQL chokes
            return _db.Calls.Where(c => c.StateNumber == null || c.StateNumber.Length == 0).ToList().OrderByDescending(c => c.IncidentNumber);
        }

        #endregion

        #region IncidentRecords
        public YearlyIncidentRecord FindIncidentRecordByYear(int year)
        {
            return _db.YearlyIncidents.Where(i => i.Year == year).FirstOrDefault();
        }

        public YearlyIncidentRecord SaveIncidentRecord(YearlyIncidentRecord record)
        {
            if (record.IsNew)
            {
                record.ID = Guid.NewGuid();
                _db.YearlyIncidents.InsertOnSubmit(record);
            }
            return record;
        }
        #endregion

        #region People
        public Person FindPersonByID(Guid id)
        {
            return FindPerson(p => p.ID == id);
        }
        
        public Person FindPerson(Func<Person, bool> predicate)
        {
            return _db.People.Where(predicate).FirstOrDefault();
        }

        public Person SavePerson(Person person)
        {
            // Assign a new GUID if needed and add to the database.
            if (person.IsNew)
            {
                person.ID = Guid.NewGuid();
                _db.People.InsertOnSubmit(person);
            }
            return person;
        }
        
        public IEnumerable<Person> AllPeople()
        {
            return _db.People.OrderBy(p => p.LastName);
        }
        
        #endregion

        #region Associations
        
        public CallPersonAssociation FindAssociationByID(Guid id)
        {
            return _db.CallPersonAssociations.Where(a => a.ID == id).FirstOrDefault();
        }

        public IEnumerable<CallPersonAssociation> FindAssociationsForCall(Guid callID)
        {
            return _db.CallPersonAssociations.Where(a => a.CallID == callID).ToList();
        }

        public IEnumerable<Guid> FindPeopleForCall(Guid callID)
        {
            return FindAssociationsForCall(callID).Select(a => a.PersonID).ToList();
        }

        public IEnumerable<Guid> FindCallsForPerson(Guid personID)
        {
            return _db.CallPersonAssociations.Where(a => a.PersonID == personID).Select(a => a.CallID).ToList();
        }

        public IEnumerable<Call> OutstandingCallsForUser(Guid userID)
        {
            // This is hideously inefficient but I can't figure out the join :(
            // Can't use string.isnullorempty here because SQL chokes
            var outstandingCalls = from c in _db.Calls
                                   where (c.StateNumber == null || c.StateNumber.Length == 0)
                                   select c;
            var personCalls = from a in _db.CallPersonAssociations
                              where a.PersonID == userID
                              select a;
            var finalList = (from c in outstandingCalls
                             where personCalls.Any(a => a.CallID == c.CallID)
                             select c).ToList();

            return finalList;
        }

        public CallPersonAssociation SaveAssociation(CallPersonAssociation association)
        {
            // Assign a new GUID if needed and add to the database.
            if (association.IsNew)
            {
                association.ID = Guid.NewGuid();
                _db.CallPersonAssociations.InsertOnSubmit(association);
            }
            return association;
        }

        public void DeleteAssociation(CallPersonAssociation association)
        {
            _db.CallPersonAssociations.DeleteOnSubmit(association);
        }
        #endregion       
    }
}