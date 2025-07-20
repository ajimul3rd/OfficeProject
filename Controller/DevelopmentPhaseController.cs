//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using OfficeProject.Models.DTO;
//using OfficeProject.Servicess;

//namespace OfficeProject.Controller
//{

//    [Authorize(Policy = "AdminOnly")]
//    [Route("api/dev")]
//    [ApiController]
//    public class DevelopmentPhaseController : ControllerBase
//    {
//        private readonly IDevelopmentPhaseServices _developmentPhaseServices;

//        public DevelopmentPhaseController(IDevelopmentPhaseServices developmentPhaseServices)
//        {
//            _developmentPhaseServices = developmentPhaseServices;
//        }

//        // ✅ POST add DesignPhase for create or update
//        [HttpPost("add-or-update")]
//        public async Task<IActionResult> AddOrUpdateDevelopment([FromBody] DevelopmentPhaseDTO development)
//        {
//            if (development == null)
//                return BadRequest("DevelopmentPhaseDTO is null.");

//            try
//            {
//                await _developmentPhaseServices.AddDevelopmentPhaseAsync(development);
//                return Ok(new { message = "Dvelopment task saved successfully." });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { error = ex.Message });
//            }
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetDevelopmentPhase(int id)
//        {
//            if (id <= 0)
//                return BadRequest("Invalid ID.");

//            try
//            {
//                var result = await _developmentPhaseServices.GetDevelopmentPhaseAsync(id);
//                if (result == null)
//                    return NotFound(new { message = $"DevelopmentPhase with ID {id} not found." });

//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { error = ex.Message });
//            }
//        }
//    }
//}
