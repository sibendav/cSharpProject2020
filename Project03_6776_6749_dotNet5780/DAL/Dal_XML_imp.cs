using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using BE;
using System.Threading;
using System.Xml.Serialization;
using System.Net;

namespace DAL
{
    public class Dal_XML_imp : Idal
    {
        private XElement banksRoot;
        private XElement ConfigRoot;
        private XElement HostRoot;
        private XElement OrderRoot;
        private XElement GuestRequestRoot;
        static readonly string ProjectPath = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory.ToString()).FullName).FullName;
        private const string BanksRootPath = @"D:\LUSTIG\SECONDYEAR\FIRSTSEMESTER\מיני פרויקט במערכת חלונות\תרגילי בית\פרוייקט חלק 3\new\Project03_6776_6749_dotNet5780\Banks.xml";
        private const string ConfigRootPath = @"D:\LUSTIG\SECONDYEAR\FIRSTSEMESTER\מיני פרויקט במערכת חלונות\תרגילי בית\פרוייקט חלק 3\new\Project03_6776_6749_dotNet5780\Configuration.xml";
        private const string HostRootPath = @"D:\LUSTIG\SECONDYEAR\FIRSTSEMESTER\מיני פרויקט במערכת חלונות\תרגילי בית\פרוייקט חלק 3\new\Project03_6776_6749_dotNet5780\HostingUnits.xml";
        private const string OrderRootPath = @"D:\LUSTIG\SECONDYEAR\FIRSTSEMESTER\מיני פרויקט במערכת חלונות\תרגילי בית\פרוייקט חלק 3\new\Project03_6776_6749_dotNet5780\Order.xml";
        private const string GuestRequestRootPath = @"D:\LUSTIG\SECONDYEAR\FIRSTSEMESTER\מיני פרויקט במערכת חלונות\תרגילי בית\פרוייקט חלק 3\new\Project03_6776_6749_dotNet5780\GuestRequests.xml";
        private string filesWithWrongStruct = "";       

        public Dal_XML_imp()
        {
            try
            {
                Load(ref banksRoot, BanksRootPath);
            }
            catch 
            {
                filesWithWrongStruct = "Banks.xml";
                banksRoot = new XElement("Banks");
            }
            try
            {
                Thread t1 = new Thread(DownloadBanks);
                t1.Start();
            }
            catch
            {
                filesWithWrongStruct = "Banks.xml";
                banksRoot = new XElement("Banks");
            }
            try
            {

                Load(ref ConfigRoot, ConfigRootPath);
            }
            catch
            {
                filesWithWrongStruct = "Configuration.xml";
                ConfigRoot = new XElement("Configuration");
            }
            try
            {
                Load(ref HostRoot, HostRootPath);
            }
            catch
            {
                filesWithWrongStruct += "HostingUnits.xml\n";
                HostRoot = new XElement("HostingUnits");
            }
            try
            {
                Load(ref OrderRoot, OrderRootPath);
            }
            catch
            {
                filesWithWrongStruct += "Orders.xml\n";
                OrderRoot = new XElement("Orders");
            }
            try
            {
                Load(ref GuestRequestRoot, GuestRequestRootPath);
            }
            catch
            {
                filesWithWrongStruct += "GuestRequests.xml\n";
                ConfigRoot = new XElement("GuestRequests");
            }
        }

        #region threadFunc
        private static void DownloadBanks()
        {
            WebClient wc = new WebClient();
            try
            {
                bool i = true;
                while (i)
                {
                    try
                    {
                        string xmlServerPath = @"https://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml";
                        //string xmlServerPath = @"https://drive.google.com/file/d/1FpcqslnRD6naLHOjrCvKArCg3Ihkb9hR/view?usp= sharing";
                        wc.DownloadFile(xmlServerPath, BanksRootPath);
                        i = false;
                    }
                    catch
                    {
                        Thread.Sleep(500);
                    }

                }
            }
            finally
            {
                wc.Dispose();
            }
        }
        #endregion

        #region AllTheAdding
        public GuestRequest AddCustomerReq(GuestRequest gr)
        {
            try
            {
                Load(ref GuestRequestRoot, GuestRequestRootPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }
            if (gr != null)
            {
                var it = (from item in GuestRequestRoot.Elements()
                          where item.Element("MailAddress").Value == gr.MailAddress &&
                                Convert.ToDateTime(item.Element("EntryDate").Value)==gr.EntryDate &&
                                Convert.ToDateTime(item.Element("ReleaseDate").Value)==gr.ReleaseDate
                          select item).FirstOrDefault();
                if (it != null)
                    throw new MyException("This guest request is already exists! ");
                gr.GuestRequestKey = GetRunningNumForGR();
                GuestRequestRoot.Add(GRCreatorToXML(gr));
                GuestRequestRoot.Save(GuestRequestRootPath);
                return gr;
            }
            else
                return null;
        }

        public HostingUnit AddHostingUnit(HostingUnit hu)
        {
            try
            {
                Load(ref HostRoot, HostRootPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }
            var it = (from item in HostRoot.Elements()
                      where (item.Element("Owner").Element("MailAddress").Value == hu.Owner.MailAddress)
                      && (item.Element("HostingUnitName").Value == hu.HostingUnitName)
                      select item).FirstOrDefault();
            if (it != null)
                throw new MyException("This hosting unit is already exist");
            if (hu.Owner.HostKey == 0)
                hu.Owner.HostKey = GetRunningNumForHO();
            hu.HostingUnitKey = GetRunningNumForHU();
            HostRoot.Add(HUCreatorToXML(hu));
            
            HostRoot.Save(HostRootPath);
            return hu;
        }

        public Order AddOrder(Order or)
        {
            try
            {
                Load(ref OrderRoot, OrderRootPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }
            var it = (from item in OrderRoot.Elements()
                      where (item.Element("GuestRequestKey").Value == Convert.ToString(or.GuestRequestKey))
                      && (item.Element("HostingUnitKey").Value == Convert.ToString(or.HostingUnitKey))
                      && (item.Element("CreateDate").Value == Convert.ToString(or.CreateDate))
                      select item).FirstOrDefault();
            if (it != null)
                throw new MyException("This order is already exist");

            or.OrderKey = GetRunningNumForOR();
            OrderRoot.Add(OrCreatorToXML(or));
            
            OrderRoot.Save(OrderRootPath);
            return or;
        }
        #endregion

        #region Bringing Data
        public List<BankBranch> AllBankBranch()
        {
            bool i = false;
            while (!i)
            {
                try
                {
                    Load(ref banksRoot, BanksRootPath);
                    i = true;
                }
                catch (DirectoryNotFoundException r)
                {
                    Thread.Sleep(800);
                }
            }
            return (from item in banksRoot.Elements()
                    select new BankBranch()
                    {
                        BankNumber = Convert.ToInt32(item.Element("קוד_בנק").Value),
                        BankName = Convert.ToString(item.Element("שם_בנק").Value),
                        BranchNumber = Convert.ToInt32(item.Element("קוד_סניף").Value),
                        BranchAddress = Convert.ToString(item.Element("כתובת_ה-ATM").Value),
                        BranchCity = Convert.ToString(item.Element("ישוב").Value)
                    }
                        ).Distinct().ToList();
        }

        public List<GuestRequest> AllGuestRequests()
        {
            try
            {
                Load(ref GuestRequestRoot, GuestRequestRootPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }
            return (from item in GuestRequestRoot.Elements()
                    select new GuestRequest()
                    {
                        GuestRequestKey=Convert.ToInt32(item.Element("GuestRequestKey").Value),
                        PrivateName = item.Element("PrivateName").Value,
                        FamilyName = item.Element("FamilyName").Value,
                        MailAddress = item.Element("MailAddress").Value,
                        Status = Convert.ToBoolean(item.Element("Status").Value),
                        RegistrationDate = Convert.ToDateTime(item.Element("RegistrationDate").Value),
                        EntryDate = Convert.ToDateTime(item.Element("EntryDate").Value),
                        ReleaseDate = Convert.ToDateTime(item.Element("ReleaseDate").Value),
                        Area = (Area)Enum.Parse(typeof(Area),item.Element("Area").Value),
                        SubArea = item.Element("SubArea").Value,
                        Type = (ResortType)Enum.Parse(typeof(ResortType),item.Element("Type").Value),
                        Adults = Convert.ToInt32(item.Element("Adults").Value),
                        Children = Convert.ToInt32(item.Element("Children").Value),
                        Pool = (Extra)Enum.Parse(typeof(Extra), item.Element("Pool").Value),
                        Jacuzzi = (Extra)Enum.Parse(typeof(Extra), item.Element("Jacuzzi").Value),
                        Garden = (Extra)Enum.Parse(typeof(Extra), item.Element("Garden").Value),
                        ChildrensAttractions = (Extra)Enum.Parse(typeof(Extra), item.Element("ChildrensAttractions").Value),
                        UserName = item.Element("UserName").Value,
                        Password = item.Element("Password").Value
                    }
                    ).ToList();
        }

        public List<HostingUnit> AllHostingUnits()
        {
            try
            {
                Load(ref HostRoot, HostRootPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }
            return (from item in HostRoot.Elements()
                    select new HostingUnit()
                    {
                        HostingUnitKey = Convert.ToInt32(item.Element("HostingUnitKey").Value),
                        Owner = new Host()
                        {
                            HostKey = Convert.ToInt32(item.Element("Owner").Element("HostKey").Value),
                            PrivateName = item.Element("Owner").Element("PrivateName").Value,
                            FamilyName = item.Element("Owner").Element("FamilyName").Value,
                            PhoneNumber = Convert.ToInt32(item.Element("Owner").Element("PhoneNumber").Value),
                            MailAddress = item.Element("Owner").Element("MailAddress").Value,
                            BankBranchDetails = new BankBranch()
                            {
                                BankNumber = Convert.ToInt32(item.Element("Owner").Element("BankBranchDetails").Element("BankNumber").Value),
                                BankName = item.Element("Owner").Element("BankBranchDetails").Element("BankName").Value,
                                BranchNumber = Convert.ToInt32(item.Element("Owner").Element("BankBranchDetails").Element("BranchNumber").Value),
                                BranchAddress = item.Element("Owner").Element("BankBranchDetails").Element("BranchAddress").Value,
                                BranchCity = item.Element("Owner").Element("BankBranchDetails").Element("BranchCity").Value
                            },
                            BankAccountNumber = Convert.ToInt32(item.Element("Owner").Element("BankAccountNumber").Value),
                            CollectionClearance = Convert.ToBoolean(item.Element("Owner").Element("CollectionClearance").Value),
                            UserName = item.Element("Owner").Element("UserName").Value,
                            Password = item.Element("Owner").Element("Password").Value
                        },
                        HostingUnitName = item.Element("HostingUnitName").Value,
                        diaryXML = getArr(item.Element("Diary").Value),
                        Area = (Area)Enum.Parse(typeof(Area), item.Element("Area").Value),
                        Type = (ResortType)Enum.Parse(typeof(ResortType), item.Element("Type").Value),
                        Adults = Convert.ToInt32(item.Element("Adults").Value),
                        Children = Convert.ToInt32(item.Element("Children").Value),
                        Pool = Convert.ToBoolean(item.Element("Pool").Value),
                        Jacuzzi = Convert.ToBoolean(item.Element("Jacuzzi").Value),
                        Garden = Convert.ToBoolean(item.Element("Garden").Value),
                        ChildrensAttractions = Convert.ToBoolean(item.Element("ChildrensAttractions").Value)
                    }).ToList();
            
        }

        private static bool[] getArr(string t)
        {
            List<bool> arr = new List<bool>();
            while(t!="")
            {
                if(t[0]=='t')
                {
                    arr.Add(true);
                    t=t.Substring(4);
                }
                if (t[0]=='f')
                {
                    arr.Add(false);
                    t=t.Substring(5);
                }
            }
            return arr.ToArray();
        }

        public List<Host> AllHosts()
        {
            var list = (from HostingUnit hu in AllHostingUnits()
                        select hu.Owner.Clone()).Distinct().ToList();
            return (list.Count == 0) ? throw new MyException("There is no Hosts!") : list;
        }

        public List<Order> AllOrders()
        {
            try
            {
                Load(ref OrderRoot, OrderRootPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }
            return (from item in OrderRoot.Elements()
                    select new Order()
                    {
                        HostingUnitKey=Convert.ToInt32(item.Element("HostingUnitKey").Value),
                        GuestRequestKey = Convert.ToInt32(item.Element("GuestRequestKey").Value),
                        OrderKey = Convert.ToInt32(item.Element("OrderKey").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus) ,item.Element("Status").Value),
                        CreateDate = Convert.ToDateTime(item.Element("CreateDate").Value),
                        OrderDate = Convert.ToDateTime(item.Element("OrderDate").Value),
                        OrderFee = Convert.ToDouble(item.Element("OrderFee").Value)
                    }).ToList();
        }

        public List<Order> CustomerReqOrder(int key)
        {
            var list = (from Order item in AllOrders()
                        where item.GuestRequestKey == key
                        select item.Clone()).ToList();
            return (list.Count == 0) ? null : list;
        }

        public bool DeleteHostingUnit(HostingUnit hu)
        {
            try
            {
                Load(ref HostRoot, HostRootPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }
            if (hu != null)
            {
                XElement original = (from item in HostRoot.Elements()
                                     where item.Element("HostingUnitKey").Value == Convert.ToString(hu.HostingUnitKey)
                                     select item).FirstOrDefault();
                if (original == null)
                    throw new MyException("This hosting unit does not exist");
                original.Remove();
                HostRoot.Save(HostRootPath);
                return true;
            }
            else
                return false;
        }

        public GuestRequest GetGuestRequest(int key)
        {
            GuestRequest gr = AllGuestRequests().FirstOrDefault(item => item.GuestRequestKey == key);
            return gr == null ? null : gr.Clone();
        }

        public HostingUnit GetHostingUnit(int key)
        {
            HostingUnit hu =AllHostingUnits().FirstOrDefault(item => item.HostingUnitKey == key);
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

        public List<Order> HostingUnitOrder(int key)
        {
            var list = (from Order item in AllOrders()
                        where item.HostingUnitKey == key
                        select item.Clone()).ToList();
            return (list.Count == 0) ? null : list;
        }
        #endregion

        #region AllTheUpdates
        public GuestRequest UpdateCustomerReq(GuestRequest gr)
        {
            try
            {
                Load(ref GuestRequestRoot, GuestRequestRootPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }
            XElement original = (from item in GuestRequestRoot.Elements()
                                where item.Element("GuestRequestKey").Value == Convert.ToString(gr.GuestRequestKey)
                                select item).FirstOrDefault();
            if (original == null)
            {
                gr = AddCustomerReq(gr);
                throw new MyException("This customer requirement does not exist", gr);
            }
            original.Remove();
            GuestRequestRoot.Add(GRCreatorToXML(gr));
            GuestRequestRoot.Save(GuestRequestRootPath);
            return gr;
        }

        public HostingUnit UpdateHostingUnit(HostingUnit hu)
        {
            try
            {
                Load(ref HostRoot, HostRootPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }
            XElement original = (from item in HostRoot.Elements()
                                 where item.Element("HostingUnitKey").Value == Convert.ToString(hu.HostingUnitKey)
                                 select item).FirstOrDefault();
            if (original == null)
            {
                hu = AddHostingUnit(hu);
                throw new MyException("This hosting unit does not exist", hu);
            }
            original.Remove();
            HostRoot.Add(HUCreatorToXML(hu));
            HostRoot.Save(HostRootPath);
            return hu;
        }

        public Order UpdateOrder(Order or)
        {
            try
            {
                Load(ref OrderRoot, OrderRootPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }
            XElement original = (from item in OrderRoot.Elements()
                                 where item.Element("OrderKey").Value == Convert.ToString(or.OrderKey)
                                 select item).FirstOrDefault();
            if (original == null)
            {
                or = AddOrder(or);
                throw new MyException("This order does not exist", or);
            }
            original.Remove();
            OrderRoot.Add(OrCreatorToXML(or));
            OrderRoot.Save(OrderRootPath);
            return or;
        }
        #endregion

        #region *** Additional Function ***
        private void Load(ref XElement t, string a)
        {
            try
            {
                t = XElement.Load(a);
            }
            catch
            {
                throw new DirectoryNotFoundException(" ERROR READIND THE FILE" + a);
            }

        }
        private XElement GRCreatorToXML(GuestRequest gr)
        {
            return new XElement("GuestRequest", new XElement("GuestRequestKey", gr.GuestRequestKey),
                                                new XElement("PrivateName", gr.PrivateName),
                                                new XElement("FamilyName", gr.FamilyName),
                                                new XElement("MailAddress", gr.MailAddress),
                                                new XElement("Status", gr.Status),
                                                new XElement("RegistrationDate", gr.RegistrationDate),
                                                new XElement("EntryDate", gr.EntryDate),
                                                new XElement("ReleaseDate", gr.ReleaseDate),
                                                new XElement("Area", gr.Area),
                                                new XElement("SubArea", gr.SubArea),
                                                new XElement("Type", gr.Type),
                                                new XElement("Adults", gr.Adults),
                                                new XElement("Children", gr.Children),
                                                new XElement("Pool", gr.Pool),
                                                new XElement("Jacuzzi", gr.Jacuzzi),
                                                new XElement("Garden", gr.Garden),
                                                new XElement("ChildrensAttractions", gr.ChildrensAttractions),
                                                new XElement("UserName", gr.UserName),
                                                new XElement("Password", gr.Password));
        }
        private XElement HUCreatorToXML(HostingUnit hu)
        {
            return new XElement("HostingUnit", new XElement("HostingUnitKey", hu.HostingUnitKey),
                                               new XElement("Owner", new XElement("HostKey", hu.Owner.HostKey),
                                                                     new XElement("PrivateName", hu.Owner.PrivateName),
                                                                     new XElement("FamilyName", hu.Owner.FamilyName),
                                                                     new XElement("PhoneNumber", hu.Owner.PhoneNumber),
                                                                     new XElement("MailAddress", hu.Owner.MailAddress),
                                                                     new XElement("BankBranchDetails", new XElement("BankNumber", hu.Owner.BankBranchDetails.BankNumber),
                                                                                                       new XElement("BankName", hu.Owner.BankBranchDetails.BankName),
                                                                                                       new XElement("BranchNumber", hu.Owner.BankBranchDetails.BranchNumber),
                                                                                                       new XElement("BranchAddress", hu.Owner.BankBranchDetails.BranchAddress),
                                                                                                       new XElement("BranchCity", hu.Owner.BankBranchDetails.BranchCity)),
                                                                     new XElement("BankAccountNumber", hu.Owner.BankAccountNumber),
                                                                     new XElement("CollectionClearance", hu.Owner.CollectionClearance),
                                                                     new XElement("UserName", hu.Owner.UserName),
                                                                     new XElement("Password", hu.Owner.Password)),
                                               new XElement("HostingUnitName", hu.HostingUnitName),
                                               new XElement("Diary", hu.diaryXML),
                                               new XElement("Area", hu.Area),
                                               new XElement("Type", hu.Type),
                                               new XElement("Adults", hu.Adults),
                                               new XElement("Children", hu.Children),
                                               new XElement("Pool", hu.Pool),
                                               new XElement("Jacuzzi", hu.Jacuzzi),
                                               new XElement("Garden", hu.Garden),
                                               new XElement("ChildrensAttractions", hu.ChildrensAttractions));
        }
        private XElement OrCreatorToXML(Order or)
        {
            return new XElement("Order", new XElement("HostingUnitKey", or.HostingUnitKey),
                                         new XElement("GuestRequestKey", or.GuestRequestKey),
                                         new XElement("OrderKey", or.OrderKey),
                                         new XElement("Status", or.Status),
                                         new XElement("CreateDate", or.CreateDate),
                                         new XElement("OrderDate", or.OrderDate),
                                         new XElement("OrderFee", or.OrderFee));
        }
        
        private int GetRunningNumForGR()
        {
            try
            {
                Load(ref ConfigRoot, ConfigRootPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            XElement it = ConfigRoot.Element("runningNumberForGuestRequest");
            int currNum = Convert.ToInt32(it.Value);
            it.SetValue(currNum + 1);
            ConfigRoot.Save(ConfigRootPath); 

            return currNum;
        }
        private int GetRunningNumForHU()
        {
            try
            {
                Load(ref ConfigRoot, ConfigRootPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }
            XElement it = ConfigRoot.Element("runningNumberForHostingUnit");
            int currNum = Convert.ToInt32(it.Value);
            it.SetValue(currNum + 1);
            ConfigRoot.Save(ConfigRootPath);

            return currNum;
        }
        private int GetRunningNumForHO()
        {
            try
            {
                Load(ref ConfigRoot, ConfigRootPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }
            XElement it = ConfigRoot.Element("runningNumberForHost");
            int currNum = Convert.ToInt32(it.Value);
            it.SetValue(currNum + 1);
            ConfigRoot.Save(ConfigRootPath);

            return currNum;
        }
        private int GetRunningNumForOR()
        {
            try
            {
                Load(ref ConfigRoot, ConfigRootPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }
            XElement it = ConfigRoot.Element("runningNumberForOrder");
            int currNum = Convert.ToInt32(it.Value);
            it.SetValue(currNum + 1);
            ConfigRoot.Save(ConfigRootPath);

            return currNum;
        }
        private int GetFee()
        {
            try
            {
                Load(ref ConfigRoot, ConfigRootPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }
            return Convert.ToInt32(ConfigRoot.Element("fee").Value);

        }
        public int GetNumDaysExpire()
        {
            try
            {
                Load(ref ConfigRoot, ConfigRootPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }
            return Convert.ToInt32(ConfigRoot.Element("numDaysExpire").Value);

        }
        public DateTime GetLastDate()
        {
            try
            {
                Load(ref ConfigRoot, ConfigRootPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }
            return Convert.ToDateTime(ConfigRoot.Element("lastUpdateOrders").Value);
        }
        #endregion
    }
}
