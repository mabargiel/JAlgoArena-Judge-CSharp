using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Judge.API;
using Judge.API.Controllers;
using Judge.API.Models;
using Judge.Infrastructure.Data.Repositories;
using Judge.Infrastructure.ProblemsSchema;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Judge.UnitTests
{
    public class ProblemsControllerTests
    {
        public ProblemsControllerTests()
        {
            var mapper = new MapperConfiguration(cfg =>
                    cfg.AddProfile(new AutoMapperProfileConfiguration()))
                .CreateMapper();
            var problemsRepository = MockProblemsRepository();
            _controller = new ProblemsController(mapper, problemsRepository);
        }

        private readonly ProblemsController _controller;
        private static readonly string _someProblemId = "someId";

        private static IProblemsRepository MockProblemsRepository()
        {
            var problemsRepository = Substitute.For<IProblemsRepository>();
            problemsRepository.GetAllAsync().Returns(new List<Problem>());
            problemsRepository.FindByIdAsync(_someProblemId).Returns(new Problem());
            return problemsRepository;
        }

        [Fact]
        public async Task GetByIdReturnsOneProblem()
        {
            var okResult = Assert.IsType<OkObjectResult>(await _controller.GetById(_someProblemId));
            okResult.StatusCode.ShouldBe(200);
            var valueResult = Assert.IsType<ProblemDto>(okResult.Value);
        }

        [Fact]
        public async Task GetReturnsAllProblems()
        {
            var okResult = Assert.IsType<OkObjectResult>(await _controller.Get());
            okResult.StatusCode.ShouldBe(200);
            var valueResult = Assert.IsType<ProblemDto[]>(okResult.Value);
        }

        [Fact]
        public async Task SubmitExecutesJudgeAndReturnsJudgeResult()
        {
            var okResult =
                Assert.IsType<OkObjectResult>(await _controller.Submit(_someProblemId, new JudgeRequestDto()));
            okResult.StatusCode.ShouldBe(200);
            var valueResult = Assert.IsType<JudgeResultDto>(okResult.Value);
        }
    }
}