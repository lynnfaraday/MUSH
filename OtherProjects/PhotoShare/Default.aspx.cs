// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Default.aspx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the _Default type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using App_Code;

public partial class Default : BasePage
{
    protected void PageLoad(object sender, EventArgs e)
    {
        
    }
    protected void LoginButtonClick(object sender, EventArgs e)
    {
        var users = WebUser.GetUsernames(DataDir);
        if (!users.Contains(Username.Text))
        {
            ErrorMessage.Text = "Invalid username or password.";
            InitializeCurrentUser(string.Empty);
            return;
        }
        Password password = new Password(PasswordEntry.Text, Password.GetCommonSalt(RootDir));
        string actualPassword = password.ComputeSaltedHash();
        string expectedPassword = Password.GetCommonSaltedPassword(RootDir);
        if (actualPassword != expectedPassword)
        {
            ErrorMessage.Text = "Invalid username or password.";
            InitializeCurrentUser(string.Empty);
            return;
        }

        InitializeCurrentUser(Username.Text);
        Response.Redirect("PhotoList.aspx");
    }
}
