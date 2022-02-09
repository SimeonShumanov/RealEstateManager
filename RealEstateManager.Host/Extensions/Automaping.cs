using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using RealEstateManager.Models.DTO;
using RealEstateManager.Models.Requests;
using RealEstateManager.Models.Responses;

namespace RealEstateManager.Host.Extensions
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<House, HouseResponse>()
                .ForMember(m => m.EstateID, tm => tm.MapFrom(x => x.EstateID + x.Location))
                .ReverseMap();
            CreateMap<HouseRequest, House>().ReverseMap();
            //CreateMap<IEnumerable<Tag>, IEnumerable<TagResponse>>().ReverseMap();

            CreateMap<Apartment, ApartmentResponse>()
                .ForMember(m => m.EstateID, tm => tm.MapFrom(x => x.EstateID + x.Location))
                .ReverseMap();
            CreateMap<ApartmentRequest, Apartment>().ReverseMap();

            CreateMap<warehouse, warehouseResponse>()
                .ForMember(m => m.EstateID, tm => tm.MapFrom(x => x.EstateID + x.Location))
                .ReverseMap();
            CreateMap<warehouseRequest, warehouse>().ReverseMap();

        }
    }
}
