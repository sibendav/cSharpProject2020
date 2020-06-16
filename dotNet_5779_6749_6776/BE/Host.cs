using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Host
    {
        private int hostKey;
        private string privateName;
        private string familyName;
        private int phoneNumber; //F
        private string mailAddress;
        private BankBranch bankBranchDetails;
        private int bankAccountNumber;
        private bool collectionClearance;
        private string userName;
        private string password;

        public int HostKey
        {
            get { return hostKey; }
            set { hostKey = value; }
        }
        public string PrivateName
        {
            get { return privateName; }
            set { privateName = value; }
        }
        public string FamilyName
        {
            get { return familyName; }
            set { familyName = value; }
        }
        public int PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        public string MailAddress
        {
            get { return mailAddress; }
            set { mailAddress = value; }
        }
        public BankBranch BankBranchDetails
        {
            get { return bankBranchDetails; }
            set { bankBranchDetails = value; }
        }
        public int BankAccountNumber
        {
            get { return bankAccountNumber; }
            set { bankAccountNumber = value; }
        }
        public bool CollectionClearance
        {
            get { return collectionClearance; }
            set { collectionClearance = value; }
        }
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public override string ToString() //do it later
        {
            return HostKey.ToString();
        }


    }
}
