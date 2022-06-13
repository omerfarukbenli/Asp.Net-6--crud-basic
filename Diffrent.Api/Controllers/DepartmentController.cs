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
    public class DepartmentController : ControllerBase
    {
        private readonly IGenericRepository<Department> _genericRepository;
        private readonly IMapper _mapper;

        public DepartmentController(IGenericRepository<Department> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("~/api/department/GetDepartment")]
        public async Task<IActionResult> GetDepartment()
        {
            try
            {
                var data = await _genericRepository.GetAllAsync();
                var result = _mapper.Map<IEnumerable<DepartmentDto>>(data);
                return Ok(new ApiResponsive<IEnumerable<DepartmentDto>>
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
        [Route("~/api/department/GetDepartmentById/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var data = await _genericRepository.GetByIdAsync(x => x.Id == id);
                var result = _mapper.Map<DepartmentDto>(data);
                return Ok(new ApiResponsive<DepartmentDto>
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
        [Route("~/api/department/PostDepartment")]
        public async Task<IActionResult> PostDepartment(DepartmentCreateDto obj)
        {
            try
            {
                var data = _mapper.Map<Department>(obj);
                var result = await _genericRepository.Create(data);
                return Ok(new ApiResponsive<Department>
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
        [Route("~/api/department/PutDepartment")]
        public async Task<IActionResult> PutDepartment(DepartmentDto obj)
        {
            try
            {
                var data = _mapper.Map<Department>(obj);
                var result = await _genericRepository.Update(data);
                return Ok(new ApiResponsive<Department>
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
        [Route("~/api/department/DeleteDepartment/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                await _genericRepository.Delete(id);
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
  