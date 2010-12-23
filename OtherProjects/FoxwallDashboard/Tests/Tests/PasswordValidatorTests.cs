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
            Assert.IsTrue(PasswordValidator.Validate(Password));
        }

        [TestMethod]
        public void empty_password_fails_validation()
        {
            const string Password = "";
            Assert.IsFalse(PasswordValidator.Validate(Password));
        }

        [TestMethod]
        public void short_password_fails_validation()
        {
            const string Password = "ab$6";
            Assert.IsFalse(PasswordValidator.Validate(Password));
        }

        [TestMethod]
        public void letters_only_password_fails_validation()
        {
            const string Password = "abcde";
            Assert.IsFalse(PasswordValidator.Validate(Password));
        }

        [TestMethod]
        public void numbers_only_password_fails_validation()
        {
            const string Password = "12345";
            Assert.IsFalse(PasswordValidator.Validate(Password));
        }

        [TestMethod]
        public void no_special_char_password_fails_validation()
        {
            const string Password = "ab123";
            Assert.IsFalse(PasswordValidator.Validate(Password));
        }
    }
}
// ReSharper restore InconsistentNaming