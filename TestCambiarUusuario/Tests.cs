using SmartTrade.Entities;
using SmartTrade.Logica.Services;
using SmartTrade.Persistencia.DataAccess;
using SmartTrade.Views;
using SmartTrade.Logica.Estado;

namespace SmartTrade.Tests
{ 
    public class Tests
    {
        Pedido pedido;
        IEstadoPedido estadoPreparacion;
        IEstadoPedido estadoEnviado;
        IEstadoPedido estadoEntregado;

        [SetUp]
        public void Setup()
        {
            pedido = new Pedido();
            estadoPreparacion = new EstadoEnPreparacion(pedido);
            estadoEnviado = new EstadoEnviado(pedido);
            estadoEntregado = new EstadoEntregado(pedido);
        }

        [TestCase]
        public void TestCambiarEstadoPreparacion()
        {
            pedido.CambiarEstado(estadoPreparacion);
            Assert.That(pedido.Estado, Is.EqualTo("Pedido en preparación"));
        }

        [TestCase]
        public void TestCambiarEstadoEnviado()
        {
            pedido.CambiarEstado(estadoEnviado);
            Assert.That(pedido.Estado, Is.EqualTo("Pedido enviado"));
        }

        [TestCase]
        public void TestCambiarEstadoEntregado()
        {
            pedido.CambiarEstado(estadoEntregado);
            Assert.That(pedido.Estado, Is.EqualTo("Pedido entregado"));
        }
    }
}