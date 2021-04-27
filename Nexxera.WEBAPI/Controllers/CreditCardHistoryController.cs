using System;
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
    public class CreditCardHistoryController  : ControllerBase
    {
        private readonly INexxeraRepository _repo;

        private readonly IMapper _mapper;

        public CreditCardHistoryController(INexxeraRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreditCardHistoryDto creditCardHistoryDto)
        {
            try{
                
                CreditCardHistory creditCardHistoryModel = _mapper.Map<CreditCardHistory>(creditCardHistoryDto);
                if(creditCardHistoryModel != null)
                {
                    CreditCard creditCard = await _repo.GetCreditCard(creditCardHistoryModel.CreditCardId,null);
                    if(creditCard != null)
                    {
                        creditCardHistoryModel.BalanceCreditCardHistory = creditCard.Balance;
                        if(creditCard.Balance < creditCardHistoryModel.Value)
                        {
                            return BadRequest("Não autorizado. Limite Disponível  R$" + creditCard.Balance);
                        }                        
                        creditCard.Balance -= creditCardHistoryModel.Value; 
                        _repo.Update(creditCard);
                        await _repo.SaveChangesAsync();                
                    }
                    else{
                        return NotFound("Cartão do cliente não encontrado");
                    }                    
                }
                
                creditCardHistoryModel.CreateDate = DateTime.Now;
                _repo.Add(creditCardHistoryModel);

                if(await _repo.SaveChangesAsync()){
                    // mapper reverse                    
                        return Created($"/api/CreditCardHistory/{creditCardHistoryDto.Id}",creditCardHistoryModel);
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