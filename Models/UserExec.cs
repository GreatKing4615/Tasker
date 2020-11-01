using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasker.Models
{
    public class UserExec
    {
        [Key]
        public int UserId { get; set; }
        public User User { get; set; }

        public int ExecuteId { get; set; }
        [ForeignKey("ExecuteId")]
        public Work Execute { get; set; }
    }
}
