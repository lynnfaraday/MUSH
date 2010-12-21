﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Default.aspx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the DefaultPage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace FoxwallDashboard
{
    public partial class DefaultPage : System.Web.UI.Page
    {
        protected void LoadButtonClick(object sender, EventArgs e)
        {
            FoxwallDb db = new FoxwallDb(); 
            var call = db.Calls.Where(c => c.CallID == new Guid("8E6F6D3C-F63A-430C-9359-BD63AD1EB807")).FirstOrDefault();
            CallControl.CallData = call;
        }
    }

    
}