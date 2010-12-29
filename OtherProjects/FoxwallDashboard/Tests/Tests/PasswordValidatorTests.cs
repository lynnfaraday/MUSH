// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PasswordValidatorTests.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the PasswordValidatorTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using FoxwallDashboard.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// Disabling per unit test conventions.
// ReSharper disable InconsistentNaming

namespace Tests
{
    [TestClass]
    public class PasswordValidatorTests
    {       
        [TestMethod]
        public void good_password_passes_validation()
        {
            const string Password = "ab$d6";
            PasswordValidator.Validate("test", Password, Password);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPasswordException))]
        public void empty_password_fails_validation()
        {
            const string Password = "";
            PasswordValidator.Validate("test", Password, Password);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPasswordException))]
        public void short_password_fails_validation()
        {
            const string Password = "ab$6";
            PasswordValidator.Validate("test", Password, Password);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPasswordException))]
        public void letters_only_password_fails_validation()
        {
            const string Password = "abcde";
            PasswordValidator.Validate("test", Password, Password);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPasswordException))]
        public void numbers_only_password_fails_validation()
        {
            const string Password = "12345";
            PasswordValidator.Validate("test", Password, Password);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPasswordException))]
        public void no_special_char_password_fails_validation()
        {
            const string Password = "ab123";
            PasswordValidator.Validate("test", Password, Password);
        }

        [TestMethod]
        public void password_ignored_when_username_is_blank()
        {
            const string Password = "1";
            PasswordValidator.Validate(string.Empty, Password, Password);
        }

        [TestMethod]
        [ExpectedException(typeof(PasswordsDontMatchException))]
        public void validation_fails_when_passwords_dont_match()
        {
            const string Password = "test3$";
            PasswordValidator.Validate("test", Password, "xyz");
        }
    }
}
// ReSharper restore InconsistentNaming