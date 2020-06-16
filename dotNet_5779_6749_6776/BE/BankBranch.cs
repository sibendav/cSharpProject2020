using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class BankBranch
    {
        private int bankNumber;
        private Banks bankName;
        private int branchNumber;
        private String branchAddress;
        private string branchCity;

        public int BankNumber
        {
            get { return bankNumber; }
            set { bankNumber = value; }
        }
        public Banks BankName
        {
            get { return bankName; }
            set { bankName = value; }
        }
        public int BranchNumber
        {
            get { return branchNumber; }
            set { branchNumber = value; }
        }
        public String BranchAddress
        {
            get { return branchAddress; }
            set { branchAddress = value; }
        }
        public string BranchCity
        {
            get { return branchCity; }
            set { branchCity = value; }
        }



        public override string ToString() //do it later
        {
            return "Bank Branch: " + BankNumber + " " + BankName + " " + BranchNumber + " " + BranchAddress + " " + BranchCity;
        }

    }

}
