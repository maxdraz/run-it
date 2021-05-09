using System;
using System.IO;
using RunIt.Saving;
using UnityEngine;

namespace RunIt.Settings
{
    public class GameSettings : MonoBehaviour
    {
        public static GameSettings Instance;
        private AudioSettings audioSettings;
        [SerializeField] private PlayerSettings playerSettings;

        public PlayerSettings PlayerSettings
        {
            get => playerSettings;
            set => playerSettings = value;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

           // audioSettings = new AudioSettings();
          //  audioSettings = SaveSystem.Load<AudioSettings>(audioSettings.directory,audioSettings.fileName);


          audioSettings = new AudioSettings();
          playerSettings = new PlayerSettings();
        }

        private void Start()
        {
           // if (!File.Exists(playerSettings.fullPath))
          //  {
             //   SaveSystem.Save(playerSettings,playerSettings.directory,playerSettings.fileName);
           // }
           // playerSettings = SaveSystem.Load<PlayerSettings>(playerSettings.fullPath);

           playerSettings = SaveSystem.Load<PlayerSettings>(playerSettings.directory, playerSettings.fileName);
        }

        private void OnDisable()
        {
            //SaveSystem.Save(audioSettings,audioSettings.directory,audioSettings.fileName);
            SaveSystem.Save(playerSettings,playerSettings.directory,playerSettings.fileName);
        }

        public void SetMasterVolume(float value)
        {
            audioSettings.masterVolume = value;
        }

        public void SetPlayerName(string name)
        {
            playerSettings.playerName = name;
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
    public class ControlSettings
    {
        public float sensitivity;
    }
    [System.Serializable]
    public class PlayerSettings
    { 
        public readonly string directory = "PlayerSettings/";
        public readonly string fileName = "playerSettings.json";
        public readonly string fullPath = "PlayerSettings/playerSettings.json";
        
        public string playerName= "";
    }


}