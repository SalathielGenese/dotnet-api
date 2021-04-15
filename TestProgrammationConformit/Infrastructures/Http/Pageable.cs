using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TestProgrammationConformit.Infrastructures.Http
{
    [ModelBinder(BinderType = typeof(Binder))]
    public class Pageable
    {
        public int Page { get; set; }

        public int Size { get; set; }

        public class Binder : IModelBinder
        {
            public Binder(Env env)
            {
                Env = env;
            }

            private Env Env { get; }

            public Task BindModelAsync(ModelBindingContext bindingContext)
            {
                if (null == bindingContext)
                {
                    throw new ArgumentNullException(nameof(bindingContext));
                }

                var pageable = new Pageable();

                bindingContext.HttpContext.Request.Query.TryGetValue("page", out var pageQuery);
                bindingContext.HttpContext.Request.Query.TryGetValue("size", out var sizeQuery);

                int.TryParse(pageQuery, out int page);
                int.TryParse(sizeQuery, out int size);

                pageable.Page = Math.Max(Env.Pageable.Page, page);
                pageable.Size = Math.Max(Env.Pageable.Size, size);

                bindingContext.Result = ModelBindingResult.Success(pageable);

                return Task.CompletedTask;
            }
        }
    }
}
