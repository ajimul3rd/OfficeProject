//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using OfficeProject.Models.DTO;
//using OfficeProject.Servicess;

//namespace OfficeProject.Controller
//{
//    [Authorize(Policy = "AdminOnly")]
//    [Route("api/design")]
//    [ApiController]
//    public class DesignPhaseController : ControllerBase
//    {
//        private readonly IDesignPhaseServices _designPhaseServices;

//        public DesignPhaseController(IDesignPhaseServices designPhaseServices)
//        {
//            _designPhaseServices = designPhaseServices;
//        }

//        // ✅ POST add DesignPhase for create or update
//        [HttpPost("add-or-update")]
//        public async Task<IActionResult> AddOrUpdateDesign([FromBody] DesignPhaseDTO designPhaseDTO)
//        {
//            if (designPhaseDTO == null)
//                return BadRequest("DesignPhaseDTO is null.");

//            try
//            {
//                await _designPhaseServices.AddDesignAsync(designPhaseDTO);
//                return Ok(new { message = "Design task saved successfully." });
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
//                var result = await _designPhaseServices.GetDesignPhaseAsync(id);
//                if (result == null)
//                    return NotFound(new { message = $"DesignPhase with ID {id} not found." });

//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { error = ex.Message });
//            }
//        }
//    }
//}
