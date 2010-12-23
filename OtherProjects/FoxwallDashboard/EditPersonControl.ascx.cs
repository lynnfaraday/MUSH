// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditPersonControl.ascx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the EditPersonControl type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Web.UI.WebControls;
using FoxwallDashboard.Database;
using FoxwallDashboard.Handlers;
using FoxwallDashboard.Models;

namespace FoxwallDashboard
{
    public partial class EditPersonControl : System.Web.UI.UserControl
    {
        public EditPersonControl()
        {
            Load += HandlePageLoad;
        }

        private Guid PersonID
        {
            get { return (Guid)ViewState["PersonID"]; }
            set { ViewState["PersonID"] = value; }
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
                Person personData;

                // If we're being asked to load an old call, try to do so.
                if (Request.QueryString.AllKeys.Contains("PersonID"))
                {
                    PersonID = new Guid(Request.QueryString["PersonID"]);
                    var repo = new Repository();
                    personData = repo.FindPersonByID(PersonID);

                    if (personData == null)
                    {
                        throw new ApplicationException("Could not find person with ID " + PersonID + ".");
                    }
                }

                // Otherwise it's new.
                else
                {
                    PersonID = new Guid();
                    personData = Person.New();
                }

                UpdateFieldsFromPersonData(personData);

                HideNotice();
            }
            catch (Exception ex)
            {
                ShowNotice("Error loading call.  Please try again.<br>If the problem persists, contact support and " +
                          "mention the following message: " + ex.Message, true);
            }
        }

        private void UpdateFieldsFromPersonData(Person person)
        {
            FirstNameBox.Text = person.FirstName;
            LastNameBox.Text = person.LastName;
            IsActiveBox.Checked = person.Active;
            UsernameBox.Text = person.Username;
            // As a security feature, ASP tries to prevent you from directly setting the 'Text' property on a 
            // password box.  To set it, you have to use the 'value' attribute.  We really want to set it here,
            // because otherwise the person editing the user would have to re-enter their password every time
            // they edited them!
            PasswordBox.Attributes.Add("value", person.Password); // TODO: Dehash

        }

        private void UpdatePersonDataFromFields(Person person)
        {
            person.FirstName = FirstNameBox.Text;
            person.LastName = LastNameBox.Text;
            person.Active = IsActiveBox.Checked;
            person.Username = UsernameBox.Text;
            person.Password = PasswordBox.Text;  // TODO: Hash
        }

        protected void SaveButtonClick(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                ShowNotice("Please correct the errors shown above and try again.", false);
                return;
            }

            var repo = new Repository();
            var saveHandler = new SavePersonHandler(repo);
            try
            {
                // Set up a temporary call containing our call data from the GUI.
                var person = Person.New();
                person.ID = PersonID;
                UpdatePersonDataFromFields(person);

                person = saveHandler.Save(person);
                PersonID = person.ID;

                ShowNotice("Person saved!", false);
            }
            // This is an expected exception if they enter a username that already exists, so don't throw up the "something bad happened"
            // error message.
            catch (DuplicateUsernameException ex)
            {
                ShowNotice(ex.Message, false);
            }
            catch (Exception ex)
            {
                ShowNotice("Ooops, something went wrong saving your person.  Please try again.<br>If the problem persists, contact support and " +
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

        protected void ValidatePassword(object source, ServerValidateEventArgs args)
        {
            // Password is only required at all if a username is set.
            if (string.IsNullOrEmpty(UsernameBox.Text))
            {
                args.IsValid = true;
                return;
            }

            args.IsValid = PasswordValidator.Validate(PasswordBox.Text);
        }
    }
}