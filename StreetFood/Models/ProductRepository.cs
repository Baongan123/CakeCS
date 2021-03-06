﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreetFood.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDBContext context;

        public ProductRepository(AppDBContext context)
        {
            this.context = context;
        }
        public Product Create(Product pd)
        {
            context.Products.Add(pd);
            context.SaveChanges();
            return pd;
        }

        public bool Delete(int id)
        {
            var pd = Get(id);
            if (pd != null)
            {
                context.Products.RemoveRange(pd);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public Product Edit(Product pd)
        {
            var edit=context.Products.Attach(pd);
            edit.State = EntityState.Modified;
            context.SaveChanges();
            return pd;
        }

        public Product Get(int id)
        {
            return context.Products.Find(id);
        }
        public IEnumerable<Product> Gets()
        {
            return context.Products;
        }
    }
}
