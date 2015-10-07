using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThriftyBooks.Data;
using ThriftyBooks.ResultsSearch;

namespace ThriftyBooks
{
    public class Main
    {
        public static SourceList rentList;
        public static SourceList usedList;
        public static SourceList newList;
        
        public static int ISBN;

        public void ProcessSearchTerm()
        {
            resetLists();

            Search search = new Search();
            search.getResults();
        }

        public void resetLists()
        {
            //This is the singleton design pattern. It checks if an object is already created,
            //and if not, it creates one.  This ensures exactly ONE object gets created.
            if (rentList == null)
            {
                rentList = new SourceList();
            }
            if (usedList == null)
            {
                usedList = new SourceList();
            }
            if (newList == null)
            {
                newList = new SourceList();
            }

            rentList.deleteList();
            usedList.deleteList();
            newList.deleteList();
        }
    }
}