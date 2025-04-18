using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace RestWithASPNet.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("user_name")]
        public string UserName { get; set; }

        [Column("full_name")]
        public string FullName { get; set; }

        [Column("password")]
        public string Password{ get; set; }

        [Column("refresh_token")]
        [AllowNull]
        public string RefreshToken { get; set; }

        [Column("refresh_token_expiry_time")]
        [AllowNull]
        public DateTime RefreshTokeneExpireTime { get; set; }

    }
}
