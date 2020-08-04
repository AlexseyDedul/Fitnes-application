using Fitness.BL.Controller;
using Fitness.BL.Model;
using System;

namespace Fitness.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветствует приложение Fitnes");

            Console.WriteLine("Введите имя пользователя");
            var name = Console.ReadLine();

            var useController = new UserController(name);
            var eatingController = new EatingController(useController.CurrentUser);
            if (useController.IsNewUser)
            {
                Console.WriteLine("Введите пол:");
                var gender = Console.ReadLine();
                var birthDate = ParseDateTime();
                var weight = ParseDouble("вес");
                var height = ParseDouble("рост");
                
                useController.SetNewUserData(gender, birthDate, weight, height);
            }
            Console.Write(useController.CurrentUser);

            Console.WriteLine("Что вы хотите сделать?");
            Console.WriteLine("Е - ввести прием пищи");
            var key = Console.ReadKey();
            if(key.Key == ConsoleKey.E)
            {
                var foods = EnterEating();
                eatingController.AddEating(foods.Item1, foods.Item2);

                foreach(var item in eatingController.Eating.Foods)
                {
                    Console.WriteLine($"\t{item.Key} - {item.Value}");
                }
                
            }
            Console.ReadLine();
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

        private static DateTime ParseDateTime()
        {
            DateTime birthDate;
            while (true)
            {
                Console.WriteLine("Введите дату рождения (dd.MM.yyyy):");
                if (DateTime.TryParse(Console.ReadLine(), out birthDate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Не верный формат даты");
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
