using System;
using System.Collections.Generic;
using Tasker.Models;

namespace TaskManager.Models
{
    /// <summary>
    /// Модель, описывающая механизм пагинации: текущая страница, кол-во объектов на экране, всего объектов, всего страниц
    /// </summary>
    public class PageInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }


    public class UsersModel
    {
        public IEnumerable<User> Users { get; set; }
        public PageInfo PageInfo { get; set; }
    }

    public class WorksModel
    {
        public IEnumerable<Work> Works { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
