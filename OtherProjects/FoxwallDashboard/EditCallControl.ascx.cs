// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditCallControl.ascx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the EditCallControl type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace FoxwallDashboard
{
    public partial class EditCallControl : System.Web.UI.UserControl
    {
        public EditCallControl()
        {
            // Assume it's a new call.  If editing an old one, someone will
            // set our CallData later.
            CallData = new Call
            {
                CallID = new Guid(),
                Dispatched = DateTime.Today
            };

            Load += HandlePageLoad;
        }

        private Call _callData;
        public Call CallData
        {
            get { return _callData; }
            set
            {
                _callData = value;
               // DataBind();
            }
        }

        private void HandlePageLoad(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               // DataBind();
            }
        }

        protected void SaveButtonClick(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                ErrorLabel.Text = "Please correct the errors shown above and try again.";
                return;
            }

            FoxwallDb db = new FoxwallDb();

           // if (CallData.IsNew)
            {
                db.Calls.InsertOnSubmit(CallData);              
            }
            db.SubmitChanges();

        }

        protected void RenderCalendar(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date > DateTime.Now)
            {
                e.Cell.BackColor = System.Drawing.Color.LightGray;
                e.Day.IsSelectable = false;
                e.Cell.ToolTip = "Cannot select days in the future.";
            }
        }
    }
}