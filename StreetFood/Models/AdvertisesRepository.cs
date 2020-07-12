using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreetFood.Models
{
    public class AdvertisesRepository : IAdvertisesRepository
    {
        private readonly AppDBContext context;

        public AdvertisesRepository(AppDBContext context)
        {
            this.context = context;
        }
        public Advertises Edit(Advertises advertises)
        {
            var edit=context.Advertises.Attach(advertises);
            edit.State = EntityState.Modified;
            context.SaveChanges();
            return advertises;
        }

        public Advertises Get(int id)
        {
            return context.Advertises.Find(id);
        }

        public IEnumerable<Advertises> Gets()
        {
            return context.Advertises;
        }
    }
}
