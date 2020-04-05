using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_sem_3.Models;

namespace Project_sem_3.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]

    public class LifeInsurancesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int Id)
            {
                var lifeInsurances = db.LifeInsurances.Include(m => m.Contract).Include(m => m.InsurancePackage).Where(x => x.ContractId == Id);
                return View(lifeInsurances.ToList());
            }
    }
}