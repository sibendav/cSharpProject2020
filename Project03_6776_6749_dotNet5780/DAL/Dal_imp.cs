//using System;
using System.Collections.Generic;
using System.Text;
using BE;
using DS;
using System.Linq;
using System;

namespace DAL
{
    //***Dal_imp impliment Idal interface***
    //***I added public to the function's signs

    public class Dal_imp : Idal
    {
        //Add and Update for guestRequest        
        public GuestRequest AddCustomerReq(GuestRequest gr)
        {
            if (DS.DataSource.AllGuestRequestsList.Count != 0 && DS.DataSource.AllGuestRequestsList.Exists(item => gr.MailAddress == item.MailAddress && gr.EntryDate == item.EntryDate && gr.ReleaseDate == item.ReleaseDate))
            {
                throw new MyException("This guest request is already exists! ");
            }
            else
            {
                gr.GuestRequestKey = Configuration.getRunningNumberForGuestRequest();
                DS.DataSource.AllGuestRequestsList.Add(gr.Clone());
                return gr;
            }
        }
        public GuestRequest UpdateCustomerReq(GuestRequest gr)
        {
            GuestRequest grOriginal = GetGuestRequest(gr.GuestRequestKey);

            if (grOriginal == null) //there is no privias object to update
            {
                //if (gr.GuestRequestKey == 0)
                gr.GuestRequestKey = Configuration.getRunningNumberForGuestRequest();
                DS.DataSource.AllGuestRequestsList.Add(gr.Clone());
                throw new MyException("This customer requirement does not exist", gr);
            }
            else
            {
                DS.DataSource.AllGuestRequestsList.Remove(grOriginal);
                DS.DataSource.AllGuestRequestsList.Add(gr.Clone());
                return gr;
            }
        }


        //Add, Delete and Update for HostingUnit         
        public HostingUnit AddHostingUnit(HostingUnit hu)
        {
            if (DS.DataSource.AllHostingUnitsList.Count != 0 && DS.DataSource.AllHostingUnitsList.Exists(item => (hu != null && hu.Owner.MailAddress == item.Owner.MailAddress && hu.HostingUnitName == item.HostingUnitName)))
                throw new MyException("This hosting unit is already exist");
            else
            {
                hu.HostingUnitKey = Configuration.getRunningNumberForHostingUnit();
                if (hu.Owner.HostKey == 0)
                    hu.Owner.HostKey = Configuration.getRunningNumberForHost();
                DS.DataSource.AllHostingUnitsList.Add(hu.Clone());
                return hu;//Clone();
            }
        }
        public bool DeleteHostingUnit(HostingUnit hu)
        {
            HostingUnit item = DS.DataSource.AllHostingUnitsList.FirstOrDefault(x => x.HostingUnitKey == hu.HostingUnitKey);
            if (item == null)
                throw new MyException("This hosting unit does not exist");
            else
            {
                DS.DataSource.AllHostingUnitsList.Remove(item);
                return true;
            }
        }
        public HostingUnit UpdateHostingUnit(HostingUnit hu)
        {
            HostingUnit item = DS.DataSource.AllHostingUnitsList.Find(x => x.HostingUnitKey == hu.HostingUnitKey);
            if (item == null)
            {
                hu = AddHostingUnit(hu);
                throw new MyException("This hosting unit does not exist", hu);
            }
            else
            {
                DS.DataSource.AllHostingUnitsList.Remove(item);
                DS.DataSource.AllHostingUnitsList.Add(hu);
                return hu;
            }
        }


        //Add and Update for Order        
        public Order AddOrder(Order or)
        {

            if (DS.DataSource.AllOrdersList.Count != 0)
            {
                var oldo = (from Order item in DS.DataSource.AllOrdersList
                            where (item.GuestRequestKey == or.GuestRequestKey) && (item.HostingUnitKey == or.HostingUnitKey)
                            select item).FirstOrDefault();
                if (oldo != null)
                    throw new MyException("This order is already exist");
                else
                {
                    or.OrderKey = Configuration.getRunningNumberForOrder();
                    DS.DataSource.AllOrdersList.Add(or.Clone());
                    return or;
                }

            }
            else
            {
                or.OrderKey = Configuration.getRunningNumberForOrder();
                DS.DataSource.AllOrdersList.Add(or.Clone());
                return or;
            }
        }
        public Order UpdateOrder(Order or)
        {
            Order item = DS.DataSource.AllOrdersList.Find(x => x.OrderKey == or.OrderKey);
            if (item == null)
            {
                if (or.OrderKey == 0)
                    or.OrderKey = Configuration.getRunningNumberForOrder();
                DS.DataSource.AllOrdersList.Add(or.Clone());
                throw new MyException("This order does not exist", or);
            }
            else
            {
                DS.DataSource.AllOrdersList.Remove(item);
                DS.DataSource.AllOrdersList.Add(or);
                return or;
            }
        }


        //***Bringing data lists from dataSource***//
        public List<HostingUnit> AllHostingUnits()
        {
            var list = (from item in DataSource.AllHostingUnitsList
                        select item.Clone()).ToList();
            //return (list == null) ? throw new MyException("There is no Hosting units!") : list;   
            return list;
        }
        public List<GuestRequest> AllGuestRequests()
        {
            var list = (from item in DataSource.AllGuestRequestsList
                        select item.Clone()).ToList();
            //return (list == null) ? throw new MyException("There is no guest requests!") : list;
            return list;
        }
        public List<Order> AllOrders()
        {
            var list = (from item in DataSource.AllOrdersList
                        select item.Clone()).ToList();
            //return (list == null) ? throw new MyException("There is no orders!") : list;
            return list;
        }
        public List<BankBranch> AllBankBranch()
        {
            List<HostingUnit> ls = AllHostingUnits();
            var list = (from HostingUnit item in ls
                        let hostItem = item.Owner
                        select hostItem.BankBranchDetails.Clone()).Distinct().ToList();
            return (list.Count == 0) ? throw new MyException("There is no bank branches!") : list;
        }
        public List<Host> AllHosts()
        {
            var list = (from HostingUnit hu in DS.DataSource.AllHostingUnitsList
                        select hu.Owner.Clone()).Distinct().ToList();
            return (list.Count == 0) ? throw new MyException("There is no Hosts!") : list;
        }


        //***Additional fuctions***//
        public GuestRequest GetGuestRequest(int key)
        {
            GuestRequest gr = DataSource.AllGuestRequestsList.FirstOrDefault(item => item.GuestRequestKey == key);
            return gr == null ? null : gr.Clone();
        }
        public HostingUnit GetHostingUnit(int key)
        {
            HostingUnit hu = DataSource.AllHostingUnitsList.FirstOrDefault(item => item.HostingUnitKey == key);
            return hu == null ? null : hu.Clone();
        }
        public Order GetOrder(int key)
        {
            var or = (from Order item in AllOrders()
                      let Okey = item.OrderKey
                      where Okey == key
                      select new { ThisOrder = item.Clone() }).FirstOrDefault();

            return or == null ? null : or.ThisOrder;
        }
        public List<Order> CustomerReqOrder(int key)
        {
            var list = (from Order item in DS.DataSource.AllOrdersList
                        where item.GuestRequestKey == key
                        select item.Clone()).ToList();
            return (list.Count == 0) ? null : list;
            //throw new MyException("There is no customer requests for this order!")
        }
        public List<Order> HostingUnitOrder(int key)
        {
            var list = (from Order item in DS.DataSource.AllOrdersList
                        where item.HostingUnitKey == key
                        select item.Clone()).ToList();
            return (list.Count == 0) ? null : list;
            //throw new MyException("There is no Hosting units for this order!")
        }

        public int GetNumDaysExpire()
        {
            throw new NotImplementedException();
        }

        public DateTime GetLastDate()
        {
            throw new NotImplementedException();
        }
    }
}
