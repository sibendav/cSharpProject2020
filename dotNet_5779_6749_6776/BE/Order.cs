using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Order
    {
        private int hostingUnitKey;
        private int guestRequestKey;
        private int orderKey;
        private OrderStatus status;
        private DateTime createDate;
        private DateTime orderDate;
        public int HostingUnitKey
        {
            get { return hostingUnitKey; }
            set { hostingUnitKey = value; }
        }
        public int GuestRequestKey
        {
            get { return guestRequestKey; }
            set { guestRequestKey = value; }
        }
        public int OrderKey
        {
            get { return orderKey; }
            set { orderKey = value; }
        }
        public OrderStatus Status
        {
            get { return status; }
            set { status = value; }
        }
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
        public DateTime OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; }
        }

        //***Additional properties***//

        private double orderFee;

        public double OrderFee
        {
            get { return orderFee; }
            set { orderFee = value; }
        }

        public override string ToString() //do it later
        {
            string s = String.Format("{0:MM/dd/yyyy}", CreateDate);
            return "Order: HELLO I'M NEW ORDER, CREATED IN: " + s;
        }


    }
}
