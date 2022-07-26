using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjetoFinalApi.Repository.Interfaces;
using ProjetoFinalApi.DTOs;
using ProjetoFinalApi.Pagination;
using Newtonsoft.Json;
using FluentValidation;
using ProjetoFinalApi.Models.Data;
using ProjetoFinalApi.Extensions;
using ProjetoFinalApi.Services;
using ProjetoFinalApi.Models.Enuns;

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

    // TODO endpoint para alterar lista de times do torneio

    #region Eventos das Partidas

    [HttpPost("{id:int:min(1)}/partidas/{partidaId:int:min(1)}/eventos/inicio")]
    public async Task<ActionResult> PostEventoInicio([FromServices] AzureServiceBusPublisher publisher, int id, int partidaId)
    {
        var torneio = await _uof.TorneioRepository.GetByIdAsync(x => x.Id == id);

        if (torneio is null)
            return NotFound("Torneio não encontrado");

        var partida = await _uof.PartidaRepository.GetByIdAsync(x => x.Id == partidaId);

        if (partida is null)
            return NotFound("Partida não encotnrada.");

        var eventoPartidaInicio = new EventoPartida
        {
            PartidaId = partidaId,
            TipoEvento = Models.Enuns.TipoEvento.INICIO,
            Data = DateTime.UtcNow            
        };

        _uof.EventoPartidaRepository.Add(eventoPartidaInicio);
        await _uof.CommitAsync();

        // Publica na fila do Azure
        await publisher.Send(Models.Enuns.TipoEvento.INICIO, JsonConvert.SerializeObject(eventoPartidaInicio));

        return StatusCode(StatusCodes.Status201Created, eventoPartidaInicio);
    }

    [HttpPost("{id:int:min(1)}/partidas/{partidaId:int:min(1)}/eventos/gol")]
    public async Task<ActionResult> PostEventoGol([FromServices] AzureServiceBusPublisher publisher, 
        int id, int partidaId, [FromQuery] int timeId)
    {
        var torneio = await _uof.TorneioRepository.GetByIdAsync(x => x.Id == id);

        if (torneio is null)
            return NotFound("Torneio não encontrado");

        var partida = await _uof.PartidaRepository.GetByIdAsync(x => x.Id == partidaId);

        if (partida is null)
            return NotFound("Partida não encotnrada.");

        var time = await _uof.TimeRepository.GetByIdAsync(x => x.Id == timeId);

        if (time is null)
            return NotFound("Time não encotnrado.");

        var eventoPartidaGol = new EventoPartida
        {
            PartidaId = partidaId,
            TipoEvento = Models.Enuns.TipoEvento.GOL,
            Data = DateTime.UtcNow,
            Descricao = $"GOL do {time.Nome}"
        };

        _uof.EventoPartidaRepository.Add(eventoPartidaGol);
        await _uof.CommitAsync();

        // Publica na fila do Azure
        await publisher.Send(Models.Enuns.TipoEvento.GOL, JsonConvert.SerializeObject(eventoPartidaGol));

        return StatusCode(StatusCodes.Status201Created, eventoPartidaGol);
    }

    [HttpPost("{id:int:min(1)}/partidas/{partidaId:int:min(1)}/eventos/intervalo")]
    public async Task<ActionResult> PostEventoIntervalo([FromServices] AzureServiceBusPublisher publisher, int id, int partidaId)
    {
        var torneio = await _uof.TorneioRepository.GetByIdAsync(x => x.Id == id);

        if (torneio is null)
            return NotFound("Torneio não encontrado");

        var partida = await _uof.PartidaRepository.GetByIdAsync(x => x.Id == partidaId);

        if (partida is null)
            return NotFound("Partida não encotnrada.");

        var eventoPartidaIntervalo = new EventoPartida
        {
            PartidaId = partidaId,
            TipoEvento = Models.Enuns.TipoEvento.INTERVALO,
            Data = DateTime.UtcNow
        };

        _uof.EventoPartidaRepository.Add(eventoPartidaIntervalo);
        await _uof.CommitAsync();

        // Publica na fila do Azure
        await publisher.Send(Models.Enuns.TipoEvento.INICIO, JsonConvert.SerializeObject(eventoPartidaIntervalo));

        return StatusCode(StatusCodes.Status201Created, eventoPartidaIntervalo);
    }

    [HttpPost("{id:int:min(1)}/partidas/{partidaId:int:min(1)}/eventos/acrescimo")]
    public async Task<ActionResult> PostEventoAcrescimo([FromServices] AzureServiceBusPublisher publisher, 
        int id, int partidaId, [FromQuery] int acrescimos)
    {
        var torneio = await _uof.TorneioRepository.GetByIdAsync(x => x.Id == id);

        if (torneio is null)
            return NotFound("Torneio não encontrado");

        var partida = await _uof.PartidaRepository.GetByIdAsync(x => x.Id == partidaId);

        if (partida is null)
            return NotFound("Partida não encotnrada.");

        var eventoPartidaAcrescimo = new EventoPartida
        {
            PartidaId = partidaId,
            TipoEvento = Models.Enuns.TipoEvento.ACRESCIMO,
            Data = DateTime.UtcNow,
            Descricao = $"ACRESCIMOS de {acrescimos} minutos"
        };

        _uof.EventoPartidaRepository.Add(eventoPartidaAcrescimo);
        await _uof.CommitAsync();

        // Publica na fila do Azure
        await publisher.Send(Models.Enuns.TipoEvento.INICIO, JsonConvert.SerializeObject(eventoPartidaAcrescimo));

        return StatusCode(StatusCodes.Status201Created, eventoPartidaAcrescimo);
    }

    [HttpPost("{id:int:min(1)}/partidas/{partidaId:int:min(1)}/eventos/substituicao")]
    public async Task<ActionResult> PostEventoSubstituicao([FromServices] AzureServiceBusPublisher publisher,
        int id, int partidaId, [FromQuery] int timeId, [FromQuery] int jogadorSaidaId, [FromQuery] int jogadorEntradaId)
    {
        var torneio = await _uof.TorneioRepository.GetByIdAsync(x => x.Id == id);

        if (torneio is null)
            return NotFound("Torneio não encontrado");

        var partida = await _uof.PartidaRepository.GetByIdAsync(x => x.Id == partidaId);

        if (partida is null)
            return NotFound("Partida não encotnrada.");

        var time = await _uof.TimeRepository.GetByIdAsync(x => x.Id == timeId);

        if (time is null)
            return NotFound("Time não encotnrado.");

        var jogadorSaida = await _uof.JogadorRepository.GetByIdAsync(x => x.Id == jogadorSaidaId);

        if (jogadorSaida is null)
            return NotFound("Jogador que saiu não encotnrado.");

        var jogadorEntrada = await _uof.JogadorRepository.GetByIdAsync(x => x.Id == jogadorEntradaId);

        if (jogadorEntrada is null)
            return NotFound("Jogador que entrou não encotnrado.");

        var eventoPartidaSubstituicao = new EventoPartida
        {
            PartidaId = partidaId,
            TipoEvento = Models.Enuns.TipoEvento.SUBSTITUICAO,
            Data = DateTime.UtcNow,
            Descricao = $"SUBSTITUICAO no Time: {time.Nome}. Sai {jogadorSaida.Nome} para a entrada de {jogadorEntrada.Nome}"
        };

        _uof.EventoPartidaRepository.Add(eventoPartidaSubstituicao);
        await _uof.CommitAsync();

        // Publica na fila do Azure
        await publisher.Send(Models.Enuns.TipoEvento.GOL, JsonConvert.SerializeObject(eventoPartidaSubstituicao));

        return StatusCode(StatusCodes.Status201Created, eventoPartidaSubstituicao);
    }

    [HttpPost("{id:int:min(1)}/partidas/{partidaId:int:min(1)}/eventos/advertencia")]
    public async Task<ActionResult> PostEventoAdvertencia([FromServices] AzureServiceBusPublisher publisher,
        int id, int partidaId, [FromQuery] int timeId, [FromQuery] int jogadorId, [FromQuery] TipoAdvertencia tipoAdvertencia)
    {
        var torneio = await _uof.TorneioRepository.GetByIdAsync(x => x.Id == id);

        if (torneio is null)
            return NotFound("Torneio não encontrado");

        var partida = await _uof.PartidaRepository.GetByIdAsync(x => x.Id == partidaId);

        if (partida is null)
            return NotFound("Partida não encotnrada.");

        var time = await _uof.TimeRepository.GetByIdAsync(x => x.Id == timeId);

        if (time is null)
            return NotFound("Time não encotnrado.");

        var jogador = await _uof.JogadorRepository.GetByIdAsync(x => x.Id == jogadorId);

        if (jogador is null)
            return NotFound("Jogador não encotnrado.");

        var advertencia = tipoAdvertencia == TipoAdvertencia.AMARELO ? "Cartão Amarelo" : "Cartão Vermelho";

        var eventoPartidaAdvertencia = new EventoPartida
        {
            PartidaId = partidaId,
            TipoEvento = Models.Enuns.TipoEvento.ADVERTENCIA,
            Data = DateTime.UtcNow,
            Descricao = $"ADVERTENCIA no Time: {time.Nome}. {advertencia} para o jogado {jogador.Nome}"
        };

        _uof.EventoPartidaRepository.Add(eventoPartidaAdvertencia);
        await _uof.CommitAsync();

        // Publica na fila do Azure
        await publisher.Send(Models.Enuns.TipoEvento.GOL, JsonConvert.SerializeObject(eventoPartidaAdvertencia));

        return StatusCode(StatusCodes.Status201Created, eventoPartidaAdvertencia);
    }

    [HttpPost("{id:int:min(1)}/partidas/{partidaId:int:min(1)}/eventos/fim")]
    public async Task<ActionResult> PostEventoFim([FromServices] AzureServiceBusPublisher publisher, int id, int partidaId)
    {
        var torneio = await _uof.TorneioRepository.GetByIdAsync(x => x.Id == id);

        if (torneio is null)
            return NotFound("Torneio não encontrado");

        var partida = await _uof.PartidaRepository.GetByIdAsync(x => x.Id == partidaId);

        if (partida is null)
            return NotFound("Partida não encotnrada.");

        var eventoPartidaFim = new EventoPartida
        {
            PartidaId = partidaId,
            TipoEvento = Models.Enuns.TipoEvento.FIM,
            Data = DateTime.UtcNow
        };

        _uof.EventoPartidaRepository.Add(eventoPartidaFim);
        await _uof.CommitAsync();

        // Publica na fila do Azure
        await publisher.Send(Models.Enuns.TipoEvento.INICIO, JsonConvert.SerializeObject(eventoPartidaFim));

        return StatusCode(StatusCodes.Status201Created, eventoPartidaFim);
    }

    #endregion
}
