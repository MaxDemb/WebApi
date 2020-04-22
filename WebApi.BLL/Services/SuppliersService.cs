using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.BLL.DTMs;
using WebApi.BLL.Interfaces;
using WebApi.DAL;
using WebApi.DAL.Entities;
using AutoMapper;
using System.Threading.Tasks;

namespace WebApi.BLL.Services
{
    public class SuppliersService : ISuppliersService
    {

        private readonly UnitOfWork uow;
        private readonly IMapper mapper;

        public SuppliersService()
        {
            uow = new UnitOfWork();

            MapperConfiguration config = new MapperConfiguration(con =>
            {
                con.CreateMap<Suppliers, SuppliersDTM>();
                con.CreateMap<SuppliersDTM, Suppliers>();
            });
            this.mapper = config.CreateMapper();
        }
        public async Task<IEnumerable<SuppliersDTM>> GetAllAsync()
        {
            IEnumerable<Suppliers> suppliers = await Task.Run(() => uow.Suppliers.GetAll());
            return mapper.Map<IEnumerable<SuppliersDTM>>(suppliers);
        }

        public async Task<SuppliersDTM> GetByIdAsync(int id)
        {
            Suppliers supplier = await Task.Run(() => uow.Suppliers.Get(id));
            if(supplier == null)
            {
                return null;
            }
            return mapper.Map<SuppliersDTM>(supplier);
        }

        public async Task<IEnumerable<SuppliersDTM>> GetByCategoryAsync(int id)
        {

            IEnumerable<Products> products = await Task.Run(() => uow.Products.GetAll().Where(x => x.Category.Id == id));
            IEnumerable<Suppliers> suppliersAll = await Task.Run(() => uow.Suppliers.GetAll());
            HashSet<Suppliers> suppliers = new HashSet<Suppliers>();
            foreach (var i in suppliersAll)
            {
                foreach (var j in products)
                {

                    if (i.Id == j.Supplier.Id)
                    {
                        suppliers.Add(i);
                    }
                }
            }
            return mapper.Map<IEnumerable<SuppliersDTM>>(suppliers);
        }

        public IEnumerable<SuppliersDTM> GetByCity(string cityName)
        {
            IEnumerable<Suppliers> suppliers = uow.Suppliers.GetAll().Where(x => x.City == cityName);
            return mapper.Map<IEnumerable<SuppliersDTM>>(suppliers);
        }

        public async void AddAsync(SuppliersDTM supplierDTM)
        {
            var supplier = mapper.Map<Suppliers>(supplierDTM);
            await Task.Run(() => uow.Suppliers.Create(supplier));
        }

        public async void DeleteAsync(int id)
        {
           await Task.Run(() => uow.Suppliers.Delete(id));
        }

        public async void UpdateAsync(SuppliersDTM suppliersDTM)
        {
            var supplier = mapper.Map<Suppliers>(suppliersDTM);
            await Task.Run(() => uow.Suppliers.Update(supplier));
        }

    }
}
 