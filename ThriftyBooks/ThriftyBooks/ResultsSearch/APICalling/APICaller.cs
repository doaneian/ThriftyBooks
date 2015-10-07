using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThriftyBooks.ResultsSearch.APICalling.APIs;
using ThriftyBooksEnums;

namespace ThriftyBooks.ResultsSearch.APICalling
{
    public class APICaller
    {
        public int callAPIs()
        {
            APIDemo demo = new APIDemo();
            int rc = demo.callAPIDemo();

            /*
            if(rc == returnCodes.eSuccess)
            {
                APISource2 source2 = new APISource2();
                rc = source2.callAPISource2();
            }
            */

            return rc;
        }
    }
}