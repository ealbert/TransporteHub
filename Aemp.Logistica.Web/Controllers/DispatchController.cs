using System;
using System.Collections.Generic;
using System.Linq;
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
      var invalidFlag = IsInvalidUploadFile(uploadedFile);
      if (!ModelState.IsValid || invalidFlag)
      {
        ModelState.AddModelError("", "Compruebe por favor que todos los datos fueron correctamente introducidos");
        if(invalidFlag)        
        {
          ModelState.AddModelError("", InvalidUploadFileNotification(uploadedFile));  
        }
        return View("Index", model);  
      }            
      return RedirectToAction("Listing", model);
    }

    private bool IsInvalidUploadFile(HttpPostedFileBase uploadedFile)
    {
      if (uploadedFile == null) return true;
      if (uploadedFile.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") return true;
      return false;
    }

    private string InvalidUploadFileNotification(HttpPostedFileBase uploadedFile)
    {
      if(uploadedFile == null) return "Seleccione un documento de excel con la informacion del pedido";
      if (uploadedFile.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
      {
        return "Confirme que el archivo con el listado es un documento valido de Excel del tipo XLSX";
      }
      return "El archivo no es reconocido, contacte con el administrador";
    }

    public ActionResult Listing(UploadListadoModel model)
    {
      var listado = GetAlbaran(model);
      const string msg = "{0} - Nuevo albarán con fecha {1:dd-MM-yyyy} con {2} lineas";
      ViewBag.Message = string.Format(msg, listado.Transportista, listado.Fecha, listado.Lineas.Count);    
      Session.Add("Listado", listado);
      return View(listado);
    }

    public ActionResult Enquiry()
    {
      var model = Listados();
      return View(model);
    }

    public ActionResult ExcelTemplate()
    {
      return File("~/Content/documents/Listado_Plantilla.xlsx", "application/vnd.ms-excel", "Listado_Plantilla.xlsx");
    }

    private DispatchModel GetAlbaran(UploadListadoModel model)
    {
      var result = new DispatchModel {Estado = "Recibido", Fecha = model.PedidoFecha.Value, Transportista = "KillerLogistics", Camion = model.CamionReferencia, PedidoReferencia = model.PedidoReferencia};
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
      var listado = (DispatchModel) Session["Listado"];
      listado.FechaCreado = DateTime.Now;
      Listados().Add(listado);
      return RedirectToAction("Enquiry");
    }

    public ActionResult Discard()
    {
      TempData["NotificationMsg"] = "Ultimo albaran importado fue descartado.";
      return RedirectToAction("Index");
    }

    private List<DispatchModel> Listados()
    {
      if (Session["Listados"] == null)
      {
        Session["Listados"] = new List<DispatchModel>();
      }
      return (List<DispatchModel>) Session["Listados"];
    }
  }
}