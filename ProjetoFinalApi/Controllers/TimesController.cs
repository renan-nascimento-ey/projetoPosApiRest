using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProjetoFinalApi.Dtos;
using ProjetoFinalApi.Extensions;
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
        private readonly IMapper _mapper;

        public TimesController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TimeDTO>> Get()
        {
            var times = _uof.TimeRepository.Get().ToList();
            
            return _mapper.Map<List<TimeDTO>>(times);
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterTime")]
        public ActionResult<TimeDTO> Get(int id)
        {
            var time = _uof.TimeRepository.GetById(time => time.Id == id);

            if (time is null)
                return NotFound();

            return Ok(_mapper.Map<TimeDTO>(time));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromServices] IValidator<Time> validator, [FromBody] TimeDTO timeDto)
        {
            var time = _mapper.Map<Time>(timeDto);

            var validacaoResult = await validator.ValidateAsync(time);

            if (!validacaoResult.IsValid)
            {
                validacaoResult.AddToModelState(ModelState);

                return BadRequest(ModelState);
            }

            _uof.TimeRepository.Add(time);
            await _uof.CommitAsync();

            return new CreatedAtRouteResult("ObterTime",
                new { id = time.Id }, _mapper.Map<TimeDTO>(time));
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> Put(int id, [FromBody]TimeDTO timeDto)
        {
            if (id != timeDto.Id)
                return BadRequest();

            var time = _mapper.Map<Time>(timeDto);

            _uof.TimeRepository.Update(time);
            await _uof.CommitAsync();

            return Ok(timeDto);
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult<TimeDTO>> Delete(int id)
        {
            var time = _uof.TimeRepository.GetById(time => time.Id == id);

            if (time is null)
                return NotFound();

            _uof.TimeRepository.Delete(time);
            await _uof.CommitAsync();

            return Ok(_mapper.Map<TimeDTO>(time));
        }
    }
}
