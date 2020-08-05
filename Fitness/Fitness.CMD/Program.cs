using Fitness.BL.Controller;
using Fitness.BL.Model;
using System;
using System.Globalization;
using System.Resources;

namespace Fitness.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            var culture = CultureInfo.CreateSpecificCulture("en-us");
            var resourseManager = new ResourceManager("Fitness.CMD.Languages.Messages", typeof(Program).Assembly);

            Console.WriteLine(resourseManager.GetString("Hello", culture));

            Console.WriteLine(resourseManager.GetString("EnterName", culture));
            var name = Console.ReadLine();

            var useController = new UserController(name);
            var eatingController = new EatingController(useController.CurrentUser);
            var exerciseController = new ExerciseController(useController.CurrentUser);

            if (useController.IsNewUser)
            {
                Console.WriteLine(resourseManager.GetString("EnterGender", culture));
                var gender = Console.ReadLine();
                var birthDate = ParseDateTime("дата рождения");
                var weight = ParseDouble("вес");
                var height = ParseDouble("рост");
                
                useController.SetNewUserData(gender, birthDate, weight, height);
            }
            Console.Write(useController.CurrentUser);
            
            while (true)
            {
                Console.WriteLine("Что вы хотите сделать?");
                Console.WriteLine("Е - ввести прием пищи");
                Console.WriteLine("А - ввести упражнение");
                Console.WriteLine("Q - выход");

                var key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.E:
                        {
                            var foods = EnterEating();
                            eatingController.AddEating(foods.Item1, foods.Item2);

                            foreach (var item in eatingController.Eating.Foods)
                            {
                                Console.WriteLine($"\t{item.Key} - {item.Value}");
                            }
                            break;
                        }
                    case ConsoleKey.A:

                        {
                            var exe = EnterExerise();
                            var exercise = new Exercise(exe.Item1, exe.Item2, exe.Item3, useController.CurrentUser);

                            exerciseController.Add(exe.Item3, exe.Item1, exe.Item2);

                            foreach(var item in exerciseController.Exercises)
                            {
                                Console.WriteLine($"\t{item.Activity} c {item.Start.ToShortTimeString()} по {item.Finish.ToShortTimeString()} user {item.User.Name}");
                            }

                            break;
                        }
                    case ConsoleKey.Q:
                        {
                            Environment.Exit(0);
                            break;
                        }
                }
            }
            Console.ReadLine();
        }

        private static Tuple<DateTime, DateTime, Activity> EnterExerise()
        {
            Console.WriteLine("Введите названия упражнения:");
            var name = Console.ReadLine();

            var energy = ParseDouble("расход энергии в минуту");

            var begin = ParseDateTime("начало упражнения");
            var end = ParseDateTime("конец упражнения");

            var activity = new Activity(name, energy);
            return Tuple.Create(begin, end, activity);
        }

        private static Tuple<Food, double> EnterEating()
        {
            Console.WriteLine("Введите имя продукта");
            var food = Console.ReadLine();
            
            var calories = ParseDouble("калорийность");
            var prots = ParseDouble("белки");
            var fats = ParseDouble("жиры");
            var carbs = ParseDouble("углеводы");

            Console.Write("Введите вес порции");
            var weight = ParseDouble("вес порции");
            var product = new Food(food, calories, prots, fats, carbs);

            return Tuple.Create(product, weight);
        }

        private static DateTime ParseDateTime(string value)
        {
            DateTime birthDate;
            while (true)
            {
                Console.WriteLine($"Введите {value} (dd.MM.yyyy):");
                if (DateTime.TryParse(Console.ReadLine(), out birthDate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Не верный формат {value}");
                }
            }

            return birthDate;
        }

        private static double ParseDouble(string name)
        {
            double value;
            while (true)
            {
                Console.Write($"Введите {name}:");
                if (double.TryParse(Console.ReadLine(), out value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine($"Не верный формат {name}а");
                }
            }
        }
    }
}
