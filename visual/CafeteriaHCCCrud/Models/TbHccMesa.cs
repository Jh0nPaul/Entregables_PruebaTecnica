using System;
using System.Collections.Generic;

namespace CafeteriaHCCCrud.Models
{
    public partial class TbHccMesa
    {
        public TbHccMesa()
        {
            TbHccOrdenes = new HashSet<TbHccOrdene>();
        }

        public int Id { get; set; }
        public int Lugares { get; set; }
        public bool? Disponible { get; set; }

        public virtual ICollection<TbHccOrdene> TbHccOrdenes { get; set; }
    }
}
