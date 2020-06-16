using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Xml.Serialization;

namespace BE
{
    public class HostingUnit
    {
        private int hostingUnitKey;
        private Host owner;
        private string hostingUnitName;
        [XmlIgnore]
        public bool[,] Diary { get; set; }

        
        [XmlArray("Diary")]
        public bool[] diaryXML
        {
            get { return Diary.Flatten(); }
            set { Diary = value.Expand(12); }//or 12
        }


        public int HostingUnitKey
        {
            get { return hostingUnitKey; }
            set { hostingUnitKey = value; }
        }
        public Host Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        public string HostingUnitName
        {
            get { return hostingUnitName; }
            set { hostingUnitName = value; }
        }
        



        //***Additional properties***//
        private Area area;
        private ResortType type;
        private int adults;
        private int children;
        private bool pool;
        private bool jacuzzi;
        private bool garden;
        private bool childrensAttractions;

        public Area Area
        {
            get { return area; }
            set { area = value; }
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
        public bool Pool
        {
            get { return pool; }
            set { pool = value; }
        }
        public bool Jacuzzi
        {
            get { return jacuzzi; }
            set { jacuzzi = value; }
        }
        public bool Garden
        {
            get { return garden; }
            set { garden = value; }
        }
        public bool ChildrensAttractions
        {
            get { return childrensAttractions; }
            set { childrensAttractions = value; }
        }


        public override string ToString() //do it later
        {
            return "Hosting Unit: Welcome to: " + HostingUnitName + " The kingdom of :" + Owner.PrivateName + " " + Owner.FamilyName;
        }

    }
}
