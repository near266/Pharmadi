using AutoMapper;
using BFF.Web.Constants;
using BFF.Web.DTOs;
using Jhipster.Domain.Services.Interfaces;
using Jhipster.gRPC.Contracts.Shared.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Factor.Application.Commands.MerchantCm;
using Module.Factor.Application.Persistences;
using Module.Factor.Application.Queries.MerchantQ;
using Module.Factor.Domain.Entities;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;

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
        private readonly IMediator _mediator;
        public MerchantController(IMerchantRepository service, IMediator mediator,ILogger<MerchantController> logger,IUserService userService, IMapper mapper, IAccountService accountService)
        {
            _service = service;
            _mediator = mediator;
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
               var AddRole = new HashSet<string>();
                AddRole.Add("ROLE_MERCHANT");
                request.Id = Guid.NewGuid();
                request.CreatedDate = DateTime.Now;
                request.LangKey = "en";
                request.Roles =AddRole;
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
        [Authorize(Roles = RolesConstants.ADMIN)]
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
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpDelete("DeleteMerchant")]
        public async Task<IActionResult> DeleteMerchant([FromBody] MerchantDeleteCommand request)
        {
            _logger.LogDebug($"REST request DeleteMerchant : {JsonConvert.SerializeObject(request)}");
            try
            {
                return Ok(await _mediator.Send(request));

            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to RegisterByAdmin fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }

        }
        [Authorize(Roles = RolesConstants.MERCHANT)]

        [HttpPut("MerchantUpdate")]
        public async Task<IActionResult> MerchantUpdate([FromBody] MerchantUpdateCommand request)
        {
            _logger.LogDebug($"REST request MerchantUpdate : {JsonConvert.SerializeObject(request)}");
            try
            {
                return Ok(await _mediator.Send(request));

            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to MerchantUpdate fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }

        }
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpGet("MerchantGetAllAdmin")]
        public async Task<IActionResult> MerchantGetAllAdmin([FromQuery] MerchantGetAllAdminQuery request)
        {
            _logger.LogDebug($"REST request MerchantGetAllAdmin : {JsonConvert.SerializeObject(request)}");
            try
            {
                return Ok(await _mediator.Send(request));

            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to MerchantGetAllAdmin fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }

        }
        [Authorize(Roles = RolesConstants.MERCHANT)]

        [HttpGet("MerchantViewDetail")]
        public async Task<IActionResult> MerchantViewDetail([FromQuery] MerchantViewDetailQuery request)
        {
            _logger.LogDebug($"REST request MerchantViewDetail : {JsonConvert.SerializeObject(request)}");
            try
            {
                return Ok(await _mediator.Send(request));

            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to MerchantViewDetail fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }

        }

    }
}


