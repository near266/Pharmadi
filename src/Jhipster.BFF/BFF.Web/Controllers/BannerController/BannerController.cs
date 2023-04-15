using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFF.Web.Controllers.BannerController
{

    [ApiController]
    [Route("gw/[controller]")]
    public class BannerController : ControllerBase
    {
        [HttpPost("ViewBigBanner")]
        public async Task<IActionResult> ViewBigBanner()
        {
            var value = new List<string>()
            {
                "https://image.pharmadi.vn/StorageProduct/photo_6149787805572773493_y_638162050740532203_ORIGIN.png",
                "https://image.pharmadi.vn/StorageProduct/photo_6149787805572773505_y_638162050740555799_ORIGIN.png"
            };
            return Ok(value);
        }
        [HttpPost("ViewBanner")]
        public async Task<IActionResult> ViewBanner()
        {
            var value = new List<string>()
            {
                 "https://image.pharmadi.vn/StorageProduct/photo_6152080691043612522_x_638162056676434910_ORIGIN.png",
                 "https://image.pharmadi.vn/StorageProduct/photo_6152080691043612522_x_638162056676434910_ORIGIN.png",
                 "https://image.pharmadi.vn/StorageProduct/photo_6152080691043612522_x_638162056676434910_ORIGIN.png",
                 "https://image.pharmadi.vn/StorageProduct/photo_6152080691043612522_x_638162056676434910_ORIGIN.png",
                 "https://image.pharmadi.vn/StorageProduct/photo_6152080691043612522_x_638162056676434910_ORIGIN.png",
                 "https://image.pharmadi.vn/StorageProduct/photo_6152080691043612522_x_638162056676434910_ORIGIN.png",
            };
            return Ok(value);
        }
    }
}
