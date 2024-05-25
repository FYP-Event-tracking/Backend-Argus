using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService_ArgusBackend.Models
{
    [Table("userlogin", Schema ="dbo")]
    public class UserLogin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("userId")]
        public string UserId { get; set; }

        [Column("userType")]
        public string UserType { get; set; }

        [Column("userPassword")]
        public string UserPassword { get; set; }
    }
}