// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PreferenceManager.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the PreferenceManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using FoxwallDashboard.Database;
using FoxwallDashboard.Models;

namespace FoxwallDashboard.Handlers
{
    public class PreferenceManager : IPreferenceManager
    {
        private readonly IRepository _repo;

        public PreferenceManager(IRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// This value is ADDED TO the local time to get the server time.
        /// </summary>
        public int ServerToLocalOffsetHours
        {
            get { return int.Parse(GetPreference("ServerToLocalOffsetHours")); }
            set { SetPreference("ServerToLocalOffsetHours", value.ToString()); }
        }

        private string GetPreference(string name)
        {
            Preference pref = _repo.FindPreference(name);

            if (pref == null)
            {
                throw new ApplicationException("Error loading preferences: Can't find a preference named " + name);
            }
            return pref.Value;
        }

        private void SetPreference(string name, string value)
        {
            Preference pref = _repo.FindPreference(name);
            if (pref == null)
            {
                pref = Preference.New();
            }
            pref.Name = name;
            pref.Value = value;
            _repo.SavePreference(pref);
            _repo.CommitChanges();
        }
    }

    public interface IPreferenceManager
    {
        int ServerToLocalOffsetHours { get; set; }
    }
}