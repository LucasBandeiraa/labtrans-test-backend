using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using LabTrans.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LabTrans.Controllers
{
    [Route("v1/[controller]")]
    public class ReservasController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReservasController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWork.ReservaRepository.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReservaDTO reservaDTO)
        {
            var reserva = _mapper.Map<Reserva>(reservaDTO);
            await _unitOfWork.ReservaRepository.Add(reserva);
            _unitOfWork.Commit();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ReservaDTO reservaDTO)
        {
            var entity = await _unitOfWork.ReservaRepository.Get(id);

            if (entity != null)
            {
                var reserva = _mapper.Map(reservaDTO, entity);
                _unitOfWork.ReservaRepository.Update(reserva);
                _unitOfWork.Commit();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _unitOfWork.ReservaRepository.Get(id);

            if(entity != null)
            {
               _unitOfWork.ReservaRepository.Delete(entity);
               _unitOfWork.Commit();
            }

            return Ok();
        }
    }
}
