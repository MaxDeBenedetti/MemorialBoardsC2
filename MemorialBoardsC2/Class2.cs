using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;


namespace MemorialBoardsC2
{
    class FileHandler
    {
        public const string input = "yahrzeits.csv";
        public const string outputHeader = "rmdHeader.txt";
        public const string outputBody = "Bnai Zion Memorial Boards.rmd";
        //public const string hebPeople = "PreferHebrew.txt";

        public static string hebPeopleS = "";

        //extracts data from the report
        public List<Person> FetchPeople()
        {
            List<Person> people = new List<Person>();
            if (File.Exists(input))
            {
                var reader = new StreamReader(input);
                var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);

                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    //next part is done first in order to short circuit if the record does not have a plaque or is the marble
                    string plaque = csv.GetField("Plaque");
                    if (String.IsNullOrEmpty(plaque) || plaque.Contains("M"))
                        continue;

                    Person p = new Person();
                    p.PlaqueNum1 = plaque.Substring(0, 1);
                    p.PlaqueNum2 = plaque.Substring(1, 1);
                    p.PlaqueNum3 = plaque.Substring(3, 2);

                    p.Id = csv.GetField("Id");
                    p.NameF = csv.GetField("Deceased First Name");
                    p.NameF = p.NameF.Replace('"', '\'');
                    p.NameL = csv.GetField("Deceased Last Name");
                    p.NameL = p.NameL.Replace('"', '\'');

                    //I decided to calculate the Hebrew dates from the English date rather than parsing the Hebrew date from the file
                    string deathDate = csv.GetField("English Date");
                    string[] numbers = deathDate.Split('-');
                    p.YearG = int.Parse(numbers[0]);
                    if (p.YearG < 1) continue;
                    p.MonthG = int.Parse(numbers[1]);
                    p.DayG = int.Parse(numbers[2]);

                    p.PreferEnglish = csv.GetField("English Observance").Equals("Y") ? 1 : 0;

                    people.Add(p);
                }
            }
            else
                throw new System.IO.FileNotFoundException("No input file");
            return people;
        }

        //makes the .rmb file
        public void makeFile(List<Person> people)
        {
            string header = "";
            if (File.Exists(outputHeader))
            {
                header = File.ReadAllText(outputHeader);
            }
            else
                throw new System.IO.FileNotFoundException("No header file");

            List<string> lines = new List<string>();
            foreach(Person p in people)
            {
                lines.Add(p.ToString());
            }
            File.WriteAllText(outputBody, header + Environment.NewLine);
            
            File.AppendAllLines(outputBody, lines);
        }

        
    }
}
