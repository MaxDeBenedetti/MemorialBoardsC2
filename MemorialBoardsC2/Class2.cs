﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace MemorialBoardsC2
{
    class FileHandler
    {
        public const string input = "Yahrzeit Plaque Export.txt";
        public const string outputHeader = "rmdHeader.txt";
        public const string outputBody = "Bnai Zion Memorial Boards.rmd";
        public const string hebPeople = "PreferHebrew.txt";

        public static string hebPeopleS = "";

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
                foreach (string temp in lines)
                {
                    line = temp.Replace(",", "");
                    if (!(temp.ElementAt<char>(1) == 'M' || temp.ElementAt<char>(1) == 'P' || temp.Contains("0000")))//ignore header and marble memorials
                    {
                        //Parsing the information and adding the people goes here
                        p = new Person();
                        line.Replace(",\"", "");
                        p.PlaqueNum1 = line.Substring(1, 1);
                        p.PlaqueNum2 = line.Substring(2, 1);
                        p.PlaqueNum3 = line.Substring(4, 2);
                        splitLine = line.Split('\"');
                        p.Id = splitLine[3];
                        p.NameF = splitLine[7];
                        p.NameL = splitLine[9];
                        numbers = splitLine[10].Split('/');
                        p.DayG = int.Parse(numbers[1]);
                        p.MonthG = int.Parse(numbers[0]);
                        p.YearG = int.Parse(numbers[2]);

                        people.Add(p);
                    }
                }
            }
            else
                Console.WriteLine("No Input file");
            return people;
        }

        public void makeFile(List<Person> people)
        {
            string header = "";
            if (File.Exists(outputHeader))
            {
                header = File.ReadAllText(outputHeader);
            }
            else
                Console.WriteLine("No header file");
            List<string> lines = new List<string>();
            foreach(Person p in people)
            {
                lines.Add(p.ToString());
            }
            File.WriteAllText(outputBody, header + Environment.NewLine);
            
            File.AppendAllLines(outputBody, lines);
        }

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