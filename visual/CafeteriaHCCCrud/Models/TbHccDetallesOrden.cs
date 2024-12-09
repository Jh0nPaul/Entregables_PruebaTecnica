using System;
using System.Collections.Generic;

namespace CafeteriaHCCCrud.Models
{
    public partial class TbHccDetallesOrden
    {
        public int Id { get; set; }
        public int OrdenId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }

        public virtual TbHccOrdene Orden { get; set; } = null!;
        public virtual TbHccProducto Producto { get; set; } = null!;
    }
}
