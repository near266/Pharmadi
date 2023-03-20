using AutoMapper;
using BFF.Web.DTOs;
using Jhipster.Domain.Services.Interfaces;
using Jhipster.gRPC.Contracts.Shared.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Factor.Application.Persistences;
using Module.Factor.Domain.Entities;
using Newtonsoft.Json;

namespace BFF.Web.Controllers.FactorSvc
{

    [ApiController]
    [Route("gw/[controller]")]
    public class MerchantController : ControllerBase
    {
        private readonly IMerchantRepository _service;
        private readonly ILogger<MerchantController> _logger;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        public MerchantController(IMerchantRepository service, ILogger<MerchantController> logger,IUserService userService, IMapper mapper, IAccountService accountService)
        {
            _service = service;
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
            _accountService = accountService;
        }
        [HttpPost("RegisterByUser")]
        public async Task<IActionResult> RegisterByUser([FromBody] RegisterByUserDTO request)
        {
            _logger.LogInformation($"REST request RegisterByUser : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid();
                request.CreatedDate = DateTime.Now;
                request.LangKey = "en";
                //request.Roles.Add("")
                var tem1 = _mapper.Map<RegisterRequest>(request);
                tem1.Id = request.Id.ToString();
                //adduser
                var step1 = await _accountService.RegisterAccount(tem1);

                if(step1!=null)
                {
                    var temp2 = _mapper.Map<Merchant>(request);
                    var result =await _service.Add(temp2);
                    return Ok(result);
                }
                return null;
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to RegisterByUser fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("RegisterByAdmin")]
        public async Task<IActionResult> RegisterByAdmin([FromBody] RegisterByAdminDTO request)
        {
            _logger.LogDebug($"REST request RegisterByAdmin : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid();
                request.CreatedDate = DateTime.Now;
                request.LangKey = "en";
                var tem1 = _mapper.Map<RegisterAdminRequest>(request);
                tem1.Id = request.Id.ToString();
                //adduser
                var step1 = await _accountService.RegisterAccountAdmin(tem1);

                if (step1 != null)
                {
                    var temp2 = _mapper.Map<Merchant>(request);
                    var result =await _service.Add(temp2);
                    return Ok(result);
                }
                return null;

            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to RegisterByAdmin fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}


