using System;
using System.ComponentModel.DataAnnotations;
using SalesWebMvc.Models.Enums;

namespace SalesWebMvc.Models {
    public class SalesRecord {
        public int id { get; set; }

        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime date { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
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