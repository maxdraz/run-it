using System;
using System.IO;
using RunIt.Saving;
using UnityEngine;

namespace RunIt.Settings
{
    public class GameSettings : MonoBehaviour
    {
        public AudioSettings audioSettings;
        public float val;
        private void Awake()
        {
            audioSettings= SaveSystem.Load<AudioSettings>(audioSettings.fullPath);
            
        }

        private void OnDisable()
        {
            SaveSystem.Save(audioSettings,audioSettings.directory,audioSettings.fileName);
        }

        public void SetMasterVolume(float value)
        {
            audioSettings.masterVolume = value;
        }
    }

    [System.Serializable]
    public class AudioSettings
    {
        public readonly string directory = "AudioSettings/";
        public readonly string fileName = "audioSettings.json";
        public readonly string fullPath = "AudioSettings/audioSettings.json";

        public string FileName => fileName;

        public float masterVolume;
        public float musicVolume;
        public float effectsVolume;
    }
    
    [System.Serializable]
    public class Settings
    {
        public float sensitivity;
    }

   
}