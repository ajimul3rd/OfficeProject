using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeProject.Models.DTO;
using OfficeProject.Servicess;

namespace OfficeProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkTaskController : ControllerBase
    {
        private readonly IWorkTaskDetailsService _workTaskDetailsService;

        public WorkTaskController(IWorkTaskDetailsService workTaskDetailsService)
        {
            _workTaskDetailsService = workTaskDetailsService;
        }


        [HttpPost("save-or-update")]
        //[Authorize(Policy = "AdminOrManagerOrUser")]
        public async Task<IActionResult> AddOrUpdateUserWorkingRecordAsync([FromBody] WorkTaskDetailsDto workTaskDetailsDto)
        {
            if (workTaskDetailsDto == null)
                return BadRequest("Work Task is missing.");

            try
            {
                await _workTaskDetailsService.AddOrUpdateUserWorkingRecordAsync(workTaskDetailsDto);
                return Ok(new { message = "Work Task saved or updated successfully." });
            }
            catch (Exception ex)
            {
                // Optionally log error here
                return StatusCode(500, new { message = "An error occurred while saving or updating work task.", error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<WorkTaskDetailsDto?>>> GetWorkingRecords()
        {
            try
            {
                var result = await _workTaskDetailsService.GetWorkingRecordPerUserAsync();
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{workTaskId}")]
        public async Task<IActionResult> GetWorkTaskDetailsById(int workTaskId)
        {
            try
            {
                var result = await _workTaskDetailsService.GetWorkTaskDetailsById(workTaskId);

                if (result == null)
                {
                    return NotFound(new { message = "Work task not found." });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving work task details.",
                    error = ex.Message
                });
            }
        }


    }
}
