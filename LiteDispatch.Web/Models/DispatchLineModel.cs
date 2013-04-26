namespace LiteDispatch.Web.Models
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  public class DispatchModel
  {
    public DispatchModel()
    {
      Lineas = new List<DispatchLineModel>();
    }

    public string Transportista { get; set; }

    [Display(Name = "Fecha de Envio")]
    [DataType(DataType.Date)]
    public DateTime Fecha { get; set; }

    [Display(Name = "Estado de Listado")]
    public string Estado { get; set; }
        
    [Display(Name = "Referencia de Camion")]
    public string Camion { get; set; }

    [Display(Name = "Numero de Referencia")]
    public string PedidoReferencia { get; set; }

    public List<DispatchLineModel> Lineas { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime FechaCreado { get; set; }

    public Guid Guid { get; set; }
    
    public string Usuario { get; set; }

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