using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OperationMonitoring.Data;
using OperationMonitoring.Models;

namespace OperationMonitoring.Controllers
{
    public class SharedController : Controller
    {
        private readonly ApplicationContext db;
        public SharedController(ApplicationContext context)
        {
            db = context;
        }

        // Add note
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddNote(int equipmentId, string entityType, int? entityId, string noteText, string controllerId)
        {
            try
            {
                var equipment = db.Equipment.Include(x => x.Status)
                    .FirstOrDefault(x => x.Id == equipmentId);
                var maintenance = db.Maintenances.Where(x => x.Id == entityId).FirstOrDefault();
                var type = db.HistoryTypes.FirstOrDefault(x => x.Title == entityType);
                MaintenanceHistory history = new MaintenanceHistory()
                {
                    Date = DateTime.Now,
                    Maintenance = maintenance,
                    Commentary = noteText
                };
                db.MaintenanceHistory.Add(history);
                await db.SaveChangesAsync();

                if (controllerId == "Equipment")
                {
                    return RedirectToAction("Details", "Equipment", new { id = equipmentId });
                }
                else
                {
                    return RedirectToAction("Details", controllerId, new { id = entityId });
                }

            }
            catch
            {
                return RedirectToAction("Index", "Equipment");
            }
        }

        // Add Hours
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddHours(int equipmentId, int orderId, string controllerId, string hours, string minutes)
        {
            try
            {
                var equipment = db.Equipment.Include(x => x.Status)
                    .FirstOrDefault(x => x.Id == equipmentId);
                int operatingHours = int.Parse(hours) * 60 + int.Parse(minutes);
                equipment.OperatingTime -= operatingHours;
                if (equipment.OperatingTime < 0)
                {
                    equipment.OperatingTime = 0;
                }

                string time = (equipment.OperatingTime / 60) + ":";
                if (equipment.OperatingTime % 60 < 10)
                {
                    time += "0";
                }
                time += (equipment.OperatingTime % 60).ToString();

                var order = db.Orders.Where(x => x.Id == orderId).FirstOrDefault();

                OrderHistory history = new OrderHistory()
                {
                    Date = DateTime.Now,
                    Order = order,
                    OperatingTime = operatingHours / 60,
                    Commentary = "Operating hours: " + hours + ":" + minutes + ", resuorse left - " + time
                };
                db.OrderHistory.Add(history);
                await db.SaveChangesAsync();

                if (controllerId == "Equipment")
                {
                    return RedirectToAction("Details", "Equipment", new { id = equipmentId });
                }
                else
                {
                    return RedirectToAction("Details", controllerId, new { id = orderId });
                }
            }
            catch
            {
                return RedirectToAction("Index", "Equipment");
            }
        }

        // close order
        public IActionResult CloseOrder(int orderId, string controllerId)
        {
            try
            {
                var order = db.Orders.Include(x => x.Equipment).FirstOrDefault(x => x.Id == orderId);
                var equipment = db.Equipment.FirstOrDefault(x => x.Id == order.Equipment.Id);
                order.IsOpen = false;
                order.DateFinish = DateTime.Now;
                OrderHistory history = new OrderHistory()
                {
                    Date = DateTime.Now,
                    Order = order,
                    Message = "Order closed"
                };
                db.OrderHistory.Add(history);

                db.SaveChanges();
                if (controllerId == "Equipment")
                {
                    return RedirectToAction("Details", "Equipment", new { id = equipment.Id });
                }
                else
                {
                    return RedirectToAction("Details", "Orders", new { id = orderId });
                }
            }
            catch
            {
                return RedirectToAction("Index", "Orders");
            }

        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> UploadImg(int equipmentId, int maintenanceId, string controllerId, IFormFileCollection imgfilepath)
        //{
        //    try
        //    {
        //        int added = 0;
        //        string addedimages = "Scans added: ";
        //        var maintenance = db.Maintenances.FirstOrDefault(x => x.Id == maintenanceId);
        //        foreach (var uploadedFile in imgfilepath)
        //        {
        //            string imagestring = ImageHelper.ImageToBase64(uploadedFile);
        //            DocScan scan = new DocScan() { base64Image = imagestring, Maintenance = maintenance, Filename = uploadedFile.FileName };
        //            db.DocScans.Add(scan);
        //            added++;
        //            addedimages += scan.Filename + ", ";
        //        }

        //        if (added > 0)
        //        {
        //            addedimages = addedimages.Substring(0, addedimages.Length - 2);
        //            var equipment = db.Equipment.Include(x => x.Status)
        //                .FirstOrDefault(x => x.Id == equipmentId);
        //            var type = db.HistoryTypes.FirstOrDefault(x => x.Type == "Maintenance");
        //            History history = new History()
        //            {
        //                Date = DateTime.Now,
        //                EquipmentStatus = equipment.Status.Title,
        //                Type = type,
        //                EntityID = maintenance.Id,
        //                Equipment = equipment,
        //                Commentary = addedimages
        //            };
        //            db.History.Add(history);
        //        }

        //        await db.SaveChangesAsync();
        //        if (controllerId == "Equipment")
        //        {
        //            return RedirectToAction("Details", "Equipment", new { id = equipmentId });
        //        }
        //        else
        //        {
        //            return RedirectToAction("Details", "Maintenance", new { id = maintenanceId });
        //        }
        //    }
        //    catch
        //    {
        //        return RedirectToAction("Index", "Maintenance");
        //    }
        //}

        public IActionResult MaintenanceChangeStatus(int equipmentId, int maintenanceId, string controllerId)
        {
            try
            {
                var equipment = db.Equipment.FirstOrDefault(x => x.Id == equipmentId);
                var status = db.EquipmentStatuses.FirstOrDefault(x => x.Title == "SP");
                var maintenance = db.Maintenances.FirstOrDefault(x => x.Id == maintenanceId);
                equipment.Status = status;

                MaintenanceHistory history = new MaintenanceHistory()
                {
                    Date = DateTime.Now,
                    Maintenance = maintenance,
                    Message = "Status changed (Need Repairs)"
                };
                db.MaintenanceHistory.Add(history);

                db.SaveChanges();

                if (controllerId == "Equipment")
                {
                    return RedirectToAction("Details", "Equipment", new { id = equipmentId });
                }
                else
                {
                    return RedirectToAction("Details", "Maintenance", new { id = maintenanceId });
                }
            }
            catch
            {
                return RedirectToAction("Index", "Maintenance");
            }
        }

        // SET DUE DATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetMaintenanceDueDate(int equipmentId, int maintenanceId, string controllerId, string dueDate)
        {
            try
            {
                var equipment = db.Equipment.Include(x => x.Status)
                    .FirstOrDefault(x => x.Id == equipmentId);
                var maintenance = db.Maintenances.FirstOrDefault(x => x.Id == maintenanceId);

                string[] date = dueDate.Split('-');
                DateTime setDate = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
                maintenance.EstimateDate = setDate;

                MaintenanceHistory history = new MaintenanceHistory()
                {
                    Date = DateTime.Now,
                    Maintenance = maintenance,
                    Message = "Maintenance Due Date set: " + maintenance.EstimateDate.Date
                };
                db.MaintenanceHistory.Add(history);

                await db.SaveChangesAsync();

                if (controllerId == "Equipment")
                {
                    return RedirectToAction("Details", "Equipment", new { id = equipmentId });
                }
                else
                {
                    return RedirectToAction("Details", controllerId, new { id = maintenanceId });
                }

            }
            catch
            {
                return RedirectToAction("Index", "Equipment");
            }
        }

        // CLOSE MAINTENANCE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CloseMaintenance(int equipmentId, int maintenanceId, string controllerId, string equipmentStatus, string operatingTime, string operatingTimeMin)
        {
            try
            {
                var equipment = db.Equipment.Include(x => x.Status)
                    .FirstOrDefault(x => x.Id == equipmentId);
                equipment.Status = db.EquipmentStatuses.FirstOrDefault(x => x.Title == equipmentStatus);

                if (equipment.Status.Title == "RFU")
                {
                    equipment.OperatingTime = int.Parse(operatingTime) * 60;
                    equipment.WarningTime = int.Parse(operatingTimeMin) * 60;
                }
                else
                {
                    equipment.OperatingTime = 0;
                    equipment.WarningTime = 0;
                }

                var maintenance = db.Maintenances.FirstOrDefault(x => x.Id == maintenanceId);
                maintenance.IsOpened = false;
                maintenance.FinishDate = DateTime.Now;

                MaintenanceHistory history = new MaintenanceHistory()
                {
                    Date = DateTime.Now,
                    Maintenance = maintenance,
                    Message = "Maintenance finished"
                };
                db.MaintenanceHistory.Add(history);

                await db.SaveChangesAsync();

                if (controllerId == "Equipment")
                {
                    return RedirectToAction("Details", "Equipment", new { id = equipmentId });
                }
                else
                {
                    return RedirectToAction("Details", controllerId, new { id = maintenanceId });
                }

            }
            catch
            {
                return RedirectToAction("Index", "Equipment");
            }
        }


    }
}
