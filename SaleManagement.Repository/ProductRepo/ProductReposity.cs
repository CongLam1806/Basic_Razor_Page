using Microsoft.EntityFrameworkCore;
using SaleManagement.BusinessObject.Entity;

namespace SaleManagement.Repository.ProductRepo
{
    public class ProductReposity : IProductReposity
    {
        private readonly FStoreContext _context;

        public ProductReposity(FStoreContext context)
        {
            _context = context;
        }

        public ProductReposity()
        {
            _context = new FStoreContext();
        }

        public async Task<bool> CreateProduct(Product product)
        {
            
            await _context.Products.AddAsync(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.OrderByDescending(p => p.ProductId).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var productToUpdate = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == product.ProductId);

            if (productToUpdate == null)
            {
                return false;
            }
            if(!product.ProductName.Equals(productToUpdate.ProductName))
            {
                if (await _context.Products.AnyAsync(p => p.ProductName.Equals(product.ProductName)))
                {
                    return false;
                }
            }
            

            _context.Products.Update(product);
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<Product?> GetProduct(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<IEnumerable<Product>> SearchProduct(string valueSearch)
        {
            var isNumber = int.TryParse(valueSearch, out int number);

            if(isNumber)
            {
                return await _context.Products.Where(p => p.UnitsInStock == number || p.UnitPrice == number || p.ProductId == number).ToListAsync();
            }

            return await _context.Products.Where(p => p.ProductName.Contains(valueSearch)).ToListAsync();

        }
    }
}
