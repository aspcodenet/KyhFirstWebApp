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
            Suppliers.Add("Supplier 1");
            Suppliers.Add("Supplier 2");
            Suppliers.Add("Supplier 3");
        }
    }
}
