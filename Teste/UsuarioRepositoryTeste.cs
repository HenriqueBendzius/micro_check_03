

using Moq;
using web_app_domain;
using web_app_repository;

namespace Teste
{
    public class UsuarioRepositoryTeste
    {
        [Fact]

        public async Task ListarUsuarios()
        {
            var usuarios = new List<Usuario>()
            {
                  new Usuario()
            {
                Email = "xxx@gmail.com",
                Id = 1,
                Nome = "rick silva"
            },
            new Usuario()

            {
                Email = "xxx@gmail.com",
                Id = 2,
                Nome = "rick silva"

            }
            };

          
             var userRepositoryMock = new Mock<IUsuarioRepository>();
            userRepositoryMock.Setup(u => u.ListarUsuarios()).ReturnsAsync(usuarios);
            var userRepository = userRepositoryMock.Object;

           var result = await userRepository.ListarUsuarios();

            Assert.Equal(usuarios , result);

        }

     

        [Fact]
        public async Task SalvarUsuario()
        {
            var usuario = new Usuario()
            {

                Id = 1,
                Email = "xxx@gmail.com",
                Nome = "ricky silva"

            };

            var userRepositoryMock = new Mock<IUsuarioRepository>();
            userRepositoryMock
                .Setup(u => u.SalvarUsuario(It.IsAny<Usuario>()))
                .Returns(Task.CompletedTask);
            var userRepository = userRepositoryMock.Object;

            await userRepository.SalvarUsuario(usuario);

            userRepositoryMock.Verify(u=>u.SalvarUsuario(It.IsAny<Usuario>()), Times.Once);
        }

    }
}

