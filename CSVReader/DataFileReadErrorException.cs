using System.Runtime.Serialization;

namespace CSVReader
{
    /// <summary>
    /// DataFileReadError Exception
    /// </summary>
    [Serializable]
    public class DataFileReadErrorException : Exception
    {
        public DataFileReadErrorException()
        {
        }

        public DataFileReadErrorException(string? message) : base(message)
        {
        }

        public DataFileReadErrorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DataFileReadErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}