// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddCall.aspx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the AddCall type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace FoxwallDashboard
{
    public partial class AddCall : System.Web.UI.Page
    {
// ReSharper disable InconsistentNaming
        protected void Page_Load(object sender, EventArgs e)
// ReSharper restore InconsistentNaming
        {
            TitleLabel.Text = Request.QueryString.HasKeys() ? "<h1>Edit a Call</h1>" : "<h1>Add a Call</h1>";
        }
    }
}