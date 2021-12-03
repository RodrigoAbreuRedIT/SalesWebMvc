using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;

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
            // instruçao com join, para juntar as tabelas dos sellers com as dos departamentos
            return this._context.Seller.Include(obj => obj.department).FirstOrDefault(obj => obj.id == id);
        }

        public void Remove(int id) {
            var obj = this._context.Seller.Find(id);
            this._context.Seller.Remove(obj);
            this._context.SaveChanges();
        }

        public void Update(Seller obj) {
            if(!_context.Seller.Any(x => x.id == obj.id)) {
                throw new NotFoundException("Id not found");
            }
            try {
                this._context.Update(obj);
                this._context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException e) {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}