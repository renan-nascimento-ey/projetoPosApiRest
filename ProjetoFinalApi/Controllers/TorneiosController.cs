using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjetoFinalApi.Repository.Interfaces;
using ProjetoFinalApi.DTOs;
using ProjetoFinalApi.Pagination;
using Newtonsoft.Json;
using FluentValidation;
using ProjetoFinalApi.Models.Data;
using ProjetoFinalApi.Extensions;

namespace ProjetoFinalApi.Controllers;

[ApiController]
[Route("api/v1/torneios")]
[Produces("application/json")]
public class TorneiosController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public TorneiosController(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    // CRUD
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TorneioDTO>>> Get([FromQuery] PagedListDefaultParameters pagedListDefaultParameters)
    {
        var torneios = await _uof.TorneioRepository.GetTorneiosAsync(pagedListDefaultParameters);

        var metadata = new
        {
            torneios.TotalCount,
            torneios.PageSize,
            torneios.CurrentPage,
            torneios.TotalPages,
            torneios.HasNext,
            torneios.HasPrevious
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

        return _mapper.Map<List<TorneioDTO>>(torneios);
    }

    [HttpGet("{id:int:min(1)}", Name = "ObterTorneio")]
    public async Task<ActionResult<TorneioDTO>> Get(int id)
    {
        var torneio = await _uof.TorneioRepository.GetByIdAsync(torneio => torneio.Id == id);

        if (torneio is null)
            return NotFound();

        return Ok(_mapper.Map<TorneioDTO>(torneio));
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromServices] IValidator<Torneio> validator, [FromBody] NovoTorneioDTO torneioDTO)
    {
        var torneio = _mapper.Map<Torneio>(torneioDTO);

        var modelValidationResult = await validator.ValidateAsync(torneio);

        if (!modelValidationResult.IsValid)
        {
            modelValidationResult.AddToModelState(ModelState);

            return BadRequest(ModelState);
        }

        _uof.TorneioRepository.Add(torneio);
        await _uof.CommitAsync();

        return new CreatedAtRouteResult("ObterTorneio",
            new { id = torneio.Id }, _mapper.Map<TorneioDTO>(torneio));
    }

    [HttpPut("{id:int:min(1)}")]
    public async Task<ActionResult> Put(int id, [FromServices] IValidator<Torneio> validator, [FromBody] TorneioDTO torneioDTO)
    {
        if (id != torneioDTO.Id)
            return BadRequest();

        var torneio = _mapper.Map<Torneio>(torneioDTO);

        var modelValidationResult = await validator.ValidateAsync(torneio);

        if (!modelValidationResult.IsValid)
        {
            modelValidationResult.AddToModelState(ModelState);

            return BadRequest(ModelState);
        }

        _uof.TorneioRepository.Update(torneio);
        await _uof.CommitAsync();

        return Ok(torneioDTO);
    }

    [HttpDelete("{id:int:min(1)}")]
    public async Task<ActionResult<TorneioDTO>> Delete(int id)
    {
        var torneio = await _uof.TorneioRepository.GetByIdAsync(torneio => torneio.Id == id);

        if (torneio is null)
            return NotFound();

        _uof.TorneioRepository.Delete(torneio);
        await _uof.CommitAsync();

        return Ok(_mapper.Map<TorneioDTO>(torneio));
    }

    // TODO alterar lista de times do torneio

    // Extensions

    // TODO salvar no banco e enviar para um fila

    /*
POST /torneios/<id>/partidas/<id>/eventos/inicio 
POST /torneios/<id>/partidas/<id>/eventos/gol 
POST /torneios/<id>/partidas/<id>/eventos/intervalo 
POST /torneios/<id>/partidas/<id>/eventos/acrescimo 
POST /torneios/<id>/partidas/<id>/eventos/substituicao 
POST /torneios/<id>/partidas/<id>/eventos/advertencia 
POST /torneios/<id>/partidas/<id>/eventos/fim 
     */
}
