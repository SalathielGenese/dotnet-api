using System;

namespace TestProgrammationConformit.Infrastructures
{
    public class Env
    {
        public Env()
        {
            PostgresConnectionString =
                Environment.GetEnvironmentVariable("ConnectionStrings__ConformitDb") ?? string.Empty;
        }
        public string PostgresConnectionString { get; }
    }
}