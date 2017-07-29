using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllianceApplication.SubClasses
{
    public class Address:FileSystemMangement<Address>
    {
        public Address(string streetIn, string cityIn,string stateIn, string zipIn)
            :base()
        {
            street = streetIn;
            city = cityIn;
            state = stateIn;
            zip = zipIn;
        }
        public string street;
        public string city;
        public string state;
        public string zip;

        public override bool Equals(Object obj)
        {
            if (!(obj is Address))
                return false;
            Address p = (Address)obj;
            if (this.street != p.street || this.city != p.city || this.state != p.state || this.zip != p.zip)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + (!Object.ReferenceEquals(null, street) ? street.GetHashCode() : 0);
                hash = (hash * 7) + (!Object.ReferenceEquals(null, city) ? city.GetHashCode() : 0);
                hash = (hash * 7) + (!Object.ReferenceEquals(null, state) ? state.GetHashCode() : 0);
                hash = (hash * 7) + (!Object.ReferenceEquals(null, zip) ? zip.GetHashCode() : 0);
                return hash;
            }
        }
    }
}
