using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_sem_3.Controllers
{
    [Authorize]
    public class OrderNoticeController : Controller
    {
        // GET: OrderNotice
        public ActionResult Index()
        {
            return View();
        }
    }
}