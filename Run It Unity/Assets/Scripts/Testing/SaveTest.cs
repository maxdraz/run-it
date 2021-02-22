using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace RunIt.Testing
{
    public class SaveTest : MonoBehaviour
    {
        public Slider slider;
        public float value;
        private string savePath;
        private SensSettings sensSettings;
        private void Awake()
        {
            savePath = Application.dataPath + "/SaveData/Testing/";

            if (!Directory.Exists(savePath))
            {
                CreateDirectory();
            }

            sensSettings = new SensSettings() {sensitivity = 10f};
        }

        private void Start()
        {
            SaveValue();
        }

        private void CreateDirectory()
        {
            Directory.CreateDirectory(savePath);
            print("directory created");
        }

        private void SaveValue()
        {
            var json = JsonUtility.ToJson(sensSettings);
            print(json);
            File.WriteAllText(savePath + "sensitivity.json",json);
            print("saved "+ value + json);
        }

        void Load()
        {
            if (File.Exists(savePath + "sensitivity.json")) ;
            {
                var json = File.ReadAllText(savePath + "sensitivity.json");
                var obj = JsonUtility.FromJson<SensSettings>(json);

                value = obj.sensitivity;
            }
        }
    }

    public class SensSettings
    {
        public float sensitivity;
    }
}