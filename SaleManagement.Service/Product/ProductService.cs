using SaleManagement.BusinessObject.Entity;
using SaleManagement.Repository.ProductRepo;

namespace SaleManagement
    .Service
{
    public class ProductService : IProductService
    {
        private readonly IProductReposity _productReposity;

        public ProductService(IProductReposity productReposity)
        {
            _productReposity = productReposity;
        }

        public async Task<Product?> GetProduct(int id)
        {
            return await _productReposity.GetProduct(id);
        }

        public async Task<IEnumerable<Product>> GetProducts(string valueSearch = "")
        {
            if(!string.IsNullOrEmpty(valueSearch))
            {
                return await _productReposity.SearchProduct(valueSearch);
            }

            return await _productReposity.GetProducts();
        }

        public async Task<bool> CreateProduct(Product product)
        {
            return await _productReposity.CreateProduct(product);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            return await _productReposity.DeleteProduct(productId);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            return await _productReposity.UpdateProduct(product);
        }

    }
}
