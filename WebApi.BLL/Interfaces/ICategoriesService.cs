using WebApi.BLL.DTMs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.BLL.Interfaces
{
    public interface ICategoriesService
    {
        Task<IEnumerable<CategoriesDTM>> GetAllAsync();

        Task<CategoriesDTM> GetByIdAsync(int id);
        void AddAsync(CategoriesDTM categoriesDTM);
        void DeleteAsync(int id);

        void UpdateAsync(CategoriesDTM categoriesDTM);
    }
}
