// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddCall.aspx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the AddCall type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FoxwallDashboard
{
    public partial class AddCall : BasePage
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