namespace MovieRatingAPI.Exceptions
{
    public class DuplicateTitleException : Exception
    {
        public DuplicateTitleException(string message) : base(message)
        {
        }
    }
}
