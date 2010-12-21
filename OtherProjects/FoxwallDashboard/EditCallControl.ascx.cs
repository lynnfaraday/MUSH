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
            Load += HandlePageLoad;
            
        }

        // Have to store this in the session data so that it can be carried across page loads.
        // (could use viewstate but then I have to make it serializable)
        public Call CallData
        {
            get { return Session["CallData"] as Call; }
            set
            {
                Session["CallData"] = value;
                UpdateFieldsFromCallData();
            }
        }

        // Sure would be great to do this in binding, if only I could figure out how :(
        private void UpdateFieldsFromCallData()
        {
            StateNumberBox.Text = CallData.StateNumber;
            DispatchedCalendar.SelectedDate = CallData.Dispatched.Date;
            DispatchTimeBox.Text = CallData.Dispatched.ToString("HH:mm");
            LocationBox.Text = CallData.Location;
            BoroughSelection.SelectedValue = CallData.Borough;
            ChiefComplaintBox.Text = CallData.ChiefComplaint;
            AgeBox.Text = CallData.Age.ToString();
            AgeUnitsSelection.SelectedValue = CallData.AgeUnits;
            DispositionSelection.SelectedValue = CallData.Disposition;
            ALSCrew.Checked = CallData.ALS;

            if (CallData.IsNew)
            {
                IncidentNumberValue.Text = "Incident # be assigned when you save the call.";
            }
        }

        private void UpdateCallDataFromFields(Call call)
        {
            // It's ok to do all this updating blindly because we know that our validation already ran.
            call.StateNumber = StateNumberBox.Text;
            var dateString = DispatchedCalendar.SelectedDate.ToString("MM/dd/yyyy");
            call.Dispatched = DateTime.Parse(dateString + " " + DispatchTimeBox.Text);
            call.Location = LocationBox.Text;
            call.Borough = BoroughSelection.SelectedValue;
            call.ChiefComplaint = ChiefComplaintBox.Text;
            call.Age = int.Parse(AgeBox.Text);
            call.AgeUnits = AgeUnitsSelection.SelectedValue;
            call.Disposition = DispositionSelection.SelectedValue;
            call.ALS = ALSCrew.Checked;
        }

        private void HandlePageLoad(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Assume it's a new call.  If editing an old one, someone will
                // set our CallData later.
                CallData = Call.NewCall();
                UpdateFieldsFromCallData();
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

            // TODO: Try catches and sensible error handling here!
            if (CallData.IsNew)
            {
                // If it's a new call, assign it a GUID and add it.
                CallData.CallID = Guid.NewGuid();
                // TODO: Use incident number table to assign new incident number at this point.
                db.Calls.InsertOnSubmit(CallData);              
            }
            else
            {
                // Oddly, even though we loaded the call a minute ago we need to RE-load it so that we can update it.
                var call = db.Calls.Where(c => c.CallID == CallData.CallID).FirstOrDefault();
                if (call == null)
                {
                    throw new ApplicationException("Wait a minute... something happened to the call I was working on.");
                }
                UpdateCallDataFromFields(call);
            }
            db.SubmitChanges();
            IncidentNumberValue.Text = CallData.IncidentNumber.ToString();

        }

        // Keep them from selecting dates in the future.
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