using System.Reflection.Metadata;
using FirstWebApp.Models;
using FirstWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApp.Pages
{
    public class OrdersModel : PageModel
    {
        public OrdersModel(NorthwindContext context, IFreightService freightService)
        {
            _context = context;
            _freightService = freightService;
        }

        public List<OrderViewModel> Orders { get; set; }

        public class OrderViewModel
        {
            public int Id { get; set; }
            public string DateTime { get; set; }
            public string CustomerName { get; set; }

            public bool HasFreeFreight { get; set; }
        }

        private readonly NorthwindContext _context;
        private readonly IFreightService _freightService;

        public void OnGet()
        {
            Orders = _context.Orders.Include(e=>e.Customer)
                .Select(o => new OrderViewModel
                {
                    Id = o.OrderId,
                    CustomerName = o.Customer.CompanyName,
                    DateTime = o.OrderDate.Value.ToString("yyyy-MM-dd"),
                    HasFreeFreight = _freightService.HasFreeFreight(o)
                })
                .ToList();
        }
    }
}
