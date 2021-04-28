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
        
        [HttpGet("{accountId}")]
        [HttpGet("{accountId},{periodId}")]
        public async Task<IActionResult> Get(int accountId, int? periodId)
        {
            try{
                CreditCard CreditCardModel = await _repo.GetCreditCard(accountId,periodId,true);
                // match date to dto
                CreditCardDto result = _mapper.Map<CreditCardDto>(CreditCardModel);
                return Ok(result);
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"Banco dados falhou {ex.Message}");
            }   
        }


        [HttpGet("GetCardById/{creditCardId}")]
        public async Task<IActionResult> Get(int creditCardId)
        {
            try{
                CreditCard CreditCardModel = await _repo.GetCreditCardById(creditCardId);
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

        [HttpPost("PaymentCreditCard/{accountId}")]
        public async Task<IActionResult> PaymentCreditCard(int accountId)
        {
            try{
                                
                CreditCard creditCardModel = await _repo.GetCreditCard(accountId,null,false);                
                creditCardModel.Balance = 0;
                _repo.Update(creditCardModel);

                if(await _repo.SaveChangesAsync()){
                    // mapper reverse                    
                    return Created($"/api/CreditCard/{accountId}",creditCardModel);
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