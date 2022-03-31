using FirstWebApp.Models;

namespace FirstWebApp.Services
{
    public class FreightService : IFreightService
    {
        
        public bool HasFreeFreight(Order order)
        {
            return order.Customer.City.StartsWith("A");
        }
    }
}
