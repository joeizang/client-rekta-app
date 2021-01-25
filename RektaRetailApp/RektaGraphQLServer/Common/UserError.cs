namespace RektaGraphQLServer.Common
{
    public class UserError
    {
        public UserError(string message, string code)
        {
            Message = message;
            Code = code;
        }

        public string Code { get; }

        public string Message { get; }
    }
}