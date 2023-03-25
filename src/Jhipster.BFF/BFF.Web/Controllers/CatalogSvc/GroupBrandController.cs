﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Catalog.Application.Commands.GroupBrandCm;
using Module.Catalog.Application.Queries.GroupBrandQ;
using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using BFF.Web.Constants;
using BFF.Web.DTOs.CatalogSvc;
using AutoMapper;
using Module.Catalog.Application.Commands.BrandCm;
using Jhipster.Infrastructure.Migrations;

namespace BFF.Web.ProductSvc
{
    [ApiController]
    [Route("gw/[controller]")]
    public class GroupBrandController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GroupBrandController> _logger;
        private readonly IMapper _mapper;

        public GroupBrandController(IMediator mediator, ILogger<GroupBrandController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        private string GetUserIdFromContext()
        {
            return User.FindFirst("UserId")?.Value;
        }
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("Add")]
        public async Task<ActionResult<int>> Add([FromBody] GroupBrandAddCommand request)
        {
            _logger.LogInformation($"REST request add GroupBrand : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid();
                request.CreatedDate = DateTime.Now;
                var UserId = new Guid(GetUserIdFromContext());
                request.CreatedBy=UserId;
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add GroupBrand fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] GroupBrandUpdateCommand request)
        {
            _logger.LogInformation($"REST request update GroupBrand : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.LastModifiedDate = DateTime.Now;
                var UserId = new Guid(GetUserIdFromContext());
                request.LastModifiedBy=UserId;
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update GroupBrand fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] GroupBrandDeleteCommand request)
        {
            _logger.LogInformation($"REST request delete GroupBrand : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete GroupBrand fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Search")]
        public async Task<ActionResult<IEnumerable<GroupBrand>>> Search([FromBody] GroupBrandSearchQuery request)
        {
            _logger.LogInformation($"REST request search GroupBrand : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to search GroupBrand fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("GetAllAdmin")]
        public async Task<ActionResult<PagedList<GroupBrand>>> GetAllAdmin([FromBody] GroupBrandGetAllAdminQuery request)
        {
            _logger.LogInformation($"REST request get all GroupBrand by Admin : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to get all GroupBrand by Admin  fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("PinGroupBrand")]
        public async Task<IActionResult> PinGroupBrand([FromBody] PinGroupBrandCommand request)
        {
            _logger.LogInformation($"REST request to PinGroupBrand : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request toPinGroupBrand  fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("CreatNewGroupAndBrand")]
        public async Task<ActionResult<int>> CreatNewGroupAndBrand([FromBody] CreatNewGroupAndBrandRequest request)
        {
            _logger.LogInformation($"REST request CreatNewGroupAndBrand : {JsonConvert.SerializeObject(request)}");
            try
            {
                //Add Group
                request.GroupBrand.Id = new Guid();
                var gr = new GroupBrandAddCommand
                {
                    Id = request.GroupBrand.Id,
                    GroupBrandName = request.GroupBrand.GroupBrandName,
                    Pin = request.GroupBrand.Pin,
                    CreatedDate = DateTime.Now,
                    CreatedBy = new Guid(GetUserIdFromContext()),
                
                };
                 await _mediator.Send(gr);
                //Add Brandto group
                foreach (var item in request.Brands) {

                    var Br = new BrandAddCommand { 
                    Id= new Guid(),
                    BrandName=item.BrandName,
                    LogoBrand=item.LogoBrand,
                    Intro=item.Intro,
                    Pin=item.Pin,
                    CreatedDate= DateTime.Now,
                    CreatedBy= new Guid(GetUserIdFromContext()),
                    };
                    await _mediator.Send(Br);
                   
                }

                return Ok(1);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request CreatNewGroupAndBrand fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}


