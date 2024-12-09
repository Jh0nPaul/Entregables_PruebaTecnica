using System;
using System.Collections.Generic;

namespace CafeteriaHCCCrud.Models
{
    public partial class TbHccAlmacen
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }

        public virtual TbHccProducto Producto { get; set; } = null!;
    }
}
