using System.Collections.Generic;
using System;
using System.Linq;

namespace SalesWebMvc.Models {
    public class Department {
        public int id { get; set; }
        public string name { get; set; }
        public ICollection<Seller> sellers { get; set; } = new List<Seller>();

        public Department() { }

        public Department(int id, string name) {
            this.id = id;
            this.name = name;
        }

        public void AddSeller(Seller seller) {
            this.sellers.Add(seller);
        }

        public double TotalSales(DateTime inicial, DateTime final) {
            return this.sellers.Sum(seller => seller.TotalSales(inicial, final));
        }
    }
}