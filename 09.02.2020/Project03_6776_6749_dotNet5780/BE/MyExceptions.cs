using System;
using System.Collections.Generic;
using System.Text;
//using System.Runtime.Serialization;

namespace BE
{
    //[Serializable()]
    public class MyException : Exception
    {
        public object item;
        public MyException() { }
        public MyException(string message) : base(message) { }
        public MyException(string message, GuestRequest gr) : base(message) { item = gr; }
        public MyException(string message, HostingUnit hu) : base(message) { item = hu; }
        public MyException(string message, Order or) : base(message) { item = or; }
        public MyException(string message, Exception inner) : base(message, inner) { }

    }
}
