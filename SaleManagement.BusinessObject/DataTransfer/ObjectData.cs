using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManagement.BusinessObject.DataTransfer
{
    public class ObjectData
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Value { get; set; }
    }
}
