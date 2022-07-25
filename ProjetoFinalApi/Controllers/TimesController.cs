using Microsoft.AspNetCore.Mvc;
using ProjetoFinalApi.Models.Data;
using ProjetoFinalApi.Repository.Interfaces;

namespace ProjetoFinalApi.Controllers
{
    [ApiController]
    [Route("api/v1/times")]
    [Produces("application/json")]
    public class TimesController : ControllerBase
    {
        private readonly IUnitOfWork _uof;

        public TimesController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Time>> Get()
        {
            return _uof.TimeRepository.Get().ToList();
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterTime")]
        public ActionResult<Time> Get(int id)
        {
            var time = _uof.TimeRepository.GetById(time => time.Id == id);

            if (time is null)
                return NotFound();

            return Ok(time);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Time time)
        {
            if (time is null)
                return BadRequest();

            _uof.TimeRepository.Add(time);
            await _uof.CommitAsync();

            return new CreatedAtRouteResult("ObterTime",
                new { id = time.Id }, time);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> Put(int id, [FromBody]Time time)
        {
            if (id != time.Id)
                return BadRequest();

            _uof.TimeRepository.Update(time);
            await _uof.CommitAsync();

            return Ok(time);
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> Delete(int id)
        {
            var time = _uof.TimeRepository.GetById(time => time.Id == id);

            if (time is null)
                return NotFound();

            _uof.TimeRepository.Delete(time);
            await _uof.CommitAsync();

            return Ok(time);
        }
    }
}
