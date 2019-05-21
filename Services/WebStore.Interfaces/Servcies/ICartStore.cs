using WebStore.Domain.Models;

namespace WebStore.Interfaces.Servcies
{
    public interface ICartStore
    {
        Cart Cart { get; set; }
    }
}