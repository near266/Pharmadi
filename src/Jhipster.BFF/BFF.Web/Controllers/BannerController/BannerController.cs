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
                "https://image.pharmadi.vn/StorageProduct/photo_6109251637945152821_y_638151259858033496_ORIGIN.png",
                "https://image.pharmadi.vn/StorageProduct/photo_6109251637945152821_y_638151259858033496_ORIGIN.png"
            };
            return Ok(value);
        }
    }
}
