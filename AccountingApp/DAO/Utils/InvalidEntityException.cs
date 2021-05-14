using System;

namespace DAO.Utils
{
    [Serializable]
    public class InvalidEntityException : Exception
    {
        public InvalidEntityException(string message) : base(message)
        {
        }
    }
}