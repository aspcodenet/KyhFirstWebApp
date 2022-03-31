using FirstWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FirstWebApp.Pages
{
    public class SuppliersModel : PageModel
    {
        private readonly NorthwindContext _context;

        public SuppliersModel(NorthwindContext context)
        {
            _context = context;
        }

        public class SupplierViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string City { get; set; }
        }
        public List<SupplierViewModel> Suppliers = new List<SupplierViewModel>();
        public string Weekday { get; set; }

        public void OnGet()
        {
            Weekday = DateTime.Now.DayOfWeek.ToString();

            Suppliers = _context.Suppliers.Take(30).Select(s => 
                 new SupplierViewModel
                 {
                     City = s.City,
                     Id = s.SupplierId,
                     Name = s.CompanyName
                 } ).ToList();
        }
    }
}
