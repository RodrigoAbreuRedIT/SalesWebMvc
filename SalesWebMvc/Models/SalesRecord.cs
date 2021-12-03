using System;
using SalesWebMvc.Models.Enums;

namespace SalesWebMvc.Models {
    public class SalesRecord {
        public int id { get; set; }
        public DateTime date { get; set; }
        public double amount { get; set; }
        public SaleStatus status { get; set; }
        public Seller seller { get; set; }

        public SalesRecord() { }

        public SalesRecord(int id, DateTime date, double amount, SaleStatus status, Seller seller) {
            this.id = id;
            this.date = date;
            this.amount = amount;
            this.status = status;
            this.seller = seller;
        }
    }
}