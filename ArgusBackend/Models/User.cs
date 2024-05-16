using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArgusBackend.Models
{
    [Table("user", Schema = "dbo")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public string UserId { get; set; }

        [Column("user_name")]
        public string UserName { get; set; }

        [Column("user_type")]
        public string UserType { get; set; }

        [Column("user_address")]
        public string UserAddress { get; set; }
        
        [Column("user_telephone")]
        public string UserTelephone { get; set; }
    }
}