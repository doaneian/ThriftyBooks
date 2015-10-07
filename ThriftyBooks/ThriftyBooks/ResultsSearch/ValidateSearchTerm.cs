using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThriftyBooks.Data;
using ThriftyBooksEnums;

namespace ThriftyBooks.ResultsSearch
{
    public class ValidateSearchTerm
    {
        public int CheckInput()
        {
            return (int) returnCodes.eSuccess;
        }
    }
}