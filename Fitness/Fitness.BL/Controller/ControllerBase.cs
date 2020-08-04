using Fitness.BL.Model;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Fitness.BL.Controller
{
    public abstract class ControllerBase
    {
        protected void Save(string fileName, object item)
        {
            var formatter = new BinaryFormatter();

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, item);

            }
        }

        protected T Load<T>(string fileName) where T : class
        {
            var formatter = new BinaryFormatter();

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                if (fs.Length > 0)
                {
                    var items = formatter.Deserialize(fs) as T;

                    return items;
                }
                else
                {
                    return default(T);
                }
            }
        }
    }
}
