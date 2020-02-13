using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace MemorialBoardsC2
{
    class FileHandler
    {
        public const string input = "Memorial Plaques - Bnai Zion Congregation.csv";
        public const string outputHeader = "rmdHeader.txt";
        public const string outputBody = "Bnai Zion Memorial Boards.rmd";
        public const string hebPeople = "PreferHebrew.txt";

        public static string hebPeopleS = "";

        //extracts data from the report
        public List<Person> FetchPeople()
        {
            List<Person> people = new List<Person>();
            if (File.Exists(input))
            {
                string[] lines = File.ReadAllLines(input);
                Person p;
                string[] splitLine;
                string[] numbers;
                string line;
                int idNum = 0;
                foreach (string temp in lines)
                {
                    line = temp.Replace("\"", "");
                    if (!(temp.ElementAt<char>(1) == 'M' || temp.ElementAt<char>(1) == 'P' || temp.Contains("0000")))//ignore header and marble memorials
                    {
                        //Parsing the information and adding the people goes here
                        p = new Person();
                        p.PlaqueNum1 = line.Substring(0, 1);
                        p.PlaqueNum2 = line.Substring(1, 1);
                        p.PlaqueNum3 = line.Substring(3, 2);
                        splitLine = line.Split(',');
                        p.Id = idNum + "";//This does not appear in the new input file. *gulp*
                        idNum++;
                        p.NameF = splitLine[4];
                        p.NameL = splitLine[5];
                        numbers = splitLine[1].Split('-');
                        p.DayG = int.Parse(numbers[2]);
                        p.MonthG = int.Parse(numbers[1]);
                        p.YearG = int.Parse(numbers[0]);

                        people.Add(p);
                    }
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

        /// <summary>
        /// Checks to see if the person's ID is in the file of IDs of people that prefer Hebrew
        /// </summary>
        /// <param name="p">person who's prefernce you want to check</param>
        /// <returns>true if person is not on the list, false otherwise</returns>
        public static bool checkPreference(Person p)
        {
            if (hebPeopleS.Equals(""))
            {
                hebPeopleS = File.ReadAllText(hebPeople);
            }
            return !hebPeopleS.Contains(p.Id);
        }
    }
}
