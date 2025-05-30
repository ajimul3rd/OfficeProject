using Microsoft.AspNetCore.Mvc;
using OfficeProject.Servicess;

namespace OfficeProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignUserController : ControllerBase
    {
        private readonly IAssignedUsersService _assignedUsersService;

        public AssignUserController(IAssignedUsersService assignedUsersService)
        {
            _assignedUsersService = assignedUsersService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignedUser(int id)
        {
            var result = await _assignedUsersService.DeleteAssignedUserAsync(id);
            if (!result)
                return NotFound();

            return Ok(new { message = "Assigned user deleted successfully." });
        }
    }
}
