using System;
using Judge.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Judge.API.Controllers
{
    /// <summary>
    /// Problems resource web api
    /// </summary>
    [Route ("problems")]
    public class ProblemsController : Controller
    {
        /// <summary>
        /// Returns all problems
        /// </summary>
        /// <returns>A <see cref="Problem"> array</returns>
        [HttpGet]
        public IActionResult Get ()
        {
            //TODO
            return Ok (new ProblemDto[] { });
        }

        /// <summary>
        /// Returns a problem specified by Id param
        /// </summary>
        /// <param name="id">Id of a problem</param>
        /// <returns>A <see cref="JudgeResultDto"> by specified Id</returns>
        [HttpGet ("{id}")]
        public IActionResult GetById (string id)
        {
            //TODO
            return Ok (new ProblemDto ());
        }

        /// <summary>
        /// Compiles and runs a solution for a problem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost ("{id}/submit")]
        public IActionResult Submit (string id)
        {
            //TODO
            return Ok (new JudgeResultDto ());
        }
    }
}