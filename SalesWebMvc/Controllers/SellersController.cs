using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers {
    public class SellersController : Controller {

        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService) {
            this._sellerService = sellerService;
            this._departmentService = departmentService;
        }

        public IActionResult Index() {
            var list = this._sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create() {
            var departments = this._departmentService.FindAll();
            var viewModel = new SellerFormViewModel() { departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller) {
            this._sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}