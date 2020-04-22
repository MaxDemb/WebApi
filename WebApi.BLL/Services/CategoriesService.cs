using System.Collections.Generic;
using WebApi.BLL.Interfaces;
using WebApi.BLL.DTMs;
using WebApi.DAL;
using WebApi.DAL.Entities;
using AutoMapper;
using System.Threading.Tasks;

namespace WebApi.BLL.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly UnitOfWork uow;
        private readonly IMapper mapper;

        public CategoriesService()
        {
            uow = new UnitOfWork();

            MapperConfiguration config = new MapperConfiguration(con =>
            {
                con.CreateMap<CategoriesDTM, Categories>();
                con.CreateMap<Categories, CategoriesDTM>();
            });

            this.mapper = config.CreateMapper();
        }


        public async  Task<IEnumerable<CategoriesDTM>> GetAllAsync()
        {
            IEnumerable<Categories> categories = await Task.Run(() => uow.Categories.GetAll());
            return mapper.Map<IEnumerable<CategoriesDTM>>(categories);
        }

        public async Task<CategoriesDTM> GetByIdAsync(int id)
        {
            Categories categories = await Task.Run(() => uow.Categories.Get(id));
            if(categories == null)
            {
                return null;
            }
            return mapper.Map<CategoriesDTM>(categories);
        }



        public async void AddAsync(CategoriesDTM categoriesDTM)
        {
            var categories = mapper.Map<Categories>(categoriesDTM);
            await Task.Run(() => uow.Categories.Create(categories));
        }

        public async void DeleteAsync(int id)
        {
            await Task.Run(() => uow.Products.Delete(id));
        }

        public async void UpdateAsync(CategoriesDTM categoriesDTM)
        {
            var categories = mapper.Map<Categories>(categoriesDTM);
            await Task.Run(() => uow.Categories.Update(categories));
        }

    }
}
