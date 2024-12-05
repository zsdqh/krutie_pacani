using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Team:INameAndCopy
{

    protected String name;
    public String Name
    {
        get { return name; }
        set
        {
            if (value[0].ToString().ToUpper() == value[0].ToString())
            {
                name = value;
            }
            else
            {
                throw new ArgumentException("Название организации должно начинаться с большой буквы");
            }
        }
    }
    protected int id;
    public int Id
    {
        get { return id; }
        set
        {
            if (value > 0)
            {
                id = value;
            }
            else
            {
                throw new ArgumentException("Регистрационный номер должен быть положительным");
            }
        }
    }
    public Team(string name="", int id=1)
    {
        this.Name = name;
        this.Id = id;
    }
    public virtual object DeepCopy()
    {
        return new Team(this.Name, this.Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj is Team t) return t.Name==this.Name && t.Id==this.Id;
        return false;
    }
    public override int GetHashCode()
    {
        return this.Name.GetHashCode()+this.Id;
    }
    public override string ToString()
    {
        return $"{Id}. Организация {Name}";
    }



}