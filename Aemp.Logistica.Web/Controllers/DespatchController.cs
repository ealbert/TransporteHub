using System.Web;
using System.Web.Mvc;

namespace Aemp.Logistica.Web.Controllers
{
  public class DespatchController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult UploadFile(HttpPostedFileBase contactsFile)
    {
      return View("Index");
    }
  }
}