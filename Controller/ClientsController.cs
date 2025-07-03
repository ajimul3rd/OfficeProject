using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;
using OfficeProject.Servicess;
using OfficeProject.Web.Pages;

namespace OfficeProject.Controller
{
   // [Authorize(Policy = "AdminOnly")] // 🔒 This ensures only authenticated users with valid JWTs can access
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService clientService;
        public ClientsController(IClientService clientService)
        {
            this.clientService = clientService;
        }



        // ✅ Add a client with associated projects
        [HttpPost]
        //[Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> AddClient([FromBody] ClientsDTO clientDto)
        {
            if (clientDto == null)
            {
                return BadRequest("Client data is required.");
            }

            try
            {
                await clientService.AddClienttAsync(clientDto);
                return Ok(new { message = "Client added successfully." });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                // Log this somewhere in production!
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // ✅ Get all clients with their projects for the logged-in user
        // GET: api/clients
        [HttpGet]
        //[Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult<List<ClientsDTO>>> GetClients()
        {
            try
            {
                var clients = await clientService.GetClientListAsync();
                return Ok(clients);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving clients", error = ex.Message });
            }
        }
    

}
}
