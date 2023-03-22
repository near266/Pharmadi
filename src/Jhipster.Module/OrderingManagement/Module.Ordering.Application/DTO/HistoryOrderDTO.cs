using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Ordering.Application.DTO
{
    public class HistoryOrderDTO
    {
        public Guid MerchantId { get; set; }
        public Decimal ShippingFee { get; set; }
        public Decimal TotalPrice { get; set; }
        public Decimal TotalPayment { get; set; }
        public int Status { get; set; }
        public int ToTalProduct { get; set; }  // số sản phẩm
        public int ToTalOrderItem { get; set; }// số loại sp
    }
    public class HistoryOrderDTOs
    {
        public Guid MerchantId { get; set; }
        public Decimal ShippingFee { get; set; }
        public Decimal TotalPrice { get; set; }
        public Decimal TotalPayment { get; set; }
        public int Status { get; set; }
        public Guid OrderItemId { get; set; }
        public int QuantityOrderItem { get; set; }
    }
}
