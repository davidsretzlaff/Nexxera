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
        [HttpGet("{customerId},{periodId}")]
        public async Task<IActionResult> Get(int customerId, int? periodId)
        {
            try{
                CreditCard CreditCardModel = await _repo.GetCreditCard(customerId,periodId);
                // match date to dto
                CreditCardDto result = _mapper.Map<CreditCardDto>(CreditCardModel);
                return Ok(result);
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"Banco dados falhou {ex.Message}");
            }   
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreditCardDto creditCardDto)
        {
            try{
                
                CreditCard creditCardModel = _mapper.Map<CreditCard>(creditCardDto);                
                _repo.Add(creditCardModel);

                if(await _repo.SaveChangesAsync()){
                    // mapper reverse                    
                    return Created($"/api/customer/{creditCardDto.Id}",creditCardModel);
                }
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"Banco dados falhou {ex.Message}");
            }            

            return BadRequest();
        }
    }
}