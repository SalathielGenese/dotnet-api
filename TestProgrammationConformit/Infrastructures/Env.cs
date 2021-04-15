using System;
using TestProgrammationConformit.Infrastructures.Http;

namespace TestProgrammationConformit.Infrastructures
{
    public class Env
    {
        public Env()
        {
            int page, size;
            var pageString = Environment.GetEnvironmentVariable("ConnectionStrings__page") ?? string.Empty;
            var sizeString = Environment.GetEnvironmentVariable("ConnectionStrings__size") ?? string.Empty;

            int.TryParse(pageString, out page);
            int.TryParse(sizeString, out size);
            page = Math.Max(1, page);
            size = Math.Max(1, size);
            Pageable = new Pageable {Page = page, Size = size};

            PostgresConnectionString =
                Environment.GetEnvironmentVariable("ConnectionStrings__ConformitDb") ?? string.Empty;
        }

        public Pageable Pageable { get; }
        public string PostgresConnectionString { get; }
    }
}
