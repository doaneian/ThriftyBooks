using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThriftyBooksEnums;

namespace ThriftyBooks.ResultsSearch.APICalling.APIs
{
    public class APIDemo
    {
        public int callAPIDemo()
        {
            Main.rentList.insertNode("Source 1", 19.99, "http://www.google.com");
            Main.usedList.insertNode("Source 1", 54.76, "http://www.google.com");
            Main.newList.insertNode("Source 1", 231.35, "http://www.google.com");

            return (int) returnCodes.eSuccess;
        }
    }
}