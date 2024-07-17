using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.ViewModels
{
    public class WeeklyRevenue
    {
        public string Week { get; set; } = null!;
        public float Revenue { get; set; }
    }
    public class DailyRevenue
    {
        public string Date { get; set; } = null!;
        public float Revenue { get; set; }
    }
}
