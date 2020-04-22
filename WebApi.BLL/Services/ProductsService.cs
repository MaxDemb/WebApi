using System.Collections.Generic;
using System.Linq;
using WebApi.BLL.Interfaces;
using WebApi.BLL.DTMs;
using WebApi.DAL;
using WebApi.DAL.Entities;
using System;
using AutoMapper;
using System.Threading.Tasks;

namespace WebApi.BLL.Services
{
    public class ProductsService : IProductsService
    {
        private readonly UnitOfWork uow;
        private readonly IMapper mapper;
        public ProductsService()
        {
            uow = new UnitOfWork();

            MapperConfiguration config = new MapperConfiguration(con =>
            {
               con.CreateMap<ProductsDTM, Products>();
               con.CreateMap<Products, ProductsDTM>();
            });

            mapper = config.CreateMapper();

        }
        public async Task<IEnumerable<ProductsDTM>> GetAllAsync()
        {
            IEnumerable<Products> products = await Task.Run(() => uow.Products.GetAll());
            return mapper.Map<IEnumerable<ProductsDTM>>(products);
        }

        public async Task<ProductsDTM> GetByIdAsync(int id)
        {
            Products product = await Task.Run( () => uow.Products.Get(id));
            if(product == null)
            {
                throw new Exception();
            }
            return mapper.Map<ProductsDTM>(product);
        }

        public IEnumerable<ProductsDTM> GetByCategory(int id)
        {
            IEnumerable<Products> products = uow.Products.GetAll()
                .Where(x => x.Category.Id == id);
            return mapper.Map<IEnumerable<ProductsDTM>>(products);
        }

        public IEnumerable<ProductsDTM> GetMinPrice()
        {
            IEnumerable<Products> allProducts = uow.Products.GetAll();
            decimal minPrice = allProducts.Min(x => x.Price);
            IEnumerable<Products> products = allProducts.Where(x => x.Price == minPrice);
            return mapper.Map<IEnumerable<ProductsDTM>>(products);
        }

        public IEnumerable<ProductsDTM> GetMaxPrice()
        {
            IEnumerable<Products> allProducts = uow.Products.GetAll();
            decimal maxPrice = allProducts.Max(x => x.Price);
            IEnumerable<Products> products = allProducts.Where(x => x.Price == maxPrice);
            return mapper.Map<IEnumerable<ProductsDTM>>(products);

        }

        public IEnumerable<ProductsDTM> GetWithPrice(decimal priceNeed)
        {
            IEnumerable<Products> products = uow.Products.GetAll().Where(x => x.Price == priceNeed);
            return mapper.Map<IEnumerable<ProductsDTM>>(products);

        }


        public async void AddAsync(ProductsDTM productDTM)
        {
            var product = mapper.Map<Products>(productDTM);
            await Task.Run(() => uow.Products.Create(product));
        }

        public async void DeleteAsync(int id)
        {
            await Task.Run(() => uow.Products.Delete(id));
        }

        public async void UpdateAsync(ProductsDTM productsDTM)
        {

            var products = mapper.Map<Products>(productsDTM);
            await Task.Run(() => uow.Products.Update(products));
        }

        public IEnumerable<ProductsDTM> GetBySupplier(int id)
        {

            IEnumerable<Products> products = uow.Products.GetAll()
            .Where(x => x.Supplier.Id == id);
            return mapper.Map<IEnumerable<ProductsDTM>>(products);
        }
    }
}
