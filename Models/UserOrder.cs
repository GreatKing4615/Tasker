using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasker.Models
{
    public class UserOrder
    {
        [Key]
        public int UserId { get; set; }
        public User User { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Work Order { get; set; }


    }
}
