using Fitness.BL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.BL.Controller
{
    /// <summary>
    /// Контроллер еды
    /// </summary>
    public class EatingController : ControllerBase
    {
        /// <summary>
        /// Файл для сохранения еды
        /// </summary>
        private const string FOOD_FILE_SAVE = "foods.dat";
        /// <summary>
        /// Файл для сохранения приемов пищи
        /// </summary>
        private const string EATING_FILE_SAVE = "eating.dat";
        /// <summary>
        /// Пользователь
        /// </summary>
        private readonly User User;
        /// <summary>
        /// Список продуктов
        /// </summary>
        public List<Food> Foods { get; }

        public Eating Eating { get; }

        public EatingController(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException("Пользователь не можеть быть null", nameof(user));                
            }

            User = user;
            Foods = GetAllFoods();
            Eating = GetEating();
        }

        public void AddEating(Food food, double weight)
        {
            var product = Foods.SingleOrDefault(f => f.Name == food.Name);
            if(product == null)
            {
                Foods.Add(food);
                Eating.Add(food, weight);
                Save();
            }
            else
            {
                Eating.Add(product, weight);
                Save();
            }
        }

        /// <summary>
        /// Считывание данных о приемах пищи
        /// </summary>
        /// <returns>Список приемов пищи</returns>
        private Eating GetEating()
        {
            return Load<Eating>(EATING_FILE_SAVE) ?? new Eating(User);
        }

        /// <summary>
        /// Считывание данных о еде из файла
        /// </summary>
        /// <returns>Список продуктов</returns>
        private List<Food> GetAllFoods()
        {
            return Load<List<Food>>(FOOD_FILE_SAVE) ?? new List<Food>();
        }

        /// <summary>
        /// Сохранение списка продуктов в файл
        /// </summary>
        private void Save()
        {
            Save(FOOD_FILE_SAVE, Foods);
            Save(EATING_FILE_SAVE, Eating);
        }
    }
}
