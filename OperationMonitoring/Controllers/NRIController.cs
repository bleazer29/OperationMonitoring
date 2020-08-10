using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OperationMonitoring.Controllers
{
    public class NRIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
