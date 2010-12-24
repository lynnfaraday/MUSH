// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasePage.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the BasePage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Web.UI.WebControls;
using FoxwallDashboard.Database;
using FoxwallDashboard.Models;

namespace FoxwallDashboard
{
    public class BasePage : System.Web.UI.Page
    {
        protected Panel NotLoggedInPanel
        {
            get { return Master == null ? null : Master.FindControl("NotLoggedInPanel") as Panel; }
        }

        protected ContentPlaceHolder MainContent
        {
            get { return Master == null ? null : Master.FindControl("MainContent") as ContentPlaceHolder; }
        }

        protected LinkButton LogoutButton
        {
            get { return Master == null ? null : Master.FindControl("LogoutButton") as LinkButton; }
        }

        protected Label LoginNameDisplay
        {
            get { return Master == null ? null : Master.FindControl("LoginNameDisplay") as Label; }
        }
        
        // ReSharper disable InconsistentNaming
        protected void Page_Load(object sender, EventArgs e)
        // ReSharper restore InconsistentNaming
        {           
            try
            {
                LoginNameDisplay.Text = "Welcome, " + CurrentUser.FirstName;
                LogoutButton.Visible = true;
                NotLoggedInPanel.Visible = false;
                MainContent.Visible = true;                               
            }
            catch (Exception)
            {
                NotLoggedInPanel.Visible = true;
                MainContent.Visible = false;
                LogoutButton.Visible = false;
                LoginNameDisplay.Text = "";
            }

            DoCustomPageLoad();
        }

        // Does nothing here but can be overridden if desired.
        protected virtual void DoCustomPageLoad()
        {
        }

        protected Person CurrentUser
        {
            get
            {
                var repo = new Repository();
                var userID = (Guid)Session["UserID"];
                return repo.FindPersonByID(userID);
            }
        }
    }
}