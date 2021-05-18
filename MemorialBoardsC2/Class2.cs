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
                TextReader reader = new StreamReader(input);
                CsvReader csv = new CsvReader((IParser)reader);

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
