using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Units.Models;

namespace Units.Controllers
{
    public class HomeController : Controller
    {
   
        public static List<Unit> Units { get; set; }

        public static string errorMessage = "";

        /// <summary>
        /// The entry point into the application
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Message = errorMessage;

            if (Units != null)
            {
                ViewBag.List = Units;
                

                List<SelectListItem> items = new List<SelectListItem>();
                foreach (Unit u in Units)
                {
                    items.Add(new SelectListItem { Text = u.Title, Value = u.Title});
                }

                ViewBag.Drop = items;
            }
       
            return View();
        }

        /// <summary>
        /// Upload file to the server and convert it into a set of objects List<Unit>
        /// </summary>
        /// <param name="upload"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            
            string filePath = "";
            string validation = ".xml";

            if (upload != null)
            {
                errorMessage = "";

                string fileName = System.IO.Path.GetFileName(upload.FileName);

                if (fileName.Contains(validation) == false)
                {
                    errorMessage = "Sorry, this file is not XML, select XML file";
                    return RedirectToAction("Index");
                }
                if (upload.ContentLength > 3000000)
                {
                    errorMessage = "Sorry, file exceeds the specified size of 4 MB";
                    return RedirectToAction("Index");
                }
                upload.SaveAs(Server.MapPath("~/Data/" + fileName));
                filePath = Server.MapPath("~/Data/" + fileName);
            }

            else if (upload == null)
            {
                errorMessage = "File not selected! Please select the file to continue";
                return RedirectToAction("Index");
            }
        

            if (filePath != "")
            {
                XmlLoader repository = new XmlLoader(filePath);
                Units = repository.GetData();
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Returns data in JSON format for table
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetDataJson(string unit)
        {
            if (Units != null)
            {
                List<Employee> data = Units.Find(u => u.Title == unit).Employees;
                var res = Json(new { data = data }, JsonRequestBehavior.AllowGet);
                return res;
            }
            else return null;
        }
    }
}
