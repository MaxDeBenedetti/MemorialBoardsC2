using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemorialBoardsC2
{
    class Program
    {
        static void Main(string[] args)
        {
            FileHandler fh = new FileHandler();
            List<Person> people = fh.FetchPeople();
            foreach(Person p in people)
            {
                p.ConvertToHebrew(FileHandler.checkPreference(p));
            }
            fh.makeFile(people);
        }
    }
}
