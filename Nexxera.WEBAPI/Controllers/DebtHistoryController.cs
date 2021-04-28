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
    public class DebtHistoryController : ControllerBase
    {
        private readonly INexxeraRepository _repo;

        private readonly IMapper _mapper;

        public DebtHistoryController(INexxeraRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{accountId}")]
        [HttpGet("{accountId},{periodId}")]
        public async Task<IActionResult> Get(int accountId, int? periodId)
        {
            try{
                DebtHistory[] DebtHistoryModel = await _repo.GetDebtHistory(accountId, periodId);
                // match date to dto
                DebtHistoryDto[] result = _mapper.Map<DebtHistoryDto[]>(DebtHistoryModel);
                return Ok(result);
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"Banco dados falhou {ex.Message}");
            }   
        }

        [HttpPost]
        public async Task<IActionResult> Post(DebtHistoryDto debtHistoryDto)
        {
            try{
                
                DebtHistory debtHistoryModel = _mapper.Map<DebtHistory>(debtHistoryDto);
                if(debtHistoryModel != null)
                {
                    Account account = await _repo.GetAccount(null,debtHistoryModel.AccountId);
                    if(account != null)
                    {
                        debtHistoryModel.BalanceAccountHistory = account.Balance;
                         account.Balance -=  debtHistoryModel.Value;
                        _repo.Update(account);
                    }
                    else{
                        return NotFound("Conta cliente não encontrada");
                    }
                }
                debtHistoryModel.CreateDate = DateTime.Now;
                _repo.Add(debtHistoryModel);

                if(await _repo.SaveChangesAsync()){
                    // mapper reverse                    
                        return Created($"/api/debthistory/{debtHistoryDto.Id}",debtHistoryModel);
                }
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"Banco dados falhou {ex.Message}");
            }            

            return BadRequest();
        }

        [HttpPost("Deposit")]
        public async Task<IActionResult> Deposit(DebtHistoryDto debtHistoryDto)
        {
            try{
                
                DebtHistory debtHistoryModel = _mapper.Map<DebtHistory>(debtHistoryDto);
                if(debtHistoryModel != null)
                {
                    Account account = await _repo.GetAccount(null,debtHistoryModel.AccountId);
                    if(account != null)
                    {
                        debtHistoryModel.BalanceAccountHistory = account.Balance;
                        account.Balance +=  debtHistoryModel.Value;
                        _repo.Update(account);
                    }
                    else{
                        return NotFound("Conta cliente não encontrada");
                    }
                }
                debtHistoryModel.CreateDate = DateTime.Now;
                _repo.Add(debtHistoryModel);

                if(await _repo.SaveChangesAsync()){
                    // mapper reverse                    
                        return Created($"/api/debthistory/{debtHistoryDto.Id}",debtHistoryModel);
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