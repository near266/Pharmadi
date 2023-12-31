﻿using AutoMapper;
using BFF.Web.DTOs;
using BFF.Web.DTOs.FactorSvc;
using Jhipster.Domain.Services.Interfaces;
using Jhipster.gRPC.Contracts.Shared.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
using Module.Redis.Library.Helpers;
using Microsoft.Extensions.Caching.Distributed;
using Module.Redis.Configurations;
using Jhipster.Domain;
using LanguageExt.Pretty;
using Module.Factor.Application.Commands.UtmMechantCm;
using BFF.Web.Share;

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
        private readonly RedisConfig _redisConfiguration;
        private readonly IDistributedCache _cache;
        private readonly IConfiguration _configuration;

        public MerchantController(IMerchantRepository service, IDistributedCache distributedCache, RedisConfig redisConfiguration, IConfiguration configuration, IMediator mediator, ILogger<MerchantController> logger, IUserService userService, IMapper mapper, IAccountService accountService)
        {
            _service = service;
            _mediator = mediator;
            _logger = logger;
            _configuration = configuration;
            _userService = userService;
            _cache = distributedCache;
            _mapper = mapper;
            _accountService = accountService;
        }
        private string GetUserIdFromContext()
        {
            return User.FindFirst("UserId")?.Value;
        }
        private List<string> GetListUserRole()
        {
            var key = User.FindFirst("auth")?.Value;
            return key.Split(',').ToList();
        }

        [HttpPost("RegisterByUser")]
        public async Task<IActionResult> RegisterByUser([FromBody] RegisterByUserDTO request)
        {
            _logger.LogInformation($"REST request RegisterByUser : {JsonConvert.SerializeObject(request)}");
            try
            {
                try
                {
                    if (request.Email == null || request.Email.Length == 0) { throw new Exception("Không để trống email,vui lòng kiểm tra lại"); }
                    if (CheckString.CheckValidEmail(request.Email) == false) { throw new Exception("Email không hợp lệ, vui lòng kiểm tra lại"); }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"REST request to RegisterByUser fail: {ex.Message}");
                    var errorObject = new
                    {
                        ErrorMessage = ex.Message,
                        Field = "Email"
                    };
                    return StatusCode(400, errorObject);
                }
                try
                {
                    if (request.PhoneNumber == null || request.PhoneNumber.Length == 0) { throw new Exception("Không để trống số điện thoại,vui lòng kiểm tra lại"); }
                    if (CheckString.CheckInvalidPhoneNumber(request.PhoneNumber) == false) { throw new Exception("Số điện thoại không hợp lệ, vui lòng kiểm tra lại"); }

                }
                catch (Exception ex)
                {
                    _logger.LogError($"REST request to RegisterByUser fail: {ex.Message}");
                    var errorObject = new
                    {
                        ErrorMessage = ex.Message,
                        Field = "Phone Number"
                    };
                    return StatusCode(400, errorObject);
                }
                try
                {
                    if (CheckString.CheckValidPassword(request.Password) == false) { throw new Exception("Mật khẩu phải có tối thiểu 6 kí tự"); }
                    if (request.Password == null || request.Password.Length == 0) { throw new Exception("Không để mật khẩu trống"); }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"REST request to RegisterByUser fail: {ex.Message}");
                    var errorObject = new
                    {
                        ErrorMessage = ex.Message,
                        Field = "Password"
                    };
                    return StatusCode(400, errorObject);
                }
                try
                {
                    if (request.Password != request.ConfirmPassword) { throw new Exception("Mật khẩu không khớp ,vui lòng kiểm tra lại"); }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"REST request to RegisterByUser fail: {ex.Message}");
                    var errorObject = new
                    {
                        ErrorMessage = ex.Message,
                        Field = "ConfirmPassword"
                    };
                    return StatusCode(400, errorObject);
                }
                //if (request.Pdf == null || request.Pdf.Count == 0) { throw new Exception("Không để trống giấy phép GPP, vui lòng điền đầy đủ thông tin"); }
                // role merchant
                var AddRole = new HashSet<string>();
                AddRole.Add("ROLE_MERCHANT");

                request.Id = Guid.NewGuid();
                request.CreatedDate = DateTime.Now;
                request.LangKey = "en";
                request.Roles = AddRole;
                // đăng ký với sđt
                var tem1 = _mapper.Map<RegisterAdminRequest>(request);
                tem1.Id = request.Id.ToString();
                tem1.Login = request.PhoneNumber;
                //adduser
                var step1 = await _accountService.RegisterAccountAdmin(tem1);
                //addbody
                var body = new
                {
                    Username = request.PhoneNumber,
                    Password = request.Password,
                    rememberMe = true,
                    Campaign = request.Campaign,
                    Content = request.Content,
                    Medium = request.Medium,
                    Source = request.Source

                };
                //gen token
                var client = new RestClient(_configuration.GetConnectionString("AIO"));

                var requestAddTranaction = new RestRequest($"/api/authenticate", Method.Post);
                requestAddTranaction.AddJsonBody(body);
                var reponse = await client.ExecuteAsync(requestAddTranaction);
                //add merchant
                if (reponse.Content != null)
                {
                    string[] parts = request.Email.Contains("@") ? request.Email.Split('@') : (request.Email + "@").Split('@');
                    string result = parts[0];
                    var rqMerchant = new MerchantAddCommand()
                    {
                        Id = Guid.Parse(step1.Id),
                        PhoneNumber = request.PhoneNumber,
                        MerchantName = result,
                        CreatedDate = DateTime.Now,
                        Email = request.Email,
                        Status = 1,
                        GPPImage = request.Pdf,
                        AddressStatus = 1

                    };
                    await _mediator.Send(rqMerchant);

                }
                //add Utm
                request.UtmId = Guid.NewGuid();
                var utm = new AddUtmMerchantCommand
                {
                    Id = (Guid)request.UtmId,
                    Utmlink = request.Utmlink,
                    Campaign = request.Campaign,
                    Content = request.Content,
                    Medium = request.Medium,
                    Source = request.Source,
                    DateLogin = request.DateLogin,
                    DateRegister = request.DateRegister,
                    CreatedDate = DateTime.Now,
                    CreatedBy = GetUserIdFromContext(),


                };
                await _mediator.Send(utm);
                //add utmuser
                var utmUser = new AddUtmUserCommand
                {
                    Id = Guid.NewGuid(),
                    UtmId = request.UtmId,
                    UserId = step1.Id,
                    CreatedDate = DateTime.Now,
                };
                await _mediator.Send(utmUser);
                return Ok(reponse.Content);

            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to RegisterByUser fail: {ex.Message}");
                var errorObject = new
                {
                    ErrorMessage = ex.Message
                };
                return StatusCode(500, errorObject);
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
                request.AddressStatus = 1;
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
        /// <summary>
        /// Tạo tài khoản employee
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("RegisterEmployee")]
        public async Task<IActionResult> RegisterEmployee([FromBody] RegisterEmployeeByUserDTO request)
        {
            _logger.LogDebug($"REST request RegisterByAdmin : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid().ToString();
                request.CreatedDate = DateTime.Now;
                request.LangKey = "en";

                //adduser
                var step1 = await _accountService.RegisterEmployee(request);

                return Ok(1);

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
        [HttpPost("MerchantUpdate")]
        public async Task<IActionResult> MerchantUpdate([FromBody] MerchantUpdateCommand request)
        {
            _logger.LogDebug($"REST request MerchantUpdate : {JsonConvert.SerializeObject(request)}");
            try
            {
                var role = GetListUserRole();
                if (role.Any(s => s.Contains(RolesConstants.ADMIN)) == true)
                {
                    request.LastModifiedDate = DateTime.Now;
                    request.AddressStatus = 1;
                    request.Status = 1;
                    return Ok(await _mediator.Send(request));
                }
                else
                {
                    request.LastModifiedDate = DateTime.Now;
                    request.AddressStatus = 1;
                    request.Status = 1;
                    return Ok(await _mediator.Send(request));
                }
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
        [Authorize]

        [HttpPost("MerchantViewDetail")]
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

        [HttpGet("CheckMerchant")]
        public async Task<IActionResult> CheckMerchant()
        {
            _logger.LogDebug($"REST request CheckMerchant");
            try
            {
                var request = new MerchantViewDetailQuery
                {
                    Id = new Guid(GetUserIdFromContext())
                };
                var res = await _mediator.Send(request);
                if (res.Status == 2 && res.AddressStatus == 2) return Ok(true);
                else return Ok(false);

            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to CheckMerchant fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }

        }
        [Authorize(Roles = RolesConstants.ADMIN)]


        [HttpPost("MerchantSearchToChoose")]
        public async Task<IActionResult> MerchantSearchToChoose([FromBody] MerchantSearchToChooseQuery request)
        {
            _logger.LogInformation($"REST request to get all point :{JsonConvert.SerializeObject(request)}");
            try
            {
                IEnumerable<Merchant>? res;

                string recordKey = $"{HttpContext.Request.Path}{HttpContext.Request.QueryString}";
                res = await _cache.GetRecordAsync<IEnumerable<Merchant>>(recordKey);

                if (res is null)
                {
                    res = await _mediator.Send(request);
                    await _cache.SetRecordAsync(recordKey, res, TimeSpan.FromMinutes(30));
                }

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to MerchantSearchToChooseQuery fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }

        }
        //[Authorize(Roles = RolesConstants.ADMIN)]

        //[HttpPost("RequestAddressStatus")]
        //public async Task<IActionResult> RequestStatus([FromBody] UpdateAddressStatusCommand request)
        //{
        //    _logger.LogDebug($"REST request RequestAddressStatus : {JsonConvert.SerializeObject(request)}");
        //    try
        //    {
        //        request.Id = Guid.Parse(GetUserIdFromContext());
        //        return Ok(await _mediator.Send(request));

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"REST request to RequestAddressStatus fail: {ex.Message}");
        //        return StatusCode(500, ex.Message);
        //    }

        //}
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("ApproveAddressStatus")]
        public async Task<IActionResult> ApproveAddressStatus([FromBody] List<ApproveAddressStatusRq> request)
        {
            _logger.LogDebug($"REST request ApproveAddressStatus : {JsonConvert.SerializeObject(request)}");
            try
            {
                int result = 0;
                foreach (var item in request)
                {
                    var tem = new UpdateAddressStatusCommand
                    {
                        Id = item.Id,

                    };
                    result = await _mediator.Send(tem);
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to ApproveAddressStatus fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }

        }

    }
}


