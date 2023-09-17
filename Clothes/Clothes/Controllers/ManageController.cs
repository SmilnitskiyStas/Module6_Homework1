using Clothes.Interfaces;
using Clothes.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clothes.Controllers
{
    [Route("/api/[controller]")]
    public class ManageController : Controller, IAdded
    {
        private int NextProductId = products.Count() == 0 ? 1 : products.Max(x => x.Id) + 1;

        private static List<Product> products = new List<Product>(new[]
        {
            new Product { Id = 1, Name = "Shirt", Price = 14.99m},
            new Product { Id = 2, Name = "Hat", Price = 21.99m},
            new Product { Id = 3, Name = "Jince", Price = 18.99m},
        });

        [HttpGet("Products")]
        public IEnumerable<Product> Get() => products;

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Product? product = products.SingleOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            products.Remove(products.SingleOrDefault(p => p.Id == id));
            return Ok();
        }

        [HttpPost("AddProduct")]
        public IActionResult Add(Product product)  // Будуть створюватися форми для заповнення
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            product.Id = NextProductId;
            products.Add(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product); // CreatedAtAction вказує на те, що доданий товар буде відразу нам доступний
        }

        //[HttpPost("AddProduct")]
        //public IActionResult PostBody([FromBody] Product product) => Post(product); // Додавання товару в вигляді JSON об'єкта

        [HttpPut("EditProduct")]
        public IActionResult Put(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var storedProduct = products.SingleOrDefault(p => p.Id == product.Id);
            if (storedProduct == null)
            {
                return NotFound(product.Id);
            }

            storedProduct.Name = product.Name;
            storedProduct.Price = product.Price;

            return Ok(storedProduct);
        }
    }
}
