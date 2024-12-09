using System;
using System.Collections.Generic;

namespace CafeteriaHCCCrud.Models
{
    public partial class TbHccOrdene
    {
        public TbHccOrdene()
        {
            TbHccDetallesOrdens = new HashSet<TbHccDetallesOrden>();
        }

        public int Id { get; set; }
        public int MesaId { get; set; }
        public int Estatus { get; set; }
        public DateTime FechaCreacion { get; set; }

        public virtual TbHccMesa Mesa { get; set; } = null!;
        public virtual ICollection<TbHccDetallesOrden> TbHccDetallesOrdens { get; set; }
    }
}
