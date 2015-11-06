using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Thrifty
{
    public enum condition { eRent = 0, eUsed, eNew };
    public enum returnCodes { eSuccess = 0, eFail = -1 }
    public enum tableStatusCode { eEmpty = 0, eInitial, eExpanded }
    public enum searchBy { eISBN = 0, eTitle, eAuthor }

    public class SourceNode
    {
        SourceNode next;

        string sourceName;
        double price;
        string link;

        public SourceNode(string sourceName, double price, string link)
        {
            this.sourceName = sourceName;
            this.price = price;
            this.link = link;
        }

        public SourceNode getNext()
        {
            return next;
        }

        public string getSourceName()
        {
            return sourceName;
        }

        public double getPrice()
        {
            return price;
        }

        public string getLink()
        {
            return link;
        }

        public void setNext(SourceNode next)
        {
            this.next = next;
        }

        public void setSourceName(string sourceName)
        {
            this.sourceName = sourceName;
        }

        public void setPrice(double price)
        {
            this.price = price;
        }

        public void setLink(string link)
        {
            this.link = link;
        }
    }

    public class Search
    {
        int ISBN;

        public int getResults(string searchTerm, int searchType)
        {
            ValidateSearchTerm validate = new ValidateSearchTerm();

            int rc = validate.CheckInput(searchTerm);
            if (rc != (int)returnCodes.eSuccess)
            {
                giveError(rc);
                return rc;
            }

            APICaller apiCaller = new APICaller();
            rc = apiCaller.callAPIs(searchTerm, searchType);

            return rc;
        }

        public void giveError(int rc)
        {

        }
    }

    public class SourceList
    {
        SourceNode head = new SourceNode("", 0, "");

        public SourceNode getNode(int index)
        {
            int counter = 0;
            SourceNode returnNode = head;

            while (counter < index)
            {
                try
                {
                    returnNode = returnNode.getNext();
                    counter++;
                }
                catch
                {
                    returnNode = null;
                    break;
                }
            }

            return returnNode;
        }

        //I created this function so that when we add more criteria to determining what sources come in what order,
        //such as commission rates, we can easily add them here.
        public bool comesAfterNode(SourceNode insert, SourceNode current)
        {
            if (current == head)
            {
                return false;
            }

            if (insert.getPrice() <= current.getPrice())
            {
                return true;
            }

            return false;
        }

        public void insertNode(string sourceName, double price, string link)
        {
            SourceNode pointer = head;
            SourceNode insertNode = new SourceNode(sourceName, price, link);

            while (pointer.getNext() != null)
            {
                if (comesAfterNode(insertNode, pointer.getNext()))
                {
                    SourceNode temp = pointer.getNext();
                    pointer.setNext(insertNode);
                    pointer.getNext().setNext(temp);
                    return;
                }

                pointer = pointer.getNext();
            }

            pointer.setNext(insertNode);
        }

        public void deleteList()
        {
            //Garbage collector will clean out the dereferenced nodes
            head.setNext(null);
        }
    }

    public class APICaller
    {
        public int callAPIs(string searchTerm, int searchType)
        {
            CampusBooksAPI campusBooks = new CampusBooksAPI();
            campusBooks.callCampusBooksAPI(searchTerm, searchType);

            return (int)returnCodes.eSuccess;
        }
    }

    public class CampusBooksAPI
    {
        public int callCampusBooksAPI(string searchTerm, int searchType)
        {
            string key = "tBelKTdZHebtuwvSNKu";
            string getString = "";
            long ISBN = 0;

            if (searchType == (int)searchBy.eISBN)
            {
                ISBN = long.Parse(searchTerm);
            }
            else
            {
                ISBN = getISBN(searchTerm, searchType);
            }

            getString = string.Format("http://api2.campusbooks.com/12/rest/prices?key={0}&isbn={1}", key, ISBN);

            HttpWebRequest webRequest = WebRequest.Create(getString) as HttpWebRequest;
            webRequest.Method = "GET";

            StreamReader responseStream = new StreamReader(webRequest.GetResponse().GetResponseStream());
            XDocument responseReader = XDocument.Parse(responseStream.ReadToEnd().ToString());
            webRequest.GetResponse().Close();

            XElement responseElement = responseReader.Element("response");
            if ((string)responseElement.Attribute("status") != "ok")
            {
                return (int)returnCodes.eFail;
            }

            foreach (XElement conditionElement in responseElement.Element("page").Element("offers").Elements("condition"))
            {
                foreach (XElement offerElement in conditionElement.Elements("offer"))
                {
                    string sourceName = (string)offerElement.Element("merchant_image");
                    double price = double.Parse((string)offerElement.Element("total_price"));
                    string link = (string)offerElement.Element("link");
                    int conditionElementID = (int)conditionElement.Attribute("id");
                    if (conditionElementID == 1)
                    {
                        if (Main.newList == null)
                        {
                            Main.newList = new SourceList();
                        }
                        Main.newList.insertNode(sourceName, price, link);
                        Main.tableStatus[(int)condition.eNew] = (int)tableStatusCode.eInitial;
                    }
                    else if (conditionElementID == 2)
                    {
                        if (Main.usedList == null)
                        {
                            Main.usedList = new SourceList();
                        }
                        Main.usedList.insertNode(sourceName, price, link);
                        Main.tableStatus[(int)condition.eUsed] = (int)tableStatusCode.eInitial;
                    }
                    else if (conditionElementID == 3)
                    {
                        if (Main.rentList == null)
                        {
                            Main.rentList = new SourceList();
                        }
                        Main.rentList.insertNode(sourceName, price, link);
                        Main.tableStatus[(int)condition.eRent] = (int)tableStatusCode.eInitial;
                    }
                }
            }

            return (int)returnCodes.eSuccess;
        }

        public long getISBN(string searchTerm, int searchType)
        {
            string getString = "";
            string key = "tBelKTdZHebtuwvSNKu";

            if (searchType == (int)searchBy.eTitle)
            {
                getString = string.Format("http://api2.campusbooks.com/12/rest/search?key={0}&title={1}", key, searchTerm);
            }
            else if (searchType == (int)searchBy.eAuthor)
            {
                getString = string.Format("http://api2.campusbooks.com/12/rest/search?key={0}&author={1}", key, searchTerm);
            }

            HttpWebRequest bookInfoRequest = WebRequest.Create(getString) as HttpWebRequest;
            bookInfoRequest.Method = "GET";

            StreamReader responseStream = new StreamReader(bookInfoRequest.GetResponse().GetResponseStream());
            XDocument bookInfoResponseReader = XDocument.Parse(responseStream.ReadToEnd().ToString());
            bookInfoRequest.GetResponse().Close();

            XElement responseElement = bookInfoResponseReader.Element("response");
            if ((string)responseElement.Attribute("status") != "ok")
            {
                return (int)returnCodes.eFail;
            }
            if ((int)responseElement.Element("page").Element("count") == 0)
            {
                return (int)returnCodes.eFail;
            }

            XElement bookElement = responseElement.Element("page").Element("results").Element("book");
            return (long)bookElement.Element("isbn13");
        }
    }

    public class ValidateSearchTerm
    {
        public int CheckInput(string searchTerm)
        {
            if(searchTerm == "")
            {
                return (int)returnCodes.eFail;
            }

            return (int)returnCodes.eSuccess;
        }
    }

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

    public partial class ThriftyBooks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Main.newList == null || Main.usedList == null || Main.rentList == null)
            {
                btnExpandRent2.Visible = false;
                btnExpandUsed2.Visible = false;
                btnExpandNew2.Visible = false;

                rbISBN.Checked = true;
                txtbxSearchTerm.Text = "";
            }
        }

        protected void SearchEvent(object sender, EventArgs e)
        {
            int searchType = -1;

            if(rbISBN.Checked)
            {
                searchType = (int)searchBy.eISBN;
            }
            else if (rbTitle.Checked)
            {
                searchType = (int)searchBy.eTitle;
            }
            else if (rbAuthor.Checked)
            {
                searchType = (int)searchBy.eAuthor;
            }

            ProcessSearchTerm(txtbxSearchTerm.Text, searchType);
        }

        public void ProcessSearchTerm(string searchTerm, int type)
        {
            resetLists();

            Search search = new Search();
            search.getResults(searchTerm, type);

            insertInitialResultsIntoTables();
        }

        public void insertInitialResultsIntoTables()
        {
            buildTable((int)condition.eRent);
            buildTable((int)condition.eUsed);
            buildTable((int)condition.eNew);
        }

        public void resetLists()
        {
            //This is the singleton design pattern. It checks if an object is already created,
            //and if not, it creates one.  This ensures exactly ONE object gets created.
            if (Main.rentList == null)
            {
                Main.rentList = new SourceList();
            }
            if (Main.usedList == null)
            {
                Main.usedList = new SourceList();
            }
            if (Main.newList == null)
            {
                Main.newList = new SourceList();
            }

            Main.rentList.deleteList();
            Main.usedList.deleteList();
            Main.newList.deleteList();
        }

        protected void btnExpandRent(object sender, EventArgs e)
        {
            expandRentTable();
        }

        protected void btnExpandUsed(object sender, EventArgs e)
        {
            expandUsedTable();
        }

        protected void btnExpandNew(object sender, EventArgs e)
        {
            expandNewTable();
        }

        public void refreshAllTables()
        {
            refreshRentTable();
            refreshUsedTable();
            refreshNewTable();
        }

        public void refreshTable(int type)
        {
            if (Main.tableStatus[type] == (int)tableStatusCode.eInitial)
            {
                buildTable(type);
            }
            else if (Main.tableStatus[type] == (int)tableStatusCode.eExpanded)
            {
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
            if(Main.newList == null || Main.usedList == null || Main.rentList == null)
            {
                return;
            }
            Table table = new Table();
            setTableStyle(table);

            btnExpandRent2.Visible = true;
            btnExpandUsed2.Visible = true;
            btnExpandNew2.Visible = true;

            setTableHeader(type, table);
            for (int i = 1; i <= 5; i++)
            {
                insertDataRow(i, type, table);
            }

            addTableToForm(type, table);

            Main.tableStatus[type] = (int)tableStatusCode.eExpanded;
        }

        public void buildTable(int type)
        {
            if(Main.newList == null || Main.usedList == null || Main.rentList == null)
            {
                return;
            }

            Table table = new Table();
            setTableStyle(table);

            btnExpandRent2.Visible = true;
            btnExpandUsed2.Visible = true;
            btnExpandNew2.Visible = true;

            setTableHeader(type, table);
            insertDataRow(1, type, table);

            addTableToForm(type, table);

            Main.tableStatus[type] = (int)tableStatusCode.eInitial;
        }

        public void addTableToForm(int type, Table table)
        {
            if (type == (int)condition.eRent)
            {
                form1.Controls.AddAt(11, table);
            }
            if (type == (int)condition.eUsed)
            {
                form1.Controls.AddAt(14, table);
            }
            if (type == (int)condition.eNew)
            {
                form1.Controls.AddAt(17, table);
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
            if (node == null)
            {
                return;
            }

            TableRow trow = new TableRow();
            table.Controls.Add(trow);

            TableCell source = new TableCell();
            trow.Controls.Add(source);
            Image image = new Image();
            source.Controls.Add(image);
            image.ImageUrl = node.getSourceName();
            setSourceCellStyle(source);

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
            table.BackColor = System.Drawing.Color.White;
        }

        public void setSourceCellStyle(TableCell source)
        {
            source.BorderStyle = BorderStyle.Solid;
            source.BorderWidth = Unit.Pixel(1);
            source.Height = 60;
            source.Width = 60;
        }

        public void setPriceCellStyle(TableCell price)
        {
            price.BorderStyle = BorderStyle.Solid;
            price.BorderWidth = Unit.Pixel(1);
            price.HorizontalAlign = HorizontalAlign.Center;
        }
    }
}