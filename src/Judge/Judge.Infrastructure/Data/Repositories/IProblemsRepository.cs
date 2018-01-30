using System.Collections.Generic;
using Judge.Infrastructure.ProblemsSchema;

namespace Judge.Infrastructure.Data.Repositories
{
    public interface IProblemsRepository
    {
        List<Problem> GetAll();
        Problem FindById(string id);
    }
}
