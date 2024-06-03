using SmartTrade.Entities;
using SmartTrade.Logica.Services;
using SmartTrade.Persistencia.DataAccess;
using SmartTrade.Views;

namespace SmartTrade.Tests
{ 
    public class Tests
    {
        STService service = new STService(new STDAL(new SupabaseContext()));
        private Usuario usuario;
        private Perfil perfil;

        string contraseñaPrevia;
        string emailPrevio;

        [SetUp]
        public async Task Setup()
        {
                this.usuario = await service.GetUsuarioById("usuariotest");
                if (usuario == null) Assert.Fail("No se pudo encontrar al usuario con el nick proporcionado");
                this.contraseñaPrevia = this.usuario.Password;
                this.emailPrevio = this.usuario.Email;
                
                perfil = new Perfil();
        }

        [TestCase("nuevaContraseña", "nuevoEmail")]
        public async Task TestCambiarUsuario(string contraseñaEsperada, string emailEsperado)
        {
            if (contraseñaEsperada == contraseñaPrevia || emailEsperado == emailPrevio)
            {
                Assert.Fail("¡La contraseña esperada y la contraseña previa son iguales!");
            }

            perfil.ModificarContraseña(contraseñaEsperada);
            perfil.ModificarEmail(emailEsperado);

            service.SaveChanges();

            usuario = await service.GetUsuarioById("usuariotest");

            Assert.Equals(contraseñaEsperada, usuario.Password);
            Assert.Equals(emailEsperado, usuario.Email);
        }
    }
}