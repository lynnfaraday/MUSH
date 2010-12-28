﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditCall.aspx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the EditCall type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FoxwallDashboard
{
    public partial class EditCall : BasePage
    {
        protected override System.Web.UI.WebControls.Button DefaultButton
        {
            get { return CallControl.DefaultButton; }
        }
        protected override System.Web.UI.Control DefaultFocus
        {
            get { return CallControl.DefaultFocus; }
        }
    }
}