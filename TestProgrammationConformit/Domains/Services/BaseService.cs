using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TestProgrammationConformit.Domains.Models;
using TestProgrammationConformit.Infrastructures;

#nullable enable
namespace TestProgrammationConformit.Domains.Services
{
    public class BaseService<TModel, TId> : IService<TModel, TId> where TModel : Model<TId>
    {
        public BaseService(ConformitContext conformitContext, DbSet<TModel> dbSet, TId identity)
        {
            PropertyInfos = typeof(TModel).UnderlyingSystemType.GetProperties().Where(info =>
                !info.Name.Equals("Id") && info.CanRead && info.CanWrite);
            ConformitContext = conformitContext;
            Identity = identity;
            DbSet = dbSet;
        }

        protected readonly IEnumerable<PropertyInfo> PropertyInfos;
        protected readonly ConformitContext ConformitContext;
        protected readonly DbSet<TModel> DbSet;
        protected readonly TId Identity;

        public TModel? Persist(TModel entity)
        {
            TModel? model;

            if (Identity!.Equals(entity.Id))
            {
                model = DbSet.Add(entity).Entity;
            }
            else
            {
                model = DbSet.Find(entity.Id);

                if (null != model)
                {
                    foreach (var propertyInfo in PropertyInfos)
                    {
                        var value = propertyInfo.GetValue(entity, null);
                        if (null != value)
                        {
                            propertyInfo.SetValue(model, value, null);
                        }
                    }

                    model = DbSet.Update(model).Entity;
                }
            }

            ConformitContext.SaveChanges();

            return model;
        }

        public TModel? Find(TId id)
        {
            return DbSet.Find(id);
        }

        public IEnumerable<TModel> Find(int page, int size)
        {
            return DbSet.Skip(size * (page - 1)).Take(size).ToList();
        }

        public bool Delete(TId id)
        {
            var model = DbSet.Find(id);

            if (null == model)
            {
                return false;
            }

            DbSet.Remove(model);
            ConformitContext.SaveChanges();

            return true;
        }
    }
}
