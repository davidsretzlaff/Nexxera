

using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexxera.Domain;
using Nexxera.Repository;
using Nexxera.WEBAPI.Dto;

namespace Nexxera.WEBAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly INexxeraRepository _repo;

        private readonly IMapper _mapper;

        public CustomerController(INexxeraRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try{
                Customer[] CustomerModel = await _repo.GetAllCustomer();
                // match date to dto
                CustomerDto[] result = _mapper.Map<CustomerDto[]>(CustomerModel);
                return Ok(result);
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"Banco dados falhou {ex.Message}");
            }            
        }
    }
}