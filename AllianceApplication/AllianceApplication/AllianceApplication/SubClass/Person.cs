using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllianceApplication.SubClasses
{
    public class Person:FileSystemMangement<Person>
    {
        public Person(string fNameIn, string lNameIn, Address addressIn)
            :base()
        {
            FirstName = fNameIn;
            LastName = lNameIn;
            Address = addressIn;
        }
        public string FirstName;
        public string LastName;
        public Address Address;
        
        public override bool Equals(Object obj)
        {
            if (!(obj is Person))
                return false;
            Person p = (Person)obj;
            if (this.FirstName != p.FirstName || this.LastName != p.LastName || !this.Address.Equals(p.Address))
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + (!Object.ReferenceEquals(null, FirstName) ? FirstName.GetHashCode() : 0);
                hash = (hash * 7) + (!Object.ReferenceEquals(null, LastName) ? LastName.GetHashCode() : 0);
                hash = (hash * 7) + (!Object.ReferenceEquals(null, Address) ? Address.GetHashCode() : 0);
                return hash;
            }
        }
    }
}
