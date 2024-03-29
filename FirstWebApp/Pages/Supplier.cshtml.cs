using FirstWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FirstWebApp.Pages
{
    public class SupplierModel : PageModel
    {
        private readonly NorthwindContext _context;


        public string CompanyName { get; set; } = null!;
        public string? ContactName { get; set; }
        public string? ContactTitle { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }


        public SupplierModel(NorthwindContext context)
        {
            _context = context;
        }
        public void OnGet(int supplierId)
        {
            var supplier = _context.Suppliers.First(e => e.SupplierId == supplierId);
            CompanyName = supplier.CompanyName;
            ContactName = supplier.ContactName;
            ContactTitle = supplier.ContactTitle;
            Address = supplier.Address;
            City = supplier.City;
        }
    }
}
