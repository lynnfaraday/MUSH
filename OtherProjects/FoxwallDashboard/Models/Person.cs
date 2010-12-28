// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Person.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the Crew type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Data.Linq.Mapping;

namespace FoxwallDashboard.Models
{
    [Table(Name = "People")]
    public class Person
    {
        [Column(IsPrimaryKey = true)]
        public Guid ID { get; set; }

        [Column]
        public string FirstName { get; set; }

        [Column]
        public string LastName { get; set; }

        [Column]
        public string Username { get; set; }

        [Column]
        public string Password { get; set; }

        [Column]
        public bool Administrator { get; set; }

        [Column]
        public int Salt { get; set; }

        [Column]
        public bool Active { get; set; }
        
        public bool IsNew
        {
            get { return ID == NewPersonID; }
        }

        public static Guid NewPersonID
        {
            get { return new Guid(); }
        }
        public string DisplayName
        {
            get { return LastName + ", " + FirstName; }
        }

        public static Person New()
        {
            return new Person
            {
                ID = NewPersonID,
                Active = true,
                Salt = Models.Password.CreateRandomSalt()
            };
        }

        public void UpdateFrom(Person otherPerson)
        {
            FirstName = otherPerson.FirstName;
            LastName = otherPerson.LastName;
            Active = otherPerson.Active;
            Username = otherPerson.Username;
            Password = otherPerson.Password;
            Administrator = otherPerson.Administrator;
        }
    }
}