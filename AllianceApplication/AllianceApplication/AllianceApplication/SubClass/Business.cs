using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllianceApplication.SubClasses
{
    public class Business:FileSystemMangement<Business>
    {
        public Business(string nameIn, Address addressIn)
            :base()
        {
            Name = nameIn;
            Address = addressIn;
        }
        public string Name;
        public Address Address;

        public override bool Equals(Object obj)
        {
            if (!(obj is Business))
                return false;
            Business p = (Business)obj;
            if (this.Name != p.Name || !this.Address.Equals(p.Address))
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + (!Object.ReferenceEquals(null, Name) ? Name.GetHashCode() : 0);
                hash = (hash * 7) + (!Object.ReferenceEquals(null, Address) ? Address.GetHashCode() : 0);
                return hash;
            }
        }
    }
}
