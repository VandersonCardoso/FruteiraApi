using FruteiraApi.Controllers;
using FruteiraApi.Core.Domain.Models;
using FruteiraApi.Core.Domain.Responses;
using FruteiraApi.Core.Interfaces;
using FruteiraApi.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FruteiraApi.Tests
{
    [TestFixture]
    public class FrutaUnitTests
    {
        private Mock<IFrutaService> mockFrutaService;

        #region Setup
        [SetUp]
        public void Setup()
        {
            mockFrutaService = new Mock<IFrutaService>();
        }
        #endregion

        #region GetConsultarFrutas_ModelStateInvalid
        [Test]
        public async Task GetConsultarFrutas_ModelStateInvalid()
        {
            FrutaController frutaController = new FrutaController(mockFrutaService.Object);
            frutaController.ModelState.AddModelError("Key", "errorMessage");
            var result = frutaController.GetConsultarFrutas();
            Assert.IsFalse(frutaController.ModelState.IsValid);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region GetConsultarFrutas_BadRequest
        [Test]
        public async Task GetConsultarFrutas_BadRequest()
        {
            mockFrutaService.Setup(fru => fru.ConsultarFrutas()).Returns(new List<Frutas>());
            FrutaController frutaController = new FrutaController(mockFrutaService.Object);
            var result = frutaController.GetConsultarFrutas();
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region GetConsultarFrutas_Ok
        [Test]
        public async Task GetConsultarFrutas_Ok()
        {
            mockFrutaService.Setup(fru => fru.ConsultarFrutas()).Returns(new List<Frutas> {
                new Frutas
                {
                    Nome = "Uva",
                    Descricao = "Fruta de cor roxa",
                    Foto = null,
                    QuantidadeEstoque = 100,
                    Valor = 4.50M
                }
            });
            FrutaController frutaController = new FrutaController(mockFrutaService.Object);
            var result = frutaController.GetConsultarFrutas();
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        #endregion

        #region GetConsultarFruta_ModelStateInvalid
        [TestCase(1)]
        public async Task GetConsultarFruta_ModelStateInvalid(int id)
        {
            FrutaController frutaController = new FrutaController(mockFrutaService.Object);
            frutaController.ModelState.AddModelError("Key", "errorMessage");
            var result = frutaController.GetConsultarFruta(id);
            Assert.IsFalse(frutaController.ModelState.IsValid);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region GetConsultarFruta_BadRequest
        [TestCase(999)]
        public async Task GetConsultarFruta_BadRequest(int id)
        {
            mockFrutaService.Setup(fru => fru.ConsultarFruta(It.IsAny<int>())).Returns((Frutas)null);
            FrutaController frutaController = new FrutaController(mockFrutaService.Object);
            var result = frutaController.GetConsultarFruta(id);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region GetConsultarFruta_Ok
        [TestCase(1)]
        public async Task GetConsultarFruta_Ok(int id)
        {
            mockFrutaService.Setup(fru => fru.ConsultarFruta(It.IsAny<int>())).Returns(new Frutas
            {
                Nome = "Uva",
                Descricao = "Fruta de cor roxa",
                Foto = null,
                QuantidadeEstoque = 100,
                Valor = 4.50M
            });
            FrutaController frutaController = new FrutaController(mockFrutaService.Object);
            var result = frutaController.GetConsultarFruta(id);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        #endregion

        #region PostIncluirFruta_ModelStateInvalid
        [Test]
        public async Task PostIncluirFruta_ModelStateInvalid()
        {
            FrutaController frutaController = new FrutaController(mockFrutaService.Object);
            frutaController.ModelState.AddModelError("Key", "errorMessage");
            var result = await frutaController .PostIncluirFruta(new Fruta());
            Assert.IsFalse(frutaController.ModelState.IsValid);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region PostIncluirFruta_BadRequest
        [Test]
        public async Task PostIncluirFruta_BadRequest()
        {
            mockFrutaService.Setup(fru => fru.IncluirFruta(It.IsAny<Fruta>())).ReturnsAsync(new IncluirFrutaResponse { Mensagem = "Erro ao incluir fruta.", FrutaIncluida = false });
            FrutaController frutaController = new FrutaController(mockFrutaService.Object);
            var result = await frutaController .PostIncluirFruta(new Fruta());
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region PostIncluirFruta_Ok
        [Test]
        public async Task PostIncluirFruta_Ok()
        {
            mockFrutaService.Setup(fru => fru.IncluirFruta(It.IsAny<Fruta>())).ReturnsAsync(new IncluirFrutaResponse { Mensagem = "Fruta incluída com sucesso.", FrutaIncluida = true });
            FrutaController frutaController = new FrutaController(mockFrutaService.Object);
            var result = await frutaController .PostIncluirFruta(new Fruta());
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        #endregion

        #region PutAtualizarFruta_ModelStateInvalid
        [TestCase(1)]
        public async Task PutAtualizarFruta_ModelStateInvalid(int id)
        {
            FrutaController frutaController = new FrutaController(mockFrutaService.Object);
            frutaController.ModelState.AddModelError("Key", "errorMessage");
            var result = await frutaController.PutAtualizarFruta(id, new Fruta());
            Assert.IsFalse(frutaController.ModelState.IsValid);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region PutAtualizarFruta_BadRequest
        [TestCase(999)]
        public async Task PutAtualizarFruta_BadRequest(int id)
        {
            mockFrutaService.Setup(fru => fru.AtualizarFruta(It.IsAny<int>(), It.IsAny<Fruta>())).ReturnsAsync(new AtualizarFrutaResponse { Mensagem = "Erro ao atualizar fruta.", FrutaAtualizada = false });
            FrutaController frutaController = new FrutaController(mockFrutaService.Object);
            var result = await frutaController .PutAtualizarFruta(id, new Fruta());
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region PutAtualizarFruta_Ok
        [TestCase(1)]
        public async Task PutAtualizarFruta_Ok(int id)
        {
            mockFrutaService.Setup(fru => fru.AtualizarFruta(It.IsAny<int>(), It.IsAny<Fruta>())).ReturnsAsync(new AtualizarFrutaResponse { Mensagem = "Fruta atualizada com sucesso.", FrutaAtualizada = true });
            FrutaController frutaController = new FrutaController(mockFrutaService.Object);
            var result = await frutaController.PutAtualizarFruta(id, new Fruta());
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        #endregion

        #region DeleteExcluirFruta_ModelStateInvalid
        [TestCase(1)]
        public async Task DeleteExcluirFruta_ModelStateInvalid(int id)
        {
            FrutaController frutaController = new FrutaController(mockFrutaService.Object);
            frutaController.ModelState.AddModelError("Key", "errorMessage");
            var result = await frutaController.DeleteExcluirFruta(id);
            Assert.IsFalse(frutaController.ModelState.IsValid);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region DeleteExcluirFruta_BadRequest
        [TestCase(999)]
        public async Task DeleteExcluirFruta_BadRequest(int id)
        {
            mockFrutaService.Setup(fru => fru.ExcluirFruta(It.IsAny<int>())).ReturnsAsync(new ExcluirFrutaResponse { Mensagem = "Erro ao excluir fruta.", FrutaExcluida = false });
            FrutaController frutaController = new FrutaController(mockFrutaService.Object);
            var result = await frutaController.DeleteExcluirFruta(id);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region DeleteExcluirFruta_Ok
        [TestCase(1)]
        public async Task DeleteExcluirFruta_Ok(int id)
        {
            mockFrutaService.Setup(fru => fru.ExcluirFruta(It.IsAny<int>())).ReturnsAsync(new ExcluirFrutaResponse { Mensagem = "Fruta excluída com sucesso.", FrutaExcluida = true });
            FrutaController frutaController = new FrutaController(mockFrutaService.Object);
            var result = await frutaController .DeleteExcluirFruta(id);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        #endregion
    }
}