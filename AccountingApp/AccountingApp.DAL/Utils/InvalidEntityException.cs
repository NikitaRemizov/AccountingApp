using System;

namespace AccountingApp.DAL.Utils
{
    [Serializable]
    public class InvalidEntityException : Exception
    {
        public InvalidEntityException(string message, Exception? innerException) 
            : base(message, innerException)
        {
        }
    }
}