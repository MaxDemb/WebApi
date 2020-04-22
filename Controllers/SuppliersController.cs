using System.Collections.Generic;
using System.Web.Http;
using WebApi.BLL.Interfaces;
using WebApi.BLL.DTMs;
using System.Net.Http;
using System.Net;
using AutoMapper;
using WebApi.Models;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class SuppliersController : ApiController
    {
        ISuppliersService service;
        ICategoriesService categoriesService;

        private readonly IMapper mapper;

        public SuppliersController(ISuppliersService suppliersService, ICategoriesService categoriesService)
        {
            this.service = suppliersService;
            this.categoriesService = categoriesService;
            MapperConfiguration config = new MapperConfiguration(con =>
            {
                con.CreateMap<SuppliersView, SuppliersDTM>();
                con.CreateMap<SuppliersDTM, SuppliersView>();
            });

            mapper = config.CreateMapper();

        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            IEnumerable<SuppliersDTM> suppliers = await service.GetAllAsync();
            if(suppliers == null)
            {
                return NotFound();
            }
            return Ok(suppliers);
        }


        [HttpGet]
        public async Task<IHttpActionResult> GetAsync(int id)
        {
            SuppliersDTM supplier = await service.GetByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost]
        public HttpResponseMessage Add([FromBody] SuppliersView supplierView)
        {
            var supplier = mapper.Map<SuppliersDTM>(supplierView);

            HttpResponseMessage responseMessage;
            try
            {
                service.AddAsync(supplier);
                responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {
                
                responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                responseMessage.Content = new StringContent("Name can`t be empty!");
            }
            return responseMessage;
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {

            HttpResponseMessage responseMessage;
            try
            {
                service.DeleteAsync(id);
                responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {

                responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);

            }
            return responseMessage;
        }

        [HttpPut]
        public IHttpActionResult Update([FromBody] SuppliersView supplierView)
        {
            var supplier = mapper.Map<SuppliersDTM>(supplierView);
            try
            {
                service.UpdateAsync(supplier);
                return Ok();

            }
            catch 
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("Api/Categories/{id}/Suppliers")]
        public async Task<IHttpActionResult> GetSuppliersByCategoryAsync(int id)
        {
            CategoriesDTM category = await categoriesService.GetByIdAsync(id);
            if(category == null)
            {
                return NotFound();
            }
            IEnumerable<SuppliersDTM> suppliers = await service.GetByCategoryAsync(id);
            if(suppliers == null)
            {
                return NotFound();
            }
            return Ok(suppliers);
        }
    }

}
