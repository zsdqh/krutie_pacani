using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KrytieParni
{
    enum TimeFrame { Year, TwoYears, Long}
    class ResearchTeam: Team, INameAndCopy
    {
        protected String research;
        public new String Research
        {
            get { return research; }
            set
            {
                if (value[0].ToString().ToUpper() == value[0].ToString())
                {
                    research = value;
                }
                else
                {
                    throw new Exception("Название исследования должно начинаться с большой буквы");
                }
            }
        }
        private List<Person> people;
        public List<Person> People
        {
            get { return people; }
            set { people = value; }
        }
        public TimeFrame Length { get; set; }
        private List<Paper> publications;
        public List<Paper> Publications
        {
            get { return publications; }
            set
            {
                publications = value;
            }
        }
        public Team GetBaseTeam
        {
            get { return null; } //КОропа уточнит завтра(в субботу)
        }

        public Paper LatestPaper
        {
            get
            {
                if (publications.Count == 0)
                {
                    return null;
                }
                Paper res = publications[0];
                foreach (Paper p in publications)
                {
                    if (p.Date >= res.Date)
                    {
                        res = p;
                    }
                }
                return res;
            }
        }
        public bool this[TimeFrame t]
        {
            get
            {
                return t == Length;
            }
        }
        public void AddPapers(List<Paper> papers)
        {
            foreach (Paper p in papers)
            {
                publications.Add(p);
            }
        }
        public void AddMembers(List<Person> per)
        {
            foreach(Person p in per)
            {
                people.Add(p);
            }
        }

        public override string ToString()
        {
            String res = $"{this.Research}" +
                $"\n\t{this.Name}" +
                $"\n\t{this.Id}" +
                $"\n\t{((int)this.Length == 2 ? "несколько" : (int)Length+1)} лет" +
                $"\n\tПубликации:" +
                $"\n\t{{";
            foreach (Paper p in publications)
            {
                res += "\n\t\t" + p.ToString();
            }
            res += "\n\n\t}";
            res += "\n\tУчастники:" +
                "\n\t{";
            foreach(Person p in people)
            {
                res += "\n\t\t" + p.ToString();
            }
            return res + "\n\n\t}";
        }

        public String ToShortString()
        {
            return $"{this.Research}" +
                $"\n\t{this.Name}" +
                $"\n\t{this.Id}" +
                $"\n\t{((int)this.Length == 2 ? "несколько" : (int)Length + 1)} лет"+
                $"\n\tКол-во публикаций: {publications.Count}" +
                $"\n\tКОл-во участников: {people.Count}";
        }

        public ResearchTeam(String name="Исследование", String organization="Организация", int id=-1, TimeFrame tf=TimeFrame.Long, List<Paper> pub=null, List<Person> peop=null):base(organization, id)
        {
            this.Research = name;
            this.Length =tf;
            this.publications = pub;
            this.people = peop;
            if (pub == null ) 
                this.publications = new List<Paper>();
            if(peop== null )
                this.people = new List<Person>();
        }
        public override object DeepCopy()
        {
            return new ResearchTeam(Research, Name, Id, Length, new List<Paper>(publications), new List<Person>(People));
        }


    }
}
