using Fitness.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.BL.Controller
{
    public class ExerciseController : ControllerBase
    {
        private const string EXERCISES_FILE_SAVE = "exercise.dat";
        private const string ACTIVITIES_FILE_SAVE = "activities.dat";
        private readonly User user;
        public List<Exercise> Exercises { get; }
        public List<Activity> Activities { get; }

        public ExerciseController(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            this.user = user;
            Exercises = GetAllExeircises();
            Activities = GetAllActivities();
        }

        private List<Activity> GetAllActivities()
        {
            return Load<List<Activity>>(ACTIVITIES_FILE_SAVE) ?? new List<Activity>();
        }

        public void Add(Activity activity, DateTime begin, DateTime end)
        {
            var act = Activities.SingleOrDefault(a => a.Name == activity.Name);

            if(act == null)
            {
                Activities.Add(activity);

                var exercise = new Exercise(begin, end, activity, user);
                Exercises.Add(exercise);
            }
            else
            {
                var exercise = new Exercise(begin, end, act, user);
                Exercises.Add(exercise);
            }
            Save();
        }

        private List<Exercise> GetAllExeircises()
        {
            return Load<List<Exercise>>(EXERCISES_FILE_SAVE) ?? new List<Exercise>();
        }

        private void Save()
        {
            Save(EXERCISES_FILE_SAVE, Exercises);
            Save(ACTIVITIES_FILE_SAVE, Activities);
        }
    }
}
