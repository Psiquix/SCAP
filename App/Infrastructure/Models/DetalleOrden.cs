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
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(DetalleOrdenMetadata))]
    public partial class DetalleOrden
    {
        public int idOrden { get; set; }
        public int idProd { get; set; }
        public Nullable<double> subtotal { get; set; }
        public Nullable<int> cantidad { get; set; }
    
        public virtual Orden Orden { get; set; }
        public virtual Producto Producto { get; set; }
    }
}