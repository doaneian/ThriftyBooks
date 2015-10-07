using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using ThriftyBooks.Data;
using ThriftyBooksEnums;

namespace ThriftyBooks
{
    public partial class ThriftyBooks : System.Web.UI.Page
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
            SourceNode node = Main.rentList.getNode(1);

            TableRow tHeaderRow = new TableRow();
            tblRent.Controls.Add(tHeaderRow);

            TableCell header = new TableCell();
            header.Text = "Rentals";
            header.BorderStyle = BorderStyle.Solid;
            header.BorderWidth = Unit.Pixel(1);
            header.ColumnSpan = 2;
            header.HorizontalAlign = HorizontalAlign.Center;
            header.Font.Size = FontUnit.Point(20);
            tHeaderRow.Controls.Add(header);

            TableRow trow = new TableRow();
            tblRent.Controls.Add(trow);

            TableCell source = new TableCell();
            source.Text = node.getSourceName();
            source.BorderStyle = BorderStyle.Solid;
            source.BorderWidth = Unit.Pixel(1);
            trow.Controls.Add(source);

            TableCell price = new TableCell();
            price.Text = "$" + node.getPrice();
            price.BorderStyle = BorderStyle.Solid;
            price.BorderWidth = Unit.Pixel(1);
            price.HorizontalAlign = HorizontalAlign.Right;
            trow.Controls.Add(price);
        }

        public void buildUsedTable()
        {
            SourceNode node = Main.usedList.getNode(1);

            TableRow tHeaderRow = new TableRow();
            tblUsed.Controls.Add(tHeaderRow);

            TableCell header = new TableCell();
            header.Text = "Used";
            header.BorderStyle = BorderStyle.Solid;
            header.BorderWidth = Unit.Pixel(1);
            header.ColumnSpan = 2;
            header.HorizontalAlign = HorizontalAlign.Center;
            header.Font.Size = FontUnit.Point(20);
            tHeaderRow.Controls.Add(header);

            TableRow trow = new TableRow();
            tblUsed.Controls.Add(trow);

            TableCell source = new TableCell();
            source.Text = node.getSourceName();
            source.BorderStyle = BorderStyle.Solid;
            source.BorderWidth = Unit.Pixel(1);
            trow.Controls.Add(source);

            TableCell price = new TableCell();
            price.Text = "$" + node.getPrice();
            price.BorderStyle = BorderStyle.Solid;
            price.BorderWidth = Unit.Pixel(1);
            price.HorizontalAlign = HorizontalAlign.Right;
            trow.Controls.Add(price);
        }

        public void buildNewTable()
        {
            SourceNode node = Main.newList.getNode(1);

            TableRow tHeaderRow = new TableRow();
            tblNew.Controls.Add(tHeaderRow);

            TableCell header = new TableCell();
            header.Text = "New";
            header.BorderStyle = BorderStyle.Solid;
            header.BorderWidth = Unit.Pixel(1);
            header.ColumnSpan = 2;
            header.HorizontalAlign = HorizontalAlign.Center;
            header.Font.Size = FontUnit.Point(20);
            tHeaderRow.Controls.Add(header);

            TableRow trow = new TableRow();
            tblNew.Controls.Add(trow);

            TableCell source = new TableCell();
            source.Text = node.getSourceName();
            source.BorderStyle = BorderStyle.Solid;
            source.BorderWidth = Unit.Pixel(1);
            trow.Controls.Add(source);

            TableCell price = new TableCell();
            price.Text = "$" + node.getPrice();
            price.BorderStyle = BorderStyle.Solid;
            price.BorderWidth = Unit.Pixel(1);
            price.HorizontalAlign = HorizontalAlign.Right;
            trow.Controls.Add(price);
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