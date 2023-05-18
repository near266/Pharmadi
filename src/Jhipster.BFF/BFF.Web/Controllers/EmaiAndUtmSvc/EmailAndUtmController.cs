using AutoMapper;
using BFF.Web.ProductSvc;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Email.Application.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFF.Web.Controllers.EmaiAndUtmSvc
{

    [ApiController]
    [Route("gw/[controller]")]
    public class EmailAndUtmController :ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EmailAndUtmController> _logger;
        private readonly IMapper _mapper;
        public EmailAndUtmController(IMediator mediator, IMapper mapper,ILogger<EmailAndUtmController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }
        private string GetUserIdFromContext()
        {
            return User.FindFirst("UserId")?.Value;
        }
        [HttpPost("AddUtm")]
        public async Task<IActionResult> AddUtm(AddUtmCommand request)
        {
            _logger.LogInformation($"REST request delete Product : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid();
                request.CreatedDate = DateTime.Now;
                request.CreatedBy = GetUserIdFromContext();
               var result= await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete Product fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
         
        }
    }
}
