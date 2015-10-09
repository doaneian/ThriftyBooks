﻿using System;
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
        Table tblRentTemp;
        Table tblUsedTemp;
        Table tblNewTemp;

        public void refreshAllTables()
        {
            refreshRentTable();
            refreshUsedTable();
            refreshNewTable();
        }

        public void refreshTable(int type)
        {
            Table table = getTable(type);

            if (Main.tableStatus[type] == (int)tableStatusCode.eInitial)
            {
                buildTable(type);
            }
            else if (Main.tableStatus[type] == (int)tableStatusCode.eExpanded)
            {
                table = new Table();
                expandTable(type);
            }
        }

        public void refreshNewTable()
        {
            refreshTable((int)condition.eNew);
        }

        public void refreshUsedTable()
        {
            refreshTable((int)condition.eUsed);
        }

        public void refreshRentTable()
        {
            refreshTable((int)condition.eRent);
        }

        public void removeTable(int type)
        {
            form1.Controls.Remove(getTable(type));
        }

        public void expandNewTable()
        {
            refreshTable((int)condition.eRent);
            refreshTable((int)condition.eUsed);
            expandTable((int)condition.eNew);
        }

        public void expandUsedTable()
        {
            refreshTable((int)condition.eRent);
            expandTable((int)condition.eUsed);
            refreshTable((int)condition.eNew);
        }

        public void expandRentTable()
        {
            expandTable((int)condition.eRent);
            refreshTable((int)condition.eUsed);
            refreshTable((int)condition.eNew);
        }

        public void expandTable(int type)
        {
            Table table = getTable(type);
            table = new Table();
            setTableStyle(table);

            btnExpandRent2.Visible = true;
            btnExpandUsed2.Visible = true;
            btnExpandNew2.Visible = true;

            setTableHeader(type, table);
            for (int i = 1; i <= 3; i++)
            {
                insertDataRow(i, type, table);
            }

            addTableToForm(type, table);

            Main.tableStatus[type] = (int) tableStatusCode.eExpanded;
        }

        public void buildTable(int type)
        {
            Table table = getTable(type);
            table = new Table();
            setTableStyle(table);

            btnExpandRent2.Visible = true;
            btnExpandUsed2.Visible = true;
            btnExpandNew2.Visible = true;

            setTableHeader(type, table);
            insertDataRow(1, type, table);

            addTableToForm(type, table);

            Main.tableStatus[type] = (int)tableStatusCode.eInitial;
        }

        public Table getTable(int type)
        {
            if (type == (int)condition.eRent)
            {
                return tblRentTemp;
            }

            if (type == (int)condition.eUsed)
            {
                return tblUsedTemp;
            }

            if (type == (int)condition.eNew)
            {
                return tblNewTemp;
            }

            return null;
        }

        public void addTableToForm(int type, Table table)
        {
            if (type == (int)condition.eRent)
            {
                form1.Controls.AddAt(5, table);
            }
            if (type == (int)condition.eUsed)
            {
                form1.Controls.AddAt(8, table);
            }
            if (type == (int)condition.eNew)
            {
                form1.Controls.AddAt(11, table);
            }
        }

        public void setTableHeader(int type, Table table)
        {
            TableRow tHeaderRow = new TableRow();
            table.Controls.Add(tHeaderRow);

            TableCell header = new TableCell();
            header.Text = getTableHeaderText(type);
            header.BorderStyle = BorderStyle.Solid;
            header.BorderWidth = Unit.Pixel(1);
            header.ColumnSpan = 2;
            header.HorizontalAlign = HorizontalAlign.Center;
            header.Font.Size = FontUnit.Point(20);
            tHeaderRow.Controls.Add(header);
        }

        public string getTableHeaderText(int type)
        {
            if (type == (int)condition.eRent)
            {
                return "Rentals";
            }

            if (type == (int)condition.eUsed)
            {
                return "Used";
            }

            if (type == (int)condition.eNew)
            {
                return "New";
            }

            return null;
        }

        public void insertDataRow(int index, int type, Table table)
        {
            SourceNode node = getSourceNode(index, type);

            TableRow trow = new TableRow();
            table.Controls.Add(trow);

            TableCell source = new TableCell();
            source.Text = node.getSourceName();
            setSourceCellStyle(source);
            trow.Controls.Add(source);

            TableCell price = new TableCell();
            HyperLink link = new HyperLink();
            link.NavigateUrl = node.getLink();
            link.Text = node.getPrice().ToString("C");
            link.Target = "_blank";
            price.Controls.Add(link);

            setPriceCellStyle(price);
            trow.Controls.Add(price);
        }

        public SourceNode getSourceNode(int index, int type)
        {
            if (type == (int)condition.eRent)
            {
                return Main.rentList.getNode(index);
            }

            if (type == (int)condition.eUsed)
            {
                return Main.usedList.getNode(index);
            }

            if (type == (int)condition.eNew)
            {
                return Main.newList.getNode(index);
            }

            return null;
        }

        public void setTableStyle(Table table)
        {
            table.CellSpacing = 0;
            table.Visible = true;
            table.CellPadding = 5;
            table.Width = 200;
        }

        public void setSourceCellStyle(TableCell source)
        {
            source.BorderStyle = BorderStyle.Solid;
            source.BorderWidth = Unit.Pixel(1);
            source.Height = 60;
        }

        public void setPriceCellStyle(TableCell price)
        {
            price.BorderStyle = BorderStyle.Solid;
            price.BorderWidth = Unit.Pixel(1);
            price.HorizontalAlign = HorizontalAlign.Center;
        }
    }
}