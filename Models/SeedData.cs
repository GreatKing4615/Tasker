using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Tasker;
using Tasker.Models;
using TaskManager.Builder;

namespace TaskManager.Models
{
    public class SeedData
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {

            using (var _context = new TaskerDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<TaskerDbContext>>()))
            {
                if (_context.Users.Any() && _context.Users.Any())
                {
                    _context.Database.EnsureDeleted();
                    _context.Database.EnsureCreated();
                }
                List<User> users = new List<User>();
                List<Work> works = new List<Work>();

                for (int i = 0; i < 5; i++)
                    users.Add(GenerateUser(i));
                for (int i = 0; i < 25; i++)
                    works.Add(GenerateTask(i));

                _context.Users.AddRange(users);
                _context.Works.AddRange(works);
                _context.SaveChanges();


                var rnd = new Random();
                foreach (var w in works)
                {
                    int userIndex = rnd.Next(0, 5);
                    int orderIndex = rnd.Next(0, 25);
                    int execIndex = rnd.Next(0, 25);
                    users[userIndex].UserOrders.Add(new UserOrder { UserId = users[userIndex].Id, OrderId = works[orderIndex].Id });
                    users[userIndex].UserExecutes.Add(new UserExec { UserId = users[userIndex].Id, ExecuteId = works[execIndex].Id });
                    _context.SaveChanges();
                }



            }
        }





        /// <summary>
        /// Генерация пользователя с реализацией паттерна "Builder"
        /// </summary>
        /// <param name = "i" > Номер пользователя</param>
        /// <returns>Сгенерированный пользователь</returns>
        private static User GenerateUser(int i)
        {

            var user = new UserBuilder();
            //user.BuildId();            
            user.BuildFirstName("Пользователь" + i.ToString());
            user.BuildLastName("Lastname" + i.ToString());
            user.BuildDateOfCreate(user.RndData());
            user.BuildDateOfLastChange(user.RndData());

            user.BuildStatus();
            return user.GetUser();
        }




        /// <summary>
        /// Генерация задачи с реализацией паттерна "Builder"
        /// </summary>
        /// <param name="i">Номер задачи</param>
        /// <returns>Сгенерированная задача</returns>
        private static Work GenerateTask(int i)
        {
            var task = new WorkBuilder();
            //task.BuilderId();
            task.BuilderName("Задача №" + i.ToString());
            task.BuilderDesqription("Описание задания №" + i.ToString());
            task.BuilderDataCreate(task.RndData());
            task.BuilderDataOfLastChange(task.RndData());
            task.BuilderStatus();
            return task.GetWork();
        }
    }

}
