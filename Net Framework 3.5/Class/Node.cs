using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Net_Framework_3._5
{
    public class Node<T>
    {
        T date;
        Node<T> link;
        public Node(T date, Node<T> link)
        {
            this.date = date;
            this.link = link;
        }
    }
}
