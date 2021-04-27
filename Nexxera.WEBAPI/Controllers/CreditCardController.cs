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
    public class CreditCardController : ControllerBase
    {
        private readonly INexxeraRepository _repo;

        private readonly IMapper _mapper;

        public CreditCardController(INexxeraRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> Get(int customerId)
        {
            try{
                Account accountModel = await _repo.GetAccount(customerId);
                // match date to dto
                AccountDto result = _mapper.Map<AccountDto>(accountModel);
                return Ok(result);
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"Banco dados falhou {ex.Message}");
            }   
        }
    }
}