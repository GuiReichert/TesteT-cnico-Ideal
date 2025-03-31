namespace TesteTécnicoIdeal.API.Exceptions
{
    public class CustomExceptions : CustomExceptionsBase
    {
        public IList<string> ErrorMessages { get; set; }
        public CustomExceptions(IList<string> errors)
        {
            ErrorMessages = errors;
        }

        public CustomExceptions(string error)
        {
            ErrorMessages = new List<string> { error };
        }


    }
}
