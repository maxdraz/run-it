using System;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

namespace RunIt.Settings
{
    [System.Serializable]
    public class Settings
    {
        public float sensitivity;
    }
    public class SensitivitySettings : MonoBehaviour
    {
        public static SensitivitySettings Instance;
        public delegate void ValueChangeHandler(float value);
        
        public event ValueChangeHandler ValueChanged;
        [SerializeField] private Slider sensitivitySlider;

        private string saveDirectory; 
        private string savePath;
        [SerializeField] private Settings settings;

        private void Awake()
        {
            saveDirectory = Application.dataPath + "/SaveData/";
            savePath = saveDirectory + "sensitivity.json";
            
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
            
            //load data
            settings = LoadSettings();
            SetSliderValue(settings.sensitivity);
        }

        private void Start()
        {
            OnValueChanged(settings.sensitivity);
        }

        private void OnEnable()
        {
            sensitivitySlider.onValueChanged.AddListener (delegate {OnValueChanged (sensitivitySlider.value);});
        }

        private void OnDestroy()
        {
            sensitivitySlider.onValueChanged.RemoveListener(delegate {OnValueChanged (sensitivitySlider.value);});
            SaveSettings();
        }

        private void OnValueChanged(float newValue)
        {
            var roundedValue = Mathf.Round(newValue * 10) / 10;
            settings.sensitivity = roundedValue;
            ValueChanged?.Invoke(settings.sensitivity);
        }

        private Settings LoadSettings()
        {
            //if no directory
            if (!Directory.Exists(saveDirectory))
                Directory.CreateDirectory(saveDirectory);
            
            //if no file, return default
            if (!File.Exists(savePath))
            {
                File.Create(savePath);
                return new Settings() {sensitivity = sensitivitySlider.value};
            }
            
            //else return obj from file
            var json = File.ReadAllText(savePath);
            var obj = JsonUtility.FromJson<Settings>(json);
           
            return obj;
        }

        private void SaveSettings()
        {
            var json = JsonUtility.ToJson(settings);

            if (Directory.Exists(saveDirectory))
            {
                if (File.Exists(savePath))
                {
                    File.WriteAllText(savePath,json);
                }
            }
        }

        private void SetSliderValue(float val)
        {
            sensitivitySlider.value = val;
        }
    }
}