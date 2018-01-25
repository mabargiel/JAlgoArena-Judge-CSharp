using System;
using Judge.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Judge.API.Controllers
{
    [Route("problems")]
    public class ProblemsController : Controller 
    {
        [HttpGet]
        public IActionResult Get()
        {
            //TODO
            return Ok(new JudgeResult());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            //TODO
            return Ok(new JudgeResult());
        }

        [HttpPost("{id}/submit")]
        public IActionResult Submit(string id) 
        {
            //TODO
            return Ok(new JudgeResult());
        }
    }
}