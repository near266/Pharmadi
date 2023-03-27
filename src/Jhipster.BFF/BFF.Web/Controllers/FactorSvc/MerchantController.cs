﻿using AutoMapper;
using BFF.Web.Constants;
using BFF.Web.DTOs;
using Jhipster.Crosscutting.Constants;
using Jhipster.Domain.Services.Interfaces;
using Jhipster.gRPC.Contracts.Shared.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Module.Factor.Application.Commands.MerchantCm;
using Module.Factor.Application.Persistences;
using Module.Factor.Application.Queries.MerchantQ;
using Module.Factor.Domain.Entities;
using Newtonsoft.Json;
using RestSharp;
using RolesConstants = BFF.Web.Constants.RolesConstants;

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

        private readonly IConfiguration _configuration;

        public MerchantController(IMerchantRepository service, IConfiguration configuration, IMediator mediator, ILogger<MerchantController> logger, IUserService userService, IMapper mapper, IAccountService accountService)
        {
            _service = service;
            _mediator = mediator;
            _logger = logger;
            _configuration = configuration;
            _userService = userService;
            _mapper = mapper;
            _accountService = accountService;
        }
        private string GetUserIdFromContext()
        {
            return User.FindFirst("UserId")?.Value;
        }
        //[HttpPost("RegisterByUser")]
        //public async Task<IActionResult> RegisterByUser([FromBody] RegisterByUserDTO request)
        //{
        //    _logger.LogInformation($"REST request RegisterByUser : {JsonConvert.SerializeObject(request)}");
        //    try
        //    {
        //        var AddRole = new HashSet<string>();
        //        AddRole.Add("ROLE_MERCHANT");
        //        request.Id = Guid.NewGuid();
        //        request.CreatedDate = DateTime.Now;
        //        request.LangKey = "en";
        //        request.Roles = AddRole;
        //        request.Status = 0;

        //        //request.Roles.Add("")
        //        var tem1 = _mapper.Map<RegisterRequest>(request);
        //        tem1.Id = request.Id.ToString();
        //        //adduser
        //        var step1 = await _accountService.RegisterAccount(tem1);

        //        if (step1 != null)
        //        {
        //            var temp2 = _mapper.Map<Merchant>(request);
        //            var map = _mapper.Map<MerchantAddCommand>(temp2);
        //            var result = await _mediator.Send(map);
        //            return Ok(result);
        //        }
        //        return null;


        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"REST request to RegisterByUser fail: {ex.Message}");
        //        return StatusCode(500, ex.Message);
        //    }
        //}
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
                request.Roles = AddRole;


                //request.Roles.Add("")
                var tem1 = _mapper.Map<RegisterAdminRequest>(request);
                tem1.Id = request.Id.ToString();
                tem1.Login = request.PhoneNumber;
                //adduser
                var step1 = await _accountService.RegisterAccountAdmin(tem1);
                var body = new
                {
                    Username = request.PhoneNumber,
                    Password = request.Password,
                    rememberMe = true
                };
                var client = new RestClient(_configuration.GetConnectionString("AIO"));
               
                var requestAddTranaction = new RestRequest($"/api/authenticate", Method.Post);
                requestAddTranaction.AddJsonBody(body);
                var reponse = await client.ExecuteAsync(requestAddTranaction);

                return Ok(reponse.Content);

            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to RegisterByUser fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("AddMerchant")]
        public async Task<IActionResult> AddMerchant([FromBody] MerchantAddCommand rq)
        {

            _logger.LogInformation($"REST request AddMerchant : {JsonConvert.SerializeObject(rq)}");
            try
            {
                rq.Id = Guid.Parse(GetUserIdFromContext());
                rq.CreatedDate = DateTime.Now;
                rq.CreatedBy = rq.Id;
                return Ok(await _mediator.Send(rq));

            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to AddMerchant fail: {ex.Message}");
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
                request.Status = 1;
                var tem1 = _mapper.Map<RegisterAdminRequest>(request);
                tem1.Id = request.Id.ToString();
                //adduser
                var step1 = await _accountService.RegisterAccountAdmin(tem1);

                if (step1 != null)
                {
                    var temp2 = _mapper.Map<Merchant>(request);
                    var map = _mapper.Map<MerchantAddCommand>(temp2);
                    var result = await _mediator.Send(map);
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

        [HttpPost("DeleteMerchant")]
        public async Task<IActionResult> DeleteMerchant([FromBody] List<MerchantDeleteCommand> request)
        {
            _logger.LogDebug($"REST request DeleteMerchant : {JsonConvert.SerializeObject(request)}");
            try
            {
                foreach (var item in request)
                {
                    await _userService.deleteUserByMerchantId(item.Id);
                    await _mediator.Send(item);
                }
                return Ok(1);
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

        [HttpPost("MerchantGetAllAdmin")]
        public async Task<IActionResult> MerchantGetAllAdmin([FromBody] MerchantGetAllAdminQuery request)
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
        [Authorize(Roles = RolesConstants.ADMIN)]


        [HttpPost("MerchantSearchToChoose")]
        public async Task<IActionResult> MerchantSearchToChoose([FromBody] MerchantSearchToChooseQuery request)
        {
            _logger.LogDebug($"REST request MerchantSearchToChooseQuery : {JsonConvert.SerializeObject(request)}");
            try
            {
                return Ok(await _mediator.Send(request));

            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to MerchantSearchToChooseQuery fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }

        }

    }
}


