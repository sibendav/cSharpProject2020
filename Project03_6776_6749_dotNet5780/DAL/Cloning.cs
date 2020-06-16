﻿using System;
using System.Collections.Generic;
using System.Text;
using BE;

namespace DAL
{
    public static class Cloning
    {
        public static BankBranch Clone(this BankBranch original)
        {
            BankBranch target = new BankBranch();
            target.BankNumber = original.BankNumber;
            target.BankName = original.BankName;
            target.BranchNumber = original.BranchNumber;
            target.BranchAddress = original.BranchAddress;
            target.BranchCity = original.BranchCity;
            return target;

        }
        public static GuestRequest Clone(this GuestRequest original)
        {
            GuestRequest target = new GuestRequest();
            target.GuestRequestKey = original.GuestRequestKey;
            target.PrivateName = original.PrivateName;
            target.FamilyName = original.FamilyName;
            target.MailAddress = original.MailAddress;
            target.Status = original.Status;
            target.RegistrationDate = new DateTime(original.RegistrationDate.Year, original.RegistrationDate.Month, original.RegistrationDate.Day);
            target.EntryDate = new DateTime(original.EntryDate.Year, original.EntryDate.Month, original.EntryDate.Day);
            target.ReleaseDate = new DateTime(original.ReleaseDate.Year, original.ReleaseDate.Month, original.ReleaseDate.Day);
            target.Area = original.Area;
            target.SubArea = original.SubArea;
            target.Type = original.Type;
            target.Adults = original.Adults;
            target.Children = original.Children;
            target.Pool = original.Pool;
            target.Jacuzzi = original.Jacuzzi;
            target.Garden = original.Garden;
            target.ChildrensAttractions = original.ChildrensAttractions;
            target.UserName = original.UserName;
            target.Password = original.Password;
            return target;
        }
        public static Host Clone(this Host original)
        {
            Host target = new Host();
            target.HostKey = original.HostKey;
            target.PrivateName = original.PrivateName;
            target.FamilyName = original.FamilyName;
            target.PhoneNumber = original.PhoneNumber;
            target.MailAddress = original.MailAddress;
            target.BankBranchDetails = original.BankBranchDetails.Clone();
            target.BankAccountNumber = original.BankAccountNumber;
            target.CollectionClearance = original.CollectionClearance;
            target.UserName = original.UserName;
            target.Password = original.Password;
            return target;
        }
        public static HostingUnit Clone(this HostingUnit original)
        {
            HostingUnit target = new HostingUnit();
            target.HostingUnitKey = original.HostingUnitKey;
            target.Owner = original.Owner.Clone();
            target.HostingUnitName = original.HostingUnitName;
            target.Diary = original.Diary; //new bool[,];
            target.Type = original.Type;
            target.Area = original.Area;
            target.Adults = original.Adults;
            target.Children = original.Children;
            target.Pool = original.Pool;
            target.Jacuzzi = original.Jacuzzi;
            target.Garden = original.Garden;
            target.ChildrensAttractions = original.ChildrensAttractions;
            return target;
        }
        public static Order Clone(this Order original)
        {
            Order target = new Order();
            target.HostingUnitKey = original.HostingUnitKey;
            target.GuestRequestKey = original.GuestRequestKey;
            target.OrderKey = original.OrderKey;
            target.Status = original.Status;
            target.CreateDate = new DateTime(original.CreateDate.Year, original.CreateDate.Month, original.CreateDate.Day);
            target.OrderDate = new DateTime(original.OrderDate.Year, original.OrderDate.Month, original.OrderDate.Day);
            target.OrderFee = original.OrderFee;
            return target;
        }
    }
}
