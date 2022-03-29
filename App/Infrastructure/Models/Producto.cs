//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infrastructure.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producto()
        {
            this.DetalleOrdens = new HashSet<DetalleOrden>();
        }
    
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public Nullable<int> idTipoUnidad { get; set; }
        public Nullable<int> idTipoProducto { get; set; }
        public Nullable<int> idMarca { get; set; }
        public Nullable<double> precioUnitario { get; set; }
        public Nullable<int> cantidadNum { get; set; }
        public Nullable<double> cantidadPeso { get; set; }
        public byte[] imagen { get; set; }
        public bool estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleOrden> DetalleOrdens { get; set; }
        public virtual Marca Marca { get; set; }
        public virtual TipoProducto TipoProducto { get; set; }
        public virtual TipoUnidad TipoUnidad { get; set; }
    }
}
