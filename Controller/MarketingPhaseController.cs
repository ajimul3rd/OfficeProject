//using System.Text.Json;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using OfficeProject.Models.DTO;
//using OfficeProject.Servicess;

//namespace OfficeProject.Controller
//{
//    [Authorize(Policy = "AdminOnly")]
//    [Route("api/marketing")]
//    [ApiController]
//    public class MarketingPhaseController : ControllerBase
//    {
//        private readonly IMarketingPhaseService _marketingPhaseService;


//        public MarketingPhaseController(IMarketingPhaseService marketingPhaseService)
//        {
//            _marketingPhaseService = marketingPhaseService;
//        }

//        // ✅ POST add MarketingPhase for create or update
//        [HttpPost("add-or-update")]
//        public async Task<IActionResult> AddMarketingPhaseAsync([FromBody] MarketingPhaseDTO marketingPhaseDTO)
//        {
//            if (marketingPhaseDTO == null)
//                return BadRequest("MaintenancePhaseDTO is null.");

//            try
//            {
//                await _marketingPhaseService.AddMarketingPhaseAsync(marketingPhaseDTO);

//                return Ok(new { message = "Maintenance task saved successfully." });
              

//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new
//                {
//                    error = "An error occurred while saving the marketing phase",
//                    details = ex.Message,
//                    innerException = ex.InnerException?.Message
//                });
//            }
//        }

//    }
//}


////// Log the DTO as JSON
////var jsonOptions = new JsonSerializerOptions
////{
////    WriteIndented = true,  // For pretty-printing
////    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
////};

////string json = System.Text.Json.JsonSerializer.Serialize(marketingPhaseDTO, jsonOptions);
////Console.WriteLine($"Try To add MarketingPhase JSON:\n{json}");