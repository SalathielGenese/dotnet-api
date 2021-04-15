namespace TestProgrammationConformit.Infrastructures.Http
{
    public class Response<T>
    {
        public Response(T content)
        {
            Content = content;
        }

        public T Content { get; }
    }
}
