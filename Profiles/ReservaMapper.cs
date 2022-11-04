using AutoMapper;
using Domain.Entities;
using LabTrans.DTO;

namespace LabTrans.Profiles
{
    public class ReservaMapper : Profile
    {
        public ReservaMapper()
        {
            CreateMap<ReservaDTO, Reserva>();
            CreateMap<Reserva, ReservaDTO>();
        }
    }
}
