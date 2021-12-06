using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models {
    public class Seller {
        public int id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage ="{0} required")]
        [StringLength(60, MinimumLength =3, ErrorMessage = "{0} size should be between {2} and {1}")]
        public string name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime birthDate { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double baseSalary { get; set; }
        public int DepartmentId { get; set; }
        public Department department { get; set; }
        public ICollection<SalesRecord> sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department) {
            this.id = id;
            this.name = name;
            this.email = email;
            this.birthDate = birthDate;
            this.baseSalary = baseSalary;
            this.department = department;
        }

        public void AddSeles(SalesRecord sr) {
            this.sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr) {
            this.sales.Remove(sr);
        }

        public double TotalSales(DateTime inicial, DateTime final) {
            return this.sales.Where(sr => sr.date >= inicial && sr.date <= final).Sum(sr => sr.amount);
        }
    }
}