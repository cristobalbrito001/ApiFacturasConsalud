using FacturasApi.Controllers;
using FacturasApi.Services;
using FacturasApi.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Collections.Generic;
using FacturasApi.DTO;

namespace FacturasApi.Tests.Controllers
{
    public class FacturasControllerTests
    {
        private readonly Mock<FacturasService> _mockService; // Mock del servicio
        private readonly FacturasController _controller; // Instancia del controlador

        // Constructor
        public FacturasControllerTests()
        {
            // Usar el constructor sin parámetros
            _mockService = new Mock<FacturasService>();

            // Inicializar el controlador con el mock
            _controller = new FacturasController(_mockService.Object);
        }

        // Prueba 1: Devuelve 204 No Content cuando no hay datos
        [Fact]
        public void GetAll_ShouldReturnNoContent_WhenNoData()
        {
            // Arrange: Configurar el mock para devolver una lista vacía de facturas
            _mockService.Setup(s => s.GetAll()).Returns(new List<Factura>());

            // Act: Llamar al método del controlador
            var result = _controller.GetAll();

            // Assert: Verificar que el resultado sea NoContent
            var actionResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, actionResult.StatusCode);
        }

        // Prueba 2: Devuelve 200 OK cuando hay datos
        [Fact]
        public void GetAll_ShouldReturnOk_WhenDataExists()
        {
            // Arrange: Configurar el mock para devolver datos de prueba
            var data = new List<Factura>
            {
                new Factura { NumeroDocumento = 1, RUTComprador = 12345678, TotalFactura = 1000 },
                new Factura { NumeroDocumento = 2, RUTComprador = 87654321, TotalFactura = 2000 }
            };
            _mockService.Setup(s => s.GetAll()).Returns(data);

            // Act: Llamar al método del controlador
            var result = _controller.GetAll();

            // Assert: Verificar que el resultado sea Ok con los datos
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, actionResult.StatusCode);
            Assert.Equal(data, ((OkObjectResult)actionResult).Value);
        }

        // Prueba 3: Devuelve 204 No Content cuando no hay datos para GetByComprador
        [Fact]
        public void GetByComprador_ShouldReturnNoContent_WhenNoData()
        {
            // Arrange: Configurar el mock para devolver una lista vacía de facturas
            _mockService.Setup(s => s.GetByComprador(It.IsAny<double>())).Returns(new List<Factura>());

            // Act: Llamar al método del controlador
            var result = _controller.GetByComprador(12345678);

            // Assert: Verificar que el resultado sea NoContent
            var actionResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, actionResult.StatusCode);
        }

        // Prueba 4: Devuelve 200 OK cuando hay datos para GetByComprador
        [Fact]
        public void GetByComprador_ShouldReturnOk_WhenDataExists()
        {
            // Arrange: Configurar el mock para devolver datos de prueba
            var data = new List<Factura>
            {
                new Factura { NumeroDocumento = 1, RUTComprador = 12345678, TotalFactura = 500 }
            };
            _mockService.Setup(s => s.GetByComprador(It.IsAny<double>())).Returns(data);

            // Act: Llamar al método del controlador
            var result = _controller.GetByComprador(12345678);

            // Assert: Verificar que el resultado sea Ok con los datos
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, actionResult.StatusCode);
            Assert.Equal(data, ((OkObjectResult)actionResult).Value);
        }

        // Prueba 5: Devuelve 204 No Content cuando no hay datos para GetTopComprador
        [Fact]
        public void GetTopComprador_ShouldReturnNoContent_WhenNoData()
        {
            // Arrange: Configurar el mock para devolver null del tipo esperado (CompradorDTO)
            _mockService.Setup(s => s.GetTopComprador()).Returns((FacturasApi.DTO.CompradorDTO)null);

            // Act: Llamar al método del controlador
            var result = _controller.GetTopComprador();

            // Assert: Verificar que el resultado sea NoContent
            var actionResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, actionResult.StatusCode);
        }

        // Prueba 6: Devuelve 200 OK cuando hay datos para GetTopComprador
        [Fact]
        public void GetTopComprador_ShouldReturnOk_WhenDataExists()
        {
            // Arrange: Configurar el mock para devolver datos de prueba como un CompradorDTO
            var data = new CompradorDTO { Rut = 12345678, Total = 500 };
            _mockService.Setup(s => s.GetTopComprador()).Returns(data);

            // Act: Llamar al método del controlador
            var result = _controller.GetTopComprador();

            // Assert: Verificar que el resultado sea Ok con los datos
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, actionResult.StatusCode);
            Assert.Equal(data, ((OkObjectResult)actionResult).Value);
        }

        // Prueba 7: Devuelve 204 No Content cuando no hay datos para GetCompradoresTotales
        [Fact]
        public void GetCompradoresTotales_ShouldReturnNoContent_WhenNoData()
        {
            // Arrange: Configurar el mock para devolver una lista vacía de CompradorDTO
            _mockService.Setup(s => s.GetCompradoresTotales()).Returns(new List<FacturasApi.DTO.CompradorDTO>());

            // Act: Llamar al método del controlador
            var result = _controller.GetCompradoresTotales();

            // Assert: Verificar que el resultado sea NoContent
            var actionResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, actionResult.StatusCode);
        }

        [Fact]
        public void GetCompradoresTotales_ShouldReturnOk_WhenDataExists()
        {
            // Arrange: Configurar el mock para devolver una lista de CompradorDTO con datos
            var data = new List<FacturasApi.DTO.CompradorDTO>
    {
        new FacturasApi.DTO.CompradorDTO { Rut = 12345678, Total = 1000 },
        new FacturasApi.DTO.CompradorDTO { Rut = 87654321, Total = 2000 }
    };
            _mockService.Setup(s => s.GetCompradoresTotales()).Returns(data);

            // Act: Llamar al método del controlador
            var result = _controller.GetCompradoresTotales();

            // Assert: Verificar que el resultado sea Ok con los datos
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, actionResult.StatusCode);
            Assert.Equal(data, ((OkObjectResult)actionResult).Value);
        }


        // Prueba 9: Devuelve 204 No Content cuando no hay datos para GetByComuna
        [Fact]
        public void GetByComuna_ShouldReturnNoContent_WhenNoData()
        {
            // Arrange: Configurar el mock para devolver una lista vacía de grupos de facturas
            _mockService.Setup(s => s.GetByComuna()).Returns(new List<IGrouping<double, Factura>>());

            // Act: Llamar al método del controlador
            var result = _controller.GetByComuna();

            // Assert: Verificar que el resultado sea NoContent
            var actionResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, actionResult.StatusCode);
        }


        // Prueba 10: Devuelve 200 OK cuando hay datos para GetByComuna
        [Fact]
        public void GetByComuna_ShouldReturnOk_WhenDataExists()
        {
            // Arrange: Configurar el mock para devolver una lista de grupos de facturas
            var facturas = new List<Factura>
    {
        new Factura { NumeroDocumento = 1, ComunaComprador = 1, TotalFactura = 500 },
        new Factura { NumeroDocumento = 2, ComunaComprador = 1, TotalFactura = 1000 },
        new Factura { NumeroDocumento = 3, ComunaComprador = 2, TotalFactura = 1500 }
    };

            var groupedFacturas = facturas
                .GroupBy(f => f.ComunaComprador)  // Agrupando por "Comuna"
                .ToList();

            _mockService.Setup(s => s.GetByComuna()).Returns(groupedFacturas);

            // Act: Llamar al método del controlador
            var result = _controller.GetByComuna();

            // Assert: Verificar que el resultado sea Ok con los datos agrupados
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, actionResult.StatusCode);
            Assert.Equal(groupedFacturas, ((OkObjectResult)actionResult).Value);
        }

    }
}
