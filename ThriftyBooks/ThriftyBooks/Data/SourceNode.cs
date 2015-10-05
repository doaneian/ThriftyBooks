using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThriftyBooks.Data
{
    public class SourceNode
    {
        SourceNode next;

        String sourceName;
        double price;
        String link;

        public SourceNode(String sourceName, double price, String link)
        {
            this.sourceName = sourceName;
            this.price = price;
            this.link = link;
        }

        public SourceNode getNext()
        {
            return next;
        }

        public String getSourceName()
        {
            return sourceName;
        }

        public double getPrice()
        {
            return price;
        }

        public String getLink()
        {
            return link;
        }

        public void setNext(SourceNode next)
        {
            this.next = next;
        }

        public void setSourceName(String sourceName)
        {
            this.sourceName = sourceName;
        }

        public void setPrice(double price)
        {
            this.price = price;
        }

        public void setLink(String link)
        {
            this.link = link;
        }
    }
}