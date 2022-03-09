using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    internal partial class UsuarioMetadata
    {
        [Display(Name = "Identificación")]
        public int id { get; set; }
        [Display(Name = "Nombre")]
        //[Required(ErrorMessage = "El nombre es un campo obligatorio")]

        public string nombre { get; set; }
        [Display(Name = "Primer apellido")]
        //[Required(ErrorMessage = "El apellido es un campo obligatorio")]
        public string apellidos { get; set; }


        [Display(Name = "Correo")]
        [Required(ErrorMessage = "El correo es un campo obligatorio")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        public string email { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Debe digitar una contraseña")]
        public string contrasena { get; set; }
        [Display(Name = "Teléfono")]


        public Nullable<int> idTelf { get; set; }
        [Display(Name = "Rol")]
        public Nullable<int> idRol { get; set; }
        [Display(Name = "Estado")]
        public Nullable<bool> estado { get; set; }
    }
    internal partial class ProductoMetadata
    {
        [Display(Name = "Identificación")]
        public int id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Use solo letras y espacios")]
        [Display(Name = "Nombre del Producto")]
        public string nombre { get; set; }

        [Display(Name = "Descripción del producto")]
        [Required(ErrorMessage = "La descripción es requerida")]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Use solo letras y espacios")]
        public string descripcion { get; set; }


        [Display(Name = "Tipo de unidad")]
        [Required(ErrorMessage = "Debe seleccionar un tipo de unidad")]
        public Nullable<int> idTipoUnidad { get; set; }

        [Display(Name = "Tipo de producto")]
        [Required(ErrorMessage = "Debe seleccionar un tipo de producto")]
        public Nullable<int> idTipoProducto { get; set; }

        [Display(Name = "Marca")]
        [Required(ErrorMessage = "Debe seleccionar una marca")]
        public Nullable<int> idMarca { get; set; }

        [Display(Name = "Precio Unitario")]
        [DataType(DataType.Currency)]
        [MaxLength(12)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El precio debe un valor numérico")]
        [Required(ErrorMessage = "El precio es requerido")]
        public Nullable<double> precioUnitario { get; set; }

        [Display(Name = "Cantidad Númerico")]
        [MaxLength(12)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "La cantidad debe un valor numérico")]
        public Nullable<int> cantidadNum { get; set; }

        [Display(Name = "Cantidad en peso")]
        [MaxLength(12)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El peso debe un valor numérico")]
        public Nullable<double> cantidadPeso { get; set; }

        [Display(Name = "Imagen del producto")]
        [Required(ErrorMessage = "La imagen es requerida")]
        public byte[] imagen { get; set; }

        [Display(Name = "Estado")]
        public Nullable<bool> estado { get; set; }
    }
    internal partial class CitaMetadata
    {
        public int id { get; set; }
        public Nullable<int> idUsuario { get; set; }
        public Nullable<System.DateTime> fechaCita { get; set; }
        public Nullable<System.DateTime> horaCita { get; set; }
        public string descripcion { get; set; }
        public Nullable<int> idTipoCita { get; set; }

    }
    internal partial class DetalleOrdenMetadata
    {
        public int idOrden { get; set; }
        public int idProd { get; set; }
        public Nullable<double> subtotal { get; set; }
        public Nullable<int> cantidad { get; set; }
    }
    internal partial class MarcaMetadata
    {
        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Tipo de Producto")]
        [Required(ErrorMessage = "Debe seleccionar un tipo de producto")]
        public Nullable<int> idTipoProducto { get; set; }

        [Display(Name = "Nombre de la marca")]
        [Required(ErrorMessage = "El nombre es una campo requerido")]
        public string descripcion { get; set; }
    }
    internal partial class OrdenMetadata
    {
        public int id { get; set; }
        public Nullable<int> idUsuario { get; set; }
        public Nullable<double> impuesto { get; set; }
        public Nullable<double> total { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public Nullable<bool> estado { get; set; }
    }
    internal partial class RolMetadata
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public Nullable<bool> estado { get; set; }
    }
    internal partial class TelefonoUsuarioMetadata
    {
        public int id { get; set; }
        public string numeroTelf { get; set; }
        public Nullable<bool> estado { get; set; }
    }
    internal partial class TipoProductoMetadata
    {
        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Tipo de producto")]
        [Required(ErrorMessage = "El campo tipo producto es requerido")]
        public string descripcionTipo { get; set; }

    }
    internal partial class TipoCitaMetadata
    {
        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Descripción")]
        public string descripcion { get; set; }
    }
}
