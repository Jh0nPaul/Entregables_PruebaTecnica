using System;
using System.Collections.Generic;

namespace CafeteriaHCCCrud.Models
{
    public partial class TbHccProducto
    {
        public TbHccProducto()
        {
            TbHccAlmacens = new HashSet<TbHccAlmacen>();
            TbHccDetallesOrdens = new HashSet<TbHccDetallesOrden>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<TbHccAlmacen> TbHccAlmacens { get; set; }
        public virtual ICollection<TbHccDetallesOrden> TbHccDetallesOrdens { get; set; }
    }
}
