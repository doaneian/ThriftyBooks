using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThriftyBooks.Data;
using ThriftyBooks.ResultsSearch.APICalling;
using ThriftyBooksEnums;

namespace ThriftyBooks.ResultsSearch
{
    public class Search
    {
        int ISBN;

        public int getResults()
        {
            ValidateSearchTerm validate = new ValidateSearchTerm();

            int rc = validate.CheckInput();
            if (rc != (int) returnCodes.eSuccess)
            {
                giveError(rc);
                return rc;
            }

            APICaller apiCaller = new APICaller();
            rc = apiCaller.callAPIs();

            return rc;
        } 

        public void giveError(int rc)
        {

        }
    }
}