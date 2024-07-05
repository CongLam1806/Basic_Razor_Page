using SaleManagement.BusinessObject.DataTransfer;

namespace SaleManagement.BusinessObject.Constants
{
    public class Data
    {
        public static IEnumerable<ObjectData> Shippings = new List<ObjectData>
        {
            new() { Id = 1, Name = "Standard Shipping", Value = 10000 },
            new() { Id = 2, Name = "Normal Shipping", Value = 20000 },
            new() { Id = 3, Name = "Express Shipping", Value = 30000 }
        };

        public static IEnumerable<ObjectData> Discounts = new List<ObjectData>
        {
            new() { Id = 1, Name = "PrideMonth", Value = 0 },
            new() { Id = 2, Name = "Birthday", Value = 10 },
            new() { Id = 3, Name = "FPTU", Value = 20 },
            new() { Id = 4, Name = "CocSaiGon", Value = 30 }
        };
    }
}
