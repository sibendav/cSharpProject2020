using System;
using System.Collections.Generic;
using System.Text;
using BE;

namespace DS
{
    public class DataSource
    {
        public static List<HostingUnit> AllHostingUnitsList = new List<HostingUnit>();
        //= new List<HostingUnit>
        //{
        //    //***HostingUnit num1***//
        //    new HostingUnit
        //    {
        //        HostingUnitKey=Configuration.getRunningNumberForHostingUnit(), //we should ++ in the config class, but how?
        //        Owner= new Host
        //        {
        //            HostKey=Configuration.getRunningNumberForHost(),
        //            PrivateName= "Mose",
        //            FamilyName="Cohen",
        //            PhoneNumber=0507788445,
        //            MailAddress= "moshco123@gmail.com",
        //            BankBranchDetails= new BankBranch
        //            {
        //                BankNumber=11, //Configuration.getRunningNumberForBankBranch(),
        //                BankName="Discount",
        //                BranchNumber=117,
        //                BranchAddress="vizman 3",
        //                BranchCity="ness-ziona"

        //            },
        //            BankAccountNumber=786543902,
        //            CollectionClearance=true //aproval from the bank 
        //        },
        //        HostingUnitName="wow sel zimmer",
        //        Diary=null
        //    },

        //    //***HostingUnit num2***//
        //    new HostingUnit
        //    {
        //        HostingUnitKey=Configuration.getRunningNumberForHostingUnit(),
        //        Owner= new Host
        //        {
        //            HostKey=Configuration.getRunningNumberForHost(),
        //            PrivateName= "Efrat",
        //            FamilyName="Ankonina",
        //            PhoneNumber=0507878445,
        //            MailAddress= "efratush37@gmail.com",
        //            BankBranchDetails= new BankBranch
        //            {
        //                BankNumber=13, //Configuration.getRunningNumberForBankBranch(),
        //                BankName="Paggi",
        //                BranchNumber=55,
        //                BranchAddress="shaked 67",
        //                BranchCity="Migdal HaEmek"

        //            },
        //            BankAccountNumber=95762381,
        //            CollectionClearance=true //aproval from the bank 
        //        },
        //        HostingUnitName="tiperet hagalil",
        //        Diary=null                            //how to initialize this boolean metrix?
        //    },

        //    //***HostingUnit num3***//
        //    new HostingUnit
        //    {
        //        HostingUnitKey=Configuration.getRunningNumberForHostingUnit(),
        //        Owner= new Host
        //        {
        //            HostKey=Configuration.getRunningNumberForHost(),
        //            PrivateName= "Simha",
        //            FamilyName="Ben-David",
        //            PhoneNumber=0507788225,
        //            MailAddress= "simkha555@gmail.com",
        //            BankBranchDetails= new BankBranch
        //            {
        //                BankNumber=11, //Configuration.getRunningNumberForBankBranch(),
        //                BankName="Discount",
        //                BranchNumber=117,
        //                BranchAddress="vizman 3",
        //                BranchCity="ness-ziona"

        //            },
        //            BankAccountNumber=854163522,
        //            CollectionClearance=true //aproval from the bank 
        //        },
        //        HostingUnitName="Malchot David",
        //        Diary=null
        //    },

        //    //***HostingUnit num4***//
        //    new HostingUnit
        //    {
        //        HostingUnitKey=Configuration.getRunningNumberForHostingUnit(),
        //        Owner= new Host
        //        {
        //            HostKey=Configuration.getRunningNumberForHost(),
        //            PrivateName= "nava tehila",
        //            FamilyName="nagar",
        //            PhoneNumber=0506489652,
        //            MailAddress= "navatehilanagar@gmail.com",
        //            BankBranchDetails= new BankBranch
        //            {
        //                BankNumber=12, //Configuration.getRunningNumberForBankBranch(),
        //                BankName="leumi",
        //                BranchNumber=155,
        //                BranchAddress="vizman 1",
        //                BranchCity="ness-ziona"

        //            },
        //            BankAccountNumber=96547852,
        //            CollectionClearance=true //aproval from the bank 
        //        },
        //        HostingUnitName="HaZimmer sel nava",
        //        Diary=null
        //    }
        //}; // initial reset
        public static List<GuestRequest> AllGuestRequestsList = new List<GuestRequest>(); // initial reset
        public static List<Order> AllOrdersList = new List<Order>(); // initial reset
    }
}
