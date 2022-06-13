using AutoMapper;
using Diffrent.Data.Abstract;
using Diffrent.Data.ApiResponsive;
using Diffrent.Entity;
using Diffrent.Entity.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Diffrent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IGenericRepository<Department> _genericdepartmentRepository;
        private readonly IGenericRepository<Employee> _genericemployeeRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IGenericRepository<Department> genericdepartmentRepository, IGenericRepository<Employee> genericemployeeRepository, IMapper mapper)
        {
            _genericdepartmentRepository = genericdepartmentRepository;
            _genericemployeeRepository = genericemployeeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("~/api/employee/GetEmployee")]
        public async Task<IActionResult> GetEmployee()
        {
            try
            {
                var data = await _genericemployeeRepository.GetAllAsync();
                var result = _mapper.Map<IEnumerable<EmployeeDto>>(data);
                return Ok(new ApiResponsive<IEnumerable<EmployeeDto>>
                {
                    Code = "200",
                    Status = "Ok",
                    Message = "Data Found",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponsive<string>
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Data Not Found",
                    Data = ex.Message
                });
            }
        }
        [HttpGet]
        [Route("~/api/employee/GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var data = await _genericemployeeRepository.GetByIdAsync(x => x.Id == id);
                var result = _mapper.Map<EmployeeDto>(data);
                return Ok(new ApiResponsive<EmployeeDto>
                {
                    Code = "200",
                    Status = "Ok",
                    Message = "Data Found",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponsive<string>
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Data Not Found",
                    Data = ex.Message
                });
            }

        }
        [HttpPost]
        [Route("~/api/employee/PostEmployee")]
        public async Task<IActionResult> PostEmployee([FromForm] EmployeeCreateDto obj)
        {
            try
            {

                var data = _mapper.Map<Employee>(obj);
                var result = await _genericemployeeRepository.Create(data);
                return Ok(new ApiResponsive<Employee>
                {
                    Code = "201",
                    Status = "Created",
                    Message = "Data saved",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponsive<string>
                {
                    Code = "400",
                    Status = "bad request",
                    Message = "Data Not Created",
                    Data = ex.Message
                });
            }
        }
        [HttpPut]
        [Route("~/api/employee/PutEmployee")]
        public async Task<IActionResult> PutEmployee([FromForm] EmployeeDto obj)
        {
            try
            {
                var data = _mapper.Map<Employee>(obj);
                var result = await _genericemployeeRepository.Update(data);
                return Ok(new ApiResponsive<Employee>
                {
                    Code = "202",
                    Status = "Accepted",
                    Message = "Data Updated",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponsive<string>
                {
                    Code = "400",
                    Status = "bad request",
                    Message = "Data Not Updated",
                    Data = ex.Message
                });
            }
        }
        [HttpDelete]
        [Route("~/api/employee/DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                
               await _genericemployeeRepository.Delete(id);
                return Ok(new ApiResponsive<string>
                {
                    Code = "202",
                    Status = "Accepted",
                    Message = "Data Deleted",
                    Data = "Data Deleted"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponsive<string>
                {
                    Code = "400",
                    Status = "bad request",
                    Message = "Data Not Deleted",
                    Data = ex.Message
                });
            }
        }

    }
}