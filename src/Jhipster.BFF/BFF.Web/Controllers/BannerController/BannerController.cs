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
        [HttpPost("IconToll")]
        public async Task<IActionResult> IconTool()//nhóm công dụng
        {
            var value = new List<string>()
            {
               "https://image.pharmadi.vn/StorageProduct/Bệnh-phụ-khoa_638210961932979203_ORIGIN.png",
               "https://image.pharmadi.vn/StorageProduct/Cải-thiện-sinh-lý-nam_638210961933037905_ORIGIN.png",
               "https://image.pharmadi.vn/StorageProduct/Cơ-xương-khớp_638210961933050279_ORIGIN.png",
               "https://image.pharmadi.vn/StorageProduct/Dinh-dưỡng_638210961933059517_ORIGIN.png",
               "https://image.pharmadi.vn/StorageProduct/Hỗ-trợ-bổ-não,-thần-kinh_638210961933068080_ORIGIN.png",
               "https://image.pharmadi.vn/StorageProduct/Hỗ-trợ-tiêu-hóa_638210961933076441_ORIGIN.png",
               "https://image.pharmadi.vn/StorageProduct/Làm-đẹp-và-sinh-lý-nữ_638210961933085484_ORIGIN.png",
               "https://image.pharmadi.vn/StorageProduct/Sức-khỏe-trẻ-em_638210961933093731_ORIGIN.png",
               "https://image.pharmadi.vn/StorageProduct/Tăng-cường-đề-kháng_638210961933102598_ORIGIN.png",
               "https://image.pharmadi.vn/StorageProduct/Thận-tiết-niệu_638210961933110682_ORIGIN.png",
               "https://image.pharmadi.vn/StorageProduct/Tim-mạch-mỡ-máu_638210961933118289_ORIGIN.png",
               "https://image.pharmadi.vn/StorageProduct/Vitamin-và-khoáng-chất_638210961933125861_ORIGIN.png"
            };
            return Ok(value);
        }
        [HttpPost("BannerServices")]
        public async Task<IActionResult> BannerServices()// dịch vụ
        {
            var value = new List<string>()
            {
               "https://image.pharmadi.vn/StorageProduct/cải-thiện-tồn-kho_638210966560746524_ORIGIN.png",
               "https://image.pharmadi.vn/StorageProduct/Tăng-trưởng-doanh-thu_638210966560775666_ORIGIN.png",
               "https://image.pharmadi.vn/StorageProduct/thu-hút-khách-hàng_638210966560789007_ORIGIN.png"
            };
            return Ok(value);
        }

        [HttpPost("Content")]
        public async Task<IActionResult> Content()// dịch vụ
        {
            var value = new List<string>()
            {
              "https://image.pharmadi.vn/StorageProduct/1_638210970682855790_ORIGIN.png",
              "https://image.pharmadi.vn/StorageProduct/2_638210970682871065_ORIGIN.png",
              "https://image.pharmadi.vn/StorageProduct/3_638210970682883834_ORIGIN.png",
              "https://image.pharmadi.vn/StorageProduct/4_638210970682897415_ORIGIN.png"
            };
            return Ok(value);
        }
    }
}
