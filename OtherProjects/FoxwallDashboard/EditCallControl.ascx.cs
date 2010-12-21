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

        private Guid CallID
        {
            get { return (Guid)ViewState["CallID"]; }
            set { ViewState["CallID"] = value;  }
        }

        // Sure would be great to do this in binding, if only I could figure out how :(
        private void UpdateFieldsFromCallData(Call call)
        {
            StateNumberBox.Text = call.StateNumber;
            DispatchedCalendar.SelectedDate = call.Dispatched.Date;
            DispatchTimeBox.Text = call.Dispatched.ToString("HH:mm");
            LocationBox.Text = call.Location;
            BoroughSelection.SelectedValue = call.Borough;
            ChiefComplaintBox.Text = call.ChiefComplaint;
            AgeBox.Text = call.Age.ToString();
            AgeUnitsSelection.SelectedValue = call.AgeUnits;
            DispositionSelection.SelectedValue = call.Disposition;
            ALSCrew.Checked = call.ALS;

            if (call.IsNew)
            {
                IncidentNumberValue.Text = "(Not Saved)";
            }
            else
            {
                IncidentNumberValue.Text = call.IncidentNumber.ToString();
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
                try
                {
                    Call callData;

                    // If we're being asked to load an old call, try to do so.
                    if (Request.QueryString.AllKeys.Contains("CallID"))
                    {
                        CallID = new Guid(Request.QueryString["CallID"]);
                        var db = new FoxwallDb();
                        callData = db.Calls.Where(c => c.CallID == CallID).FirstOrDefault();

                        if (callData == null)
                        {
                            throw new ApplicationException("Could not find call with ID " + CallID + ".");
                        }
                    }

                    // Otherwise assume it's new.
                    else
                    {
                        CallID = new Guid();
                        callData = Call.NewCall();
                    }

                    UpdateFieldsFromCallData(callData);
                }
                catch (Exception ex)
                {
                    ErrorLabel.Text = "Error loading call.  Please try again.<br>If the problem persists, contact support and " +
                                  "mention the following message: " + ex.Message;
                }
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
            try
            {
                Call call;
                if (CallID == new Guid())
                {
                    call = SetupNewCall(db);
                }
                else
                {
                    // Oddly, even though we loaded the call a minute ago we need to RE-load it so that we can update it.
                    // If for some reason this fails, it must mean that we need to treat it like a new call.  Could be
                    // the first attempt to save it failed.
                    call = db.Calls.Where(c => c.CallID == CallID).FirstOrDefault();
                    if (call == null)
                    {
                        call = SetupNewCall(db);
                    }
                }
                UpdateCallDataFromFields(call);
                call.IncidentNumber = IncidentNumber.UpdateOrAssignIncidentNumber(call);

                db.SubmitChanges();
                IncidentNumberValue.Text = call.IncidentNumber.ToString();
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "Ooops, something went wrong saving your call.  Please try again.<br>If the problem persists, contact support and " +
                                  "mention the following message: " + ex.Message;
            }
        }

        // When setting up a new call, we need to put all the data into it and then assign a new ID and
        // Incident Number.  This should only be done immediately before saving, to avoid wasting the
        // incident number.
        private Call SetupNewCall(FoxwallDb db)
        {
            var call = Call.NewCall();
            CallID = call.CallID = Guid.NewGuid();
            db.Calls.InsertOnSubmit(call);
            return call;
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