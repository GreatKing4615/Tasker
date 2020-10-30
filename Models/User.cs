using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tasker.Models
{
    //[JsonConverter(typeof(StringEnumConverter))]
    public enum StatusUser
    {
        Active,
        Disabled,
        Blocked
    }

    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfCreate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfLastChange { get; set; }
        //[JsonConverter(typeof(StringEnumConverter))]
        public StatusUser Status { get; set; }
        //[NotMapped]
        public List<UserOrder> UserOrders { get; set; }
        public List<UserExec> UserExecutes { get; set; }
        public User()
        {
            UserOrders = new List<UserOrder>();
            UserExecutes = new List<UserExec>();

        }

    }
}
