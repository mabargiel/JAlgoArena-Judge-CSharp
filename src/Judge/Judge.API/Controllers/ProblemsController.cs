using System.Threading.Tasks;
using AutoMapper;
using Judge.API.Models;
using Judge.Infrastructure.Data.Repositories;
using Judge.Infrastructure.ProblemsSchema;
using Microsoft.AspNetCore.Mvc;

namespace Judge.API.Controllers
{
    /// <inheritdoc />
    /// <summary>
    ///     Problems resource web api
    /// </summary>
    [Route("problems")]
    public class ProblemsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProblemsRepository _problemsRepository;

        public ProblemsController(IMapper mapper, IProblemsRepository problemsRepository)
        {
            _mapper = mapper;
            _problemsRepository = problemsRepository;
        }

        /// <summary>
        ///     Returns all problems
        /// </summary>
        /// <returns>A <see cref="Problem" /> array</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var problems = await _problemsRepository.GetAllAsync();
            var result = _mapper.Map<ProblemDto[]>(problems);

            return Ok(result);
        }

        /// <summary>
        ///     Returns a problem specified by Id param
        /// </summary>
        /// <param name="id">Id of a problem</param>
        /// <returns>A <see cref="JudgeResultDto" /> by specified Id</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("The id parameter cannot be empty");

            var problem = await _problemsRepository.FindByIdAsync(id);
            if (problem == null)
                return NotFound($"A problem with id {id} does not exist");

            var result = _mapper.Map<ProblemDto>(problem);

            return Ok(result);
        }

        /// <summary>
        ///     Compiles and runs a solution for a problem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/submit")]
        public async Task<IActionResult> Submit(string id, [FromBody] JudgeRequestDto judgeRequest)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("The id parameter cannot be empty");

            if (!ModelState.IsValid)
                return BadRequest("Invalid request body");

            var problem = await _problemsRepository.FindByIdAsync(id);
            if (problem == null)
                return NotFound($"A problem with id {id} does not exist");

            // TODO Execute judge

            return Ok(new JudgeResultDto());
        }
    }
}