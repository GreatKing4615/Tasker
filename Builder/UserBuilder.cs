using System;

namespace TaskManager.Builder
{
    class UserBuilder : UserBuilderBase
    {

        public UserBuilder() : base()
        {

        }


        public override void BuildFirstName(string firstName = "Пользователь")
        {
            User.FirstName = firstName;
        }
        public override void BuildLastName(string lastName = "Иванов")
        {
            User.LastName = lastName;
        }

        public override void BuildDateOfCreate(DateTime date)
        {
            User.DateOfCreate = date;
        }

        public override void BuildDateOfLastChange(DateTime date)
        {
            User.DateOfLastChange = date;
        }

        public override void BuildStatus()
        {
            User.Status = (Tasker.Models.StatusUser)new Random().Next(1, 3);
        }

    }
}
