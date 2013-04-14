using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aemp.Logistica.Web.Models
{
  public class DispatchModel
  {
    public DispatchModel()
    {
      Lineas = new List<DispatchLineModel>();
    }

    public string Transportista { get; set; }
    public DateTime Fecha { get; set; }
    public string Estado { get; set; }
    public List<DispatchLineModel> Lineas { get; set; }
  }

  public class DispatchLineModel
  {
    public int LineaId { get; set; }
    public string TipoProducto { get; set; }
    public string Producto { get; set; }
    public string Unidad { get; set; }
    public int Cantidad { get; set; }
    public int PuestoId { get; set; }
    public string PuestoLetra { get; set; }
    public string Comerciante { get; set; }

    public string PuestoDescription()
    {
      return string.IsNullOrEmpty(PuestoLetra)
               ? PuestoId.ToString()
               : string.Format("{0}-{1}", PuestoId, PuestoLetra);
    }
  }
}