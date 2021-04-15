using System.Collections.Generic;
using System.Linq;

namespace TestProgrammationConformit.Infrastructures.Http
{
    public class PagedResponse<T> : Response<IEnumerable<T>>
    {
        public PagedResponse(IEnumerable<T> content, int page, int size, int total) : base(content)
        {
            Count = content.Count();
            Total = total;
            Page = page;
            Size = size;
        }

        public int Page { get; }
        public int Size { get; }
        public int Total { get; }
        public int Count { get; }
    }
}
