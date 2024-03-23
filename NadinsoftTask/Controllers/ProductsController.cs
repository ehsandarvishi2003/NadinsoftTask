using Microsoft.AspNetCore.Mvc;
using NadinsoftTask.Models.Entity;
using NadinsoftTask.Models.Repository;
using NadinsoftTask.Models.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NadinsoftTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1", Deprecated = false)]
    public class ProductsController : ControllerBase
    {
        #region Ctor

        private readonly ProductRepository _productRepository;

        public ProductsController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        #endregion

        #region Get All

        // GET: api/<ProductsController>

        [HttpGet]
        public IActionResult GetAll()
        {
            var resualt = _productRepository.GetAllProducts();
            return Ok(resualt);
        }

        #endregion

        #region Get by Id

        // GET api/<ProductsController>/5

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _productRepository.GetById(id);
            return Ok(result);
        }

        #endregion

        #region Post

        // POST api/<ProductsController>
        [HttpPost]
        public IActionResult Post([FromBody] ProductViewModel model)
        {
            var result = _productRepository.Add(model);
            var url = Url.Action(nameof(GetAll), "Product", new { id = result.Id }, Request.Scheme);
            return Created("url", result);
        }

        #endregion

        #region Edite

        // PUT api/<ProductsController>/5
        [HttpPut]
        public IActionResult Put([FromBody] Product product)
        {
            var result=_productRepository.Edit(product);
            return Ok(result);
        }

        #endregion

        #region Delete by Id

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result=_productRepository.Delete(id);
            return Ok(result);

        }

        #endregion
    }
}
