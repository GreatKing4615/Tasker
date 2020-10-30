using System.ComponentModel.DataAnnotations.Schema;

namespace Tasker.Models
{
    public class UserOrder
    {
        //
        public int UserId { get; set; }
        public User User { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Work Order { get; set; }


    }
}
