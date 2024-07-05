using SaleManagement.BusinessObject.Entity;

namespace SaleManagement.Repository.ProductRepo
{
    public interface IProductReposity
    {
        Task<Product?> GetProduct(int id);
        Task<IEnumerable<Product>> GetProducts();
        Task<bool> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);
        Task<IEnumerable<Product>> SearchProduct(string valueSearch);
    }
}
