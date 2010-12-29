﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditPerson.aspx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the EditPerson type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FoxwallDashboard
{
    public partial class EditPerson : BasePage
    {
        protected override System.Web.UI.WebControls.Button DefaultButton
        {
            get { return PersonControl.DefaultButton; }
        }
        protected override System.Web.UI.Control DefaultFocus
        {
            get { return PersonControl.DefaultFocus; }
        }
    }
}