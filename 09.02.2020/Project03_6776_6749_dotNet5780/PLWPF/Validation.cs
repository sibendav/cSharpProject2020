using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLWPF
{
    public static class validation
    {
        public static List<string> AllMailTails = new List<string>() { "gmail.com", "g.jct.ac.il", "neto.net.co.il", "yahoo.com", "hotmail.com", "walla.com" };

        public static bool IsID(string id)//שיטה הבודקת תקינות תעודת זהות ישראלית
        {
            if (id.Length < 9)
            {
                for (int i = 0; i < 9 - id.Length; i++)
                {
                    id = "0" + id;
                }
            }
            int sum = 0;
            int temp;
            for (int i = 0; i < 9; i++)
            {
                if (i % 2 == 0)
                {
                    sum += Convert.ToInt32(id[i].ToString());
                }
                else
                {
                    temp = Convert.ToInt32(id[i].ToString()) * 2;
                    if (temp > 9)
                    {
                        temp = temp / 10 + temp % 10;
                    }
                    sum += temp;
                }

            }
            if (sum % 10 == 0)
                return true;
            else
                return false;
        }

        public static bool IsNum(string p)//שיטה הבודקת תקינות מספר
        {
            if (p != null)
            {
                for (int i = 0; i < p.Length; i++)
                {
                    if ((p[i] < '0') || (p[i] > '9'))
                        return false;
                }
                return true;
            }
            return false;
        }

        public static bool IsHebrew(string str)//שיטה הבודקת תקינות אותיות בעברית
        {
            if (str != null)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if ((str[i] < 'א') || (str[i] > 'ת'))
                        return false;
                }
                return true;
            }
            return false;
        }
        public static bool IsEnglish(string str)//שיטה הבודקת תקינות אותיות באנגלית
        {
            if (str != null)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if ((str[i] < 'a') && (str[i] > 'z') || (str[i] < 'A') && (str[i] > 'Z'))
                        return false;
                }
                return true;
            }
            return false;
        }

        public static bool IsPhone(string p)//שיטה הבודקת תקינות מספר טלפון
        {
            if (p != null && IsNum(p) && (p.Length == 9 || p.Length == 10)) //48='0' באסקי
                return true;
            else return false;
        }

        public static bool IsMailAddress(string str)//שיטה שבודקת אם המחרוזת היא מייל 
        {
            if (str != null)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == '@')
                    {
                        str = str.Substring(i+1);
                        foreach(string item in AllMailTails)
                        {
                            if (str == item)
                                  return true;
                        }
                        //for (string s1 = AllMailTails.First(); s1 != AllMailTails.Last(); s1 = s1 + 1)
                        //    if (str == s1)
                        //        return true;
                    }
                }
            }
            return false;
        }
    }
}
