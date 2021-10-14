using GeneralStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeneralStoreAPI.Controllers
{
    public class ProductController : ApiController
    {
        //Repo pattern in API
        //Where we store the Data
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        //CRUD (aka Post Get Put Delete)
        [HttpPost]
        public async Task<IHttpActionResult> PostProduct(Product model)
        {
            if (model == null)
                return BadRequest("no information was given");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Products.Add(model);
            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok($"{model.Name} was added to the database");
            }
            return InternalServerError();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllProducts()
        {
            return Ok(await _context.Products.ToListAsync());
        }
        //Get Product by SKU
        [HttpGet]
        //[Route("api/Product/GetProductBySKU/{sKU}")]  <--This can be used to make the URI not so ugly
        public async Task<IHttpActionResult> GetProductBySKU(string sKU)
        {
            Product product = await _context.Products.FindAsync(sKU);
            if(product == null)
                return NotFound();
            return Ok(product);
        }
        //Update Product by SKU
        [HttpPut]
        public async Task<IHttpActionResult> UpdateProductBySKU(string sKU, [FromBody]Product model)
        {
            if (model == null)
                return BadRequest("please enter data");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Product product = await _context.Products.FindAsync(sKU);

            if (product == null)
                return NotFound();
            //DO NOT UPDATE KEY VALUES!!!
            product.Name = model.Name;
            product.Price = model.Price;
            product.NumInStock = model.NumInStock;
            product.Description = model.Description;
            
            if(await _context.SaveChangesAsync() == 1)
                return Ok();

            return InternalServerError();
        }

        //Delete Product by SKU
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteProductBySKU([FromUri]string sKU)
        {
            Product product = await _context.Products.FindAsync(sKU);

            if (product == null)
                return NotFound();

            _context.Products.Remove(product);

            if (await _context.SaveChangesAsync() == 1)
                return Ok($"{product.Name} was deleted");

            return InternalServerError();
        }
    }
}
