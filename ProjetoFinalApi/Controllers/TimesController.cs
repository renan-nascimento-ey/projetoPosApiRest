using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFinalApi.Context;
using ProjetoFinalApi.Dtos;
using ProjetoFinalApi.Models.Data;

namespace ProjetoFinalApi.Controllers
{
    [ApiController]
    [Route("api/v1/times")]
    [Produces("application/json")]
    public class TimesController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public TimesController(ApiDbContext context)
        {
            _context = context;
        }

        //[HttpGet]   
        //public async Task<ActionResult<IEnumerable<Time>>> Get()
        //{
        //    try
        //    {
        //        var times = await _context.Times.AsNoTracking().ToListAsync();

        //        if (times is null)
        //            return NotFound("Times não encontrados.");

        //        return Ok(times);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Ocorreu um problema ao tratar a sua solicitação.");
        //    }

        //}

        //[HttpGet("{id:int:min(1)}", Name = "ObterTime")]
        //public async Task<ActionResult<Time>> Get(int id)
        //{
        //    try
        //    {
        //        var time = await _context.Times.AsNoTracking().FirstOrDefaultAsync(time => time.Id == id);

        //        if (time is null)
        //            return NotFound("Time não encontrado.");

        //        return Ok(time);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Ocorreu um problema ao tratar a sua solicitação.");
        //    }          
        //}

        //[HttpPost]
        //public async Task<ActionResult> Post(TimeDto timeDto)
        //{
        //    try
        //    {
        //        if (timeDto is null)
        //            return BadRequest();

        //        var time = new Time
        //        {
        //            Nome = timeDto.Nome,
        //            Apelido = timeDto.Apelido,
        //            Cores = timeDto.Cores,
        //            Localidade = timeDto.Localidade,
        //            Estadio = timeDto.Estadio,
        //            Mascote = timeDto.Mascote
        //        };

        //        try
        //        {
        //            if (timeDto.Fundacao is not null)
        //                time.Fundacao = DateOnly.Parse(timeDto.Fundacao);
        //        }
        //        catch (Exception)
        //        {
        //            return BadRequest($"Data de fundação inválida: {timeDto.Fundacao}");
        //        }

        //        _context.Times.Add(time);
        //        await _context.SaveChangesAsync();

        //        return new CreatedAtRouteResult("ObterTime",
        //            new { id = time.Id }, time);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Ocorreu um problema ao tratar a sua solicitação.");
        //    }            
        //}

        //[HttpPut("{id:int:min(1)}")]
        //public async Task<ActionResult> Put(int id, TimeDto timeDto)
        //{
        //    try
        //    {
        //        if (id != timeDto.Id)
        //            return BadRequest();

        //        var time = await _context.Times.FirstOrDefaultAsync(time => time.Id == id);

        //        if (time is null)
        //            return NotFound("Time não encontrado.");

        //        time.Nome = timeDto.Nome;
        //        time.Apelido = timeDto.Apelido;
        //        time.Cores = timeDto.Cores;
        //        time.Localidade = timeDto.Localidade;
        //        time.Estadio = timeDto.Estadio;
        //        time.Mascote = timeDto.Mascote;

        //        try
        //        {
        //            if (timeDto.Fundacao is not null)
        //                time.Fundacao = DateOnly.Parse(timeDto.Fundacao);
        //        }
        //        catch (Exception)
        //        {
        //            return BadRequest($"Data de fundação inválida: {timeDto.Fundacao}");
        //        }

        //        _context.Entry(time).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();

        //        return Ok(time);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Ocorreu um problema ao tratar a sua solicitação.");
        //    }
            
        //}

        //[HttpDelete("{id:int:min(1)}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        var time = await _context.Times.FirstOrDefaultAsync(time => time.Id == id);

        //        if (time is null)
        //            return NotFound("Time não encontrado.");

        //        _context.Times.Remove(time);
        //        await _context.SaveChangesAsync();

        //        return Ok(time);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //                            "Ocorreu um problema ao tratar a sua solicitação.");
        //    }  
        //}
    }
}
