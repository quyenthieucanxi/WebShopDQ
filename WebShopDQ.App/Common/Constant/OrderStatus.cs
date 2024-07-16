using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.Common.Constant
{
    public static class OrderStatus
    {
        public const string Pending = "Chờ xác nhận";
        public const string Confirm = "Xác nhận";
        public const string Handle = "Đang xử lý";
        public const string Delivering = "Đang giao";
        public const string Delivered = "Đã giao";
        public const string Cancle = "Huỷ";
        public const string Review = "Đánh giá";
        public const string Return = "Trả hàng";
    }
}
