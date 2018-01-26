using System;
using Xunit;
using Judge.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Judge.API.Models;
using Shouldly;

namespace Judge.UnitTests
{
    //TODO REFACTOR
    public class ProblemsControllerTests
    {
        private readonly ProblemsController _controller;

        public ProblemsControllerTests()
        {
            _controller = new ProblemsController();
        }

        [Fact]
        public void GetReturnsAllProblems()
        {
            var okResult = Assert.IsType<OkObjectResult>(_controller.Get());
            okResult.StatusCode.ShouldBe(200);
            var valueResult = Assert.IsType<ProblemDto[]>(okResult.Value);
        }

        [Fact]
        public void GivenIdSubmitReturnsJudgeResult() 
        {
            var okResult = Assert.IsType<OkObjectResult>(_controller.Submit("test_problem"));
            okResult.StatusCode.ShouldBe(200);
            var valueResult = Assert.IsType<JudgeResultDto>(okResult.Value);
        }
    }
}
