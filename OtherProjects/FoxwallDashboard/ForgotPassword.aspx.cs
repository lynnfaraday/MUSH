// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ForgotPassword.aspx.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the ForgotPassword type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Web.Mail;
namespace FoxwallDashboard
{
    public partial class ForgotPassword : BasePage
    {

        private static void SendEmail(string toAddress)
        {
            // .NET says this is obsolete but godaddy says this is how you do it.  Godaddy wins.
#pragma warning disable 612,618
            const string Server = "relay-hosting.secureserver.net";
            MailMessage oMail = new MailMessage
            {
                From = "webmaster@foxwall.org",
                To = toAddress,
                Subject = "Test email subject",
                BodyFormat = MailFormat.Html,
                Priority = MailPriority.Normal,
                Body = "Sent at: " + DateTime.Now
            };
            SmtpMail.SmtpServer = Server;
            SmtpMail.Send(oMail);
// ReSharper disable RedundantAssignment
            oMail = null; // free up resources
// ReSharper restore RedundantAssignment
#pragma warning restore 612,618
        }

        protected void ResetClick(object sender, EventArgs e)
        {
            SendEmail("lomeara@gmail.com");
        }
    }
}