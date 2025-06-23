using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeProject.Models.DTO;
using OfficeProject.Servicess;

namespace OfficeProject.Controller
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }


        [HttpPost]
        //[Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> AddOrUpdateProductAsync([FromBody] ProductsDTO productsDto)
        {
            if (productsDto == null)
            {
                return BadRequest("Project data is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _productsService.AddOrUpdateProductAsync(productsDto);
                return Ok(new { message = "Products added successfully." });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductsDTO>> GetProductDTOById(int id)
        {
            try
            {
                var productDto = await _productsService.GetProductsDTOById(id);
                return Ok(productDto);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving products", error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductsDTO>>> GetAllProductsDTOAsync()
        {
            try
            {
                var productDto = await _productsService.GetAllProductsDTOAsync();
                return Ok(productDto);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving products", error = ex.Message });
            }
        }

       
    }
}
