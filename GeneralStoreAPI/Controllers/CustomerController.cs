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
    public class CustomerController : ApiController
    {
        //This is where we store the data
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        //CRUD
        //Create (POST)
        [HttpPost]
        public async Task<IHttpActionResult> PostCustomer(Customer model)
        {
            if (model == null)
                return BadRequest("no information was given");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Customers.Add(model);
            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok($"{model.FullName} was added to the database");
            }
            return InternalServerError();
        }
        //Read (GET) All Customers
        [HttpGet]
        public async Task<IHttpActionResult> GetAllCustomers()
        {
            return Ok(await _context.Customers.ToListAsync());
        }
        //Read (GET) by Customer by Id
        [HttpGet]
        public async Task<IHttpActionResult> GetCustomerById(int Id)
        {
            Customer customer = await _context.Customers.FindAsync(Id);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }
        //Update Customer by Id
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCustomerById(int Id, Customer model)
        {
            if (model == null)
                return BadRequest("please enter data");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Customer customer = await _context.Customers.FindAsync(Id);

            if (customer == null)
                return NotFound();
            //DO NOT UPDATE KEY VALUES!!!
            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            

            if (await _context.SaveChangesAsync() == 1)
                return Ok($"{Id} was updated");

            return InternalServerError();
        }
        //Delete
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCustomerById(int Id)
        {
            Customer customer = await _context.Customers.FindAsync(Id);

            if (customer == null)
                return NotFound();

            _context.Customers.Remove(customer);

            if (await _context.SaveChangesAsync() == 1)
                return Ok($"{customer.FullName} was deleted");

            return InternalServerError();
        }
    }
}
