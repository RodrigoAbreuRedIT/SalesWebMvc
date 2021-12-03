﻿using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });

            var obj = this._sellerService.FindById(id.Value);

            if(obj == null)
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

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
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });

            var obj = this._sellerService.FindById(id.Value);

            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

            return View(obj);
        }

        public IActionResult Edit(int? id) {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });

            var obj = this._sellerService.FindById(id.Value);

            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { seller = obj, departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller) {
            // id da URL
            if (id != seller.id)
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            try {
                this._sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            } catch(NotFoundException e) {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            } catch (DbConcurrencyException e) {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message) {
            var viewModel = new ErrorViewModel { message = message, 
                                                 RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier};

            return View(viewModel);
        }
    }
}