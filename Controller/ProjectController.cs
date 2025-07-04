﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeProject.Models.DTO;
using OfficeProject.Servicess;

namespace OfficeProject.Controllers
{
    //[Authorize(Policy = "AdminOnly")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService _projectsService;

        public ProjectsController(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectsDTO>> GetProjectById(int id)
        {
            var project = await _projectsService.GetProjectByIdAsync(id);

            if (project == null)
                return NotFound();

            return Ok(project);
        }



        [HttpGet("user")]
        //[Authorize]
        public async Task<ActionResult<List<ProjectsDTO>>> GetProjectPerUser()
        {
            try
            {
                var project = await _projectsService.GetProjectPerUserAsync();
                return Ok(project);
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


        //[HttpGet("team")]
        //public async Task<ActionResult<ProjectsDTO>> GetTeamWorksAsync()
        //{
        //    try
        //    {
        //        var project = await _projectsService.GetTeamWorksAsync();
        //        return Ok(project);
        //    }
        //    catch (UnauthorizedAccessException ex)
        //    {
        //        return Unauthorized(new { message = ex.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "An error occurred while retrieving team work", error = ex.Message });
        //    }

        //}

        //[HttpGet("user/work")]
        ////[Authorize]
        //public async Task<ActionResult<List<ProjectsDTO>>> GetWorkingRecordPerUserAsync()
        //{
        //    try
        //    {
        //        var project = await _projectsService.GetWorkingRecordPerUserAsync();
        //        return Ok(project);
        //    }
        //    catch (UnauthorizedAccessException ex)
        //    {
        //        return Unauthorized(new { message = ex.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "An error occurred while retrieving clients", error = ex.Message });
        //    }
        //}

        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<List<ProjectsDTO>>> GetAllProjectAsync()
        {
            try
            {
                var project = await _projectsService.GetAllProjectAsync();
                return Ok(project);
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
        

        [HttpPost]
        public async Task<IActionResult> UpdateProjectAsync([FromBody] ProjectsDTO projectDto)
        {
            if (projectDto == null)
                return BadRequest("Project data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _projectsService.UpdateProjectAsync(projectDto);
                return Ok(new { message = "Project saved successfully." });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpPost("add")]
        //[Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> AddProjectAsync([FromBody] ProjectsDTO projectDto)
        {
            if (projectDto == null)
            {
                return BadRequest("Project data is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _projectsService.AddProjectsAsync(projectDto);
                return Ok(new { message = "Project added successfully." });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpPost("save-or-update")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> SaveOrUpdateProjects([FromBody] ProjectsDTO projectsDTO)
        {
            if (projectsDTO == null )
                return BadRequest("Project data is missing.");

            try
            {
                await _projectsService.SaveOrUpdateProjectsAsync(projectsDTO);
                return Ok(new { message = "Projects saved or updated successfully." });
            }
            catch (Exception ex)
            {
                // Optionally log error here
                return StatusCode(500, new { message = "An error occurred while saving or updating projects.", error = ex.Message });
            }
        }
    }
}