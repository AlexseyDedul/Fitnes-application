using System;

namespace Fitness.BL.Model
{
    [Serializable]
    public class Exercise
    {
        public DateTime Start { get; }
        public DateTime Finish { get; }
        public Activity Activity { get; }
        public User User { get; }

        public Exercise(DateTime start, DateTime finish, Activity activity, User user)
        {
            if (start < DateTime.Parse("01.01.1970") || start >= DateTime.Now)
            {
                throw new ArgumentException("Невозможная дата начала занятия.", nameof(start));
            }

            if (finish < DateTime.Parse("01.01.1970") /*|| finish >= DateTime.Now*/)
            {
                throw new ArgumentException("Невозможная дата конца занятия.", nameof(finish));
            }

            if(activity == null)
            {
                throw new ArgumentNullException("Активнось не может быть null", nameof(activity));
            }

            if (user == null)
            {
                throw new ArgumentNullException("Пользователь не может быть null", nameof(user));
            }

            Start = start;
            Finish = finish;
            Activity = activity;
            User = user; 
        }
    }
}
