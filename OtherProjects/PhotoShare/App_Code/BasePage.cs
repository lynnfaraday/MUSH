// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasePage.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Summary description for BasePage
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Web.UI.WebControls;

namespace App_Code
{
    public class BasePage : System.Web.UI.Page
    {
        
        protected WebUser CurrentUser
        {
            get 
            {
                return Session["user"] as WebUser;
            }
        }

        protected void InitializeCurrentUser(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                Session["user"] = null;
            }

            Session["user"] = new WebUser(username, DataDir);
        }


        // Sets up some standard page elements based on whether a user is logged in or not.
        protected void LoadPageForCurrentUser(LinkButton loginErrorButton,
            Label errorMessageBox, Panel realContentPanel,
            Label welcomeMessageBox)
        {
            if (CurrentUser == null)
            {
                loginErrorButton.Visible = true;
                realContentPanel.Visible = false;
                errorMessageBox.Text = "You must log in first.";
                return;
            }

            realContentPanel.Visible = true;
            loginErrorButton.Visible = false;
            welcomeMessageBox.Text = "Welcome, " + CurrentUser.Username;
            errorMessageBox.Text = string.Empty;
            LoadRealContent();
        }

        // Will be called from PageLoad if the user is logged in.
        protected virtual void LoadRealContent()
        {
            
        }
        protected string RootDir { get { return Server.MapPath("."); } }
        protected string DataDir { get { return Server.MapPath("Data"); } }
    }
}
