using DataAccessLayer.Models;
using Moq;
using NUnit.Util;
using BussinessLogicLayer.Services;
using DataAccessLayer.Repositories;
using BussinessLogicLayer.Exceptions;
using BussinessLogicLayer.DTOs;

namespace TestProject1
{
    [TestFixture]
    public class ProjectTest
    {
        private Mock<IProjectRepositories> _mockProjectRepository;
        private ProjectServices _projectService;
        private List<Project> _project;

        [SetUp]
        public void Setup()
        {
            _project = new List<Project>();

            _mockProjectRepository = new Mock<IProjectRepositories>();
            //_mockProjectRepository.Setup(r => r.GetAllEpics()).ReturnsAsync (_project);
            _mockProjectRepository.Setup(r => r.CreateProject(It.IsAny<Project>())).ReturnsAsync((Project e) => {
                _project.Add(e);
                return e;
            });

            _projectService = new ProjectServices(_mockProjectRepository.Object);
        }

        [Test]
        public void StatusMisMatch()
        {
            ProjectDTO projectDTO = new ProjectDTO
            {
                ProjectId = "2121",
                Title = "titleOfProject",
                Description = "descriptionsOfProject",
                Keywords = "superProject",
                AuthorId = "authId",
                Status = "InProgress"
            };

            var ex = Assert.ThrowsAsync<statusMismatch>(() => _projectService.CreateProject(projectDTO));
            Assert.That(ex.Message, Is.EqualTo("Entered status does not follow standard"));
        }

        public void ProjectDescription100wordsExceed()
        {
            ProjectDTO projectDTO = new ProjectDTO
            {
                ProjectId = "2121",
                Title = "titleOfProject",
                Description = "l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l " +
                "l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l " +
                "l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l ",
                Keywords = "superProject",
                AuthorId = "authId",
                Status = "Pending"
            };

            var ex = Assert.ThrowsAsync<BadRequestException>(() => _projectService.CreateProject(projectDTO));
            Assert.That(ex.Message, Is.EqualTo("Description exceed 100 words"));
        }
    }
}