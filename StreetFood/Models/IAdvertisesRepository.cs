using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreetFood.Models
{
    public interface IAdvertisesRepository
    {
        IEnumerable<Advertises> Gets();
        Advertises Edit(Advertises advertises);
        Advertises Get(int id);
    }
}
