using AutoMapper;
using Nexxera.Domain;
using Nexxera.WEBAPI.Dto;

namespace Nexxera.WEBAPI.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //creating map model to DTO
            CreateMap<Account,AccountDto>().ReverseMap();
            CreateMap<CreditCard,CreditCardDto>().ReverseMap();
            CreateMap<CreditCardHistory,CreditCardHistoryDto>().ReverseMap();
            CreateMap<Customer,CustomerDto>().ReverseMap();
            CreateMap<DebtHistory,DebtHistoryDto>().ReverseMap();
            CreateMap<CreditCardHistory,CreditCardHistoryDto>().ReverseMap();
            CreateMap<Period,PeriodDto>().ReverseMap();
        }
        

        
    }
}