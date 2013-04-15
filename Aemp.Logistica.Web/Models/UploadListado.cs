using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aemp.Logistica.Web.Models
{
  public class UploadListadoModel
  {
    [Required]
    [StringLength(30, ErrorMessage = "La refencia debe tener minimo {1} caracteres y maximo {0}  ", MinimumLength = 3)]
    [DataType(DataType.Text)]
    [Display(Name = "Numero de Referencia")]
    public string PedidoReferencia { get; set; }

    [Required]
    [StringLength(15, ErrorMessage = "El campo de la referencia del camion debe tener minimo {1} caracteres y maximo {0}  ", MinimumLength = 6)]
    [DataType(DataType.Text)]
    [Display(Name = "Camion")]
    public string CamionReferencia { get; set; }

    [Required]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]    
    [Display(Name = "Fecha de Pedido")]
    public DateTime? PedidoFecha { get; set; }

  }
}