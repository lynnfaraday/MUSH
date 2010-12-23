using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FoxwallDashboard.Database;

namespace FoxwallDashboard
{
    public partial class SearchForCall : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SearchButtonClick(object sender, EventArgs e)
        {
            var repo = new Repository();
            var call = repo.FindCall(c => c.IncidentNumber == int.Parse(IncidentNumberBox.Text));
            if (call == null)
            {
                NoticeLabel.Text = "Call not found.";
                return;
            }

            Response.Redirect("~/AddCall.aspx?CallID=" + call.CallID);
        }
    }
}