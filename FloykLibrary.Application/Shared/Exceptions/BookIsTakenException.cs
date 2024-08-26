namespace FloykLibrary.Application.Shared.Exceptions
{
    public class BookIsTakenException : Exception
    {
        public BookIsTakenException() 
                : base("Book is taken") { }
    }
}
