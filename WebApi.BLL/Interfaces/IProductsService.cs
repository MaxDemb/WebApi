using WebApi.BLL.DTMs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.BLL.Interfaces
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductsDTM>> GetAllAsync();

        Task<ProductsDTM> GetByIdAsync(int id);
        IEnumerable<ProductsDTM> GetByCategory(int id);
        IEnumerable<ProductsDTM> GetBySupplier(int id);
        IEnumerable<ProductsDTM> GetWithPrice(decimal priceNeed);
        IEnumerable<ProductsDTM> GetMaxPrice();
        IEnumerable<ProductsDTM> GetMinPrice();
        void AddAsync(ProductsDTM productsDTM);
        void DeleteAsync(int id);

        void UpdateAsync(ProductsDTM productsDTM);
    }
}
