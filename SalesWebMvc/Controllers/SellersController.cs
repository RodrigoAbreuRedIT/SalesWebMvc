using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
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

        public IActionResult Delete(int? id) {
            if (id == null)
                return NotFound();

            var obj = this._sellerService.FindById(id.Value);

            if(obj == null)
                return NotFound();

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) {
            this._sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id) {
            if (id == null)
                return NotFound();

            var obj = this._sellerService.FindById(id.Value);

            if (obj == null)
                return NotFound();

            return View(obj);
        }

        public IActionResult Edit(int? id) {
            if (id == null)
                return NotFound();

            var obj = this._sellerService.FindById(id.Value);

            if (obj == null)
                return NotFound();

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { seller = obj, departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller) {
            if (id != seller.id)
                return BadRequest();
            try {
                this._sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            } catch(NotFoundException) {
                return NotFound();
            } catch (DbConcurrencyException) {
                return BadRequest();
            }
        }
    }
}