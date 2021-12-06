using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services {
    public class SalesRecordService {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context) {
            this._context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate) {
            var result = from obj in this._context.SalesRecords select obj;

            if (minDate.HasValue)
                result = result.Where(x => x.date >= minDate.Value);

            if (maxDate.HasValue)
                result = result.Where(x => x.date <= maxDate.Value);

            return await result
                        .Include(x => x.seller)
                        .Include(x => x.seller.department)
                        .OrderByDescending(x => x.date)
                        .ToListAsync();
        }
    }
}
