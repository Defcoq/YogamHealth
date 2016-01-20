using AutoMapper;
using Yogam.AMC.Data.Models;
using Yogam.AMC.Infrastructure.Tasks;
using Yogam.AMC.Web.Models;

namespace Yogam.AMC.Web
{
    public class AutoMapperConfig : IRunAtInit
    {
        public void Execute()
        {
            Mapper.CreateMap<Category, CategoryViewModel>();
            Mapper.CreateMap<Customer, CustomerViewModel>();
            Mapper.CreateMap<Employee, EmployeeViewModel>();
            Mapper.CreateMap<ApplicationUser, UserRegistrationData>();

            Mapper.CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.Status,
                      opt => opt.MapFrom
                      (src => src.Discontinued ? "danger" : src.UnitPrice > 50 ? "info" : src.UnitsInStock < 20 ? "warning" : ""));

        }
    }
}