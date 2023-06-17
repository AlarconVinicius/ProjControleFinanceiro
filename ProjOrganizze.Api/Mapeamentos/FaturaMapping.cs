﻿using ProjOrganizze.Api.Dominio.DTOs.Cartao;
using ProjOrganizze.Api.Dominio.DTOs.Fatura;
using ProjOrganizze.Api.Dominio.DTOs.Transacao;
using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Extensions;

namespace ProjOrganizze.Api.Mapeamentos
{
    public class FaturaMapping
    {
        public FaturaViewDTO MapToGetDTO(Fatura objeto)
        {
            IEnumerable<TransacaoViewDTO> transacoesMapeadas = Enumerable.Empty<TransacaoViewDTO>();
            if (objeto.Transacoes != null)
            {
                transacoesMapeadas = objeto.Transacoes.Select(x => x.ToGetDTO());
            }
            return new FaturaViewDTO
            {
                Id = objeto.Id,
                Mes = objeto.Mes,
                Ano = objeto.Ano,
                Nome = objeto.Nome,
                Cartao = objeto.Cartao != null ? objeto.Cartao.Nome : string.Empty,
                DataVencimento = objeto.DataVencimento.ToString("dd/MM/yyyy"),
                DataFechamento = objeto.DataFechamento.ToString("dd/MM/yyyy"),
                Transacoes = transacoesMapeadas
            };
        }
    }
}