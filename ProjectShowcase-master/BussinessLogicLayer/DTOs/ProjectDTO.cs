using DataAccessLayer.Models;

namespace BussinessLogicLayer.DTOs
{
    public class ProjectDTO
    {
        public string ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string AuthorId { get; set; }
        public string Status { get; set; } = "Pending";

        public static implicit operator ProjectDTO(Project project)
        {
            return new ProjectDTO
            {
                ProjectId = project.ProjectId,
                Title = project.Title,
                Description = project.Description,
                Keywords = project.Keywords,
                AuthorId = project.AuthorId,
                Status = project.Status
            };
        }

        public static explicit operator Project(ProjectDTO dto)
        {
            return new Project
            {
                ProjectId = dto.ProjectId,
                Title = dto.Title,
                Description = dto.Description,
                Keywords = dto.Keywords,
                AuthorId = dto.AuthorId,
                Status = dto.Status
            };
        }
    }

}
