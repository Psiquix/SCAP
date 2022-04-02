using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    [MetadataType(typeof(UsuarioMetadata))]
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

    [MetadataType(typeof(ProductoMetadata))]
    internal partial class ProductoMetadata
    {
        [Display(Name = "Id")]
        public int id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Use solo letras y espacios")]
        [Display(Name = "Nombre del producto")]
        public string nombre { get; set; }

        [Display(Name = "Descripción del producto")]
        [Required(ErrorMessage = "La descripción es requerida")]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Use solo letras y espacios")]
        public string descripcion { get; set; }


        [Display(Name = "Tipo de unidad")]
        [Required(ErrorMessage = "Debe seleccionar un tipo de unidad")]
        public int idTipoUnidad { get; set; }

        [Display(Name = "Tipo de producto")]
        [Required(ErrorMessage = "Debe seleccionar un tipo de producto")]
        public int idTipoProducto { get; set; }

        [Display(Name = "Marca")]
        [Required(ErrorMessage = "Debe seleccionar un tipo de marca")]
        public int idMarca { get; set; }

        [Display(Name = "Precio unitario")]
        [DataType(DataType.Currency)]
        [MaxLength(12)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El precio debe ser un valor numérico")]
        [Required(ErrorMessage = "El precio es requerido")]
        public double precioUnitario { get; set; }

        [Display(Name = "Cantidad númerica")]
        [MaxLength(12)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "La cantidad debe ser un valor numérico")]
        public int cantidadNum { get; set; }

        [Display(Name = "Cantidad mínima en inventario")]
        [MaxLength(12)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "La cantidad debe ser un valor numérico")]
        public Nullable<int> cantMinNum { get; set; }

        [Display(Name = "Cantidad en peso")]
        [MaxLength(12)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El peso debe ser un valor numérico")]
        public double cantidadPeso { get; set; }

        [Display(Name = "Imagen del producto")]
        [Required(ErrorMessage = "La imagen es requerida")]
        public byte[] imagen { get; set; }

        [Display(Name = "Estado")]
        public bool estado { get; set; }
    }

    [MetadataType(typeof(CitaMetadata))]
    internal partial class CitaMetadata
    {
        [Display(Name = "Id")]
        public int id { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Tipo de la cita")]
        public Nullable<int> idTipoCita { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Nombre del cliente")]
        public string nombreCliente { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Apellidos del cliente")]
        public string apellidosCliente { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Teléfono del cliente")]
        [Range(10000000, 99999999, ErrorMessage = "El formato debe ser 88888888")]
        public string telefonoCliente { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Correo del cliente")]
        public string correoCliente { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha deseada")]
        public Nullable<System.DateTime> fechaCita { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Hora deseada")]
        public Nullable<System.TimeSpan> horaCita { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Motivo de la cita")]
        public string descripcion { get; set; }
        public Nullable<bool> estado { get; set; }

        public virtual TipoCita TipoCita { get; set; }
    }
    internal partial class DetalleOrdenMetadata
    {
        public int idOrden { get; set; }
        public int idProd { get; set; }
        [Display(Name = "Subtotal")]
        public Nullable<double> subtotal { get; set; }
        [Display(Name = "Cantidad")]
        public Nullable<int> cantidad { get; set; }
    }

    [MetadataType(typeof(MarcaMetadata))]
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

    [MetadataType(typeof(OrdenMetadata))]
    internal partial class OrdenMetadata
    {
        [Display(Name = "Id")]
        public int id { get; set; }
        [Display(Name = "Impuesto")]
        public Nullable<double> impuesto { get; set; }
        [Display(Name = "Total")]
        public Nullable<double> total { get; set; }
        [Display(Name = "Fecha")]
        public Nullable<System.DateTime> fecha { get; set; }
        [Display(Name = "Estado")]
        public Nullable<bool> estado { get; set; }

        [Display(Name = "Nombre del cliente")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string nombreCliente { get; set; }

        [Display(Name = "Apellidos del cliente")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string ApellidoCliente { get; set; }

        [Display(Name = "Descripción del cliente")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string DescripcionCliente { get; set; }

        [Display(Name = "Teléfono del cliente")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string numeroCliente { get; set; }

        [Display(Name = "E-mail del cliente")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string correoCliente { get; set; }
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

    [MetadataType(typeof(TipoProductoMetadata))]
    internal partial class TipoProductoMetadata
    {
        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Tipo de producto")]
        [Required(ErrorMessage = "El campo tipo producto es requerido")]
        public string descripcionTipo { get; set; }

        [Display(Name = "Estado")]
        public bool estado { get; set; }

    }

    [MetadataType(typeof(TipoCitaMetadata))]
    internal partial class TipoCitaMetadata
    {
        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Descripción")]
        public string descripcion { get; set; }
    }

    [MetadataType(typeof(TipoUnidadMetadata))]
    internal partial class TipoUnidadMetadata
    {
        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Descripción")]
        public string descripcionTipo { get; set; }

        public bool estado { get; set; }
    }

}
