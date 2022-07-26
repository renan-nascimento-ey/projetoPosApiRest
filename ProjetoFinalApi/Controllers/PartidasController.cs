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
[Route("api/v1/partidas")]
[Produces("application/json")]
public class PartidasController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public PartidasController(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    // CRUD
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PartidaDTO>>> Get([FromQuery] PagedListDefaultParameters pagedListDefaultParameters, [FromQuery] int torneioId)
    {
        if (torneioId == 0)
            return BadRequest("Informe o Torneio.");

        Expression<Func<Partida, bool>> predicate = (x => x.TorneioId == torneioId);

        var partidaParameters = new PartidaParameters()
        {
            PageNumber = pagedListDefaultParameters.PageNumber,
            PageSize = pagedListDefaultParameters.PageSize,
            Predicate = predicate
        };

        var partidas = await _uof.PartidaRepository.GetPartidasAsync(partidaParameters);

        var metadata = new
        {
            partidas.TotalCount,
            partidas.PageSize,
            partidas.CurrentPage,
            partidas.TotalPages,
            partidas.HasNext,
            partidas.HasPrevious
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

        return _mapper.Map<List<PartidaDTO>>(partidas);
    }

    [HttpGet("{id:int:min(1)}", Name = "ObterPartida")]
    public async Task<ActionResult<PartidaDTO>> Get(int id)
    {
        var partida = await _uof.PartidaRepository.GetByIdAsync(partida => partida.Id == id);

        if (partida is null)
            return NotFound();

        return Ok(_mapper.Map<PartidaDTO>(partida));
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromServices] IValidator<Partida> validator, [FromBody] NovaPartidaDTO partidaDTO)
    {
        var partida = _mapper.Map<Partida>(partidaDTO);
        partida.DataHoraFim = partida.DataHoraInicio.AddMinutes(90);

        var modelValidationResult = await validator.ValidateAsync(partida);

        if (!modelValidationResult.IsValid)
        {
            modelValidationResult.AddToModelState(ModelState);

            return BadRequest(ModelState);
        }

        _uof.PartidaRepository.Add(partida);
        await _uof.CommitAsync();

        return new CreatedAtRouteResult("Obterpartida",
            new { id = partida.Id }, _mapper.Map<PartidaDTO>(partida));
    }

    [HttpPut("{id:int:min(1)}")]
    public async Task<ActionResult> Put(int id, [FromServices] IValidator<Partida> validator, [FromBody] PartidaDTO partidaDTO)
    {
        if (id != partidaDTO.Id)
            return BadRequest();

        var partida = _mapper.Map<Partida>(partidaDTO);

        var modelValidationResult = await validator.ValidateAsync(partida);

        if (!modelValidationResult.IsValid)
        {
            modelValidationResult.AddToModelState(ModelState);

            return BadRequest(ModelState);
        }

        _uof.PartidaRepository.Update(partida);
        await _uof.CommitAsync();

        return Ok(partidaDTO);
    }

    [HttpDelete("{id:int:min(1)}")]
    public async Task<ActionResult<PartidaDTO>> Delete(int id)
    {
        var partida = await _uof.PartidaRepository.GetByIdAsync(partida => partida.Id == id);

        if (partida is null)
            return NotFound();

        _uof.PartidaRepository.Delete(partida);
        await _uof.CommitAsync();

        return Ok(_mapper.Map<PartidaDTO>(partida));
    }
}
