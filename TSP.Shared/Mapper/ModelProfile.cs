using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSP.Shared;

namespace TSP.Shared.Mapper
{
    public class ModelProfile:Profile
    {
        public ModelProfile()
        {
            CreateMap<SubSystem, SubSystemModel>().ReverseMap();
            CreateMap<SubMenuItem, SubMenuItemModel>().ReverseMap();
            CreateMap<SubItemDetail, SubItemDetailModel>().ReverseMap();
            CreateMap<ContactUs, ContactUsModel>().ReverseMap();
        }
    }
}
