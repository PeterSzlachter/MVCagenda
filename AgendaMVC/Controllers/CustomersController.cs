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
using PagedList;
using PagedList.Mvc;

namespace AgendaMVC.Controllers
{
    
    public class CustomersController : Controller
    {
        agendaEntities db = new agendaEntities();

        //public ActionResult Index(string search)

        //{
        //    return View(db.customers.Where(x => x.lastname == search || search == null).ToList());
        //}

        // GET: Customers
        public ActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomer(customers customer)
        {
            var list = db.customers.ToList();
            if (ModelState.IsValid)
            {
                db.customers.Add(customer);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Client enregistré";
                return RedirectToAction("CustomerList");
            }
            return View(customer);
            
        }
        public ActionResult CustomerList(string searchBy, string search, int? page)
        {
            var searchList = new List<string>() { "Nom", "Prénom", "Courriel", "Numéro de téléphone" };
            ViewBag.searchBy = new SelectList(searchList);
            var customers = db.customers.SqlQuery("select * from customers order by lastname").ToList().ToPagedList(page ?? 1, 9);

            if (searchBy == "Nom")
            {
                return View(db.customers.Where(x => x.lastname.Contains(search) || search == null).ToList().OrderBy(x => x.lastname).ToPagedList(page ?? 1, 9));
            }
            else if (searchBy == "Prénom")
            {
                return View(db.customers.Where(x => x.firstname.Contains(search) || search == null).ToList().OrderBy(x => x.lastname).ToPagedList(page ?? 1, 9));
            }
            else if (searchBy == "Courriel")
            {
                return View(db.customers.Where(x => x.mail.Contains(search) || search == null).ToList().OrderBy(x => x.lastname).ToPagedList(page ?? 1, 9));
            }
            else if (searchBy == "Numéro de téléphone")
            {
                return View(db.customers.Where(x => x.phoneNumber.Contains(search) || search == null).ToList().OrderBy(x => x.lastname).ToPagedList(page ?? 1, 9));
            }

            else if (search != null && searchBy == "Nom")
            {
                return View(customers);
            }
            //if (search != null)
            //{
            //   customers = db.customers.Where(x => x.lastname.Contains(search) || search == null).ToList().ToPagedList(page ?? 1, 7);
            //}
            return View(customers);
        }
        public ActionResult DeleteCustomer(int id)
        {
            var customer = db.customers.Where(model => model.idCustomer == id).First();
            db.customers.Remove(customer);
            db.SaveChanges();
            var list = db.customers.ToList();
            TempData["SuccessMessage"] = "Courtier supprimé";
            return View("CustomerList", list);
        }

        public ActionResult ProfilCustomer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = db.customers.Find(id);
            return View(customer);
        }
        public ActionResult EditCustomer(int? id)
        {
            customers customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomer(customers customer)
        {

            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Ficher client mis à jour";
                return RedirectToAction("CustomerList");
            }
            return View(customer);
        }
    }
}