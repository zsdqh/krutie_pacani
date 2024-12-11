using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrytieParni
{
    class Paper:INameAndCopy
    {
        string title;
        Person author;
        DateTime date;
        public string Name
        {
            get { return title; }
            set
            {
                if (value.Length > 2 && value.Length < 100)
                {
                    title = value;
                }
                else
                    throw new Exception("Не допустимая длинна названия публикации");
            }
        }
        public Person Author
        {
            get { return author; }
            set
            {
                if (value != null)
                {
                    author = value;
                }
                else
                    throw new Exception("Не допустимый автор публикации");
            }
        }
        public DateTime Date
        {
            get
            { return date; }
            set
            {
                if (value.Year < DateTime.Now.Year && value.Year > 1920)
                    date = value;
                else
                    throw new Exception("Не допустимая дата публикации");
            }
        }
        public Paper() : this("Публикация") { }
        public Paper(string title):this(title,new Person()) { }
        public Paper(string title, Person author) : this(title, author, new DateTime(2000, 1, 1)) { }
        public Paper(string title, Person author, DateTime date)
        {
            Name = title;
            Author = author;
            Date = date;
        }
        public override string ToString()
        {
            return $"\n\t\tНазвание: {title}\n\t\tАвтор: {author.ToShortString()}\n\t\tДата публикации: {date.ToString("dd MMMM yyyy")}";
        }
        public virtual object DeepCopy()
        {
            return new Paper(Name, author.DeepCopy() as Person, Date);
        }
    }

}
