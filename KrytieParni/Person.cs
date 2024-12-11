using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrytieParni
{
    class Person:INameAndCopy
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
            get { return surname; }
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
                if(value.Year<DateTime.Now.Year && value.Year > 1920)
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
        public Person() : this("Ivan") { }
        public Person(string name) : this(name, "Ivanov") { }
        public Person(string name,string surname):this(name,surname,new DateTime(2000,1,1)) { }
        public Person(string name, string surname, DateTime birth)
        {
            Name = name;
            Surname = surname;
            Birth = birth;
        }
        public override string ToString()
        {
            return $"Имя: {Name}\nФамилия: {Surname}\nДата рождения: {Birth.ToString("dd MMMM yyyy")}";
        }
        public override bool Equals(object? obj)
        {
            if ((object)obj == null || !(obj is Person))
                return false;
            else
            {
                if (this.Name == (obj as Person).Name && this.Birth == (obj as Person).Birth && this.Surname == (obj as Person).Surname)
                    return true;
                return false;
            }
        }
        public static bool operator ==(Person p1, Person p2)
        {
            if (Equals(p1, null)) return Equals(p2, null);

            return p1.Equals(p2);
        }

        public static bool operator !=(Person p1, Person p2)
        {
            if (Equals(p1, null)) return !Equals(p2, null);
            return !p1.Equals(p2);
        }
        public virtual string ToShortString()
        {
            return Name +" "+Surname;
        }
        public virtual object DeepCopy()
        {
            return MemberwiseClone();
        }
        public override int GetHashCode()
        {
            int hashCode = 17;
            hashCode = hashCode * 31 + Name.GetHashCode();
            hashCode = hashCode * 31 + Surname.GetHashCode();
            return hashCode;
        }
    }
}
