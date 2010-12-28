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
using FoxwallDashboard.Database;
using FoxwallDashboard.Handlers;
using FoxwallDashboard.Models;

namespace FoxwallDashboard.Controls
{
    public partial class EditCallControl : System.Web.UI.UserControl
    {
        private readonly Repository _repo;

        public Button DefaultButton
        {
            get { return SaveButton; }
        }

        public System.Web.UI.Control DefaultFocus
        {
            get { return StateNumberBox; }
        }

        public EditCallControl()
        {
            Load += HandlePageLoad;
            CallID = Call.NewCallID;
            _repo = new Repository();
        }

        // This stores the call ID we're working with, whether it's a new one or came out of a query string.
        // Needs to be in view state to survive postbacks.
        private Guid CallID
        {
            get { return (Guid)ViewState["CallID"]; }
            set { ViewState["CallID"] = value;  }
        }

        // Sure would be great to do this in binding, if only I could figure out how :(
        private void UpdateFieldsFromCallData(Call call)
        {
            CallID = call.CallID;
            StateNumberBox.Text = call.StateNumber;
            DispatchedCalendar.SelectedDate = call.Dispatched.Date;
            DispatchTimeBox.Text = call.Dispatched.ToString("HH:mm");
            LocationBox.Text = call.Location;
            BoroughSelection.SelectedValue = call.Borough;
            ChiefComplaintBox.Text = call.ChiefComplaint;
            AgeBox.Text = call.Age.ToString();
            AgeUnitsSelection.SelectedValue = call.AgeUnits;
            DispositionSelection.SelectedValue = call.Disposition;
            
            IncidentNumberValue.Text = call.IsNew ? "(Not Saved)" : call.IncidentNumber.ToString();

            var peopleOnCall = _repo.FindPeopleForCall(call.CallID);

            CrewList.Items.Clear();
            foreach (var person in _repo.AllPeople())
            {
                bool isOnCall = peopleOnCall.Contains(person.ID);
                // Only add if they're active or on the call (i.e. it's an old call).
                if (person.Active || isOnCall)
                {
                    var item = new ListItem
                    {
                        Text = person.DisplayName,
                        Value = person.ID.ToString(),
                        Selected = isOnCall,
                        Enabled = person.Active
                    };
                    CrewList.Items.Add(item);
                }
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
            
            int incidentNumber;
            call.IncidentNumber = int.TryParse(IncidentNumberValue.Text, out incidentNumber) ? incidentNumber : 0;
        }

        private void HandlePageLoad(object sender, EventArgs e)
        {
            try
            {
                // Ignore postbacks
                if (Page.IsPostBack)
                {
                    return;
                }

                Call callData = Call.New();

                // If we're being asked to load an old call, try to do so.
                if (Request.QueryString.AllKeys.Contains("CallID"))
                {
                    CallID = new Guid(Request.QueryString["CallID"]);
                    callData = _repo.FindCallByID(CallID);

                    if (callData == null)
                    {
                        throw new ApplicationException("Could not find call with ID " + CallID + ".");
                    }
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

            var incidentNumberAssigner = new IncidentNumberAssigner(_repo);
            var saveHandler = new SaveCallHandler(_repo, incidentNumberAssigner);
            try
            {
                var peopleOnCall = (from ListItem item in CrewList.Items
                                    where item.Selected
                                    select new Guid(item.Value)).ToList();

                // Create or update our call to save.
                var call = _repo.FindCallByID(CallID);
                if (call == null)
                {
                    call = Call.New();
                }
                UpdateCallDataFromFields(call);

                call = saveHandler.Save(call, peopleOnCall);

                // Update our call ID and incident number, which may have been set by the save handler.
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

        protected void VerifyCrewPresent(object source, ServerValidateEventArgs args)
        {
            args.IsValid = CrewList.Items.Cast<ListItem>().Any(item => item.Selected);
        }
    }
}