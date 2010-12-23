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
        Call SaveCall(Call call);
        YearlyIncidentRecord SaveIncidentRecord(YearlyIncidentRecord record);
        YearlyIncidentRecord FindIncidentRecordByYear(int year);
        Person FindPersonByID(Guid id);
        Person FindPerson(Func<Person, bool> predicate);
        Person SavePerson(Person person);

        // Sorted by lastname.
        SortedList<string, Person> AllPeople();
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
            return _db.YearlyIncidents.Where(i => i.Year == year).FirstOrDefault();
        }

        public YearlyIncidentRecord SaveIncidentRecord(YearlyIncidentRecord record)
        {
            if (record.IsNew)
            {
                record.ID = Guid.NewGuid();
                _db.YearlyIncidents.InsertOnSubmit(record);
            }
            _db.SubmitChanges();
            return record;
        }
        
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
            _db.SubmitChanges();
            return person;
        }

        public SortedList<string, Person> AllPeople()
        {
            var sort = new SortedList<string, Person>();

            foreach (var person in _db.People)
            {
                sort.Add(person.LastName, person);
            }
            return sort;
        }
    }
}