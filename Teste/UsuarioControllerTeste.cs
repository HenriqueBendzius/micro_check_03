using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_app_domain;
using web_app_performance.Controllers;
using web_app_repository;

namespace Teste
{
    public class UsuarioControllerTeste
    {

        private readonly Mock<IUsuarioRepository> _userRepositoryMock;
        private readonly UsuarioController _controller;


        public UsuarioControllerTeste()
        {
            _userRepositoryMock = new Mock<IUsuarioRepository>();
            _controller = new UsuarioController(_userRepositoryMock.Object);


        }


        [Fact]
        public async Task Get_ListarUsuariosOk()
        {
            var usuarios = new List<Usuario>()
            {
                new Usuario()
                {
                    Email = "xxx@gmail.com",
                    Id = 1,
                    Nome = "rick silva"
                }
            };
            _userRepositoryMock.Setup(r => r.ListarUsuarios()).ReturnsAsync(usuarios);


            //Act

            var result = await _controller.GetUsuario();


            //ASSERTS
            Assert.IsType<OkObjectResult>(result);
            var OkResult = result as OkObjectResult;
            Assert.Equal(JsonConvert.SerializeObject(usuarios), JsonConvert.SerializeObject(OkResult.Value));







        }



        [Fact]
        public async Task Get_ListarRetornarNotFound()
        {
            _userRepositoryMock.Setup(u => u.ListarUsuarios()).ReturnsAsync((IEnumerable<Usuario>)null);

            var result = await _controller.GetUsuario();
            Assert.IsType<NotFoundResult>(result);

        }
        [Fact]
        public async Task Post_SalvarUsuario()
        {
            var usuario = new Usuario()
            {

                Id = 1,
                Email = "xxx@gmail.com",
                Nome = "ricky silva"

            };
            _userRepositoryMock.Setup(u => u.SalvarUsuario(It.IsAny<Usuario>())).Returns(Task.CompletedTask);

            var result = await  _controller.Post(usuario);

            //Assert.IsType<OkObjectResult>(result);
            _userRepositoryMock.Verify(u => u.SalvarUsuario(It.IsAny<Usuario>()), Times.Once);

        }
    }
}