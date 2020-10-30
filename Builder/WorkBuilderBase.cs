using System;
using Tasker.Models;

namespace TaskManager.Builder
{
    abstract class WorkBuilderBase : Base
    {
        protected Work Work;

        protected WorkBuilderBase()
        {
            Work = new Work();
        }

        public Work GetWork()
        {
            return Work;
        }


        public abstract void BuilderName(string name);
        public abstract void BuilderDesqription(string description);
        public abstract void BuilderDataCreate(DateTime date);
        public abstract void BuilderDataOfLastChange(DateTime date);
        public abstract void BuilderStatus();
    }
}
