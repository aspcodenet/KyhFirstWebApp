using FirstWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FirstWebApp.Pages
{
    public class SuppliersModel : PageModel
    {
        public List<string> Suppliers = new List<string>();
        public string Weekday { get; set; }

        public void OnGet()
        {
            Weekday = DateTime.Now.DayOfWeek.ToString();

            using (var context = new NorthwindContext())
            {
                Suppliers = context.Suppliers.Select(s => s.CompanyName).ToList();
            }
        }
    }
}
