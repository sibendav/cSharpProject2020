//Efrat Ankonina 322796749
//Simha Ben-David 209166776

//Mini Project prat1

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;

namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***function of GuestRequest***\n");
            #region Add new GuestRequest
            Console.WriteLine("* Regular adding of GuestRequest:");
            GuestRequest gr = new GuestRequest
            {
                PrivateName = "Efrat",
                FamilyName = "Anconina",
                MailAddress = "efratush37@gmail.com",
                Status = true,
                RegistrationDate = DateTime.Today,
                EntryDate = new DateTime(2020, 03, 01),
                ReleaseDate = new DateTime(2020, 03, 06),
                Area = Area.North,
                SubArea = "Emeq Israel",
                Type = ResortType.Zimmer,
                Adults = 1,
                Children = 0,
                Pool = Extra.possible,
                Jacuzzi = Extra.NotInterested,
                Garden = Extra.possible,
                ChildrensAttractions = Extra.NotInterested,
                UserName = "Efush",
                Password = "050630"
            };
            try
            {
                gr = BL.BLFactory.getBL().AddCustomerReq(gr);
                Console.WriteLine("  " + gr.ToString());
            }
            catch (MyException ex)
            {
                Console.WriteLine("  " + ex.Message);
            }

            Console.WriteLine("  * Try to add exist object of GuestRequest:");
            GuestRequest gr1 = new GuestRequest
            {
                PrivateName = "Efrat",
                FamilyName = "Anconina",
                MailAddress = "efratush37@gmail.com",
                Status = true,
                RegistrationDate = DateTime.Today,
                EntryDate = new DateTime(2020, 03, 01),
                ReleaseDate = new DateTime(2020, 03, 06),
                Area = Area.North,
                SubArea = "Emeq Israel",
                Type = ResortType.Zimmer,
                Adults = 1,
                Children = 0,
                Pool = Extra.possible,
                Jacuzzi = Extra.NotInterested,
                Garden = Extra.possible,
                ChildrensAttractions = Extra.NotInterested,
                UserName = "Efush",
                Password = "050630"
            };
            try
            {
                gr1 = BL.BLFactory.getBL().AddCustomerReq(gr1);
                Console.WriteLine("    " + gr1.ToString());
            }
            catch (MyException ex)
            {
                Console.WriteLine("    " + ex.Message);
            }

            Console.WriteLine("  * Try to add GuestRequest in invalid dates:");
            GuestRequest gr2 = new GuestRequest
            {
                PrivateName = "Hadar",
                FamilyName = "sofayov",
                MailAddress = "hadar567@gmail.com",
                Status = true,
                RegistrationDate = DateTime.Today,
                EntryDate = new DateTime(2020, 03, 01),
                ReleaseDate = new DateTime(2020, 03, 01),
                Area = Area.North,
                SubArea = "Emeq Israel",
                Type = ResortType.Zimmer,
                Adults = 1,
                Children = 0,
                Pool = Extra.possible,
                Jacuzzi = Extra.NotInterested,
                Garden = Extra.possible,
                ChildrensAttractions = Extra.NotInterested,
                UserName = "Efush",
                Password = "050630"
            };
            try
            {
                gr2 = BL.BLFactory.getBL().AddCustomerReq(gr2);
                Console.WriteLine("    " + gr2.ToString());
            }
            catch (MyException ex)
            {
                Console.WriteLine("    " + ex.Message);
            }
            #endregion
            #region update GuestRequest
            Console.WriteLine("* Regular updating of GuestRequest:");
            gr.PrivateName = "Simha";
            gr.FamilyName = "Ben-David";
            try
            {
                gr = BL.BLFactory.getBL().UpdateCustomerReq(gr);
                Console.WriteLine("  " + gr.ToString());
            }
            catch (MyException ex)
            {
                Console.WriteLine("  " + ex.Message);
            }
            Console.WriteLine("  * Updating new GuestRequest:");
            GuestRequest gr3 = new GuestRequest
            {
                PrivateName = "Hadar",
                FamilyName = "sofayov",
                MailAddress = "hadar567@gmail.com",
                Status = true,
                RegistrationDate = DateTime.Today,
                EntryDate = new DateTime(2020, 11, 01),
                ReleaseDate = new DateTime(2020, 11, 10),
                Area = Area.North,
                SubArea = "Emeq Israel",
                Type = ResortType.Zimmer,
                Adults = 1,
                Children = 0,
                Pool = Extra.possible,
                Jacuzzi = Extra.NotInterested,
                Garden = Extra.possible,
                ChildrensAttractions = Extra.NotInterested,
                UserName = "Efush",
                Password = "050630"
            };
            try
            {
                BL.BLFactory.getBL().UpdateCustomerReq(gr3);
                Console.WriteLine("    " + gr3.ToString());
            }
            catch (MyException ex)
            {
                Console.WriteLine("    " + ex.Message);
                Console.WriteLine("    " + gr3.ToString());
            }
            #endregion

            Console.WriteLine("\n*********************************************************\n");

            Console.WriteLine("***function of HostingUnit***\n");
            #region Add hosting unit
            Console.WriteLine("* Regular adding of HostingUnit:");
            BankBranch bb = new BankBranch
            {
                BankNumber = 11,
                //BankName = "Yahav",
                BranchNumber = 7,
                BranchCity = "Afula",
                BranchAddress = "Hazait 35"
            };
            Host h = new Host
            {
                PrivateName = "Hadar",
                FamilyName = "Cohen",
                PhoneNumber = 0504158113,
                MailAddress = "HadarC54@gmail.com",
                BankBranchDetails = bb,
                BankAccountNumber = 154785632,
                CollectionClearance = false,
                UserName = "1234",
                Password = "hadarou"
            };
            HostingUnit hu = new HostingUnit
            {
                Owner = h,
                HostingUnitName = "Wow sel zimmer",
                Diary = new bool[12, 31],
                Area = Area.North,
                Type = ResortType.Zimmer,
                Adults = 1,
                Children = 0,
                Pool = true,
                Jacuzzi = false,
                Garden = false,
                ChildrensAttractions = true,
            };
            try
            {
                hu = BL.BLFactory.getBL().AddHostingUnit(hu);
                Console.WriteLine("  " + hu.ToString());
            }
            catch (MyException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("  * Try to add exist object of HostingUnit:");
            BankBranch bb1 = new BankBranch
            {
                BankNumber = 11,
                //BankName = "Yahav",
                BranchNumber = 7,
                BranchCity = "Afula",
                BranchAddress = "Hazait 35"
            };
            Host h1 = new Host
            {
                PrivateName = "Hadar",
                FamilyName = "Cohen",
                PhoneNumber = 0504158113,
                MailAddress = "HadarC54@gmail.com",
                BankBranchDetails = bb1,
                BankAccountNumber = 154785632,
                CollectionClearance = false,
                UserName = "1234",
                Password = "hadarou"
            };
            HostingUnit hu1 = new HostingUnit
            {
                Owner = h1,
                HostingUnitName = "Wow sel zimmer",
                Diary = new bool[12, 30],
                Area = Area.North,
                Type = ResortType.Zimmer,
                Adults = 1,
                Children = 0,
                Pool = true,
                Jacuzzi = false,
                Garden = false,
                ChildrensAttractions = true,
            };
            try
            {
                hu1 = BL.BLFactory.getBL().AddHostingUnit(hu1);
                Console.WriteLine("    " + hu1.ToString());
            }
            catch (MyException ex)
            {
                Console.WriteLine("    " + ex.Message);
            }
            #endregion
            #region Update hosting unit
            Console.WriteLine("* Regular updating of Hosting Unit:");
            hu.HostingUnitName = "HaZimmer sel mis cohen! ";
            hu.Owner.PrivateName = " mispahat";
            //hu.Owner.BankBranchDetails.BankName = "Discount";
            hu.Owner.CollectionClearance = true;
            try
            {
                BL.BLFactory.getBL().UpdateHostingUnit(hu);
                Console.WriteLine("  " + hu.ToString());
            }
            catch (MyException ex)
            {
                Console.WriteLine("  " + ex.Message);
            }

            Console.WriteLine("  * Try to update new hosting unit:");
            BankBranch b1 = new BankBranch
            {
                BankNumber = 41,
                //BankName = "Hapualim",
                BranchNumber = 93,
                BranchCity = "Jerusalem",
                BranchAddress = "Hazait 35"
            };
            Host hh1 = new Host
            {
                PrivateName = "Tamar",
                FamilyName = "Grinblat",
                PhoneNumber = 0589632589,
                MailAddress = "tamigri87@gmail.com",
                BankBranchDetails = b1,
                BankAccountNumber = 12695548,
                CollectionClearance = true,
                UserName = "Tamar",
                Password = "12345"
            };
            HostingUnit huhu1 = new HostingUnit
            {
                Owner = hh1,
                HostingUnitName = "HaZimmer Sal Tamary!",
                Diary = new bool[12, 31],
                Area = Area.Jerusalem,
                Type = ResortType.Zimmer,
                Adults = 1,
                Children = 0,
                Pool = true,
                Jacuzzi = true,
                Garden = false,
                ChildrensAttractions = false,
            };
            try
            {
                BL.BLFactory.getBL().UpdateHostingUnit(huhu1);
                Console.WriteLine("    " + huhu1.ToString());
            }
            catch (MyException ex)
            {
                Console.WriteLine("    " + ex.Message);
                Console.WriteLine("    " + huhu1.ToString());
            }
            Console.WriteLine("  * Try to update collection clearance of hosting unit with open order:");
            Order or1 = new Order
            {
                HostingUnitKey = huhu1.HostingUnitKey,
                GuestRequestKey = gr.GuestRequestKey,
                Status = OrderStatus.NotAddressed,
                CreateDate = DateTime.Today
            };
            try
            {
                or1 = BL.BLFactory.getBL().AddOrder(or1);
                huhu1.Owner.CollectionClearance = false;
                BL.BLFactory.getBL().UpdateHostingUnit(huhu1);
                Console.WriteLine("    " + or1.ToString());
            }
            catch (MyException ex)
            {
                Console.WriteLine("    " + ex.Message);
                //Console.WriteLine("    " + or1.ToString());
            }
            #endregion
            #region Deleting hosting unit
            Console.WriteLine("* Regular deleting of Hosting Unit:");
            try
            {
                BL.BLFactory.getBL().DeleteHostingUnit(hu);
                Console.WriteLine("  Bye Bye to: " + hu.ToString());
            }
            catch (MyException ex)
            {
                Console.WriteLine("  " + ex.Message);
            }
            Console.WriteLine("  * Try to deleting the same Hosting Unit:");
            try
            {
                BL.BLFactory.getBL().DeleteHostingUnit(hu);
                Console.WriteLine("    Bye Bye to: " + hu.ToString());
            }
            catch (MyException ex)
            {
                Console.WriteLine("    " + ex.Message);
            }
            Console.WriteLine("  * Try to deleting new Hosting Unit:");
            BankBranch bb2 = new BankBranch
            {
                BankNumber = 12,
                //BankName = "leumi",
                BranchNumber = 98,
                BranchCity = "kiriyat-gat",
                BranchAddress = "HaEzel 76"
            };
            Host h2 = new Host
            {
                PrivateName = "Michal",
                FamilyName = "GiGi",
                PhoneNumber = 0504158113,
                MailAddress = "GiGiHadid@gmail.com",
                BankBranchDetails = bb2,
                BankAccountNumber = 98456325,
                CollectionClearance = true,
                UserName = "GiHad2020",
                Password = "5689"
            };
            HostingUnit hu2 = new HostingUnit
            {
                Owner = h2,
                HostingUnitName = "GiGi Hadid ltd",
                Diary = new bool[12, 31],
                Area = Area.South,
                Type = ResortType.AccommodationApartment,
                Adults = 1,
                Children = 0,
                Pool = true,
                Jacuzzi = true,
                Garden = true,
                ChildrensAttractions = false,
            };
            try
            {
                BL.BLFactory.getBL().DeleteHostingUnit(hu2);
                Console.WriteLine("    Bye Bye to: " + hu.ToString());
            }
            catch (MyException ex)
            {
                Console.WriteLine("    " + ex.Message);
            }
            Console.WriteLine("   * Try to delete hosting unit with open order");
            try
            {
                BL.BLFactory.getBL().DeleteHostingUnit(huhu1);
                Console.WriteLine("  Bye Bye to: " + hu.ToString());
            }
            catch (MyException ex)
            {
                Console.WriteLine("    " + ex.Message);
            }

            #endregion

            Console.WriteLine("\n*********************************************************\n");

            Console.WriteLine("***functions of order***\n");
            #region Add Order

            Console.WriteLine("* Regular adding of new order:");
            BL.BLFactory.getBL().AddHostingUnit(hu2);
            Order or = new Order
            {
                HostingUnitKey = hu2.HostingUnitKey,
                GuestRequestKey = gr3.GuestRequestKey,
                Status = OrderStatus.NotAddressed,
                CreateDate = DateTime.Today,
            };
            try
            {
                or = BL.BLFactory.getBL().AddOrder(or);
                Console.WriteLine("  " + or.ToString());
            }
            catch (MyException ex)
            {
                Console.WriteLine("  " + ex.Message);
                Console.WriteLine("  " + or.ToString());
            }

            Console.WriteLine("  * Try adding exist order:");
            try
            {
                or = BL.BLFactory.getBL().AddOrder(or);
                Console.WriteLine("  " + or.ToString());
            }
            catch (MyException ex)
            {
                Console.WriteLine("    " + ex.Message);
            }

            Console.WriteLine("  * Try adding order in booked-up hosting unit:");
            GuestRequest gr4 = new GuestRequest
            {
                PrivateName = "Nomi",
                FamilyName = "Ben-David",
                MailAddress = "nomi0026@gmail.com",
                Status = true,
                RegistrationDate = DateTime.Today,
                EntryDate = new DateTime(2020, 03, 03),
                ReleaseDate = new DateTime(2020, 03, 05),
                Area = Area.North,
                SubArea = "Emeq Israel",
                Type = ResortType.Zimmer,
                Adults = 1,
                Children = 0,
                Pool = Extra.possible,
                Jacuzzi = Extra.NotInterested,
                Garden = Extra.possible,
                ChildrensAttractions = Extra.NotInterested,
                UserName = "noum78",
                Password = "12345"
            };
            gr4 = BL.BLFactory.getBL().AddCustomerReq(gr4);
            Order or2 = new Order
            {
                HostingUnitKey = hu2.HostingUnitKey,
                GuestRequestKey = gr4.GuestRequestKey,
                Status = OrderStatus.NotAddressed,
                CreateDate = DateTime.Today,
            };
            try
            {
                or = BL.BLFactory.getBL().AddOrder(or);
                Console.WriteLine("    " + or.ToString());
            }
            catch (MyException ex)
            {
                Console.WriteLine("    " + ex.Message);
            }
            #endregion
            #region Update Order            
            Console.WriteLine("* Regular updating of order:");
            or.CreateDate = new DateTime(2020, 10, 02);
            or.Status = OrderStatus.ClosedForCustomerUnresponsiveness;
            try
            {
                or = BL.BLFactory.getBL().UpdateOrder(or);
                Console.WriteLine("  " + or.ToString());
            }
            catch (MyException ex)
            {
                Console.WriteLine("  " + ex.Message);
                if (ex.item != null)
                    Console.WriteLine("  " + ex.item.ToString());
            }

            Console.WriteLine("  * Try to update new order:");

            hu = BL.BLFactory.getBL().AddHostingUnit(hu);
            Order or3 = new Order
            {
                HostingUnitKey = hu.HostingUnitKey,
                GuestRequestKey = gr3.GuestRequestKey,
                Status = OrderStatus.NotAddressed,
                CreateDate = DateTime.Today,
            };
            try
            {
                or3 = BL.BLFactory.getBL().UpdateOrder(or3);
                Console.WriteLine("    " + or3.ToString());
            }
            catch (MyException ex)
            {
                Console.WriteLine("    " + ex.Message);
                if (ex.item != null)
                    Console.WriteLine("    " + ex.item.ToString());
            }

            Console.WriteLine("  * Try to update closed order:");
            or.Status = OrderStatus.ClosedForCustomerUnresponsiveness;
            try
            {
                or = BL.BLFactory.getBL().UpdateOrder(or);
                Console.WriteLine("    " + or.ToString());
            }
            catch (MyException ex)
            {
                Console.WriteLine("    " + ex.Message);
                if (ex.item != null)
                    Console.WriteLine("    " + ex.item.ToString());
            }
            Console.WriteLine("  * Try to update orderStatus to 'sentMail' when collectionClearance isn't arranged:");
            or3.Status = OrderStatus.SentEmail;
            try
            {
                or3 = BL.BLFactory.getBL().UpdateOrder(or3);
                Console.WriteLine("    " + or.ToString());
            }
            catch (MyException ex)
            {
                Console.WriteLine("    " + ex.Message);
                if (ex.item != null)
                    Console.WriteLine("    " + ex.item.ToString());
            }
            Console.WriteLine("  * Try to update orderStatus to 'sentMail':");
            Order or4 = new Order
            {
                HostingUnitKey = hu2.HostingUnitKey,
                GuestRequestKey = gr.GuestRequestKey,
                Status = OrderStatus.NotAddressed,
                CreateDate = DateTime.Today,
            };
            BL.BLFactory.getBL().AddOrder(or4);
            or4.Status = OrderStatus.SentEmail;
            try
            {
                or4 = BL.BLFactory.getBL().UpdateOrder(or4);
                Console.WriteLine("    " + or4.ToString());
            }
            catch (MyException ex)
            {
                Console.WriteLine("    " + ex.Message);
                if (ex.item != null)
                    Console.WriteLine("    " + ex.item.ToString());
            }
            #endregion

            Console.WriteLine("\n*********************************************************\n");

            Console.WriteLine("***list returns functions***\n");
            #region Printing List
            Console.WriteLine("* All Hosting Units in this website:");
            foreach (HostingUnit item in BL.BLFactory.getBL().AllHostingUnits())
            {
                Console.WriteLine(" * " + item.ToString());
            }
            Console.WriteLine("\n* All Guest Requests in this website:");
            foreach (GuestRequest item in BL.BLFactory.getBL().AllGuestRequests())
            {
                Console.WriteLine(" * " + item.ToString());
            }
            Console.WriteLine("\n* All Orders in this website:");
            foreach (Order item in BL.BLFactory.getBL().AllOrders())
            {
                Console.WriteLine(" * " + item.ToString());
            }
            Console.WriteLine("\n* All Bank Branches in this website:");
            foreach (BankBranch item in BL.BLFactory.getBL().AllBankBranch())
            {
                Console.WriteLine(" * " + item.ToString());
            }
            #endregion

            Console.WriteLine("\n*********************************************************\n");

            Console.WriteLine("***Special functions for the ibl interface***\n");
            #region Special
            Console.WriteLine("* All the hostingUnits that available in this dates,");
            DateTime dt1 = DateTime.Today;
            int numDays = 20;
            DateTime dt2 = dt1.AddDays(numDays);
            string s1 = String.Format("{0:MM/dd/yyyy}", dt1);
            string s2 = String.Format("{0:MM/dd/yyyy}", dt2);
            Console.WriteLine("  from: " + s1 + " to: " + s2);
            foreach (HostingUnit item in BL.BLFactory.getBL().AvailableHostingUnit(dt1, numDays))
            {
                Console.WriteLine(" * " + item.ToString());
            }

            Console.WriteLine("* The num of pased days bitween: ");
            DateTime dt3 = new DateTime(2020, 05, 06);
            DateTime dt4 = new DateTime(2020, 12, 02);
            string s3 = String.Format("{0:MM/dd/yyyy}", dt1);
            string s4 = String.Format("{0:MM/dd/yyyy}", dt2);
            Console.WriteLine("  from: " + s3 + " to: " + s3 + "  = " + BL.BLFactory.getBL().PassedDays(dt3, dt4) + " days.");

            Console.WriteLine("* List of all the expired orders: ");
            try
            {
                List<Order> newList = BL.BLFactory.getBL().AllExpiredOrders(0);
                foreach (Order item in newList)
                {
                    Console.WriteLine("  * " + item.ToString());
                }
            }
            catch (MyException ex)
            {
                Console.WriteLine("  " + ex.Message);
            }

            #endregion

            Console.ReadKey();
        }
    }
}
/*
 * Console.WriteLine("hello entry point");
            var group = BL.BLFactory.getBL().GroupGuestReqByArea();
            foreach (var item in group)
            {
                Console.WriteLine(item.Key);
                foreach (var gr in item)
                {

                }
            }

            char ch;
            Console.WriteLine("*** Hello, And Wellcome to our usefull site ***");
            Console.WriteLine("To enter as a hoster - press h");
            Console.WriteLine("To enter as a vacationer - press v");
            Console.WriteLine("To change our call - press c");
            Console.WriteLine("To exit - press e");

            do
            {                
                ch = Convert.ToChar(Console.ReadLine());

                switch (ch)
                {
                    case 'h':
                        {
                            int num;
                            Console.WriteLine("*** HOSTER SERVICES ***");
                            //Console.WriteLine("To add a new host - perss 1");
                            //Console.WriteLine("To update host - perss 2");
                            //Console.WriteLine("To delete host - perss 3");

                            Console.WriteLine("To add a new hosting unit - perss 1");
                            Console.WriteLine("To update hosting unit - perss 2");
                            Console.WriteLine("To delete hosting unit - perss 3");
                            Console.WriteLine("To see all the vacation request that may suite - perss 4");
                            Console.WriteLine("To see all orders that belongs to me - perss 5");
                            Console.WriteLine("To place an order - perss 6");
                            Console.WriteLine("To update an order - perss 7");
                            Console.WriteLine("To exit - perss 0");
                            do
                            {
                                num = Convert.ToInt32(Console.ReadLine());
                                switch (num)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        {
                                            Console.WriteLine("BANK BRANCH DETAILS:");
                                            Console.WriteLine("Enter: BankNumber, BankName, BranchNumber, BranchAddress, BranchCity");
                                            BankBranch b=new BankBranch();
                                            b.BankNumber = Convert.ToInt32(Console.ReadLine());
                                            b.BankName = Console.ReadLine();
                                            b.BranchNumber = Convert.ToInt32(Console.ReadLine());
                                            b.BranchAddress = Console.ReadLine();
                                            b.BranchCity = Console.ReadLine();

                                            Console.WriteLine("HOST DETAILS:");
                                            Console.WriteLine("Enter: PrivateName, FamilyName, PhoneNumber, MailAddress, BankAccountNumber, CollectionClearance (yes/no)");
                                            Host h = new Host();
                                            h.PrivateName = Console.ReadLine();
                                            h.FamilyName= Console.ReadLine();
                                            h.PhoneNumber= Convert.ToInt32(Console.ReadLine());
                                            h.MailAddress= Console.ReadLine();
                                            h.BankBranchDetails = b;
                                            h.BankAccountNumber= Convert.ToInt32(Console.ReadLine());
                                            h.CollectionClearance= Convert.ToBoolean(Console.ReadLine());
                                            //user name and password

                                            Console.WriteLine("HOSTING UNIT DETAILS:");
                                            Console.WriteLine("Enter: ");
                                            HostingUnit hu;
                                            break;
                                        }
                                    case 2:
                                    case 3:
                                    case 4:
                                    case 5:
                                    case 6:
                                    case 7:
                                    default:
                                        Console.WriteLine("ERROR! This key isn't recognized!");
                                        break;
                                }
                            } while (num != 0);
                            break;
                        }
                    case 'v':
                        {
                            int num;
                            Console.WriteLine("*** VACATIONERS SERVICES ***");
                            //Console.WriteLine("To add a new host - perss 1");
                            //Console.WriteLine("To update host - perss 2");
                            //Console.WriteLine("To delete host - perss 3");

                            Console.WriteLine("To add a new vacation request - perss 1");
                            Console.WriteLine("To update vacation request - perss 2");
                            Console.WriteLine("To delete vacation request - perss 3");

                            Console.WriteLine("To print all vacation request - perss 4");
                            Console.WriteLine("To exit - perss 0");
                            do
                            {
                                num = (int)Console.ReadLine()[0];
                                switch (num)
                                {
                                    default:
                                        Console.WriteLine("ERROR! This key isn't recognized!");
                                        break;
                                }
                            } while (num != 0);
                            break;
                        }
                    case 'c':
                        break;
                    default:
                        Console.WriteLine("ERROR! This key isn't recognized!");
                        break;
                }
            } while (ch != 'e');
            Console.ReadKey();
  */
