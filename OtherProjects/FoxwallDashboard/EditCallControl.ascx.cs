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

            IncidentNumberValue.Text = call.IsNew ? "(Not Saved)" : call.IncidentNumber.ToString();
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

            int incidentNumber;
            call.IncidentNumber = int.TryParse(IncidentNumberValue.Text, out incidentNumber) ? incidentNumber : 0;
        }

        private void HandlePageLoad(object sender, EventArgs e)
        {
            // Ignore postbacks.
            if (Page.IsPostBack)
            {
                return;
            }

            try
            {
                Call callData;

                // If we're being asked to load an old call, try to do so.
                if (Request.QueryString.AllKeys.Contains("CallID"))
                {
                    CallID = new Guid(Request.QueryString["CallID"]);
                    var repo = new Repository();
                    callData = repo.FindCallByID(CallID);

                    if (callData == null)
                    {
                        throw new ApplicationException("Could not find call with ID " + CallID + ".");
                    }
                }

                // Otherwise it's new.
                else
                {
                    CallID = new Guid();
                    callData = Call.NewCall();
                }

                UpdateFieldsFromCallData(callData);

                HideNotice();
            }
            catch (Exception ex)
            {
                ShowNotice("Error loading call.  Please try again.<br>If the problem persists, contact support and " +
                          "mention the following message: " + ex.Message, true);
            }
        }

        protected void SaveButtonClick(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                ShowNotice("Please correct the errors shown above and try again.", false);
                return;
            }

            var repo = new Repository();
            var incidentNumberAssigner = new IncidentNumberAssigner(repo);
            var saveHandler = new SaveCallHandler(repo, incidentNumberAssigner);
            try
            {
                // Set up a temporary call containing our call data from the GUI.
                var call = Call.NewCall();
                call.CallID = CallID;
                UpdateCallDataFromFields(call);

                call = saveHandler.SaveCall(call);
                CallID = call.CallID;
                IncidentNumberValue.Text = call.IncidentNumber.ToString();

                ShowNotice("Call saved!", false);
            }
            catch (Exception ex)
            {
                ShowNotice("Ooops, something went wrong saving your call.  Please try again.<br>If the problem persists, contact support and " +
                          "mention the following message: " + ex.Message, false);
            }
        }

        private void ShowNotice(string message, bool hideContent)
        {
            NoticeLabel.Text = message;
            ContentPanel.Visible = !hideContent;
        }

        private void HideNotice()
        {
            ContentPanel.Visible = true;
            NoticeLabel.Text = string.Empty;
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