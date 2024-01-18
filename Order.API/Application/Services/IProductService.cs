using Order.API.DTOs;

namespace Order.API.Application.Services
{
    public interface IProductService
    {
        void UpdateProductQuantity(List<ProductUpdateQuantity> listProductUpdateQuantity);
    }
}
