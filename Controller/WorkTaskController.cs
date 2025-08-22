using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;
using OfficeProject.Servicess;

namespace OfficeProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkTaskController : ControllerBase
    {
        private readonly IWorkTaskDetailsService _workTaskDetailsService;

    private readonly IDataSerializer? DataSerializer; 

        public WorkTaskController(IWorkTaskDetailsService workTaskDetailsService, IDataSerializer? dataSerializer)
        {
            _workTaskDetailsService = workTaskDetailsService;
            DataSerializer = dataSerializer;
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
        
        [HttpPost("backlink")]
        public async Task<IActionResult> AddOrUpdateBacklinkAsync([FromBody] WorkTaskDetailsDto workTaskDetailsDto)
        {
            DataSerializer?.Serializer(workTaskDetailsDto, "API Data");
            if (workTaskDetailsDto == null)
                return BadRequest("Work Task is missing.");

            try
            {
                await _workTaskDetailsService.AddOrUpdateBacklinkAsync(workTaskDetailsDto);
                return Ok(new { message = "Backlink saved or updated successfully." });
            }
            catch (Exception ex)
            {
                // Optionally log error here
                return StatusCode(500, new { message = "An error occurred while saving or updating backlink.", error = ex.Message });
            }
        }
        [HttpPost("classified")]
        public async Task<IActionResult> AddOrUpdateClassifiedAsync([FromBody] WorkTaskDetailsDto workTaskDetailsDto)
        {
            
            if (workTaskDetailsDto == null)
                return BadRequest("Work Task is missing.");

            try
            {
                await _workTaskDetailsService.AddOrUpdateClassifiedAsync(workTaskDetailsDto);
                return Ok(new { message = "Classified saved or updated successfully." });
            }
            catch (Exception ex)
            {
                // Optionally log error here
                return StatusCode(500, new { message = "An error occurred while saving or updating classified.", error = ex.Message });
            }
        }

        [HttpGet("issued-backlink/{serviceId}")]
        public async Task<IActionResult> GetIssuedBacklinks(int serviceId)
        {
            try
            {
                var result = await _workTaskDetailsService.GetIssuedBacklinkAsync(serviceId);
                return Ok(result);  // returns list of IDs
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("issued-classified/{serviceId}")]
        public async Task<IActionResult> GetIssuedClassifiedAsync(int serviceId)
        {
            try
            {
                var result = await _workTaskDetailsService.GetIssuedClassifiedAsync(serviceId);
                return Ok(result);  // returns list of IDs
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
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
