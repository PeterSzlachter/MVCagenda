using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Net;
using System.Data.Entity;
using AgendaMVC.Models;

namespace AgendaMVC.Controllers
{
    public class BrokersController : Controller
    {
        agendaEntities db = new agendaEntities();

        // GET: Brokers
        public ActionResult AddBroker()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBroker(brokers broker)
        {
            if (ModelState.IsValid)
            {
                db.brokers.Add(broker);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Courtier enregistré";
            }
            return RedirectToAction("BrokerList");
        }

        public ActionResult BrokerList()
        {
            var brokers = db.brokers.ToList();
            return View(brokers);
        }
        public ActionResult DeleteBroker(int id)
        {
            var broker = db.brokers.Where(model => model.idBroker == id).First();
            db.brokers.Remove(broker);
            db.SaveChanges();
            var list = db.brokers.ToList();
            TempData["SuccessMessage"] = "Courtier supprimé";
            return View("BrokerList", list);
        }

        public ActionResult EditBroker(int? id)
        {
            brokers broker = db.brokers.Find(id);
            if (broker == null)
            {
                return HttpNotFound();
            }
            return View(broker);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBroker(brokers broker)
        {

            if (ModelState.IsValid)
            {
                db.Entry(broker).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Courtier mis à jour";
                return RedirectToAction("BrokerList");
            }
            return View(broker);
        }

        public ActionResult ProfilBroker(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var broker = db.brokers.Find(id);
            return View(broker);
        }
    }
}