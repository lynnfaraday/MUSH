// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PasswordValidator.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the PasswordValidator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Text.RegularExpressions;

namespace FoxwallDashboard.Handlers
{
    public class PasswordValidator
    {
        public static void Validate(string username, string password, string confirmPassword)
        {
            // Password is never required if username is empty.
            if (string.IsNullOrEmpty(username))
            {
                return;
            }

            // Password must be 5 chars long and contain at least 1 letter,  1 number and 1 special character.
            Regex regex = new Regex(@"^.*(?=.{5,})(?=.*\d)(?=.*[a-zA-Z])(?=.*[@#$%^&+=]).*$");
            if (!regex.IsMatch(password))
            {
                throw new InvalidPasswordException();
            }

            if (password != confirmPassword)
            {
                throw new PasswordsDontMatchException();
            }
        }        
    }

    public class InvalidPasswordException : Exception
    {        
    }

    public class PasswordsDontMatchException : Exception
    {        
    }
}