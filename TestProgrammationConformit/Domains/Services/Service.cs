using System.Collections.Generic;
using TestProgrammationConformit.Domains.Models;

#nullable enable
namespace TestProgrammationConformit.Domains.Services
{
    public interface IService<TModel, in TId> where TModel : Model<TId>
    {
        TModel? Persist(TModel entity);

        TModel? Find(TId id);
        IEnumerable<TModel> Find(int page, int size);

        bool Delete(TId id);
    }
}
