// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PasswordValidator.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the PasswordValidator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Text.RegularExpressions;

namespace FoxwallDashboard.Handlers
{
    public class PasswordValidator
    {
        public static bool Validate(string password)
        {
            // Password must be 5 chars long and contain at least 1 letter,  1 number and 1 special character.
            Regex regex = new Regex(@"^.*(?=.{5,})(?=.*\d)(?=.*[a-zA-Z])(?=.*[@#$%^&+=]).*$");
            return regex.IsMatch(password);
        }
    }
}