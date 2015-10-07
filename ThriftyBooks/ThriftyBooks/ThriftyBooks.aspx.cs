using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ThriftyBooks
{
    public partial class ThriftyBooks : System.Web.UI.Page
    {
        Main mainProcessor = new Main();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SearchEvent(object sender, EventArgs e)
        {
            mainProcessor.ProcessSearchTerm();
        }
    }
}