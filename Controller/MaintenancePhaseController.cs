//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using OfficeProject.Models.DTO;
//using OfficeProject.Servicess;

//namespace OfficeProject.Controller
//{

//    [Authorize(Policy = "AdminOnly")] // 🔒 This ensures only authenticated users with valid JWTs can access
//    [Route("api/maintanence")]
//    [ApiController]
//    public class MaintenancePhaseController : ControllerBase
//    {
//        private readonly IMaintenancePhaseServices _maintenancePhaseServices;

//        public MaintenancePhaseController(IMaintenancePhaseServices maintenancePhaseServices)
//        {
//            _maintenancePhaseServices = maintenancePhaseServices;
//        }

//        // ✅ POST add DesignPhase for create or update
//        [HttpPost("add-or-update")]
//        public async Task<IActionResult> AddMaintenancePhaseAsync([FromBody] MaintenancePhaseDTO maintenancePhaseDTO)
//        {
//            if (maintenancePhaseDTO == null)
//                return BadRequest("MaintenancePhaseDTO is null.");

//            try
//            {
//                await _maintenancePhaseServices.AddMaintenancePhaseAsync(maintenancePhaseDTO);
//                return Ok(new { message = "Maintenance task saved successfully." });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { error = ex.Message });
//            }
//        }


//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetDesignPhase(int id)
//        {
//            if (id <= 0)
//                return BadRequest("Invalid ID.");

//            try
//            {
//                var result = await _maintenancePhaseServices.GetMaintenancePhaseAsync(id);
//                if (result == null)
//                    return NotFound(new { message = $"MaintenancePhase with ID {id} not found." });

//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { error = ex.Message });
//            }
//        }
//    }
//}