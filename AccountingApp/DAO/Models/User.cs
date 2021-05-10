using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAO.Models
{
    [Table("users")]
    public class User : Model
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public byte[] Password { get; set; }

        public static User Empty { get; } = new User
        {
            Id = Guid.Empty,
            Email = string.Empty,
            Password = Array.Empty<byte>()
        };
    }
}
