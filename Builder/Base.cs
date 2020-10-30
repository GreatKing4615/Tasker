using System;

namespace TaskManager.Builder
{
    public abstract class Base
    {
        /// <summary>
        /// Синтетическая генерация дат для данных
        /// </summary>
        /// <returns>дату в формате дд/мм/гггг </returns>
        internal DateTime RndData()
        {
            var rnd = new Random();
            string data = rnd.Next(1, 28).ToString() + "/" + rnd.Next(1, 12).ToString() + "/" + rnd.Next(2015, 2020).ToString();
            return DateTime.Parse(data);
        }
    }
}
