using System;
using System.Web;
using System.Web.Mvc;
using Aemp.Logistica.Web.Models;

namespace Aemp.Logistica.Web.Controllers
{
  public class DispatchController : Controller
  {
    public ActionResult Index()
    {
      if (TempData.ContainsKey("NotificationMsg"))
      {
        ViewBag.NotificationMsg = TempData["NotificationMsg"];
      }
      return View();
    }

    public ActionResult UploadFile(UploadListadoModel model, HttpPostedFileBase uploadedFile)
    {
      if (!ModelState.IsValid || uploadedFile == null)
      {
        ModelState.AddModelError("", "Compruebe por favor que todos los datos fueron correctamente introducidos");
        if (uploadedFile == null)
        {
          ModelState.AddModelError("", "Seleccione un documento de excel con plantilla de listado y la informacion del pedido");  
        }
        return View("Index", model);  
      }            
      return RedirectToAction("Listing");
    }

    public ActionResult Listing()
    {
      var albaran = GetAlbaran();
      const string msg = "{0} - Nuevo albarán con fecha {1:dd-MM-yyyy} con {2} lineas";
      ViewBag.Message = string.Format(msg, albaran.Transportista, albaran.Fecha, albaran.Lineas.Count);
      return View(GetAlbaran());
    }

    private DispatchModel GetAlbaran()
    {
      var result = new DispatchModel {Estado = "Recibido", Fecha = DateTime.Now.Date, Transportista = "KillerLogistics"};
      var linea = new DispatchLineModel
        {
          LineaId = 1,
          TipoProducto = "Fresco",
          Producto = "Merluza",
          Unidad = "Kg",
          Cantidad = 25,
          PuestoId = 18,
          Comerciante = "ABELARDO ALVAREZ, S.L."
        };

      result.Lineas.Add(linea);

      linea = new DispatchLineModel
      {
        LineaId = 2,
        TipoProducto = "Congelado",
        Producto = "Pulpo Congelado",
        Unidad = "Bulto",
        Cantidad = 4,
        PuestoId = 4,
        PuestoLetra = "A",
        Comerciante = "PESCADOS JESUS CELORRIO, S.L."
      };

      result.Lineas.Add(linea);

      linea = new DispatchLineModel
      {
        LineaId = 3,
        TipoProducto = "Marisco",
        Producto = "Mejillon",
        Unidad = "Saco",
        Cantidad = 20,
        PuestoId = 112,
        Comerciante = "MOLUSCOS MADRID, S.A."
      };

      result.Lineas.Add(linea);

      return result;
    }

    public ActionResult Confirm()
    {
      TempData["NotificationMsg"] = "Ultimo albaran fue confirmado.";
      return RedirectToAction("Index");
    }

    public ActionResult Discard()
    {
      TempData["NotificationMsg"] = "Ultimo albaran importado fue descartado.";
      return RedirectToAction("Index");
    }
  }
}