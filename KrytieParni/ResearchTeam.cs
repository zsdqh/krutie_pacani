using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KrytieParni
{
    enum TimeFrame { Year, TwoYears, Long}
    class ResearchTeam
    {
        private String name;
        public String Name
            { get { return name; }
            set
            {
                if (value[0].ToString().ToUpper() == value[0].ToString())
                {
                    name = value;
                }
                else
                {
                    throw new Exception("Название исследования должно начинаться с большой буквы");
                }
            }
        }
        private String organization;
        public String Organization
        {
            get { return organization; }
            set
            {
                if (value[0].ToString().ToUpper() == value[0].ToString())
                {
                    organization = value;
                }
                else
                {
                    throw new Exception("Название организации должно начинаться с большой буквы");
                }
            }
        }
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (value>0)
                {
                    id = value;
                }
                else
                {
                    throw new Exception("Регистрационный номер должен быть положительным");
                }
            }
        }
        private TimeFrame length;
        public TimeFrame Length { get; set; }
        private Paper[] publications;
        public Paper[] Publications
        {
            get { return publications; }
            set
            {
                publications = value;
            }
        }

        public Paper LatestPaper
        {
            get
            {
                if (publications.Length==0)
                {
                    return null;
                }
                Paper res = publications[0];
                foreach (Paper p in publications)
                {
                    if (p.Date>=res.Date)
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
                return t == length;
            }
        }
        public void AddPapers(params Paper[] papers)
        {
            foreach (Paper p in papers)
            {
                publications.Append(p);
            }
        }
        public override string ToString()
        {
            String res = $"{this.Name}" +
                $"\n\t{this.organization}" +
                $"\n\t{this.Id}" +
                $"\n\t{(this.length.ToString() == "3" ? "несколько" : length.ToString())} лет" +
                $"\n\tПубликации:" +
                $"\n\t{{";
            foreach (Paper p in publications)
            {
                res += "\n\t\t" + p.ToString();
            }
            return res + "\n\t}";
        }

        public String ToShortString()
        {
            return $"{this.Name}" +
                $"\n\t{this.organization}" +
                $"\n\t{this.Id}" +
                $"\n\t{(this.length.ToString() == "3" ? "несколько" : length.ToString())} лет"+
                $"\n\tКол-во публикаций: {publications.Length}";
        }

        public ResearchTeam(String name="Исследование", String organization="Организация", int id=-1, TimeFrame tf=TimeFrame.Long)
        {
            this.Name = name;
            this.Organization = organization;
            this.Id = id;
            this.Length = tf;
        }



    }
}
