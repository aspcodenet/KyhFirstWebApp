using System.Reflection.Metadata;
using FirstWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApp.Pages
{
    public class OrdersModel : PageModel
    {
        public List<OrderViewModel> Orders { get; set; }

        public class OrderViewModel
        {
            public int Id { get; set; }
            public string DateTime { get; set; }
            public string CustomerName { get; set; }
        }

        private readonly NorthwindContext _context;

        public OrdersModel(NorthwindContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Orders = _context.Orders.Include(e=>e.Customer)
                .Select(o => new OrderViewModel
                {
                    Id = o.OrderId,
                    CustomerName = o.Customer.CompanyName,
                    DateTime = o.OrderDate.Value.ToString("yyyy-MM-dd")
                })
                .ToList();
        }
    }
}
