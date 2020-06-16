using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class GuestRequest
    {
        private int guestRequestKey;
        private string privateName;
        private string familyName;
        private string mailAddress;
        private bool status;
        private DateTime registrationDate;
        private DateTime entryDate;
        private DateTime releaseDate;
        private Area area;
        private string subArea;
        private ResortType type;
        private int adults;
        private int children;
        private Extra pool;
        private Extra jacuzzi;
        private Extra garden;
        private Extra childrensAttractions;
        private string userName;
        private string password;

        public int GuestRequestKey
        {
            get { return guestRequestKey; }
            set { guestRequestKey = value; }
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
        public string MailAddress
        {
            get { return mailAddress; }
            set { mailAddress = value; }
        }
        public bool Status
        {
            get { return status; }
            set { status = value; }
        }
        public DateTime RegistrationDate
        {
            get { return registrationDate; }
            set { registrationDate = value; }
        }
        public DateTime EntryDate
        {
            get { return entryDate; }
            set { entryDate = value; }
        }
        public DateTime ReleaseDate
        {
            get { return releaseDate; }
            set { releaseDate = value; }
        }
        public Area Area
        {
            get { return area; }
            set { area = value; }
        }
        public string SubArea
        {
            get { return subArea; }
            set { subArea = value; }
        }
        public ResortType Type
        {
            get { return type; }
            set { type = value; }
        }
        public int Adults
        {
            get { return adults; }
            set { adults = value; }
        }
        public int Children
        {
            get { return children; }
            set { children = value; }
        }
        public Extra Pool
        {
            get { return pool; }
            set { pool = value; }
        }
        public Extra Jacuzzi
        {
            get { return jacuzzi; }
            set { jacuzzi = value; }
        }
        public Extra Garden
        {
            get { return garden; }
            set { garden = value; }
        }
        public Extra ChildrensAttractions
        {
            get { return childrensAttractions; }
            set { childrensAttractions = value; }
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
            return "GuestRequest: " + PrivateName + " " + FamilyName;
            //return base.ToString();
        }
    }
}
