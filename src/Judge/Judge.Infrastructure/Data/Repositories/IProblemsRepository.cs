using System.Collections.Generic;
using System.Threading.Tasks;
using Judge.Infrastructure.ProblemsSchema;

namespace Judge.Infrastructure.Data.Repositories
{
    public interface IProblemsRepository
    {
        Task<List<Problem>> GetAllAsync();
        Task<Problem> FindByIdAsync(string id);
    }
}