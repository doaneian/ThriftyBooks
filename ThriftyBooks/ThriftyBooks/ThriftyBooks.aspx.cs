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
    public enum returnCodes { eSuccess = 0, eFail = -1, eNoResults = 2, eNoSearchTerm = 3, eInvalidISBN = 4 }
    public enum tableStatusCode { eEmpty = 0, eInitial, eExpanded }
    public enum searchBy { eISBN = 0, eTitle, eAuthor }

    public class SourceNode
    {
        SourceNode next;

        string sourceName;
        double price;
        string link;
        double days;

        public SourceNode(string sourceName, double price, string link)
        {
            this.sourceName = sourceName;
            this.price = price;
            this.link = link;
        }

        public SourceNode(string sourceName, double price, string link, double days)
        {
            this.sourceName = sourceName;
            this.price = price;
            this.link = link;
            this.days = days;
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

        public double getDays()
        {
            return days;
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
        public int getResults(string searchTerm, int searchType)
        {
            ValidateSearchTerm validate = new ValidateSearchTerm();

            int rc = validate.CheckInput(searchTerm);
            if (rc != (int)returnCodes.eSuccess)
            {
                return rc;
            }

            APICaller apiCaller = new APICaller();
            rc = apiCaller.callAPIs(searchTerm, searchType);

            return rc;
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

        public void insertNode(SourceNode insertNode)
        {
            SourceNode pointer = head;

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

    public class bookInfo
    {
        public bookInfo next;

        public long ISBN13;
        public string bookName;
        public string author;
        public string edition;
        public string bookImage;
        
        public bookInfo(long ISBN13, string bookName, string author, string edition, string bookImage)
        {
            this.ISBN13 = ISBN13;
            this.bookName = bookName;
            this.author = author;
            this.edition = edition;
            this.bookImage = bookImage;
        }
    }

    public class bookInfoList
    {
        bookInfo head = new bookInfo(0, "", "", "", "");

        public bookInfo getEndNode()
        {
            bookInfo end = head;
            while(end.next != null)
            {
                end = end.next;
            }

            return end;
        }
        
        public void insertNode(long ISBN13, string bookName, string author, string edition, string bookImage)
        {
            bookInfo end = getEndNode();
            end.next = new bookInfo(ISBN13, bookName, author, edition, bookImage);
        }

        public bookInfo getNodeAt(int index)
        {
            bookInfo returnNode = head;
            for(int i = 0; i < index; i++)
            {
                returnNode = returnNode.next;
            }

            return returnNode;
        }

        public bookInfo selectNodeAt(int index)
        {
            bookInfo selectedNode = getNodeAt(index);
            bookInfo previousNode = getNodeAt(index - 1);

            //Move selected node to the front of the list
            previousNode.next = selectedNode.next;
            selectedNode.next = head.next;
            head.next = selectedNode;

            //Call API to get prices for node
            CampusBooksAPI campusBooks = new CampusBooksAPI();
            campusBooks.callCampusBooksAPI(selectedNode.ISBN13);

            return selectedNode;
        }

        public void deleteList()
        {
            head.next = null;
        }
    }

    public class APICaller
    {
        public int callAPIs(string searchTerm, int searchType)
        {
            CampusBooksAPI campusBooks = new CampusBooksAPI();
            int rc = campusBooks.search(searchTerm, searchType);

            return rc;
        }
    }

    public class CampusBooksAPI
    {
        public int callCampusBooksAPI(long ISBN)
        {
            string key = "tBelKTdZHebtuwvSNKu";
            string getString = "";

            ISBN = Main.bookList.getNodeAt(1).ISBN13;
            Main.rentList.deleteList();
            Main.usedList.deleteList();
            Main.newList.deleteList();

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
                    XElement rentals = offerElement.Element("rental_detail");
                    if (rentals != null)
                    {
                        XElement rental = rentals.Element("rental");
                        if (Main.rentList == null)
                        {
                            Main.rentList = new SourceList();
                        }

                        double rentPrice = double.Parse((string)rental.Element("price"));
                        string rentLink = (string)rental.Element("link");
                        double days = double.Parse((string)rental.Element("days"));
                        Main.rentList.insertNode(new SourceNode(sourceName, rentPrice, rentLink, days));

                    }
                    if (conditionElementID == 1)
                    {
                        if (Main.newList == null)
                        {
                            Main.newList = new SourceList();
                        }
                        Main.newList.insertNode(new SourceNode(sourceName, price, link));
                        Main.tableStatus[(int)condition.eNew] = (int)tableStatusCode.eInitial;
                    }
                    else if (conditionElementID == 2)
                    {
                        if (Main.usedList == null)
                        {
                            Main.usedList = new SourceList();
                        }
                        Main.usedList.insertNode(new SourceNode(sourceName, price, link));
                        Main.tableStatus[(int)condition.eUsed] = (int)tableStatusCode.eInitial;
                    }
                    else if (conditionElementID == 3)
                    {
                        if (Main.rentList == null)
                        {
                            Main.rentList = new SourceList();
                        }
                        Main.rentList.insertNode(new SourceNode(sourceName, price, link));
                        Main.tableStatus[(int)condition.eRent] = (int)tableStatusCode.eInitial;
                    }
                }
            }

            return (int)returnCodes.eSuccess;
        }

        public int search(string searchTerm, int searchType)
        {
            string getString = "";
            string key = "tBelKTdZHebtuwvSNKu";

            if (searchType == (int)searchBy.eISBN)
            {
                getString = string.Format("http://api2.campusbooks.com/12/rest/bookinfo?key={0}&isbn={1}", key, searchTerm);
            }
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
                if(responseElement.Element("errors") != null)
                {
                    if((string)responseElement.Element("errors").Element("error") == "Invalid ISBN")
                    {
                        return (int)returnCodes.eInvalidISBN;
                    }
                }
                return (int)returnCodes.eFail;
            }
            if ((int)responseElement.Element("page").Element("count") == 0)
            {
                return (int)returnCodes.eNoResults;
            }

            if (Main.bookList == null)
            {
                Main.bookList = new bookInfoList();
            }

            Main.bookList.deleteList();

            if (searchType == (int)searchBy.eISBN)
            {
                foreach (XElement bookElement in responseElement.Elements("page"))
                {
                    long ISBN13 = (long)bookElement.Element("isbn13");
                    string bookName = (string)bookElement.Element("title");
                    string author = (string)bookElement.Element("author");
                    string edition = (string)bookElement.Element("edition");
                    string bookImage = (string)bookElement.Element("image");
                    Main.bookList.insertNode(ISBN13, bookName, author, edition, bookImage);
                }
            }
            else
            {
                foreach (XElement bookElement in responseElement.Element("page").Element("results").Elements("book"))
                {
                    long ISBN13 = (long)bookElement.Element("isbn13");
                    string bookName = (string)bookElement.Element("title");
                    string author = (string)bookElement.Element("author");
                    string edition = (string)bookElement.Element("edition");
                    string bookImage = (string)bookElement.Element("image");
                    Main.bookList.insertNode(ISBN13, bookName, author, edition, bookImage);
                }
            }

            return (int)returnCodes.eSuccess;
        }
    }

    public class ValidateSearchTerm
    {
        public int CheckInput(string searchTerm)
        {
            if(searchTerm == "")
            {
                return (int)returnCodes.eNoSearchTerm;
            }

            return (int)returnCodes.eSuccess;
        }
    }

    public class Main
    {
        public static SourceList rentList;
        public static SourceList usedList;
        public static SourceList newList;

        public static bookInfoList bookList;

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
            string target = Request["__EVENTTARGET"];
            if(target == "bookResult")
            {
                viewMoreBooks();
            }

            if(target == "selectResult1")
            {
                insertInitialResultsIntoTables();
            }

            if (target == "selectResult2")
            {
                Main.bookList.selectNodeAt(2);
                insertInitialResultsIntoTables();
            }

            if (target == "selectResult3")
            {
                Main.bookList.selectNodeAt(3);
                insertInitialResultsIntoTables();
            }

            if (target == "selectResult4")
            {
                Main.bookList.selectNodeAt(4);
                insertInitialResultsIntoTables();
            }

            if (target == "selectResult5")
            {
                Main.bookList.selectNodeAt(5);
                insertInitialResultsIntoTables();
            }

            if (Main.newList == null || Main.usedList == null || Main.rentList == null)
            {
                btnExpandRent2.Visible = false;
                btnExpandUsed2.Visible = false;
                btnExpandNew2.Visible = false;
            }
        }

        public void Show(String Message)
        {
            ClientScript.RegisterStartupScript(
               Page.GetType(),
               "MessageBox",
               "<script language='javascript'>alert('" + Message + "');</script>"
            );
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
            int rc = search.getResults(searchTerm, type);
            if(rc != (int)returnCodes.eSuccess)
            {
                giveError(rc);
                return;
            }

            insertInitialResultsIntoTables();
        }

        public void giveError(int rc)
        {
            if(rc == (int)returnCodes.eNoResults)
            {
                Show("No results were found");
                bookResult.Visible = false;
                btnExpandRent2.Visible = false;
                btnExpandUsed2.Visible = false;
                btnExpandNew2.Visible = false;
            }

            if(rc == (int)returnCodes.eNoSearchTerm)
            {
                Show("Please enter a search term");
                bookResult.Visible = false;
                btnExpandRent2.Visible = false;
                btnExpandUsed2.Visible = false;
                btnExpandNew2.Visible = false;
            }

            if(rc == (int)returnCodes.eInvalidISBN)
            {
                Show("Please enter a valid ISBN");
                bookResult.Visible = false;
                btnExpandRent2.Visible = false;
                btnExpandUsed2.Visible = false;
                btnExpandNew2.Visible = false;
            }
        }

        public void insertInitialResultsIntoTables()
        {
            showCurrentBook(false);
            buildTable((int)condition.eRent);
            buildTable((int)condition.eUsed);
            buildTable((int)condition.eNew);
        }

        public void expandBooks(object sender, EventArgs e)
        {

        }

        public void removeAllBookResultRows()
        {
            for (int i = 1; i <= 6; i++)
            {
                bookResult.Controls.Remove(getBookResultRow(i));
            }
        }

        public void showCurrentBook(bool expanding)
        {
            bookInfo currentBook;
            if (!expanding)
            {
                currentBook = Main.bookList.selectNodeAt(1);
            }
            else
            {
                currentBook = Main.bookList.getNodeAt(1);
            }

            removeAllBookResultRows();

            bookResult.Visible = true;
            bookResult.Controls.Add(getBookResultRow(1));
            getImage(1).ImageUrl = currentBook.bookImage;
            getTitle(1).Text = currentBook.bookName;
            getAuthor(1).Text = currentBook.author;
            getEdition(1).Text = "Edition: " + currentBook.edition;
            getISBN(1).Text = "ISBN: " + currentBook.ISBN13;
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
            showCurrentBook(true);
            refreshTable((int)condition.eRent);
            refreshTable((int)condition.eUsed);
            expandTable((int)condition.eNew);
        }

        public void expandUsedTable()
        {
            showCurrentBook(true);
            refreshTable((int)condition.eRent);
            expandTable((int)condition.eUsed);
            refreshTable((int)condition.eNew);
        }

        public void expandRentTable()
        {
            showCurrentBook(true);
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

        public void viewMoreBooks()
        {
            btnExpandRent2.Visible = false;
            btnExpandUsed2.Visible = false;
            btnExpandNew2.Visible = false;

            removeAllBookResultRows();

            for (int i = 1; i <= 5; i++)
            {
                bookInfo currentBook = Main.bookList.getNodeAt(i);
                if(currentBook == null)
                {
                    return;
                }

                bookResult.Controls.Add(getBookResultRow(i + 1));
                getImage(i + 1).ImageUrl = currentBook.bookImage;
                getTitle(i + 1).Text = currentBook.bookName;
                getAuthor(i + 1).Text = currentBook.author;
                getEdition(i + 1).Text = "Edition: " + currentBook.edition;
                getISBN(i + 1).Text = "ISBN: " + currentBook.ISBN13;
            }
        }

        public TableRow getBookResultRow(int i)
        {
            if (i == 1)
            {
                return bookResultRow1;
            }

            if (i == 2)
            {
                return bookResultRow2;
            }

            if (i == 3)
            {
                return bookResultRow3;
            }

            if (i == 4)
            {
                return bookResultRow4;
            }

            if (i == 5)
            {
                return bookResultRow5;
            }

            if (i == 6)
            {
                return bookResultRow6;
            }

            return null;
        }

        public Image getImage(int i)
        {
            if(i == 1)
            {
                return bookImage1;
            }

            if (i == 2)
            {
                return bookImage2;
            }

            if (i == 3)
            {
                return bookImage3;
            }

            if (i == 4)
            {
                return bookImage4;
            }

            if (i == 5)
            {
                return bookImage5;
            }

            if (i == 6)
            {
                return bookImage6;
            }

            return null;
        }

        public Label getTitle(int i)
        {
            if (i == 1)
            {
                return lTitle1;
            }

            if (i == 2)
            {
                return lTitle2;
            }

            if (i == 3)
            {
                return lTitle3;
            }

            if (i == 4)
            {
                return lTitle4;
            }

            if (i == 5)
            {
                return lTitle5;
            }

            if (i == 6)
            {
                return lTitle6;
            }

            return null;
        }

        public Label getAuthor(int i)
        {
            if (i == 1)
            {
                return lAuthor1;
            }

            if (i == 2)
            {
                return lAuthor2;
            }

            if (i == 3)
            {
                return lAuthor3;
            }

            if (i == 4)
            {
                return lAuthor4;
            }

            if (i == 5)
            {
                return lAuthor5;
            }

            if (i == 6)
            {
                return lAuthor6;
            }

            return null;
        }

        public Label getEdition(int i)
        {
            if (i == 1)
            {
                return lEdition1;
            }

            if (i == 2)
            {
                return lEdition2;
            }

            if (i == 3)
            {
                return lEdition3;
            }

            if (i == 4)
            {
                return lEdition4;
            }

            if (i == 5)
            {
                return lEdition5;
            }

            if (i == 6)
            {
                return lEdition6;
            }

            return null;
        }

        public Label getISBN(int i)
        {
            if (i == 1)
            {
                return lISBN1;
            }

            if (i == 2)
            {
                return lISBN2;
            }

            if (i == 3)
            {
                return lISBN3;
            }

            if (i == 4)
            {
                return lISBN4;
            }

            if (i == 5)
            {
                return lISBN5;
            }

            if (i == 6)
            {
                return lISBN6;
            }

            return null;
        }

        public void addTableToForm(int type, Table table)
        {
            if (type == (int)condition.eRent)
            {
                form1.Controls.AddAt(13, table);
            }
            if (type == (int)condition.eUsed)
            {
                form1.Controls.AddAt(16, table);
            }
            if (type == (int)condition.eNew)
            {
                form1.Controls.AddAt(19, table);
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
            if (type == (int)condition.eRent)
            {
                header.ColumnSpan = 3;
            }
            else
            {
                header.ColumnSpan = 2;
            }
            header.HorizontalAlign = HorizontalAlign.Center;
            header.Font.Size = FontUnit.Point(20);
            tHeaderRow.Controls.Add(header);

            TableRow columnLabels = new TableRow();
            table.Controls.Add(columnLabels);

            TableCell sourceLabel = new TableCell();
            columnLabels.Controls.Add(sourceLabel);
            sourceLabel.Font.Size = 14;
            sourceLabel.BorderStyle = BorderStyle.Solid;
            sourceLabel.BorderWidth = Unit.Pixel(1);
            sourceLabel.Text = "Source";

            if(type == (int)condition.eRent)
            {
                TableCell rentalPeriod = new TableCell();
                columnLabels.Controls.Add(rentalPeriod);
                rentalPeriod.BorderStyle = BorderStyle.Solid;
                rentalPeriod.BorderWidth = Unit.Pixel(1);
                rentalPeriod.Font.Size = 14;
                rentalPeriod.Text = "Rental\nPeriod";
            }

            TableCell priceLabel = new TableCell();
            columnLabels.Controls.Add(priceLabel);
            priceLabel.BorderStyle = BorderStyle.Solid;
            priceLabel.BorderWidth = Unit.Pixel(1);
            priceLabel.Font.Size = 14;
            priceLabel.Text = "Price";
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

            if(type == (int)condition.eRent)
            {
                TableCell daysRented = new TableCell();
                trow.Controls.Add(daysRented);
                setDaysCellStyle(daysRented);
                daysRented.Text = node.getDays() + " days";
            }

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
            table.Width = 300;
            table.BackColor = System.Drawing.Color.White;
        }

        public void setSourceCellStyle(TableCell source)
        {
            source.BorderStyle = BorderStyle.Solid;
            source.BorderWidth = Unit.Pixel(1);
            source.Height = 60;
            source.Width = 60;
        }

        public void setDaysCellStyle(TableCell days)
        {
            days.BorderStyle = BorderStyle.Solid;
            days.BorderWidth = Unit.Pixel(1);
        }

        public void setPriceCellStyle(TableCell price)
        {
            price.BorderStyle = BorderStyle.Solid;
            price.BorderWidth = Unit.Pixel(1);
            price.HorizontalAlign = HorizontalAlign.Center;
        }
    }
}