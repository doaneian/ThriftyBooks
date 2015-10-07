using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThriftyBooks.Data
{
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
}