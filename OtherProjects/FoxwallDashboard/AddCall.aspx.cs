﻿// --------------------------------------------------------------------------------------------------------------------
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
        protected override void DoCustomPageLoad()
        {
            TitleLabel.Text = Request.QueryString.HasKeys() ? "<h1>Edit a Call</h1>" : "<h1>Add a Call</h1>";
        }
    }
}