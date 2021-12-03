using System.Collections.Generic;

namespace SalesWebMvc.Models.ViewModels {
    public class SellerFormViewModel {
        public Seller seller { get; set; }
        public ICollection<Department> departments { get; set; }
    }
}