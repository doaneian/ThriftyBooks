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
        public void expandNewTable()
        {

        }

        public void expandUsedTable()
        {

        }

        public void expandRentTable()
        {

        }

        public void expandTable(int type)
        {

        }

        public void buildRentTable()
        {
            tblRent = new Table();

            TableCell source = new TableCell();
            SourceNode node = Main.rentList.getNode(1);
            source.Text = Main.rentList.getNode(1).getSourceName();

            TableRow trow = new TableRow();
            trow.Cells.Add(source);

            tblRent.Rows.Add(trow);
        }

        public void buildUsedTable()
        {

        }

        public void buildNewTable()
        {

        }

        public void buildTable(int type)
        {
            if(type == (int) condition.eRent)
            {
                buildRentTable();
            }

            if (type == (int) condition.eUsed)
            {
                buildUsedTable();
            }

            if (type == (int) condition.eNew)
            {
                buildNewTable();
            }
        }
    }
}