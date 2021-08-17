using FruteiraApi.Core.Domain.Models;
using FruteiraApi.Core.Domain.Responses;
using FruteiraApi.Core.Interfaces;
using FruteiraApi.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FruteiraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrutaController : ControllerBase
    {
        private readonly IFrutaService _frutaService;

        #region FrutaController
        public FrutaController(IFrutaService frutaService)
        {
            _frutaService = frutaService;
        }
        #endregion

        #region GetConsultarFrutas
        /// <summary alignment="right">
        /// Endpoint para listagem das frutas
        /// </summary>
        /// <remarks>
        /// Este endpoint tem como objetivo retornar uma lista de frutas cadastradas
        /// </remarks>
        /// <returns>Códigos de retorno para o endpoint ConsultarFrutas</returns>
        /// <response code="200">Código de retorno caso o endpoint retorne a lista de frutas.</response>
        /// <response code="400">Código de retorno caso ocorra um erro ao obter a lista de frutas.</response>
        [HttpGet("/ConsultarFrutas")]
        [ProducesResponseType(typeof(List<Frutas>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<Frutas>), (int)HttpStatusCode.BadRequest)]
        public IActionResult GetConsultarFrutas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = _frutaService.ConsultarFrutas();

            if (response.Count > 0)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        #endregion

        #region GetConsultarFruta
        /// <summary alignment="right">
        /// Endpoint para consulta de uma fruta
        /// </summary>
        /// <remarks>
        /// Este endpoint tem como objetivo retornar as informações de uma fruta cadastrada, através do seu ID.
        /// </remarks>
        /// <returns>Códigos de retorno para o endpoint ConsultarFruta</returns>
        /// <response code="200">Código de retorno caso o endpoint retorne as informações da fruta.</response>
        /// <response code="400">Código de retorno caso ocorra um erro ao obter a fruta informada ou caso o id informado seja inválido.</response>
        [HttpGet("/ConsultarFruta")]
        [ProducesResponseType(typeof(Fruta), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Fruta), (int)HttpStatusCode.BadRequest)]
        public IActionResult GetConsultarFruta(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = _frutaService.ConsultarFruta(id);

            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        #endregion

        #region PostIncluirFruta
        /// <summary alignment="right">
        /// Endpoint para inclusão de uma fruta
        /// </summary>
        /// <remarks>
        /// Este endpoint tem como incluir uma fruta no cadastro de frutas.
        /// </remarks>
        /// <returns>Códigos de retorno para o endpoint IncluirFruta</returns>
        /// <response code="200">Código de retorno caso o endpoint inclua a fruta corretamente.</response>
        /// <response code="400">Código de retorno caso ocorra um erro ao incluir a fruta informada ou caso o conteúdo da requisição esteja divergente da especificação.</response>
        [HttpPost("/IncluirFruta")]
        [ProducesResponseType(typeof(IncluirFrutaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IncluirFrutaResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostIncluirFruta([FromBody] Fruta item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _frutaService.IncluirFruta(item);

            if (response.FrutaIncluida)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        #endregion

        #region PutAtualizarFruta
        /// <summary alignment="right">
        /// Endpoint para atualização de uma fruta
        /// </summary>
        /// <remarks>
        /// Este endpoint tem como atualizar as informações de uma fruta no seu respectivo cadastro.
        /// </remarks>
        /// <returns>Códigos de retorno para o endpoint AtualizarrFruta</returns>
        /// <response code="200">Código de retorno caso o endpoint atualize as informações da fruta corretamente.</response>
        /// <response code="400">Código de retorno caso ocorra um erro ao atualizar as informações da fruta informada ou caso o conteúdo da requisição esteja divergente da especificação.</response>
        [HttpPut("/AtualizarFruta")]
        [ProducesResponseType(typeof(AtualizarFrutaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AtualizarFrutaResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutAtualizarFruta(int id,[FromBody] Fruta item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _frutaService.AtualizarFruta(id, item);

            if (response.FrutaAtualizada)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        #endregion

        #region DeleteExcluirFruta
        /// <summary alignment="right">
        /// Endpoint para exclusão de uma fruta
        /// </summary>
        /// <remarks>
        /// Este endpoint tem como excluir uma fruta do cadastro de frutas.
        /// </remarks>
        /// <returns>Códigos de retorno para o endpoint ExcluirFruta</returns>
        /// <response code="200">Código de retorno caso o endpoint exclua a fruta corretamente.</response>
        /// <response code="400">Código de retorno caso ocorra um erro ao excluir a fruta informada ou caso o conteúdo da requisição esteja divergente da especificação.</response>
        [HttpDelete("/ExcluirFruta")]
        [ProducesResponseType(typeof(ExcluirFrutaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ExcluirFrutaResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteExcluirFruta(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _frutaService.ExcluirFruta(id);

            if (response.FrutaExcluida)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        #endregion
    }
}
