using System;
using Tasker.Models;

namespace TaskManager.Builder
{
    class WorkBuilder : WorkBuilderBase
    {

        public override void BuilderName(string name)
        {
            Work.Name = name;
        }

        public override void BuilderDesqription(string desqriprion)
        {
            Work.Description = desqriprion;
        }

        public override void BuilderDataCreate(DateTime date)
        {
            Work.DateOfCreate = date;
        }


        public override void BuilderDataOfLastChange(DateTime date)
        {
            Work.DateOfLastChange = date;
        }

        public override void BuilderStatus()
        {
            Work.Status = (Tasker.Models.StatusTask)new Random().Next(1, 5);
        }
    }
}
