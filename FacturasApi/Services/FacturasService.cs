using FacturasApi.DTO;
using FacturasApi.Models;

namespace FacturasApi.Services
{
    public class FacturasService
    {
        private readonly List<Factura> _facturas;

        public FacturasService(List<Factura> facturas)
        {
            _facturas = facturas;

            foreach (var factura in _facturas)
            {
                factura.TotalFactura = factura.DetalleFactura.Sum(d => d.TotalProducto);
            }
        }
        // Constructor sin parámetros (para pruebas unitarias)
        public FacturasService()
        {
            // Inicializa _facturas con una lista vacía en el caso de pruebas
            _facturas = new List<Factura>();
        }
        public virtual List<Factura> GetAll()
        {
            return _facturas;
        }

        public virtual List<Factura> GetByComprador(double rut) =>
            _facturas.Where(f => f.RUTComprador == rut).ToList();

        public virtual CompradorDTO GetTopComprador()
        {
            var comprador = _facturas
                .GroupBy(f => f.RUTComprador)
                .Select(g => new CompradorDTO { Rut = g.Key, Total = g.Sum(f => f.TotalFactura) })
                .OrderByDescending(c => c.Total)
                .FirstOrDefault();

            return comprador;
        }

        public virtual List<CompradorDTO> GetCompradoresTotales()
        {
            var compradoresTotales = _facturas
                .GroupBy(f => f.RUTComprador)
                .Select(g => new CompradorDTO { Rut = g.Key, Total = g.Sum(f => f.TotalFactura) })
                .ToList();

            return compradoresTotales;
        }



        public virtual List<IGrouping<double, Factura>> GetByComuna() =>
            _facturas.GroupBy(f => f.ComunaComprador).ToList();

        public virtual List<Factura> GetFacturasByComuna(double comuna) =>
            _facturas.Where(f => f.ComunaComprador == comuna).ToList();
    }
}
