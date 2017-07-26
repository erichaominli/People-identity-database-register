using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace databaseGame
{
    class Person
    {
        public string name;
        public string gender;
        public int age;
        public string energy;
        public string ID;
        public Person(string Name, int Age, string Gender)
        {
            name = Name;
            age = Age;
            gender = Gender;
            ID = Generate(8);
            energy = Generate(3);
        }

        public Person(string Name, int Age, string Gender, string Id, string Energy)
        {
            name = Name;
            age = Age;
            gender = Gender;
            ID = Id;
            energy = Energy;
        }

        static public string Generate(int length)
        {
            Random random = new Random();
            string s = "";
            for (int i = 0; i < length; i++)
            {
                int num = random.Next(0, 9);
                s = s + num.ToString();
            }
            return s;
        }
            


        }

    }
