using AutoMapper;
using Judge.API.Models;
using Judge.Infrastructure.ProblemsSchema;

namespace Judge.API
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
            : this("Judge.API")
        {
        }

        protected AutoMapperProfileConfiguration(string profileName)
            : base(profileName)
        {
            CreateMap<Problem, ProblemDto>()
                .ForMember(dto => dto.TestCases,
                    expression => expression.MapFrom(problem => problem.TestCases.ToArray()));
        }
    }
}