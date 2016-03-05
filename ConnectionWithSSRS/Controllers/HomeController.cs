using System.Configuration;
using System.Data.Entity.Infrastructure.Design;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ConnectionWithSSRS.Models;
using ConnectionWithSSRS.ReportService2010;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace ConnectionWithSSRS.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext dbContext;

        public HomeController()
        {
            dbContext = ApplicationDbContext.Create();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            var userId = User.Identity.GetUserId();
            var user = dbContext.Users.FirstOrDefault(it => it.Id == userId);
            // TODO: Remove
            ViewBag.Message = "";

            var rs = new ReportingService2010()
            {
                Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MvcReportViewer.Username"], ConfigurationManager.AppSettings["MvcReportViewer.Password"], "CIFTVDS")
            };

            var items = rs.ListChildren("/", true);

            return View(new ReportPageViewModel()
            {
                User = user,
                Reports = items
            });
        }

        public ActionResult GetReport(string path)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = dbContext.Users.FirstOrDefault(it => it.Id == userId);

                if (user != null && user.HasAccessToService)
                {
                    return PartialView("_ReportView", path);
                }
            }

            return View("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}