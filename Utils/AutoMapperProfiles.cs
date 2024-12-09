using AutoMapper;
using InventarioVentas.Dto;
using InventarioVentas.Models;

namespace InventarioVentas.Utils
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ClienteDto, Cliente>();
        }
    }
}
