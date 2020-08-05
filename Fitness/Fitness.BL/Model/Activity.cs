using System;

namespace Fitness.BL.Model
{
    [Serializable]
    public class Activity
    {
        public string Name { get; set; }
        public double CaloriesPerMinute { get; set; }

        public Activity(string name, double caloriesPerMinute) 
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Имя не может быть пустым", nameof(name));
            }

            if(caloriesPerMinute <= 0)
            {
                throw new ArgumentNullException("Количество не может быть null", nameof(caloriesPerMinute));
            }

            Name = name;
            CaloriesPerMinute = caloriesPerMinute;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
