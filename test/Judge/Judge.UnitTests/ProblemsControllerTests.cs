using System;
using Xunit;
using Judge.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Judge.API.Models;
using Shouldly;

namespace Judge.UnitTests
{
    public class ProblemsControllerTests
    {
        private readonly ProblemsController _controller;

        public ProblemsControllerTests()
        {
            _controller = new ProblemsController();
        }

        //TODO This test requires a dump / mock data for both results
        [Fact]
        public void When_I_request_problems_then_a_JudgeResult_with_a_list_of_all_problems_is_returned()
        {
            var okResult = Assert.IsType<OkObjectResult>(_controller.Get());
            okResult.StatusCode.ShouldBe(200);
            var valueResult = Assert.IsType<JudgeResult[]>(okResult.Value);

            valueResult.Length.ShouldBe(2);

            valueResult[0].ConsumedMemory.ShouldBe(0);
            valueResult[0].ElapsedTime.ShouldBe(66);
            valueResult[0].ErrorMessage.ShouldBe(null);
            valueResult[0].StatusCode.ShouldBe(Status.ACCEPTED);
            valueResult[0].TestcaseResults.ShouldBe(new [] {true, false, false, true, true});

            valueResult[1].ConsumedMemory.ShouldBe(0);
            valueResult[1].ElapsedTime.ShouldBe(66);
            valueResult[1].ErrorMessage.ShouldBe(null);
            valueResult[1].StatusCode.ShouldBe(Status.ACCEPTED);
            valueResult[1].TestcaseResults.ShouldBe(new [] {true, false, false, true, true});
        }
    }
}
