using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrytieParni
{
    class Person
    {
        string name;
        string surname;
        DateTime birth;
        public string Name
        {
            get { return name; }
            set {
                if (value.Length > 2 && value.Length < 20)
                    name = value;
                else
                    throw new Exception("Не допустимая длинна имени");
            }
        }
        public string Surname
        {
            get { return name; }
            set
            {
                if (value.Length > 2 && value.Length < 20)
                    surname = value;
                else
                    throw new Exception("Не допустимая длинна фамилии");
            }
        }
        public DateTime Birth
        {
            get
            { return birth; }
            set
            {
                if(value.Year<DateTime.Now.Year)
                    birth = value;
                else
                    throw new Exception("Не допустимая дата рождения");
            }
        }
        public int BirthYear
        {
            get
            { return birth.Year; }
            set
            {
                if (value < DateTime.Now.Year)
                    birth = new DateTime(value,birth.Month,birth.Day);
                else
                    throw new Exception("Не допустимый год рождения");
            }
        }
    }
}
