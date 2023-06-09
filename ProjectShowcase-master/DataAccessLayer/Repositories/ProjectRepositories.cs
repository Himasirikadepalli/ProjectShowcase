using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ProjectRepositories : IProjectRepositories
    {
        private readonly AppDbContext _context;

        public ProjectRepositories(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<bool> ChangeStatus(string projectId, string status)
        {
            Project project = await _context.Projects.SingleOrDefaultAsync(p => p.ProjectId.Equals(projectId));
            if (project == null)
                return false;
            project.Status = status;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Project> CreateProject(Project? project)
        {
            _context.Projects.Add(project);
            if (await _context.SaveChangesAsync() == 0)
                return null;

            return project;
        }

        public async Task<Project> GetProjectById(string id)
        {
            return await _context.Projects.SingleOrDefaultAsync(x => x.ProjectId.Equals(id));
        }

        public async Task<List<Project>> SearchProject(string keyword)
        {
            return await _context.Projects.Where(p => p.Keywords.Contains(keyword)).ToListAsync();
        }
    }
}
