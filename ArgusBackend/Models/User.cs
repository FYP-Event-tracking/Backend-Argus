using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArgusBackend.Models
{
    [Table("userdata", Schema ="dbo")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("userId")]
        public string UserId { get; set; }

        [Column("userName")]
        public string UserName { get; set; }

        [Column("userType")]
        public string UserType { get; set; }

        [Column("userAddress")]
        public string UserAddress { get; set; }
        
        [Column("userTelephone")]
        public string UserTelephone { get; set; }
    }
}