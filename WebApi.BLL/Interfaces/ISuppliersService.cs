using System;
using System.Collections.Generic;
using WebApi.BLL.DTMs;
using System.Threading.Tasks;

namespace WebApi.BLL.Interfaces
{
    public interface ISuppliersService
    {
        Task<IEnumerable<SuppliersDTM>> GetAllAsync();

        Task<SuppliersDTM> GetByIdAsync(int id);
        Task<IEnumerable<SuppliersDTM>> GetByCategoryAsync(int id);
        IEnumerable<SuppliersDTM> GetByCity(string cityName);
        void AddAsync(SuppliersDTM supplierDTM);
        void DeleteAsync(int id);

        void UpdateAsync(SuppliersDTM suppliersDTM);
    }
}
