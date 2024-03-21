using BikeStore.DataAccess.Data;
using BikeStore.DataAccess.Repository.IRepository;
using BikeStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.DataAccess.Repository
{
    internal class ProductRepository : Repository<Products>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public new void Update(Products obj) 
        {
            _db.Products.Update(obj);
        }
    }
}
