using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstWebApiCoreProject.Context;
using FirstWebApiCoreProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApiCoreProject.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ProjectContext db;

        public ProductController(ProjectContext _db)
        {
            this.db = _db ?? throw new ArgumentNullException(nameof(db));

            if (db.Products.Count() == 0)
            {   
                db.Products.Add(new Product { ID = 19201, Name = "Lego Nexo Knights King I", Price = 45 });
                db.Products.Add(new Product { ID = 23942, Name = "Lego Starwars Minifigure Jedi", Price = 55 });
                db.Products.Add(new Product { ID = 30021, Name = "Star Wars çay takımı ", Price = 35.50 });
                db.Products.Add(new Product { ID = 30492, Name = "Star Wars kahve takımı", Price = 24.40 });
                
                db.SaveChanges();
            }


        }


        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return db.Products.ToList();
        }

        [HttpGet("{id}",Name ="GetProductById")]
        public IActionResult Get(int id)
        {
            var product = db.Products.Find(id);

            if (product==null)
            {
                return NotFound();
            }

            return new ObjectResult(product);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Product product)
        {
            if (product==null)
            {
                return BadRequest();
            }

            db.Products.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("GetProductById", new { id = product.ID }, product);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Product product)
        {
            var updateProduct = db.Products.Find(id);

            if (updateProduct == null)
            {
                return NotFound();
            }

            updateProduct.ID = id;
            updateProduct.Name = product.Name;
            updateProduct.Price = product.Price;
            

            db.SaveChanges();

            return CreatedAtRoute("Get",updateProduct);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var wbDeleted = db.Products.Find(id);
            if (wbDeleted==null)
            {
                return NotFound();
            }

            db.Products.Remove(wbDeleted);
            db.SaveChanges();

            return Ok();
        }

    }
}