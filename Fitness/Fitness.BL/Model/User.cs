using System;

namespace Fitness.BL.Model
{
    public class User
    {
        public string Name { get; }
        public Gender Gender { get; }
        public DateTime BirthDate { get; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public User(string name, 
                    Gender gender, 
                    DateTime birthDate, 
                    double weigth, 
                    double height)
        {
            #region Проверка условий ввода данных
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Имя пользователя не может быть пустым  или null.", nameof(name));
            }

            if (gender == null)
            {
                throw new ArgumentNullException("Пол не может быть null.", nameof(gender));
            }

            if(birthDate < DateTime.Parse("01.01.1970") || birthDate >= DateTime.Now)
            {
                throw new ArgumentException("Невозможная дата рождения.", nameof(birthDate));
            }

            if(weigth <= 0)
            {
                throw new ArgumentException("Вес не может быть меньше либо равен 0.", nameof(weigth));
            }

            if(height <= 0)
            {
                throw new ArgumentException("Рост не может быть меньше либо равен 0.", nameof(height));
            }
            #endregion

            Name = name;
            Gender = gender;
            BirthDate = birthDate;
            Weight = weigth;
            Height = height;
        }
    }

    public override string ToString()
    {
        return nameof;
    }
}
