using AutoMapper;
using Banco.Domain.Contract.ViewModel;

namespace Banco.Domain.Contract.Mapper
{
    public class ViewModelToDomainMappingProfile
        : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ClienteVM, ClienteModel>().ReverseMap();
            CreateMap<ContaHistoricoVM, ContaHistoricoModel>().ReverseMap();
            CreateMap<ContaVM, ContaModel>().ReverseMap();
            CreateMap<LoginVM, LoginModel>().ReverseMap();
        }
    }
}