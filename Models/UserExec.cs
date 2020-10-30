using System.ComponentModel.DataAnnotations.Schema;

namespace Tasker.Models
{
    public class UserExec
    {

        public int UserId { get; set; }
        public User User { get; set; }

        public int ExecuteId { get; set; }
        [ForeignKey("ExecuteId")]
        public Work Execute { get; set; }
    }
}
