using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using ThriftyBooks.Data;
using ThriftyBooksEnums;

namespace ThriftyBooks.Tables
{
    public class TableBuilder : ThriftyBooks
    {
        public void expandNewTable(ref SourceList newList)
        {

        }

        public void expandUsedTable(ref SourceList usedList)
        {

        }

        public void expandRentTable(ref SourceList rentList)
        {

        }

        public void expandTable(ref SourceList bookList, int type)
        {

        }

        public void buildRentTable(ref SourceList rentList)
        {
            TableCell source = new TableCell();
            source.Text = rentList.getNode(1).getSourceName();

            TableRow trow = new TableRow();
            trow.Cells.Add(source);

            tblRent.Rows.Add(trow);
        }

        public void buildUsedTable(ref SourceList usedList)
        {

        }

        public void buildNewTable(ref SourceList newList)
        {

        }

        public void buildTable(ref SourceList bookList, int type)
        {
            if(type == (int) condition.eRent)
            {
                buildRentTable(ref bookList);
            }

            if (type == (int) condition.eUsed)
            {
                buildUsedTable(ref bookList);
            }

            if (type == (int) condition.eNew)
            {
                buildNewTable(ref bookList);
            }
        }
    }
}