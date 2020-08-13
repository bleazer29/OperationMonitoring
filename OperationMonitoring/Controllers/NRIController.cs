﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OperationMonitoring.Data;
using OperationMonitoring.Models;
using OperationMonitoring.Models.Interfaces;

namespace OperationMonitoring.Controllers
{
    public class NRIController : Controller
    {
        private readonly ApplicationContext db;
        public NRIController(ApplicationContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // DEPARTMENT
        public IActionResult Departments()
        {
            List<Department> departments = db.Departments.ToList();
            ViewBag.Departments = departments;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateDepartment(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Departments.Add(department);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("Departments");
            }
            catch
            {
                return RedirectToAction("Departments");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditDepartment(int editId, string editTitle, string editAddress)
        {
            try
            {
                var department = db.Departments.FirstOrDefault(x => x.Id == editId);
                department.Title = editTitle;
                department.Address = editAddress;
                await db.SaveChangesAsync();
                return RedirectToAction("Departments");
            }
            catch
            {
                return RedirectToAction("Departments");
            }
        }

        // EQUIPMENT TYPE


        // EQUIPMENT CATEGORY
        public IActionResult EquipmentCategories()
        {
            var data = db.EquipmentCategories.ToList();
            var categories = data.Cast<INri>().ToList();
            ViewBag.Categories = categories;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCategory(EquipmentCategory category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.EquipmentCategories.Add(category);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("EquipmentCategories");
            }
            catch
            {
                return RedirectToAction("EquipmentCategories");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCategory(int editId, string editTitle)
        {
            try
            {
                var category = db.EquipmentCategories.FirstOrDefault(x => x.Id == editId);
                category.Title = editTitle;
                await db.SaveChangesAsync();
                return RedirectToAction("EquipmentCategories");
            }
            catch
            {
                return RedirectToAction("EquipmentCategories");
            }
        }
        

        // PROVIDERS


        // NOMENCLATURE


        // COUNTERPARTIES 


        // STORAGES


        // EMPLOYEES


        // POSITIONS
    }
}
