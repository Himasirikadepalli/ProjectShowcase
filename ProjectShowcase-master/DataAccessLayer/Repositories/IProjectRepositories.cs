using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IProjectRepositories
    {
        public Task<Project> CreateProject(Project project);

        public Task<Project> GetProjectById(string id);

        public Task<bool> ChangeStatus(string projectId, string status);

        public Task<List<Project>> SearchProject(string keyword);
    }
}
