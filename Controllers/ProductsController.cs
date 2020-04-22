using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.BLL.DTMs;
using WebApi.BLL.Interfaces;
using WebApi.Models;
using AutoMapper;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    
    public class ProductsController : ApiController
    {
        private readonly IProductsService service;
        private readonly ICategoriesService categoriesService;
        private readonly ISuppliersService suppliersService;
        private readonly IMapper mapper;

        public ProductsController(IProductsService productsService, ICategoriesService categoriesService, ISuppliersService suppliersService)
        {
            this.service = productsService;
            this.categoriesService = categoriesService;
            this.suppliersService = suppliersService;
            MapperConfiguration config = new MapperConfiguration(con =>
            {
                con.CreateMap<ProductsView, ProductsDTM>();
                con.CreateMap<ProductsDTM, ProductsView>();
            });

            mapper = config.CreateMapper();

        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllProducts()
        {
            IEnumerable<ProductsDTM> products = await service.GetAllAsync();
            if (products == null)
            {
                return BadRequest("No content");
            }
            return Ok(products);
        }


        [HttpGet]
        public async Task<IHttpActionResult> GetProductByIdAsync(int id)
        {
            ProductsDTM product  = await service.GetByIdAsync(id);
            if (product == null)
            {
                return BadRequest("There is no products with this id.");
            }
            return Ok(product);
        }

        [HttpPost]
        public HttpResponseMessage AddProduct([FromBody] ProductsView productView)
        {
            HttpResponseMessage responseMessage;
            var product = mapper.Map<ProductsDTM>(productView);
            try
            {
                service.AddAsync(product);
                responseMessage = new HttpResponseMessage(HttpStatusCode.OK);

            }
            catch
            {

                responseMessage = new HttpResponseMessage(HttpStatusCode.NotAcceptable);
                responseMessage.Content = new StringContent("Name can`t be empty!");
            }
            return responseMessage;
        }

        [HttpDelete]
        public HttpResponseMessage DeleteProduct(int id)
        {

            HttpResponseMessage responseMessage;
            try
            {
                service.DeleteAsync(id);
                responseMessage = new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            catch
            {

                responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);

            }
            return responseMessage;
        }

        [HttpPut]
        public IHttpActionResult UpdateProduct([FromBody] ProductsView productView)
        {
            var product = mapper.Map<ProductsDTM>(productView);
            try
            {
                service.UpdateAsync(product);
                return Ok("Updated product:" + product);
                

            }
            catch
            {
                return BadRequest("Can`t update products");        
            }
        }

        [HttpGet]
        [Route("Api/Categories/{id}/Products")]
        public async Task<IHttpActionResult> GetProductsByCategoryIdAsync(int id)
        {
            CategoriesDTM category = await categoriesService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            IEnumerable<ProductsDTM> result = service.GetByCategory(id);

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
            

        [HttpGet]
        [Route("Api/Suppliers/{id}/Products")]
        public async Task<IHttpActionResult> GetProductsBySupplierIdAsync(int id)
        {
            SuppliersDTM supplier = await suppliersService.GetByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            IEnumerable<ProductsDTM> result = service.GetBySupplier(id);


            return Ok();
        }

        [HttpGet]
        [Route("Api/Products/MinPrice")]
        public IHttpActionResult GetProductsWithMinPrice()
        {
            IEnumerable<ProductsDTM> products = service.GetMinPrice();
            if(products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet]
        [Route ("Api/Products/MaxPrice")]
        public IHttpActionResult GetPRoductsWithMaxPrice()
        {
            IEnumerable<ProductsDTM> products = service.GetMaxPrice();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet]
        [Route("Api/Products/Price/{Price}")]
        public IHttpActionResult GetProductsByPrice(decimal Price)
        {

            IEnumerable<ProductsDTM> products = service.GetWithPrice(Price);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

    }
}
