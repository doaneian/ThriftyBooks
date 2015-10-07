using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThriftyBooks.Data
{
    public class SourceList
    {
        SourceNode head = new SourceNode("", 0, "");

        public SourceNode getNode(int index)
        {
            int counter = 0;
            SourceNode returnNode = head;

            while(counter < index)
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
        public Boolean comesAfterNode(SourceNode insert, SourceNode current)
        {
            if(current.getNext() == null)
            {
                return true;
            }

            if (insert.getPrice() <= current.getNext().getPrice())
            {
                return true;
            }

            return false;
        }

        public void insertNode(string sourceName, double price, string link)
        {
            SourceNode pointer = head;
            while(pointer.getNext() != null)
            {
                SourceNode insertNode = new SourceNode(sourceName, price, link);
                pointer = pointer.getNext();

                if(comesAfterNode(insertNode, pointer))
                {
                    SourceNode temp = pointer.getNext();
                    pointer.setNext(insertNode);
                    pointer.getNext().setNext(temp);
                    break;
                }    
            }
        }

        public void deleteList()
        {
            //Garbage collector will clean out the dereferenced nodes
            head.setNext(null);
        }
    }
}