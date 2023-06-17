﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProjOrganizze.Api.Dominio.DTOs.Cartao;
using ProjOrganizze.Api.Dominio.DTOs.Conta;
using ProjOrganizze.Api.Dominio.Entidades;
using ProjOrganizze.Api.Dominio.Interfaces.Repositorios;
using ProjOrganizze.Api.Dominio.Interfaces.Services;
using ProjOrganizze.Api.Exceptions;
using ProjOrganizze.Api.Extensions;
using ProjOrganizze.Api.Mapeamentos;
using ProjOrganizze.Api.Services;
using ProjOrganizze.Api.Validators.Conta;

namespace ProjOrganizze.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : MainController
    {
        private readonly IContaService _contaservice;
        private readonly IValidator<ContaAddDTO> _addValidator;
        private readonly IValidator<ContaUpdDTO> _updValidator;


        public ContaController(IContaRepository contaRepository, IContaService contaService, IValidator<ContaAddDTO> addValidator, IValidator<ContaUpdDTO> updValidator)
        {
            _contaservice = contaService;
            _addValidator = addValidator;
            _updValidator = updValidator;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarConta(ContaAddDTO objeto)
        {
            var validationResult = await _addValidator.ValidateAsync(objeto);
            if (!validationResult.IsValid) return CustomResponse(validationResult);
            Conta objetoMapeado = objeto.ToAddDTO();
            try
            {
                await _contaservice.AdicionarConta(objetoMapeado);
            }
            catch (ServiceException ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
            var objetoMapeadoView = objetoMapeado.ToGetDTO();
            return CustomResponse(objetoMapeadoView);
        }

        [HttpGet]
        public async Task<IActionResult> ObterContas()
        {
            IEnumerable<Conta> objetosDb = await _contaservice.ObterContas();
            IEnumerable<ContaViewDTO> contasView = objetosDb.Select(x => x.ToGetDTO());
            return CustomResponse(contasView);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterContaPorId([FromRoute] int id)
        {
            try
            {
                var objetoDb = await _contaservice.ObterContaPorId(id);
                var objetoMapeado = objetoDb.ToGetDTO();
                return CustomResponse(objetoMapeado);
            }
            catch (ServiceException ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarConta(ContaUpdDTO objeto)
        {
            var validationResult = await _updValidator.ValidateAsync(objeto);
            if (!validationResult.IsValid) return CustomResponse(validationResult);
            Conta objetoMapeado = objeto.ToUpdDTO();
            try
            {
                var result = await _contaservice.AtualizarConta(objetoMapeado);
                return CustomResponse(result);
            }
            catch (ServiceException ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletarConta([FromQuery] int id)
        {
            await _contaservice.DeletarConta(id);
            return CustomResponse();
        }
    }
}

