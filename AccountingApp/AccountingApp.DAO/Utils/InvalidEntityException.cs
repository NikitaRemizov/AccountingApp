using System;

namespace AccountingApp.DAO.Utils
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