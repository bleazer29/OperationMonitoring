using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OperationMonitoring.Data;
using OperationMonitoring.Models;
using X.PagedList;

namespace OperationMonitoring.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationContext db;
        public OrdersController(ApplicationContext context)
        {
            db = context;
        }

        // SEARCH
        private List<Order> Searching(List<Order> orders, string searchStatus, string searchField, string searchString)
        {
            switch (searchField)
            {
                case "Counterparty":
                    orders = orders.Where(x => x.Agreement.Counterparty.Title.ToLower().Contains(searchString)).ToList();
                    break;
                case "Agreement":
                    orders = orders.Where(x => x.Agreement.AgreementNumber.ToLower().Contains(searchString)).ToList();
                    break;
                case "Well":
                    orders = orders.Where(x => x.Well.Title.ToLower().Contains(searchString)).ToList();
                    break;
                default:
                    break;
            }
            if (searchStatus == "Open")
            {
                orders = orders.Where(x => x.IsOpen == true).ToList();
            }
            else
            {
                orders = orders.Where(x => x.IsOpen == false).ToList();
            }
            return orders;
        }
        // SORT
        private List<Order> Sorting(List<Order> orders, string sortOrder)
        {
            switch (sortOrder)
            {
                case "well_desc":
                    orders = orders.OrderByDescending(x => x.Well.Title).ToList();
                    break;
                case "well":
                    orders = orders.OrderBy(x => x.Well.Title).ToList();
                    break;
                case "Agreement_desc":
                    orders = orders.OrderByDescending(x => x.Agreement.AgreementNumber).ToList();
                    break;
                case "Agreement":
                    orders = orders.OrderBy(x => x.Agreement.AgreementNumber).ToList();
                    break;
                case "Counterparty_desc":
                    orders = orders.OrderByDescending(x => x.Agreement.Counterparty.Title).ToList();
                    break;
                case "Counterparty":
                    orders = orders.OrderBy(x => x.Agreement.Counterparty.Title).ToList();
                    break;
                case "id_desc":
                    orders = orders.OrderByDescending(x => x.Id).ToList();
                    break;
                default:
                    orders = orders.OrderBy(x => x.Id).ToList();
                    break;
            }
            return orders;
        }

        public ActionResult Index(string oldSortOrder, string newSortOrder, string searchString, string searchField, string searchStatus, int? page)
        {
            ViewBag.CurrentFilter = searchString;

            ViewBag.SearchStatus = string.IsNullOrEmpty(searchStatus) ? "Open" : searchStatus;
            ViewBag.SearchField = string.IsNullOrEmpty(searchField) ? "Counterparty" : searchField;

            var orders = db.Orders.ToList();

            // SEARCH
            if (string.IsNullOrEmpty(searchStatus))
                searchStatus = "Open";
            if (string.IsNullOrEmpty(searchString))
                searchString = "";
            orders = Searching(orders, searchStatus, searchField, searchString);

            // SORTING
            if (string.IsNullOrEmpty(newSortOrder))
            {
                newSortOrder = "id";
            }
            else if (newSortOrder == oldSortOrder)
            {
                newSortOrder += "_desc";
            }
            orders = Sorting(orders, newSortOrder);
            ViewBag.CurrentSort = newSortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(orders.ToPagedList(pageNumber, pageSize));
        }

        // GET: OrdersController/Details/5
        public ActionResult Details(int id)
        {
            var order = db.Orders.FirstOrDefault(x => x.Id == id);

            ViewBag.History = db.OrderHistory.ToList();
            return View(order);
        }
    }
}
