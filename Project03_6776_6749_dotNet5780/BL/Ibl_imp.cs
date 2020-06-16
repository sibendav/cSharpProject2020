using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BE;
using DAL;
using System.Net.Mail;
using System.Threading;

namespace BL
{
    public class Ibl_imp : IBL
    {
        private static Order thisOrder;        

        public Ibl_imp()
        {
            Thread t = new Thread(closeOrder);
            t.Start();
        }

        //***Impliment of the same function in the iDal interface***//
        #region
        public GuestRequest AddCustomerReq(GuestRequest gr)
        {
            try
            {
                if ((gr.ReleaseDate - gr.EntryDate).TotalDays >= 1)
                {
                    return DalFactory.getDal().AddCustomerReq(gr.Clone());
                }
                else
                    throw new MyException("The entry date and the release date are not valid!");
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message); //check this carefully
            }
        }
        public GuestRequest UpdateAllPasswordCustomerReq(GuestRequest gr)
        {
            try
            {
                var list = (from item in AllGuestRequests()
                            where item.MailAddress == gr.MailAddress
                            select item);
                foreach (GuestRequest gr1 in list)
                {
                    gr1.Password = gr.Password;
                    gr = DAL.DalFactory.getDal().UpdateCustomerReq(gr1);
                }
                return gr;
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message, (GuestRequest)ex.item);
            }
        }
        public GuestRequest UpdateCustomerReq(GuestRequest gr)
        {
            try
            {
                return DAL.DalFactory.getDal().UpdateCustomerReq(gr);
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message, (GuestRequest)ex.item);
            }
        }

        public HostingUnit AddHostingUnit(HostingUnit hu)
        {
            try
            {
                return DAL.DalFactory.getDal().AddHostingUnit(hu);
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message);
            }
        }
        public bool DeleteHostingUnit(HostingUnit hu)
        {
            try
            {
                List<Order> newList = DAL.DalFactory.getDal().HostingUnitOrder(hu.HostingUnitKey);
                if (newList == null || !(newList.Any(item => item.Status == OrderStatus.SentEmail || item.Status == OrderStatus.NotAddressed)))
                    return DAL.DalFactory.getDal().DeleteHostingUnit(hu);
                else
                    throw new MyException("This hosting unit has open order. can't be removed!");
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message);
            }
        }
        public HostingUnit UpdateHostingUnit(HostingUnit hu)
        {
            try
            {
                HostingUnit originalHu = DAL.DalFactory.getDal().GetHostingUnit(hu.HostingUnitKey);
                if (originalHu != null && originalHu.Owner.CollectionClearance == true && hu.Owner.CollectionClearance == false)
                {
                    List<Order> allThisOrders = DAL.DalFactory.getDal().HostingUnitOrder(hu.HostingUnitKey);
                    foreach (Order item in allThisOrders)
                    {
                        if (item.Status == OrderStatus.SentEmail || item.Status == OrderStatus.NotAddressed)
                            throw new MyException("This host Has open order. you can not change the collection clearance!");
                    }
                }
                return DAL.DalFactory.getDal().UpdateHostingUnit(hu);
            }
            catch (MyException ex)
            {
                if (ex.item == null)
                    throw new MyException(ex.Message);
                throw new MyException(ex.Message, (HostingUnit)ex.item);
            }
        }

        public Order AddOrder(Order or) //I fixed it :) 
        {
            try
            {
                GuestRequest gr = DAL.DalFactory.getDal().GetGuestRequest(or.GuestRequestKey);
                HostingUnit hu = DAL.DalFactory.getDal().GetHostingUnit(or.HostingUnitKey);
                if (gr != null && hu != null)
                {
                    bool degel = true;
                    for (DateTime i = gr.EntryDate; i != gr.ReleaseDate; i = i.AddDays(1))
                    {
                        if (hu.Diary[(i.Month - 1), (i.Day - 1)] == true)
                            degel = false;
                    }
                    if (degel == true)
                    {
                        for (DateTime i = gr.EntryDate; i != gr.ReleaseDate; i = i.AddDays(1))
                            hu.Diary[i.Month - 1, i.Day - 1] = true;
                        DalFactory.getDal().UpdateHostingUnit(hu);
                        or.CreateDate = DateTime.Today;
                        return DalFactory.getDal().AddOrder(or);
                    }
                    else
                        throw new MyException("This hosting unit isn't aviable in this dates period!");
                }
                else
                    throw new MyException("The guest request or the hosting unit doesn't exist!");
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message);
            }
        }
        public Order UpdateOrder(Order or)
        //check if the update is ok or maybe we should add another update function
        {
            try
            {
                Order orOriginal = DAL.DalFactory.getDal().GetOrder(or.OrderKey);
                GuestRequest gr = DAL.DalFactory.getDal().GetGuestRequest(or.GuestRequestKey);
                HostingUnit hu = DAL.DalFactory.getDal().GetHostingUnit(or.HostingUnitKey);
                if ((orOriginal != null && gr != null && hu != null))
                {
                    if (orOriginal.Status == OrderStatus.ClosedForCustomerResponse || orOriginal.Status == OrderStatus.ClosedForCustomerUnresponsiveness)
                        throw new MyException("This order is closed for changes!");
                    else if ((or.Status == OrderStatus.SentEmail || or.Status == OrderStatus.ClosedForCustomerResponse) && orOriginal.Status != OrderStatus.ClosedForCustomerResponse && orOriginal.Status != OrderStatus.ClosedForCustomerUnresponsiveness)
                    {

                        if (or.Status == OrderStatus.ClosedForCustomerResponse)
                        {
                            //***updating all the other linked orders***//
                            List<Order> newList = DAL.DalFactory.getDal().CustomerReqOrder(or.GuestRequestKey);
                            foreach (Order item in newList)
                            {
                                item.Status = OrderStatus.ClosedForCustomerUnresponsiveness;
                                DAL.DalFactory.getDal().UpdateOrder(item.Clone());
                            }

                            //***fee calculation***//
                            or.OrderFee = ((gr.ReleaseDate - gr.EntryDate).TotalDays) * Configuration.fee;

                            //***updating the diary***//
                            for (DateTime i = gr.EntryDate; i != gr.ReleaseDate; i = i.AddDays(1))
                            {
                                hu.Diary[i.Month - 1, i.Day - 1] = true;
                            }
                            return DAL.DalFactory.getDal().UpdateOrder(or.Clone());
                        }
                        else if (or.Status == OrderStatus.SentEmail && hu.Owner.CollectionClearance == true)
                        {
                            //or.Status = OrderStatus.SentEmail;
                            or.OrderDate = DateTime.Today;
                            or = DAL.DalFactory.getDal().UpdateOrder(or.Clone());
                            GuestRequest gr1 = DAL.DalFactory.getDal().GetGuestRequest(or.GuestRequestKey);
                            if (gr1 != null)
                            {
                                gr1.Status = true;
                                DAL.DalFactory.getDal().UpdateCustomerReq(gr1);
                                thisOrder = or;
                                Thread t = new Thread(SendMail);
                                t.Start();
                            }
                            throw new MyException("You have sent an Email to this customer!", or);
                        }
                        else
                            throw new MyException("You have not erranged the collection clearance with the bank - you can not send an email!");

                    }
                    else
                        return DAL.DalFactory.getDal().UpdateOrder(or);
                }
                else
                {
                    //DAL.DalFactory.getDal().UpdateCustomerReq(gr);
                    //DAL.DalFactory.getDal().UpdateHostingUnit(hu);
                    return DAL.DalFactory.getDal().UpdateOrder(or);
                }
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message, (Order)ex.item);
            }
        }
        #endregion

        //***list returns functions***//
        #region
        public List<HostingUnit> AllHostingUnits()
        {
            try
            {
                return DAL.DalFactory.getDal().AllHostingUnits();
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message);
            }
        }
        public List<GuestRequest> AllGuestRequests()
        {
            try
            {
                return DAL.DalFactory.getDal().AllGuestRequests();
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message);
            }
        }
        public List<Order> AllOrders()
        {
            try
            {
                return DAL.DalFactory.getDal().AllOrders();
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message);
            }

        }
        public List<BankBranch> AllBankBranch()
        {
            try
            {
                return DAL.DalFactory.getDal().AllBankBranch();
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message);
            }
        }
        #endregion

        //***Special functions for the ibl interface***//
        #region
        private static void SendMail()
        {
            HostingUnit hu;
            GuestRequest gr;
            try
            {
                hu = DAL.DalFactory.getDal().GetHostingUnit(thisOrder.HostingUnitKey);
                gr = DAL.DalFactory.getDal().GetGuestRequest(thisOrder.GuestRequestKey);
            }
            catch (MyException e)
            {
                throw e;
            }
            bool f = false;
            while (!f)
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(gr.MailAddress);
                mail.From = new MailAddress(hu.Owner.MailAddress);
                mail.Subject = "We Have An Offer For You!!!";
                mail.Body = "Hey " + gr.PrivateName + " " + gr.FamilyName + "\n " + hu.HostingUnitName + " We thrilled to visit you in our " + hu.Type + "\n If you are intrested, Please send us an email \n See you soon";
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Credentials = new System.Net.NetworkCredential(hu.Owner.MailAddress, hu.Owner.Password);
                smtp.EnableSsl = true;
                try
                {
                    smtp.Send(mail);
                }
                catch
                {
                }
            }
        }

        public List<HostingUnit> FindAllThePriviaseHU(Host h)
        {
            try
            {
                return AllHostingUnitOfTheHost(h.HostKey);
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message);
            }
        }
        public List<HostingUnit> AvailableHostingUnit(DateTime begining, int numDays)
        {
            try
            {
                DateTime endingDate = begining.AddDays(numDays);
                List<HostingUnit> newlist = (from HostingUnit item in AllHostingUnits()
                                             where IsHostingUnitAvailableInThisDates(item, begining, endingDate) == true
                                             select item.Clone()).ToList();
                return newlist.Count == 0 ? throw new MyException("There is no avilable hosting units for this dates - sory!") : newlist;
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message);
            }
        } //never used
        public int PassedDays(params DateTime[] list)
        {
            if (list.Count() == 1)
                return (int)(DateTime.Today - list[0]).TotalDays;
            else if (list.Count() == 2)
                return (int)(list[1] - list[0]).TotalDays;
            else
                return 0;
        }
        public List<Order> AllExpiredOrders(int numDays)
        {
            //To check if the defualt datetime value is today's date
            try
            {
                List<Order> newList = (from Order item in AllOrders()
                                       where (item.Status == OrderStatus.SentEmail && (DateTime.Today - item.OrderDate).TotalDays >= numDays)
                                       || (item.Status == OrderStatus.NotAddressed && (DateTime.Today - item.CreateDate).TotalDays >= numDays)
                                       select item.Clone()).ToList();
                return newList.Count == 0 ? null : newList;
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message);
            }
        }
        public List<GuestRequest> GuestRequestsRequirment(Func<GuestRequest, bool> myRequirments) //delegate
        {
            try
            {
                List<GuestRequest> list = AllGuestRequests();
                List<GuestRequest> newList = (from GuestRequest item in list
                                              where myRequirments(item) == true
                                              select item).ToList();
                return newList.Count == 0 ? throw new MyException("There is no suitable guestRequest as you wished") : newList;
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message);
            }
        }
        public int NumOfSendingOrders(GuestRequest gr)
        {
            try
            {
                int mone = 0;
                List<Order> allor = AllOrders();
                foreach (Order item in allor)
                {
                    if (item.GuestRequestKey == gr.GuestRequestKey)
                        mone++;
                }
                return mone == 0 ? throw new MyException("There is no orders for this guest request") : mone;
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message);
            }
        }
        public int NumOfSendingOrdersAndClosedDeals(HostingUnit hu)
        {
            try
            {
                List<Order> newList = AllOrders();
                List<Order> allor = (from Order item in newList
                                     where item.HostingUnitKey == hu.HostingUnitKey
                                     && (item.Status == OrderStatus.ClosedForCustomerResponse
                                     || item.Status == OrderStatus.SentEmail)
                                     select item).ToList();
                return allor.Count() == 0 ? throw new MyException("This hosting unit has no sending orders or closed deals!") : allor.Count();
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message);
            }
        }
        #endregion

        //***grouping functions***//
        #region
        public IEnumerable<IGrouping<Area, GuestRequest>> GroupGuestReqByArea()
        {
            try
            {
                return from item in AllGuestRequests()
                       group item by item.Area into gr
                       select gr;
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message);
            }
        }
        public IEnumerable<IGrouping<int, GuestRequest>> GroupGuestReqBynumVacationers()
        {
            try
            {
                return (from GuestRequest item in AllGuestRequests()
                        let numVacationers = item.Adults + item.Children
                        group item by numVacationers into gr
                        select gr);
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message);
            }
        }
        public IEnumerable<IGrouping<int, Host>> GroupHostByNumHostingUnits()
        {
            try
            {
                return from Host item in DAL.DalFactory.getDal().AllHosts()
                       let numHostingUnit = AllHostingUnitOfTheHost(item.HostKey).Count()
                       group item by numHostingUnit into ho
                       select ho;
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message);
            }
        }
        public IEnumerable<IGrouping<Area, HostingUnit>> GroupHostingUnitsByArea()
        {
            try
            {
                return from HostingUnit item in AllHostingUnits()
                       group item by item.Area into hu
                       select hu;
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message);
            }
        }
        #endregion

        //***Inner functions***//
        #region
        private void closeOrder()
        {
            DateTime lastD = DAL.DalFactory.getDal().GetLastDate();
            if (lastD!=DateTime.Today)
            {
                int days = DAL.DalFactory.getDal().GetNumDaysExpire();
                var list = AllExpiredOrders(days);
                if (list != null)
                {
                    foreach(Order item in list)
                    {
                        item.Status = OrderStatus.ClosedForCustomerUnresponsiveness;
                        UpdateOrder(item);
                    }
                }
            }
        }
        private List<HostingUnit> AllHostingUnitOfTheHost(int keyHost)
        {
            try
            {
                return (from HostingUnit item in AllHostingUnits()
                        where item.Owner.HostKey == keyHost
                        select item).ToList();
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message);
            }
        }
        private bool IsHostingUnitAvailableInThisDates(HostingUnit hu, DateTime d1, DateTime d2)
        {
            bool degel = true;
            for (DateTime i = d1; i <= d2; i = i.AddDays(1))
            {
                if (hu.Diary[i.Month - 1, i.Day - 1] == true) //if the day is busy
                    degel = false;
            }
            return degel;
        }
        private static bool IsTheSameRequirments(HostingUnit hu, GuestRequest gr)
        {
            bool degel = true;
            if (hu == null || gr == null
                || gr.Pool == Extra.Necessary && !hu.Pool
                || gr.Jacuzzi == Extra.Necessary && !hu.Jacuzzi
                || gr.Garden == Extra.Necessary && !hu.Garden
                || gr.ChildrensAttractions == Extra.Necessary && !hu.ChildrensAttractions
                || gr.Area != hu.Area
                || gr.Type != hu.Type
                || gr.Adults > hu.Adults
                || gr.Children > hu.Children)
                degel = false;
            return degel;

        }
        #endregion

        //***Creative functions***//
        #region
        public List<GuestRequest> FindTheRelevanteGuestRequermentsHostingUnit(HostingUnit gr)
        {
            try
            {
                var list = new List<GuestRequest>();
                foreach (GuestRequest item in AllGuestRequests())
                {
                    if (IsTheSameRequirments(gr, item))
                        list.Add(item);
                }
                //return (from item in AllGuestRequests()
                //        let degel = Ibl_imp.IsTheSameRequirments(gr, item)
                //        where degel == true
                //        select item.Clone()).ToList();
                return list;
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message);
            }
        }
        public List<GuestRequest> FindAllThePriviaseGR(GuestRequest gr)
        {
            try
            {
                var list = (from GuestRequest item in AllGuestRequests()
                            where item.MailAddress == gr.MailAddress
                            select item.Clone()).ToList();
                return list;
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message);
            }
        }
        public List<Order> FindAllThePriviaseOrdersForGR(GuestRequest gr)
        {
            try
            {
                var list = (from Order item in AllOrders()
                            where item.GuestRequestKey == gr.GuestRequestKey
                            select item.Clone()).ToList();
                return list;
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message);
            }
        }
        public List<Order> FindAllThePriviaseOrdersForHU(HostingUnit hu)
        {
            try
            {
                if (hu != null)
                {
                    var list = (from Order item in AllOrders()
                                where item.HostingUnitKey == hu.HostingUnitKey
                                select item.Clone()).ToList();
                    return list;
                }
                return null;
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message);
            }
        }
        public GuestRequest AddTheSameGuestRequest(GuestRequest gr)
        {
            try
            {
                GuestRequest newGR = gr.Clone();
                AddCustomerReq(newGR);
                return newGR;
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message);
            }
        }
        public List<HostingUnit> AvailableHostinngUnitInTheRequiredPeriod(DateTime d1, DateTime d2)
        {
            try
            {
                return (from HostingUnit item in AllHostingUnits()
                        let itemDegel = IsHostingUnitAvailableInThisDates(item, d1, d2)
                        where itemDegel == true
                        select item.Clone()).ToList();
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message);
            }
        }
        public double ThePercentOfCapacity(HostingUnit hu)
        {
            double mone = 0;
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 31; j++)
                {
                    if (hu.Diary[i, j] == true)
                        mone++;
                }
            }
            return (mone / 365) * 100;
        }
        public GuestRequest getGRByUserName(string un)
        {
            return (from item in AllGuestRequests()
                    where item.UserName == un
                    select item.Clone()).FirstOrDefault();
        }
        public Host getHostByUserName(string un)
        {
            return (from HostingUnit item in AllHostingUnits()
                    where item.Owner.UserName == un
                    select item.Owner.Clone()).FirstOrDefault();
        }
        #endregion
    }
}
