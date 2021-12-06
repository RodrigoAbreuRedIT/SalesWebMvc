using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<Seller>> FindAllAsync() {
            return await this._context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller obj) {
            this._context.Add(obj);
            await this._context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id) {
            // instruçao com join, para juntar as tabelas dos sellers com as dos departamentos
            return await this._context.Seller.Include(obj => obj.department).FirstOrDefaultAsync(obj => obj.id == id);
        }

        public async Task RemoveAsync(int id) {
            try {
                var obj = await this._context.Seller.FindAsync(id);
                this._context.Seller.Remove(obj);
                await this._context.SaveChangesAsync();
            } catch (DbUpdateException e) {
                throw new IntegrityException(e.Message);
            }
            
        }

        public async Task UpdateAsync(Seller obj) {
            bool hasAny = await _context.Seller.AnyAsync(x => x.id == obj.id);

            if (!hasAny) {
                throw new NotFoundException("Id not found");
            }
            try {
                this._context.Update(obj);
                await this._context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e) {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}