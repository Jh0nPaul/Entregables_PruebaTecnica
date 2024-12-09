using CafeteriaHCCCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HccCafeteriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesController : ControllerBase
    {
        private readonly HccCafeteriaContext _context;

        public OrdenesController(HccCafeteriaContext context)
        {
            _context = context;
        }

        [HttpGet("total")]
        public async Task<IActionResult> GetTotalOrdenes()
        {
            var ordenes = await _context.TbHccOrdenes.Include(o => o.Mesa).ToListAsync();
            var resultado = ordenes.Select(o => new { o.Id, o.MesaId });

            return Ok(new { estatus = 200, mensaje = "Órdenes obtenidas correctamente", codigo = 1, datos = resultado });
        }

        [HttpGet("mesas-disponibles")]
        public async Task<IActionResult> GetMesasDisponibles()
        {
            var mesas = await _context.TbHccMesas.Where(m => m.Disponible == true).Select(m => new
            {
                m.Id,
                m.Lugares
            }).ToListAsync();

            return Ok(new { estatus = 200, mensaje = "Mesas disponibles obtenidas correctamente", codigo = 1, datos = mesas });
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrden([FromBody] TbHccOrdene nuevaOrden)
        {
            _context.TbHccOrdenes.Add(nuevaOrden);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTotalOrdenes), new { id = nuevaOrden.Id }, new { estatus = 201, mensaje = "Orden creada correctamente", codigo = 1 });
        }

        [HttpPut("{id}/agregar-producto")]
        public async Task<IActionResult> AgregarProducto(int id, [FromBody] TbHccDetallesOrden nuevoDetalle)
        {
            var orden = await _context.TbHccOrdenes.FindAsync(id);
            if (orden == null)
            {
                return NotFound(new { estatus = 404, mensaje = "Orden no encontrada", codigo = -1 });
            }

            nuevoDetalle.OrdenId = id;
            _context.TbHccDetallesOrdens.Add(nuevoDetalle);
            await _context.SaveChangesAsync();

            return Ok(new { estatus = 200, mensaje = "Producto agregado a la orden correctamente", codigo = 1 });
        }

        [HttpPut("{id}/cambiar-estatus")]
        public async Task<IActionResult> CambiarEstatus(int id, [FromBody] int nuevoEstatus)
        {
            var orden = await _context.TbHccOrdenes.FindAsync(id);
            if (orden == null)
            {
                return NotFound(new { estatus = 404, mensaje = "Orden no encontrada", codigo = -1 });
            }

            orden.Estatus = nuevoEstatus;
            await _context.SaveChangesAsync();

            return Ok(new { estatus = 200, mensaje = "Estatus de la orden actualizado correctamente", codigo = 1 });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrden(int id)
        {
            var orden = await _context.TbHccOrdenes.FindAsync(id);
            if (orden == null)
            {
                return NotFound(new { estatus = 404, mensaje = "Orden no encontrada", codigo = -1 });
            }

            orden.Estatus = 0; // Borrado lógico
            await _context.SaveChangesAsync();

            return Ok(new { estatus = 200, mensaje = "Orden eliminada correctamente", codigo = 1 });
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class MesasController : ControllerBase
    {
        private readonly HccCafeteriaContext _context;

        public MesasController(HccCafeteriaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMesas()
        {
            var mesas = await _context.TbHccMesas.ToListAsync();
            return Ok(new { estatus = 200, mensaje = "Mesas obtenidas correctamente", codigo = 1, datos = mesas });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMesa(int id)
        {
            var mesa = await _context.TbHccMesas.FindAsync(id);
            if (mesa == null)
            {
                return NotFound(new { estatus = 404, mensaje = "Mesa no encontrada", codigo = -1 });
            }
            return Ok(new { estatus = 200, mensaje = "Mesa obtenida correctamente", codigo = 1, datos = mesa });
        }

        [HttpPost]
        public async Task<IActionResult> CreateMesa([FromBody] TbHccMesa nuevaMesa)
        {
            _context.TbHccMesas.Add(nuevaMesa);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMesa), new { id = nuevaMesa.Id }, new { estatus = 201, mensaje = "Mesa creada correctamente", codigo = 1 });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMesa(int id, [FromBody] TbHccMesa mesaActualizada)
        {
            if (id != mesaActualizada.Id)
            {
                return BadRequest(new { estatus = 400, mensaje = "ID de la mesa no coincide", codigo = -1 });
            }

            _context.Entry(mesaActualizada).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TbHccMesas.Any(m => m.Id == id))
                {
                    return NotFound(new { estatus = 404, mensaje = "Mesa no encontrada", codigo = -1 });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { estatus = 200, mensaje = "Mesa actualizada correctamente", codigo = 1 });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMesa(int id)
        {
            var mesa = await _context.TbHccMesas.FindAsync(id);
            if (mesa == null)
            {
                return NotFound(new { estatus = 404, mensaje = "Mesa no encontrada", codigo = -1 });
            }

            _context.TbHccMesas.Remove(mesa);
            await _context.SaveChangesAsync();

            return Ok(new { estatus = 200, mensaje = "Mesa eliminada correctamente", codigo = 1 });
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly HccCafeteriaContext _context;

        public ProductosController(HccCafeteriaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductos()
        {
            var productos = await _context.TbHccProductos.ToListAsync();
            return Ok(new { estatus = 200, mensaje = "Productos obtenidos correctamente", codigo = 1, datos = productos });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducto(int id)
        {
            var producto = await _context.TbHccProductos.FindAsync(id);
            if (producto == null)
            {
                return NotFound(new { estatus = 404, mensaje = "Producto no encontrado", codigo = -1 });
            }
            return Ok(new { estatus = 200, mensaje = "Producto obtenido correctamente", codigo = 1, datos = producto });
        }

        [HttpPost]
        public async Task<IActionResult> CreateProducto([FromBody] TbHccProducto nuevoProducto)
        {
            _context.TbHccProductos.Add(nuevoProducto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProducto), new { id = nuevoProducto.Id }, new { estatus = 201, mensaje = "Producto creado correctamente", codigo = 1 });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(int id, [FromBody] TbHccProducto productoActualizado)
        {
            if (id != productoActualizado.Id)
            {
                return BadRequest(new { estatus = 400, mensaje = "ID del producto no coincide", codigo = -1 });
            }

            _context.Entry(productoActualizado).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TbHccProductos.Any(p => p.Id == id))
                {
                    return NotFound(new { estatus = 404, mensaje = "Producto no encontrado", codigo = -1 });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { estatus = 200, mensaje = "Producto actualizado correctamente", codigo = 1 });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.TbHccProductos.FindAsync(id);
            if (producto == null)
            {
                return NotFound(new { estatus = 404, mensaje = "Producto no encontrado", codigo = -1 });
            }

            _context.TbHccProductos.Remove(producto);
            await _context.SaveChangesAsync();

            return Ok(new { estatus = 200, mensaje = "Producto eliminado correctamente", codigo = 1 });
        }
    }
}
