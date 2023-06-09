using BussinessLogicLayer.DTOs;

namespace BussinessLogicLayer.Services
{
    public interface IProjectServices
    {
        public Task<ProjectDTO> CreateProject(ProjectDTO project);

        public Task<ProjectDTO> GetProjectById(string id);

        public Task<bool> ChangeStatus(string projectId, string status);

        public Task<List<ProjectDTO>> SearchProject(string keyword);
    }
}
