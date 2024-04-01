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
            var objFromDB = _db.Products.FirstOrDefault(u=>u.product_id == obj.product_id);
            if (objFromDB != null) 
            {
                obj.product_id = objFromDB.product_id;
                obj.brand_id = objFromDB.brand_id;
                obj.list_price = objFromDB.list_price;
                obj.model_year = objFromDB.model_year;
                obj.product_name = objFromDB.product_name;
                if (obj.ImageUrl != null)
                {
                    objFromDB.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
