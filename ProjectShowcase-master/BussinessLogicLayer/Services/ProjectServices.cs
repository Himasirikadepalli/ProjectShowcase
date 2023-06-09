using BussinessLogicLayer.DTOs;
using BussinessLogicLayer.Exceptions;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;

namespace BussinessLogicLayer.Services
{
    public class ProjectServices : IProjectServices
    {
        private readonly IProjectRepositories _repositories;
        public ProjectServices(IProjectRepositories repositories)
        {
            _repositories = repositories;
        }

        public async Task<bool> ChangeStatus(string projectId, string status)
        {
            if (status != "Pending" || status != "Approved" || status != "Rejected")
                throw new statusMismatch("Entered status does not follow standard");

            return await _repositories.ChangeStatus(projectId, status);
        }

        public async Task<ProjectDTO> CreateProject(ProjectDTO? project)
        {
            char[] delimiters = new char[] { ',', ' ', '\r', '\n', '\t' };
            int WordCnt = project.Description.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
            if (WordCnt > 100)
                throw new BadRequestException("Description exceed 100 words");
            return await _repositories.CreateProject((Project)project);
        }

        public async Task<ProjectDTO> GetProjectById(string id)
        {
            return await _repositories.GetProjectById(id);
        }

        public async Task<List<ProjectDTO>> SearchProject(string keyword)
        {
            List<Project> list = await _repositories.SearchProject(keyword);
            return list.Select(project => (ProjectDTO)project).ToList();
        }
    }
}
