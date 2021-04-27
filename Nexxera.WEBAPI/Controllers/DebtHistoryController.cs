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
    public class DebtHistoryController : ControllerBase
    {
        private readonly INexxeraRepository _repo;

        private readonly IMapper _mapper;

        public DebtHistoryController(INexxeraRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{customerId}")]
        [HttpGet("{customerId},{periodId}")]
        public async Task<IActionResult> Get(int customerId, int? periodId)
        {
            try{
                DebtHistory DebtHistoryModel = await _repo.GetDebtHistory(customerId, periodId);
                // match date to dto
                DebtHistoryDto result = _mapper.Map<DebtHistoryDto>(DebtHistoryModel);
                return Ok(result);
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"Banco dados falhou {ex.Message}");
            }   
        }
    }
}