using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CategoriesController : ApiController
    {
        private readonly ICategoriesService service;
        private readonly IMapper mapper;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.service = categoriesService;
            MapperConfiguration config = new MapperConfiguration(con =>
            {
                con.CreateMap<CategoriesView, CategoriesDTM>();
                con.CreateMap<CategoriesDTM, CategoriesView>();
            });

            mapper = config.CreateMapper();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            IEnumerable<CategoriesDTM> categories = await service.GetAllAsync();
            if (categories == null)
            {
                return NotFound();
            }
            return Ok(categories);
        }


        [HttpGet]
        public async Task<IHttpActionResult> GetByIdAsync(int id)
        {
            CategoriesDTM category = await service.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public HttpResponseMessage Add([FromBody] CategoriesView categoryView)
        {
            HttpResponseMessage responseMessage;

            var category = mapper.Map<CategoriesDTM>(categoryView);
            try
            {
                service.AddAsync(category);
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
        public IHttpActionResult Update([FromBody] CategoriesView categoriesView)
        {
            var category = mapper.Map<CategoriesDTM>(categoriesView);
            try
            {
                service.UpdateAsync(category);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

    }
}
