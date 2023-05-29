using BFF.Web.DTOs;
using Jhipster.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFF.Web.Controllers.Support
{
    [Route("gw/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IMailService _mailService;
        public CustomerController(ILogger<CustomerController> logger, IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }
        [HttpPost("SendMailSp")]
        public async Task<IActionResult> SendMailSp([FromBody] SendMailspDTO rq)
        {
            _logger.LogInformation($"REST request SendMailSp : {JsonConvert.SerializeObject(rq)}");
            try
            {
                await _mailService.SendMailSp(rq.Name, rq.Email, rq.PhoneNumber, rq.NamePharma, rq.Message);
                return Ok("true");
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to SendMailSp fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
