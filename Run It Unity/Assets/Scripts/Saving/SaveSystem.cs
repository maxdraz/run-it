using System.IO;
using FMODUnity;
using RunIt.UI;
using UnityEditor;
using UnityEngine;

namespace RunIt.Saving
{
    public static class SaveSystem
    {
        public static readonly string SAVE_DIRECTORY = Application.dataPath + "/SaveData/";

        public static void Save(object obj, string directory, string fileName)
        {
            if (!Directory.Exists(SAVE_DIRECTORY + directory))
            {
                Directory.CreateDirectory(SAVE_DIRECTORY + directory);
            }

            var json = JsonUtility.ToJson(obj);
            var fullPath = SAVE_DIRECTORY + directory + fileName;
            File.WriteAllText(fullPath,json);
        }
        public static T Load<T>(string filePath)
        {
            if (!File.Exists(SAVE_DIRECTORY + filePath)) return default;

            var json = File.ReadAllText(SAVE_DIRECTORY + filePath);
           
           return JsonUtility.FromJson<T>(json);
        }
        
        public static T Load<T>(string dir, string fileName) where T : new()
        {
            if (!Directory.Exists(SAVE_DIRECTORY + dir))
            {
                Directory.CreateDirectory(SAVE_DIRECTORY + dir);
            }

            string json;
            
            if (!File.Exists(SAVE_DIRECTORY + dir + fileName))
            {
                var obj = new T();
                Save(obj,dir,fileName);
            }

            json = File.ReadAllText(SAVE_DIRECTORY + dir + fileName);
           
            return JsonUtility.FromJson<T>(json);
        }
    }
}