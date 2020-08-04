using Fitness.BL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Fitness.BL.Controller
{
    /// <summary>
    /// Контроллер пользователя
    /// </summary>
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Файл для сохранения пользователей
        /// </summary>
        private const string USER_FILE_SAVE = "user.dat";
        /// <summary>
        /// Список пользователей приложения
        /// </summary>
        public List<User> Users { get; }

        /// <summary>
        /// Пользователь приложения
        /// </summary>
        public User CurrentUser { get; }

        public bool IsNewUser { get; } = false;
        
        /// <summary>
        /// Создание нового контроллера пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        public UserController(string userName)
        {
            if(string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException("Имя пользователя не может быть пустым", nameof(userName));
            }

            Users = GetUserData();

            CurrentUser = Users.SingleOrDefault(u => u.Name == userName);

            if(CurrentUser == null)
            {
                CurrentUser = new User(userName);
                Users.Add(CurrentUser);
                IsNewUser = true;
                Save();
            }
        }

        /// <summary>
        /// Получить сохраненный список пользователей
        /// </summary>
        /// <returns>Пользователь приложения</returns>
        private List<User> GetUserData()
        {
            return Load<List<User>>(USER_FILE_SAVE) ?? new List<User>();
        }

        /// <summary>
        /// Сохранить данные пользователя приложения
        /// </summary>
        private void Save()
        {
            Save(USER_FILE_SAVE, Users);
        }

        public void SetNewUserData(string genderName, DateTime birthDate, double weight = 1, double height = 1)
        {
            if (genderName == null)
            {
                throw new ArgumentNullException("Пол не может быть null.", nameof(genderName));
            }

            if (birthDate < DateTime.Parse("01.01.1970") || birthDate >= DateTime.Now)
            {
                throw new ArgumentException("Невозможная дата рождения.", nameof(birthDate));
            }

            CurrentUser.Gender = new Gender(genderName);
            CurrentUser.BirthDate = birthDate;
            CurrentUser.Weight = weight;
            CurrentUser.Height = height;
            Save();
        }
    }
}
