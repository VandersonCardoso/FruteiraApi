using FruteiraApi.Core.Domain.Models;
using FruteiraApi.Core.Domain.Responses;
using FruteiraApi.Core.Interfaces;
using FruteiraApi.Data.Contexts;
using FruteiraApi.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FruteiraApi.Services
{
    public class FrutaService : IFrutaService
    {
        private readonly FruteiraDbContext _fruteiraDbContext;

        #region FrutaService
        public FrutaService(FruteiraDbContext fruteiraDbContext)
        {
            _fruteiraDbContext = fruteiraDbContext;
        }
        #endregion

        #region ConsultarFrutas
        public List<Frutas> ConsultarFrutas()
        {
            return _fruteiraDbContext.Frutas.ToList();
        }
        #endregion

        #region ConsultarFruta
        public Frutas ConsultarFruta(int id)
        {
            return _fruteiraDbContext.Frutas.FirstOrDefault(x => x.Id == id);
        }
        #endregion

        #region IncluirFruta
        public async Task<IncluirFrutaResponse> IncluirFruta(Fruta item)
        {
            try
            {
                var maxId = await _fruteiraDbContext.Frutas.MaxAsync(x => (int?)x.Id);

                var fruta = new Frutas
                {
                    Id = maxId.HasValue ? maxId.Value + 1 : 1,
                    Nome = item.Nome,
                    Descricao = item.Descricao,
                    Foto = item.Foto,
                    QuantidadeEstoque = item.QuantidadeEstoque ?? 0,
                    Valor = item.Valor ?? 0.00M
                };

                _fruteiraDbContext.Add(fruta);
                _fruteiraDbContext.SaveChanges();

                return new IncluirFrutaResponse { Mensagem = "Fruta incluída com sucesso.", FrutaIncluida = true };
            }
            catch (Exception ex)
            {
                return new IncluirFrutaResponse { Mensagem = string.Format("Ocorreu um erro ao incluir a fruta. Erro: {0}", ex.Message), FrutaIncluida = false };
            }            
        }
        #endregion

        #region AtualizarFruta
        public async Task<AtualizarFrutaResponse> AtualizarFruta(int id, Fruta item)
        {
            try
            {
                var fruta = await _fruteiraDbContext.Frutas.FirstOrDefaultAsync(x => x.Id == id);

                if (fruta != null)
                {
                    if (!string.IsNullOrEmpty(item.Nome))
                    {
                        fruta.Nome = item.Nome;
                    }

                    if (!string.IsNullOrEmpty(item.Descricao))
                    {
                        fruta.Descricao = item.Descricao;
                    }

                    if (item.Foto != null)
                    {
                        fruta.Foto = item.Foto;
                    }

                    if (item.QuantidadeEstoque.HasValue)
                    {
                        fruta.QuantidadeEstoque = item.QuantidadeEstoque.Value;
                    }

                    if (item.Valor.HasValue)
                    {
                        fruta.QuantidadeEstoque = item.QuantidadeEstoque.Value;
                    }

                    _fruteiraDbContext.Update(fruta);
                    _fruteiraDbContext.SaveChanges();

                    return new AtualizarFrutaResponse { Mensagem = "Fruta atualizada com sucesso.", FrutaAtualizada = true };
                }
                else
                {
                    return new AtualizarFrutaResponse { Mensagem = String.Format("Fruta de ID [{0}] não localizada", id), FrutaAtualizada = false };
                }                
            }
            catch (Exception ex)
            {
                return new AtualizarFrutaResponse { Mensagem = string.Format("Ocorreu um erro ao atualizar as informações da fruta. Erro: {0}", ex.Message), FrutaAtualizada = false };
            }
        }
        #endregion

        #region ExcluirFruta
        public async Task<ExcluirFrutaResponse> ExcluirFruta(int id)
        {
            try
            {
                var fruta = await _fruteiraDbContext.Frutas.FirstOrDefaultAsync(x => x.Id == id);

                if (fruta != null)
                {
                    _fruteiraDbContext.Remove(fruta);
                    _fruteiraDbContext.SaveChanges();

                    return new ExcluirFrutaResponse { Mensagem = "Fruta excluída com sucesso.", FrutaExcluida = true };
                }
                else
                {
                    return new ExcluirFrutaResponse { Mensagem = String.Format("Fruta de ID [{0}] não localizada", id), FrutaExcluida = false };
                }                
            }
            catch (Exception ex)
            {
                return new ExcluirFrutaResponse { Mensagem = string.Format("Ocorreu um erro ao excluir a fruta. Erro: {0}", ex.Message), FrutaExcluida = false };
            }            
        }
        #endregion
    }
}
