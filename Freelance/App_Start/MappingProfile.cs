using AutoMapper;
using Freelance.Core.Models;
using Freelance.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Freelance.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostViewModel>()
                .ForMember(des => des.ClientName, src => src.MapFrom(p => p.Client.FirstName + " " + p.Client.LastName))
                .ForMember(des => des.ClientPhotoPath, src => src.MapFrom(p => p.Client.PhotoPath))
                .ForMember(des => des.ProposalsCount, src => src.MapFrom(p => p.Proposals.Count()));

            CreateMap<Post, PostRequestViewModel>()
                .ForMember(des => des.ClientPhotoPath, src => src.MapFrom(p => p.Client.PhotoPath));

            CreateMap<PostFormViewModel, Post>();

            CreateMap<Post, PostFormViewModel> ();
        }
    }
}