using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OperationMonitoring.Data;
using OperationMonitoring.Models;
using OperationMonitoring.ModelsIdentity;
using OperationMonitoring.ModelsIdentity.Security;

namespace OperationMonitoring.Controllers
{
    public class AssembliesController : Controller
    {
        ApplicationContext db;

        public AssembliesController(ApplicationContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            List<Assemble> assembles = db.Assemblies
                .Include(x => x.Equipment)
                .Include(x => x.Part)
                .ToList();
            return View(assembles);
        }
    }
}
