using System.Web.Mvc;

namespace Web.Controllers
{
    public class ChooseFromListController : Controller
    {
        // GET: Lookups/Lookup
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ViewResult GetLookup(string objType)
        {
            object o = objType;
            return View("GetLookup", o);
        }

        [HttpGet]
        public ViewResult GetLookupMultiple(string objType)
        {
            object o = objType;
            return View("GetLookupMultiple", o);
        }
    }
}