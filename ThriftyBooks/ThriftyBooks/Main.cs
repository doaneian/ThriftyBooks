using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThriftyBooks.Data;
using ThriftyBooks.ResultsSearch;
using ThriftyBooksEnums;

namespace ThriftyBooks
{
    public class Main
    {
        public static SourceList rentList;
        public static SourceList usedList;
        public static SourceList newList;
        
        public static int ISBN;
        public static int[] tableStatus = new int[3] { 0, 0, 0 };

        public ThriftyBooks formHandler;

        public Main(ThriftyBooks formHandler)
        {
            this.formHandler = formHandler;
        }

        public void ProcessSearchTerm()
        {
            resetLists();

            Search search = new Search();
            search.getResults();

            insertInitialResultsIntoTables();
        }

        public void insertInitialResultsIntoTables()
        {
            formHandler.buildTable((int) condition.eRent);
            formHandler.buildTable((int) condition.eUsed);
            formHandler.buildTable((int) condition.eNew);
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

        public void expandRentTable()
        {
            formHandler.expandRentTable();
        }

        public void expandUsedTable()
        {
            formHandler.expandUsedTable();
        }

        public void expandNewTable()
        {
            formHandler.expandNewTable();
        }
    }
}