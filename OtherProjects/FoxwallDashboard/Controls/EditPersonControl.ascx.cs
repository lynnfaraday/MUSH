// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditPersonControl.ascx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the EditPersonControl type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using FoxwallDashboard.Database;
using FoxwallDashboard.Handlers;
using FoxwallDashboard.Models;

namespace FoxwallDashboard.Controls
{
    public partial class EditPersonControl : System.Web.UI.UserControl
    {
        private readonly Repository _repo;

        public System.Web.UI.WebControls.Button DefaultButton
        {
            get { return SaveButton; }
        }
        public System.Web.UI.Control DefaultFocus
        {
            get { return FirstNameBox; }
        }

        public EditPersonControl()
        {
            Load += HandlePageLoad;
            _repo = new Repository();
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
                var handler = new EditPersonLoadHandler(_repo);
                Person personData = handler.Handle(Request.QueryString);
                
                PersonID = personData.ID;
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
        }

        private void UpdatePersonDataFromFields(Person person)
        {
            person.FirstName = FirstNameBox.Text;
            person.LastName = LastNameBox.Text;
            person.Active = IsActiveBox.Checked;
            person.Username = UsernameBox.Text;

            // Ignore the password box if it's empty, just leave the password what it was.
            if (!string.IsNullOrEmpty(PasswordBox.Text))
            {
                Password password = new Password(PasswordBox.Text, person.Salt);
                person.Password = password.ComputeSaltedHash();
            }
            
        }

        protected void SaveButtonClick(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                ShowNotice("Please correct the errors shown above and try again.", false);
                return;
            }

            var saveHandler = new SavePersonHandler(_repo);
            try
            {

                // This validation can't be done by a validator because ASP forgets the password on postback
                // and we allow it to be blank when editing an existing user.
                if (!string.IsNullOrEmpty(PasswordBox.Text) || PersonID == Person.NewPersonID)
                {
                    bool isPasswordValid = PasswordValidator.Validate(UsernameBox.Text, PasswordBox.Text);
                    PasswordError.Visible = !isPasswordValid;
                    if (!isPasswordValid)
                    {
                        return;
                    }
                }

                // Create or update our person to save.
                var person = _repo.FindPersonByID(PersonID);
                if (person == null)
                {
                    person = Person.New();
                }
                UpdatePersonDataFromFields(person);

                person = saveHandler.Save(person);
                
                // Update our ID now that we have one
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
    }
}