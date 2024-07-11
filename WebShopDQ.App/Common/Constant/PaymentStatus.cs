using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.Common.Constant
{
    public static class PaymentStatus
    {
        public static string Pending = "Chờ thanh toán";
        public const string COD = "Thanh toán khi nhận hàng";
        public const string VNpay = "Thanh toán bằng VNPay";
        public const string Error = "Giao dịch lỗi";
    }
}
