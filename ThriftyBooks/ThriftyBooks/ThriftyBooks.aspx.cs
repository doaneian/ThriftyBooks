using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThriftyBooksEnums;

namespace ThriftyBooks
{
    public partial class ThriftyBooks : System.Web.UI.Page
    {
        Main mainProcessor;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SearchEvent(object sender, EventArgs e)
        {
            if(mainProcessor == null)
            {
                mainProcessor = new Main(this);
            }

            mainProcessor.ProcessSearchTerm();
        }
    }
}