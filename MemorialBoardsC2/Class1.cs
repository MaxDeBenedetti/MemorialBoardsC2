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
        private int dayG;
        private int monthG;
        private int yearG;
        private int dayH;
        private int monthH;
        private int yearH;
        private DateTime deathdateG;
        private string plaqueNum1;
        private string plaqueNum2;
        private string plaqueNum3;

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
        /// Calculates the Hebrew date of the English anniversary of the person's death for this English year
        /// </summary>
        public void ConvertToHebrew(bool useEnglish)
        {
            int year = useEnglish ? currentYearG : yearG;
            GregorianCalendar gc = new GregorianCalendar(GregorianCalendarTypes.USEnglish);
            HebrewCalendar hc = new HebrewCalendar();
            deathdateG = new DateTime(year, monthG, dayG, gc);

            yearH = hc.GetYear(deathdateG);
            monthH = hc.GetMonth(deathdateG);
            dayH = hc.GetDayOfMonth(deathdateG);
            
        }


        /// <summary>
        /// Determins if the current person would prefer their english or hebrew death date to be used.
        /// Very hardcoded right now. should change this later.
        /// </summary>
        /// <returns>true if person prefered the english date</returns>
        public bool useEnglish()
        {
            return (!(String.Equals(id, "LAWAR1", StringComparison.CurrentCultureIgnoreCase)));
        }

        
        public override string ToString()
        {
            return String.Format("\"{0}\",\"{1}\",\"{2}\",\"{3} {4}\",\"{5}\",\"{6}\",\"{7}\",\"0\"",
                plaqueNum1, plaqueNum2, plaqueNum3, nameF, nameL, dayH, monthH, yearH);
        }
    }
}
