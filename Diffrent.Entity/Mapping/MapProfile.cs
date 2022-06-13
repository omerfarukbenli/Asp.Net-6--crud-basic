using AutoMapper;
using Diffrent.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diffrent.Entity.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>();
            CreateMap<Department, DepartmentCreateDto>().ReverseMap();
            CreateMap<DepartmentCreateDto, DepartmentDto>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<Employee, EmployeeCreateDto>().ReverseMap();
            CreateMap<EmployeeCreateDto, Employee>();
        }
    }
}
