using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using OfficeProject.Data;
using OfficeProject.Models.DTO;

namespace OfficeProject.Servicess
{
    public class GlobalDataService : IGlobalDataService
    {
        public List<ProjectsDTO> Projects { get; set; } = new();        
        public ProjectsDTO Data { get; set; } = new();
        public bool HasData => Projects != null && Projects.Count > 0;
        public void Clear()
        {
            Projects.Clear();
            Data = new ProjectsDTO();
        }
    }
}
