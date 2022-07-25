using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetoFinalApi.DTOs;
using ProjetoFinalApi.Extensions;
using ProjetoFinalApi.Models.Data;
using ProjetoFinalApi.Pagination;
using ProjetoFinalApi.Repository.Interfaces;

namespace ProjetoFinalApi.Controllers;

[ApiController]
[Route("api/v1/jogadores")]
[Produces("application/json")]
public class JogadoresController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public JogadoresController(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    // CRUD
    [HttpGet]
    public async Task<ActionResult<IEnumerable<JogadorDTO>>> Get([FromQuery] PagedListDefaultParameters pagedListDefaultParameters)
    {
        var jogadors = await _uof.JogadorRepository.GetJogadoresAsync(pagedListDefaultParameters);

        var metadata = new
        {
            jogadors.TotalCount,
            jogadors.PageSize,
            jogadors.CurrentPage,
            jogadors.TotalPages,
            jogadors.HasNext,
            jogadors.HasPrevious
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

        return _mapper.Map<List<JogadorDTO>>(jogadors);
    }

    [HttpGet("{id:int:min(1)}", Name = "ObterJogador")]
    public async Task<ActionResult<JogadorDTO>> Get(int id)
    {
        var jogador = await _uof.JogadorRepository.GetByIdAsync(jogador => jogador.Id == id);

        if (jogador is null)
            return NotFound();

        return Ok(_mapper.Map<JogadorDTO>(jogador));
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromServices] IValidator<Jogador> validator, [FromBody] JogadorDTO JogadorDTO)
    {
        var jogador = _mapper.Map<Jogador>(JogadorDTO);

        var modelValidationResult = await validator.ValidateAsync(jogador);

        if (!modelValidationResult.IsValid)
        {
            modelValidationResult.AddToModelState(ModelState);

            return BadRequest(ModelState);
        }

        _uof.JogadorRepository.Add(jogador);
        await _uof.CommitAsync();

        return new CreatedAtRouteResult("ObterJogador",
            new { id = jogador.Id }, _mapper.Map<JogadorDTO>(jogador));
    }

    [HttpPut("{id:int:min(1)}")]
    public async Task<ActionResult> Put(int id, [FromServices] IValidator<Jogador> validator, [FromBody] JogadorDTO JogadorDTO)
    {
        if (id != JogadorDTO.Id)
            return BadRequest();

        var jogador = _mapper.Map<Jogador>(JogadorDTO);

        var modelValidationResult = await validator.ValidateAsync(jogador);

        if (!modelValidationResult.IsValid)
        {
            modelValidationResult.AddToModelState(ModelState);

            return BadRequest(ModelState);
        }

        _uof.JogadorRepository.Update(jogador);
        await _uof.CommitAsync();

        return Ok(JogadorDTO);
    }

    [HttpDelete("{id:int:min(1)}")]
    public async Task<ActionResult<JogadorDTO>> Delete(int id)
    {
        var jogador = await _uof.JogadorRepository.GetByIdAsync(jogador => jogador.Id == id);

        if (jogador is null)
            return NotFound();

        _uof.JogadorRepository.Delete(jogador);
        await _uof.CommitAsync();

        return Ok(_mapper.Map<JogadorDTO>(jogador));
    }
}
