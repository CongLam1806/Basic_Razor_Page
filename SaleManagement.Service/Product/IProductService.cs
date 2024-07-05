using SaleManagement.BusinessObject.Entity;

namespace SaleManagement.Service
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts(string valueSearch = "");
        Task<Product?> GetProduct(int id);
        Task<bool> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int productId);
    }
}
