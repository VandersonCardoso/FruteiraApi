using FruteiraApi.Core.Domain.Models;
using FruteiraApi.Core.Domain.Responses;
using FruteiraApi.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FruteiraApi.Core.Interfaces
{
    public interface IFrutaService
    {
        List<Frutas> ConsultarFrutas();
        Frutas ConsultarFruta(int id);
        Task<IncluirFrutaResponse> IncluirFruta(Fruta item);
        Task<AtualizarFrutaResponse> AtualizarFruta(int id, Fruta item);
        Task<ExcluirFrutaResponse> ExcluirFruta(int id);
    }
}
