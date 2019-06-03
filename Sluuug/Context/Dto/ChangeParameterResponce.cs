namespace Slug.Context.Dto
{
    public class ChangeParameterResponce
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public static class Errors
        {
            public const string INVALID_CHARACTERS = "Parameter contains invalid characters.";
            public const string ACCESS_DENIED = "You have not access to change current parameter.";
        }
    }
}