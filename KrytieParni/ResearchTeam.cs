using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KrytieParni
{

    public interface IEnumerator<T>
    {
        bool MoveNext();
        public T Current { get; }
        void Reset();
    }
    class ResearchTeamEnumerator : IEnumerator<Person>
    {
        private List<Person> PeopleWithPublications = new List<Person>();
        private int ind = -1;
        public ResearchTeamEnumerator(List<Paper> publications)
        {
            foreach (Paper p in publications)
            {
                if (!PeopleWithPublications.Contains(p.Author))
                    PeopleWithPublications.Add(p.Author);
            }
        }
        public Person Current
        {
            get { return PeopleWithPublications[ind]; }
        }


        public bool MoveNext()
        {
            if (ind + 1 == PeopleWithPublications.Count)
            {
                Reset();
                return false;
            }
            ind++;
            return true;
        }
        public void Reset()
        {
            ind = -1;
        }

    }

    enum TimeFrame { Year, TwoYears, Long}
    // RESEARCH TEAM----------------------------------------------RESEARCH TEAM----------------------------------------RESEARCH TEAM--------
    class ResearchTeam : Team, INameAndCopy, IEnumerable<Person>
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
        private Team BaseTeam;
        public Team GetBaseTeam
        {
            get
            {
                return BaseTeam;
            }
            set
            {
                BaseTeam.Name = value.Name;
                BaseTeam.Id = value.Id;
            }
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
            BaseTeam = new Team(name, id);
        }
        public override object DeepCopy()
        {
            List<Paper> new_pub = new List<Paper>();
            foreach (Paper p in publications)
            {
                new_pub.Add((Paper)p.DeepCopy());
            }
            List<Person> new_pep = new List<Person>();
            foreach(Person p in People)
            {
                new_pep.Add((Person)p.DeepCopy());
            }

            return new ResearchTeam(Research, Name, Id, Length, new_pub, new_pep);
        }
        public IEnumerable<Person> PersonsWithoutPublications()
        {
            List<Person> authors = new List<Person>();

            foreach (Paper p in publications)
            {
                if (!authors.Contains(p.Author))
                    authors.Add(p.Author);
            }
            foreach (Person p in people)
            {
                if (!authors.Contains(p)) yield return p;
            }
            yield break;
        }
        public IEnumerable<Person> PersonsWithMany()
        {
            List<Person> authors = new List<Person>();
            foreach (Paper pub in publications)
            {
                if (!authors.Contains(pub.Author)) authors.Add(pub.Author);
                else yield return pub.Author;
            }
            yield break;

        }
        public IEnumerable<Paper> PapersLessThanN(int n)
        {
            int now = DateTime.Now.Year;
            foreach (Paper pub in publications)
            {
                if (pub.Date.Year - now <= n) yield return pub;
            }
            yield break;

        }
        public IEnumerable<Paper> LastYear()
        {
            return PapersLessThanN(1);
        }
        public IEnumerator<Person> GetEnumerator()
        {
            return new ResearchTeamEnumerator(this.Publications);
        }

        System.Collections.Generic.IEnumerator<Person> IEnumerable<Person>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
