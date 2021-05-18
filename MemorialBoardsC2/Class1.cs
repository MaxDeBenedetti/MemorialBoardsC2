using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace MemorialBoardsC2
{
    class Person
    {
        private string id;
        private string nameF;
        private string nameL;
        private int dayG;//G is for Gregorian
        private int monthG;
        private int yearG;
        private int dayH;//H is for Hebrew
        private int monthH;
        private int yearH;
        private DateTime deathdateG;
        private string plaqueNum1;
        private string plaqueNum2;
        private string plaqueNum3;

        private int preferEnglish = -1;//for dynamic programming purposes

        public static int currentYearG = DateTime.UtcNow.Year;

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string NameF
        {
            get
            {
                return nameF;
            }

            set
            {
                nameF = value;
            }
        }

        public string NameL
        {
            get
            {
                return nameL;
            }

            set
            {
                nameL = value;
            }
        }

        public int DayG
        {
            get
            {
                return dayG;
            }

            set
            {
                dayG = value;
            }
        }

        public int MonthG
        {
            get
            {
                return monthG;
            }

            set
            {
                monthG = value;
            }
        }

        public int YearG
        {
            get
            {
                return yearG;
            }

            set
            {
                yearG = value;
            }
        }

        public int DayH
        {
            get
            {
                return dayH;
            }

            set
            {
                dayH = value;
            }
        }

        public int MonthH
        {
            get
            {
                return monthH;
            }

            set
            {
                monthH = value;
            }
        }

        public int YearH
        {
            get
            {
                return yearH;
            }

            set
            {
                yearH = value;
            }
        }

        public DateTime DeathdateG
        {
            get
            {
                return deathdateG;
            }

            set
            {
                deathdateG = value;
            }
        }

        public string PlaqueNum1
        {
            get
            {
                return plaqueNum1;
            }

            set
            {
                plaqueNum1 = value;
            }
        }

        public string PlaqueNum2
        {
            get
            {
                return plaqueNum2;
            }

            set
            {
                plaqueNum2 = value;
            }
        }

        public string PlaqueNum3
        {
            get
            {
                return plaqueNum3;
            }

            set
            {
                plaqueNum3 = value;
            }
        }


        public Person()
        {

        }

        public Person(string id, string nameF, string nameL, int dayG, int monthG, int yearG, int dayH, int monthH, int yearH, string plaqueNum1, string plaqueNum2, string plaqueNum3)
        {
            this.id = id;
            this.nameF = nameF;
            this.nameL = nameL;
            this.dayG = dayG;
            this.monthG = monthG;
            this.yearG = yearG;
            this.dayH = dayH;
            this.monthH = monthH;
            this.yearH = yearH;
            this.plaqueNum1 = plaqueNum1;
            this.plaqueNum2 = plaqueNum2;
            this.plaqueNum3 = plaqueNum3;
        }


        /// <summary>
        /// Calculates the Hebrew date of the English anniversary of the person's death for the English year of death
        /// </summary>
        public void ConvertToHebrew()
        {
            //int year = useEnglish ? currentYearG : yearG; //deprecated
            GregorianCalendar gc = new GregorianCalendar(GregorianCalendarTypes.USEnglish);
            HebrewCalendar hc = new HebrewCalendar();
            deathdateG = new DateTime(yearG, monthG, dayG, gc);

            yearH = hc.GetYear(deathdateG);
            monthH = CorrectedHebrewMonth(deathdateG);
            dayH = hc.GetDayOfMonth(deathdateG);
        }


        public override string ToString()
        {
                   
            return String.Format("\"{0}\",\"{1}\",\"{2}\",\"{3} {4}\",\"{5}\",\"{6}\",\"{7}\",\"0\",\"{8}\"",
                plaqueNum1, plaqueNum2, plaqueNum3, nameF, nameL, dayG, monthG, yearG, preferEnglish);
        }

        /// <summary>
        /// Converts from Microsoft's Hebrew month numbering system to usual Hebrew month numbering system
        /// The Hebrew calendar begins on the first day of the seventh month
        /// To illustrate this problem:
        /// Microsoft says since Tishrei is the first month of the new year it is number 1
        /// However, Tishrei is normally number 7
        /// </summary>
        /// <param name="d">A DateTime</param>
        /// <returns>The corrected month number</returns>
        public int CorrectedHebrewMonth(DateTime d)
        {
            HebrewCalendar hc = new HebrewCalendar();
            int zeroCountMonth = hc.GetMonth(d) - 1;//having my months be zero count allows for modulo division
            
            if (hc.IsLeapYear(hc.GetYear(d))){
                zeroCountMonth = (zeroCountMonth + 6) % 13;
            }
            else
            {
                zeroCountMonth = (zeroCountMonth + 6) % 12;
            }
            return zeroCountMonth + 1;
        }

        /// <summary>
        /// Returns true if the person prefers the English date over Hebrew death date
        /// </summary>
        /// <returns>true if the person prefers English, false if Hebrew</returns>
        public bool CheckEnlishPrefernce()
        {
            
            if (preferEnglish == 1)
                return true;
            else return false;
        }
    }
}
