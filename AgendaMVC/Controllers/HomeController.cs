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
    
    public class HomeController : Controller
    {
        agendaEntities db = new agendaEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}