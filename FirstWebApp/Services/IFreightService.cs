using FirstWebApp.Models;

namespace FirstWebApp.Services
{
    public interface IFreightService
    {
        bool HasFreeFreight(Order order);
    }
}
