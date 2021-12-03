using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services {
    public class SellerService {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context) {
            this._context = context;
        }

        public List<Seller> FindAll() {
            return this._context.Seller.ToList();
        }

        public void Insert(Seller obj) {
            this._context.Add(obj);
            this._context.SaveChanges();
        }

        public Seller FindById(int id) {
            return this._context.Seller.FirstOrDefault(obj => obj.id == id);
        }

        public void Remove(int id) {
            var obj = this._context.Seller.Find(id);
            this._context.Seller.Remove(obj);
            this._context.SaveChanges();
        }
    }
}