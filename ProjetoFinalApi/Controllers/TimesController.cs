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
[Route("api/v1/times")]
[Produces("application/json")]
public class TimesController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public TimesController(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    // CRUD

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TimeDTO>>> Get([FromQuery] PagedListDefaultParameters pagedListDefaultParameters)
    {
        var times = await _uof.TimeRepository.GetTimesAsync(pagedListDefaultParameters);

        var metadata = new
        {
            times.TotalCount,
            times.PageSize,
            times.CurrentPage,
            times.TotalPages,
            times.HasNext,
            times.HasPrevious
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
        
        return _mapper.Map<List<TimeDTO>>(times);
    }

    [HttpGet("{id:int:min(1)}", Name = "ObterTime")]
    public async Task<ActionResult<TimeDTO>> Get(int id)
    {
        var time = await _uof.TimeRepository.GetByIdAsync(time => time.Id == id);

        if (time is null)
            return NotFound();

        return Ok(_mapper.Map<TimeDTO>(time));
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromServices] IValidator<Time> validator, [FromBody] TimeDTO timeDto)
    {
        var time = _mapper.Map<Time>(timeDto);

        var modelValidationResult = await validator.ValidateAsync(time);

        if (!modelValidationResult.IsValid)
        {
            modelValidationResult.AddToModelState(ModelState);

            return BadRequest(ModelState);
        }

        _uof.TimeRepository.Add(time);
        await _uof.CommitAsync();

        return new CreatedAtRouteResult("ObterTime",
            new { id = time.Id }, _mapper.Map<TimeDTO>(time));
    }

    [HttpPut("{id:int:min(1)}")]
    public async Task<ActionResult> Put(int id, [FromServices] IValidator<Time> validator, [FromBody]TimeDTO timeDto)
    {
        if (id != timeDto.Id)
            return BadRequest();

        var time = _mapper.Map<Time>(timeDto);

        var modelValidationResult = await validator.ValidateAsync(time);

        if (!modelValidationResult.IsValid)
        {
            modelValidationResult.AddToModelState(ModelState);

            return BadRequest(ModelState);
        }

        _uof.TimeRepository.Update(time);
        await _uof.CommitAsync();

        return Ok(timeDto);
    }

    [HttpDelete("{id:int:min(1)}")]
    public async Task<ActionResult<TimeDTO>> Delete(int id)
    {
        var time = await _uof.TimeRepository.GetByIdAsync(time => time.Id == id);

        if (time is null)
            return NotFound();

        _uof.TimeRepository.Delete(time);
        await _uof.CommitAsync();

        return Ok(_mapper.Map<TimeDTO>(time));
    }

    // Extensions

    [HttpGet("{id:int:min(1)}/jogadores")]
    public async Task<ActionResult<IEnumerable<JogadorDTO>>> GetJogadoresTime(int id)
    {
        var jogadores = await _uof.TimeRepository.GetJogadoresTimeAsync(time => time.Id == id);

        if (jogadores is null)
            return NotFound();

        return Ok(_mapper.Map<List<JogadorDTO>>(jogadores));
    }
}
