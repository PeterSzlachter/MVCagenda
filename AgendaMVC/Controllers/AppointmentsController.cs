using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Net;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using AgendaMVC.Models;

namespace AgendaMVC.Controllers
{
    public class AppointmentsController : Controller

    {
        agendaEntities db = new agendaEntities();

        public ActionResult Index()
        {
            //var appointment = db.appointments.Include(a => a.brokers).Include(a => a.customers);
            return View();
        }
        // GET: Appointments
        public ActionResult AddAppointment()
        {
            ViewBag.idCustomer = new SelectList(db.customers, "idCustomer", "lastname");
            ViewBag.idBroker = new SelectList(db.brokers, "idBroker", "lastname");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AddAppointment(appointments appointment)
        {
            DateTime dateAppointment = appointment.dateHour;
            DateTime dateAppointmentEnd = appointment.dateHour.AddMinutes(30);
            var rangeAppointment = appointment.dateHour.CompareTo(dateAppointmentEnd);
            if (db.appointments.Where(a => a.idBroker == appointment.idBroker).Any() && db.appointments.Where(a => a.dateHour == appointment.dateHour).Any() && db.appointments.Where(a => a.dateHour <= dateAppointmentEnd).Any())
            {
                ModelState.AddModelError("idBroker", "Time not available.");
                TempData["SuccessMessage"] = "Rendez-vous failed";
            }
            else
            {
                if (ModelState.IsValid)
                {
                    
                    db.appointments.Add(appointment);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Rendez-vous enregistré";
                    return RedirectToAction("Index", "Home");
                }
            }
                ViewBag.idBroker = new SelectList(db.brokers, "idBroker", "lastname", appointment.idBroker);
                ViewBag.idCustomer = new SelectList(db.customers, "idCustomer", "lastname", appointment.idCustomer);
                return View(appointment);
            
        }

        public ActionResult AppointmentList()
        {
            var appointment = db.appointments.ToList();
            return PartialView(appointment);
        }

        public ActionResult DeleteAppointment(int id)
        {
            var appointment = db.appointments.Where(model => model.idAppointment == id).First();
            db.appointments.Remove(appointment);
            db.SaveChanges();
            var list = db.appointments.ToList();
            TempData["SuccessMessage"] = "Rendez-vous supprimé";
            return RedirectToAction("Index", "Home", list);
        }

        public ActionResult ProfilAppointment(int? id)
        {
        
            appointments appointment = db.appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            
            return View("_ProfilAppointment",appointment);
        }


        public ActionResult EditAppointment(int? id)
        {
            appointments appointment = db.appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.idBroker = new SelectList(db.brokers, "idBroker", "lastname", appointment.idBroker);
            ViewBag.idCustomer = new SelectList(db.customers, "idCustomer", "lastname", appointment.idCustomer);
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAppointment(appointments appointment)
        {

            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Ficher rendez-vous mis à jour";
                return RedirectToAction("Index", "Home");
            }
            return View(appointment);
        }
    }
}