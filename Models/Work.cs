using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tasker.Models
{
    //[JsonConverter(typeof(StringEnumConverter))]
    public enum StatusTask
    {
        Not_started,
        In_progress,
        Completed,
        Canceled,
        Rejected

    }
    public class Work
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfCreate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfLastChange { get; set; }
        //[JsonConverter(typeof(StringEnumConverter))]
        public StatusTask Status { get; set; }

        [NotMapped]
        public List<UserOrder> OrderWorks { get; set; }
        public List<UserExec> ExecWorks { get; set; }

        public Work()
        {
            OrderWorks = new List<UserOrder>();
            ExecWorks = new List<UserExec>();
        }


    }
}
