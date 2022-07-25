using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjetoFinalApi.Repository.Interfaces;
using ProjetoFinalApi.DTOs;
using ProjetoFinalApi.Pagination;
using Newtonsoft.Json;
using FluentValidation;
using ProjetoFinalApi.Models.Data;
using ProjetoFinalApi.Extensions;
using System.Linq.Expressions;

namespace ProjetoFinalApi.Controllers;

[ApiController]
[Route("api/v1/transferencias")]
[Produces("application/json")]
public class TransferenciasController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public TransferenciasController(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    // CRUD
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransferenciaDTO>>> Get([FromQuery] PagedListDefaultParameters pagedListDefaultParameters,
        int? jogadorId, int? timeId)
    {
        var transferenciaParameters = new TransferenciaParameters()
        {
            PageNumber = pagedListDefaultParameters.PageNumber,
            PageSize = pagedListDefaultParameters.PageSize
        };

        if (jogadorId.HasValue && !timeId.HasValue)
            transferenciaParameters.Predicate = (x => x.JogadorId == jogadorId.Value);
        else if (!jogadorId.HasValue && timeId.HasValue)
            transferenciaParameters.Predicate = (x => x.TimeOrigemId == timeId.Value || x.TimeDestinoId == timeId.Value);
        else if (jogadorId.HasValue && timeId.HasValue)
            transferenciaParameters.Predicate = (x => x.JogadorId == jogadorId.Value && (x.TimeOrigemId == timeId.Value || x.TimeDestinoId == timeId.Value));

        var transferencias = await _uof.TransferenciaRepository.GetTransferenciasAsync(transferenciaParameters);

        var metadata = new
        {
            transferencias.TotalCount,
            transferencias.PageSize,
            transferencias.CurrentPage,
            transferencias.TotalPages,
            transferencias.HasNext,
            transferencias.HasPrevious
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

        return _mapper.Map<List<TransferenciaDTO>>(transferencias);
    }

    [HttpGet("{id:int:min(1)}", Name = "ObterTransferencias")]
    public async Task<ActionResult<TransferenciaDTO>> Get(int id)
    {
        var transferencia = await _uof.TransferenciaRepository.GetByIdAsync(transferencia => transferencia.Id == id);

        if (transferencia is null)
            return NotFound();

        return Ok(_mapper.Map<TransferenciaDTO>(transferencia));
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromServices] IValidator<Transferencia> validator, [FromBody] TransferenciaDTO transferenciaDto)
    {
        var transferencia = _mapper.Map<Transferencia>(transferenciaDto);

        var modelValidationResult = await validator.ValidateAsync(transferencia);

        if (!modelValidationResult.IsValid)
        {
            modelValidationResult.AddToModelState(ModelState);

            return BadRequest(ModelState);
        }

        _uof.TransferenciaRepository.Add(transferencia);

        var jogador = await _uof.JogadorRepository.GetByIdAsync(x => x.Id == transferencia.JogadorId);
        jogador.TimeId = transferencia.TimeDestinoId;
        _uof.JogadorRepository.Update(jogador);

        await _uof.CommitAsync();

        return new CreatedAtRouteResult("ObterTransferencias",
            new { id = transferencia.Id }, _mapper.Map<TransferenciaDTO>(transferencia));
    }

    [HttpPut("{id:int:min(1)}")]
    public async Task<ActionResult> Put(int id, [FromServices] IValidator<Transferencia> validator, [FromBody] TransferenciaDTO transferenciaDto)
    {
        if (id != transferenciaDto.Id)
            return BadRequest();

        var transferencia = _mapper.Map<Transferencia>(transferenciaDto);

        var modelValidationResult = await validator.ValidateAsync(transferencia);

        if (!modelValidationResult.IsValid)
        {
            modelValidationResult.AddToModelState(ModelState);

            return BadRequest(ModelState);
        }

        _uof.TransferenciaRepository.Update(transferencia);

        var jogador = await _uof.JogadorRepository.GetByIdAsync(x => x.Id == transferencia.JogadorId);
        jogador.TimeId = transferencia.TimeDestinoId;
        _uof.JogadorRepository.Update(jogador);

        await _uof.CommitAsync();

        return Ok(transferenciaDto);
    }

    [HttpDelete("{id:int:min(1)}")]
    public async Task<ActionResult<TransferenciaDTO>> Delete(int id)
    {
        var transferencia = await _uof.TransferenciaRepository.GetByIdAsync(transferencia => transferencia.Id == id);

        if (transferencia is null)
            return NotFound();

        _uof.TransferenciaRepository.Delete(transferencia);

        var jogador = await _uof.JogadorRepository.GetByIdAsync(x => x.Id == transferencia.JogadorId);
        jogador.TimeId = transferencia.TimeOrigemId;
        _uof.JogadorRepository.Update(jogador);

        await _uof.CommitAsync();

        return Ok(_mapper.Map<TransferenciaDTO>(transferencia));
    }
}
