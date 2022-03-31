using FirstWebApp.Models;
using FirstWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApp.Pages
{
    public class OrderInfoModel : PageModel
    {
        private readonly NorthwindContext _context;
        private readonly IFreightService _freightService;

        public OrderInfoModel(NorthwindContext context, IFreightService freightService)
        {
            _context = context;
            _freightService = freightService;
        }
        public int Id { get; set; }
        public string Datum { get; set; }
        public string CustomerName { get; set; }
        public string ContactName { get; set; }

        public List<OrderDetailViewModel> OrderRows { get; set; }

        public class OrderDetailViewModel
        {
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }

            public decimal RowPrice()
            {
                return Quantity * UnitPrice;
            } 
        }

        public void OnGet(int id)
        {

            var order = _context.Orders
                .Include(ord=> ord.Customer)
                .Include(ord => ord.OrderDetails)
                .ThenInclude(orderdetail=> orderdetail.Product)
                .First(ord => ord.OrderId == id);
            Id = order.OrderId;
            CustomerName = order.Customer.CompanyName;
            ContactName = order.Customer.ContactName;
            Datum = order.OrderDate.Value.ToString("yyyy-MM-dd");
            HasFreeFreight = _freightService.HasFreeFreight(order);

            OrderRows = order.OrderDetails
                .Select(e=>new OrderDetailViewModel
                {
                    Quantity = e.Quantity,
                    UnitPrice = e.UnitPrice,
                    ProductName = e.Product.ProductName
                })
                .ToList();
        }

        public bool HasFreeFreight { get; set; }
    }
}
