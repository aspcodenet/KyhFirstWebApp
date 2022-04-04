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


        public string SortOrder { get; set; }
        public string SortCol { get; set; }
        public int PageNo { get; set; }
        //[BindProperty(SupportsGet = true)]
        public string SearchWord { get; set; }

        public class OrderViewModel
        {
            public int Id { get; set; }
            public string DateTime { get; set; }
            public string CustomerName { get; set; }

            public bool HasFreeFreight { get; set; }
        }

        private readonly NorthwindContext _context;
        private readonly IFreightService _freightService;

        public void OnGet(string searchWord, string col = "id", string order = "asc", int pageno=1)
        {
            PageNo = pageno;
            SearchWord = searchWord;
            SortCol = col;
            SortOrder = order;

            var o  = _context.Orders.Include(e=>e.Customer).AsQueryable();

            if(!string.IsNullOrEmpty(SearchWord))
                o = o.Where( ord=>ord.Customer.CompanyName.Contains(SearchWord) 
                                      || ord.Customer.ContactName.Contains(SearchWord)  
                            );

            //Fortfarande inte skickat till SQL
            if (col == "id")
            {
                if(order == "asc")
                    o = o.OrderBy(ord => ord.OrderId);
                else
                    o = o.OrderByDescending(ord => ord.OrderId);
            }
            else if (col == "customer")
            {
                if (order == "asc")
                    o = o.OrderBy(ord => ord.Customer.CompanyName);
                else
                    o = o.OrderByDescending(ord => ord.Customer.CompanyName);
            }
            else if (col == "datum")
            {
                if (order == "asc")
                    o = o.OrderBy(ord => ord.OrderDate);
                else
                    o = o.OrderByDescending(ord => ord.OrderDate);
            }

            int toSkip = (pageno - 1) * 20;
            o = o.Skip(toSkip).Take(20);


            Orders = o.Select(o => new OrderViewModel
            {
                Id = o.OrderId,
                CustomerName = o.Customer.CompanyName,
                DateTime = o.OrderDate.Value.ToString("yyyy-MM-dd"),
                HasFreeFreight = _freightService.HasFreeFreight(o)
            }).ToList();

        }
    }
}
