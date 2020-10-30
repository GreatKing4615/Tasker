using System;
using Tasker.Models;

namespace TaskManager.Builder
{
    abstract class UserBuilderBase : Base
    {
        protected User User;
        protected UserBuilderBase()
        {
            User = new User();
        }

        public User GetUser()
        {
            return User;
        }


        public abstract void BuildFirstName(string firstName = "Пользователь");
        public abstract void BuildLastName(string lastName = "Иванов");
        public abstract void BuildDateOfCreate(DateTime date);
        public abstract void BuildDateOfLastChange(DateTime date);
        public abstract void BuildStatus();



    }
}
