using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OperationMonitoring.Data;
using OperationMonitoring.Models;
using Microsoft.EntityFrameworkCore;

namespace OperationMonitoring.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee

        private readonly ApplicationContext db;

        public EmployeeController(ApplicationContext db)
        {
            this.db = db;
        }


        public ActionResult Index()
        {
            return View(this.db.Employees.ToList());
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View(this.db.Employees.ToList().Where(x => x.Id == id).FirstOrDefault());
        }

        // GET: Employee/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employees)
        {
            try
            {
                Employee employee = new Employee
                {
                    BirthDate = employees.BirthDate,
                    Email = employees.Email,
                    FirstName = employees.FirstName,
                    LastName = employees.LastName,
                    Patronymic = employees.Patronymic,
                    UserGUID = Convert.ToString(Guid.NewGuid())
                };
                this.db.Add(employee);
                this.db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View(this.db.Employees.ToList().Where(x => x.Id == id).FirstOrDefault());
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Employee employees)
        {
            try
            {

                this.db.Entry(employees).State = EntityState.Modified;
                this.db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View(this.db.Employees.ToList().Where(x => x.Id == id).FirstOrDefault());
        }

        // POST: Employee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                this.db.Employees.Remove(this.db.Employees.Where(x => x.Id == id).FirstOrDefault());
                this.db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
