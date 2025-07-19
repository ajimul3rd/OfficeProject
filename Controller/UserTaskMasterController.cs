using Microsoft.AspNetCore.Mvc;
using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;
using OfficeProject.Servicess;

namespace OfficeProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTaskMasterController : ControllerBase
    {
        private readonly IUserTaskMasterService UserTaskMasterService;
        private readonly IDataSerializer? DataSerializer;

        public UserTaskMasterController(IUserTaskMasterService UserTaskMasterService, IDataSerializer? dataSerializer)
        {
            this.UserTaskMasterService = UserTaskMasterService;
            DataSerializer = dataSerializer;

        }



        [HttpGet]
        public async Task<ActionResult<List<UserTaskMaster>>> GetUserTasksMasterAsync()
        {
            try
            {
                var tasks = await UserTaskMasterService.GetUserTasksMasterAsync();
                return Ok(tasks);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("User is not authenticated.");
            }
        }
                [HttpPost]
        public async Task<ActionResult> AddOrUpdateUserTasksMasterAsync([FromBody] UserTaskMaster task)
        {

            DataSerializer.Serializer(task, "UserTaskMaster");
            if (task == null)
            {
                return BadRequest("Task cannot be null.");
            }

            try
            {
                await UserTaskMasterService.AddOrUpdateUserTasksMasterAsync(task);
                return Ok("Task added or updated successfully.");
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("User is not authenticated.");
            }
        }
        [HttpDelete("{taskId}")]
        public async Task<ActionResult> DeleteUserTaskMasterAsync(int taskId)
        {
            try
            {
                await UserTaskMasterService.DeleteTask(taskId);
                return Ok($"Task with ID {taskId} deleted successfully.");
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("User is not authenticated.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Task with ID {taskId} not found.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting task: {ex.Message}");
            }
        }

    }
}