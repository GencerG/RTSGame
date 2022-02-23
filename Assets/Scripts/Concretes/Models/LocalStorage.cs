using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace RTSGame.Concretes
{
    public static class LocalStorage
    {
        private static readonly string _persistentDataPath = Application.persistentDataPath;

        /// <summary>
        /// Serializes given object type to json and writes it out to a file with given name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        public static void Save<T>(string fileName, T data)
        {
            var options = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented };
            var jsonString = JsonConvert.SerializeObject(data, options);
            string path = _persistentDataPath + "/" + fileName + ".vm";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.WriteAllText(path, jsonString);
        }

        /// <summary>
        /// Loads json string from file with given and deserializes to given object type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T Load<T>(string fileName)
        {
            string path = _persistentDataPath + "/" + fileName + ".vm";

            if (File.Exists(path))
            {
                var jsonString = File.ReadAllText(path);
                var options = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented };
                var deserializedObject = JsonConvert.DeserializeObject<T>(jsonString, options);
                return deserializedObject;
            }

            return default;
        }

        /// <summary>
        /// Checks persistent data path if file with given name exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool Exists(string name)
        {
            string location = _persistentDataPath + "/" + name + ".vm";
            return File.Exists(location);
        }
    }
}